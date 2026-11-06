# BT4 Sparking Controls 0.9.8 Test

## 0.9.8 — Changes since 0.9.7

- **Fewer false incompatibility errors after game updates:** unique data symbols can move between COFF compiler emission groups without losing recognition. The exact symbolic address is still checked; ambiguous duplicate names and executable symbols keep strict scope validation.
- **Updated local sampling path:** supports the reviewed native sampler and its developer-rule auxiliary encoder. Complete instructions, references and encoder dependencies remain checked; the previous sampler is retained.
- **Controls and HUD retained:** the native 18-byte packet and its auxiliary fields are preserved. No control-payload changes, extra combat polling, logging or hooks.
- **Startup-only analysis:** initial ABI plus validation measured about 0.235 seconds in this environment. Saved preferences and the last EXE/CMD remain unchanged.

Real structural or protocol changes outside the checked interfaces can still require review. Future game builds are not universally guaranteed.

**Validation:** 1,764,747 emulated packet/ABI/native-command checks across three allocations; 441 new update-boundary checks; 571 native variant, 227 ABI, 48 writer, 51 projection and 21 last-path checks; shared settings, moved sections and suspended hook readback. The probe terminated before game execution. No new live gameplay, sustained-FPS, controller-timing or online/rollback test is claimed.

Download the Windows x64 ZIP or single EXE. Close the helper before replacement. Saved preferences and the last EXE/CMD remain under `%LOCALAPPDATA%\BT4-SparkingControls`. Select the game EXE or a translation CMD in the same game folder, then Save and start. Full instructions are on the main page and in the English TXT.

Windows test release; no validated native Linux BT4 package. No game executable, game artwork, texture pack or personal settings included. Source and AGPL-3.0 license included.

Special thanks to **Aero** for extensive testing, reproduction details and diagnostic logs.
