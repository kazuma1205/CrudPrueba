using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudPrueba.Shared
{
    public class Movimientos
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public double Cantidad { get; set; }

        public string? Tipo { get; set; }
        public string? Descripcion { get; set; }

        public Usuarios? Usuario { get; set; }

        public int UsuarioId { get; set; }


    }
}
