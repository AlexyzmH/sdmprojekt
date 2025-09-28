using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrparkverwaltung
{
    internal class Fuhrpark
    {
        private List<Fahrzeug> fuhrparkListe;

        public Fuhrpark()
        {
            
        }

        public Fuhrpark(List<Fahrzeug> fuhrparkListe)
        {
            this.fuhrparkListe = fuhrparkListe;
        }

        public List<Fahrzeug> GetFuhrparkListe()
        {
            return fuhrparkListe;
        }

        public void SetFuhrparkListe(List<Fahrzeug> fuhrparkListe)
        {
            this.fuhrparkListe = fuhrparkListe;
        }
    }
}
