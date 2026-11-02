using System;using System.IO;using System.Linq;
static class ReviewedVariantsFixture {
 static int checks;
 static void Reject(byte[] original,OnlineSignature signature,int offset,string detail){
  var bad=(byte[])original.Clone();bad[offset]^=1;bool refused=false;
  try{OnlineCompatibility.ReviewedSignature(new PeImage(bad),signature);}catch(IOException){refused=true;}
  if(!refused)throw new Exception("Accepted changed "+detail);checks++;
 }
 static int Main(string[] args){try{
  var bytes=File.ReadAllBytes(args[0]);var image=new PeImage(bytes);var metadata=OnlineCompatibility.Metadata();
  if(metadata.functions.Length!=64||metadata.game_references.Length!=29)throw new Exception("Interface coverage changed");checks++;
  var changed=metadata.functions.Where(f=>f.alternatives!=null).ToArray();if(changed.Length!=3)throw new Exception("Expected three reviewed variants");
  foreach(var original in changed){
   var selected=OnlineCompatibility.ReviewedSignature(image,original);if(selected==original)throw new Exception("New game did not use variant "+original.name);checks++;
   var mask=Program.Unhex(selected.mask);var positions=Enumerable.Range(0,mask.Length).Where(i=>mask[i]==255).ToArray();
   foreach(int index in new[]{0,1,2,positions.Length/4,positions.Length/2,positions.Length*3/4,positions.Length-1}.Distinct())Reject(bytes,original,image.RawOffset(image.Symbol(original.name)+positions[index],1),original.name+" instruction");
   foreach(var reference in selected.references)Reject(bytes,original,image.RawOffset(image.Symbol(original.name)+reference.offset,reference.width),original.name+" reference "+reference.symbol);
   foreach(var dependency in selected.dependencies??new OnlineSignature[0]){
    Reject(bytes,original,image.RawOffset(image.Symbol(dependency.name),1),dependency.name+" body");
    foreach(var reference in dependency.references)Reject(bytes,original,image.RawOffset(image.Symbol(dependency.name)+reference.offset,reference.width),dependency.name+" reference");
   }
  }
  var p=Program.LoadProfile();Program.Validate(args[0],p);if(!p.prompt_centered_projection)throw new Exception("New renderer projection not selected");checks++;
  string previous=Environment.GetEnvironmentVariable("BT4_PREVIOUS_GAME")??Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"..","CmdLaunchFixture","script folder & spaces","BT4decomp.exe"));
  var old=new PeImage(File.ReadAllBytes(previous));foreach(var signature in changed){if(OnlineCompatibility.ReviewedSignature(old,signature)!=signature)throw new Exception("Old signature lost");checks++;}
  var oldProfile=Program.LoadProfile();Program.Validate(previous,oldProfile);if(oldProfile.prompt_centered_projection)throw new Exception("Old renderer projection lost");checks++;
  Console.WriteLine("PASS: "+checks+" reviewed-variant checks; current and previous game accepted; changed instructions, symbolic targets and new helper dependencies rejected. All64 original signatures retained. No gameplay executed.");return 0;
 }catch(Exception e){Console.Error.WriteLine(e);return 1;}}
}
