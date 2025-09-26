using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrparkverwaltung
{
    internal class PKW : Fahrzeug
    {
        private int anzahlTueren;

        public PKW() 
        {
        
        }

        public PKW(string kennzeichen, string hersteller, string modell, int baujahr, int anzahlTueren) : base(kennzeichen, hersteller, modell, baujahr)
        {
            this.anzahlTueren = anzahlTueren;
        }

        public int GetAnzahlTueren()
        {
            return anzahlTueren;
        }

        public void SetAnzahlTueren(int anzahlTueren)
        {
            this.anzahlTueren = anzahlTueren;
        }
    }
}
