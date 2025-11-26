using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Audit360.Domain.Entities
{
    public class FollowUpStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripcion es obligatoria")]
        [MaxLength(100, ErrorMessage = "La descripcion no puede exceder {1} caracteres")]
        public required string Description { get; set; }
    }
}
