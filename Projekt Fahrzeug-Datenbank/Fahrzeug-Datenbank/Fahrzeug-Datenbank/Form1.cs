using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Fahrzeug_Datenbank
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // zwei Listen erstellen (für alle Fahrzeuge und gewählte Fahrzeuge)
        List<Fahrzeuge> alleFahrzeuge = new List<Fahrzeuge>();
        List<Fahrzeuge> gewählteFahrzeuge = new List<Fahrzeuge>();

        private void Daten_laden_Button_Click(object sender, EventArgs e) // wenn der "Daten laden"-Button gedrückt wird
        {   
            // OpenFileDialog-Fenster erstellen
            OpenFileDialog Laden = new OpenFileDialog();
            Laden.Filter = "CSV-Dateien (*.csv)|*.csv|Alle Dateien (*.*)|*.*";
            Laden.Title = "Laden Sie eine CSV-Datei";
            if (Laden.ShowDialog() == DialogResult.OK)
            {
                string Laden_Dateipfad = Laden.FileName;
            }

            StreamReader sr = new StreamReader(Laden.FileName); // StreamReader-Klasse erstellen
            while (!sr.EndOfStream) // solange der Stream nicht endet
            {
                string[] zeile = sr.ReadLine().Split(';'); // alle Zeilen werden gesplittet

                for (int i = 0; i < zeile.Length; i += 5) // for-Schleife zur Vereinfachung
                {
                    if (!string.IsNullOrEmpty(zeile[i])) // ist die erste Zeile in der for-Schleife nicht leer
                    {
                        try
                        {   // Zeile konvertieren
                            string typ = Convert.ToString(zeile[i]);
                            string hersteller = Convert.ToString(zeile[i+1]);
                            string modell = Convert.ToString(zeile[i+2]);
                            int kW = Convert.ToInt32(zeile[i+3]);
                            int PS = Convert.ToInt32(kW * 1.35962);                            
                            double preis = Convert.ToDouble(zeile[i+4]);

                            Fahrzeuge neuesFahrzeug = new Fahrzeuge(typ, hersteller, modell, kW, preis); // neues Fahrzeuge-Objekt erstellen

                            alleFahrzeuge.Add(neuesFahrzeug); // neues Objekt in die Liste aller Fahrzeuge hinzufügen
                            Fahrzeuge_ListBox1.Items.Add(Convert.ToString(modell)); // neues Objekt in die linke ListBox hinzufügen
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Fehler beim Erstellen: {ex.Message}"); // Fehlermeldung
                        }
                    }

                }
            }
        }

        private void FahrzeugAuswählen_Button_Click(object sender, EventArgs e) // wenn der "Fahrzeug auswählen"-Button gedrückt wird
        {
            try
            {
                if (Fahrzeuge_ListBox1.SelectedItem.ToString() != "") // wenn ein Item in der linken ListBox ausgewählt wurde
                {
                    foreach (Fahrzeuge Element in alleFahrzeuge) // alle Fahrzeuge in der Liste vergleichen
                    {
                        if (Element.getModell() == Fahrzeuge_ListBox1.SelectedItem.ToString()) // wenn es eine Übereinstimmung gibt
                        {
                            if (!gewählteFahrzeuge.Contains(Element) && !Fahrzeuge_ListBox2.Items.Contains(Element.getModell())) // ist das Element nicht in der Liste der gewählten Fahrzeugen als auch in der rechten ListBox vorhanden
                            {
                                // gefundenes Fahrzeug in die rechte ListBox und in die Liste der gewählten Fahrzeugen hinzufügen
                                Fahrzeuge_ListBox2.Items.Add(Element.getModell());
                                gewählteFahrzeuge.Add(Element);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Erstellen: {ex.Message}"); // Fehlermeldung
            }
        }

        private void DatenSpeichern_Button_Click(object sender, EventArgs e) // wenn der "Daten speichern"-Button gedrückt wird
        {
            try
            {
                // SaveFileDialog-Fenster erstellen
                SaveFileDialog Speichern = new SaveFileDialog();
                Speichern.Filter = "CSV-Dateien (*.csv)|*.csv|Alle Dateien (*.*)|*.*";
                Speichern.Title = "Speichern Sie eine CSV-Datei";
                if (Speichern.ShowDialog() == DialogResult.OK)
                {
                    string Speichern_Dateipfad = Speichern.FileName;
                }
                
                using (StreamWriter sw = File.CreateText(Speichern.FileName))
                
                // CSV-Datei erstellen
                foreach (Fahrzeuge Element in gewählteFahrzeuge)
                {
                    string typ = Element.getTyp();
                    string hersteller = Element.getHersteller();
                    string modell = Element.getModell();
                    int kW = Element.getKW();
                    double preis = Element.getPreis();

                    sw.WriteLine(typ.ToString() + ";" + hersteller.ToString() + ";" + modell.ToString() + ";" + kW.ToString() + ";" + preis.ToString() + ";");
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Fehler beim Erstellen: {ex.Message}"); // Fehlermeldung
            }

        }

        private void Infos_Button1_Click(object sender, EventArgs e) // wenn der linke "Infos"-Button gedrückt wird
        {
            try
            {
                if (Fahrzeuge_ListBox1.SelectedItem.ToString() != "") // wenn ein Item in der linken ListBox ausgewählt wurde
                {
                    foreach (Fahrzeuge Element in alleFahrzeuge) // alle Fahrzeuge vergleichen
                    {
                        if (Element.getModell() == Fahrzeuge_ListBox1.SelectedItem.ToString()) //wenn es eine Übereinstimmung gibt
                        {
                            // Fahrzeugdaten anzeigen
                            Fahrzeugdaten_TextBox.Text = $"Hersteller: {Element.getHersteller()}; Modell: {Element.getModell()}; {Element.getKW()} kW; {Element.kWinPS(Element.getKW())} PS; Preis: {Element.getPreis()} Euro";
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Fehler beim Erstellen: {ex.Message}"); // Fehlermeldung
            }

        }

        private void Infos_Button2_Click(object sender, EventArgs e) // wenn der rechte "Infos"-Button gedrückt wird
        {
            try
            {
                if (Fahrzeuge_ListBox2.SelectedItem.ToString() != "") // wenn ein Item in der rechten ListBox ausgewählt wurde
                {
                    foreach (Fahrzeuge Element in gewählteFahrzeuge) // gewählte Fahrzeuge vergleichen
                    {
                        if (Element.getModell() == Fahrzeuge_ListBox2.SelectedItem.ToString()) // wenn es eine Übereinstimmung gibt
                        {
                            // Fahrzeugdaten anzeigen
                            Fahrzeugdaten_TextBox.Text = $"Hersteller: {Element.getHersteller()}; Modell: {Element.getModell()}; {Element.getKW()} kW; {Element.kWinPS(Element.getKW())} PS; Preis: {Element.getPreis()} Euro";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Erstellen: {ex.Message}"); // Fehlermeldung
            }
        }

        private void FahrzeugeSuchen_Button_Click(object sender, EventArgs e) // wenn der "Fahrzeuge suchen"-Button gedrückt wird
        {
            try
            {
                if (Fahrzeug_ComboBox.Text != "") // ist die ComboBox nicht leer
                {
                    Fahrzeuge_ListBox1.Items.Clear(); // linke ListBox leeren

                    foreach (Fahrzeuge Element in alleFahrzeuge) // alle Fahrzeuge vergleichen
                    {
                        if (Element.getTyp() == Fahrzeug_ComboBox.Text) // wenn es eine Übereinstimmung gibt
                        {
                            Fahrzeuge_ListBox1.Items.Add(Convert.ToString(Element.getModell())); // alle Fahrzeuge mit einem bestimmten Fahrzeugtyp in die rechte ListBox hinzufügen
                        }
                        else if (Fahrzeug_ComboBox.Text == "alle") // wenn "alle" ausgewählt wurde
                        {
                            Fahrzeuge_ListBox1.Items.Add(Convert.ToString(Element.getModell())); // alle Fahrzeuge in die rechte ListBox hinzufügen
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Erstellen: {ex.Message}"); // Fehlermeldung
            }
        }

        private void Beenden_Button_Click(object sender, EventArgs e) // wenn der "Beenden"-Button gedrückt wird
        {
            Application.Exit(); // Applikation beenden
        }
    }
}