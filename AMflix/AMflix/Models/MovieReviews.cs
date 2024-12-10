namespace AMflix.Models
{
    public class MovieReviews
    {

        public int Id { get; set; }

        public int MoviesId { get; set; }

        public string Name { get; set; }

        public string Review { get; set; }

        public DateTime PublishDate { get; set; }

        public Movies Movies { get; set; }

    }
}
