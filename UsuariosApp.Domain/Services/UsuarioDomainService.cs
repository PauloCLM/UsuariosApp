using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces;

namespace UsuariosApp.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        private readonly IUsuarioRepository? _usuarioRepository;

        public UsuarioDomainService(IUsuarioRepository? usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task Criar(Usuario usuario)
        {
            if (await _usuarioRepository.Find(usuario.Email) != null)
                throw new ApplicationException("O email informado já está cadastrado.");

            await _usuarioRepository.Add(usuario);
        }

        public async Task<Usuario> Autenticar(string email, string senha)
        {
            var usuario = await _usuarioRepository.Find(email, senha);
            if(usuario == null)
                throw new ApplicationException("Usuário não encontrado.");

            return usuario;
        }
    }
}
