using Entidad;
using Modelo.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class AlternativaModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardar(Alternativa param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/alternativa/guardar
                return await webService.POST<Alternativa,Response>("alternativa", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(Alternativa param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/alternativa/modificar
                return await webService.POST<Alternativa,Response>("alternativa", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> desactivar(Alternativa param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/alternativa/desactivar
                return await webService.POST<Alternativa,Response>("alternativa", "desactivar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> eliminar(Alternativa param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/alternativa/eliminar
                return await webService.POST<Alternativa,Response>("alternativa", "eliminar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Alternativa>> alternativas(int idVariante)
        {
            try
            {
                // localhost/admeli/xcore/services.php/variante/1/alternativas
                List<Alternativa> list = await webService.GET<List<Alternativa>>("variante", String.Format("{0}/alternativas", idVariante));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<AlternativaCombinacion>> alternativaCombinacion(int idProducto, int idAlmacen)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/malterna/1/1
                List<AlternativaCombinacion> list = await webService.GET<List<AlternativaCombinacion>>("malterna", String.Format("{0}/{1}", idProducto, idAlmacen));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<AlternativaCombinacion>> generarCombinacionAlternativa(int idProducto)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/malterna/1
                List<AlternativaCombinacion> list = await webService.GET<List<AlternativaCombinacion>>("malterna", String.Format("{0}", idProducto));
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //verificarcodigosku
        public async Task<List<AlternativaCombinacion>> verificarCodigoSKU(string  codigo, int idCombinacionAlternativa)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/verificarcodigosku/codigo/VHSJHBBUHB/combinacionalternativa/1
                List<AlternativaCombinacion> responseData = await webService.GET<List<AlternativaCombinacion>>("verificarcodigosku", String.Format("codigo/{0}/combinacionalternativa/{1}", codigo, idCombinacionAlternativa));
                return responseData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificarAlternativa(AlternativaCombinacion param)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/calternativa/modificar
                Response response = await webService.POST<AlternativaCombinacion,Response>("calternativa", "modificar", param);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificarCombinacionAlternativa(AlternativaCombinacion param)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/casalmacen/modificar
                Response response = await webService.POST<AlternativaCombinacion, Response>("casalmacen", "modificar", param);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<AlternativaCombinacion>> cAlternativa31(int idPresentacion)
        {
            try
            {
                // localhost:8080/admeli/xcore/services.php/calternativa31/prod/1
                List<AlternativaCombinacion> response = await webService.GET<List<AlternativaCombinacion>>("calternativa31", String.Format("prod/{0}", idPresentacion));
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
