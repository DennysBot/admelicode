using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class CobroV
    {
        public double montoPagar { get; set; }
        public int cantidadCuotas { get; set; }
        public double interes { get; set; }
        public int estadoCobro { get; set; }
        public int estado { get; set; }
        public int idCobro { get; set; }
        public int idMoneda { get; set; }
    }

    public class VentaV
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
        public double descuento { get; set; }
        public double subTotal { get; set; }
        public double total { get; set; }
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

    public class DetalleV
    {
        public int idDetalleVenta { get; set; }
        public int nro { get; set; }
        public string descripcion { get; set; }
        public string nombreMarca { get; set; }
        public string cantidad { get; set; }
        public string precioEnvio { get; set; }
        public string cantidadUnitaria { get; set; }
        public string precioUnitario { get; set; }
        public string descuento { get; set; }
        public string total { get; set; }
        public int estado { get; set; }
        public int idCombinacionAlternativa { get; set; }
        public int idPresentacion { get; set; }
        public int idProducto { get; set; }
        public int idVenta { get; set; }
        public string codigoProducto { get; set; }
        public string nombreCombinacion { get; set; }
        public string nombrePresentacion { get; set; }
        public int idSucursal { get; set; }
        public bool ventaVarianteSinStock { get; set; }
        public string precioVenta { get; set; }
        public string precioVentaReal { get; set; }
        public string totalGeneral { get; set; }
        public string eliminar { get; set; }
        public int existeStock { get; set; }     
        public string valor { get; set; }
        public int estadoCobro { get; set; }
        public string simboloPresentacion { get; set; }


        // solo para cotizaciones
        public int idDetalleCotizacion { get; set; }  
        public int idCotizacion { get; set; }

        // variables auxiales para guardar

        private string porcentual;
        public string Porcentual
        {
            get
            {

                  if (porcentual == "")
                {
                    return "0";
                }
                return porcentual;
                

            }
            set
            {

                porcentual = value;
            }
        }
        private string efectivo;
        public string Efectivo
        {
            get
            {
                if (efectivo == "")
                {
                    return "0";
                }
                return efectivo;

            }
            set
            {

               efectivo = value;
            }
        }

        public decimal stockALmacenLocal{ get; set; } // vara ver stock del almacen local


}

public class CobroVenta
{
    public int idCaja { get; set; }
    public int idCajaSesion { get; set; }
    public int idMedioPago { get; set; }
    public int idMoneda { get; set; }
    public string moneda { get; set; }
    public int pagarVenta { get; set; }
}

public class DatosNotaSalidaV
{
    public int idProducto { get; set; }
    public int idCombinacionAlternativa { get; set; }
    public double cantidad { get; set; }
    public int idAlmacen { get; set; }
    public string descripcion { get; set; }
}

public class NotasalidaV
{
    public int idPersonal { get; set; }
    public int idTipoDocumento { get; set; }
    public List<DatosNotaSalidaV> datosNotaSalida { get; set; }
    public int generarNotaSalida { get; set; }
}

    public class TodaVenta
    {
        public Cobro cobro { get; set; }
        public Venta venta { get; set; }
        public List<DetalleV> detalle { get; set; }
        public CobroVenta cobroventa { get; set; }
        public NotasalidaV notasalida { get; set; }
    }
}
