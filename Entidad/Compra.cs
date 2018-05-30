using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Compra
    {
        public int idCompra { get; set; }
        public string numeroDocumento { get; set; }
        public string nroOrdenCompra { get; set; }
        public string nombreProveedor { get; set; }
        public string rucDni { get; set; }
        public string direccion { get; set; }
        public string formaPago { get; set; }
        public string moneda { get; set; }
        public Fecha fechaFacturacion { get; set; }
        public Fecha fechaPago { get; set; }
        public string descuento { get; set; }
        public string tipoCompra { get; set; }
        public string tipoCambio { get; set; }
        public string subTotal { get; set; }
        public string total { get; set; }
        public string observacion { get; set; }
        public int estado { get; set; }
        public int idProveedor { get; set; }
        public int idPago { get; set; }
        public int idPersonal { get; set; }
        public int idTipoDocumento { get; set; }
        public string nombreLabel { get; set; }
        public int idSucursal { get; set; }
        public string vendedor { get; set; }
        public int idCajaSesion { get; set; }       
        public Fecha fecha { get; set; }

        private string estadoString;
        public string EstadoString
        {
            get
            {
                if(estado == 1) { return "Activo"; }
                else { return "Anulado"; }
            }
            set
            {
                estadoString = value;
            }
        }


    }
    public class PagoC
    {
        public double valorTotal { get; set; }
        public double valorPagado { get; set; }
        public double saldo { get; set; }
        public string motivo { get; set; }
        public int estado { get; set; }
        public int estadoPago { get; set; }
        public int idMoneda { get; set; }
        public int idPago { get; set; }
        private string estadoString;
        public string EstadoString
        {
            get
            {
                if(estado == 1) { return "Activo"; }
                else { return "Anulado"; }
            }
            set
            {
                estadoString = value;
            }
        }
    }

    public class CompraC
    {
        public int idCompra { get; set; }
        public string numeroDocumento { get; set; }
        public string nombreProveedor { get; set; }
        public string rucDni { get; set; }
        public string direccion { get; set; }
        public string formaPago { get; set; }
        public string moneda { get; set; }
        public string fechaFacturacion { get; set; }
        public string fechaPago { get; set; }
        public double descuento { get; set; }
        public string tipoCompra { get; set; }
        public double subTotal { get; set; }
        public double total { get; set; }
        public string observacion { get; set; }
        public int estado { get; set; }
        public int idProveedor { get; set; }
        public int idPago { get; set; }
        public int idPersonal { get; set; }
        public int tipoCambio { get; set; }
        public int idTipoDocumento { get; set; }
        public int idSucursal { get; set; }
        public string nombreLabel { get; set; }
        public string vendedor { get; set; }
        public string nroOrdenCompra { get; set; }
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

    public class DetalleC
    {
        public int idDetalleCompra { get; set; }
        public int nro { get; set; }
        public string descripcion { get; set; }
        public string nombreMarca { get; set; }
        public double cantidad { get; set; }
        public double cantidadUnitaria { get; set; }
        public double precioUnitario { get; set; }
        public double descuento { get; set; }
        public double total { get; set; }
        public int estado { get; set; }
        public int idCombinacionAlternativa { get; set; }
        public int idPresentacion { get; set; }
        public int idProducto { get; set; }
        public int idCompra { get; set; }
        public string codigoProducto { get; set; }
        public string nombreCombinacion { get; set; }
        public string nombrePresentacion { get; set; }
        public int idSucursal { get; set; }
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

    public class PagocompraC
    {
        public int idPago { get; set; }
        public string moneda { get; set; }
        public int idMoneda { get; set; }
        public int idMedioPago { get; set; }
        public int idCaja { get; set; }
        public int idCajaSesion { get; set; }
        public double pagarCompra { get; set; }
    }
    public class DatosParaNotaEntrada
    {
        public int idCompra { get; set; }
        public int idSucursal { get; set; }
        public int idPersonal { get; set; }
        public List<List<int>> dato { get; set; }
    }
    public class DatoNotaEntradaC
    {
        public int idProducto { get; set; }
        public int idCombinacionAlternativa { get; set; }
        public double cantidad { get; set; }
        public int idAlmacen { get; set; }
        public string descripcion { get; set; }


        public string nro { get; set; }

        public string nombreProducto { get; set; }

        public string nombreCombinacion { get; set; }

        public string almacen { get; set; }
        public decimal stock { get; set; }
        public decimal stockTotal { get; set; }
        public decimal stockGuardar { get; set; }
        public int idPresentacion { get; set; }
        public string cantidadUnitaria { get; set; }
        public string nombrePresentacion { get; set; }
        public decimal stockCompraRestante { get; set; }
        public decimal stockCompra { get; set; }
    }

    public class NotaentradaC
    {
        public int idCompra { get; set; }
        public int idTipoDocumento { get; set; }
        public int idPersonal { get; set; }
        public List<DatoNotaEntradaC> datoNotaEntrada { get; set; }
        public int generarNotaEntrada { get; set; }
    }

    public class compraTotal
    {
        public PagoC pago { get; set; }
        public CompraC compra { get; set; }
        public List<DetalleC> detalle { get; set; }
        public PagocompraC pagocompra { get; set; }
        public NotaentradaC notaentrada { get; set; }
    }

    public class CompraModificar
    {


        public int nro { get; set; }
        public int idDetalleCompra { get; set; }
        public string descripcion { get; set; }
        public double cantidad { get; set; }
        public double cantidadUnitaria { get; set; }
        public double precioUnitario { get; set; }
        public double descuento { get; set; }
        public double total { get; set; }
        public int estado { get; set; }
        public int idCombinacionAlternativa { get; set; }
        public object alternativas { get; set; }
        public string nombreCombinacion { get; set; }
        public int idPresentacion { get; set; }
        public string nombrePresentacion { get; set; }
        public int idProducto { get; set; }
        public string codigoProducto { get; set; }
        public int idSucursal { get; set; }
        public int idCompra { get; set; }
        public string nombreMarca { get; set; }
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
    public class FechaFacturacion
    {
        public DateTime date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class FechaPago
    {
        public DateTime date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class CompraRecuperar
    {
        public int idCompra { get; set; }
        public string numeroDocumento { get; set; }
        public string nombreProveedor { get; set; }
        public string rucDni { get; set; }
        public string direccion { get; set; }
        public string formaPago { get; set; }
        public string moneda { get; set; }
        public FechaFacturacion fechaFacturacion { get; set; }
        public FechaPago fechaPago { get; set; }
        public double descuento { get; set; }
        public string tipoCompra { get; set; }
        public string tipoCambio { get; set; }
        public double subTotal { get; set; }
        public double total { get; set; }
        public int estado { get; set; }
        public int idProveedor { get; set; }
        public int idPago { get; set; }
        public int idPersonal { get; set; }
        public int idTipoDocumento { get; set; }
        public int idSucursal { get; set; }
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


    public class CompraNEntrada
    {
        public int idCompra { get; set; }
        public string numeroDocumento { get; set; }
        public string nombreProveedor { get; set; }
        public string rucDni { get; set; }
        public Fecha fechaFacturacion { get; set; }
        public Fecha fechaPago { get; set; }

        public object nroOrdenCompra { get; set; }

        private string ordenCompraNro;
        public DateTime nFechaPago;
        public DateTime nFechaFacturacion;

        public string OrdenCompraNro
        {

            get
            {
                if (nroOrdenCompra == null)
                {

                    return "";
                }
                else
                {

                    return nroOrdenCompra.ToString();
                }               
            }
            set
            {


                ordenCompraNro = value;
            }


        }

        public DateTime NFechaPago
        {

            get
            {
               

                    return fechaPago.date;
                
            }
            set
            {


                nFechaPago = value;
            }


        }

        public DateTime NFechaFacturacion
        {

            get
            {


                return fechaFacturacion.date;

            }
            set
            {


                nFechaFacturacion = value;
            }


        }
    }

}
