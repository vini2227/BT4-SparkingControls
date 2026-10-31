using System;using System.IO;using System.Threading;
static class CmdLaunchFixture {
 static int Main(string[] args){try{
 string folder=Path.Combine(args[1],"script folder & spaces");Directory.CreateDirectory(folder);
 string game=Path.Combine(folder,"BT4decomp.exe");File.Copy(args[0],game,true);
 string script=Path.Combine(folder,"Jogar em Portugues.cmd"),marker=Path.Combine(folder,"setup.txt");
 File.WriteAllText(script,"@echo off\r\nsetlocal\r\ncd /d \"%~dp0\"\r\nset \"BT3_MENU_LANG=pt-BR\"\r\n>\"setup.txt\" echo %BT3_MENU_LANG%\r\nstart \"\" \"BT4decomp.exe\"\r\n");
 if(GameLaunch.ResolveGame(script)!=game||GameLaunch.ResolveGame(game)!=game)throw new Exception("Launch target resolution");
 using(var session=new GameSession(script,null,false,true,false,false,true)){
 session.Start();for(int n=0;n<100&&!session.Done;n++)Thread.Sleep(50);
 if(session.Failure!=null)throw session.Failure;
 if(session.Latest==null||!session.Done)throw new Exception("Script descendant not prepared or probe did not finish");
 if(File.ReadAllText(marker).Trim()!="pt-BR")throw new Exception("Script setup/environment/current directory lost");
 }
 string nested=Path.Combine(folder,"Escolher idioma.cmd");File.WriteAllText(nested,"@echo off\r\ncd /d \"%~dp0\"\r\ncall \"Jogar em Portugues.cmd\"\r\n");
 using(var session=new GameSession(nested,null,false,true,false,false,true)){session.Start();for(int n=0;n<100&&!session.Done;n++)Thread.Sleep(50);if(session.Failure!=null)throw session.Failure;if(session.Latest==null||!session.Done)throw new Exception("Nested CMD launch failed");}
 Console.WriteLine("PASS: direct/nested CMD, spaces and ampersand in paths, script setup/current directory, descendant hook before first instruction; probe children stopped before game code.");return 0;
 }catch(Exception e){Console.Error.WriteLine(e);return 1;}}
}
