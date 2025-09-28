using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrparkverwaltung
{
    internal class Fahrzeug
    {
        private string kennzeichen;
        private string hersteller;
        private string modell;
        private int baujahr;

        public Fahrzeug() 
        {

        }

        public Fahrzeug(string kennzeichen, string hersteller, string modell, int baujahr)
        {
            this.kennzeichen = kennzeichen;
            this.hersteller = hersteller;
            this.modell = modell;
            this.baujahr = baujahr;
        }

        public string GetKennzeichen() 
        {
            return kennzeichen;
        }

        public void SetKennzeichen(string kennzeichen) 
        {
            this.kennzeichen = kennzeichen;
        }

        public string GetHersteller() 
        {
            return hersteller;
        }

        public void SetHersteller(string hersteller) 
        {
            this.hersteller = hersteller;
        }

        public string GetModell() 
        {
            return modell;
        }

        public void SetModell(string modell) 
        {
            this.modell = modell;
        }

        public int GetBaujahr() 
        {
            return baujahr;
        }

        public void SetBaujahr(int baujahr) 
        {
            this.baujahr = baujahr;
        }
    }
}
