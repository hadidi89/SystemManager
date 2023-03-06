using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SystemManager.Models.Entities;

namespace SystemManager.Models.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    internal class EmployeeEntity
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; } = null!;
        [StringLength(50)]
        public string LastName { get; set; } = null!;
        [StringLength(100)]
        public string Email { get; set; } = null!;
        [Column(TypeName = "char(13)")]
        public string PhoneNumber { get; set; } = null!;
        public ICollection<CommentEntity> Cases { get; set; } = new HashSet<CommentEntity>();
    }

}

