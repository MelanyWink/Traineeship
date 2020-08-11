using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Introductie.Models
{
    public class Afspraak
    {

        public int AfspraakID { get; set; }
        public string Datum { get; set; }
        public string Tijd { get; set; }

        public virtual Klant Klant { get; set; }
    }
}
