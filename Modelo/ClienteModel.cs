using Entidad;
using Newtonsoft.Json;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ClienteModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar(ClienteG param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/cliente/guardar
                return await webService.POST<ClienteG, Response>("cliente", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> modificar(Cliente param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.phpcliente/modificar
                return await webService.POST<Cliente, Response>("cliente", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminar()
        {

        }

        public async Task<RootObject<Cliente>> clientes(int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/clientes/estado/1/100
                RootObject<Cliente> clientes = await webService.GET<RootObject<Cliente>>("clientes", String.Format("estado/{0}/{1}", page, items));
                return clientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RootObject<Cliente>> buscarClientesLike(string like, int page, int items)
        {
            try
            {
                // www.lineatienda.com/services.php/buscarclienteslike/nombre/Jhon/1/100
                RootObject<Cliente> clientes = await webService.GET<RootObject<Cliente>>("buscarclienteslike", String.Format("nombre/{0}/{1}/{2}",like, page, items));
                return clientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      





        public Task<Response> eliminar(Cliente currentCliente)
        {
            throw new NotImplementedException();
        }

        public Task<Response> desactivar(Cliente currentCliente)
        {
            throw new NotImplementedException();
        }
        public async Task<List<GrupoCliente>> listarGrupoClienteIdGCNombreByActivos()
        {
            try
            {
                // www.lineatienda.com/services.php/gclientes21
                List<GrupoCliente> clientes = await webService.GET<List<GrupoCliente>>("gclientes21");
                return clientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
         public async Task<List<GrupoClienteC>> documentoidentificaciones()
        {
            try
            {
                // www.lineatienda.com/services.php/gclientes21
                List<GrupoClienteC> clientes = await webService.GET<List<GrupoClienteC>>("gclientes21", "");
                return clientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///buscarclientes/estado/:est/tipo/:td

        public async Task<List<Cliente>> ListarClientesActivos()
        {
            try
            {
                // www.lineatienda.com/services.php/buscarclienteslike/nombre/Jhon/1/100
                List<Cliente> clientes = await webService.GET<List<Cliente>>("buscarclientes", String.Format("estado/1/tipo/Todos"));
                return clientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



       
    }
}
