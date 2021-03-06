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
    public class TrainingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trainings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Trainings.Include(t => t.Hall).Include(t => t.Sport).Include(t => t.TrainingType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Trainings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Trainings
                .Include(t => t.Hall)
                .Include(t => t.Sport)
                .Include(t => t.TrainingType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // GET: Trainings/Create
        public IActionResult Create()
        {
            ViewData["HallId"] = new SelectList(_context.TrainingHalls, "Id", "Name");
            ViewData["SportId"] = new SelectList(_context.Sports, "Id", "Name");
            ViewData["TrainingTypeId"] = new SelectList(_context.TrainingTypes, "Id", "Name");
            return View();
        }

        // POST: Trainings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SportId,TrainerId,HallId,TrainingTypeId")] Training training)
        {
            if (ModelState.IsValid)
            {
                _context.Add(training);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HallId"] = new SelectList(_context.TrainingHalls, "Id", "Name", training.HallId);
            ViewData["SportId"] = new SelectList(_context.Sports, "Id", "Name", training.SportId);
            ViewData["TrainingTypeId"] = new SelectList(_context.TrainingTypes, "Id", "Name", training.TrainingTypeId);
            return View(training);
        }

        // GET: Trainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Trainings.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }
            ViewData["HallId"] = new SelectList(_context.TrainingHalls, "Id", "Name", training.HallId);
            ViewData["SportId"] = new SelectList(_context.Sports, "Id", "Name", training.SportId);
            ViewData["TrainingTypeId"] = new SelectList(_context.TrainingTypes, "Id", "Name", training.TrainingTypeId);
            return View(training);
        }

        // POST: Trainings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SportId,TrainerId,HallId,TrainingTypeId")] Training training)
        {
            if (id != training.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(training);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingExists(training.Id))
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
            ViewData["HallId"] = new SelectList(_context.TrainingHalls, "Id", "Name", training.HallId);
            ViewData["SportId"] = new SelectList(_context.Sports, "Id", "Name", training.SportId);
            ViewData["TrainingTypeId"] = new SelectList(_context.TrainingTypes, "Id", "Name", training.TrainingTypeId);
            return View(training);
        }

        // GET: Trainings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Trainings
                .Include(t => t.Hall)
                .Include(t => t.Sport)
                .Include(t => t.TrainingType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // POST: Trainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var training = await _context.Trainings.FindAsync(id);
            _context.Trainings.Remove(training);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingExists(int id)
        {
            return _context.Trainings.Any(e => e.Id == id);
        }
    }
}
