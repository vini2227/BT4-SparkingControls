# Windows build

Install Python 3 and .NET Framework 4.x with the C# compiler. From the repository root:

```powershell
python -m pip install -r requirements-build.txt
python desenvolvimento/tools/build-single-exe-windows.py BT4-SparkingControls.exe
```

`CSC` may override the compiler path. Profiles and the native payload are included; rebuilding the frontend does not need the game. Keep the reviewed metadata intact.

For tests, set `BT4_GAME` to your own BT4decomp.exe, then run `python run_checks.py HudFontFixture Bt4CardsFixture SavedSettingsFixture SplitCardsFixture`. CmdLaunchFixture launches isolated script descendants and terminates its game children before game code. CodeTableFixture also requires the previously reviewed executable at `$env:BT4_GAME + '.old'`.

Native maintenance additionally requires Zig 0.13 (`ZIG`), `BT4_GAME` (legacy alias `BT3_GAME`) and a reviewed game. `build-native-windows.py` rebuilds the payload/profile and generated addresses. Do not rebuild native metadata merely to bypass a rejected game: review native interfaces, preserve typed pointer/string/jump-table contracts and validate the 28 payload references (including gBattleWork +0x19f0/+0x19f8). The generic metadata generator does not preserve every hand-reviewed contract. No game executable is distributed.

Native packet tests: after a reviewed native build, run `python desenvolvimento/tests/packet_tests.py --game GAME_EXE`. For a resolved update, first run the helper with `--compat-test GAME_EXE`, then provide its version-cache directory using `--resolved-root CACHE`. These tests emulate the packet translator and are not live online matches.

The BT4 format-4 input contract checks the packet writer symbolically instead of pinning the entire collector implementation. It retains the reviewed Win64 save/restore, argument binding and sample_local caller contracts. Keep the reviewed metadata intact. Run `python run_checks.py Bt4BoundaryFixture` for compatible-reordering/register-allocation and incompatible-packet/ABI/caller tests. The 0.7–0.9 payload has 32 reviewed game references.
