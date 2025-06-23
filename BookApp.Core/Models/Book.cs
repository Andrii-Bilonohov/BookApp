namespace BookApp.Core.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string Language { get; set; }
        public int FirstPublishYear { get; set; }
        public ICollection<AuthorBook> BookAuthors { get; set; } = new List<AuthorBook>();


        public override string ToString()
        {
            var authors = BookAuthors != null && BookAuthors.Any()
                ? string.Join(", ", BookAuthors.Select(ab => ab.Author?.Name ?? "Unknown Author"))
                : "Unknown";

            return $"Title: {Title}\n" +
                   $"Language: {Language}\n" +
                   $"First Publish Year: {FirstPublishYear}\n" +
                   $"Authors: {authors}\n";
        }

    }
}
