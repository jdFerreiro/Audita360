using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Audit360.Domain.Entities
{
    public class FollowUp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción es obligatoria")]
        [MaxLength(3000, ErrorMessage = "La descripción no puede exceder {1} caracteres")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "La fecha de compromiso es obligatoria")]
        public required DateTime CommitmentDate { get; set; }

        [Required(ErrorMessage = "El estado del seguimiento es obligatorio")]
        public required FollowUpStatus Status { get; set; }

        // Relationships
        [Required(ErrorMessage = "El hallazgo asociado es obligatorio")]
        public required int FindingId { get; set; }
        public Finding Finding { get; set; } = null!;
    }
}
