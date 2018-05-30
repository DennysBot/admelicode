using Entidad;
using Entidad.Configuracion;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class CajaModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar<T>(T param)
        {
            try
            {
                // localhost/admeli/xcore/services.php/cajasesion/guardar
                return await webService.POST<T,Response>("cajasesion", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(CajaSesion param)
        {
            try
            {
                // 192.168.1.19:8080/admeli/xcore/services.php/cajasesion/eliminar
                return await webService.POST<CajaSesion, Response>("cajasesion", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        //
        public async Task<T> correlativoSerie<T>(int idCaja, int tipo = 0)
        {
            try
            {
                // www.lineatienda.com/services.php/cajacorrelativoserie/caja/1/tipo/0
                List<T> list = await webService.GET<List<T>>("cajacorrelativoserie", String.Format("caja/{0}/tipo/{1}", idCaja,tipo));
                return list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<CajaSesion>> cajaSesionesInicializadas(int idSucursal, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/cajasesionesinicializadas/suc/1/1/100
                RootObject<CajaSesion> cajaSesion = await webService.GET<RootObject<CajaSesion>>("cajasesionesinicializadas", String.Format("suc/{0}/{1}/{2}", idSucursal, page, items));
                return cajaSesion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Moneda>> cierreCajaIngresoMenosEgreso(int mediopago, int cajaSesion)
        {
            try
            {
                // localhost/admeli/xcore/services.php/cierrecajaingresomenosegreso/mediopago/1/cajasesion/7
                //List<Moneda> list = await webService.GET<List<Moneda>>("cajasesionesinicializadas", String.Format("mediopago/{0}/cajasesion/{1}", mediopago, cajaSesion));
                List<Moneda> list = await webService.GET<List<Moneda>>("cierrecajaingresomenosegreso", String.Format("mediopago/{0}/cajasesion/{1}", mediopago, cajaSesion));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CajaSesion>> cajaSesion(int idAsignarCaja)
        {
            try
            {
                // localhost/admeli/xcore/services.php/cajasesion/idasignarcaja/1
                List<CajaSesion> list = await webService.GET<List<CajaSesion>>("cajasesion", String.Format("idasignarcaja/{0}", idAsignarCaja));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Moneda>> cierrecajaiIgresoMenosEgresoEditarEgreso(int mediopago, int cajaSesion, double egreso)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/cierrecajaingresomenosegresoeditaregreso/mediopago/1/cajasesion/7/egreso/0
                List<Moneda> list = await webService.GET<List<Moneda>>("cierrecajaingresomenosegresoeditaregreso", String.Format("mediopago/{0}/cajasesion/{1}/egreso/{2}", mediopago, cajaSesion, egreso));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Moneda>> verificarActividad(int idSesion)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/cajasesion/verificaractividad/sesion/9
                List<Moneda> list = await webService.GET<List<Moneda>>("cajasesion", String.Format("verificaractividad/sesion/{0}", idSesion));
                if (list.Count == 0) throw new Exception("Usted no registro ninguna actividad.");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<CajaCorrelativo>> listarCajasByIdSucursal(int idSucursal)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/cajas/suc/1
                List<CajaCorrelativo> list = await webService.GET<List<CajaCorrelativo>>("cajas", String.Format("suc/{0}", idSucursal));
               
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }      
        public async Task<List<DineroCompra>> totalCajaCompra(int idMedioPago, int idCajasesion, int idCompra )
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php///cierrecajaingresomenosegresocompra/mediopago/1/cajasesion/2/compra/0

                
                List<DineroCompra> list = await webService.GET<List<DineroCompra>>("cierrecajaingresomenosegresocompra", String.Format("mediopago/{0}/cajasesion/{1}/compra/{2}", idMedioPago, idCajasesion, idCompra));

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //cierrecajaingresomenosegresocompra/mediopago/1/cajasesion/4/compra/0
        public async Task<List<Moneda>> cierreCajaCompra(int mediopago, int cajaSesion,int idCompra)
        {
            try
            {
                // localhost/admeli/xcore/services.php/cierrecajaingresomenosegreso/mediopago/1/cajasesion/7
                //List<Moneda> list = await webService.GET<List<Moneda>>("cajasesionesinicializadas", String.Format("mediopago/{0}/cajasesion/{1}", mediopago, cajaSesion));
                List<Moneda> list = await webService.GET<List<Moneda>>("cierrecajaingresomenosegresocompra", String.Format("mediopago/{0}/cajasesion/{1}/compra/{2}", mediopago, cajaSesion, idCompra));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
