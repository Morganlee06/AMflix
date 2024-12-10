namespace AMflix.Models
{
    public class Movies
    {

        public int Id { get; set; } // Primary key

        public string Title { get; set; } // Title of the movie  

        public string Description { get; set; } // Description of the movie

        public string AgeRating { get; set; } // Age rating of the movie 

        public DateOnly RealeaseDate { get; set; } // The realse date of the movie 


        public ICollection<MovieRating>? movieRatings { get; set; } // Links this model to the MovieRating model

        public ICollection<MovieReviews>? MovieReviews { get; set; } // Links this model to the MovieReview model

    }
}
