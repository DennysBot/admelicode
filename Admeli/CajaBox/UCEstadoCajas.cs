using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidad.Configuracion;
using Modelo;
using Entidad;

namespace Admeli.CajaBox
{
    public partial class UCEstadoCajas : UserControl
    {
        private FormPrincipal formPrincipal;
        private MonedaModel monedaModel = new MonedaModel();
        public UCEstadoCajas()
        {
            InitializeComponent();
        }
        public UCEstadoCajas(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;
            this.reLoad();

        }
        public void reLoad()
        {
            cargarSucursales();
            cargarMonedas();
        }
        
        private async void cargarMonedas()
        {
            try
            {
                List<Moneda> listMoneda = await monedaModel.monedas();
                monedaBindingSource.DataSource = listMoneda;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar Monedas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
}
        private void cargarSucursales()
        {
            try
            {
                List<Sucursal> listSucursales = ConfigModel.listSucursales;
                sucursalBindingSource.DataSource = listSucursales;
                cbxMoneda.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar Sucursales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {

        }
    }
}
