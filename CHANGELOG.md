# Changelog

## 0.9.7
- Socket and output-pointer arguments can use distinct preserved Win64
  registers. The output buffer is no longer assumed to reside in RSI.
- The bounded packet decoder follows the verified output register. Stack,
  volatile-register and overlapping argument bindings remain rejected.
- Complete reviewed native input variants remain checked; the newer producer
  additionally pins the native Pad_Update consumer to verify packet layout.
- Previous producer variants and all64 earlier signatures remain available.
  Address moves resolve symbolically; unsupported structural/protocol changes
  can still need review. Analysis is startup-only; native controls unchanged.

## 0.9.6
- Three native capture-field addresses now follow their exact symbolic match
  fields instead of being treated as fixed instruction literals. Code changes
  and redirected symbols are still rejected; harmless address moves can pass.
- The input ABI checker accepts the socket argument in a saved Win64 register,
  while preserving the output-pointer binding, full save/restore contract,
  18-byte packet proof and native online caller checks. Stack/output/volatile
  argument bindings remain rejected. No EXE hash added to the runtime allowlist.
- Startup-only compatibility changes; native controls payload unchanged.
- Complete instruction/reference checks cover the newer native target-stick
  packet trailer, including its auxiliary byte. The filter preserves auxiliary
  bytes instead of assuming that the packet tail is unused or always zero.

## 0.9.5
- The replacement technique glyph uses the reviewed newer renderer's centered
  4:3 transform when the game's HUD is Centered. This follows the native
  Triangle's position and size instead of using a fixed horizontal offset.
- Previous renderer projection and Original HUD placement remain preserved.
- Renderer capability is selected during existing startup compatibility checks.
  No image searching, new game hooks or extra combat diagnostics.

## 0.9.4
- Remembers the last EXE or CMD as soon as it is selected, after a short pause
  while typing, when leaving the field, and when closing the launcher.
- The location is remembered even if game compatibility validation fails.
  Launching still requires the same compatibility checks.
- The last saved location takes priority over a stale startup path argument.
- Incomplete/unsupported entries do not replace the last EXE or CMD choice.
- Saves only when the path changes; the short debounce timer belongs to the
  launcher window and is disposed before gameplay. Other controls unchanged.

## 0.9.3
- Recognizes the reviewed newer native model lookup, Z-Counter capture and HUD
  marker variants. Old interfaces remain supported. Complete instruction,
  symbolic target and new dependency checks are retained for each variant.
- Variant detection happens only on startup/relaunch. No whitelist of new EXE
  hashes, combat-time scanning, extra diagnostics or control-payload changes.
- The 0.9.2 recovery controls and saved settings remain unchanged.
- Future structural changes outside reviewed interfaces can still need review.

## 0.9.2
- Standard-style recovery controls in reviewed launch, blow-away and falling
  reactions: R1 sends the native recovery face button, without counter stance.
- Up/Down remain available for native recovery instead of opening panels or
  attack shortcuts. In the launched reaction, a vertical direction encodes the
  native direction-plus-face recovery chord. Stick and D-pad both work.
- Grounded recovery keeps the original buttons and directional choices.
- These are input mappings, not Sparking! ZERO physics or recovery rules.
  Native timing, Ki costs, eligibility, animations and automatic recovery stay
  under BT4 control. No repeating recovery macros or game-state writes.
- Online mapping is performed only on the local pad before native sampling.
  End-to-end online gameplay still requires testing.

## 0.9.1
- Restored native grounded recovery. Square, Triangle, Cross, Circle and
  directional recovery are no longer restricted to physical Circle.
- While downed, the original controller packet reaches the game unchanged.
  R1 is not translated to Circle during that state. The game still decides
  recovery eligibility and automatic recovery timing.
- Other controls, compatibility checks, HUD and saved settings are unchanged.
- The historical Circle-only behavior described under 0.7 below is superseded.

Run BT4-SparkingControls.exe, select BT4decomp.exe, then Save and start.
Target reviewed: BT4 B14 Rev2 / v0.1.39.2 R7.1 Hotfix; earlier reviewed interfaces are retained.

The historical Circle-only behavior in 0.7–0.9 is superseded by 0.9.1.

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
