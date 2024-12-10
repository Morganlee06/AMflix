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
    public class MovieRatingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieRatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieRatings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MovieRating.Include(m => m.Movies);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MovieRatings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieRating = await _context.MovieRating
                .Include(m => m.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieRating == null)
            {
                return NotFound();
            }

            return View(movieRating);
        }

        // GET: MovieRatings/Create
        public IActionResult Create()
        {
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id");
            return View();
        }

        // POST: MovieRatings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MoviesId,Rating")] MovieRating movieRating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieRating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id", movieRating.MoviesId);
            return View(movieRating);
        }

        // GET: MovieRatings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieRating = await _context.MovieRating.FindAsync(id);
            if (movieRating == null)
            {
                return NotFound();
            }
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id", movieRating.MoviesId);
            return View(movieRating);
        }

        // POST: MovieRatings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MoviesId,Rating")] MovieRating movieRating)
        {
            if (id != movieRating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieRatingExists(movieRating.Id))
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
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id", movieRating.MoviesId);
            return View(movieRating);
        }

        // GET: MovieRatings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieRating = await _context.MovieRating
                .Include(m => m.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieRating == null)
            {
                return NotFound();
            }

            return View(movieRating);
        }

        // POST: MovieRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieRating = await _context.MovieRating.FindAsync(id);
            if (movieRating != null)
            {
                _context.MovieRating.Remove(movieRating);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieRatingExists(int id)
        {
            return _context.MovieRating.Any(e => e.Id == id);
        }
    }
}
