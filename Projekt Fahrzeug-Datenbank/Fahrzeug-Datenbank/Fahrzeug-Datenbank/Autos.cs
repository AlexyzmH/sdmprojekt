using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrzeug_Datenbank
{
    class Autos : Fahrzeuge
    {
        private int räder;
        private string antrieb;

        public Autos() 
        {

        }

        public Autos(string typ, string hersteller, string modell, int kW, double preis, int räder, string antrieb) : base(typ, hersteller, modell, kW, preis)
        {
            this.räder = 4;
            this.antrieb = antrieb;
        }

        public int getRäder()
        {
            return räder;
        }

        public void setRäder(int räder)
        {
            this.räder = 4;
        }

        public string getAntrieb() 
        {
            return antrieb;
        }

        public void setAntrieb(string antrieb) 
        {
            this.antrieb = antrieb;
        }
    }
}
