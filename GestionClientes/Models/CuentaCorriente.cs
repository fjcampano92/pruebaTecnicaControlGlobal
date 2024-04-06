using System;
using System.Collections.Generic;

namespace GestionClientes.Models
{
    public partial class CuentaCorriente
    {
        public int MovimientoId { get; set; }
        public int? ClienteId { get; set; }
        public DateTime FhMovimiento { get; set; }
        public decimal Importe { get; set; }
        public string? Descripcion { get; set; }

        public virtual Cliente? Cliente { get; set; }
    }
}
