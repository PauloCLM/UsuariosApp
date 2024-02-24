using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces;
using UsuariosApp.Services.Models;
using UsuariosApp.Services.Security;

namespace UsuariosApp.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioDomainService? _usuarioDomainService;

        public UsuariosController(IUsuarioDomainService? usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        [HttpPost("Criar")]
        [ProducesResponseType(typeof(CriarUsuarioResponseModel), 201)]
        public async Task<IActionResult> Criar([FromBody] CriarUsuarioRequestModel model)
        {
            try
            {
                var usuario = new Usuario
                {
                    Id = Guid.NewGuid(),
                    Nome = model.Nome,
                    Email = model.Email,
                    Senha = model.Senha
                };

                await _usuarioDomainService.Criar(usuario);

                var result = new CriarUsuarioResponseModel
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    DataHoraCadastro = DateTime.Now
                };

                return StatusCode(201, result);
            }
            catch(ApplicationException e)
            {
                return StatusCode(400, new { e.Message });
            }
        }

        [HttpPost("Autenticar")]
        [ProducesResponseType(typeof(AutenticarUsuarioResponseModel), 200)]
        public async Task<IActionResult> Autenticar([FromBody] AutenticarUsuarioRequestModel model) 
        {
            try
            {
                var usuario = await _usuarioDomainService.Autenticar(model.Email, model.Senha);
                var result = new AutenticarUsuarioResponseModel
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    DataHoraAcesso = DateTime.Now,
                    AccessToken = JwtBearerSecurity.GenerateToken(usuario.Email),
                    DataHoraExpiracao = JwtBearerSecurity.CreateExpiration()
                };

                return StatusCode(200, result);
            }
            catch(Exception e)
            {
                return StatusCode(401, new { e.Message });
            }
        }
    }
}
