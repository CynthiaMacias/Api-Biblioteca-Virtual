using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_BV.Models
{
    public class Subcategoria
    {
        
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
       /// [ForeignKey("Categoria")]
        public int Id_categoria { get; set; }
        public Boolean Estatus { get; set; }
    }
}
