ver. 0.1 
	- Utworzono wiêkszoœæ formatek
ver. 0.11 
	- Zmieniono nazwy czêœci klas, dodano plik .gitignore który powoduje ignorowanie 
	plików generownych przy kompilacji podczas aktualizacji repozytorium.
ver. 0.12 
	- Dodano do projektu bazê danych w formie pliku.
	- Dodano plik Readme.txt
	- Stworzono formatkê wyboru rodzaju po³¹czenia do bazy (plik lub serwer).
	- Poprawiono nazwy elementów w formatkach.
	- Dodano diagramy.
ver. 0.13
	- Zaktualizowano diagram ERD
	- Dodano FormAdmin
	- Stworzono prowizoryczne logowanie
ver. 0.14 
	- Zmodyfikowano ERD
	- Dodano formularz dodawania pracownika
	- Dodano mo¿liwoœæ informowania nowego pracownika o loginie i haœle przez administratora
	- Dodano klasê StringSha posiadaj¹c¹ statyczn¹ metodê do wyznaczenia skrótu sha256managed
	- Has³a s¹ zapisywane w bazie poprzezz skrót sha256managed
	- Dodano formularz zmiany has³a i loginu
	- Dodano klasê Dictionary pozwalaj¹c¹ pobraæ z bazy zawartoœci tabel s³ownikowych
	- Dodano klasê LeaveType
	- Dodano klasê TimeTools rozszerzaj¹c¹ strukturê DateTime o dodatkowe funkcje:
		=> Trim pozwalaj¹c¹ 'obci¹æ' datê do okr¹g³ej liczby dni, minut itp. poprzez podanie np. TimeSpan.TicksPerDay
		=> GetNumberOfWorkDays
		=> IsDateFromPeriodUsed informuj¹c¹, czy jakaœ data z przedzia³u czasu jest ju¿ zarezerwowana w jakimœ zg³oszeniu urlopowym.
	- Dodano mo¿liwoœæ zg³oszenia chêci odbycia urlopu

ver. 0.15
	- Naprawiono zg³aszanie chêci urlopu	
	- Dodano mo¿liwoœæ zatwierdzania/odrzucania urlopu przez asystentke	
	- Dodano mo¿liwoœæ zatwierdzania/odrzucania urlopu przez kierownika
	- Dodano mo¿liwoœæ dodawania/usuwania nowych wpisów do s³owników pozycji i typów urlopów przez administratora
	- Dodano mo¿liwoœæ edycji wpisów urlopowych przez asystentke
	- Dodano mo¿liwoœæ dodawania chorobowego

ver 0.16
	- przeniesiono wiêkszoœæ zapytañ do jednej klasy DatabaseOperator. Obiekt tej klasy jest przekazywany do formularzy.

ver. 0.17
	- Poprawiono kilka b³êdów.
	- Skomentowano wiêkszoœæ kodu (klasa DatabaseOperator wymaga dokomentowania).
	- Program wymaga gruntownego testowania po przebudowie.

ver. 0.18
	- Poprawiono kilka b³êdów.
	- Skoñczono komentowaæ klasê DatabaseOperator.
	- Program wymaga gruntownego testowania po przebudowie.

ver. 0.19
	- Uzupe³niono listê todo w pliku Readme.txt.
	- Przeprowadzono powierzchowne testy wszystkich funkcji.
	- Naprawiono dodawanie zg³oszenia urlopowego przez pracownika.
	- Skomentowano LeaveManagerForm.

ver. 0.2
	- Dodano pole Leave_ID w tabeli urlopów.
	- Zmieniono nazwy metod na z du¿ej litery.
	- Naprawiono kilka b³êdów.
	- Dodano pole Used_days w tabeli urlopów. Przechowuje ono liczbê dni zu¿ytych przez urlop.
	- Skomentowano rodzaje wyj¹tków rzucanych przez metody zdefiniowane w klasie DataOperator.

