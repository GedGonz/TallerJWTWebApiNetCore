using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TallerJWTWebApiNetCore.Models;

namespace TallerJWTWebApiNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Autentica([FromBody]Usuario usuario) 
        {

            if (!(usuario.Codigo=="ged" && usuario.Clave=="123456789")) 
            {
                return BadRequest("Los datos ingresados no son validos");
            }
            //generar una semilla
            string ClaveSecreta = "123456ABCDEabdebg";
            //Se convierte en bytes
            byte[] claveEnByte = Encoding.UTF8.GetBytes(ClaveSecreta);

            //nuget: System.IdentityModel.Tokens.Jwt
            var key = new SymmetricSecurityKey(claveEnByte);

            //Crea la credencial con el algoritmo HmacSha256
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var miClaims = new[] {
                new Claim("user",usuario.Codigo),
                new Claim("rol","Admin")
            };

            //generador de token.
            //para quien se genera el toke, expiracion, se agregan los claims

            var jwtSecurityToken = new JwtSecurityToken(
                audience: "GedGonz",
                issuer: "GedGonz",
                claims: miClaims,
                expires:DateTime.Now.AddMinutes(10),
                signingCredentials: cred
                );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(new { Token= token });
        }
    }
}
