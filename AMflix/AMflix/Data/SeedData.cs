using AMflix.Models;

namespace AMflix.Data
{
    public class SeedData
    {
        public static void SeedMovies(ApplicationDbContext context)
        {
            if(!context.Movies.Any())
            {
                var Movies = new List<Movies>
                {
                    new Movies
                    {
                     Title = "Morgan And The Seven Seas",
                     Description = "Morgan is a powerful Caribbean priest, " +
                     "deeply connected to the ancient spiritual traditions of " +
                     "Vodou, Santería, and other Afro-Caribbean religions."

                    },
                };
            }
        }

    }
}
