using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fahrzeug_Datenbank
{
    class Motor
    {
        private string bezeichnung;

        public Motor() 
        {

        }

        public Motor(string bezeichnung)
        {
            this.bezeichnung = bezeichnung;
        }

        public string getBezeichnung() 
        {
            return bezeichnung;
        }

        public void setBezeichnung(string bezeichnung)
        {
            this.bezeichnung = bezeichnung;
        }
    }
}
