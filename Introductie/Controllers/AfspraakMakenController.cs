using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Introductie.Models.ViewModels;
using Introductie.Models;
using Introductie.Data;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Introductie.Controllers
{
    public class AfspraakMakenController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AfspraakMakenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<AfspraakStappenController>/
        public IActionResult Index()
        {
            return View();
        }


        // Stap1Van1
        public IActionResult Stap1Van1()
        {
            KlantAfspraak afspraakDatums = new KlantAfspraak
            {
                // haal datums uit de database vanaf huidige dag die 3 of meer keer voorkomen om vervolgens uit te schakelen in de datepicker
                NietBeschikbareDatums = _context.Afspraken.
                                                Select(d => d.Datum).ToList().
                                                Where(d => Convert.ToDateTime(d) >= DateTime.Today).
                                                GroupBy(d => d).Where(d => d.Count() >= 3).
                                                Select(d => d.Key).ToList()
            };

            return View(afspraakDatums);
        }

        // POST Stap2Van1
        [HttpPost]
        public IActionResult Stap2Van1(KlantAfspraak afspraak)
        {
            // haal bezette tijden op van geselecteerde datum om vervolgens uit te schakelen
            afspraak.NietBeschikbareTijden = _context.Afspraken.Where(s => s.Datum == afspraak.Datum).Select(s => s.Tijd).ToList();

            return View(afspraak);
        }


        // POST Stap2
        [HttpPost]
        public IActionResult Stap2(KlantAfspraak afspraak)
        {

            return View(afspraak);
        }


        // POST Stap3
        [HttpPost]
        public IActionResult Stap3(KlantAfspraak afspraak)
        {
            return View(afspraak);
        }


        // POST AfspraakVoltooid
        [HttpPost]
        public IActionResult AfspraakVoltooid(KlantAfspraak afspraak)
        {
            // maak nieuwe klant en afspraak
            Klant klant = new Klant();
            Afspraak newAfspraak = new Afspraak();

            // zet gegevens over van viewmodel naar normale model
            klant.Naam = afspraak.Klant.Naam;
            klant.TrouwDatum = afspraak.Klant.TrouwDatum;
            klant.Telnr = afspraak.Klant.Telnr;
            klant.Email = afspraak.Klant.Email;

            newAfspraak.Datum = afspraak.Datum;
            newAfspraak.Tijd = afspraak.Tijd;

            // voeg de afspraak toe aan de klant
            klant.Afspraken.Add(newAfspraak);

            // voeg klant toe aan de database
            _context.Klanten.Add(klant);
            _context.SaveChanges();          

            return View();
        }
    }
}
