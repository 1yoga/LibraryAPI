using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public ICollection<BookIssue> BookIssues { get; set; } = new List<BookIssue>();
    }
}
