using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Audit360.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre de usuario es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre de usuario no puede exceder {1} caracteres")]
        public required string Username { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
        [MaxLength(200, ErrorMessage = "El correo electrónico no puede exceder {1} caracteres")]
        public required string Email { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "La contraseña es obligatoria")]
        public required string PasswordHash { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre completo es obligatorio")]
        [MaxLength(200, ErrorMessage = "El nombre completo no puede exceder {1} caracteres")]
        public required string FullName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "El id del rol es obligatorio")]
        public required int RoleId { get; set; }

        // Relationships
        public required Role Role { get; set; }
    }
}
