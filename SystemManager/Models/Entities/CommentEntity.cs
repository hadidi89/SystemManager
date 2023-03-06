using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemManager.Models.Entities
{
    internal class CommentEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Text { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        [ForeignKey(nameof(Case))]
        public int CaseId { get; set; }

        [Required]
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public CaseEntity Case { get; set; } = null!;
        public EmployeeEntity Employee { get; set; } = null!;
    }

}
