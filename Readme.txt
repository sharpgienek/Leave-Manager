ver. 0.1 
	- Utworzono wi�kszo�� formatek
ver. 0.11 
	- Zmieniono nazwy cz�ci klas, dodano plik .gitignore kt�ry powoduje ignorowanie 
	plik�w generownych przy kompilacji podczas aktualizacji repozytorium.
ver. 0.12 
	- Dodano do projektu baz� danych w formie pliku.
	- Dodano plik Readme.txt
	- Stworzono formatk� wyboru rodzaju po��czenia do bazy (plik lub serwer).
	- Poprawiono nazwy element�w w formatkach.
	- Dodano diagramy.
ver. 0.13
	- Zaktualizowano diagram ERD
	- Dodano FormAdmin
	- Stworzono prowizoryczne logowanie
ver. 0.14
	- Zmodyfikowano ERD
	- Dodano formularz dodawania pracownika
	- Dodano mo�liwo�� informowania nowego pracownika o loginie i ha�le przez administratora
	- Dodano klas� StringSha posiadaj�c� statyczn� metod� do wyznaczenia skr�tu sha256managed
	- Has�a s� zapisywane w bazie poprzezz skr�t sha256managed
	- Dodano formularz zmiany has�a i loginu
	- Dodano klas� Dictionary pozwalaj�c� pobra� z bazy zawarto�ci tabel s�ownikowych
	- Dodano klas� LeaveType
	- Dodano klas� TimeTools rozszerzaj�c� struktur� DateTime o dodatkowe funkcje:
		=> Trim pozwalaj�c� 'obci��' dat� do okr�g�ej liczby dni, minut itp. poprzez podanie np. TimeSpan.TicksPerDay
		=> GetNumberOfWorkDays
		=> IsDateFromPeriodUsed informuj�c�, czy jaka� data z przedzia�u czasu jest ju� zarezerwowana w jakim� zg�oszeniu urlopowym.
	- Dodano mo�liwo�� zg�oszenia ch�ci odbycia urlopu

ver. 0.15
	- Naprawiono zg�aszanie ch�ci urlopu	
	- Dodano mo�liwo�� zatwierdzania/odrzucania urlopu przez asystentke	
	- Dodano mo�liwo�� zatwierdzania/odrzucania urlopu przez kierownika
	- Dodano mo�liwo�� dodawania/usuwania nowych wpis�w do s�ownik�w pozycji i typ�w urlop�w przez administratora
	- Dodano mo�liwo�� edycji wpis�w urlopowych przez asystentke

INFO
StringSha.GetSha256Managed("admin") == 8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918

Pytania:
Kiedy ustawia� now� pul� leave days?
Jakie mo�liwo�ci edycji urlopu da� rejestratorce/kierownikowi?

Wymagania do danych
W tabeli Permission id musi by� od 0 do max bez warto�ci pustych (nie mo�e by� np. takiej sytuacji {0,1,3})

Do zrobienia na ko�cu:
Obs�uga b��d�w przy wszystkich transakcjach, lub wykonaniach zapyta�.