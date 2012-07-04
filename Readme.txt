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

INFO
StringSha.GetSha256Managed("admin") == 8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918
manager == 6EE4A469CD4E91053847F5D3FCB61DBCC91E8F0EF10BE7748DA4C4A1BA382D17
assistant == A39A7FFAD4A3013F29DA97B84F264337F234C1CF9B3C40C7C30C677A8A18609A
employee == 2FDC0177057D3A5C6C2C0821E01F4FA8D90F9A3BB7AFD82B0DB526AF98D68DE8

Pytania:

Wymagania do danych
W tabeli Permission id musi byæ od 0 do max bez wartoœci pustych (nie mo¿e byæ np. takiej sytuacji {0,1,3})
W tabeli Status_type typ Pending validation musi mieæ index 0, ¿eby by³ domyœlnym statusem wpisywanym przy braniu urlopu przez pracownika.

Do zrobienia na koñcu:
Obs³uga b³êdów przy wszystkich transakcjach, lub wykonaniach zapytañ.
Przemyœlenie wszystich transakcji.
Zmiana nazw metod na z du¿ych liter.



 todo list:
 * - Zawrzeæ w komentarzu wszystkie wyj¹tki jakie rzucaj¹ metody z DatabaseOperator.
 * 
 * - Przetestowaæ metodê z DatabaseOperator  GetLeave.
 * 
 * - Przetestowaæ metodê z DatabaseOperator  private static void DeleteLeave(LeaveManagerForm form, Leave leave)
 * 
 * - Dodaæ sta³y typ urlopu "na ¿¹danie". Pamiêtaæ, ¿e trzeba zablokowaæ mo¿liwoœæ usuniêcia go.
 * 
 * - Zjeba³em grubo: Wszêdzie urlop rozpoznajê po parz employee.id i pierwszy dzieñ urlopu zak³adaj¹c, 
 * ¿e s¹ unikalne a wcale nie musz¹ byæ (dodanie chorobowego identycznego z istniej¹cym urlopem). Trzeba
 * dodaæ id do tabeli urlopów i po nim je rozpoznawaæ ;/.. no i wszystkie u¿ycia przekodziæ ;/
 * 
 * - Dodanie ewidencji godzin/dni pracy i uwzglêdnienie tego przy sprawdzaniu ile dni konsumuje urlop.
 * 
 * - Du¿o praktyczniejsze by³oby rozwi¹zanie, w którym w urlopie przechowujemy liczbê dni, 
 * które u¿ywa. (0 gdy nie dany urlop dni nie u¿ywa np. ze wzglêdu na typ).
 * 
 * - w metodzie private static void addLeave(LeaveManagerForm form, Leave leave)
 * nie zawsze jest konieczna transakcja. Mo¿naby nie zawsze jej wymagaæ.
 * 
 * - w metodzie private static void DeleteLeave(LeaveManagerForm form, Leave leave)
 * nie zawsze jest konieczna transakcja. Mo¿naby nie zawsze jej wymagaæ
 * 
 * - Czy dodawanie urlopu zachodz¹cego na urlop ze stanem odrzuconym/anulowanym(przez chorobowe) powiedzie siê?
- Obs³uga b³êdów przy wszystkich odow³aniach do metod z DatabaseOperator.
- Zmiana nazw metod na z du¿ych liter.
