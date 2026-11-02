using System;using System.IO;using System.Reflection;using System.Threading;using System.Windows.Forms;
static class LastGamePathFixture {
 static int checks;
 static void Check(bool okay,string message){if(!okay)throw new Exception(message);checks++;}
 static TextBox Box(GameLocationWindow window){return (TextBox)typeof(GameLocationWindow).GetField("pathBox",BindingFlags.NonPublic|BindingFlags.Instance).GetValue(window);}
 static void Pump(){var end=DateTime.UtcNow.AddMilliseconds(650);while(DateTime.UtcNow<end){Application.DoEvents();Thread.Sleep(5);}}
 [STAThread]static int Main(string[] args){try{
  string root=Path.Combine(args[0],"last-location-"+Guid.NewGuid().ToString("N"));Directory.CreateDirectory(root);SavedSettings.OverrideDirectory=root;
  string file=SavedSettings.FilePath("game-path.txt"),old=Path.Combine(root,"old.exe"),exe=Path.Combine(root,"new game.exe"),cmd=Path.Combine(root,"New translation.cmd");
  File.WriteAllText(file,old);File.WriteAllText(exe,"not a compatible game");File.WriteAllText(cmd,"@echo off");
  using(var window=new GameLocationWindow(old,file)){
   var handle=window.Handle;Box(window).Text=exe;
   Check(File.ReadAllText(file)==old,"Typed path should be debounced");Pump();Check(File.ReadAllText(file)==exe,"Last typed EXE not autosaved");
   DateTime stamp=DateTime.UtcNow.AddDays(-1);File.SetLastWriteTimeUtc(file,stamp);Pump();Check(File.GetLastWriteTimeUtc(file)==stamp,"Idle timer kept writing");
   Box(window).Text="\""+cmd+"\"";typeof(GameLocationWindow).GetMethod("PersistPath",BindingFlags.NonPublic|BindingFlags.Instance).Invoke(window,null);
   Check(File.ReadAllText(file)==cmd,"CMD/quoted path not remembered immediately");
   Box(window).Text=exe;typeof(GameLocationWindow).GetMethod("Save",BindingFlags.NonPublic|BindingFlags.Instance).Invoke(window,new object[]{false});
   Check(File.ReadAllText(file)==exe,"Failed compatibility validation lost the last path");Check(window.GamePath==null,"Remembering path bypassed compatibility validation");
   Box(window).Text=cmd;window.Close();Check(File.ReadAllText(file)==cmd,"Closing before debounce lost CMD");
  }
  Check(SavedSettings.InitialGamePath(file,old)==cmd,"Stale startup argument overrode latest choice");
  using(var reopened=new GameLocationWindow(SavedSettings.InitialGamePath(file,old),file)){Check(Box(reopened).Text==cmd,"Reopening lost latest CMD");}
  foreach(string invalid in new[]{"","new.exe",Path.Combine(root,"notes.txt"),"C:\\bad\0.exe"}){Check(!SavedSettings.RememberGamePath(file,invalid),"Incomplete/unsupported entry accepted");Check(File.ReadAllText(file)==cmd,"Incomplete entry overwrote latest choice");}
  string missing=Path.Combine(root,"new install","game.exe");Check(SavedSettings.RememberGamePath(file,missing),"Missing but complete EXE should remain remembered");
  Check(SavedSettings.InitialGamePath(file,old)==missing,"Missing latest EXE reverted to stale path");
  string newVersion=Path.Combine(root,"online-test-next");Directory.CreateDirectory(newVersion);File.WriteAllText(Path.Combine(newVersion,"game-path.txt"),old);
  Check(File.ReadAllText(SavedSettings.Resolve(root,newVersion,"game-path.txt"))==missing,"Version migration overwrote latest path");
  Check(SavedSettings.InitialGamePath(Path.Combine(root,"no-choice.txt"),exe)==exe,"First launch fallback broken");
  Console.WriteLine("PASS: "+checks+" last-location checks: typed EXE debounce, quoted CMD, close flush, failed-validation persistence without bypass, reopening, stale startup arguments, incomplete edits, missing file, version update and no idle rewrites. Isolated settings only; no game started.");return 0;
 }catch(Exception e){Console.Error.WriteLine(e);return 1;}}
}
