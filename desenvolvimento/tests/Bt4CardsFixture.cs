using System;using System.IO;using System.Drawing;using System.Threading;
static class Bt4CardsFixture {
 static void Check(bool ok,string message){if(!ok)throw new Exception(message);}
 static int Main(string[] args){try{using(var art=new TransformationArt(args[0])){foreach(int id in new[]{4,5,59,122}){Check(!art.Name(id).StartsWith("#"),"Missing BT4 name "+id);Bitmap image=null;for(int n=0;n<100&&image==null;n++){image=art.Get(id);Thread.Sleep(20);}Check(image!=null,"Portrait loading failed "+id);Check(image.Width==256&&image.Height==512,"Portrait size");var c=art.Crop(id);Check(c.X>=0&&c.Y>=0&&c.Right<=256&&c.Bottom<=512,"Crop bounds");}Check(art.Name(59).Contains("Super Saiyan God"),"BT4 roster mapping");}
 foreach(int width in new[]{1440,1920,2560,3440}){var p=new TechniquePrompt{NativeBounds=new RectangleF(58,384,24,32),Layout=0};var box=p.Project(new Size(width,1080));Check(Math.Abs(box.X-58f*width/512)<.1,"Original BT4 anchor");p.Layout=1;box=p.Project(new Size(width,1080));Check(Math.Abs(box.X-58f*1440/512)<.1,"Expanded anchor");}
 Console.WriteLine("PASS: BT4 portrait runtime loading, names, crop bounds and original/expanded projection at four aspect ratios.");return 0;}catch(Exception e){Console.Error.WriteLine(e);return 1;}}
}
