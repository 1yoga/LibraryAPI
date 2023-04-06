using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Article { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;

        public ICollection<BookIssue> BookIssues { get; set; } = new List<BookIssue>();

    }
}
