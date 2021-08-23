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
    [Route("api/paises")]
    public class PaisesController : ControllerBase
    {
        private readonly dbContext context;

        public PaisesController(dbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pais>>> Get()
        {
            return await context.Paises.Include(x => x.Provincias).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pais>> Get(int id)
        {
            var pais = await context.Paises.Where(x => x.Id == id).Include(x => x.Provincias).FirstOrDefaultAsync();
            if (pais==null)
            {
                return NotFound($"No existe el pais con id igual a {id}.");
            }
            return pais;
        }

        [HttpPost]
        public async Task<ActionResult<Pais>> Post(Pais pais)
        {
            try
            {
                context.Paises.Add(pais);
                await context.SaveChangesAsync();
                return pais;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Pais pais)
        {
            if(id != pais.Id)
            {
                return BadRequest("Datos incorrectos");
            }

            var pepe = await context.Paises.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (pepe == null)
            {
                return NotFound("no existe el pais a modificar.");
            }
            pepe.CodPais = pais.CodPais;
            pepe.NombrePais = pais.NombrePais;
            try
            {
                context.Paises.Update(pepe);
                await context.SaveChangesAsync();
                return Ok("Los datos han sido cambiados");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var pais = await context.Paises.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (pais == null)
            {
                return NotFound($"No existe el pais con id igual a {id}.");
            }

            try
            {
                context.Paises.Remove(pais);
                await context.SaveChangesAsync();
                return Ok($"El país {pais.NombrePais} ha sido borrado.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
