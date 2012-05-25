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

INFO
StringSha.GetSha256Managed("admin") == 8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918

Pytania:
Kiedy ustawiaæ now¹ pulê leave days?
Jakie mo¿liwoœci edycji urlopu daæ rejestratorce/kierownikowi?

Wymagania do danych
W tabeli Permission id musi byæ od 0 do max bez wartoœci pustych (nie mo¿e byæ np. takiej sytuacji {0,1,3})

Do zrobienia na koñcu:
Obs³uga b³êdów przy wszystkich transakcjach, lub wykonaniach zapytañ.