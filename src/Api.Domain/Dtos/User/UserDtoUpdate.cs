using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class UserDtoUpdate
    {
        [Required(ErrorMessage = "Id é um campo obrigatório")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        [MaxLength(60, ErrorMessage = "Nome deve ter no máximo {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é um campo obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")] // Valida o formato do email
        [MaxLength(100, ErrorMessage = "Email deve ter no máximo {1} caracteres")]
        public string Email { get; set; }
    }
}
