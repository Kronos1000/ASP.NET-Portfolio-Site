using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Microsoft.AspNetCore.Http;

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
        public async Task<IActionResult> Create([Bind("ProjectID,ProjectTitle,ShortProjectDescription,ProjectDescription,ProjectPurpose,ProjectImageFileName,DevLanguages,GitRepoLink,roundedBorder,ImageWidth,ImageHeight")] PreviousExperience previousExperience, IFormFile ProjectImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ProjectImageFile != null && ProjectImageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(ProjectImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ProjectImageFile.CopyToAsync(stream);
                    }

                    previousExperience.ProjectImageFileName = fileName;

                    // Make sure to dispose of the FileStream before attempting to read the image
                    using (var imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    using (var image = Image.FromStream(imageStream))
                    {
                        previousExperience.ImageWidth = image.Width;
                        previousExperience.ImageHeight = image.Height;
                    }
                }

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
