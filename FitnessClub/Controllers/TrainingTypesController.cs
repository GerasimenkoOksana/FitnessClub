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
    public class TrainingTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TrainingTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrainingTypes.ToListAsync());
        }

        // GET: TrainingTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingType = await _context.TrainingTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingType == null)
            {
                return NotFound();
            }

            return View(trainingType);
        }

        // GET: TrainingTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainingTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TrainingType trainingType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingType);
        }

        // GET: TrainingTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingType = await _context.TrainingTypes.FindAsync(id);
            if (trainingType == null)
            {
                return NotFound();
            }
            return View(trainingType);
        }

        // POST: TrainingTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TrainingType trainingType)
        {
            if (id != trainingType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingTypeExists(trainingType.Id))
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
            return View(trainingType);
        }

        // GET: TrainingTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingType = await _context.TrainingTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingType == null)
            {
                return NotFound();
            }

            return View(trainingType);
        }

        // POST: TrainingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingType = await _context.TrainingTypes.FindAsync(id);
            _context.TrainingTypes.Remove(trainingType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingTypeExists(int id)
        {
            return _context.TrainingTypes.Any(e => e.Id == id);
        }
    }
}
