[English](../../README.md) · [Français](README.fr.md) · [Italiano](README.it.md) · [Deutsch](README.de.md) · [Español (España)](README.es.md) · [العربية](README.ar.md) · [Bahasa Indonesia](README.id.md) · [Español (Latinoamérica)](README.es-419.md) · [Português (Brasil)](README.pt.md) · [Polski](README.pl.md) · [Русский](README.ru.md) · [简体中文](README.zh-Hans.md) · [繁體中文](README.zh-Hant.md) · [日本語](README.ja.md) · [한국어](README.ko.md) · [ไทย](README.th.md)

---

# BT4 Sparking Controls — 0.9.8 Test

Nowości 0.9.8: mniej błędnych blokad, gdy unikalne symbole danych zmieniają grupę kompilatora. Obsługa sprawdzonej nowej ścieżki wejścia lokalnego i natywnego kodera pomocniczego. Kod, cele i niejednoznaczne nazwy nadal są sprawdzane. Sterowanie, HUD i ustawienia zachowane; kontrola tylko przy uruchomieniu. Rzeczywiste zmiany protokołu mogą wymagać przeglądu.

Nowości 0.9.7: sprawdzone nowe warianty wejścia, adresy rozpoznawane przez symbole, niezależne rozpoznawanie rejestrów obu argumentów i zachowane dodatkowe pola wyboru celu. Kontrola tylko przy uruchomieniu. Rzeczywiste zmiany struktury lub protokołu mogą nadal wymagać przeglądu.

Nowości 0.9.5: natywne przyciski i kierunki odzyskiwania kontroli na ziemi; R1 oraz ↑/↓ żądają odzyskania kontroli w obsługiwanych reakcjach w powietrzu; sprawdzone nowe interfejsy gry; ostatni EXE/CMD zapisywany automatycznie nawet po błędzie zgodności; kwadrat/okrąg podąża za pozycją i skalą trójkąta w nowym wyśrodkowanym HUD. Reguły, czas i koszty nadal określa BT4. Starsza wersja pozostaje obsługiwana. Poprawki 0.9.1–0.9.5; testy automatyczne nie zastępują prawdziwego meczu online.

Rozpakuj ZIP Windows i uruchom BT4-SparkingControls.exe. Wybierz BT4decomp.exe albo plik .cmd w tym samym folderze, następnie zapisz i uruchom. Wersja 0.9.8 Test zawiera poprawki czcionek, uruchamiania CMD, starć i portretów. Ustawienia są zachowywane. Tylko Windows.

[Windows ZIP](https://github.com/vini2227/BT4-SparkingControls/releases/download/v0.9.8-test/BT4-SparkingControls-0.9.8-Test-Windows.zip) · [0.9.8 Changes / English](../../CHANGELOG.md) · [Full English guide](../../README.md)

## Sterowanie

□ = Kwadrat; △ = Trójkąt; ○ = Kółko; LS = lewy drążek. Strzałki LS oznaczają drążek, nie krzyżak. Zwykłe menu zachowują oryginalne sterowanie.

| Przycisk / kombinacja | Działanie |
| --- | --- |
| `□` | Ataki Rush |
| `△` | Pociski Ki |
| `X` | Natywny ruch i doskoki |
| `R2` | Ładowanie Ki |
| `L1` | Wznoszenie |
| `L2` | Opadanie |
| `R2 + L1` | Pionowy Z Dash w górę |
| `R2 + L2` | Pionowy Z Dash w dół |
| `R1` | Blok poza własnymi obsługiwanymi kombinacjami |
| `R2 + □` | Superatak 1 |
| `R2 + △` | Superatak 2 |
| `R2 + ○` | Atak ostateczny zgodnie z warunkami gry |
| `R2 + D-pad ↑ / ↓` | Zdolność 1 |
| `R2 + D-pad ← / →` | Zdolność 2 |
| `○` | Natywna postawa kontry |
| `R1 (combo)` | Natywny teleport do przodu w obsługiwanym combo |
| `R1 + LS (combo)` | Natywny teleport kierunkowy w tym combo |
| `R1 + □` | Zwykły chwyt |
| `R1 + LS↓ + □` | Alternatywny chwyt na ziemi, tylko drążek |
| `LS↑ + △` | Chwyt na ziemi oryginalnym poleceniem |
| `R3` | Emergency Blaster Wave zgodnie z warunkami gry |
| `L3` | Wyszukiwanie / namierzanie celu |
| `Select / View` | Zmiana kamery |
| `D-pad ↑` | Przytrzymaj: transformacje/fuzje |
| `D-pad ←` | Przytrzymaj: zmiana członka drużyny |
| `D-pad ↓ / → (no R2)` | Zablokowane w walce, aby zapobiec pomyłkom |

Specjały można wybierać przy już trzymanym R2. Aby dalej ładować atak, trzymaj przypisany przycisk ataku; samo puszczenie R2 go nie zwalnia. Przy komunikacie boost użyj □ + wskazanego kierunku. Nakładka zastępuje tylko Trójkąt i nie zasłania dłoni. Skróty zdolności nie odtwarzają dokładnie sterowania ZERO.

○ wywołuje kontrę BT4 z priorytetem nad trzymanym atakiem w obsługiwanych combo, nie pełne Perception/Super Perception ZERO. Teleporty R1 wymagają natywnego Ki i okien akcji; nie gwarantują pozycji za przeciwnikiem ani pozostania w miejscu. R1 + □ ma priorytet i generuje X–zwolnienie–X, zależnie od zasięgu i dostępności. Oryginalne podwójne X pozostaje. Chwyt na ziemi nie używa krzyżaka ↓; LS↑ + ○ go nie uruchamia. R3 zużywa zdrowie na warunkach BT4, zamiast punktów umiejętności Revenge Counter z ZERO. Fizyczne oryginalne skróty R3 transformacji/fuzji są przechwytywane; karty generują wymagane wejścia.

## Transformacje, fuzja i zmiana postaci

Przytrzymaj krzyżak ↑ i naciśnij □/△/X/○ z karty; puść ↑, by zamknąć. L1/R1 przełączają tylko istniejące transformacje i fuzje. Puste kategorie są pomijane; sama fuzja otwiera się od razu. Przytrzymaj krzyżak ←, aby wybrać kolegę z drużyny. Partner, forma, wskaźniki, odnowienie i stan walki nadal muszą spełniać warunki BT4. Stałe pozycje kart, puste miejsca i zdrowie rezerwowych. Online lokalne panele są po lewej także dla Gracza 2, bez zmiany logicznej strony wejścia.


## HUD / F6 · F7 · F8

F6: attack panel · F7: outer frame · F8: panel placement. Configure shortcuts and shortcut lock in the launcher. Settings: `%LOCALAPPDATA%\BT4-SparkingControls`.

[Compatibility, validation, source and license / English](../../README.md#compatibility-online-and-validation)
