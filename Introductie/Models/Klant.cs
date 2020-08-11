using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Introductie.Models
{
    public class Klant
    {
        public Klant()
        {
            this.Afspraken = new List<Afspraak>();
        }

        public int KlantID { get; set; }
        public string Naam { get; set; }
        public string TrouwDatum { get; set; }
        public string Telnr { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Afspraak> Afspraken { get; set; }
    }
}
