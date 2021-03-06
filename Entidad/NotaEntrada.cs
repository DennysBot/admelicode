﻿using Entidad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class NotaEntrada
    {

        public int idNotaEntrada { get; set; }
        public string serie { get; set; }
        public string correlativo { get; set; }
        public Fecha fecha { get; set; }
        public Fecha fechaEntrada { get; set; }
        public string observacion { get; set; }
        public int estadoEntrega { get; set; }
        public int estado { get; set; }
        public int idTipoDocumento { get; set; }
        public int idAlmacen { get; set; }
        public string idCompra { get; set; }
        public string numeroDocumento { get; set; }
        public string nombreProveedor { get; set; }
        public string rucDni { get; set; }
        public string personal { get; set; }
        public string nombre { get; set; }
        public int idSucursal { get; set; }
        public int idPersonal { get; set; }

        public string nombreAlmacen { get; set; }
        public object numeroDocumentoCompra { get; set; }

        private string estadoString;
        public string EstadoString
        {
            get
            {
                if (estadoEntrega == 1) { return "Entregado"; }
                else { return "Pendiente"; }
            }
            set
            {
                estadoString = value;
            }
        }


    }

    public class CargaCompraSinNota
    {

        public int idDetalleNotaEntrada { get; set; }
        public string descripcion { get; set; }
        public double cantidad { get; set; }
        public double cantidadUnitaria { get; set; }
        public int idCombinacionAlternativa { get; set; }
        public object alternativas { get; set; }
        public string nombreCombinacion { get; set; }
        public int idPresentacion { get; set; }
        public string nombrePresentacion { get; set; }
        public int idProducto { get; set; }
        public string codigoProducto { get; set; }
        public int idNotaEntrada { get; set; }
        public double cantidadRecibida { get; set; }
        public bool ventaVarianteSinStock { get; set; }
        public string nombreMarca { get; set; }         
        public int estado { get; set; }   
        public int nro { get; set; }
        // dato solo cuando se modifica
        public string cantidadRestante { get; set; }


       



        public int idDetalleNotaSalida { get; set; }
        
        public string variante { get; set; }
       
        public int idNotaSalida { get; set; }
        public double total { get; set; }
  
        public int idDetalleVenta { get; set; }
        public double precioEnvio { get; set; }
        public double descuento { get; set; }
     
        public int idVenta { get; set; }
    
        public int idDetalleGuiaRemision { get; set; }
        public int idGuiaRemision { get; set; }

        // solo para la decoracion despues de ver si abastece o no 
        public bool noExiteStock { get; set; }








    }



    public class CargaCompraSinNotaComparer : IEqualityComparer<CargaCompraSinNota>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(CargaCompraSinNota x, CargaCompraSinNota y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.idCombinacionAlternativa == y.idCombinacionAlternativa && x.idPresentacion == y.idPresentacion;
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(CargaCompraSinNota product)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(product, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashProductName = product.idCombinacionAlternativa ==0? 0 : product.idCombinacionAlternativa.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = product.idPresentacion.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode;
        }

    }




    public class ComprobarNota
    {
        public int idCompra { get; set; }
        public int idNotaEntrada { get; set; }
        public List<List<int>> dato { get; set; }
    }
  
    public class CompraEntradaGuardar
    {
        public int  idCompra { get; set; }
    }

    


}
