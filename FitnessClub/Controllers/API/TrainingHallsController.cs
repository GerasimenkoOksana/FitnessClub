using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data;
using FitnessClub.Data.Entities;

namespace FitnessClub.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingHallsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainingHallsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainingHalls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingHall>>> GetTrainingHalls()
        {
            return await _context.TrainingHalls.ToListAsync();
        }

        // GET: api/TrainingHalls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingHall>> GetTrainingHall(int id)
        {
            var trainingHall = await _context.TrainingHalls.FindAsync(id);

            if (trainingHall == null)
            {
                return NotFound();
            }

            return trainingHall;
        }

        // PUT: api/TrainingHalls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingHall(int id, TrainingHall trainingHall)
        {
            if (id != trainingHall.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainingHall).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingHallExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TrainingHalls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrainingHall>> PostTrainingHall(TrainingHall trainingHall)
        {
            _context.TrainingHalls.Add(trainingHall);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingHall", new { id = trainingHall.Id }, trainingHall);
        }

        // DELETE: api/TrainingHalls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingHall(int id)
        {
            var trainingHall = await _context.TrainingHalls.FindAsync(id);
            if (trainingHall == null)
            {
                return NotFound();
            }

            _context.TrainingHalls.Remove(trainingHall);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainingHallExists(int id)
        {
            return _context.TrainingHalls.Any(e => e.Id == id);
        }
    }
}
