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

- Zawrze� w komentarzu wszystkie wyj�tki jakie rzucaj� metody z DatabaseOperator.
  
- Doda� sta�y typ urlopu "na ��danie". Pami�ta�, �e trzeba zablokowa� mo�liwo�� usuni�cia go. 
  
- Dodanie ewidencji godzin/dni pracy i uwzgl�dnienie tego przy sprawdzaniu ile dni konsumuje urlop.
	- Przy zmianie dni pracy trzeba przetestowa�, czy liczba konsumowanych dni si� zgadza i nie przekracza
	liczby dni dost�pnych. Np. kto� robi w pon i �r, wzi�� urlop od pon do �r, wi�c mu 2 dni wzi�o. Gdy 
	zmieni mu si� harmonogram pracy i dojd� wtorki do niego, a urlop jest p�niej, to trzeba zweryfikowa�
	liczb� konsumowanych dni.

- Du�o praktyczniejsze by�oby rozwi�zanie, w kt�rym w urlopie przechowujemy liczb� dni, 
kt�re u�ywa. (0 gdy nie dany urlop dni nie u�ywa np. ze wzgl�du na typ). 
<<<Rozwi�za�o by to te� problem
z usuwanymi typami konsumuj�cymi i podmian� przez typ nie konsumuj�cy. W takim wyj�tkowym wypadku
typ nie konsumuj�cy konsumowa�by dni.
 
- w metodzie private static void addLeave(LeaveManagerForm form, Leave leave)
nie zawsze jest konieczna transakcja. Mo�naby nie zawsze jej wymaga�.
 
- w metodzie private static void DeleteLeave(LeaveManagerForm form, Leave leave)
nie zawsze jest konieczna transakcja. Mo�naby nie zawsze jej wymaga�
 
- Czy dodawanie urlopu zachodz�cego na urlop ze stanem odrzuconym/anulowanym(przez chorobowe) 
powiedzie si�?: NIE!! A POWINNO!

- Obs�uga b��d�w przy wszystkich odow�aniach do metod z DatabaseOperator.

- Usun�� mo�liwo�� usuni�cia typu urlopu: maternal i normal. 

- Jest zgrzyt: Gdy usunie si� typ, kt�ry konsumuje dni i przypisze w jego miejsce jaki�, kt�ry dni nie konsumuje.

- Employee>usuwanie urlopu nie dzia�a (gdzie� nie jest zamkni�ty SqlDataReader)

- Assistant>zabra� mo�liwo�� edycji wpis�w urlopowych (mo�e opr�cz uwag.)

- Dodawanie urlopu pracownikowi przez assistant> Trzeba z listy status�w usun�� Canceled, bo to bez sensu.

- Metoda dodawani chorobowego szwankuje: gdy np. biore urlop na 16-17, a dostaje 
chorobowe na 16, to urlop mi anuluje, a nie powinno.



Nie zaiplementowane:
- Usuwanie pracownika.
- Wyszukiwanie pracownik�w
- Manager>ogl�danie danych pracownik�w
- Zast�pstwa
- Manager>Raportowanie
- Dodanie stanu urlop w trakcie i urlop zako�czony.
- Assistant/Manager>Reject without consideration
- Rozpisywanie dni zaj�tych przez innych pracownik�w tego samego typu w oknie rozwa�ania zg�oszenia urlopowego.
- Liczby wymaganych pracownik�w na danej pozycji
- Resetowania dni urlopowych przy zmianie roku
- Przydzielanie tylko cz�ci przys�uguj�cych dni urlopowych przy rozpocz�ciu pracy w zale�no�ci 
od dnia zatrudnienia.
- EmployeeData>Delete leave entry
- Ewidencja godzin/dni pracy.

In progress:
- Zjeba�em grubo: Wsz�dzie urlop rozpoznaj� po parze employee.id i pierwszy dzie� urlopu zak�adaj�c, 
 �e s� unikalne a wcale nie musz� by� (dodanie chorobowego identycznego z istniej�cym urlopem). Trzeba
 doda� id do tabeli urlop�w i po nim je rozpoznawa� ;/.. no i wszystkie u�ycia przekodzi� ;/
	(Wojtek)

Notki zrobione: 
- Zmiana nazw metod na z du�ych liter.