ver. 0.21
	- Dodano sta³y typ urlopu na ¿¹danie.
	- Naprawiono b³ad zwi¹zany z dodawaniem urlopów w miejsce urlopów odrzuconych/anulowanych.
	- Zabroniono zastêpowaæ usuwany typ urlopu typem, który ma inn¹ wartoœæ parametru okreœlaj¹cego
		czy dany typ konsumuje dni, czy nie.
	- Dodano mo¿liwoœæ usuwania typu urlopu bez zastêpowania go innym. W takim wypadku wszystkie
		wpisy urlopowe danego typu zostaj¹ usuniête, a dni za nie zostaj¹ zwrócone.
	- Zabrano rejestratorce mo¿liwoœæ edycji wpisu urlopowego za wyj¹tkiem uwag.
	- Naprawiono kilka b³êdów.


ver. 0.22
	- Edycja danych pracowników.
	- Wyszukiwanie pracowników.
	- Stworzono prototyp formatki ustalania godzin pracy pracownika.

ver. 0.225
	- FormLeaveApplication > po zmianie typu urlopu na extraordinary blokuje siê mo¿liwoœæ wyboru stanu urlopu.
	- Dodano kierownikowi dostêp do formularza danych urlopowych pracownika.
	- Dodano obs³ugê guzika Reject without consideration w formularzach rejestratorki i kierownika.
	- Zmieniono argument przyjmowany przez metodê deleteLeave z obiektu urlopu na numer id urlopu.
	- Dodano mo¿liwoœæ usuwania urlopów z poziomu formularza danych urlopowych pracownika.
	- Coroczna aktualizacja liczby dostêpnych dni urlopowych.

ver. 0.23
	- Dodano mo¿liwoœæ edycji godzin pracy przez kierownika.

ver. 0.24
	- Usprawniono mo¿liwoœæ edycji godzin pracy.
	- Sprawdzanie iloœci dni urlopu na podstawie rozpiski godzin pracy.
ver. 0.25
	- Dodano wypisywanie liczby dostêpnych pracowników w formularzu LeaveConsideretion
	- Podczas dodawania pracownika dodawany jest równie¿ odpowiedni wiersz do tabeli Work_hours

INFO
StringSha.GetSha256Managed("admin") == 8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918
manager == 6EE4A469CD4E91053847F5D3FCB61DBCC91E8F0EF10BE7748DA4C4A1BA382D17
assistant == A39A7FFAD4A3013F29DA97B84F264337F234C1CF9B3C40C7C30C677A8A18609A
employee == 2FDC0177057D3A5C6C2C0821E01F4FA8D90F9A3BB7AFD82B0DB526AF98D68DE8

Pytania:

Wymagania do danych:
W tabeli Permission id musi byæ od 0 do max bez wartoœci pustych (nie mo¿e byæ np. takiej sytuacji {0,1,3})
W tabeli Status_type typ Pending validation musi mieæ index 0, ¿eby by³ domyœlnym statusem wpisywanym przy braniu urlopu przez pracownika.

Do zrobienia na koñcu:
Obs³uga b³êdów przy wszystkich transakcjach, lub wykonaniach zapytañ.
Przemyœlenie wszystich transakcji.


 todo list:
- Zamieniæ wszystkie DateTime.Now na pobieranie czasu z bazy danych.

- Obs³uga b³êdów przy wszystkich odow³aniach do metod z DatabaseOperator.

- w metodzie private static void addLeave(LeaveManagerForm form, Leave leave)
nie zawsze jest konieczna transakcja. Mo¿naby nie zawsze jej wymagaæ.
 
- w metodzie private static void DeleteLeave(LeaveManagerForm form, Leave leave)
nie zawsze jest konieczna transakcja. Mo¿naby nie zawsze jej wymagaæ

- Przetestowaæ gruntownie edycjê urlopu przez managera (np. zmiana typu na nie konsumuj¹cy dni itp. itd.)

Nie zaiplementowane:

- Zastêpstwa
- Manager>Raportowanie
- Rozpisywanie dni zajêtych przez innych pracowników tego samego typu w oknie rozwa¿ania zg³oszenia urlopowego.
- Liczby wymaganych pracowników na danej pozycji
- Dodanie tabeli zawieraj¹cej dni ustawowo wolne od pracy i uwzglêdnienie ich.


Opcjonalnie:
- Usuwanie pracownika.
- Dodanie stanu urlop w trakcie i urlop zakoñczony.
- Auto logout
- Usun¹æ konstruktory bezargumentowe > nie u¿ywaæ .GetType do porównania typów a s³owa kluczowego "is"
