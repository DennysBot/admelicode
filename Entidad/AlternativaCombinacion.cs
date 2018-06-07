using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class AlternativaCombinacion
    {
        public int idCombinacionAlternativa { get; set; }
        public int idCombinacionAlternativaStockAlmacen { get; set; }
        public int idProducto { get; set; }
        public int idAlmacen { get; set; }
        public string alertaStock { get; set; }
        public string alternativas { get; set; }
        public string codigoSku { get; set; }
        public string nombreCombinacion { get; set; }
        public string precio { get; set; }
        public string stock { get; set; }
        public string stockIdeal { get; set; }
        public string stockMinimo { get; set; }
    }


    public class CombinacionStock
    {
        public int idCombinacionAlternativa { get; set; }
        public string alternativas { get; set; }
        public decimal precio { get; set; }
        public string nombreCombinacion { get; set; }
        public int idPresentacion { get; set; }
        public int idProducto { get; set; }
        public string codigoSku { get; set; }
        public decimal stock { get; set; }
        public int idAlmacen { get; set; }
        public string productoAlmacen { get; set; }

        public decimal precioVenta { get; set; }

        public decimal precioVentaTotal;
        public decimal PrecioVentaTotal {
            get {

                return  (precio + precioVenta);

            } set
            {

                precioVentaTotal = value;
            }
        }

    }
} 