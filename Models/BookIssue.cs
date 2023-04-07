namespace LibraryAPI.Models
{
    public class BookIssue
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ReaderId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Book Book { get; set; }
        public Reader Reader { get; set; }
    }
}
