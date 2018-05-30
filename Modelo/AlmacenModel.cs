using Entidad;
using Entidad.Location;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class AlmacenModel
    {
        private WebService webService = new WebService();
        private LocationModel locationModel = new LocationModel();

        public async Task<Response> guardar(UbicacionGeografica ubicacionGeografica, Almacen param)
        {
            try
            {
                // Obteniendo de la ubicacion geografica del sucursal
                Response res = await locationModel.guardarUbigeo(ubicacionGeografica);
                param.idUbicacionGeografica = res.id;

                // localhost:8080/admeli/xcore2/xcore/services.php/almacen/guardar
                return await webService.POST<Almacen,Response>("almacen", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(UbicacionGeografica ubicacionGeografica, Almacen param)
        {
            try
            {
                // Obteniendo de la ubicacion geografica del sucursal
                Response res = await locationModel.guardarUbigeo(ubicacionGeografica);
                param.idUbicacionGeografica = res.id;

                // localhost:8080/admeli/xcore2/xcore/services.php/almacen/modificar
                return await webService.POST<Almacen,Response>("almacen", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<Response> desactivar(Almacen param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/almacen/desactivar
                return await webService.POST<Almacen,Response>("almacen", "desactivar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<Response> eliminar(Almacen param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/almacen/eliminar
                return await webService.POST<Almacen,Response>("almacen", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Almacen>> verificarAlmacen(string nombre, int sucursal, int idAlmcaen)
        {
            try
            {
                // www.lineatienda.com/services.php/verificarnombrealmacen/nombre/almacen%201/sucursal/1/almacen/-1
                List<Almacen> list = await webService.GET<List<Almacen>>("verificarnombrealmacen", String.Format("nombre/{0}/sucursal/{1}/almacen/{2}", nombre, sucursal, idAlmcaen));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Almacen>> almacenes(int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/almacenes/estado/1/100
                RootObject<Almacen> almacenes = await webService.GET<RootObject<Almacen>>("almacenes", String.Format("estado/{0}/{1}", page, items));
                return almacenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Almacen>> almacenes(int estado = 1)
        {
            try
            {
                // wwww.lineatienda.com/services.php/personalalmacenes/per/1/suc/2
                // www.lineatienda.com/services.php/almacenes/id/nombre/estado/1
                List<Almacen> list = await webService.GET<List<Almacen>>("almacenes", String.Format("id/nombre/estado/{0}",estado));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Almacen>> almacenesPorSucursales(int idSucursal)
        {
            try
            {
                // www.lineatienda.com/services.php/almacenesporsucursal/sucursal/0
                List<Almacen> list = await webService.GET<List<Almacen>>("almacenesporsucursal", String.Format("sucursal/{0}", idSucursal));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<AlmacenCompra>> almacenesCompra( int idSucursal,int idPersonal)
        {
            try
            {
                // http://localhost:8080/admeli/xcore/services.php/personalalmacenes/per/1/suc/1
                List<AlmacenCompra> list = await webService.GET<List<AlmacenCompra>>("personalalmacenes", String.Format("per/{0}/suc/{1}", idPersonal, idSucursal));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // almacen/suc/

        public async Task<List<Almacen>> listarAlmacenPorIdSucursal(int idSucursal)
        {
            try
            {
                // http://localhost:8080/admeli/xcore/services.php/almacen/suc/1
                List<Almacen> list = await webService.GET<List<Almacen>>("almacen", String.Format("suc/{0}", idSucursal));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<List<Almacen>> almacenesAsignados(int idSucursal,int idPersonal, int tipo=1)
        {
            try
            {
                // http://localhost:8080/admeli/xcore/services.php/almacenesporsucursal/sucursal/1/personal/1/tipo/1
                List<Almacen> list = await webService.GET<List<Almacen>>("almacenesporsucursal", String.Format("sucursal/{0}/personal/{1}/tipo/{2}", idSucursal,idPersonal,tipo));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<AlmacenCorrelativo>> DocCorrelativoAlmacen(int idAlmacen, int idTipoDocumento=8)
        {
            try
            {
                // http://localhost:8080/admeli/xcore/services.php/verificarexistenciaalmacencorrelativo/almacen/1/tipodoc/8
                List<AlmacenCorrelativo> list = await webService.GET<List<AlmacenCorrelativo>>("verificarexistenciaalmacencorrelativo", String.Format("almacen/{0}/tipodoc/{1}", idAlmacen, idTipoDocumento));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
