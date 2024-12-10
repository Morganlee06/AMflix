using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AMflix.Data;
using AMflix.Models;

namespace AMflix.Controllers
{
    public class MovieReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieReviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MovieReviews.Include(m => m.Movies);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MovieReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieReviews = await _context.MovieReviews
                .Include(m => m.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieReviews == null)
            {
                return NotFound();
            }

            return View(movieReviews);
        }

        // GET: MovieReviews/Create
        public IActionResult Create()
        {
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id");
            return View();
        }

        // POST: MovieReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MoviesId,Name,Review,PublishDate")] MovieReviews movieReviews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieReviews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id", movieReviews.MoviesId);
            return View(movieReviews);
        }

        // GET: MovieReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieReviews = await _context.MovieReviews.FindAsync(id);
            if (movieReviews == null)
            {
                return NotFound();
            }
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id", movieReviews.MoviesId);
            return View(movieReviews);
        }

        // POST: MovieReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MoviesId,Name,Review,PublishDate")] MovieReviews movieReviews)
        {
            if (id != movieReviews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieReviews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieReviewsExists(movieReviews.Id))
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
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id", movieReviews.MoviesId);
            return View(movieReviews);
        }

        // GET: MovieReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieReviews = await _context.MovieReviews
                .Include(m => m.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieReviews == null)
            {
                return NotFound();
            }

            return View(movieReviews);
        }

        // POST: MovieReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieReviews = await _context.MovieReviews.FindAsync(id);
            if (movieReviews != null)
            {
                _context.MovieReviews.Remove(movieReviews);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieReviewsExists(int id)
        {
            return _context.MovieReviews.Any(e => e.Id == id);
        }
    }
}
