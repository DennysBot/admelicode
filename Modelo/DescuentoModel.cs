using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class DescuentoModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar(DescuentoEnviar param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/descuento/guardar
                return await webService.POST<DescuentoEnviar, Response>("descuento", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Response> guardarTodo(Descuento param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/descuento/guardar/todo
                return await webService.POST<Descuento, Response>("descuento", "guardar/todo", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //descuento/editar/todo

        public async Task<Response> modificar(DescuentoEnviar param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/descuento/modificar
                return await webService.POST<DescuentoEnviar,Response>("descuento", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Response> modificarTodo(Descuento param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/descuento/editar/todo
                return await webService.POST<Descuento, Response>("descuento", "editar/todo", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DescuentoReceive>> descuentoTotalALaFechaGrupo(DescuentoSubmit param)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/productos/descuentototalalafechagrupo
                return await webService.POST<DescuentoSubmit, List<DescuentoReceive>>("productos", "descuentototalalafechagrupo", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DescuentoReceive>> descuentototalentrefechasgrupo(DescuentoSubmit param)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/productos/descuentototalentrefechasgrupo
                return await webService.POST<DescuentoSubmit, List<DescuentoReceive>>("productos", "descuentototalentrefechasgrupo", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // descuentoTotalALaFecha
        public async Task<DescuentoProductoReceive> descuentoTotalALaFecha(DescuentoProductoSubmit param)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/productos/descuentototalalafechagrupo
                return await webService.POST<DescuentoProductoSubmit, DescuentoProductoReceive>("productos", "descuentototalalafecha", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DescuentoProductoReceive> descuentototalentrefechas(DescuentoProductoSubmit param)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/productos/descuentototalalafechagrupo
                return await webService.POST<DescuentoProductoSubmit, DescuentoProductoReceive>("productos", "descuentototalentrefechas", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<Response> desactivar(Descuento param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/desccascasuento/elicascsminar
                return await webService.POST<Descuento,Response>("catecascascasgoria", "dascascasesactivar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(Descuento param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/descuento/eliminar
                return await webService.POST<Descuento,Response>("descuento", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Descuento>> descuentos(int idPresentacion)
        {
            try
            {
                // localhost/admeli/xcore/services.php/descuento/producto/21
                List<Descuento> descuentos = await webService.GET<List<Descuento>>("descuento", String.Format("presentacion/{0}", idPresentacion));
                return descuentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ObjetosDescuentosOfertas> descuentoofertacodigo(int pos, int tam)
        {
            try
            {
                // localhost/admeli/xcore/services.php/descuentoofertacodigo/1/15
                ObjetosDescuentosOfertas descuentos = await webService.GET<ObjetosDescuentosOfertas>("descuentoofertacodigo", String.Format("{0}/{1}", pos,tam));
                return descuentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
