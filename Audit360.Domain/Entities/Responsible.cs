using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Audit360.Domain.Entities
{
    public class Responsible
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(150, ErrorMessage = "El nombre no puede exceder {1} caracteres")]
        public required string Name { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
        [MaxLength(200, ErrorMessage = "El correo electrónico no puede exceder {1} caracteres")]
        public required string Email { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "El área es obligatoria")]
        [MaxLength(100, ErrorMessage = "El área no puede exceder {1} caracteres")]
        public required string Area { get; set; } = string.Empty;

        // Relationships
        public ICollection<Audit> Audits { get; set; } = new List<Audit>();
    }
}
