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
            // Select * form Paises ----- SQL
            return context.Paises.ToList();
        }

        [HttpPost]
        public ActionResult<Pais> Post(Pais pais)
        {
            context.Paises.Add(pais);
            context.SaveChanges();
            return pais;
        }
    }
}
