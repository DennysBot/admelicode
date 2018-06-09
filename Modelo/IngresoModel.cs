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
    public class IngresoModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar<T>(T param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/ingreso/guardarenuno
                return await webService.POST<T,Response>("ingreso", "guardarenuno", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar<T>(T param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/ingreso/modificarenuno
                return await webService.POST<T,Response>("ingreso", "modificarenuno", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> desactivar(Ingreso param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/ingreso/anularingresonoventa
                return await webService.POST<Ingreso,Response>("ingreso", "anularingresonoventa", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Task<Response> eliminar(Ingreso currentIngreso)
        {
            return null;
        }

        public async Task<RootObject<Ingreso>> ingresos(int idSucursal, int idPersonal, int idCajaSesion, string idEstado, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/ingresos/sucursal/0/personal/0/cajasesion/0/estado/todos/1/100
                RootObject<Ingreso> ingresos = await webService.GET<RootObject<Ingreso>>("ingresos", String.Format("sucursal/{0}/personal/{1}/cajasesion/{2}/estado/{3}/{4}/{5}", idSucursal, idPersonal, idCajaSesion, idEstado,  page, items));
                return ingresos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Ingreso>> ingresos(int idCajaSesion)
        {
            try
            {
                // www.lineatienda.com/services.php/ingresos/montoinicio/3
                List<Ingreso> ingresos = await webService.GET<List<Ingreso>>("ingresos", String.Format("montoinicio/{0}", idCajaSesion));
                return ingresos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /**
         * Guardar configuraciones al iniciar la caja
         * **/
        public async Task<Response> guardarEnUno<T>(T param)
        {
            try
            {
                // localhost/admeli/xcore/services.php/ingreso/guardarenuno
                Response response = await webService.POST<T,Response>("ingreso", "guardarenuno", param);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificarEnUno<T>(T param)
        {
            try
            {
                //localhost/admeli/score/services.php/ingreso/modificarenuno
                Response response = await webService.POST<T, Response>("ingreso", "guardarenuno", param);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // hacer adelantos utilizando una cotizacion 

        public async Task<Response> guardarAdelanto(Adelanto param)
        {
            try
            {
                // www.admeli.com/demo2/services.php//Adelanto/guardar
                Response response = await webService.POST<Adelanto, Response>("Adelanto", "guardar", param);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
