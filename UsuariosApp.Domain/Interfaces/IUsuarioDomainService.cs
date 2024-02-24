using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Domain.Interfaces
{
    /// <summary>
    /// Contrato de serviços de dominio para Usuario
    /// </summary>
    public interface IUsuarioDomainService
    {
        Task Criar(Usuario usuario);
        Task<Usuario> Autenticar(string email, string senha);
    }
}
