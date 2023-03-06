using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace SystemManager.Models.Entities
{
    internal class CaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        [Required]
        public string Status { get; set; } = "NotStarted";

        [Required]
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;
        public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

    }
    public enum CaseStatus
    {
        NotStarted,
        InProgress,
        Completed 
    }

}
