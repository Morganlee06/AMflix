namespace AMflix.Models
{
    public class Movies
    {

        public int Id { get; set; }

        public string Title { get; set; }   

        public string Description { get; set; }

        public string AgeRating { get; set; }

        public DateTime RealeaseDate { get; set; }

        public ICollection<MovieRating>? movieRatings { get; set; }

        public ICollection<MovieReviews>? MovieReviews { get; set; }

    }
}
