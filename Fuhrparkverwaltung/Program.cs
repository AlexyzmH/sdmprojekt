using System;

namespace Fuhrparkverwaltung
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starte Fuhrparkverwaltung...");

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n--- Hauptmenü ---");
                Console.WriteLine("1: Fahrzeug suchen");
                Console.WriteLine("2: Fahrzeug verwalten");
                Console.WriteLine("3: Fahrzeug hinzufügen");
                Console.WriteLine("0: Beenden");
                Console.Write("Bitte wähle eine Option: ");
                string input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        Console.Write("Gib ein Kennzeichen zum Suchen ein: ");
                        string kennzeichen = Console.ReadLine();

                        Fahrzeug gefundenesFahrzeug = SqlManager.GetFahrzeugByKennzeichen(kennzeichen);

                        if (gefundenesFahrzeug != null)
                        {
                            if (gefundenesFahrzeug is LKW lkw)
                            {
                                Console.WriteLine($"Gefunden: {lkw.GetHersteller()} {lkw.GetModell()}, Baujahr {lkw.GetBaujahr()}, Ladekapazität: {lkw.GetLadekapazitaet()} Tonnen");
                            }
                            else if (gefundenesFahrzeug is PKW pkw)
                            {
                                Console.WriteLine($"Gefunden: {pkw.GetHersteller()} {pkw.GetModell()}, Baujahr {pkw.GetBaujahr()}, Türen: {pkw.GetAnzahlTueren()}");
                            }
                            else
                            {
                                Console.WriteLine($"Gefunden: {gefundenesFahrzeug.GetHersteller()} {gefundenesFahrzeug.GetModell()}, Baujahr {gefundenesFahrzeug.GetBaujahr()}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Kein Fahrzeug mit diesem Kennzeichen gefunden.");
                        }
                        break;

                    case "2":
                        Console.WriteLine("\n--- Fahrzeug verwalten ---");
                        Console.WriteLine("1: Fahrzeug löschen");
                        Console.WriteLine("2: Fahrzeugdetails ändern");
                        Console.WriteLine("0: Zurück zum Hauptmenü"); 
                        Console.Write("Wähle eine Option: ");
                        string verwaltenOption = Console.ReadLine();
                        Console.Clear();


                        switch (verwaltenOption)
                        {
                            case "1": // Fahrzeug löschen
                                Console.Write("Kennzeichen des zu löschenden Fahrzeugs: ");
                                string loeschKennzeichen = Console.ReadLine();
                                SqlManager.DeleteFahrzeugByKennzeichen(loeschKennzeichen); // ✅ Einfach aufrufen
                                break;

                            case "2": // Fahrzeug ändern
                                Console.Write("Kennzeichen des zu ändernden Fahrzeugs: ");
                                string aendernKennzeichen = Console.ReadLine();
                                Fahrzeug f = SqlManager.GetFahrzeugByKennzeichen(aendernKennzeichen);

                                if (f == null)
                                {
                                    Console.WriteLine("Fahrzeug nicht gefunden.");
                                    break;
                                }

                                bool updating = true;
                                while (updating)
                                {
                                    Console.WriteLine("\n--- Was soll geändert werden? ---");
                                    Console.WriteLine("1: Alles ändern");
                                    Console.WriteLine("2: Hersteller");
                                    Console.WriteLine("3: Modell");
                                    Console.WriteLine("4: Baujahr");

                                    if (f is PKW)
                                        Console.WriteLine("5: Anzahl der Türen");
                                    else if (f is LKW)
                                        Console.WriteLine("5: Ladekapazität");

                                    Console.WriteLine("0: Zurück zum Hauptmenü");
                                    Console.Write("Deine Auswahl: ");
                                    string auswahl = Console.ReadLine();

                                    switch (auswahl)
                                    {
                                        case "1": // Alles ändern
                                            Console.Write("Neuer Hersteller: ");
                                            f.SetHersteller(Console.ReadLine());

                                            Console.Write("Neues Modell: ");
                                            f.SetModell(Console.ReadLine());

                                            Console.Write("Neues Baujahr: ");
                                            if (int.TryParse(Console.ReadLine(), out int neuesBJ))
                                            {
                                                f.SetBaujahr(neuesBJ);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Ungültiges Baujahr.");
                                            }

                                            if (f is PKW pkwAll)
                                            {
                                                Console.Write("Neue Anzahl der Türen: ");
                                                if (int.TryParse(Console.ReadLine(), out int neueTuerenAll))
                                                {
                                                    pkwAll.SetAnzahlTueren(neueTuerenAll);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Ungültige Anzahl.");
                                                }
                                            }
                                            else if (f is LKW lkwAll)
                                            {
                                                Console.Write("Neue Ladekapazität (in Tonnen): ");
                                                if (double.TryParse(Console.ReadLine(), out double neueKapazitaetAll))
                                                {
                                                    lkwAll.SetLadekapazitaet(neueKapazitaetAll);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Ungültige Ladekapazität.");
                                                }
                                            }

                                            SqlManager.UpdateFahrzeug(f);
                                            Console.WriteLine("Alle Änderungen gespeichert.");
                                            break;

                                        case "2":
                                            Console.Write("Neuer Hersteller: ");
                                            f.SetHersteller(Console.ReadLine());
                                            break;

                                        case "3":
                                            Console.Write("Neues Modell: ");
                                            f.SetModell(Console.ReadLine());
                                            break;

                                        case "4":
                                            Console.Write("Neues Baujahr: ");
                                            if (int.TryParse(Console.ReadLine(), out int neuesBaujahr))
                                            {
                                                f.SetBaujahr(neuesBaujahr);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Ungültiges Baujahr.");
                                            }
                                            break;

                                        case "5":
                                            if (f is PKW pkw)
                                            {
                                                Console.Write("Neue Anzahl der Türen: ");
                                                if (int.TryParse(Console.ReadLine(), out int neueTueren))
                                                {
                                                    pkw.SetAnzahlTueren(neueTueren);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Ungültige Eingabe.");
                                                }
                                            }
                                            else if (f is LKW lkw)
                                            {
                                                Console.Write("Neue Ladekapazität (in Tonnen): ");
                                                if (double.TryParse(Console.ReadLine(), out double neueKapazitaet))
                                                {
                                                    lkw.SetLadekapazitaet(neueKapazitaet);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Ungültige Eingabe.");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Ungültige Eingabe.");
                                            }
                                            break;

                                        case "0":
                                            updating = false;
                                            Console.Clear();
                                            continue;

                                        default:
                                            Console.WriteLine("Ungültige Eingabe.");
                                            continue;
                                    }

                                    SqlManager.UpdateFahrzeug(f);
                                    Console.WriteLine("Änderung gespeichert.");
                                }

                                break;

                            case "0":
                                Console.Clear();
                                break;

                            default:
                                Console.WriteLine("Ungültige Eingabe.");
                                break;
                        }

                        break;




                    case "3":
                        Console.WriteLine("\nFahrzeugtyp anwählen:");
                        Console.WriteLine("1: PKW");
                        Console.WriteLine("2: LKW");
                        Console.Write("Bitte wähle den Fahrzeugtyp: ");
                        string fahrzeugTyp = Console.ReadLine();

                        Console.Write("Kennzeichen: ");
                        string neuesKennzeichen = Console.ReadLine();

                        Console.Write("Hersteller: ");
                        string hersteller = Console.ReadLine();

                        Console.Write("Modell: ");
                        string modell = Console.ReadLine();

                        int baujahr;
                        while (true)
                        {
                            Console.Write("Baujahr: ");
                            if (int.TryParse(Console.ReadLine(), out baujahr))
                                break;
                            Console.WriteLine("Ungültiges Baujahr. Bitte erneut eingeben.");
                        }


                        if (fahrzeugTyp == "2")
                        {
                            double ladekapazitaet;
                            while (true)
                            {
                                Console.Write("Ladekapazität (in Tonnen): ");
                                if (double.TryParse(Console.ReadLine(), out ladekapazitaet))
                                    break;
                                Console.WriteLine("Ungültige Ladekapazität. Bitte erneut eingeben.");
                            }


                            LKW neuerLKW = new LKW(neuesKennzeichen, hersteller, modell, baujahr, ladekapazitaet);
                            SqlManager.InsertFahrzeug(neuerLKW);
                            Console.WriteLine("LKW wurde hinzugefügt.");
                        }
                        else if (fahrzeugTyp == "1")
                        {
                            int anzahlTueren;
                            while (true)
                            {
                                Console.Write("Anzahl der Türen: ");
                                if (int.TryParse(Console.ReadLine(), out anzahlTueren))
                                    break;
                                Console.WriteLine("Ungültige Anzahl der Türen. Bitte erneut eingeben.");
                            }

                            PKW neuerPKW = new PKW(neuesKennzeichen, hersteller, modell, baujahr, anzahlTueren);
                            SqlManager.InsertFahrzeug(neuerPKW);
                            Console.WriteLine("PKW wurde hinzugefügt.");
                        }
                        else
                        {
                            Console.WriteLine("Ungültige Fahrzeugtyp-Auswahl.");
                        }

                        break;

                    case "0":
                        running = false;
                        Console.Clear();
                        Console.WriteLine("Programm beendet.");
                        break;

                    default:
                        Console.WriteLine("Ungültige Eingabe.");
                        break;
                }
            }
        }
    }
}
