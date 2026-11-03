# BT4 Sparking Controls 0.9.7 Test

Includes all compatibility fixes from local 0.9.6 and 0.9.7 since public 0.9.5.

- Resolve moved capture fields by exact symbolic targets rather than fixed instruction literals.
- Recognize distinct preserved Win64 socket/output registers; follow the verified packet buffer register, including extended-register addressing. Stack, volatile and overlapping bindings stay rejected.
- Verify complete newer native input variants and their typed references, including target-stick auxiliary signaling and the latest native packet consumer. Keep older reviewed variants and all 64 original signatures.
- Preserve auxiliary packet bytes. Native controls payload, saved preferences, last EXE/CMD, recovery behavior and HUD projection are retained.
- Checks run only on startup/relaunch: about 0.24 seconds in this environment. No new combat polling, diagnostics or hooks.

**Scope:** compatible address/register changes cause fewer false rejections. Real structure or protocol changes can still require review. Universal future-build compatibility is not promised.

**Validation:** 1,764,747 emulated packet/ABI/native-command checks with nonzero auxiliary fields; 571 native variant checks; 227 ABI checks; 48 writer tests across eight output registers; 51 projection and 21 last-path checks; shared settings, relocation and suspended-hook readback. These are not a live online/rollback match, physical-controller timing or sustained-FPS test.

Download the Windows x64 ZIP or single EXE. Close the helper before replacing it. Saved preferences remain under `%LOCALAPPDATA%\BT4-SparkingControls`. Select the game EXE or a translation CMD in the same game folder, then Save and start. Full instructions are on the main page and in the English TXT.

Windows test release; no validated native Linux BT4 package. No game executable, artwork, texture pack or personal settings included. Source and AGPL-3.0 license included.

Special thanks to **Aero** for extensive testing, reproduction details and diagnostic logs.
