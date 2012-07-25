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
	- Dodano mo�liwo�� dodawania chorobowego

ver 0.16
	- przeniesiono wi�kszo�� zapyta� do jednej klasy DatabaseOperator. Obiekt tej klasy jest przekazywany do formularzy.

ver. 0.17
	- Poprawiono kilka b��d�w.
	- Skomentowano wi�kszo�� kodu (klasa DatabaseOperator wymaga dokomentowania).
	- Program wymaga gruntownego testowania po przebudowie.

ver. 0.18
	- Poprawiono kilka b��d�w.
	- Sko�czono komentowa� klas� DatabaseOperator.
	- Program wymaga gruntownego testowania po przebudowie.

ver. 0.19
	- Uzupe�niono list� todo w pliku Readme.txt.
	- Przeprowadzono powierzchowne testy wszystkich funkcji.
	- Naprawiono dodawanie zg�oszenia urlopowego przez pracownika.
	- Skomentowano LeaveManagerForm.

ver. 0.2
	- Dodano pole Leave_ID w tabeli urlop�w.
	- Zmieniono nazwy metod na z du�ej litery.
	- Naprawiono kilka b��d�w.
	- Dodano pole Used_days w tabeli urlop�w. Przechowuje ono liczb� dni zu�ytych przez urlop.
	- Skomentowano rodzaje wyj�tk�w rzucanych przez metody zdefiniowane w klasie DataOperator.

ver. 0.21
	- Dodano sta�y typ urlopu na ��danie.
	- Naprawiono b�ad zwi�zany z dodawaniem urlop�w w miejsce urlop�w odrzuconych/anulowanych.
	- Zabroniono zast�powa� usuwany typ urlopu typem, kt�ry ma inn� warto�� parametru okre�laj�cego
		czy dany typ konsumuje dni, czy nie.
	- Dodano mo�liwo�� usuwania typu urlopu bez zast�powania go innym. W takim wypadku wszystkie
		wpisy urlopowe danego typu zostaj� usuni�te, a dni za nie zostaj� zwr�cone.
	- Zabrano rejestratorce mo�liwo�� edycji wpisu urlopowego za wyj�tkiem uwag.
	- Naprawiono kilka b��d�w.


ver. 0.22
	- Edycja danych pracownik�w.
	- Wyszukiwanie pracownik�w.
	- Stworzono prototyp formatki ustalania godzin pracy pracownika.

ver. 0.225
	- FormLeaveApplication > po zmianie typu urlopu na extraordinary blokuje si� mo�liwo�� wyboru stanu urlopu.
	- Dodano kierownikowi dost�p do formularza danych urlopowych pracownika.
	- Dodano obs�ug� guzika Reject without consideration w formularzach rejestratorki i kierownika.
	- Zmieniono argument przyjmowany przez metod� deleteLeave z obiektu urlopu na numer id urlopu.
	- Dodano mo�liwo�� usuwania urlop�w z poziomu formularza danych urlopowych pracownika.
	- Coroczna aktualizacja liczby dost�pnych dni urlopowych.

ver. 0.23
	- Dodano mo�liwo�� edycji godzin pracy przez kierownika.

ver. 0.24
	- Usprawniono mo�liwo�� edycji godzin pracy.
	- Sprawdzanie ilo�ci dni urlopu na podstawie rozpiski godzin pracy.
ver. 0.25
	- Dodano wypisywanie liczby dost�pnych pracownik�w w formularzu LeaveConsideretion
	- Podczas dodawania pracownika dodawany jest r�wnie� odpowiedni wiersz do tabeli Work_hours

INFO
StringSha.GetSha256Managed("admin") == 8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918
manager == 6EE4A469CD4E91053847F5D3FCB61DBCC91E8F0EF10BE7748DA4C4A1BA382D17
assistant == A39A7FFAD4A3013F29DA97B84F264337F234C1CF9B3C40C7C30C677A8A18609A
employee == 2FDC0177057D3A5C6C2C0821E01F4FA8D90F9A3BB7AFD82B0DB526AF98D68DE8

Pytania:

Wymagania do danych:
W tabeli Permission id musi by� od 0 do max bez warto�ci pustych (nie mo�e by� np. takiej sytuacji {0,1,3})
W tabeli Status_type typ Pending validation musi mie� index 0, �eby by� domy�lnym statusem wpisywanym przy braniu urlopu przez pracownika.

Do zrobienia na ko�cu:
Obs�uga b��d�w przy wszystkich transakcjach, lub wykonaniach zapyta�.
Przemy�lenie wszystich transakcji.


 todo list:
- Zamieni� wszystkie DateTime.Now na pobieranie czasu z bazy danych.

- Obs�uga b��d�w przy wszystkich odow�aniach do metod z DatabaseOperator.

- w metodzie private static void addLeave(LeaveManagerForm form, Leave leave)
nie zawsze jest konieczna transakcja. Mo�naby nie zawsze jej wymaga�.
 
- w metodzie private static void DeleteLeave(LeaveManagerForm form, Leave leave)
nie zawsze jest konieczna transakcja. Mo�naby nie zawsze jej wymaga�

- Przetestowa� gruntownie edycj� urlopu przez managera (np. zmiana typu na nie konsumuj�cy dni itp. itd.)

Nie zaiplementowane:

- Zast�pstwa
- Manager>Raportowanie
- Rozpisywanie dni zaj�tych przez innych pracownik�w tego samego typu w oknie rozwa�ania zg�oszenia urlopowego.
- Liczby wymaganych pracownik�w na danej pozycji
- Dodanie tabeli zawieraj�cej dni ustawowo wolne od pracy i uwzgl�dnienie ich.


Opcjonalnie:
- Usuwanie pracownika.
- Dodanie stanu urlop w trakcie i urlop zako�czony.
- Auto logout
- Usun�� konstruktory bezargumentowe > nie u�ywa� .GetType do por�wnania typ�w a s�owa kluczowego "is"
