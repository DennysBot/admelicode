﻿using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Descuento
    {
        public int idDescuentoProductoGrupo { get; set; }
        public string codigo { get; set; }
        public string descuento { get; set; }
        public string  fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public string tipoDescuento { get; set; }
        public string tipo { get; set; }
        public string cantidadMinima { get; set; }
        public string cantidadMaxima { get; set; }
        public int estado { get; set; }
        public int idGrupoCliente { get; set; }
        public string nombreGrupo { get; set; }
        public int idSucursal { get; set; }
        public string nombre { get; set; }
        public int idAfectoProducto { get; set; }

        public int idProducto { get; set; }
        public int idPresentacion { get; set; }
        public string nombreSucursal { get; set; }
        public object nombreProducto { get; set; }



    }



    #region========http://localhost:8080/admeli/xcore/services.php/productos/descuentototalalafechagrup =========
    public class DescuentoSubmit /// descuentototalalafecha
    {
        public string cantidades { get; set; }
        public int idGrupoCliente { get; set; }
        public int idSucursal { get; set; }
        public string idProductos { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }

    }
    public class DescuentoReceive
    {
        public int idProducto { get; set; }
        public string cantidad { get; set; }
        public double descuento { get; set; }
    }
    #endregion========http://localhost:8080/admeli/xcore/services.php/productos/descuentototalalafechagrupo   =========

    #region===========http://localhost:8080/admeli/xcore/services.php/productos/descuentototalalafecha ================


    public class DescuentoProductoSubmit
    {
        public int cantidad { get; set; }
        public string cantidades { get; set; }
        public int idGrupoCliente { get; set; }
        public int idSucursal { get; set; }
        public int idProducto { get; set; }
        public string idProductos { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
    }

    public class DescuentoProductoReceive
    {
        public double descuento { get; set; }
    }

    #endregion=========================================================================================================


}
