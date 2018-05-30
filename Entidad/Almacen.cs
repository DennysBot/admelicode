using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Almacen
    {
        public int idAlmacen { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public bool principal { get; set; }
        public int estado { get; set; }
        public int idSucursal { get; set; }
        public int idUbicacionGeografica { get; set; }
        public string nombreSucursal { get; set; }
        public string tieneRegistros { get; set; }

        public int idPersonalAlmacen { get; set; }
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
        public string descripcion;

          public string Descripcion
            { get { return nombre + " - " + nombreSucursal; }

                set{ descripcion = value; }
            }
 

    }
    //para hacer cambios

    public class AlmacenCompra
    {

        public int idAlmacen { get; set; }
        public string nombre { get; set; }
        public int idPersonalAlmacen { get; set; }
        public bool principal { get; set; }
        public int cantidad { get; set; } // solo para ventas con varios almacenes asignados

    }

    public class AlmacenNEntrada
    {
        public int idAlmacen { get; set; }
        public int idTipoDocumento { get; set; }
        public string fechaEntrada { get; set; }
        public string observacion { get; set; }
        public int estadoEntrega { get; set; }
        public int idPersonal { get; set; }

        public int idNotaEntrada { get; set; }
      
    }

   

}
