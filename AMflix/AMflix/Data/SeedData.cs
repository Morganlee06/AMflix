using AMflix.Models;
using System.Globalization;

namespace AMflix.Data
{
    public class SeedData
    {
        public static void SeedMovies(ApplicationDbContext context) 
        {
            if (!context.Movies.Any()) // Checks if there are any movies already in the database
            {
                var Movies = new List<Movies> // Creates a list containing the movies we want to add
                {
                    // Creates a new movie
                    new Movies
                    {
                        // Assigns values to the movies information
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
                        AgeRating = "1",
                        RealeaseDate = DateOnly.Parse("21/12/2009")
                    },

                     new Movies
                    {
                      Title = "ACE And The East Green Treasure",
                      Description = "ACE is a Week Litte Boy, " +
                      "deeply connected to the ancient spiritual powers" +
                      " twisted hair, and glistening green eyes," +
                      " he commands respect and has a powerful army to protect him with his incredible intellect" +
                      " he belives he can take on the world .",
                        AgeRating = "12",
                        RealeaseDate = DateOnly.Parse("11/6/2007")
                     },

                     new Movies
                    {
                      Title = "Faruk and the Neverland Pirates",
                      Description = "faruk is a powerful Pirate , " +
                      "deeply connected to his Nigeran traditions." +
                      "With his Dry face, Nappy hair, and Blue eyes," +
                      "he commands respect as a healer, protector," +
                      " and guardian of sacred knowledge.",
                        AgeRating = "18",
                        RealeaseDate = DateOnly.Parse("7/7/2017")
                     }
                };

                context.Movies.AddRange(Movies); // Pass the list through
                context.SaveChanges(); // Save the changes

            }

        }

    }
}

