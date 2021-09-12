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
    [Authorize(Roles = "Admin")]
    public class TrainingHallsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingHallsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TrainingHalls
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrainingHalls.ToListAsync());
        }

        // GET: TrainingHalls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingHall = await _context.TrainingHalls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingHall == null)
            {
                return NotFound();
            }

            return View(trainingHall);
        }

        // GET: TrainingHalls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainingHalls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TrainingHall trainingHall)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingHall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingHall);
        }

        // GET: TrainingHalls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingHall = await _context.TrainingHalls.FindAsync(id);
            if (trainingHall == null)
            {
                return NotFound();
            }
            return View(trainingHall);
        }

        // POST: TrainingHalls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TrainingHall trainingHall)
        {
            if (id != trainingHall.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingHall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingHallExists(trainingHall.Id))
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
            return View(trainingHall);
        }

        // GET: TrainingHalls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingHall = await _context.TrainingHalls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingHall == null)
            {
                return NotFound();
            }

            return View(trainingHall);
        }

        // POST: TrainingHalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingHall = await _context.TrainingHalls.FindAsync(id);
            _context.TrainingHalls.Remove(trainingHall);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingHallExists(int id)
        {
            return _context.TrainingHalls.Any(e => e.Id == id);
        }
    }
}
