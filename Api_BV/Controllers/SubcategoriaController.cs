using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_BV.Models;
using Api_BV.Data;
using Microsoft.EntityFrameworkCore;
namespace Api_BV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriaController : Controller
        
    {
        private readonly ApplicationDbContext context;

        public SubcategoriaController(ApplicationDbContext contexto)
        {
            context = contexto;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subcategoria>>> GetSub()
        {
            return await context.Subcategoria.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subcategoria>> GetCatitem (int id)
        {
            var item = (from A in context.Subcategoria where A.Id_categoria.Equals(id)
                select new
                {
                    get = A

                });

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        
    }
        [HttpGet("cat/{id}")]
        public async Task<ActionResult<Subcategoria>> GetSubId(int id)
        {
            var item = await context.Subcategoria.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult AddCategoria([FromBody] Subcategoria subcategoria)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();

            }
            this.context.Subcategoria.Add(subcategoria);
            this.context.SaveChanges();
            return Ok(subcategoria);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSub ([FromBody] Subcategoria subcategoria, int id)
        {
            if(subcategoria.Id != id)
            {
                return BadRequest();
            }
            context.Entry(subcategoria).State = EntityState.Modified;
            context.SaveChanges();
            await context.SaveChangesAsync();
            return Ok(subcategoria);
        }
    }
}