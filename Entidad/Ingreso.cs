using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Ingreso
    {
        public int idIngreso { get; set; }
        public string numeroOperacion { get; set; }
        public Fecha fecha { get; set; }
        public Fecha fechaPago { get; set; }
        public Decimal monto { get; set; }
        public string motivo { get; set; }
        public string observacion { get; set; }
        public int estado { get; set; }
        public string moneda { get; set; }
        public int idMoneda { get; set; }
        public int idCajaSesion { get; set; }
        public int idDetalleCobro { get; set; }
        public int idMedioPago { get; set; }
        public string medioPago { get; set; }
        public string esDeVenta { get; set; }
        public string personal { get; set; }
        public int idAsignarCaja { get; set; }
        public string montoCierre { get; set; }
        public object fechaCierre { get; set; }

        public string[] valores { get; set; }

        private string estadoString;
        public string EstadoString
        {
            get
            {
                if (estado == 1) { return "Activo"; }
                else { return "Anulado"; }
            }
            set
            {
                estadoString = value;
            }
        }
    }
    public class Adelanto
    {
        // public int idIngreso { get; set; }
        public int idCotizacion { get; set; }

        public int idIngreso { get; set; }

    }

}
