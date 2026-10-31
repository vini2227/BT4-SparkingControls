using System;using System.IO;using System.Diagnostics;using System.Linq;
static class Bt4BoundaryFixture {
 static int checks;static PacketContract contract;
 static int Locate(byte[] bytes,byte[] pattern){for(int i=0;i<=bytes.Length-pattern.Length;i++)if(bytes.Skip(i).Take(pattern.Length).SequenceEqual(pattern))return i;throw new Exception("Mutation pattern not found");}
 static void Reject(byte[] bytes,string detail){bool refused=false;try{Bt4PacketBoundary.Check(new PeImage(bytes),contract);}catch(IOException){refused=true;}if(!refused)throw new Exception("Accepted incompatible "+detail);checks++;}
 static int Main(string[] args){try{
 var timer=Stopwatch.StartNew();byte[] bytes=File.ReadAllBytes(args[0]);var image=new PeImage(bytes);var m=OnlineCompatibility.Metadata();contract=m.packet_contract;
 Bt4PacketBoundary.Check(image,contract);checks++;var p=Program.LoadProfile();Program.Validate(args[0],p);checks++;long initial=timer.ElapsedMilliseconds;
 int start=image.RawOffset(image.Symbol("Port_PadRead"),image.FunctionLength("Port_PadRead"));byte[] function=image.Read(image.Symbol("Port_PadRead"),image.FunctionLength("Port_PadRead"));
 foreach(string hex in new[]{contract.entry,contract.epilogue,contract.binding}){byte[] pattern=Program.Unhex(hex);int loc=start+Locate(function,pattern);for(int k=0;k<pattern.Length;k++){var bad=(byte[])bytes.Clone();bad[loc+k]^=1;Reject(bad,"register/argument contract byte");}}
 foreach(string hex in new[]{"b812000000","41f7d2","884e03","885604","44884602","884605","48c7460800000000","6644894e10"}){byte[] pattern=Program.Unhex(hex);int loc=start+Locate(function,pattern);for(int k=0;k<pattern.Length;k++){var bad=(byte[])bytes.Clone();bad[loc+k]^=1;Reject(bad,"packet encoding "+hex+" byte "+k);}}
 foreach(string pointerMove in new[]{"4189f2","4189e2"}){var bad=(byte[])bytes.Clone();Buffer.BlockCopy(Program.Unhex(pointerMove),0,bad,start+Locate(function,Program.Unhex("4589fa")),3);Reject(bad,"packet button word sourced from pointer");}
 var caller=(byte[])bytes.Clone();caller[image.RawOffset(image.Symbol("sample_local"),1)]^=1;Reject(caller,"online sampling caller");
 var aligned=(byte[])bytes.Clone();int tail=start+function.Length-1;if(aligned[tail]!=0x90)throw new Exception("Alignment fixture changed");aligned[tail]=0xcc;Bt4PacketBoundary.Check(new PeImage(aligned),contract);checks++;
 var reordered=(byte[])bytes.Clone();int first=start+Locate(function,Program.Unhex("0fb68c24950000000fb6942496000000"));Buffer.BlockCopy(bytes,first+8,reordered,first,8);Buffer.BlockCopy(bytes,first,reordered,first+8,8);Bt4PacketBoundary.Check(new PeImage(reordered),contract);checks++;
 var register=(byte[])bytes.Clone();foreach(var pair in new[]{new[]{"4589fa","4589fb"},new[]{"41f7d2","41f7d3"},new[]{"66448916","6644891e"}}){byte[] old=Program.Unhex(pair[0]),replacement=Program.Unhex(pair[1]);Buffer.BlockCopy(replacement,0,register,start+Locate(function,old),replacement.Length);}Bt4PacketBoundary.Check(new PeImage(register),contract);checks++;
 if(File.Exists(args[0]+".old")){var previous=Program.LoadProfile();Program.Validate(args[0]+".old",previous);checks++;}
 Console.WriteLine("PASS: "+checks+" BT4 boundary checks: current/previous build, equivalent axis reorder and register allocation accepted; changed packet/register/argument/online contracts rejected. Initial boundary + full validation ms="+initial+". No gameplay executed.");return 0;
 }catch(Exception e){Console.Error.WriteLine(e);return 1;}}
}
