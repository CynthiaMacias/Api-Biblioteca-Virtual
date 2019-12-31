using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api_BV.Models
{
    public class UsuariosRol
    {

        public int Id { get; set; }
        [ForeignKey("Usuarios")]
        public int IdUser {get; set; }
        public int RoleId { get; set; }
    }
}
