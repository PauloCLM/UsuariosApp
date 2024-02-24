using System.ComponentModel.DataAnnotations;

namespace UsuariosApp.Services.Models
{
    /// <summary>
    /// Modelo de dados da requisição do serviço de cadastro de usuário
    /// </summary>
    public class CriarUsuarioRequestModel
    {
        [Required(ErrorMessage = "Informe o nome do usuário.")]
        [MinLength(8, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "Informe no máximo {1} caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Informe o email do usuário.")]
        [EmailAddress(ErrorMessage = "Informe um endereço de email válido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$",
            ErrorMessage = "Por favor, informe a senha com letras maiúsculas, minúsculas, números, símbolos e pelo menos 8 caracteres.")]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "Confirme a senha do usuário.")]
        [Compare("Senha", ErrorMessage = "Senhas não conferem.")]
        public string? SenhaConfirmacao { get; set; }
    }
}
