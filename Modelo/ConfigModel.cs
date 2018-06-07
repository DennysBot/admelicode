using Entidad;
using Entidad.Configuracion;
using Entidad.Location;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ConfigModel
    {

        private LocationModel locationModel = new LocationModel();
        private WebService webService = new WebService();

        public static DatosGenerales datosGenerales { get; set; }
        public static Sucursal sucursal { get; set; }
        public static AsignacionPersonal asignacionPersonal { get; set; }
        public static ConfiguracionGeneral configuracionGeneral { get; set; }
        public static List<Moneda> monedas { get; set; }
        public static List<TipoCambioMoneda> tipoCambioMonedas { get; set; }

        public static List<Sucursal> listSucursales { get; set; }

        // TIPOS DE DOCUMENTO       www.lineatienda.com/services.php/tipodoc21/estado/1
        // ALMACENES                www.lineatienda.com/services.php/personalalmacenes/per/8/suc/1
        // ASIGNACION PERSONAL      www.lineatienda.com/services.php/asignarpuntoventas/sucursal/1/personal/8
        // CAJA SECION              www.lineatienda.com/services.php/cajasesion/idasignarcaja/3
        public static List<TipoDocumento> tipoDocumento { get; set; }
        public static List<Almacen> alamacenes { get; set; }
        public static List<PuntoDeVenta> puntosDeVenta { get; set; }
        public static CajaSesion cajaSesion { get; set; }

        // www.lineatienda.com/services.php/cierrecajaingresomenosegreso/mediopago/1/cajasesion/62
        public static List<Moneda> cierreIngresoEgreso { get; set; }

        /**
         * OTROS PARAMETROS
         * estas configuraciones se deben establecer al inicial la aplicacion
         * segun el numero de almacenes y puntos de venta
         * */
        public static int currentIdAlmacen { get; set; }
        public static int currentPuntoVenta { get; set; }
        public static bool cajaIniciada { get; set; }
        public static Dictionary<string, int> currentProductoCategory = new Dictionary<string, int>();
        public static Dictionary<string, int> currentProductoCategoryStock = new Dictionary<string, int>();


        public async Task<Response> guardarDatosGenerales(UbicacionGeografica ubicacionGeografica, DatosGenerales datosGenerales)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/datosgenerales/modificar
                Response res = await locationModel.guardarUbigeo(ubicacionGeografica);
                datosGenerales.idUbicacionGeografica = res.id;
                
                return await webService.POST<DatosGenerales,Response>("datosgenerales", "modificar", datosGenerales);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> guardarConfigGeneral(ConfiguracionGeneral configGeneral)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/configuraciongeneral/modificar
                return await webService.POST<ConfiguracionGeneral,Response>("configuraciongeneral", "modificar", configGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        ///  Cargando la configuracion general
        /// </summary>
        /// <returns></returns>
        public async Task loadConfiGeneral(string version)
        {
            try
            {
                // www.lineatienda.com/services.php/configeneral
                List<ConfiguracionGeneral> list = await webService.GET<List<ConfiguracionGeneral>>("configeneral");
                configuracionGeneral = list[0];
                if (configuracionGeneral.version != version)
                {
                    throw new Exception("Está usando una versión diferente, descargue la versión correcta del sistema!!");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cargando las monedas
        /// </summary>
        /// <returns></returns>
        public async Task loadMonedas()
        {
            try
            {
                // www.lineatienda.com/services.php/monedas/estado/1
                List<Moneda> list = await webService.GET<List<Moneda>>("monedas", "estado/1");
                monedas = list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cargando los tipos de cambios de las monedas
        /// </summary>
        /// <returns></returns>
        public async Task loadTipoCambioMonedas()
        {
            try
            {
                // www.lineatienda.com/services.php/tipocambiotodasmonedas/hoy
                List<TipoCambioMoneda> list = await webService.GET<List<TipoCambioMoneda>>("tipocambiotodasmonedas", "hoy");
                tipoCambioMonedas = list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cargando los tipos de documentos
        /// </summary>
        /// <returns></returns>
        public async Task loadTipoDocumento()
        {
            try
            {
                // www.lineatienda.com/services.php/tipodoc21/estado/1
                List<TipoDocumento> list = await webService.GET<List<TipoDocumento>>("tipodoc21", "estado/1");
                tipoDocumento = list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cargando Loas almacenes por personal y sucursal asignado
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <param name="idSucursal"></param>
        /// <returns></returns>
        public async Task loadAlmacenes(int idPersonal, int idSucursal)
        {
            try
            {
                // www.lineatienda.com/services.php/personalalmacenes/per/8/suc/1
                List<Almacen> list = await webService.GET<List<Almacen>>("personalalmacenes", String.Format("per/{0}/suc/{1}", idPersonal, idSucursal));
                alamacenes = list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cargando Puntos de venta
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <param name="idSucursal"></param>
        /// <returns></returns>
        public async Task loadPuntoDeVenta(int idPersonal, int idSucursal)
        {
            try
            {
                // www.lineatienda.com/services.php/asignarpuntoventas/sucursal/1/personal/8
                List<PuntoDeVenta> list = await webService.GET<List<PuntoDeVenta>>("asignarpuntoventas", String.Format("sucursal/{0}/personal/{1}", idSucursal, idPersonal));
                puntosDeVenta = list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cargando la session de la caja
        /// </summary>
        /// <param name="idAsignarCaja"></param>
        /// <returns></returns>
        public async Task loadCajaSesion(int idAsignarCaja)
        {
            try
            {

                // www.lineatienda.com/services.php/cajasesion/idasignarcaja/3
                List<CajaSesion> list = await webService.GET<List<CajaSesion>>("cajasesion", String.Format("idasignarcaja/{0}", idAsignarCaja));
                if (list.Count > 0)
                {
                    cajaSesion = list[0];
                    ConfigModel.cajaSesion = cajaSesion;
                    ConfigModel.cajaIniciada = true;
                }
                else
                {
                    ConfigModel.cajaIniciada = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Cargando Ingresos Y Egresos De la caja cerrada
        /// </summary>
        /// <param name="idMedioPago"></param>
        /// <param name="idCajaSesion"></param>
        /// <returns></returns>
        public async Task loadCierreIngresoEgreso(int idMedioPago, int idCajaSesion)
        {
            try
            {

                // localhost:8080/admeli/xcore/services.php/cierrecajaingresomenosegreso/mediopago/1/cajasesion/24
                List<Moneda> list = await webService.GET<List<Moneda>>("cierrecajaingresomenosegreso", String.Format("mediopago/{0}/cajasesion/{1}", idMedioPago, idCajaSesion));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task loadDatosGenerales()
        {
            try
            {
                // www.lineatienda.com/services.php/generales
                List<DatosGenerales> list = await webService.GET<List<DatosGenerales>>("generales");
                datosGenerales = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DatosGenerales> getDatosGenerales()
        {
            try
            {
                // www.lineatienda.com/services.php/generales
                List<DatosGenerales> listData = await webService.GET<List<DatosGenerales>>("generales");
                return listData[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task loadSucursalPersonal(int idPersonal)
        {
            try
            {
                // www.lineatienda.com/services.php/sucursalespersonal/8
                 listSucursales = await webService.GET<List<Sucursal>>("sucursalespersonal",idPersonal.ToString());
                if (listSucursales.Count == 0) throw new Exception("Usted no pertenece a una sucursal no podrá ingresar al sistema.");
                sucursal = listSucursales[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task loadAsignacionPersonales(int idPersonal, int idSucursal)
        {
            try
            {
                // www.lineatienda.com/services.php/personales/asignacionpersonal/per/8/suc/1
                AsignacionPersonal datos = await webService.GET<AsignacionPersonal>("personales", String.Format("asignacionpersonal/per/{0}/suc/{1}", idPersonal, idSucursal));
                asignacionPersonal = datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<ConfiguracionGeneral> getConfiGeneral()
        {
            try
            {
                // www.lineatienda.com/services.php/configeneral
                List<ConfiguracionGeneral> list = await webService.GET<List<ConfiguracionGeneral>>("configeneral");
                return list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        // TIPOS DE DOCUMENTO       www.lineatienda.com/services.php/tipodoc21/estado/1
        // ALMACENES                www.lineatienda.com/services.php/personalalmacenes/per/8/suc/1
        // ASIGNACION PERSONAL      www.lineatienda.com/services.php/asignarpuntoventas/sucursal/1/personal/8
        // CAJA SECION              www.lineatienda.com/services.php/cajasesion/idasignarcaja/8








    }
}
