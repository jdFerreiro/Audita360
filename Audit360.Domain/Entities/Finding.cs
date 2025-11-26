using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Audit360.Domain.Entities
{
    public class Finding
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción es obligatoria")]
        [MaxLength(3000, ErrorMessage = "La descripción no puede exceder {1} caracteres")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "El tipo de hallazgo es obligatorio")]
        public required FindingType Type { get; set; }

        [Required(ErrorMessage = "La severidad del hallazgo es obligatoria")]
        public required FindingSeverity Severity { get; set; }

        [Required(ErrorMessage = "La fecha del hallazgo es obligatoria")]
        public DateTime Date { get; set; }

        // Relationships
        [Required(ErrorMessage = "La auditoría asociada es obligatoria")]
        public int AuditId { get; set; }
        public Audit Audit { get; set; } = null!;
        public ICollection<FollowUp> FollowUps { get; set; } = new List<FollowUp>();
    }
}
