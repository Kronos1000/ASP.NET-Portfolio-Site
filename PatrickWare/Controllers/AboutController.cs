using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using PatrickWare.Data;
using PatrickWare.Models;
using System.Text.RegularExpressions;

namespace PatrickWare.Controllers
{
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AboutController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: About
        public async Task<IActionResult> Index()
        {
              return _context.About != null ? 
                          View(await _context.About.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.About'  is null.");
        }

        // GET: About/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.About == null)
            {
                return NotFound();
            }

            var about = await _context.About
                .FirstOrDefaultAsync(m => m.Id == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // GET: About/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: About/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Headings,Description,ImageFileName,roundedBorder,ImageWidth,ImageHeight")] About about, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    about.ImageFileName = fileName;

                    // Make sure to dispose of the FileStream before attempting to read the image
                    using (var imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    using (var image = Image.FromStream(imageStream))
                    {
                        about.ImageWidth = image.Width;
                        about.ImageHeight = image.Height;
                    }
                }
                about.Description= Regex.Replace(about.Description, @"(\r\n?|\n){2,}", "\n\n\n");
                _context.Add(about);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(about);
        }


        // GET: About/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.About == null)
            {
                return NotFound();
            }

            var about = await _context.About.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        // POST: About/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Headings,Description,ImageFileName,roundedBorder,ImageWidth,ImageHeight")] About about)
        {
            if (id != about.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(about);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutExists(about.Id))
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
            return View(about);
        }

        // GET: About/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.About == null)
            {
                return NotFound();
            }

            var about = await _context.About
                .FirstOrDefaultAsync(m => m.Id == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // POST: About/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.About == null)
            {
                return Problem("Entity set 'ApplicationDbContext.About'  is null.");
            }
            var about = await _context.About.FindAsync(id);
            if (about != null)
            {
                _context.About.Remove(about);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutExists(int id)
        {
          return (_context.About?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
