using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Introductie.Data;
using Introductie.Models;
using System;

namespace Introductie.Controllers
{
    public class AfsprakenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AfsprakenController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Afspraken
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(GetAfspraken());

            } else
            {
                return Redirect("/Identity/Account/Login");

            }

        }

        // GET: Afspraken/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afspraak = await _context.Afspraken.SingleOrDefaultAsync(m => m.AfspraakID == id);
            if (afspraak == null)
            {
                return NotFound();
            }

            return View(afspraak);
        }

        // GET: Afspraken/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Afspraken/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AfspraakID,Datum,Tijd")] Afspraak afspraak)
        {
            if (ModelState.IsValid)
            {
                _context.Add(afspraak);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(afspraak);
        }

        // GET: Afspraken/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afspraak = await _context.Afspraken.SingleOrDefaultAsync(m => m.AfspraakID == id);
            if (afspraak == null)
            {
                return NotFound();
            }
            return View(afspraak);
        }

        // POST: Afspraken/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AfspraakID,Datum,Tijd")] Afspraak afspraak)
        {
            if (id != afspraak.AfspraakID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(afspraak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AfspraakExists(afspraak.AfspraakID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(afspraak);
        }

        // GET: Afspraken/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afspraak = await _context.Afspraken.SingleOrDefaultAsync(m => m.AfspraakID == id);
            if (afspraak == null)
            {
                return NotFound();
            }

            return View(afspraak);
        }

        // POST: Afspraken/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var afspraak = await _context.Afspraken.SingleOrDefaultAsync(m => m.AfspraakID == id);
            _context.Afspraken.Remove(afspraak);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AfspraakExists(int id)
        {
            return _context.Afspraken.Any(e => e.AfspraakID == id);
        }


        // zet afspraken in een lijst
        private List<Afspraak> GetAfspraken()
        {
            // maak nieuwe lijst aan
            List<Afspraak> lijst = new List<Afspraak>();

            foreach (int id in _context.Afspraken.Select(x => x.AfspraakID))
            {
               
                if (Convert.ToDateTime(GetAfspraak(id).Datum) >= DateTime.Today)
                { 
                lijst.Add(GetAfspraak(id));
                }

            }

            return lijst;
        }

        // haal afspraak op uit database
        private Afspraak GetAfspraak(int? id)
        {
            return _context.Afspraken
                .Include(x => x.Klant)
                .SingleOrDefault(m => m.AfspraakID == id);
        }
    }
}
