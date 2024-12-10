namespace AMflix.Models
{
    public class MovieRating
    {

        public int Id { get; set; } // Primary key

        public int MoviesId { get; set; } // Foreign key

        public float Rating { get; set; } // Rating left for the movie

        public Movies Movies { get; set; } // Navigation Property

    }
}
