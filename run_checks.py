from pathlib import Path
import subprocess,sys
r=Path(__file__).parent;d=r/'desenvolvimento';src=d/'src';checks=r/'checks';checks.mkdir(exist_ok=True)
csc=r'C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe'
game=__import__('os').environ['BT4_GAME']
names=sys.argv[1:] or ['CmdLaunchFixture', 'CodeTableFixture', 'HudFontFixture', 'Bt4CardsFixture', 'SavedSettingsFixture', 'SplitCardsFixture']
sources=[str(p) for p in src.glob('*.cs')]
resources={p.relative_to(d).as_posix():p for p in (d/'assets').glob('*') if p.is_file()}
resources.update({n:d/n for n in ['profile.json','SparkingControls.payload','compatibility.json','compatibility-legacy.json']})
for name in names:
    dest=checks/name;dest.mkdir(exist_ok=True);exe=dest/(name+'.exe')
    command=[csc,'/nologo','/target:exe','/platform:x64','/optimize+','/main:'+name,'/out:'+str(exe),'/r:System.Windows.Forms.dll','/r:System.Drawing.dll','/r:System.Web.Extensions.dll']
    command+=['/resource:'+str(p)+',OnlineBundle/'+n for n,p in resources.items()]
    subprocess.run(command+sources+[str(d/'tests'/(name+'.cs'))],check=True)
    args=[str(dest)] if name in ['SavedSettingsFixture','ShortcutFixture','LauncherFixture','PreviewShortcutFixture'] else [game,str(dest)]
    if name=='ArenaPlacementFixture':args=[str(r/'arena-probe.exe'),str(dest)]
    if name=='RendererCaptureFixture':args=[game,str(r/'renderer-capture-probe.exe'),str(dest)]
    if name=='HeapEnvironmentFixture':args=[str(r/'heap-probe.exe'),str(r/'heap-parent.exe'),str(dest)]
    if name=='HudRecoveryFixture':args=[game,str(dest/'hud-recovery-results.txt')]
    result=subprocess.run([str(exe)]+args,capture_output=True,text=True,timeout=180)
    (dest/'console.txt').write_text(result.stdout+result.stderr,encoding='utf-8')
    print(name,result.returncode,result.stdout.strip(),result.stderr.strip(),flush=True)
    if result.returncode:raise SystemExit(result.returncode)
