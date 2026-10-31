# BT4 Sparking Controls 0.9 Test

This release includes **all work since the previous public 0.6 Test release**, including the local 0.7 and 0.8 updates.

## 0.9 — Adaptive R7.1 Hotfix compatibility
- Accepts compatible native input-collector changes without a full-file hash allowlist. A bounded symbolic check verifies the 18-byte packet, inverted buttons, four axes, zero padding, successful return, Win64 preservation and unchanged online sampling caller.
- Equivalent axis-load reordering and register allocation are accepted; incompatible packet/ABI/caller changes remain rejected. Other reviewed native interfaces and references stay checked.
- Analysis runs only at startup/relaunch; initial boundary plus full validation measured about 0.2 seconds in this test environment. No new per-frame work.

## 0.8 — Boost prompt alignment
- Square/Circle replacements stay over the native Triangle in the BT4 Centered HUD. The prompt is no longer centered a second time.
- Verified against live captured coordinates, with 33 checks covering four aspect ratios and both sides.

## 0.7 — Circle-only grounded recovery
- Manual get-up while grounded/downed requires Circle. Circle + direction retains native directional recovery.
- Standing R1 guard and native automatic recovery remain. The change is scoped to lying/down actions, not every airborne hit reaction.
- Other local-player menu/state, remote input and CPU/scripted input are preserved.

## Included earlier improvements
- In-memory HUD fonts, available font-style selection and fallback for the reported Fira Sans Condensed / Regular error.
- EXE and CMD translation launch, including nested scripts and inherited language variables.
- Native Triangle preserved in energy struggles; BT4 character names and face-centered portraits.
- Red launcher, four-star emblem, four panel placements, split-screen, configurable F6/F7/F8 shortcuts, shortcut lock and persisted in-game/app preferences.

## Validation and scope
1,541,133 emulated packet/ABI/native-command checks passed against resolved R7.1 addresses at three allocations; 155 focused input-boundary acceptance/rejection checks; relocation/corruption/stripped-symbol tests; suspended hook, CMD and saved-setting checks. Earlier font, portrait, split-screen and projection reports are included. These do not replace physical-controller testing, sustained FPS measurements or live online/rollback matches. Structural changes outside the supported contracts can still require review.

Windows x64 test release; no validated native Linux BT4 package. Game files, game artwork, texture packs and personal settings are not included. Source and AGPL-3.0 license are included.

Special thanks to **Aero** for extensive testing, reproduction details and diagnostic logs.
