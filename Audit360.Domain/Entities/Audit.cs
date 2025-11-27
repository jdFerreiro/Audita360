using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Audit360.Domain.Entities
{
    public class Audit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El título es obligatorio")]
        [MaxLength(200, ErrorMessage = "El título no puede exceder {1} caracteres")]
        public required string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El área es obligatoria")]
        [MaxLength(100, ErrorMessage = "El área no puede exceder {1} caracteres")]
        public required string Area { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public required DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "El estado de la auditoría es obligatorio")]
        public int AuditStatusId { get; set; }
        public required AuditStatus Status { get; set; }

        // Relationships
        [Required(ErrorMessage = "El responsable es obligatorio")]
        public int ResponsibleId { get; set; }
        public Responsible Responsible { get; set; } = null!;
        public ICollection<Finding> Findings { get; set; } = [];
    }
}
