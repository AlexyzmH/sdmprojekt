using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrzeug_Datenbank
{
    class Motorräder : Fahrzeuge
    {
        private int räder;

        public Motorräder() 
        {

        }

        public Motorräder(string typ, string hersteller, string modell, int kW, double preis, int räder) : base(typ, hersteller, modell, kW, preis)
        {
            this.räder = 2;
        }

        public int getRäder() 
        {
            return räder;
        }

        public void setRäder(int räder)
        {
            this.räder = 2;
        }
    }
}
