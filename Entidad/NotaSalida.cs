using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class NotaSalida
    {
        public int idNotaSalida { get; set; }
        public string serie { get; set; }
        public string correlativo { get; set; }
        public string motivo { get; set; }
        public Fecha fecha { get; set; }
        public Fecha fechaSalida { get; set; }
        public string descripcion { get; set; }
        public string destino { get; set; }
        public int estadoEnvio { get; set; }
        public int estado { get; set; }
        public int idAlmacen { get; set; }
        public int idTipoDocumento { get; set; }
        public string idVenta { get; set; }
        public string numeroDocumento { get; set; }
        public string nombreCliente { get; set; }
        public string rucDni { get; set; }
        public string personal { get; set; }
        public string nombre { get; set; }
        public int idSucursal { get; set; }
        public int idPersonal { get; set; }

        private string estadoString;
        public string EstadoString
        {
            get
            {
                string estadoEnvioString="";
                switch(estadoEnvio)
                {
                    case 0:
                        estadoEnvioString = "Pendiente";
                    break;
                    case 1:
                        estadoEnvioString = "Revisado";
                        break;
                    case 2:
                        estadoEnvioString = "Entregado";
                        break;
                }



                return estadoEnvioString;

            }
            set
            {
                estadoString = value;
            }
        }


    }






    public class FechaR
    {
        public DateTime date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class FechaSalidaR
    {
        public DateTime  date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class NotaSalidaR
    {
        public int idNotaSalida { get; set; }
        public string serie { get; set; }
        public string correlativo { get; set; }
        public string motivo { get; set; }
        public Fecha fecha { get; set; }
        public Fecha fechaSalida { get; set; }
        public string descripcion { get; set; }
        public string destino { get; set; }
        public int estadoEnvio { get; set; }
        public int estado { get; set; }
        public int idAlmacen { get; set; }
        public int idTipoDocumento { get; set; }
        public string idVenta { get; set; }
        public string numeroDocumento { get; set; }
        public string nombreCliente { get; set; }
        public string rucDni { get; set; }
        private DateTime rFecha;
        private DateTime rFechaSalida;

        public DateTime RFecha
        {

            get
            {
                return fecha.date;
            }
            set
            {
                rFecha = value;
            }


        }
        public DateTime RFechaSalida
        {

            get
            {
                return fechaSalida.date;
            }
            set
            {
                rFechaSalida = value;
            }


        }

    }

    public class DetalleNotaSalida
    {
        public int nro { get; set; }
        public int idDetalleNotaSalida { get; set; }
        public string descripcion { get; set; }
        public double cantidad { get; set; }
        public double cantidadUnitaria { get; set; }
        public int estado { get; set; }
        public int idCombinacionAlternativa { get; set; }
        public string variante { get; set; }
        public object alternativas { get; set; }
        public int idPresentacion { get; set; }
        public string presentacion { get; set; }
        public int idProducto { get; set; }
        public string codigoProducto { get; set; }
        public int idNotaSalida { get; set; }
        public double total { get; set; }
        public string nombreMarca { get; set; }
        public int idDetalleVenta { get; set; }     
        public double precioEnvio { get; set; }
        public double descuento { get; set; }        
        public string nombreCombinacion { get; set; }  
        public string nombrePresentacion { get; set; }     
        public int idVenta { get; set; }

        public bool ventaVarianteSinStock { get; set; }
        public int idDetalleGuiaRemision { get; set; }     
        public int idGuiaRemision { get; set; }





        // para la nota de entrada
        public int idDetalleNotaEntrada { get; set; }
      
        public int idNotaEntrada { get; set; }
        public double cantidadRecibida { get; set; }
       
        public string cantidadRestante { get; set; }


    } 

    public class VentasNSalida
    {

        public int idVenta { get; set; }
        public string numeroDocumento { get; set; }
        public string nombreCliente { get; set; }
        public string rucDni { get; set; }
        public Fecha fechaVenta { get; set; }
        public Fecha fechaPago { get; set; }

        private DateTime fechaVentaS;
        private DateTime fechaPagoS;

        public DateTime FechaVentaS
        {
            get
            {

                return fechaVenta.date;
            }
            set
            {

                fechaVentaS = value;
            }


        }
        public DateTime FechaPagoS
        {
            get
            {

                return fechaPago.date;
            }
            set
            {

                fechaPagoS = value;
            }


        }

    }



    public class ComprobarNotaSalida
    {
        public int idNotaSalida { get; set; }
        public int idVenta { get; set; }
        public int idAlmacen { get; set; }
        public List<List<object>> dato { get; set; }
    }


    public class AlmacenSalida
    {

        public int idNotaSalida { get; set; }
        public int idAlmacen { get; set; }
        public int idTipoDocumento { get; set; }
        public string motivo { get; set; }
        public string descripcion { get; set; }
        public int estadoEnvio { get; set; }
        public string destino { get; set; }
        public int idPersonal { get; set; }
        public string fechaSalida { get; set; }
    }
    public class VentaSalida
    {
        public int idVenta { get; set; }     
    }

}



