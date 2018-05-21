using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class PresentacionModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar(Presentacion param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/presentacion/guardar
                return await webService.POST<Presentacion,Response>("presentacion", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(Presentacion param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/presentacion/modificar
                return await webService.POST<Presentacion,Response>("presentacion", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(Presentacion param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/presentacion/eliminar
                return await webService.POST<Presentacion,Response>("presentacion", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Presentacion>> presentaciones(int idProducto)
        {
            try
            {
                // localhost/admeli/xcore/services.php/presentacion/producto/21
                List<Presentacion> list = await webService.GET<List<Presentacion>>("presentacion", String.Format("producto/{0}", idProducto));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Presentacion>> presentacionesTodas(int idSucursal){
            try
            {
                //localhost/admeli/xcore/services.php/presentacion/producto/0
                List<Presentacion> list = await webService.GET<List<Presentacion>>("presentacion", String.Format("producto/todas/{0}", idSucursal));
                return list;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Presentacion>> presentacionVentas(int idProducto)
        {
            try
            {
                // localhost/admeli/xcore/services.php/listarpresentacionventas/producto/28
                List<Presentacion> list = await webService.GET<List<Presentacion>>("listarpresentacionventas", String.Format("producto/{0}", idProducto));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<PresentacionV>> presentacionVentas(int idProducto,int otro)
        {
            try
            {
                // localhost/admeli/xcore/services.php/listarpresentacionventas/producto/28
                List<PresentacionV> list = await webService.GET<List<PresentacionV>>("listarpresentacionventas", String.Format("producto/{0}", idProducto));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //precio/producto/:idp/sucursal/:ids

        public async Task<List<Producto>> precioProducto(int idPresentacion, int idSucursal)
        {
            try
            {
                // localhost/admeli/xcore/services.php/precio/producto/28
                List<Producto> list = await webService.GET<List<Producto>>("precio", String.Format("producto/{0}/sucursal/{1}", idPresentacion, idSucursal));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
