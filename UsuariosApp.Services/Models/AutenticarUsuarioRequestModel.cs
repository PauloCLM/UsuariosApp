using System.ComponentModel.DataAnnotations;

namespace UsuariosApp.Services.Models
{
    /// <summary>
    /// Modelo de dados da requisição do serviço de autenticação de usuário
    /// </summary>
    public class AutenticarUsuarioRequestModel
    {
        [Required(ErrorMessage = "Informe o email do usuário.")]
        [EmailAddress(ErrorMessage = "Informe um endereço de email válido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$",
            ErrorMessage = "Por favor, informe a senha com letras maiúsculas, minúsculas, números, símbolos e pelo menos 8 caracteres.")]
        public string? Senha { get; set; }
    }
}
