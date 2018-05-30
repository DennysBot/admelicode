using Entidad;
using Modelo.Recursos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class CompraModel
    {
        private WebService webService = new WebService();


        public async Task<Response> ralizarCompra(compraTotal total)
        {
            try
            {
                
                // localhost:8080/admeli/xcore2/xcore/services.php/compra/guardartotal
                return await webService.POST<compraTotal,Response>("compra", "guardartotal", total);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Response> guardar(Producto param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/compra/guardar
                return await webService.POST<Producto,Response>("compra", "guardar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> modificar(Compra param)
        {
            try
            {
                // localhost:8080/admeli/xcore2/xcore/services.php/compra/modificar
                return await webService.POST<Compra,Response>("compra", "modificar", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response> anular(Compra param)
        {
            try
            {
                // localhost/admeli/xcore/services.php/compra/anular
                return await webService.POST<Compra,Response>("compra", "anular", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List< DatoNotaEntradaC>> ListaStockSucursal(DatosParaNotaEntrada param)
        {
            try
            {
                // localhost/admeli/xcore/services.php/compra/anular
                return await webService.POST<DatosParaNotaEntrada,List<DatoNotaEntradaC>>("listaralmacenesstockproductocombinacion", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<RootObject<Compra>> getByPersonalEstado(int idSucursal, int idPersonal, string idEstado, int page, int items)
        {
            try
            {
                RootObject<Compra> ordenCompra = await webService.GET<RootObject<Compra>>("compras", String.Format("sucursal/{0}/personal/{1}/estado/{2}/{3}/{4}", idSucursal,idPersonal,idEstado, page, items));
                return ordenCompra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Compra>> comprasUltimas(int idPersonal, int idSucursal, int todos, int gerente)
        {
            try
            {
                // www.lineatienda.com/services.php/comprasultimas/3/1/1/1
                List<Compra> comprasultimas = await webService.GET<List<Compra>>("comprasultimas", String.Format("{0}/{1}/{2}/{3}", idPersonal, idSucursal, todos, gerente));
                return comprasultimas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<CompraModificar>> dCompras(int idCompra)
        {
            try
            {
                // www.lineatienda.com/services.php//dcompras/:id
                List<CompraModificar> comprasultimas = await webService.GET< List<CompraModificar >> ("dcompras", String.Format("{0}", idCompra));
                return comprasultimas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CompraRecuperar>> Compras(int idCompra)
        {
            try
            {
                // www.lineatienda.com/services.php//compras/:id
                List<CompraRecuperar> comprasultimas = await webService.GET<List<CompraRecuperar>>("compras", String.Format("{0}", idCompra));
                return comprasultimas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Compra>> comprasAll()
        {
            try
            {
                // www.lineatienda.com/services.php/comprasultimas/3/1/1/1
                List<Compra> comprasAll = await webService.GET<List<Compra>>("compras/todos");
                return comprasAll;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<CompraNEntrada>> comprasSinNota(int idSucursal)
        {
            try
            {
                // www.lineatienda.com/services.php/comprassinnotaentrada/sucursal/1
                List<CompraNEntrada> comprasAll = await webService.GET<List<CompraNEntrada>>("comprassinnotaentrada", String.Format("sucursal/{0}", idSucursal));
                return comprasAll;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






    }
}
