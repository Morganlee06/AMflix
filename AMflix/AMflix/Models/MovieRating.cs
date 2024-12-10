namespace AMflix.Models
{
    public class MovieRating
    {

        public int Id { get; set; }

        public int MoviesId { get; set; }

        public float Rating { get; set; }

        public Movies Movies { get; set; }

    }
}
