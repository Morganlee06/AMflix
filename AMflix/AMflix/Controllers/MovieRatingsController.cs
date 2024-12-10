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

        // Constructor: Initializes the controller with the database context.
        public MovieRatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieRatings
        // Retrieves and displays a list of movie ratings along with their associated movies.
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MovieRating.Include(m => m.Movies); // Includes related movie data.
            return View(await applicationDbContext.ToListAsync()); // Passes the data to the View for display.
        }

        // GET: MovieRatings/Details/5
        // Retrieves and displays the details of a specific movie rating by its ID.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) // Checks if the ID is null.
            {
                return NotFound(); // Returns a "Not Found" result if the ID is invalid.
            }

            var movieRating = await _context.MovieRating
                .Include(m => m.Movies) // Includes related movie data.
                .FirstOrDefaultAsync(m => m.Id == id); // Finds the rating by its ID.
            if (movieRating == null) // Checks if the rating does not exist.
            {
                return NotFound(); // Returns a "Not Found" result if the rating is not found.
            }

            return View(movieRating); // Passes the rating data to the View.
        }

        // GET: MovieRatings/Create
        // Displays the form for creating a new movie rating.
        public IActionResult Create()
        {
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id"); // Populates the dropdown list for selecting a movie.
            return View(); // Returns the Create View.
        }

        // POST: MovieRatings/Create
        // Handles the submission of the Create form.
        // Protects against overposting by only binding specific properties.
        [HttpPost]
        [ValidateAntiForgeryToken] // Validates the Anti-Forgery Token for security.
        public async Task<IActionResult> Create([Bind("Id,MoviesId,Rating")] MovieRating movieRating)
        {
            if (ModelState.IsValid) // Checks if the user-provided data is valid.
            {
                _context.Add(movieRating); // Adds the rating to the database context.
                await _context.SaveChangesAsync(); // Saves changes to the database asynchronously.
                return RedirectToAction(nameof(Index)); // Redirects to the Index action after successful creation.
            }
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id", movieRating.MoviesId); // Repopulates the dropdown list if validation fails.
            return View(movieRating); // Returns the Create View with the submitted data.
        }

        // GET: MovieRatings/Edit/5
        // Displays the form for editing an existing movie rating.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) // Checks if the ID is null.
            {
                return NotFound(); // Returns a "Not Found" result if the ID is invalid.
            }

            var movieRating = await _context.MovieRating.FindAsync(id); // Finds the rating by its ID.
            if (movieRating == null) // Checks if the rating does not exist.
            {
                return NotFound(); // Returns a "Not Found" result if the rating is not found.
            }
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id", movieRating.MoviesId); // Populates the dropdown list for selecting a movie.
            return View(movieRating); // Passes the rating data to the Edit View.
        }

        // POST: MovieRatings/Edit/5
        // Handles the submission of the Edit form.
        // Protects against overposting by only binding specific properties.
        [HttpPost]
        [ValidateAntiForgeryToken] // Validates the Anti-Forgery Token for security.
        public async Task<IActionResult> Edit(int id, [Bind("Id,MoviesId,Rating")] MovieRating movieRating)
        {
            if (id != movieRating.Id) // Checks if the ID in the URL does not match the ID in the model.
            {
                return NotFound(); // Returns a "Not Found" result if there's a mismatch.
            }

            if (ModelState.IsValid) // Checks if the user-provided data is valid.
            {
                try
                {
                    _context.Update(movieRating); // Updates the rating in the database context.
                    await _context.SaveChangesAsync(); // Saves changes to the database asynchronously.
                }
                catch (DbUpdateConcurrencyException) // Handles concurrency issues.
                {
                    if (!MovieRatingExists(movieRating.Id)) // Checks if the rating still exists.
                    {
                        return NotFound(); // Returns a "Not Found" result if the rating no longer exists.
                    }
                    else
                    {
                        throw; // Rethrows the exception if another issue occurred.
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirects to the Index action after successful update.
            }
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id", movieRating.MoviesId); // Repopulates the dropdown list if validation fails.
            return View(movieRating); // Returns the Edit View with the submitted data.
        }

        // GET: MovieRatings/Delete/5
        // Displays a confirmation page for deleting a movie rating.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) // Checks if the ID is null.
            {
                return NotFound(); // Returns a "Not Found" result if the ID is invalid.
            }

            var movieRating = await _context.MovieRating
                .Include(m => m.Movies) // Includes related movie data.
                .FirstOrDefaultAsync(m => m.Id == id); // Finds the rating by its ID.
            if (movieRating == null) // Checks if the rating does not exist.
            {
                return NotFound(); // Returns a "Not Found" result if the rating is not found.
            }

            return View(movieRating); // Passes the rating data to the Delete View.
        }

        // POST: MovieRatings/Delete/5
        // Handles the confirmation of the Delete action.
        [HttpPost, ActionName("Delete")] // Maps this method to the Delete action name.
        [ValidateAntiForgeryToken] // Validates the Anti-Forgery Token for security.
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieRating = await _context.MovieRating.FindAsync(id); // Finds the rating by its ID.
            if (movieRating != null) // Checks if the rating exists.
            {
                _context.MovieRating.Remove(movieRating); // Removes the rating from the database context.
            }

            await _context.SaveChangesAsync(); // Saves changes to the database asynchronously.
            return RedirectToAction(nameof(Index)); // Redirects to the Index action after successful deletion.
        }

        // Helper method to check if a movie rating exists in the database by its ID.
        private bool MovieRatingExists(int id)
        {
            return _context.MovieRating.Any(e => e.Id == id); // Returns true if a rating with the specified ID exists.
        }
    }
}
