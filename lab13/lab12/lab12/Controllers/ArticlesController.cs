using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab12.Data;
using lab12.Models;
using Microsoft.AspNetCore.Hosting;
using lab12.ViewModels;
using System.IO;

namespace lab12.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        private static string no_img = "no_image.jpg";

        public ArticlesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Articles.Include(a => a.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Image,CategoryId")] CreateArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                string imgPath;
                string uniqueName;

                if (articleVM.Image != null)
                {
                    uniqueName = Guid.NewGuid().ToString() + " " + articleVM.Image.FileName;
                    imgPath = Path.Combine("upload", uniqueName);
                    string streamPath = Path.Combine(_webHostEnvironment.WebRootPath, imgPath);
                    var stream = new FileStream(streamPath, FileMode.Create);
                    articleVM.Image.CopyTo(stream);
                }
                else
                {
                    imgPath = Path.Combine("image", no_img);
                }

                Article article = new()
                {
                    Id = articleVM.Id,
                    Name = articleVM.Name,
                    Price = articleVM.Price,
                    Filename = imgPath,
                    CategoryId = articleVM.CategoryId
                };

                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", articleVM.CategoryId);
            return View(articleVM);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Filename,CategoryId")] EditArticleVM articleView)
        {
            if (id != articleView.Id)
            {
                return NotFound();
            }

            Article article = _context.Articles.Find(articleView.Id);

            if (ModelState.IsValid)
            {
                try
                {
                    article.Name = articleView.Name;
                    article.Price = articleView.Price;
                    article.CategoryId = articleView.CategoryId;

                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(articleView.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", articleView.CategoryId);
            return View(article);

        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);

            if (!article.Filename.StartsWith("image"))
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, article.Filename);
                System.IO.File.Delete(path);
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
