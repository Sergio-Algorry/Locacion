using Locacion.Comunes.Data;
using Locacion.Comunes.Data.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locacion.Server.Controllers
{
    [ApiController]
    [Route("api/provincias")]
    public class ProvinciasController : ControllerBase
    {
        private readonly dbContext context;

        public ProvinciasController(dbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<List<Provincia>> Get()
        {
            return context.Provincias.Include(x => x.Pais).ToList();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Provincia> Get(int id)
        {
            var provincia = context.Provincias.Where(x => x.Id == id).Include(x => x.Pais).FirstOrDefault();
            if (provincia == null)
            {
                return NotFound($"No existe la provincia con id igual a {id}.");
            }
            return Ok(provincia);
        }

        [HttpPost]
        public ActionResult<Provincia> Post(Provincia provincia)
        {
            try
            {
                context.Provincias.Add(provincia);
                context.SaveChanges();
                return provincia;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Provincia provincia)
        {
            if (id != provincia.Id)
            {
                return BadRequest("Datos incorrectos");
            }

            var pepe = context.Provincias.Where(x => x.Id == id).FirstOrDefault();
            if (pepe == null)
            {
                return NotFound("no existe la provincia a modificar.");
            }
            pepe.CodProvincia = provincia.CodProvincia;
            pepe.NombreProvincia = provincia.NombreProvincia;
            pepe.PaisId = provincia.PaisId;
            try
            {
                context.Provincias.Update(pepe);
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
            var provincia = context.Provincias.Where(x => x.Id == id).FirstOrDefault();
            if (provincia == null)
            {
                return NotFound($"No existe la provincia con id igual a {id}.");
            }

            try
            {
                context.Provincias.Remove(provincia);
                context.SaveChanges();
                return Ok($"La provincia {provincia.NombreProvincia} ha sido borrado.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
