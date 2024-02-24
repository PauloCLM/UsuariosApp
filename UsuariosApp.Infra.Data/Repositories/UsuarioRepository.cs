using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces;
using UsuariosApp.Infra.Data.Contexts;

namespace UsuariosApp.Infra.Data.Repositories
{
    /// <summary>
    /// Implementação do repositório de dados de usuário
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        public async Task Add(Usuario usuario)
        {
            using (var dataContext = new DataContext())
            {
                await dataContext.Usuario.AddAsync(usuario);
                await dataContext.SaveChangesAsync();
            }
        }

        public async Task<Usuario> Find(string email)
        {
            using (var dataContext = new DataContext())
            {
                return await dataContext.Usuario
                    .FirstOrDefaultAsync(u => u.Email.Equals(email));
            }
        }

        public async Task<Usuario> Find(string email, string senha)
        {
            using (var dataContext = new DataContext())
            {
                return await dataContext.Usuario
                    .FirstOrDefaultAsync(u => u.Email.Equals(email)
                                           && u.Senha.Equals(senha));
            }
        }
    }
}
