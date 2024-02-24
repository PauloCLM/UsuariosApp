using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UsuariosApp.Services.Security
{
    /// <summary>
    /// Classe para geração da autenticação do usuário (JWT - JSON WEB TOKENS)
    /// </summary>
    public class JwtBearerSecurity
    {
        #region Dados para geração do TOKEN JWT

        /// <summary>
        /// Chave secreta utilizada para criptografar e assinar o TOKEN JWT
        /// </summary>
        public static string SecretKey => "579e9d5-af1d-47f4-a6e2-d1a8db42a6b1";

        /// <summary>
        /// Tempo em horas para expiração do TOKEN
        /// </summary>
        public static int ExpirationInHours => 6;

        #endregion

        /// <summary>
        /// Método para gerar e retornar um TOKEN JWT
        /// </summary>
        public static string GenerateToken(string usuario)
        {
            #region Gerar o token JWT

            var jwtSecurityToken = new JwtSecurityToken(
                claims: CreateClaims(usuario),
                signingCredentials: CreateCredentials(),
                expires: CreateExpiration()
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(jwtSecurityToken);

            #endregion
        }

        public static DateTime CreateExpiration()
        {
            return DateTime.Now.AddHours(Convert.ToDouble(ExpirationInHours));
        }

        private static SigningCredentials CreateCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return credentials;
        }

        private static Claim[] CreateClaims(string usuario)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, usuario)
            };

            return claims;
        }
    }
}
