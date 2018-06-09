using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class CierreCaja
    {
        public int idCierreCaja { get; set; }
        public Fecha fechaInicio { get; set; }
        public Fecha fechaCierre { get; set; }
        public string observacion { get; set; }
        public int idCajaSesion { get; set; }
        public string nombres { get; set; }
        public int idPersonal { get; set; }
        public int idSucursal { get; set; }
        public string nombre { get; set; }
    }

    public class EstadoCuentas
    {
        public string personal { get; set; }
        public string tatalIngreso { get; set; }
        public string totalEgreso { get; set; }
        public string moneda { get; set; }
        public decimal total { get; set; }
    }

    public class  DineroCompra
    {
        public int idMoneda { get; set; }
        public string moneda { get; set; }
        public double total { get; set; }
    }
}
