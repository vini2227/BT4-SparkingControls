# Changelog

## 0.9
- Compatible with the reviewed R7.1 Hotfix input collector without adding its full-file hash to an allowlist or regenerating its controls payload.
- A bounded symbolic input-boundary check accepts equivalent instruction reordering/register allocation in the native packet writer. It verifies the 18-byte layout, inverted button word, four analog bytes, zero padding, successful return, Win64 register/stack preservation and unchanged online sampling caller.
- The remaining reviewed native interfaces, symbolic references and corruption checks remain active. Structural changes outside the supported contracts can still require review; universal future-version support is not promised.
- Input-boundary analysis is startup/relaunch-only, never per frame. The measured initial boundary plus full validation took about 0.24 seconds in this test environment.
- 155 focused tests accepted equivalent compiler changes and the previous/current builds, and rejected altered packet, register, argument and online-caller contracts. Complete relocation, changed-instruction/reference and stripped-symbol checks passed.
- Recovery on Circle, corrected boost-prompt placement, in-memory fonts, translation CMD launch and saved settings remain unchanged.

## 0.8
- Fixed the replacement Square/Circle boost glyph being displaced in BT4 Centered HUD. Prompt geometry retains its native page coordinates; it is no longer centered a second time.
- Verified against the live captured 24x32 prompt at native (48,384): at 1920x1080 the replacement glyph covers x=193.75..241.25, matching the native Triangle, instead of appearing near x=404.
- 33 placement/scale checks cover Original and Centered HUD, four aspect ratios and both sides. Portrait loading/projection checks also passed. No per-frame pixel searching or new combat work.
- Controls, Circle-only manual get-up, fonts, CMD launch, theme and saved settings are unchanged.

## 0.7
- Manual get-up while grounded/downed requires physical Circle. R1 and other buttons or directions alone do not request that recovery. Circle plus a direction retains native directional get-up.
- Standing guard/counter controls and native automatic get-up remain unchanged. This change is scoped to lying/down actions, not every airborne hit reaction.
- Downed-player shortcut state is cleared only for that local controller. Other local panels, remote input, CPU/script-injected input and ordinary menus are preserved.

## 0.6
- HUD fonts are copied into private memory once when opening the overlay, so game updater file replacement does not invalidate the loaded font.
- Text and glyph paths use a style actually available in the selected font, with a validated system fallback for missing, unsupported or invalid fonts. This addresses the unhandled Fira Sans Condensed / Regular font error.
- Font checks and loading occur at overlay initialization, outside combat rendering. CMD launch, theme and saved settings remain unchanged.
- Validation: 200 repeated font draw/measure passes after overwriting/deleting the font file, invalid/missing font fallback, and card-menu layout checks. The reported intermittent dialog was not reproduced in this test run; the previously unchecked style and file-lifetime paths are now covered.

## 0.5
- Select an EXE or a CMD using the existing Browse button. A CMD must be in the same folder as BT4decomp.exe. Its path is saved normally.
- Script preparation, language environment and nested CMD calls are preserved. The matching game process is intercepted before its first instruction, including relaunches. The overlay reads artwork from the actual game folder.
- No launcher layout, colors, controls or theme changes.
- Compatibility validation now checks internal jump tables by exact function-relative destinations and the direction-name table by exact strings instead of fixed addresses; current and previous builds are checked and corrupted targets rejected. Analysis remains outside combat.

## 0.4
- Native Triangle input is preserved throughout beam/rush struggle actions. Super-hold button aliases and replacement prompts no longer apply during those actions.
- Square/Circle technique prompt replacements follow BT4 Original HUD horizontal coordinates; the BT3 narrowing factor is no longer applied to that layout.
- Transformation, fusion and switch cards use the BT4 roster names and portraits read from your game installation.
- Portrait crops are adjusted to center faces and keep eyes visible, including the different BT4 character poses.
- Existing red theme, four-star emblem, configurable shortcuts, split-screen placement and saved preferences remain available.


Earlier BT4 test builds introduced reviewed BT4 interfaces, red launcher styling and the four-star emblem.
