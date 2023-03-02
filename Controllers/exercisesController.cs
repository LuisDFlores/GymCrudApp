using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gymCrudApp.Data;
using gymCrudApp.Models;

namespace gymCrudApp.Controllers
{
    public class exercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public exercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: exercises
        public async Task<IActionResult> Index()
        {
              return View(await _context.exercise.ToListAsync());
        }

        // GET: exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.exercise == null)
            {
                return NotFound();
            }

            var exercise = await _context.exercise
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: exercises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: exercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,exerciseName,excerciseFrequency")] exercise exercise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }

        // GET: exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.exercise == null)
            {
                return NotFound();
            }

            var exercise = await _context.exercise.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        // POST: exercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,exerciseName,excerciseFrequency")] exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!exerciseExists(exercise.Id))
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
            return View(exercise);
        }

        // GET: exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.exercise == null)
            {
                return NotFound();
            }

            var exercise = await _context.exercise
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.exercise == null)
            {
                return Problem("Entity set 'ApplicationDbContext.exercise'  is null.");
            }
            var exercise = await _context.exercise.FindAsync(id);
            if (exercise != null)
            {
                _context.exercise.Remove(exercise);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool exerciseExists(int id)
        {
          return _context.exercise.Any(e => e.Id == id);
        }
    }
}
