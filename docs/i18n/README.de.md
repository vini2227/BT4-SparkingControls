[English](../../README.md) · [Français](README.fr.md) · [Italiano](README.it.md) · [Deutsch](README.de.md) · [Español (España)](README.es.md) · [العربية](README.ar.md) · [Bahasa Indonesia](README.id.md) · [Español (Latinoamérica)](README.es-419.md) · [Português (Brasil)](README.pt.md) · [Polski](README.pl.md) · [Русский](README.ru.md) · [简体中文](README.zh-Hans.md) · [繁體中文](README.zh-Hant.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · [ไทย](README.th.md)

---

# BT4 Sparking Controls — 0.9.7 Test

Neu in 0.9.7: geprüfte neue native Eingabevarianten, symbolisch aufgelöste Adressen, getrennte Erkennung der beiden Argumentregister und Erhalt zusätzlicher Zielfelder. Prüfung nur beim Start. Echte Struktur- oder Protokolländerungen können weiterhin eine Überprüfung erfordern.

Neu in 0.9.5: native Tasten und Richtungen zur Erholung am Boden; R1 und ↑/↓ fordern die Erholung in unterstützten Luftreaktionen an; geprüfte neue Spielschnittstellen; letzter EXE/CMD-Pfad wird auch bei fehlgeschlagener Prüfung automatisch gespeichert; Quadrat/Kreis folgt Position und Skalierung des Dreiecks im neueren zentrierten HUD. Regeln, Zeiten und Kosten bestimmt weiterhin BT4. Ältere Versionen bleiben unterstützt. Enthält 0.9.1–0.9.5; automatische Tests ersetzen kein echtes Online-Match.

Entpacke das Windows-ZIP und starte BT4-SparkingControls.exe. Wähle BT4decomp.exe oder eine .cmd-Datei im selben Ordner und speichere und starte. Version 0.9.7 Test enthält Korrekturen für Schriftarten, CMD-Start, Strahlduelle und Porträts. Einstellungen bleiben erhalten. Nur Windows.

[Windows ZIP](https://github.com/vini2227/BT4-SparkingControls/releases/download/v0.9.7-test/BT4-SparkingControls-0.9.7-Test-Windows.zip) · [0.9.7 Changes / English](../../CHANGELOG.md) · [Full English guide](../../README.md)

## Steuerung

□ = Quadrat, △ = Dreieck, ○ = Kreis, LS = linker Analogstick. LS-Pfeile bezeichnen den Stick, nicht das Steuerkreuz. Die normale Menünavigation bleibt unverändert.

| Taste / Kombination | Aktion |
| --- | --- |
| `□` | Rush-Angriffe |
| `△` | Ki-Schüsse |
| `X` | Native Bewegung und Dash |
| `R2` | Ki aufladen |
| `L1` | Aufsteigen |
| `L2` | Absteigen |
| `R2 + L1` | Vertikaler Z Dash nach oben |
| `R2 + L2` | Vertikaler Z Dash nach unten |
| `R1` | Blocken außerhalb eigener unterstützter Kombos |
| `R2 + □` | Superangriff 1 |
| `R2 + △` | Superangriff 2 |
| `R2 + ○` | Ultimativer Angriff unter nativen Voraussetzungen |
| `R2 + D-pad ↑ / ↓` | Fähigkeit 1 |
| `R2 + D-pad ← / →` | Fähigkeit 2 |
| `○` | Native Konterhaltung |
| `R1 (combo)` | Nativer Vorwärtsteleport in unterstützten Kombos |
| `R1 + LS (combo)` | Nativer Richtungsteleport in diesen Kombos |
| `R1 + □` | Normaler Griff |
| `R1 + LS↓ + □` | Alternativer Bodengriff, nur Analogstick |
| `LS↑ + △` | Bodengriff mit Originaleingabe |
| `R3` | Emergency Blaster Wave bei nativer Verfügbarkeit |
| `L3` | Ziel suchen / anvisieren |
| `Select / View` | Kamera wechseln |
| `D-pad ↑` | Halten: Verwandlungen/Fusionen |
| `D-pad ←` | Halten: Teamwechsel |
| `D-pad ↓ / → (no R2)` | Im Kampf gegen unbeabsichtigte Eingaben gesperrt |

Spezialangriffe lassen sich bei bereits gehaltenem R2 wählen. Zum weiteren Aufladen die zugewiesene Angriffstaste halten; R2 allein loszulassen beendet dies nicht. Bei einer Boost-Aufforderung □ + angezeigte Richtung verwenden. Das Overlay ersetzt nur das Dreieck und lässt die Hand sichtbar. Die Fähigkeitskürzel sind keine exakte Kopie der ZERO-Steuerung.

○ fordert BT4s Konterhaltung mit Vorrang vor gehaltenen Angriffen in unterstützten Kombos an, nicht ZERO s vollständiges Perception/Super-Perception-System. R1-Teleports behalten native Ki-Anforderungen und Zeitfenster; sie garantieren keine Position hinter dem Gegner und keinen Stillstand. R1 + □ hat Vorrang und erzeugt X–Loslassen–X, abhängig von Reichweite und Verfügbarkeit. Doppel-X bleibt erhalten. Steuerkreuz ↓ wählt keinen Bodengriff; LS↑ + ○ löst ihn nicht aus. R3 verwendet BT4s Lebenskosten und Bedingungen statt ZERO s Skill-Count-Revenge-Counter. Originale physische R3-Verwandlungs-/Fusionseingaben werden abgefangen; die Karten erzeugen die nötigen Eingaben intern.

## Verwandlung, Fusion und Wechsel

Steuerkreuz ↑ halten und die auf der Karte angezeigte Taste □/△/X/○ drücken; Loslassen schließt. L1/R1 wechseln nur zwischen vorhandenen Verwandlungen und Fusionen. Leere Kategorien werden übersprungen; reine Fusionscharaktere öffnen direkt Fusionen. Steuerkreuz ← halten, um Teammitglieder zu wählen. Partner, Form, Anzeigen, Abklingzeiten und Kampfzustand bleiben native Voraussetzungen. Einheitliche Kartenpositionen, leere Platzhalter und Reserve-Lebensbalken. Online stehen lokale Menüs links, auch als Spieler 2; die logische Eingabeseite bleibt korrekt.


## HUD / F6 · F7 · F8

F6: attack panel · F7: outer frame · F8: panel placement. Configure shortcuts and shortcut lock in the launcher. Settings: `%LOCALAPPDATA%\BT4-SparkingControls`.

[Compatibility, validation, source and license / English](../../README.md#compatibility-online-and-validation)
