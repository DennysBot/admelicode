using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Oferta
    {
        public int idOfertaProductoGrupo { get; set; }
        public string codigo { get; set; }
        public string descuento { get; set; }
        public string tipo { get; set; }
        public int estado { get; set; }
        public int idAfectoProducto { get; set; }
        public Fecha fechaInicio { get; set; }
        public Fecha fechaFin { get; set; }
        public int idProducto { get; set; }
        public int idGrupoCliente { get; set; }
        public int idSucursal { get; set; }
        public string nombreSucursal { get; set; }
        public string nombreGrupo { get; set; }
        public object nombreProducto { get; set; }
    }
    public class OfertaG
    {
        public int idOfertaProductoGrupo { get; set; }
        public string codigo { get; set; }
        public string descuento { get; set; }
        public string tipo { get; set; }
        public int estado { get; set; }
        public int idAfectoProducto { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public int idPresentacion { get; set; }
        public int idGrupoCliente { get; set; }
        public int idSucursal { get; set; }
        public string nombreSucursal { get; set; }
        public string nombreGrupo { get; set; }
        public object nombreProducto { get; set; }
    }


}
