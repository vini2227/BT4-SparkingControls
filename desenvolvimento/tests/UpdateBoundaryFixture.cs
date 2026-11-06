using System;using System.IO;using System.Linq;using System.Reflection;using System.Collections.Generic;
static class UpdateBoundaryFixture {
 static int checks;
 static void Assert(bool ok,string what){if(!ok)throw new Exception(what);checks++;}
 static void Reject(byte[] bytes,OnlineSignature caller,int at,string what){var bad=(byte[])bytes.Clone();bad[at]^=1;bool rejected=false;try{OnlineCompatibility.ReviewedSignature(new PeImage(bad),caller);}catch(IOException){rejected=true;}Assert(rejected,what);}
 static int Main(string[] args){try{
  byte[] bytes=File.ReadAllBytes(args[0]);var image=new PeImage(bytes);var metadata=OnlineCompatibility.Metadata();
  var fields=(Dictionary<string,List<PeSymbol>>)typeof(PeImage).GetField("candidates",BindingFlags.Instance|BindingFlags.NonPublic).GetValue(image);
  foreach(string name in new[]{"sMe","sMode"}){
   var symbols=fields[name];var data=symbols.Single();string obsolete="previous-emission-group";
   Assert(image.MatchesSymbol(name,data.Rva,"Port_NetMe",obsolete),"unique static may change COFF group");
   Assert(!image.MatchesSymbol(name,data.Rva+4,"Port_NetMe",obsolete),"changed target must fail");
   var duplicate=new PeSymbol{Rva=data.Rva+4,Section=data.Section,Type=data.Type,Scope="unrelated-group"};symbols.Add(duplicate);
   Assert(!image.MatchesSymbol(name,data.Rva,"Port_NetMe",obsolete),"ambiguous static cannot use group fallback");
   Assert(!image.MatchesSymbol(name,duplicate.Rva,"Port_NetMe",obsolete),"unrelated duplicate cannot use group fallback");
   Assert(image.MatchesSymbol(name,data.Rva,"Port_NetMe",data.Scope),"explicit matching group preserved");symbols.Remove(duplicate);
  }
  Assert(!image.MatchesSymbol("Port_NetMe",image.Symbol("Port_NetMe"),"Port_NetMe","wrong-group"),"executable symbols cannot use data fallback");
  var caller=metadata.packet_contract.caller;var selected=OnlineCompatibility.ReviewedSignature(image,caller);
  Assert(selected!=caller&&selected.dependencies.Length==1&&selected.dependencies[0].name=="Port_DevRulesEncode","reviewed new local sampler and native auxiliary encoder");
  foreach(var signature in new[]{selected,selected.dependencies[0]}){
   var mask=Program.Unhex(signature.mask);int start=image.RawOffset(image.Symbol(signature.name),signature.length);
   foreach(int at in Enumerable.Range(0,mask.Length).Where(i=>mask[i]==255))Reject(bytes,caller,start+at,"changed sampler/encoder instruction rejected");
   foreach(var reference in signature.references)Reject(bytes,caller,start+reference.offset,"redirected sampler/encoder reference rejected");
  }
  foreach(string name in new[]{"Port_NetActive","Port_NetMe"})Assert(OnlineCompatibility.ReviewedSignature(image,metadata.functions.Single(f=>f.name==name))!=null,"unchanged network function resolves across group changes");
  if(File.Exists(args[0]+".old")){var old=new PeImage(File.ReadAllBytes(args[0]+".old"));Assert(OnlineCompatibility.ReviewedSignature(old,caller)==caller,"previous local sampler preserved");}
  Console.WriteLine("PASS: "+checks+" update-boundary checks: unique data groups may move; duplicate data, executable scopes, sampler/auxiliary code and redirected references remain checked. No gameplay executed.");return 0;
 }catch(Exception e){Console.Error.WriteLine(e);return 1;}}
}
