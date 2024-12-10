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
    public class MoviesController : Controller
    {
        // Creates a private variable that cannot be changed based on the applicationdbcontext file
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context; // Crates a local version of the context
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync()); // Returns the movie home view
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) // Checks if Id is null
            {
                return NotFound(); // If Id is null returns error page
            }

            var movies = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id); // Creates movies variable storing id
            if (movies == null) // Checks if there is any data in movies  
            {
                return NotFound(); // if there arnt any return error page
            }

            return View(movies);
        }

        // GET: Movies/Create
        // Displays the form for creating a new movie entry.
        public IActionResult Create()
        {
            return View(); // Returns the View for creating a movie.
        }

        // POST: Movies/Create
        // Handles the submission of the Create form.
        // Protects against overposting by only binding specific properties.
        // For more details, see: http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] // Validates the Anti-Forgery Token for security.
        public async Task<IActionResult> Create([Bind("Id,Title,Description,AgeRating,RealeaseDate")] Movies movies)
        {
            if (ModelState.IsValid) // Checks if the data provided by the user is valid.
            {
                _context.Add(movies); // Adds the movie to the database context.
                await _context.SaveChangesAsync(); // Saves changes to the database asynchronously.
                return RedirectToAction(nameof(Index)); // Redirects to the Index action after successful creation.
            }
            return View(movies); // Returns the View with the submitted data if validation fails.
        }

        // GET: Movies/Edit/5
        // Displays the form for editing an existing movie.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) // Checks if the provided ID is null.
            {
                return NotFound(); // Returns a "Not Found" result if the ID is invalid.
            }

            var movies = await _context.Movies.FindAsync(id); // Finds the movie by its ID.
            if (movies == null) // Checks if the movie does not exist.
            {
                return NotFound(); // Returns a "Not Found" result if the movie is not found.
            }
            return View(movies); // Returns the View for editing the movie.
        }

        // POST: Movies/Edit/5
        // Handles the submission of the Edit form.
        // Protects against overposting by only binding specific properties.
        // For more details, see: http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] // Validates the Anti-Forgery Token for security.
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,AgeRating,RealeaseDate")] Movies movies)
        {
            if (id != movies.Id) // Checks if the ID in the URL does not match the ID in the model.
            {
                return NotFound(); // Returns a "Not Found" result if there's a mismatch.
            }

            if (ModelState.IsValid) // Checks if the data provided by the user is valid.
            {
                try
                {
                    _context.Update(movies); // Updates the movie in the database context.
                    await _context.SaveChangesAsync(); // Saves changes to the database asynchronously.
                }
                catch (DbUpdateConcurrencyException) // Handles concurrency issues.
                {
                    if (!MoviesExists(movies.Id)) // Checks if the movie still exists.
                    {
                        return NotFound(); // Returns a "Not Found" result if the movie no longer exists.
                    }
                    else
                    {
                        throw; // Rethrows the exception if another issue occurred.
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirects to the Index action after successful update.
            }
            return View(movies); // Returns the View with the submitted data if validation fails.
        }

        // GET: Movies/Delete/5
        // Displays a confirmation page for deleting a movie.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) // Checks if the provided ID is null.
            {
                return NotFound(); // Returns a "Not Found" result if the ID is invalid.
            }

            var movies = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id); // Finds the movie by its ID.
            if (movies == null) // Checks if the movie does not exist.
            {
                return NotFound(); // Returns a "Not Found" result if the movie is not found.
            }

            return View(movies); // Returns the View for confirming deletion.
        }

        // POST: Movies/Delete/5
        // Handles the confirmation of the Delete action.
        [HttpPost, ActionName("Delete")] // Maps this method to the Delete action name.
        [ValidateAntiForgeryToken] // Validates the Anti-Forgery Token for security.
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movies = await _context.Movies.FindAsync(id); // Finds the movie by its ID.
            if (movies != null) // Checks if the movie exists.
            {
                _context.Movies.Remove(movies); // Removes the movie from the database context.
            }

            await _context.SaveChangesAsync(); // Saves changes to the database asynchronously.
            return RedirectToAction(nameof(Index)); // Redirects to the Index action after successful deletion.
        }

        // Helper method to check if a movie exists in the database by its ID.
        private bool MoviesExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id); // Returns true if a movie with the specified ID exists.
        }

        public IActionResult Movies()
        {
            return View();
        }

        public IActionResult MovieReviews()
        {
            return View();
        }

        public IActionResult MovieRatings()
        {
            return View();
        }

        public async Task<IActionResult> SortByAgeRating(bool ascending)
        {
            if (ascending == true)
            {
                var 
            }


        }
    }
}


