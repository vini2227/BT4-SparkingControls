using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
sealed class PeSection { public int Rva,VirtualSize,Raw,RawSize,Header; public uint Flags; }
sealed class PeSymbol { public int Rva,Section,Type; public string Scope; }
sealed class PeImage {
    public readonly byte[] Bytes; public readonly long ImageBase; public readonly int SizeOfImage,PeOffset;
    public readonly List<PeSection> Sections=new List<PeSection>();
    readonly Dictionary<string,List<PeSymbol>> candidates=new Dictionary<string,List<PeSymbol>>(StringComparer.Ordinal);
    readonly Dictionary<string,PeSymbol> symbols=new Dictionary<string,PeSymbol>(StringComparer.Ordinal);
    readonly Dictionary<string,int> functionLengths=new Dictionary<string,int>(StringComparer.Ordinal);
    string digest;
    public string Sha256 {get{return digest??(digest=Digest(Bytes));}}
    public static string Digest(byte[] b) { using(var sha=SHA256.Create())return Program.Hex(sha.ComputeHash(b)); }
    void Check(long offset,long size) { if(offset<0 || size<0 || offset+size>Bytes.LongLength)throw new IOException(UiLanguage.Text("Executavel PE truncado ou invalido.")); }
    int I32(int offset) { Check(offset,4);return BitConverter.ToInt32(Bytes,offset); }
    ushort U16(int offset) { Check(offset,2);return BitConverter.ToUInt16(Bytes,offset); }
    uint U32(int offset) { Check(offset,4);return BitConverter.ToUInt32(Bytes,offset); }
    public PeImage(byte[] bytes) {
        Bytes=bytes;
        if(bytes.Length<256 || bytes.Length>300*1024*1024 || U16(0)!=0x5a4d)throw new IOException(UiLanguage.Text("O arquivo escolhido nao e um executavel Windows valido."));
        PeOffset=I32(60);Check(PeOffset,24);
        if(U32(PeOffset)!=0x4550 || U16(PeOffset+4)!=0x8664)throw new IOException(UiLanguage.Text("Esta revisao exige o port Windows x64."));
        int optional=PeOffset+24,optionalSize=U16(PeOffset+20);
        Check(optional,optionalSize);
        if(optionalSize<112 || U16(optional)!=0x20b)throw new IOException(UiLanguage.Text("Formato PE64 inesperado."));
        ImageBase=BitConverter.ToInt64(Bytes,optional+24);SizeOfImage=I32(optional+56);
        if(ImageBase<0x10000 || ImageBase+SizeOfImage>=0x80000000L || SizeOfImage<0x1000)throw new IOException(UiLanguage.Text("Layout de memoria do port fora da faixa suportada."));
        int sectionCount=U16(PeOffset+6),sectionTable=optional+optionalSize;
        if(sectionCount<1 || sectionCount>128)throw new IOException(UiLanguage.Text("Tabela de secoes PE invalida."));
        Check(sectionTable,sectionCount*40);
        for(int i=0;i<sectionCount;i++) {
            int at=sectionTable+i*40;
            var section=new PeSection { Rva=I32(at+12),VirtualSize=I32(at+8),Raw=I32(at+20),RawSize=I32(at+16),Flags=U32(at+36),Header=at };
            if(section.Rva<0 || section.VirtualSize<0 || section.RawSize<0 || (long)section.Rva+Math.Max(section.VirtualSize,section.RawSize)>SizeOfImage)throw new IOException(UiLanguage.Text("Secao PE fora dos limites da imagem."));
            if(section.RawSize>0)Check(section.Raw,section.RawSize);
            Sections.Add(section);
        }
        int table=I32(PeOffset+12),count=I32(PeOffset+16);
        if(table<=0 || count<=0)throw new IOException(UiLanguage.Text("Esta compilacao removeu a tabela de simbolos. O auxiliar nao consegue reconhecer esta versao automaticamente; nenhum patch foi aplicado."));
        if(count>2000000)throw new IOException(UiLanguage.Text("Tabela de simbolos fora dos limites."));
        Check(table,(long)count*18+4);int strings=checked(table+count*18),stringSize=I32(strings);Check(strings,stringSize);
        if(stringSize<4)throw new IOException(UiLanguage.Text("Tabela de nomes PE invalida."));
        string scope="";
        for(int i=0;i<count;) {
            int at=table+i*18;string name;
            if(U32(at)==0) {
                int index=I32(at+4);
                if(index<4 || index>=stringSize)throw new IOException(UiLanguage.Text("Nome de simbolo PE fora dos limites."));
                int begin=strings+index,end=begin;
                while(end<strings+stringSize && Bytes[end]!=0 && end-begin<=1024)end++;
                if(end>=strings+stringSize || end-begin>1024)throw new IOException(UiLanguage.Text("Nome de simbolo PE invalido."));
                name=Encoding.ASCII.GetString(Bytes,begin,end-begin);
            } else {
                int length=0;while(length<8 && Bytes[at+length]!=0)length++;
                name=Encoding.ASCII.GetString(Bytes,at,length);
            }
            short section=BitConverter.ToInt16(Bytes,at+12);int aux=Bytes[at+17];
            if(i+aux>=count)throw new IOException(UiLanguage.Text("Registro auxiliar PE invalido."));
            if(name==".file" && aux>0)scope=Encoding.ASCII.GetString(Bytes,at+18,aux*18).TrimEnd('\0');
            if(section>0 && section<=Sections.Count) {
                long rva=(long)Sections[section-1].Rva+U32(at+8);
                if(rva<0 || rva>=SizeOfImage) { i+=1+aux;continue; } // COFF may include non-address sentinel/debug symbols.
                if(U16(at+14)==32)scope=name;
                var symbol=new PeSymbol { Rva=(int)rva,Section=section,Type=U16(at+14),Scope=scope };
                List<PeSymbol> matches;if(!candidates.TryGetValue(name,out matches))candidates[name]=matches=new List<PeSymbol>();matches.Add(symbol);
                PeSymbol previous;
                if(!symbols.TryGetValue(name,out previous) || symbol.Type==32)symbols[name]=symbol;
            }
            i+=1+aux;
        }
    }
    public bool HasSymbol(string name){return symbols.ContainsKey(name);}
    public int Symbol(string name) {
        if(name=="$image")return 0;PeSymbol s;if(!symbols.TryGetValue(name,out s))throw new IOException(UiLanguage.Text("Atualizacao ainda nao reconhecida: falta o simbolo ")+name+UiLanguage.Text(". Nenhum patch foi aplicado."));
        return s.Rva;
    }
    public bool MatchesSymbol(string name,long rva,string owner,string scope=null) {
        List<PeSymbol> matches;
        if(!candidates.TryGetValue(name,out matches)){
            int dot=name.LastIndexOf('.');int ordinal;
            if(scope==null||dot<1||!int.TryParse(name.Substring(dot+1),out ordinal))return false;
            string prefix=name.Substring(0,dot+1);
            var local=candidates.Where(x=>x.Key.StartsWith(prefix,StringComparison.Ordinal)&&int.TryParse(x.Key.Substring(prefix.Length),out ordinal)).SelectMany(x=>x.Value).Where(x=>x.Type!=32&&x.Scope==scope).ToArray();
            return local.Select(x=>x.Rva).Distinct().Count()==1&&local.Any(x=>x.Rva==rva);
        }
        if(scope!=null)return matches.Any(s=>s.Rva==rva&&s.Scope==scope);
        if(name.StartsWith(".",StringComparison.Ordinal))return Symbol(name)==rva;
        // Local COFF names may repeat across translation units. Resolve them in
        // the referencing function's COFF emission group, never against an unrelated duplicate.
        if(matches.Select(s=>s.Rva).Distinct().Count()>1){PeSymbol function;if(!symbols.TryGetValue(owner,out function)||string.IsNullOrEmpty(function.Scope))return false;return matches.Any(s=>s.Rva==rva&&s.Scope==owner);}
        return matches.Any(s=>s.Rva==rva);
    }
    public bool MatchesLocal(string name,long rva,string scope,int index,int count){
        int dot=name.LastIndexOf('.');if(dot<1||count<1||count>64||index<0||index>=count||scope==null)return false;
        string prefix=name.Substring(0,dot+1);int suffix;
        var local=candidates.Where(x=>x.Key.StartsWith(prefix,StringComparison.Ordinal)&&int.TryParse(x.Key.Substring(prefix.Length),out suffix)).SelectMany(x=>x.Value).Where(x=>x.Type!=32&&x.Scope==scope).Select(x=>x.Rva).Distinct().OrderBy(x=>x).ToArray();
        return local.Length==count&&local[index]==rva;
    }
    public int RawOffset(int rva,int count) {
        foreach(var section in Sections)if(rva>=section.Rva && (long)rva+count<=section.Rva+(long)section.RawSize) {
            int raw=checked(section.Raw+rva-section.Rva);Check(raw,count);return raw;
        }
        throw new IOException(UiLanguage.Text("Trecho de codigo fora de uma secao legivel."));
    }
    public byte[] Read(int rva,int count) { int raw=RawOffset(rva,count);byte[] result=new byte[count];Buffer.BlockCopy(Bytes,raw,result,0,count);return result; }
    public int FunctionLength(string name) {
        int cached;if(functionLengths.TryGetValue(name,out cached))return cached;
        PeSymbol s;Symbol(name);s=symbols[name];
        if(s.Type!=32 || (Sections[s.Section-1].Flags&0x20000000)==0)throw new IOException(UiLanguage.Text("O simbolo ")+name+UiLanguage.Text(" nao aponta para uma funcao executavel."));
        int end=Sections[s.Section-1].Rva+Sections[s.Section-1].RawSize;
        foreach(var other in symbols.Values)if(other.Section==s.Section && other.Type==32 && other.Rva>s.Rva && other.Rva<end)end=other.Rva;
        return functionLengths[name]=end-s.Rva;
    }
}
public sealed class OnlineDataPointer {public int offset;public string symbol,literal;}
public sealed class OnlineReference { public int[] code_offsets;public int local_index,local_count,data_origin_addend;public OnlineDataPointer[] pointers;public int data_length;public string data_mask,data_bytes;public int offset,width,addend,bias; public string symbol,kind,data_expected,symbol_scope; }
public sealed class OnlineSignature { public string name,expected,mask; public int length; public OnlineReference[] references; public OnlineSignature[] alternatives,dependencies; public bool prompt_centered_projection; }
public sealed class PacketContract { public int format;public string epilogue,binding;public string entry,entry_call_symbol,packet;public int entry_call_offset,return_offset;public OnlineSignature caller; }
public sealed class OnlineMetadata {
    public PacketContract packet_contract;
    public int format,entry_offset,trampoline_length,online_addend;
    public string payload_sha256,baseline_game_sha256,online_symbol;public string[] approved_history_sha256;
    public OnlineSignature[] functions;public OnlineReference[] game_references;public string[] globals;
}
static class OnlineCompatibility {
    public static OnlineMetadata Metadata(){return new JavaScriptSerializer {MaxJsonLength=4*1024*1024}.Deserialize<OnlineMetadata>(File.ReadAllText(Path.Combine(Program.Root,"compatibility.json")));}
    static IOException Changed(string name){return new IOException(UiLanguage.Text("Esta versão do jogo não é compatível. Nenhum jogo foi iniciado ou alterado.")+Environment.NewLine+"Changed native interface: "+name);}
    static long Number(byte[] bytes,int offset,int width){if(offset<0||offset+width>bytes.Length)throw Changed("reference bounds");if(width==8)return BitConverter.ToInt64(bytes,offset);if(width==4)return BitConverter.ToInt32(bytes,offset);throw Changed("reference width");}
    static void Put(byte[] bytes,int offset,int width,long value){if(offset<0||offset+width>bytes.Length)throw Changed("payload bounds");byte[] b=width==8?BitConverter.GetBytes(value):width==4?BitConverter.GetBytes(checked((int)value)):null;if(b==null)throw Changed("payload width");Buffer.BlockCopy(b,0,bytes,offset,width);}
    public static void Resolve(string exe,Profile profile){
        Resolve(new PeImage(File.ReadAllBytes(exe)),profile);
    }
    public static void Resolve(PeImage image,Profile profile){
        string report;
        if(VariantSupport.Resolve(image,profile,out report)){File.WriteAllText(Path.Combine(Program.Root,"compatibility-report.txt"),report);return;}
        try{report=ResolveImage(image,profile,Metadata());}
        catch(IOException primary){string legacy=Path.Combine(Program.Root,"compatibility-legacy.json");if(!File.Exists(legacy))throw;var previous=new JavaScriptSerializer {MaxJsonLength=4*1024*1024}.Deserialize<OnlineMetadata>(File.ReadAllText(legacy));try{report=ResolveImage(image,profile,previous);}catch(IOException secondary){throw new IOException(primary.Message+Environment.NewLine+"Legacy profile: "+secondary.Message,primary);}}

        File.WriteAllText(Path.Combine(Program.Root,"compatibility-report.txt"),report);
    }
    internal static OnlineSignature ReviewedSignature(PeImage image,OnlineSignature signature) {
        try { CheckSignature(image,signature);return signature; }
        catch(IOException original) {
            if(signature.alternatives==null||signature.alternatives.Length>4)throw;
            foreach(var alternative in signature.alternatives){
                if(alternative==null||alternative.name!=signature.name||alternative.alternatives!=null||alternative.dependencies!=null&&alternative.dependencies.Length>4)continue;
                try {
                    CheckSignature(image,alternative);
                    foreach(var dependency in alternative.dependencies??new OnlineSignature[0])CheckSignature(image,dependency);
                    return alternative;
                }catch(IOException) { }
            }
            throw original;
        }
    }
    internal static void CheckSignature(PeImage image,OnlineSignature signature) {
        int rva=image.Symbol(signature.name);byte[] expected=Program.Unhex(signature.expected),mask=Program.Unhex(signature.mask);
        if(signature.length!=expected.Length||mask.Length!=expected.Length||signature.length<5||image.FunctionLength(signature.name)!=signature.length)throw Changed(signature.name+" length");
        byte[] actual=image.Read(rva,signature.length);
        for(int i=0;i<actual.Length;i++)if((actual[i]&mask[i])!=(expected[i]&mask[i]))throw Changed(signature.name+" + 0x"+i.ToString("X"));
        foreach(var reference in signature.references){
            long value=Number(actual,reference.offset,reference.width);
            long target=reference.kind=="relative"?image.ImageBase+rva+reference.offset+reference.bias+value:value;
            if(reference.code_offsets!=null){
                long location=target-image.ImageBase;int length=checked(reference.code_offsets.Length*8);
                if(length<8||length>1024||!image.Sections.Any(x=>location>=x.Rva&&location+length<=x.Rva+(long)x.RawSize&&(x.Flags&0xa0000000)==0))throw Changed(signature.name+" code table location");
                byte[] table=image.Read((int)location,length);
                for(int k=0;k<reference.code_offsets.Length;k++){int offset=reference.code_offsets[k];if(offset<0||offset>=signature.length||Number(table,k*8,8)!=image.ImageBase+rva+offset)throw Changed(signature.name+" code table target");}
            }else if(reference.pointers!=null){
                long location=target-image.ImageBase-reference.data_origin_addend;
                if(!image.MatchesSymbol(reference.symbol,location-reference.addend+reference.data_origin_addend,signature.name,reference.symbol_scope)||reference.data_length<8||reference.data_length>1024||!image.Sections.Any(x=>location>=x.Rva&&location+reference.data_length<=x.Rva+(long)x.RawSize&&(x.Flags&0xa0000000)==0))throw Changed(signature.name+" pointer table location");
                byte[] table=image.Read((int)location,reference.data_length),expectedTable=Program.Unhex(reference.data_bytes),tableMask=Program.Unhex(reference.data_mask);
                if(expectedTable.Length!=table.Length||tableMask.Length!=table.Length)throw Changed("pointer table metadata");
                for(int k=0;k<table.Length;k++)if((table[k]&tableMask[k])!=(expectedTable[k]&tableMask[k]))throw Changed(signature.name+" pointer table value");
                foreach(var pointer in reference.pointers){long valueInTable=Number(table,pointer.offset,8)-image.ImageBase;
                    if(pointer.symbol!=null){if(!image.MatchesSymbol(pointer.symbol,valueInTable,pointer.symbol))throw Changed(signature.name+" pointer target "+pointer.symbol);}
                    else{byte[] text=Program.Unhex(pointer.literal);if(valueInTable<0||valueInTable>int.MaxValue||!image.Sections.Any(x=>valueInTable>=x.Rva&&valueInTable+text.Length<=x.Rva+(long)x.RawSize&&(x.Flags&0xa0000000)==0)||!image.Read((int)valueInTable,text.Length).SequenceEqual(text))throw Changed(signature.name+" pointer string");}
                }
            }else if(!string.IsNullOrEmpty(reference.data_expected)){
                byte[] content=Program.Unhex(reference.data_expected);long location=target-image.ImageBase;
                if(location<0||location>int.MaxValue||!image.Sections.Any(section=>location>=section.Rva&&location+content.Length<=section.Rva+(long)section.RawSize&&(section.Flags&0xa0000000)==0)||!image.Read((int)location,content.Length).SequenceEqual(content))throw Changed(signature.name+" literal bytes");
            }else if(reference.local_count>0?!image.MatchesLocal(reference.symbol,target-image.ImageBase-reference.addend,reference.symbol_scope,reference.local_index,reference.local_count):!image.MatchesSymbol(reference.symbol,target-image.ImageBase-reference.addend,signature.name,reference.symbol_scope))throw Changed(signature.name+" target "+reference.symbol);
        }
    }
    // Only the public cached-state ABI is an add-on dependency. The native
    // initializer remains native and may add connection types/options. Decode a
    // bounded, explicit x64 fast path; never execute or scan arbitrary game code.
    internal static void CheckNetActiveContract(PeImage image,OnlineMetadata metadata) {
        if(metadata.online_symbol!="sMode"||metadata.online_addend!=0)throw Changed("online state contract metadata");
        int rva=image.Symbol("Port_NetActive"),length=image.FunctionLength("Port_NetActive");
        if(length<64||length>32768)throw Changed("online state contract bounds");
        byte[] code=image.Read(rva,Math.Min(length,128));int at=0;var pushes=new List<int>();
        while(at<16){
            int reg=-1,used=0;
            if(code[at]>=0x50&&code[at]<=0x57){reg=code[at]-0x50;used=1;}
            else if(code[at]==0x41&&code[at+1]>=0x50&&code[at+1]<=0x57){reg=8+code[at+1]-0x50;used=2;}
            if(reg<0)break;
            if(!(reg==3||reg==5||reg==6||reg==7||reg>=12)||pushes.Contains(reg))throw Changed("online state saved registers");
            pushes.Add(reg);at+=used;
        }
        if(code[at++]!=0x48||code[at++]!=0x83||code[at++]!=0xec)throw Changed("online state stack ABI");
        int stack=code[at++];if(stack<32||stack>120||((8-pushes.Count*8-stack)&15)!=0)throw Changed("online state stack alignment");
        bool high=code[at]==0x44;if(high)at++;
        if(code[at++]!=0x8b)throw Changed("online state load opcode");
        int modrm=code[at++],regIndex=((modrm>>3)&7)+(high?8:0);
        if((modrm&0xc7)!=5)throw Changed("online state load addressing");
        long target=(long)rva+at+4+BitConverter.ToInt32(code,at);at+=4;
        if(!image.MatchesSymbol("sMode",target,"Port_NetActive")||!image.Sections.Any(s=>target>=s.Rva&&target+4<=s.Rva+(long)s.VirtualSize&&(s.Flags&0x80000000)!=0))throw Changed("online state load target");
        if(high&&code[at++]!=0x45)throw Changed("online state test register prefix");
        if(code[at++]!=0x85||code[at++]!=(0xc0|((regIndex&7)<<3)|(regIndex&7)))throw Changed("online state test register");
        long initializer;
        if(code[at]==0x78){at++;initializer=at+1+(sbyte)code[at];at++;}
        else if(code[at]==0x0f&&code[at+1]==0x88){at+=2;initializer=(long)at+4+BitConverter.ToInt32(code,at);at+=4;}
        else throw Changed("online state signed branch");
        byte[] result={0x0f,0x9f,0xc0,0x0f,0xb6,0xc0,0x48,0x83,0xc4,(byte)stack};
        foreach(byte value in result)if(code[at++]!=value)throw Changed("online state boolean return");
        for(int i=pushes.Count-1;i>=0;i--){int reg=pushes[i];if(reg>=8&&code[at++]!=0x41)throw Changed("online state restore prefix");if(code[at++]!=0x58+(reg&7))throw Changed("online state restore register");}
        if(code[at++]!=0xc3||initializer<at||initializer>=length)throw Changed("online state return boundary");
    }
    // The original reader still executes normally. Only its returned local packet is
    // translated. Its device/keyboard collection body is not an auxiliary dependency.
    // Bound this fallback to a reviewed Windows ABI and exact packet-producing block,
    // including the shared epilogue, and require the unchanged net sampling caller.
    static void CheckPacketContract(PeImage image,PacketContract c) {
        if(c.format==4){Bt4PacketBoundary.Check(image,c);return;}
        int rva=image.Symbol("Port_PadRead"),length=image.FunctionLength("Port_PadRead");
        if(length<256||length>32768||c.entry_call_offset!=0x5b||c.return_offset!=0x6a||c.caller==null||c.caller.name!="sample_local")throw Changed("input contract metadata");
        byte[] actual=image.Read(rva,length),entry=Program.Unhex(c.entry),packet=Program.Unhex(c.packet);
        if(entry.Length!=160||packet.Length!=91)throw Changed("input contract bounds");
        for(int i=0;i<entry.Length;i++)if((i<c.entry_call_offset||i>=c.entry_call_offset+4)&&actual[i]!=entry[i])throw Changed("Port_PadRead entry/ABI");
        if(rva+c.entry_call_offset+4+BitConverter.ToInt32(actual,c.entry_call_offset)!=image.Symbol(c.entry_call_symbol))throw Changed("Port_PadRead entry target");
        int found=-1;
        for(int i=entry.Length;i+packet.Length+5<=actual.Length;i++) {
            bool equal=true;for(int j=0;j<packet.Length;j++)if(actual[i+j]!=packet[j]){equal=false;break;}
            if(!equal)continue;
            if(found>=0)throw Changed("ambiguous packet writer");found=i;
        }
        if(found<0||actual[found+packet.Length]!=0xe9||found+packet.Length+5+BitConverter.ToInt32(actual,found+packet.Length+1)!=c.return_offset)throw Changed("Port_PadRead packet/return");
        CheckSignature(image,c.caller);
    }
    public static string ResolveImage(PeImage image,Profile profile,OnlineMetadata metadata,string resourceRoot=null){
        byte[] template=File.ReadAllBytes(Path.Combine(resourceRoot??Program.Root,"SparkingControls.payload"));
        if(metadata.format!=2||metadata.payload_sha256!=profile.payload_sha256||PeImage.Digest(template)!=metadata.payload_sha256||metadata.functions==null||metadata.functions.Length<50||metadata.game_references==null||(resourceRoot==null?metadata.game_references.Length!=29:metadata.game_references.Length<24||metadata.game_references.Length>64)||metadata.entry_offset<0x1000||metadata.entry_offset>=0xe000||metadata.trampoline_length!=6||image.ImageBase!=profile.image_base)throw Changed("compatibility metadata");
        var report=new StringBuilder();report.AppendLine("Automatic resolution: COFF symbols, native interface signatures, bounded input/state ABI contracts and symbolic reference targets.");report.AppendLine("Game SHA256: "+image.Sha256);
        // Preserve exact historical executables accepted by the previous release.
        // A full-file cryptographic match is required here. Unknown hashes still
        // pass every instruction, address-target and literal-content check below.
        bool approvedHistory=metadata.approved_history_sha256!=null&&metadata.approved_history_sha256.Contains(image.Sha256);
        bool centeredPrompt=false;
        foreach(var signature in metadata.functions){
            if(approvedHistory){report.AppendLine("PASS exact historical executable: "+signature.name);continue;}
            try {
                var selected=ReviewedSignature(image,signature);
                if(signature.name=="Port_PadRead"&&selected!=signature)Bt4PacketBoundary.CheckAbi(image,metadata.packet_contract);
                if(signature.name=="HudNode_Draw")centeredPrompt=selected.prompt_centered_projection;
                if(selected!=signature)report.AppendLine("PASS reviewed native variant with complete instruction/reference and dependency checks: "+signature.name);
            }
            catch(IOException) {
                if(signature.name=="Port_NetActive"){
                    CheckNetActiveContract(image,metadata);
                    report.AppendLine("PASS online-state boundary contract: signed sMode cache, boolean return, Windows x64 stack/register preservation; native initializer may differ.");
                }else{
                    if(signature.name!="Port_PadRead" || metadata.packet_contract==null)throw;
                    CheckPacketContract(image,metadata.packet_contract);
                    report.AppendLine("PASS input boundary contract: packet ABI, buffer layout, return path and online sampling caller; input collection implementation may differ.");
                }
            }
            report.AppendLine("PASS "+signature.name+" @ 0x"+image.Symbol(signature.name).ToString("X"));
        }
        byte[] payload=(byte[])template.Clone();
        foreach(var reference in metadata.game_references)Put(payload,reference.offset,reference.width,image.ImageBase+image.Symbol(reference.symbol)+reference.addend);
        int site=image.Symbol("Port_PadRead"),length=metadata.trampoline_length;byte[] prologue=image.Read(site,length);
        // This is the complete sequence of three pushes; it has no RIP-relative operands.
        if(Program.Hex(prologue)!="415741564155")throw Changed("Port_PadRead trampoline");
        Buffer.BlockCopy(prologue,0,payload,0xe000,length);payload[0xe000+length]=0xe9;
        Put(payload,0xe000+length+1,4,image.ImageBase+site+length-(profile.allocation+0xe000+length+5));
        byte[] jump=new byte[]{0xe9,0,0,0,0,0x90};Put(jump,1,4,profile.allocation+metadata.entry_offset-(image.ImageBase+site+5));
        var globals=new Dictionary<string,int>();foreach(string name in metadata.globals)globals[name]=image.Symbol(name);
        globals["online_mode"]=checked(image.Symbol(metadata.online_symbol)+metadata.online_addend);
        profile.patches=new[]{new Patch {rva=site,expected=Program.Hex(prologue),replacement=Program.Hex(jump)}};
        profile.symbols=globals;profile.resolved_payload=payload;profile.game_sha256=image.Sha256;profile.prompt_centered_projection=centeredPrompt;
        report.AppendLine("PASS: one local packet hook, rebased payload references, all HUD globals and the native online status.");
        report.AppendLine("No game file changed. Resolution occurs at launch/relaunch, never in the input loop. Structural compatibility is not an online gameplay test.");return report.ToString();
    }
}
static class OnlineCompatibilityTests {
    static long NumberForTest(byte[] b,int o,int w){return w==8?BitConverter.ToInt64(b,o):BitConverter.ToInt32(b,o);}
    static void Assert(bool value,string text){if(!value)throw new Exception("FAILED: "+text);}
    public static int Run(string exe){
        var log=new StringBuilder();try{
            var bytes=File.ReadAllBytes(exe);var image=new PeImage(bytes);var metadata=OnlineCompatibility.Metadata();var baseline=Program.LoadProfile();
            log.AppendLine(OnlineCompatibility.ResolveImage(image,baseline,metadata));
            var normal=Program.LoadProfile();Program.Validate(exe,normal);Assert(normal.resolved_payload!=null||normal.game_sha256==PeImage.Digest(bytes),"normal launcher validation resolves or exactly matches current executable");
            var changed=(byte[])bytes.Clone();changed[image.PeOffset+8]^=0x5a;var stamp=Program.LoadProfile();OnlineCompatibility.ResolveImage(new PeImage(changed),stamp,metadata);Assert(stamp.game_sha256!=baseline.game_sha256,"different timestamp/hash accepted");log.AppendLine("PASS: changed hash accepted without editing a whitelist.");
            var moved=(byte[])bytes.Clone();const int delta=0x10000;
            foreach(var section in image.Sections)Buffer.BlockCopy(BitConverter.GetBytes(section.Rva+delta),0,moved,section.Header+12,4);
            Buffer.BlockCopy(BitConverter.GetBytes(image.SizeOfImage+delta),0,moved,image.PeOffset+24+56,4);
            // Relative references move with their containing section. Absolute references need the same delta.
            var selectedFunctions=metadata.functions.Where(f=>f.name!="Port_PadRead").Select(f=>OnlineCompatibility.ReviewedSignature(image,f)).ToArray();
            // A reviewed collector variant can contain absolute tables/fields;
            // relocate its actual references too. Older bounded collectors do
            // not need a complete matching instruction signature here.
            try{var originalPacket=metadata.functions.First(f=>f.name=="Port_PadRead");var selectedPacket=OnlineCompatibility.ReviewedSignature(image,originalPacket);if(selectedPacket!=originalPacket)selectedFunctions=selectedFunctions.Concat(new[]{selectedPacket}).ToArray();}catch(IOException){}
            var selectedWithDependencies=selectedFunctions.Concat(selectedFunctions.SelectMany(f=>f.dependencies??new OnlineSignature[0])).ToArray();
            foreach(var f in selectedWithDependencies.Concat(metadata.packet_contract==null?new OnlineSignature[0]:new[]{metadata.packet_contract.caller}))foreach(var reference in f.references)if(reference.kind=="absolute"){
                int off=image.RawOffset(image.Symbol(f.name)+reference.offset,reference.width);
                if(reference.width==4)Buffer.BlockCopy(BitConverter.GetBytes(checked(BitConverter.ToInt32(moved,off)+delta)),0,moved,off,4);
                else Buffer.BlockCopy(BitConverter.GetBytes(BitConverter.ToInt64(moved,off)+delta),0,moved,off,8);
            }
            foreach(var f in selectedWithDependencies)foreach(var reference in f.references)if(reference.pointers!=null||reference.code_offsets!=null){
                byte[] original=image.Read(image.Symbol(f.name),f.length);long v=NumberForTest(original,reference.offset,reference.width);long target=reference.kind=="relative"?image.Symbol(f.name)+reference.offset+reference.bias+v:v-image.ImageBase;
                if(reference.code_offsets!=null)for(int k=0;k<reference.code_offsets.Length;k++){int off=image.RawOffset((int)target+k*8,8);Buffer.BlockCopy(BitConverter.GetBytes(BitConverter.ToInt64(bytes,off)+delta),0,moved,off,8);}
                foreach(var pointer in reference.pointers??new OnlineDataPointer[0]){int off=image.RawOffset((int)target-reference.data_origin_addend+pointer.offset,8);Buffer.BlockCopy(BitConverter.GetBytes(BitConverter.ToInt64(bytes,off)+delta),0,moved,off,8);}
            }
            var shifted=Program.LoadProfile();OnlineCompatibility.ResolveImage(new PeImage(moved),shifted,metadata);
            Assert(shifted.patches.Length==1&&shifted.patches[0].rva==baseline.patches[0].rva+delta,"moved packet hook");
            foreach(string name in baseline.symbols.Keys)Assert(shifted.symbols[name]==baseline.symbols[name]+delta,"moved HUD "+name);
            foreach(var reference in metadata.game_references){int a=BitConverter.ToInt32(baseline.resolved_payload,reference.offset),b=BitConverter.ToInt32(shifted.resolved_payload,reference.offset);Assert(b==a+delta,"rebased external target");}
            foreach(long allocation in new long[]{0x13000000,0x14000000,0x60000000}){
                byte[] relocated=Program.RelocatePayload(shifted,allocation);int tail=0xe000+metadata.trampoline_length;
                Assert(allocation+tail+5+BitConverter.ToInt32(relocated,tail+1)==image.ImageBase+shifted.patches[0].rva+metadata.trampoline_length,"trampoline after both relocations");
                foreach(var reference in metadata.game_references)Assert(BitConverter.ToInt32(relocated,reference.offset)==image.ImageBase+new PeImage(moved).Symbol(reference.symbol)+reference.addend,"external reference unaffected by allocation");
            }
            log.AppendLine("PASS: moved executable sections rebuild hook, trampoline, payload targets and HUD at three allocation addresses.");
            foreach(string name in new[]{"Port_PadRead","BtlInput_BuildCommands","BtlAct_CanFuse","Port_NetActive"}){
                var bad=(byte[])bytes.Clone();bad[image.RawOffset(image.Symbol(name),1)]^=1;bool refused=false;try{OnlineCompatibility.ResolveImage(new PeImage(bad),Program.LoadProfile(),metadata);}catch(IOException){refused=true;}Assert(refused,"changed instructions "+name);
            }
            if(metadata.packet_contract!=null && metadata.packet_contract.format!=4 && image.FunctionLength("Port_PadRead")!=metadata.functions.First(f=>f.name=="Port_PadRead").length){
                var padded=(byte[])bytes.Clone();int padEnd=image.RawOffset(image.Symbol("Port_PadRead")+image.FunctionLength("Port_PadRead")-1,1);
                Assert(padded[padEnd]==0x90,"fixture trailing alignment NOP");padded[padEnd]=0xcc;
                OnlineCompatibility.ResolveImage(new PeImage(padded),Program.LoadProfile(),metadata);
                foreach(string hex in new[]{metadata.packet_contract.packet,"b812000000"}) {
                    byte[] pattern=Program.Unhex(hex);var bad=(byte[])bytes.Clone();int loc=-1,begin=image.RawOffset(image.Symbol("Port_PadRead"),image.FunctionLength("Port_PadRead"));
                    for(int i=begin;i<begin+image.FunctionLength("Port_PadRead")-pattern.Length;i++)if(bad.Skip(i).Take(pattern.Length).SequenceEqual(pattern)){loc=i;break;}
                    Assert(loc>=0,"packet fixture located");bad[loc]^=1;bool refused=false;try{OnlineCompatibility.ResolveImage(new PeImage(bad),Program.LoadProfile(),metadata);}catch(IOException){refused=true;}Assert(refused,"packet ABI mutation rejected");
                }
                var netBad=(byte[])bytes.Clone();netBad[image.RawOffset(image.Symbol("sample_local"),1)]^=1;bool netRefused=false;try{OnlineCompatibility.ResolveImage(new PeImage(netBad),Program.LoadProfile(),metadata);}catch(IOException){netRefused=true;}Assert(netRefused,"online sampling mutation rejected");
                var duplicate=metadata.functions.First(f=>f.name=="Port_NetActive").references.First(r=>r.symbol=="k_.128");
                var wrong=(byte[])bytes.Clone();int fieldAt=image.RawOffset(image.Symbol("Port_NetActive")+duplicate.offset,4);
                long wrongRva=image.Symbol(duplicate.symbol)+duplicate.addend;
                long wrongDisp=wrongRva-(image.Symbol("Port_NetActive")+duplicate.offset+duplicate.bias);
                Buffer.BlockCopy(BitConverter.GetBytes(checked((int)wrongDisp)),0,wrong,fieldAt,4);
                bool duplicateRefused=false;try{OnlineCompatibility.CheckSignature(new PeImage(wrong),metadata.functions.First(f=>f.name=="Port_NetActive"));}catch(IOException){duplicateRefused=true;}Assert(duplicateRefused,"same-named local from another function rejected");
                log.AppendLine("PASS: strict signature reference checks resolve duplicate COFF names in their owner group. Native initializer-only changes may use the bounded online-state contract instead.");
                log.AppendLine("PASS: input-reader alignment changes accepted; packet format, return and online sampling changes rejected.");
            }
            var redirect=(byte[])bytes.Clone();var owner=metadata.functions.First(f=>f.references.Length>0);var field=owner.references[0];redirect[image.RawOffset(image.Symbol(owner.name)+field.offset,1)]^=1;bool rejected=false;try{OnlineCompatibility.ResolveImage(new PeImage(redirect),Program.LoadProfile(),metadata);}catch(IOException){rejected=true;}Assert(rejected,"changed masked target rejected");
            var stripped=(byte[])bytes.Clone();Buffer.BlockCopy(new byte[4],0,stripped,image.PeOffset+16,4);rejected=false;try{OnlineCompatibility.ResolveImage(new PeImage(stripped),Program.LoadProfile(),metadata);}catch(IOException){rejected=true;}Assert(rejected,"stripped symbols rejected");
            log.AppendLine("PASS: changed native instructions, redirected references and stripped symbols rejected before launching.");
            File.WriteAllText(Path.Combine(Program.Root,"compatibility-test-results.txt"),log.ToString());
            File.WriteAllText(Path.Combine(Program.Root,"resolved-test-profile.json"),new JavaScriptSerializer().Serialize(baseline));
            File.WriteAllBytes(Path.Combine(Program.Root,"resolved-test.payload"),baseline.resolved_payload);
            return 0;
        }catch(Exception ex){log.AppendLine(ex.ToString());File.WriteAllText(Path.Combine(Program.Root,"compatibility-test-results.txt"),log.ToString());return 2;}
    }
}




