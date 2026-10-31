using System;using System.IO;using System.Drawing;
static class HudFontFixture {
 static int Main(string[] args){try{
 string source=Path.Combine(Path.GetDirectoryName(args[0]),"fonts/fira/FiraSansCondensed-Bold.ttf");string copy=Path.Combine(args[1],"font-under-update.ttf");File.Copy(source,copy,true); using(var legacy=new System.Drawing.Text.PrivateFontCollection()){legacy.AddFontFile(source);var old=legacy.Families[0];Console.WriteLine("Old file loading: "+old.Name+" regular="+old.IsStyleAvailable(FontStyle.Regular)+" bold="+old.IsStyleAvailable(FontStyle.Bold));try{using(var f=new Font(old,14,FontStyle.Regular)){}Console.WriteLine("Legacy regular constructor succeeded in this run.");}catch(ArgumentException){Console.WriteLine("REPRODUCED: legacy Regular font constructor failed.");}}
 using(var loaded=new HudFontResource()){
 var family=loaded.Load(copy);Console.WriteLine("Selected "+family.Name+" style="+loaded.Style+" regular="+family.IsStyleAvailable(FontStyle.Regular));if(!family.IsStyleAvailable(loaded.Style))throw new Exception("Unsupported selected style");
 File.WriteAllText(copy,"updater temporarily replaced this file");File.Delete(copy);
 using(var image=new Bitmap(800,400))using(var g=Graphics.FromImage(image)){
 var metrics=new NativeMenuMetrics(null);for(int i=0;i<200;i++){
 using(var font=new Font(family,14,loaded.Style,GraphicsUnit.Pixel))g.DrawString("Goku / Vegeta",font,Brushes.White,0,0);
 metrics.Draw(g,"Kamehameha",family,Color.White,new RectangleF(0,40,400,60),2,false,true,.85f,loaded.Style);
 metrics.Wrapped(g,"Goku (End) Super Saiyan",family,Color.White,new RectangleF(0,120,160,100),1,loaded.Style);
 float cw,nw;Overlay.MeasurePanelWidth(g,family,new[]{new MoveRow("R2 + Square","Kamehameha","3",true,false)},1,800,false,out cw,out nw,metrics,loaded.Style);
 }image.Save(Path.Combine(args[1],"bold-font-draw.png"));}
 }
 foreach(string path in new[]{copy,Path.Combine(args[1],"bad.ttf")}){if(path.EndsWith("bad.ttf"))File.WriteAllText(path,"invalid font");using(var resource=new HudFontResource()){var family=resource.Load(path);using(var font=new Font(family,14,resource.Style)){}if(!family.IsStyleAvailable(resource.Style))throw new Exception("Fallback style invalid");}}
 Console.WriteLine("PASS: game Fira font; 200 repeated HUD draws/measurements; font overwritten and deleted after loading; missing/invalid font fallbacks.");return 0;
 }catch(Exception e){Console.Error.WriteLine(e);return 1;}}
}



