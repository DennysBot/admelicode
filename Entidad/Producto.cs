using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Producto
    {
        //Compra,Venta,Stock cambiado el tipo de string a Decimal
        public int idProducto { get; set; }   // porque haci  manda desde el servicio modificado 
        public string idPresentacion { get; set; }
        public bool cantidadFraccion { get; set; }
        public string codigoBarras { get; set; }
        public string codigoProducto { get; set; }
        public string controlSinStock { get; set; }
        public string descripcionCorta { get; set; }
        public string descripcionLarga { get; set; }
        public bool enCategoriaEstrella { get; set; }
        public bool enPortada { get; set; }
        public bool enUso { get; set; }
        public bool estado { get; set; }
        public int idMarca { get; set; }
        public int idUnidadMedida { get; set; }
        public string keywords { get; set; }
        public string limiteMaximo { get; set; }
        public string limiteMinimo { get; set; }
        public bool mostrarPrecioWeb { get; set; }
        public bool mostrarVideo { get; set; }
        public bool mostrarWeb { get; set; }
        public string nombreMarca { get; set; }
        public string nombreProducto { get; set; }
        public string nombreUnidad { get; set; }
        public Decimal precioCompra { get; set; }
        public string urlVideo { get; set; }
        public bool ventaVarianteSinStock { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }
        private string estadoString;

        public string EstadoString
        {
            get
            {
                if(estado == true) { return "Activo"; }
                else { return "Anulado"; }
            }
            set
            {
                estadoString = value;
            }
        }
        public Decimal precioVenta { get; set; }
        public Decimal stock { get; set; }
        public string stockFinanciero { get; set; }
        public int idPresentacionAfectada { get; set; }
        public int idAlmacen { get; set; }
        public string nombreAlmacen { get; set; }

        private int idPresentacionint;
        public int IdPresentacionint
        {
            get
            {

                if (idPresentacion == null)
                {
                    return 0;
                }
                else
                {
                    return Int32.Parse(idPresentacion);
                }

            }
            set
            {
                idPresentacion = value.ToString();

            }


        }
            


    }

    public class ProductoVenta : Producto
    {
       
            public  new string precioVenta { get; set; }
            public new int idPresentacion { get; set; }
             public  new string precioCompra { get; set; }
            public string cantidadUnitaria { get; set; }
            public decimal StockTotal { get; set; }
            public decimal stockFalte { get; set; }
    }

    public class ProductoSinImpuesto
    {
        public int idProducto { get; set; }
        public int idPresentacion { get; set; }
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public string precioCompra { get; set; }
    }
    public class ProductoData
    {
        //Compra,Impuesto,Utilidad,Venta,Stock cambiando los tipos de string a Decimal
        public int idProducto { get; set; }
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public string descripcionCorta { get; set; }        
        public Decimal precioCompra { get; set; }
        public Decimal precioConImpuesto { get; set; }
        public Decimal utilidad { get; set; }
        public int estado { get; set; }
        public int idUnidadMedida { get; set; }
        public int idMarca { get; set; }
        public string nombreUnidad { get; set; }
        public string nombreMarca { get; set; }
        public bool enUso { get; set; }
        public bool mostrarWeb { get; set; }
        public bool mostrarPrecioWeb { get; set; }
        public Decimal precioVenta { get; set; }
        public Decimal stock { get; set; }
        public string stockFinanciero { get; set; }
        public int idSucursal { get; set; }
        public string sucursal { get; set; }
        public int idAlmacen { get; set; }
        public string almacen { get; set; }
        public string productoAlmacen { get; set; }
        private string estadoString;
        public List<ImpuestoData> impuesto { get; set; }

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

        public int idPresentacion { get; set; }
        public string codigo { get; set; }
        public string nombrePresentacion { get; set; }
     
        


    }
    public class ImpuestoData
    {
        public int idImpuesto { get; set; }
        public string valorImpuesto { get; set; }
        public bool porcentual { get; set; }
    }
    public class ProductoStockGuardar
    {
        public List<ProductoData> datos { get; set; }
    }


}
