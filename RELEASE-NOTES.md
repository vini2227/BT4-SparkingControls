# BT4 Sparking Controls 0.9.5 Test

Includes all fixes from 0.9.1 through 0.9.5 since the previous public 0.9 Test release.

- **Recovery:** native ground buttons/directions restored; R1 and Up/Down request supported air recovery using native input mappings. BT4 still determines timing, eligibility and Ki cost. The old Circle-only restriction is removed.
- **Compatibility:** complete checked alternatives for the reviewed newer model lookup, Z-Counter capture and HUD marker functions, retaining the earlier 64 signatures and verifying new dependencies. Startup/relaunch analysis only; unsupported future structure can still need review.
- **Last EXE/CMD:** automatically remembered on selection, typing pause, field exit and close, even if validation fails. Latest saved choice takes priority over stale startup paths. No idle rewrites or gameplay timer.
- **Prompt alignment:** Square/Circle uses the newer renderer's native centered projection. Older renderer and Original HUD behavior remain preserved; no fixed pixel offset, image search or new hooks.

All earlier BT4 features remain included. Settings remain under `%LOCALAPPDATA%\BT4-SparkingControls`. Close the helper before replacing its EXE. Extract the Windows ZIP, select the game EXE or a sibling translation CMD, then Save and start. See the main page and included English TXT for full setup and controls.

**Validation:** 1,764,747 emulated packet/ABI/native-command checks on the reviewed reinstalled game; 236 interface/variant rejection checks; 154 input-boundary checks; 51 projection checks; 21 last-path persistence checks; shared-settings, relocation and suspended-hook readback. The payload is unchanged since 0.9.2. Automated checks are not a live online match, physical-controller timing or sustained-FPS test; the corrected 0.9.5 alignment still awaits live-match confirmation.

Windows x64 test release. No validated native Linux BT4 package. No game executable, artwork, texture pack or personal settings included. Source and AGPL-3.0 license included.

Special thanks to **Aero** for extensive testing, reproduction details and diagnostic logs.
