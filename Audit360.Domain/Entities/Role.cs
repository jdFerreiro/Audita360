using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Audit360.Domain.Entities
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre del rol es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre del rol no puede exceder {1} caracteres")]
        public required string Name { get; set; } = string.Empty;

        [MaxLength(300, ErrorMessage = "La descripción no puede exceder {1} caracteres")]
        public string? Description { get; set; }

        // Relationships
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
