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

        // Constructor: Initializes the controller with the database context.
        public MovieReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieReviews
        // Retrieves and displays a list of movie reviews with their associated movies.
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MovieReviews.Include(m => m.Movies); // Includes related movies data.
            return View(await applicationDbContext.ToListAsync()); // Passes the data to the View.
        }

        // GET: MovieReviews/Details/5
        // Retrieves and displays the details of a specific movie review by its ID.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) // Checks if the ID is null.
            {
                return NotFound(); // Returns a "Not Found" result if the ID is invalid.
            }

            var movieReviews = await _context.MovieReviews
                .Include(m => m.Movies) // Includes related movie data.
                .FirstOrDefaultAsync(m => m.Id == id); // Finds the review by its ID.
            if (movieReviews == null) // Checks if the review does not exist.
            {
                return NotFound(); // Returns a "Not Found" result if the review is not found.
            }

            return View(movieReviews); // Passes the review data to the View.
        }

        // GET: MovieReviews/Create
        // Displays the form for creating a new movie review.
        public IActionResult Create(int movieReviewId)
        {
            ViewBag.MoviesId = movieReviewId;
            return View();
        }

        // POST: MovieReviews/Create
        // Handles the submission of the Create form.
        // Protects against overposting by only binding specific properties.
        [HttpPost]
        [ValidateAntiForgeryToken] // Validates the Anti-Forgery Token for security.
        public async Task<IActionResult> Create([Bind("Id,MoviesId,Name,Review,PublishDate")] MovieReviews movieReviews)
        {

            var movieReview = await _context.Movies.FindAsync(movieReviews.MoviesId);

            if (movieReview == null)
            {
                return View(movieReviews);
            }

            movieReviews.Movies = movieReview;

            ModelState.Remove("Movies");


            if (ModelState.IsValid) // Checks if the user-provided data is valid.
            {
                _context.Add(movieReviews); // Adds the review to the database context.
                await _context.SaveChangesAsync(); // Saves changes to the database asynchronously.
                return RedirectToAction(nameof(Index)); // Redirects to the Index action after successful creation.
            }
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id", movieReviews.MoviesId); // Repopulates the dropdown list if validation fails.
            return View(movieReviews); // Returns the Create View with the submitted data.
        }

        // GET: MovieReviews/Edit/5
        // Displays the form for editing an existing movie review.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) // Checks if the ID is null.
            {
                return NotFound(); // Returns a "Not Found" result if the ID is invalid.
            }

            var movieReviews = await _context.MovieReviews.FindAsync(id); // Finds the review by its ID.
            if (movieReviews == null) // Checks if the review does not exist.
            {
                return NotFound(); // Returns a "Not Found" result if the review is not found.
            }
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id", movieReviews.MoviesId); // Populates the dropdown list for selecting a movie.
            return View(movieReviews); // Passes the review data to the Edit View.
        }

        // POST: MovieReviews/Edit/5
        // Handles the submission of the Edit form.
        // Protects against overposting by only binding specific properties.
        [HttpPost]
        [ValidateAntiForgeryToken] // Validates the Anti-Forgery Token for security.
        public async Task<IActionResult> Edit(int id, [Bind("Id,MoviesId,Name,Review,PublishDate")] MovieReviews movieReviews)
        {
            if (id != movieReviews.Id) // Checks if the ID in the URL does not match the ID in the model.
            {
                return NotFound(); // Returns a "Not Found" result if there's a mismatch.
            }

            if (ModelState.IsValid) // Checks if the user-provided data is valid.
            {
                try
                {
                    _context.Update(movieReviews); // Updates the review in the database context.
                    await _context.SaveChangesAsync(); // Saves changes to the database asynchronously.
                }
                catch (DbUpdateConcurrencyException) // Handles concurrency issues.
                {
                    if (!MovieReviewsExists(movieReviews.Id)) // Checks if the review still exists.
                    {
                        return NotFound(); // Returns a "Not Found" result if the review no longer exists.
                    }
                    else
                    {
                        throw; // Rethrows the exception if another issue occurred.
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirects to the Index action after successful update.
            }
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id", movieReviews.MoviesId); // Repopulates the dropdown list if validation fails.
            return View(movieReviews); // Returns the Edit View with the submitted data.
        }

        // GET: MovieReviews/Delete/5
        // Displays a confirmation page for deleting a movie review.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) // Checks if the ID is null.
            {
                return NotFound(); // Returns a "Not Found" result if the ID is invalid.
            }

            var movieReviews = await _context.MovieReviews
                .Include(m => m.Movies) // Includes related movie data.
                .FirstOrDefaultAsync(m => m.Id == id); // Finds the review by its ID.
            if (movieReviews == null) // Checks if the review does not exist.
            {
                return NotFound(); // Returns a "Not Found" result if the review is not found.
            }

            return View(movieReviews); // Passes the review data to the Delete View.
        }

        // POST: MovieReviews/Delete/5
        // Handles the confirmation of the Delete action.
        [HttpPost, ActionName("Delete")] // Maps this method to the Delete action name.
        [ValidateAntiForgeryToken] // Validates the Anti-Forgery Token for security.
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieReviews = await _context.MovieReviews.FindAsync(id); // Finds the review by its ID.
            if (movieReviews != null) // Checks if the review exists.
            {
                _context.MovieReviews.Remove(movieReviews); // Removes the review from the database context.
            }

            await _context.SaveChangesAsync(); // Saves changes to the database asynchronously.
            return RedirectToAction(nameof(Index)); // Redirects to the Index action after successful deletion.
        }

        // Helper method to check if a movie review exists in the database by its ID.
        private bool MovieReviewsExists(int id)
        {
            return _context.MovieReviews.Any(e => e.Id == id); // Returns true if a review with the specified ID exists.
        }
    }
}
