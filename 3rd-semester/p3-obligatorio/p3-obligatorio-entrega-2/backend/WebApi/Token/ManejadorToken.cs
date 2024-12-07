using Compartido.DTOs.Usuarios;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Token
{
    public class ManejadorToken
    {
        internal static string CrearToken(DTOUsuarioLogeado dtoUsuario)
        {

            byte[] clave = Encoding.ASCII.GetBytes("ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=");

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            //clave secreta, generalmente se incluye en el archivo de configuración
            //Debe ser un vector de bytes         

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, dtoUsuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, dtoUsuario.Email),
                    new Claim(ClaimTypes.Role, dtoUsuario.Rol.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(clave),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

}