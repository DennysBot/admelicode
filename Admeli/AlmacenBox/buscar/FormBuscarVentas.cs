using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.AlmacenBox.fecha;
using Admeli.AlmacenBox.Nuevo;
using Admeli.Componentes;
using Entidad;
using Entidad.Configuracion;
using Modelo;


namespace Admeli.AlmacenBox.buscar
{
    public partial class FormBuscarVentas : Form
    {


        // servicios necesarios

        AlmacenModel AlmacenModel = new AlmacenModel();
        ProductoModel productoModel = new ProductoModel();
        FechaModel fechaModel = new FechaModel();
        NotaSalidaModel NotaSalidaModel = new NotaSalidaModel();
        CompraModel compraModel = new CompraModel();        // objetos que cargan a un inicio

        private List<VentasNSalida> listVentasNSalida { get; set; }

        // entidadades auxiliares

     
        private string formato { get; set; }
        private int nroDecimales = ConfigModel.configuracionGeneral.numeroDecimales;
        private FechaSistema fechaSistema { get; set; }
        public  VentasNSalida currentVenta { get; set; }


        //objetos en tiempo real
        
     

        public FormBuscarVentas()
        {
            InitializeComponent();
            
            formato = "{0:n" + nroDecimales + "}";
           


        }


       

        #region=======================metodos de apoyo
        private string darformato(object dato)
        {
            return string.Format(CultureInfo.GetCultureInfo("en-US"), this.formato, dato);
        }
        #endregion
        #region ================================ Root Load ================================

        private void FormNotaSalidaNew_Load(object sender, EventArgs e)
        {
            cargarVentas();

        }
        private void reLoad()
        {
            
        }

        #endregion

        #region ============================== Load ==============================


        private async void cargarVentas()
        {
            try
            {
                listVentasNSalida = await NotaSalidaModel.VentasSinNotasSalida(ConfigModel.sucursal.idSucursal);
                ventasNSalidaBindingSource.DataSource = listVentasNSalida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        #endregion

        #region=========================estados=================  
        private void loadState(bool state)
        {
            appLoadState(state);
            this.Enabled = true;
        }

        public void appLoadState(bool state)
        {
            if (state)
            {
                progressStatus.Style = ProgressBarStyle.Marquee;
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                progressStatus.Style = ProgressBarStyle.Blocks;
                this.Cursor = Cursors.Default;
            }
        }
        #endregion=========================estados=====================

     

      
        private void dgvNotaSalida_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            entrarGuiaremision();

        }

        private void entrarGuiaremision()
        {
            if (dgvVentas.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "nota salida", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dgvVentas.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idVenta = Convert.ToInt32(dgvVentas.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview

            currentVenta = listVentasNSalida.Find(x => x.idVenta== idVenta);         
            this.Close();
        }

        private void txtNroCompra_TextChanged(object sender, EventArgs e)
        {
            BindingList<VentasNSalida> filtered = new BindingList<VentasNSalida>(listVentasNSalida.Where(obj => obj.numeroDocumento.Contains(txtNroVenta.Text)).ToList());
            ventasNSalidaBindingSource.DataSource = filtered;
            dgvVentas.Update();
        }

       
        //nombre cliente
        private void txtNombreProveedor_TextChanged(object sender, EventArgs e)
        {
            BindingList<VentasNSalida> filtered = new BindingList<VentasNSalida>(listVentasNSalida.Where(obj => obj.nombreCliente.ToUpper().Contains(txtNombreCliente.Text.Trim().ToUpper())).ToList());
            ventasNSalidaBindingSource.DataSource = filtered;
            dgvVentas.Update();
        }

        private void txtNroDocumento_TextChanged(object sender, EventArgs e)
        {
            BindingList<VentasNSalida> filtered = new BindingList<VentasNSalida>(listVentasNSalida.Where(obj => obj.rucDni.ToUpper().Contains(txtDni.Text.Trim().ToUpper())).ToList());
            ventasNSalidaBindingSource.DataSource = filtered;
            dgvVentas.Update();
        }

        private void btnfechaFacturacion_Click(object sender, EventArgs e)
        {
            FormFecha fecha = new FormFecha();
            fecha.ShowDialog();
            BindingList<VentasNSalida> filtered = new BindingList<VentasNSalida>(listVentasNSalida.Where(obj => obj.FechaVentaS >= fecha.desde && obj.FechaVentaS <= fecha.hasta).ToList());
            ventasNSalidaBindingSource.DataSource = filtered;
            dgvVentas.Update();
        }

        private void btnFechaPago_Click(object sender, EventArgs e)
        {
            FormFecha fecha = new FormFecha();
            fecha.ShowDialog();
            BindingList<VentasNSalida> filtered = new BindingList<VentasNSalida>(listVentasNSalida.Where(obj => obj.FechaPagoS >= fecha.desde && obj.FechaPagoS <= fecha.hasta).ToList());
            ventasNSalidaBindingSource.DataSource = filtered;
            dgvVentas.Update();
        }
    }
}
