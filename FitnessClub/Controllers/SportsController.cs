using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data;
using FitnessClub.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sports
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sports.ToListAsync());
        }

        // GET: Sports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sport = await _context.Sports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sport == null)
            {
                return NotFound();
            }

            return View(sport);
        }

        // GET: Sports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Sport sport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sport);
        }

        // GET: Sports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sport = await _context.Sports.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }
            return View(sport);
        }

        // POST: Sports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Sport sport)
        {
            if (id != sport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportExists(sport.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sport);
        }

        // GET: Sports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sport = await _context.Sports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sport == null)
            {
                return NotFound();
            }

            return View(sport);
        }

        // POST: Sports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sport = await _context.Sports.FindAsync(id);
            _context.Sports.Remove(sport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportExists(int id)
        {
            return _context.Sports.Any(e => e.Id == id);
        }
    }
}
