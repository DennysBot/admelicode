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
    public class PersonalModel
    {
        public static bool logged { get; set; }
        public static Personal personal { get; set; }

        private LocationModel locationModel = new LocationModel();
        private WebService webService = new WebService();

        /**
         *  Login del usuario
         *      Usuario y Contraseña
         * */
        public async Task loginPersonal(string usuario, string password)
        {
            try
            {
                Personal personal = new Personal();
                personal.usuario = usuario;
                personal.password = password;

                List<Personal> user = await webService.POST<Personal, List<Personal>>("personal", "buscar", personal);
                if (user.Count == 0)
                {
                    throw new Exception("El nombre de usuario o contraseña es incorrecta!!");
                }
                PersonalModel.personal = user[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> guardar<T>(T param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/personal/guardar
                return await webService.POST<T, Response>("personal", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> usuarioActualizar<T>(T param)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/personal/cambiar/datos
                return await webService.POST<T, Response>("personal", "actualizar/datos", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> usuarioCambiar<T>(T param)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/personal/cambiar/datos
                return await webService.POST<T, Response>("personal", "cambiar/datos", param);
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
                // localhost:8080/admeli/xcore2/xcore/services.php/personal/modificar
                return await webService.POST<T, Response>("personal", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> anular<T>(T param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/personal/anular
                return await webService.POST<T, Response>("personal", "anular", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> eliminar(Personal param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/personal/eliminar
                return await webService.POST<Personal, Response>("personal", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void cambiarClave()
        {

        }

        //personalactualizarpermisos
        public async Task<Response> Asignacion(Responsabilidades param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/personal/eliminar
                return await webService.POST<Responsabilidades, Response>("personalactualizarpermisos", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<RootObject<Personal>> listar(int page, int items)
        {
            try
            {
                RootObject<Personal> personales = await webService.GET<RootObject<Personal>>("personales", String.Format("estado/{0}/{1}", page, items));
                return personales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Personal>> buscarLike(string like, int page, int items)
        {
            try
            {
                /// localhost:8080/admeli/xcore/services.php/buscarpersonales/nombre/perezd/1/30
                RootObject<Personal> personales = await webService.GET<RootObject<Personal>>("buscarpersonales", String.Format("nombre/{0}/{1}/{2}", like, page, items));
                return personales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Personal>> listarPersonalCompras(int idSucursal)
        {
            try
            {
                List<Personal> list = await webService.GET<List<Personal>>("listarpersonalcompras", String.Format("sucursal/{0}", idSucursal));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Personal>> listarPersonalAlmacen(int idSucursal)
        {
            try
            {
                // www.lineatienda.com/services.php/listarpersonalalmacen/sucursal/0
                List<Personal> list = await webService.GET<List<Personal>>("listarpersonalalmacen", String.Format("sucursal/{0}", idSucursal));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Asignacion> asignar(int idPersonal, int idSucursal)
        {
            try
            {
                // www.lineatienda.com/services.php/personales/asignacionpersonal/per/1/suc/1
                Asignacion list = await webService.GET<Asignacion>("personales/asignacionpersonal", String.Format("per/{0}/suc/{1}", idPersonal, idSucursal));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Permisos> > listarPuntosByIdSucursal(int idSucursal, int idPersonal)
        {
            try
            {
                // www.lineatienda.com/services.php/listarareasporsucursal/ids/1/personal/0
                List<Permisos> list = await webService.GET<List<Permisos>>("listarareasporsucursal/ids", String.Format("{0}/personal/{1}", idSucursal, idPersonal));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
