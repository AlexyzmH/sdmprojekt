using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrzeug_Datenbank
{
    class Fahrzeuge
    {
        private string typ;
        private string hersteller;
        private string modell;
        private int kW;
        private double preis;
        private List<Motor> Motorenliste;

        public Fahrzeuge() 
        {

        }

        public Fahrzeuge(string typ, string hersteller, string modell, int kW, double preis) 
        {
            this.typ = typ;
            this.hersteller = hersteller;
            this.modell = modell;
            this.kW = kW;
            this.preis = preis;
        }

        public string getTyp() 
        {
            return typ;
        }

        public void setTyp(string typ)
        {
            this.typ = typ;
        }

        public string getHersteller() 
        {
            return hersteller;
        }

        public void setHersteller(string hersteller)
        {
            this.hersteller = hersteller;
        }

        public string getModell() 
        {
            return modell;
        }

        public void setModell(string modell)
        {
            this.modell = modell;
        }

        public int getKW() 
        {
            return kW;
        }

        public void setKW(int kW)
        {
            this.kW = kW;
        }

        public double getPreis() 
        {
            return preis;
        }

        public void setPreis(double preis)
        {
            this.preis = preis;
        }

        public int kWinPS(int kW) 
        {
            int PS;
            PS = Convert.ToInt16(kW * 1.35962);
            return PS;
        }
    }
}
