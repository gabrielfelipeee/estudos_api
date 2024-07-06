using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O email é um campo obrigatório para fazer login")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")] // Valida o formato do email
        [MaxLength(100, ErrorMessage = "O email deve ter no máximo {1} caracteres")]
        public string Email { get; set; }
    }
}
