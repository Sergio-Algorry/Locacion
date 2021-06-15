using Locacion.Comunes.Data;
using Locacion.Comunes.Data.Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locacion.Server.Controllers
{
    [ApiController]
    [Route("api/paises")]
    public class PaisesController : ControllerBase
    {
        private readonly dbContext context;

        public PaisesController(dbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<List<Pais>> Get()
        {
            // Select * from Paises ----- SQL
            return context.Paises.ToList();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Pais> Get(int id)
        {
            // Select * from Paises Where Id = id ----- SQL
            var pais = context.Paises.Where(x => x.Id == id).FirstOrDefault();
            if (pais==null)
            {
                return NotFound($"No existe el pais con id igual a {id}.");
            }
            return pais;
        }

        [HttpPost]
        public ActionResult<Pais> Post(Pais pais)
        {
            try
            {
                context.Paises.Add(pais);
                context.SaveChanges();
                return pais;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Pais pais)
        {
            if(id != pais.Id)
            {
                return BadRequest("Datos incorrectos");
            }

            var pepe = context.Paises.Where(x => x.Id == id).FirstOrDefault();
            if (pepe == null)
            {
                return NotFound("no existe el pais a modificar.");
            }
            pepe.CodPais = pais.CodPais;
            pepe.NombrePais = pais.NombrePais;
            try
            {
                context.Paises.Update(pepe);
                context.SaveChanges();
                return Ok("Los datos han sido cambiados");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var pais = context.Paises.Where(x => x.Id == id).FirstOrDefault();
            if (pais == null)
            {
                return NotFound($"No existe el pais con id igual a {id}.");
            }

            try
            {
                context.Paises.Remove(pais);
                context.SaveChanges();
                return Ok($"El país {pais.NombrePais} ha sido borrado.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
