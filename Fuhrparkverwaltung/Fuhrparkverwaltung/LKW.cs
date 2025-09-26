using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrparkverwaltung
{
    internal class LKW : Fahrzeug
    {
        private double ladekapazitaet;

        public LKW()
        {
        
        }

        public LKW(string kennzeichen, string hersteller, string modell, int baujahr, double ladekapazitaet) : base(kennzeichen, hersteller, modell, baujahr)
        {
            this.ladekapazitaet = ladekapazitaet;
        }

        public double GetLadekapazitaet()
        {
            return ladekapazitaet;
        }

        public void SetLadekapazitaet(double ladekapazitaet)
        {
            this.ladekapazitaet = ladekapazitaet;
        }
    }
}
