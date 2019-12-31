using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_BV.Models;
using Api_BV.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Api_BV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext context;
      
        public CategoriaController(ApplicationDbContext contexto)
        {
            context = contexto;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoria()
        {
            return await context.Categoria.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCatId(int id)
        {
            var item = await context.Categoria.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult AddCategoria ([FromBody] Categoria categoria)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();

            }
            this.context.Categoria.Add(categoria);
            this.context.SaveChanges();
            return Ok(categoria);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria ([FromBody] Categoria categoria,int id)
        {
            if (categoria.Id != id)
            {
                return BadRequest();

            }
            context.Entry(categoria).State = EntityState.Modified;
            context.SaveChanges();
            await context.SaveChangesAsync();
            return Ok(categoria);
        }
        [HttpPut("est/{id}")]
        public IActionResult Put([FromBody] Categoria cat, int id)
        {
            using (var db = context)
            {
                if (cat.Id != id)
                {
                    return BadRequest();
                }
                db.Categoria.Attach(cat);
                db.Entry(cat).Property(x => x.Estatus).IsModified = true;
                db.SaveChanges();
                return Ok(cat);
            }
        }

    }
}