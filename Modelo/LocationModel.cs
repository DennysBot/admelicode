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
    public class LocationModel
    {
        private WebService webService = new WebService();

        public async Task<Response> guardarUbigeo(UbicacionGeografica param)
        {
            try
            {
                // www.admeli.com/demo2/services.php/ubigeo
                Response response = await webService.POST<UbicacionGeografica,Response>("ubigeo",param);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> guardarUbigeo(UbicacionGeograficaG param)
        {
            try
            {
                // www.admeli.com/demo2/services.php/ubigeo
                Response response = await webService.POST<UbicacionGeograficaG, Response>("ubigeo", param);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificar()
        {

        }

        public void eliminar()
        {

        }

        public async Task<List<Nivel1>>  nivel1(int idPais)
        {
            try
            {
                // www.lineatienda.com/services.php/pais/21/nivel1
                List<Nivel1> data = await webService.GET<List<Nivel1>>("pais", String.Format("{0}/nivel1", idPais));
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Nivel2>> nivel2(int idNivel1)
        {
            try
            {
                // www.lineatienda.com/services.php/nivel1/482/nivel2
                List<Nivel2> data = await webService.GET<List<Nivel2>>("nivel1", String.Format("{0}/nivel2", idNivel1));
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Nivel3>> nivel3(int idNivel2)
        {
            try
            {
                // www.lineatienda.com/services.php/nivel2/66153/nivel3
                List<Nivel3> data = await webService.GET<List<Nivel3>>("nivel2", String.Format("{0}/nivel3", idNivel2));
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Nivel4>> nivel4(int idNivel3)
        {
            try
            {
                // www.lineatienda.com/services.php/nivel2/66153/nivel4
                List<Nivel4> data = await webService.GET<List<Nivel4>>("nivel3", String.Format("{0}/nivel4", idNivel3));
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<LabelUbicacion>> labelUbicacion(int idPais)
        {
            try
            {
                // www.lineatienda.com/services.php/lubicacion/pais/26
                List<LabelUbicacion> data = await webService.GET<List<LabelUbicacion>>("lubicacion",String.Format("pais/{0}",idPais));
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UbicacionGeografica> ubigeoActual(int idUbicacionGeografica)
        {
            try
            {
                // www.lineatienda.com/services.php/ubigeoAtrib/2
                List<UbicacionGeografica> data = await webService.GET<List<UbicacionGeografica>>("ubigeoAtrib",String.Format("{0}", idUbicacionGeografica));
                return data[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Pais>> paises()
        {
            try
            {
                // www.lineatienda.com/services.php/pais
                List<Pais> painsesData = await webService.GET<List<Pais>>("pais");
                return painsesData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

    }
}
