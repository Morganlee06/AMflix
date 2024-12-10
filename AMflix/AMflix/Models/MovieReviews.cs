namespace AMflix.Models
{
    public class MovieReviews
    {

        public int Id { get; set; } // Primary key

        public int MoviesId { get; set; } // Forign key

        public string Name { get; set; } // Name of the aurthour of the review

        public string Review { get; set; } // The review they have left

        public DateTime PublishDate { get; set; } // Date the review was published

        public Movies Movies { get; set; } // Navigation property

    }
}
