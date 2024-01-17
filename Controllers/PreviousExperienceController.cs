using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public async Task<IActionResult> Create([Bind("ProjectID," +
            "ProjectTitle," +
            "ShortProjectDescription," +
            "ProjectDescription," +
            "ProjectPurpose," +
            "ProjectImageFileName,DevLanguages,ImageWidth,ImageHeight,GitRepoLink,roundedBorder,GalleryImage1FileName,GalleryImage2FileName,GalleryImage3FileName," +
            "GalleryImage4FileName,GalleryImage5FileName")] PreviousExperience previousExperience , IFormFile? ProjectImageFile, IFormFile? GalleryImage1, IFormFile? GalleryImage2,
            IFormFile? GalleryImage3, IFormFile? GalleryImage4, IFormFile? GalleryImage5)
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

                // Gallery Images 
                if (GalleryImage1 != null && GalleryImage1.Length > 0)
                {
                    var fileName = System.IO.Path.GetFileName(GalleryImage1.FileName);
                    var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await GalleryImage1.CopyToAsync(stream);
                    }
                    previousExperience.GalleryImage1FileName = fileName;
                }

                if (GalleryImage2 != null && GalleryImage2.Length > 0)
                {
                    var fileName = System.IO.Path.GetFileName(GalleryImage2.FileName);
                    var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await GalleryImage2.CopyToAsync(stream);
                    }

                }

                if (GalleryImage3 != null && GalleryImage3.Length > 0)
                {
                    var fileName = System.IO.Path.GetFileName(GalleryImage3.FileName);
                    var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await GalleryImage3.CopyToAsync(stream);
                    }

                }

                if (GalleryImage4 != null && GalleryImage4.Length > 0)
                {
                    var fileName = System.IO.Path.GetFileName(GalleryImage4.FileName);
                    var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await GalleryImage4.CopyToAsync(stream);
                    }

                }

                if (GalleryImage5 != null && GalleryImage5.Length > 0)
                {
                    var fileName = System.IO.Path.GetFileName(GalleryImage5.FileName);
                    var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await GalleryImage5.CopyToAsync(stream);
                    }

                }





                previousExperience.ProjectDescription = Regex.Replace(previousExperience.ProjectDescription, @"(\r\n?|\n){2,}", "\n\n\n");
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
        public async Task<IActionResult> Edit(int? id, [Bind("ProjectID," +
    "ProjectTitle," +
    "ShortProjectDescription," +
    "ProjectDescription," +
    "ProjectPurpose," +
    "ProjectImageFileName,DevLanguages,ImageWidth,ImageHeight,GitRepoLink,roundedBorder,GalleryImage1FileName,GalleryImage2FileName,GalleryImage3FileName," +
    "GalleryImage4FileName,GalleryImage5FileName")] PreviousExperience previousExperience, IFormFile? ProjectImageFile, IFormFile? GalleryImage1, IFormFile? GalleryImage2,
    IFormFile? GalleryImage3, IFormFile? GalleryImage4, IFormFile? GalleryImage5)
        {
            if (id == null)
            {
                return NotFound();
            }

            var existingExperience = await _context.PreviousExperience.FindAsync(id);

            if (existingExperience == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // _context.Entry(existingExperience).CurrentValues.SetValues(previousExperience);


                if (ProjectImageFile != null && ProjectImageFile.Length > 0)
                {
                    var fileName = System.IO.Path.GetFileName(ProjectImageFile.FileName);
                    var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ProjectImageFile.CopyToAsync(stream);
                    }

                    previousExperience.ProjectImageFileName = fileName;
                    existingExperience.ProjectImageFileName = fileName;

                    // Make sure to dispose of the FileStream before attempting to read the image
                    using (var imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    using (var image = System.Drawing.Image.FromStream(imageStream))
                    {
                        previousExperience.ImageWidth = image.Width;
                        previousExperience.ImageHeight = image.Height;
                    }
                }

                // Gallery Images 
                if (GalleryImage1 != null && GalleryImage1.Length > 0)
                {
                    var fileName = System.IO.Path.GetFileName(GalleryImage1.FileName);
                    var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await GalleryImage1.CopyToAsync(stream);
                    }

                    previousExperience.GalleryImage1FileName = fileName;
                    existingExperience.GalleryImage1FileName = fileName; // Update the property in the existingExperience object
                }

                if (GalleryImage2 != null && GalleryImage2.Length > 0)
                {
                    var fileName = System.IO.Path.GetFileName(GalleryImage2.FileName);
                    var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await GalleryImage2.CopyToAsync(stream);
                    }

                    previousExperience.GalleryImage2FileName = fileName;
                    existingExperience.GalleryImage2FileName = fileName; // Update the property in the existingExperience object
                }

                if (GalleryImage3 != null && GalleryImage3.Length > 0)
                {
                    var fileName = System.IO.Path.GetFileName(GalleryImage3.FileName);
                    var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await GalleryImage3.CopyToAsync(stream);
                    }

                    previousExperience.GalleryImage3FileName = fileName;
                    existingExperience.GalleryImage3FileName = fileName; // Update the property in the existingExperience object
                }

                if (GalleryImage4 != null && GalleryImage4.Length > 0)
                {
                    var fileName = System.IO.Path.GetFileName(GalleryImage4.FileName);
                    var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await GalleryImage4.CopyToAsync(stream);
                    }
                    previousExperience.GalleryImage4FileName = fileName;
                    existingExperience.GalleryImage4FileName = fileName; // Update the property in the existingExperience object
                }

                if (GalleryImage5 != null && GalleryImage5.Length > 0)
                {
                    var fileName = System.IO.Path.GetFileName(GalleryImage5.FileName);
                    var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await GalleryImage5.CopyToAsync(stream);
                    }

                    previousExperience.GalleryImage5FileName = fileName;
                    existingExperience.GalleryImage5FileName = fileName; // Update the property in the existingExperience object
                }



                previousExperience.ProjectDescription = Regex.Replace(existingExperience.ProjectDescription, @"(\r\n?|\n){2,}", "\n\n\n");
                existingExperience.ProjectDescription = Regex.Replace(existingExperience.ProjectDescription, @"(\r\n?|\n){2,}", "\n\n\n");

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(existingExperience);
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
