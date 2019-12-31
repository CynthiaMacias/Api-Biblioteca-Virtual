using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_BV.Data;
using Api_BV.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Api_BV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {

        private readonly ApplicationDbContext context;

        public LibrosController(ApplicationDbContext contexto)
        {
            context = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libros>>> GetL()
        {
            return await context.Libros.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Libros>> GetLibrosItem(int id)
        {
            var item = await context.Libros.FindAsync(id);
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }
        [HttpGet("cat/{id}")]
        public async Task<ActionResult<Libros>> GetCaItem(int id)
        {
   
            var item = context.Libros.Where(x=> x.CategoriaId == id);
      

            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult AddLibros([FromBody] Libros libros)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            this.context.Libros.Add(libros);
            this.context.SaveChanges();
            return Ok(libros.Id);
        }
        [HttpPost("uploadFile")]
        public async Task<IActionResult> AddFile([FromForm] FileArchivo lib)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            var id = lib.Id;
            // Getting Image
            var archivoF = lib.ArchivoFile;
            // Saving Image on Server
            var path = Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\TSM CONNECT\\Desktop\\BibliotecaVirtual\\public\\files", id + "_" + archivoF.FileName);

            var l = context.Libros.Find(id);

            if (archivoF.Length > 0)
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await archivoF.CopyToAsync(fileStream);
                    l.Archivo = id + "_" + archivoF.FileName;
                    await context.SaveChangesAsync();
                }
            }
            return Ok(new { status = true, message = "Cargado correctamente" });
      
        }
        private string ufile(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Libros libros, int id)
        {

            if (libros.Id != id)
            {
                return BadRequest();
            }
            context.Entry(libros).State = EntityState.Modified;
            context.SaveChanges();
            await context.SaveChangesAsync();
            return Ok(libros);
        }

    }
}