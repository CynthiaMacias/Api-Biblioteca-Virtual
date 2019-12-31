using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Api_BV.Models
{
    public class FileArchivo
    {
        public int Id { get; set; }
        public IFormFile ArchivoFile { get; set; }
    }
}
