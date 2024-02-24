using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Domain.Interfaces
{
    /// <summary>
    /// Contrato de repositório de dados
    /// </summary>
    public interface IUsuarioRepository
    {
        Task Add(Usuario usuario);
        Task<Usuario> Find(string email);
        Task<Usuario> Find(string email, string senha);
    }
}
