using System;using System.IO;using System.Diagnostics;using System.Linq;
static class Bt4BoundaryFixture {
 static int checks;static PacketContract contract;static OnlineMetadata metadata;
 static int Locate(byte[] bytes,byte[] pattern){for(int i=0;i<=bytes.Length-pattern.Length;i++)if(bytes.Skip(i).Take(pattern.Length).SequenceEqual(pattern))return i;throw new Exception("Mutation pattern not found");}
 static void Reject(byte[] bytes,string detail){bool refused=false;try{Bt4PacketBoundary.CheckSupported(new PeImage(bytes),metadata);}catch(IOException){refused=true;}if(!refused)throw new Exception("Accepted incompatible "+detail);checks++;}
 static int Main(string[] args){try{
 var timer=Stopwatch.StartNew();byte[] bytes=File.ReadAllBytes(args[0]);var image=new PeImage(bytes);metadata=OnlineCompatibility.Metadata();contract=metadata.packet_contract;
 Bt4PacketBoundary.CheckSupported(image,metadata);checks++;var p=Program.LoadProfile();Program.Validate(args[0],p);checks++;long initial=timer.ElapsedMilliseconds;
 int start=image.RawOffset(image.Symbol("Port_PadRead"),image.FunctionLength("Port_PadRead"));byte[] function=image.Read(image.Symbol("Port_PadRead"),image.FunctionLength("Port_PadRead"));
 foreach(string hex in new[]{contract.entry,contract.epilogue}){byte[] pattern=Program.Unhex(hex);int loc=start+Locate(function,pattern);for(int k=0;k<pattern.Length;k++){var bad=(byte[])bytes.Clone();bad[loc+k]^=1;Reject(bad,"register/argument contract byte");}}
 int binding=Bt4PacketBoundary.FindBinding(function,53,128),socket,output,bindingWidth;if(binding<0||!Bt4PacketBoundary.ReadBinding(function,binding,out socket,out output,out bindingWidth))throw new Exception("Native binding not found");
 for(int k=0;k<bindingWidth;k++){var bad=(byte[])bytes.Clone();bad[start+binding+k]^=1;Reject(bad,"argument binding byte");}
 foreach(int reg in new[]{0,1,2,4,output}){var bad=(byte[])bytes.Clone();bad[start+binding+1]=(byte)(0xc8+reg);Reject(bad,"volatile/stack/overlapping socket register");}
 foreach(int reg in new[]{0,1,2,4,socket}){var bad=(byte[])bytes.Clone();bad[start+binding+bindingWidth-1]=(byte)(0xd0+reg);Reject(bad,"volatile/stack/overlapping output register");}
 foreach(int from in new[]{3,5,6,7,12,13,14,15})foreach(int to in new[]{3,5,6,7,12,13,14,15}){
  var pair=new System.Collections.Generic.List<byte>();if(from>=8)pair.Add(0x41);pair.Add(0x89);pair.Add((byte)(0xc8+(from&7)));pair.Add((byte)(to>=8?0x49:0x48));pair.Add(0x89);pair.Add((byte)(0xd0+(to&7)));
  int a,b,w;bool accepted=Bt4PacketBoundary.ReadBinding(pair.ToArray(),0,out a,out b,out w);
  if(accepted!=(from!=to)||accepted&&(a!=from||b!=to))throw new Exception("Preserved register pair mapping");checks++;
 }
 foreach(string hex in new[]{"b812000000","41f7d2","884d03","885504","44884502","884505","48c7450800000000","6644895510"}){byte[] pattern=Program.Unhex(hex);int loc=start+Locate(function,pattern);for(int k=0;k<pattern.Length;k++){var bad=(byte[])bytes.Clone();bad[loc+k]^=1;Reject(bad,"packet encoding "+hex+" byte "+k);}}
 foreach(string pointerMove in new[]{"4189ea","4189e2"}){var bad=(byte[])bytes.Clone();Buffer.BlockCopy(Program.Unhex(pointerMove),0,bad,start+Locate(function,Program.Unhex("4589fa")),3);Reject(bad,"packet button word sourced from pointer");}
 var caller=(byte[])bytes.Clone();caller[image.RawOffset(image.Symbol("sample_local"),1)]^=1;Reject(caller,"online sampling caller");
 // Native auxiliary protocol branches are accepted only by their complete
 // reviewed body/targets. Arbitrary postprocessing changes must be rejected.
 var auxiliary=(byte[])bytes.Clone();int aux=start+Locate(function,Program.Unhex("884d11"));auxiliary[aux+2]=0x10;Reject(auxiliary,"target-stick auxiliary byte offset");
 if(File.Exists(args[0]+".old")){var previous=Program.LoadProfile();Program.Validate(args[0]+".old",previous);checks++;}
 Console.WriteLine("PASS: "+checks+" BT4 boundary checks: current/previous build and preserved-register argument allocation accepted; changed packet/auxiliary/register/argument/online contracts rejected. Initial boundary + full validation ms="+initial+". No gameplay executed.");return 0;
 }catch(Exception e){Console.Error.WriteLine(e);return 1;}}
}
