using System.Text.Json.Serialization;

namespace BookApp.Core.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string? AlternateNames { get; set; }
        public string? BirthDate { get; set; }
        public string? DeathDate { get; set; }
        public required string Name { get; set; }
        public double? RatingsAverage { get; set; }
        public ICollection<AuthorBook> BookAuthors { get; set; } = new List<AuthorBook>();


        public override string ToString()
        {
            var books = BookAuthors != null && BookAuthors.Any()
                ? string.Join(", ", BookAuthors.Select(ab => ab.ToString()))
                : "No books";

            return $"Author Name: {Name}\n" +
                   $"Alternate Names: {AlternateNames}\n" +
                   $"Birth Date: {BirthDate}\n" +
                   $"Death Date: {DeathDate}\n" +
                   $"Ratings Average: {RatingsAverage}\n" +
                   $"Books: {books}\n";
        }
    }
}
