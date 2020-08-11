using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Introductie.Models.ViewModels
{
    public class KlantAfspraak
    {
        public KlantAfspraak()
        {
            this.NietBeschikbareDatums = new List<string>();
            this.NietBeschikbareTijden = new List<string>();
        }

        public string Datum { get; set; }
        public string Tijd { get; set; }
        public Klant Klant { get; set; }
        public List<string> NietBeschikbareDatums { get; set; }
        public List<string> NietBeschikbareTijden { get; set; }
    }
}
