namespace BookApp.Core.Models
{
    public class AuthorBook
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public override string ToString()
        {
            return $"Author: {Author.Name}, Book: {Book.Title}";
        }
    }
}
