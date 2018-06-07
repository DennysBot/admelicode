using Entidad;
using Modelo.Recursos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ProductoModel
    {
        public static Producto producto { get; set; }
        private WebService webService = new WebService();

        //guardar varios productos
        public async Task<ResponseD> guardarVariosProductos<T>(List<T> listaProductos)
        {
            try
            {
                string stringProductos = "[";
                foreach(T producto in listaProductos)
                {
                    stringProductos += JsonConvert.SerializeObject(producto)+",";
                }
                stringProductos= stringProductos.Substring(0, stringProductos.Length - 1);
                stringProductos += "]";
                ResponseD response = await webService.PostSendTexto<ResponseD>("producto/guardarvarios", stringProductos);
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        // validacion de un producto     
        public async Task<List<Producto>> validarProducto(Producto param)
        {
            try
            {
                // localhost/admeli/xcore/services.php/verificarproductonombrecodigo
                List<Producto> list = await webService.POSTResponse("verificarproductonombrecodigo", param);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> guardar(Producto param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/producto/guardar
                return await webService.POST<Producto,Response>("producto", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> guardarCategoria(string TextoPlano)
        {
            try
            {
                //http://localhost:8085/admeli/xcore/services.php/cproducto1
                return await webService.POSTSerializado< Response>("cproducto1", TextoPlano);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(Producto param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/producto/modificar
                return await webService.POST<Producto,Response>("producto", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(Producto param)
        {
            try
            {
                // localhost/admeli/xcore/services.php/producto/eliminar
                return await webService.POST<Producto,Response>("producto", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<Response> inhabilitar(Producto param)
        {
            try
            {
                //http://localhost:8085/ad_meli/xcore/services.php/producto/inhabilitar
                return await webService.POST<Producto, Response>("producto", "inhabilitar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Producto, CombinacionStock>> productosPorCategoria(Dictionary<string, int> dictionary, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/productos/categoria/1/100
                Dictionary<string, int>[] dataSend = { dictionary };

                RootObject<Producto, CombinacionStock> productos = await webService.POST<Dictionary<string, int>[], RootObject<Producto, CombinacionStock>>("productos", String.Format("categoria/{0}/{1}",page,items), dataSend);
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Producto, CombinacionStock>> productosPorCategoriaBuscar(Dictionary<string, int> dictionary, string like, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/productos/categoria/1/100/pantalla
                Dictionary<string, int>[] dataSend = { dictionary };

                RootObject<Producto, CombinacionStock> productos = await webService.POST<Dictionary<string, int>[], RootObject<Producto, CombinacionStock>>("productos", String.Format("categoria/{0}/{1}/{2}", page, items,like), dataSend);
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Producto, CombinacionStock>> productosStock(Dictionary<string, int> dictionary, string like, int idAlmacen, int idSucursal, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/productos/categoria/stock/1/100/1/1
                Dictionary<string, int>[] dataSend = { dictionary };

                RootObject<Producto, CombinacionStock> productos = await webService.POST<Dictionary<string, int>[], RootObject<Producto, CombinacionStock>>("productos", String.Format("categoria/stock/{0}/{1}/{2}/{3}", page, items, idAlmacen, idSucursal), dataSend);
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<RootObject<ProductoVenta, CombinacionStock>> productosVentaStock(Dictionary<string, int> dictionary, string like, int idAlmacen, int idSucursal, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/productos/categoria/stock/1/100/1/1
                Dictionary<string, int>[] dataSend = { dictionary };

                RootObject<ProductoVenta, CombinacionStock> productos = await webService.POST<Dictionary<string, int>[], RootObject<ProductoVenta, CombinacionStock>>("productos", String.Format("categoria/stock/{0}/{1}/{2}/{3}", page, items, idAlmacen, idSucursal), dataSend);
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<RootObject<Producto, CombinacionStock>> productosStockLike(Dictionary<string, int> dictionary, string like, int idAlmacen, int idSucursal, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/productos/categoria/stock/1/100/pantalla/1/1
                Dictionary<string, int>[] dataSend = { dictionary };

                RootObject<Producto, CombinacionStock> productos = await webService.POST<Dictionary<string, int>[], RootObject<Producto, CombinacionStock>>("productos", String.Format("categoria/stock/{0}/{1}/{2}/{3}/{4}", page, items, like, idAlmacen, idSucursal), dataSend);
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> stockHerramienta<T>(Dictionary<string, int> dictionary, int idAlmacen, int idSucursal, int page, int items)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/productos/categoria/stockherramienta/1/100/0/0
                Dictionary<string, int>[] dataSend = { dictionary };

                T productos = await webService.POST<Dictionary<string, int>[], T>("productos", String.Format("categoria/stockherramienta/{0}/{1}/{2}/{3}", page, items, idAlmacen, idSucursal), dataSend);
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> stockHerramientaLike<T>(Dictionary<string, int> dictionary, string like, int idAlmacen, int idSucursal, int page, int items)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/productos/categoria/stockherramienta/1/100/0/0
                // localhost:8080/admeli/xcore/services.php/productos/categoria/stockherramienta/1/100000/moto/0/1
                Dictionary<string, int>[] dataSend = { dictionary };

                T productos = await webService.POST<Dictionary<string, int>[], T>("productos", String.Format("categoria/stockherramienta/{0}/{1}/{2}/{3}/{4}", page, items, like, idAlmacen, idSucursal), dataSend);
                return productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Producto>> productos21(int idProducto)
        {
            try
            {
                // localhost/admeli/xcore/services.php/productos21/17
                List<Producto> list = await webService.GET<List<Producto>>("productos21", String.Format("{0}", idProducto));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Producto> productoDatos(int idProducto)
        {
            try
            {
                // localhost/admeli/xcore/services.php/producto/datos/21
                List<Producto> list = await webService.GET<List<Producto>>("producto", String.Format("datos/{0}", idProducto));
                return list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Producto>> productoRelacionado(int idProducto, string tipoRelacion, int productoRelacion)
        {
            try
            {
                // localhost/admeli/xcore/services.php/listarproductosnorelacionados/producto/21/tiporelacion/complementaria/productorelacion/0
                List<Producto> list = await webService.GET<List<Producto>>("listarproductosnorelacionados", String.Format("producto/{0}/tiporelacion/{1}/productorelacion/{2}", idProducto, tipoRelacion, productoRelacion));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Producto>> productos()
        {
            try
            {
                // localhost/admeli/xcore/services.php/productos41
                List<Producto> list = await webService.GET<List<Producto>>("productos41");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductoVenta>> productos(int idSucursal, int idPersonal, int idAlmacen)
        {
            try
            {
                // localhost/admeli/xcore/services.php/productos/suc/1/personal/1
                List<ProductoVenta> list = await webService.GET<List<ProductoVenta>>("productos", String.Format("suc/{0}/personal/{1}/almacen/{2}", idSucursal, idPersonal, idAlmacen));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<Producto>> productosSinCategoria()
        {
            try
            {
                // localhost/admeli/xcore/services.php/productos41/categoria
                List<Producto> list = await webService.GET<List<Producto>>("productos41", "categoria");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductoVenta>> productosVenta(int idAlmacen)
        {
            try
            {
                // localhost/admeli/xcore/services.php//productosVenta/Almacen/:idAlmacen
                List<ProductoVenta> list = await webService.GET<List<ProductoVenta>>("productosVenta", String.Format("Almacen/{0}", idAlmacen));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //listarProductoPorIdProductoCodigoNombreSinImpuesto
        public async Task<List<ProductoSinImpuesto>> listarProductoPorIdProductoCodigoNombreSinImpuesto(int idSucursal)
        {
            try
            {
                // localhost/admeli/xcore/services.php/productos41/impuesto/sucursal/1
                List<ProductoSinImpuesto> list = await webService.GET<List<ProductoSinImpuesto>>("productos41", String.Format("impuesto/sucursal/{0}", idSucursal));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<ProductoVenta>> listarProductosTipoPrecioVenta(int idSucursal,int idPersonal)
        {
            try
            {
                // localhost/admeli/xcore/services.php/productos/suc/1/personal/1
                List<ProductoVenta> list = await webService.GET<List<ProductoVenta>>("productos", String.Format("suc/{0}/personal/{1}", idSucursal, idPersonal));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public async Task<List<Producto>> BuscarProducto(int idProducto)
        {
            try
            {
                // localhost/admeli/xcore/services.php/producto//1
                List<Producto> list = await webService.GET<List<Producto>>("producto", String.Format("{0}", idProducto));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
