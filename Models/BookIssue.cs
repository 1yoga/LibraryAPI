namespace LibraryAPI.Models
{
    public class BookIssue
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ReaderId { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public DateTimeOffset? ReturnDate { get; set; }

        public Book Book { get; set; }
        public Reader Reader { get; set; }
    }
}
