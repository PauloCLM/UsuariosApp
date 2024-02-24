namespace UsuariosApp.Services.Models
{
    /// <summary>
    /// Modelo de dados da resposta do serviço de cadastro de usuário
    /// </summary>
    public class CriarUsuarioResponseModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime? DataHoraCadastro { get; set; }
    }
}
