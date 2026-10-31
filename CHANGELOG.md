# Changelog

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


Earlier BT4 test builds introduced the reviewed BT4 interface profile, four-star branding and red launcher theme.