// Bounded symbolic decoder for the native 18-byte packet writer. Never executes
// game code, never scans process memory, and runs only during launch resolution.
static class Bt4PacketBoundary {
 static void Require(bool ok,string detail){if(!ok)throw new IOException("Changed native interface: BT4 input boundary "+detail);}
 static int Find(byte[] code,byte[] pattern,int begin,int limit){int found=-1;for(int i=begin;i+pattern.Length<=Math.Min(limit,code.Length);i++){bool equal=true;for(int k=0;k<pattern.Length;k++)if(code[i+k]!=pattern[k]){equal=false;break;}if(equal){if(found>=0)return -2;found=i;}}return found;}
 public static void Check(PeImage image,PacketContract c){
  int exit=CheckAbi(image,c);
  int rva=image.Symbol("Port_PadRead"),length=image.FunctionLength("Port_PadRead");
  byte[] code=image.Read(rva,length),entry=Program.Unhex(c.entry);
  var exits=new HashSet<int>();
  for(int i=entry.Length;i<code.Length-5;i++)if(code[i]==0xb8&&code[i+1]==18&&code[i+2]==0&&code[i+3]==0&&code[i+4]==0)
   for(int begin=Math.Max(entry.Length,i-160);begin<=i;begin++){int socket,output,width;int binding=FindBinding(code,entry.Length,128);ReadBinding(code,binding,out socket,out output,out width);var decoder=new Writer(code,begin,Math.Min(code.Length,i+128),exit,output);int finish=decoder.Run();if(finish>=0)exits.Add(finish);}
  Require(exits.Count==1,"18-byte writer/return (buttons, four axes, zero padding)");
 }
 internal static void CheckSupported(PeImage image,OnlineMetadata metadata){
  try{Check(image,metadata.packet_contract);}catch(IOException){
   var original=metadata.functions.First(f=>f.name=="Port_PadRead");var selected=OnlineCompatibility.ReviewedSignature(image,original);
   Require(selected!=original,"reviewed auxiliary packet writer");CheckAbi(image,metadata.packet_contract);
  }
 }
 internal static int CheckAbi(PeImage image,PacketContract c){
  int rva=image.Symbol("Port_PadRead"),length=image.FunctionLength("Port_PadRead");
  Require(length>=256&&length<=32768&&c.caller!=null&&c.caller.name=="sample_local","bounds/caller");
  byte[] code=image.Read(rva,length),entry=Program.Unhex(c.entry),epilogue=Program.Unhex(c.epilogue),binding=Program.Unhex(c.binding);
  Require(entry.Length==53&&epilogue.Length==54&&binding.Length==5,"metadata");
  Require(code.Take(entry.Length).SequenceEqual(entry),"Win64 register/stack save");
  Require(FindBinding(code,entry.Length,128)>=0,"socket/output arguments");
  int exit=Find(code,epilogue,entry.Length,256);Require(exit>=0,"Win64 register/stack restore");
  OnlineCompatibility.CheckSignature(image,c.caller);
  return exit;
 }
 internal static int FindBinding(byte[] code,int begin,int limit){
  int found=-1;
  for(int i=begin;i<Math.Min(limit,code.Length)-4;i++){
   int socket,output,width;
   if(!ReadBinding(code,i,out socket,out output,out width)||i+width>limit)continue;
   if(found>=0)return -2;found=i;i+=width-1;
  }
  return found;
 }
 static bool Preserved(int reg){return reg==3||reg==5||reg==6||reg==7||reg>=12&&reg<=15;}
 internal static bool ReadBinding(byte[] code,int at,out int socket,out int output,out int width){
  socket=output=-1;width=0;if(at<0||at+5>code.Length)return false;int begin=at,high=0;
  if(code[at]==0x41){high=8;at++;}
  if(at+5>code.Length||code[at]!=0x89||(code[at+1]&0xf8)!=0xc8)return false;
  socket=(code[at+1]&7)+high;at+=2;
  int rex=code[at++];if((rex!=0x48&&rex!=0x49)||code[at++]!=0x89||(code[at]&0xf8)!=0xd0)return false;
  output=(code[at++]&7)+(rex==0x49?8:0);width=at-begin;
  return Preserved(socket)&&Preserved(output)&&socket!=output;
 }
 sealed class Writer {
  readonly byte[] b;readonly int limit,exit,packetRegister;int at;bool bad;
  readonly string[] registers=new string[16],output=new string[18];
  public Writer(byte[] bytes,int begin,int end,int epilogue,int packet){b=bytes;at=begin;limit=end;exit=epilogue;packetRegister=packet;for(int i=0;i<16;i++)registers[i]="r"+i;registers[4]="stack";registers[packetRegister]="packet";}
  int Byte(){if(at>=limit){bad=true;return 0;}return b[at++];}
  int Int(){if(at>limit-4){bad=true;return 0;}int value=BitConverter.ToInt32(b,at);at+=4;return value;}
  void Assign(int reg,string value){if(reg==4||reg==packetRegister){bad=true;return;}registers[reg]=value;}
  bool Memory(int mod,int rm,int rex,out int address,out bool stack){
   address=0;stack=false;if(mod==3)return false;
   int basis=rm+((rex&1)!=0?8:0);
   if(rm==4){int sib=Byte();if(sib!=0x24||(rex&2)!=0)return false;basis=4+((rex&1)!=0?8:0);}
   if(mod==0&&rm==5)return false;
   if(mod==1)address=(sbyte)Byte();else if(mod==2)address=Int();
   if(basis!=4&&basis!=packetRegister)return false;stack=basis==4;return !bad;
  }
  void Store(int offset,int width,string value){
   if(offset<0||offset>18-width){bad=true;return;}
   if(value=="0"){for(int i=0;i<width;i++)output[offset+i]="0";return;}
   if(width==2&&value.StartsWith("~r",StringComparison.Ordinal)){output[offset]="lo:"+value;output[offset+1]="hi:"+value;return;}
   if(width==1&&value.Length==2&&value[0]=='a'){output[offset]=value;return;}
   bad=true;
  }
  bool Complete(){return registers[0]=="18"&&output[0]!=null&&output[0].StartsWith("lo:~r",StringComparison.Ordinal)&&output[1]=="hi:"+output[0].Substring(3)&&output[2]=="a0"&&output[3]=="a1"&&output[4]=="a2"&&output[5]=="a3"&&output.Skip(6).All(x=>x=="0");}
  public int Run(){
   for(int count=0;count<64&&!bad&&at<limit;count++){
    bool word=false;int rex=0,op=Byte();if(op==0x66){word=true;op=Byte();}if(op>=0x40&&op<=0x4f){rex=op&15;op=Byte();}
    int width=word?2:(rex&8)!=0?8:4;
    if(op==0xe9){int displacement=Int();return !bad&&at+displacement==exit&&Complete()?at-5:-1;}
    if(op>=0xb8&&op<=0xbf){if(width!=4)return -1;int value=Int();Assign(op-0xb8+((rex&1)!=0?8:0),value.ToString(System.Globalization.CultureInfo.InvariantCulture));continue;}
    if(op==0x0f){if(Byte()!=0xb6||word||(rex&8)!=0)return -1;int field=Byte(),address;bool stack;int reg=((field>>3)&7)+((rex&4)!=0?8:0);if(!Memory(field>>6,field&7,rex,out address,out stack)||!stack||address<0x94||address>0x97)return -1;Assign(reg,"a"+(address-0x94));continue;}
    if(op!=0x89&&op!=0x88&&op!=0x31&&op!=0xf7&&op!=0xc7)return -1;
    int modrm=Byte(),mod=modrm>>6,regId=((modrm>>3)&7)+((rex&4)!=0?8:0),rm=modrm&7,dest=rm+((rex&1)!=0?8:0);
    if(op==0x31){if(mod!=3||regId!=dest)return -1;Assign(dest,"0");continue;}
    if(op==0xf7){if(mod!=3||((modrm>>3)&7)!=2||width!=4)return -1;string value=registers[dest];if(!value.StartsWith("r",StringComparison.Ordinal))return -1;Assign(dest,"~"+value);continue;}
    if(mod==3){if(op!=0x89||width!=4)return -1;Assign(dest,registers[regId]);continue;}
    int offset;bool isStack;if(!Memory(mod,rm,rex,out offset,out isStack)||isStack)return -1;
    if(op==0xc7){if(((modrm>>3)&7)!=0||word)return -1;int value=Int();if(value!=0)return -1;Store(offset,width,"0");continue;}
    if(op==0x88){if(rex==0&&regId>=4)return -1;Store(offset,1,registers[regId]);}else Store(offset,width,registers[regId]);
   }
   return -1;
  }
 }
}
