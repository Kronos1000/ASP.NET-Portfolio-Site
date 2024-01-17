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
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;

namespace PatrickWare.Controllers
{
    public class PreviousExperienceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PreviousExperienceController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
                    var fileName = System.IO.Path.GetFileName(ProjectImageFile.FileName);
                    var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ProjectImageFile.CopyToAsync(stream);
                    }

                    previousExperience.ProjectImageFileName = fileName;

                    // Make sure to dispose of the FileStream before attempting to read the image
                    using (var imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    using (var image = System.Drawing.Image.FromStream(imageStream))
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
        public async Task<IActionResult> Edit(int id, [Bind("ProjectID,ProjectTitle,ShortProjectDescription,ProjectDescription,ProjectPurpose,ProjectImageFileName,DevLanguages,GitRepoLink,roundedBorder,ImageWidth,ImageHeight")] PreviousExperience previousExperience, IFormFile? ProjectImageFile)
        {
            if (id != previousExperience.ProjectID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve existing portfolio item from the database
                    var existingPortfolioItem = await _context.PreviousExperience.AsNoTracking().FirstOrDefaultAsync(p => p.ProjectID == id);

                    // Copy the properties from the existing portfolio item to the edited portfolio item
                    previousExperience.ProjectImageFileName = existingPortfolioItem.ProjectImageFileName; // Assuming you have an ImageUrl property
                    previousExperience.ImageWidth = existingPortfolioItem.ImageWidth;
                    previousExperience.ImageHeight = existingPortfolioItem.ImageHeight;

                    // Update other properties with the new values
                    previousExperience.ProjectDescription = Regex.Replace(previousExperience.ProjectDescription, @"(\r\n?|\n){2,}", "\n\n");

                    // Handle image upload (similar to your existing code)
                    if (ProjectImageFile != null && ProjectImageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(ProjectImageFile.FileName);
                        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "ProjectImages", fileName);

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

                    // Update portfolio item in the database
                    _context.Update(previousExperience);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
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
                catch (Exception ex)
                {
                    // Log the exception and display a user-friendly error message
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data.");
                }
            }

            return View(previousExperience);
        }


        private bool PreviousExperienceExists(int id)
        {
            return _context.PreviousExperience.Any(e => e.ProjectID == id);
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

       
    }
}
