using AMflix.Models;
using System.Globalization;

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
                     "Vodou, Santería, and other Afro-Caribbean religions." +
                     "With his weathered face, braided hair, and sharp eyes," +
                     " he commands respect as a healer, protector, and guardian of sacred knowledge." +
                     " Known for his ability to communicate with spirits and perform powerful rituals," +
                     " Morgan helps those in need but also defends the delicate balance of the islands'" +
                     " spiritual forces.\r\n\r\nThough revered, Morgan struggles with the weight of his responsibility" +
                     " and the growing influence of the modern world." +
                     " He walks a fine line between the physical and spiritual realms, haunted by his ancestors'" +
                     " expectations and the forces he commands.",
                     AgeRating = "PG13",
                     RealeaseDate = DateOnly.Parse("21/12/2009"),

                    }
                };
            }
        }

    }
}
