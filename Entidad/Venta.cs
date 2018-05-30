using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Venta
    {
        public Fecha fecha { get; set; }
        public int idVenta { get; set; }
        public string serie { get; set; }
        public string correlativo { get; set; }
        public string nombreCliente { get; set; }
        public string rucDni { get; set; }
        public string direccion { get; set; }
        public int idGrupoCliente { get; set; }
        public string formaPago { get; set; }
        public Fecha fechaPago { get; set; }
        public Fecha fechaVenta { get; set; }
        public string observacion { get; set; }
        public string subTotal { get; set; }
        public string total { get; set; }
        public string descuento { get; set; }
        public string tipoVenta { get; set; }
        public string moneda { get; set; }
        public string tipoCambio { get; set; }
        public int estado { get; set; }
        public int idCliente { get; set; }
        public int idCobro { get; set; }
        public int idAsignarPuntoVenta { get; set; }
        public int idTipoDocumento { get; set; }
        public string nombreLabel { get; set; }
        public string notaSalida { get; set; }
        public string vendedor { get; set; }
        public string guiaRemision { get; set; }
        public string documentoIdentificacion { get; set; }

        public string numeroDocumento { get; set; }

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

    public class Venta_correlativo
    {
        public string serie { get; set; }
        public string correlativoActual { get; set; }

    }



    // objetos para guardAR UNA VENTA
    public class Cobrov
    {
        public int montoPagar { get; set; }
        public int cantidadCuotas { get; set; }
        public int interes { get; set; }
        public int estadoCobro { get; set; }
        public int estado { get; set; }
        public int idCobro { get; set; }
        public int idMoneda { get; set; }
    }

    public class Ventav
    {
        public int idVenta { get; set; }
        public string correlativo { get; set; }
        public string serie { get; set; }
        public string nombreCliente { get; set; }
        public int idDocumentoIdentificacion { get; set; }
        public string documentoIdentificacion { get; set; }
        public string rucDni { get; set; }
        public string direccion { get; set; }
        public string fechaVenta { get; set; }
        public string fechaPago { get; set; }
        public string moneda { get; set; }
        public int tipoCambio { get; set; }
        public string descuento { get; set; }
        public string subTotal { get; set; }
        public string total { get; set; }
        public string tipoVenta { get; set; }
        public string formaPago { get; set; }
        public string observacion { get; set; }
        public int estado { get; set; }
        public int idCliente { get; set; }
        public int idAsignarPuntoVenta { get; set; }
        public int idPuntoVenta { get; set; }
        public bool editar { get; set; }
        public int idTipoDocumento { get; set; }
    }

    

    public class CobroVentaV
    {
        public int idCaja { get; set; }
        public int idCajaSesion { get; set; }
        public int idMedioPago { get; set; }
        public int idMoneda { get; set; }
        public string moneda { get; set; }
        public int pagarVenta { get; set; }
    }

    public class DatosNotaSalidaVenta
    {
        public int idProducto { get; set; }
        public int idCombinacionAlternativa { get; set; }
        public int cantidad { get; set; }
        public int idAlmacen { get; set; }
        public string descripcion { get; set; }      
        public int idPresentacion { get; set; }
        public string nro { get; set; }
        public string nombreProducto { get; set; }     
        public string nombreCombinacion { get; set; }
        public string almacen { get; set; }
        public decimal stock { get; set; }
        public decimal stockOriginal;
        public decimal stockTotal { get; set; }
        public decimal stockGuardar { get; set; }     
        public string cantidadUnitaria { get; set; }
        public string nombrePresentacion { get; set; }
        public string ventaVarianteSinStock { get; set; }
        public decimal cantidadVenta { get; set; }
        public decimal cantidadVentaRestante { get; set; }

        public decimal StockOriginal { get {

                return stock; 

            } set {
                stockOriginal = value;
            } }

    }

    public class NotasalidaVenta
    {
        public int idPersonal { get; set; }
        public int idTipoDocumento { get; set; }
        public List<DatosNotaSalidaVenta> datosNotaSalida { get; set; }
        public int generarNotaSalida { get; set; }
    }

    public class VentaTotal
    {
        public Cobrov cobro { get; set; }
        public Ventav venta { get; set; }
        public List<DetalleV> detalle { get; set; }
        public CobroVentaV cobroventa { get; set; }
        public NotasalidaVenta notasalida { get; set; }
    }

    public class VentaAnular
    {      
        public int idCajaSesion { get; set; }
        public int idVenta { get; set; }
    }

    public class DatosVentaAbastece 
    {
        public int idVenta { get; set; }
        public int idAlmacen { get; set; }
        public int idPersonal { get; set; }
        public int idSucursal { get; set; }
        public List<List<object>> dato { get; set; }
    }


}
