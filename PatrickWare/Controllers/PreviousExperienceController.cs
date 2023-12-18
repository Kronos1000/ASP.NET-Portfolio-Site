using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatrickWare.Data;
using PatrickWare.Models;

namespace PatrickWare.Controllers
{
    public class PreviousExperienceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PreviousExperienceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PreviousExperience
        public async Task<IActionResult> Index()
        {
              return _context.PreviousExperience != null ? 
                          View(await _context.PreviousExperience.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PreviousExperience'  is null.");
        }

        // GET: PreviousExperience/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PreviousExperience == null)
            {
                return NotFound();
            }

            var previousExperience = await _context.PreviousExperience
                .FirstOrDefaultAsync(m => m.ProjectID == id);
            if (previousExperience == null)
            {
                return NotFound();
            }

            return View(previousExperience);
        }

        // GET: PreviousExperience/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PreviousExperience/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectID,ProjectTitle,ShortProjectDescription,ProjectDescription,ProjectPurpose,ProjectImageFileName,DevLanguages,GitRepoLink,roundedBorder,ImageWidth,ImageHeight")] PreviousExperience previousExperience)
        {
            if (ModelState.IsValid)
            {
                _context.Add(previousExperience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(previousExperience);
        }

        // GET: PreviousExperience/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PreviousExperience == null)
            {
                return NotFound();
            }

            var previousExperience = await _context.PreviousExperience.FindAsync(id);
            if (previousExperience == null)
            {
                return NotFound();
            }
            return View(previousExperience);
        }

        // POST: PreviousExperience/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectID,ProjectTitle,ShortProjectDescription,ProjectDescription,ProjectPurpose,ProjectImageFileName,DevLanguages,GitRepoLink,roundedBorder,ImageWidth,ImageHeight")] PreviousExperience previousExperience)
        {
            if (id != previousExperience.ProjectID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(previousExperience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreviousExperienceExists(previousExperience.ProjectID))
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
            return View(previousExperience);
        }

        // GET: PreviousExperience/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PreviousExperience == null)
            {
                return NotFound();
            }

            var previousExperience = await _context.PreviousExperience
                .FirstOrDefaultAsync(m => m.ProjectID == id);
            if (previousExperience == null)
            {
                return NotFound();
            }

            return View(previousExperience);
        }

        // POST: PreviousExperience/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PreviousExperience == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PreviousExperience'  is null.");
            }
            var previousExperience = await _context.PreviousExperience.FindAsync(id);
            if (previousExperience != null)
            {
                _context.PreviousExperience.Remove(previousExperience);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreviousExperienceExists(int id)
        {
          return (_context.PreviousExperience?.Any(e => e.ProjectID == id)).GetValueOrDefault();
        }
    }
}
