using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TallerJWTWebApiNetCore.Models;

namespace TallerJWTWebApiNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Listar() 
        {
            var clientes = new List<Cliente>();

            clientes.Add(new Cliente { Codigo = 1, Nombre = "Juan Perez" });
            clientes.Add(new Cliente { Codigo = 2, Nombre = "JMaria Lopez" });
            clientes.Add(new Cliente { Codigo = 3, Nombre = "Pablo Gomez" });

            return Ok(clientes);

        }
    }
}
