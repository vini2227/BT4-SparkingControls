using System;using System.IO;using System.Drawing;
static class PromptProjectionFixture {
 static int Main(string[] args){try{int checks=0;using(var icons=new GameButtonIcons(args[0])){
 foreach(var size in new[]{new Size(1440,1080),new Size(1920,1080),new Size(2560,1080),new Size(3440,1440)})foreach(int side in new[]{0,1})foreach(int layout in new[]{0,2}){
 var p=new TechniquePrompt{NativeBounds=new RectangleF(side==0?48:439,384,24,32),Layout=layout,AspectMilli=0,Side=side,Texture=22,Uv=new Rectangle(32,0,24,32),Button="□"};var b=p.Project(size);float expected=p.NativeBounds.X*size.Width/512f;if(Math.Abs(b.X-expected)>.01f)throw new Exception("Native prompt x");if(Math.Abs(b.Width-24f*size.Width/512f)>.01f)throw new Exception("Native prompt scale");checks+=2;
 }
 var regression=new TechniquePrompt{NativeBounds=new RectangleF(48,384,24,32),Layout=2,AspectMilli=1778,Side=0,Texture=22,Uv=new Rectangle(32,0,24,32),Button="□"};var box=regression.Project(new Size(1920,1080));var ink=icons.TriangleInk(box,22,regression.Uv);
 if(Math.Abs(box.X-180)>.1||ink.Left<180||ink.Right>270)throw new Exception("Captured centered-HUD regression");checks++;
 using(var image=new Bitmap(1920,1080))using(var g=Graphics.FromImage(image)){g.Clear(Color.FromArgb(32,40,48));regression.Draw(g,image.Size,icons);image.Save(Path.Combine(args[1],"captured-prompt-corrected.png"));}
 Console.WriteLine("PASS: "+checks+" prompt placement/scale checks; Original and Centered, four aspects, both sides. Captured 1920x1080 bound="+box+" glyph="+ink+". No per-frame pixel search.");}
 return 0;}catch(Exception e){Console.Error.WriteLine(e);return 1;}}
}
