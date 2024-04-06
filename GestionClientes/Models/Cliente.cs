using System;
using System.Collections.Generic;

namespace GestionClientes.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            CuentaCorrientes = new HashSet<CuentaCorriente>();
        }

        public int ClienteId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Identificacion { get; set; } = null!;
        public decimal? Saldo { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<CuentaCorriente> CuentaCorrientes { get; set; }
    }
}
