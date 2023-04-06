using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
    public class Reader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public ICollection<BookIssue> BookIssues { get; set; } = new List<BookIssue>();
    }
}
