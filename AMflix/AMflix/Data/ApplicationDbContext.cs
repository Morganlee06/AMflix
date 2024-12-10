using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AMflix.Models;

namespace AMflix.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AMflix.Models.Movies> Movies { get; set; } = default!;
        public DbSet<AMflix.Models.MovieRating> MovieRating { get; set; } = default!;
        public DbSet<AMflix.Models.MovieReviews> MovieReviews { get; set; } = default!;
    }
}
