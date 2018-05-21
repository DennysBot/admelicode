using Entidad;
using Entidad.Configuracion;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admeli.CajaBox.Nuevo
{
    public partial class FormCuentaCobrar : Form
    {
        private CajaModel cajaModel = new CajaModel();
        private CobroModel cobroModel = new CobroModel();
        private List<Cobro> listaCobros = new List<Cobro>();
        private List<DetalleCobro> listaDetalleCobros = new List<DetalleCobro>();
        private DatoCuentaCobrar currentCuentaCobrar;
        private Cobro currentCobro;
        private DetalleCobro currentDetalleCobro;
        private int currentIdCliente;
        private decimal currentTotal;
        private decimal currentPagado;
        
        public FormCuentaCobrar()
        {
            InitializeComponent();
        }
        
        public FormCuentaCobrar(DatoCuentaCobrar currentCuentaCobrar)
        {
            InitializeComponent();
            this.currentCuentaCobrar = currentCuentaCobrar;
            this.currentIdCliente = currentCuentaCobrar.idCliente;
            this.lbNombreCliente.Text = currentCuentaCobrar.nombreCliente;
            this.chkMostrarTodos.Checked = false;
            this.reLoad();
        }

        private void reLoad()
        {
            loadState(false);
            this.darFormatoDecimales();
            cargarCuentasCobrar();
            
            //Calcular totales y saldo pendientes

            loadState(true);
        }

        private void darFormatoDecimales()
        {
            try
            {
                dgvCobros.Columns["dataGridViewTextBoxColumn3"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;
                dgvCobros.Columns["montoPagarDataGridViewTextBoxColumn"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;
                dgvDetalleCobro.Columns["montoDataGridViewTextBoxColumn"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;
                dgvDetalleCobro.Columns["montoInteresDataGridViewTextBoxColumn"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;

                dgvCobros.Columns["dataGridViewTextBoxColumn3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvCobros.Columns["montoPagarDataGridViewTextBoxColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDetalleCobro.Columns["montoDataGridViewTextBoxColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDetalleCobro.Columns["montoInteresDataGridViewTextBoxColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            catch
            {
                MessageBox.Show("Error al dar formato a los DataGrid", "Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarCuentasCobrar()
        {
            try
            {
                //Cargar Las Cuentas por cobrar del Cliente
                if (chkMostrarTodos.Checked)
                {
                    listaCobros = await cobroModel.cobrosCliente(currentIdCliente, ConfigModel.sucursal.idSucursal, 1);
                }
                else
                {
                    listaCobros = await cobroModel.cobrosCliente(currentIdCliente, ConfigModel.sucursal.idSucursal, 0);
                }
                cobroBindingSource.DataSource = listaCobros;
                dgvCobros.Refresh();

                if (listaCobros.Count == 0) {
                    detalleCobroBindingSource.DataSource = null;
                    dgvDetalleCobro.Refresh();
                    return;
                };
                
                executeDetalles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: No se pudo cargar las Cuentas por Cobrar" + ex.Message, "Cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void loadState(bool state)
        {
            if (!state)
            {
                progressbActual.Visible = true;
                progressbActual.Style = ProgressBarStyle.Marquee;
                
            }
            else
            {
                progressbActual.Visible = false;
                progressbActual.Style = ProgressBarStyle.Blocks;
                
            }
            panelBody.Enabled = state;
            panelFooder.Enabled = state;
        }
        private void FormCuentaCobrar_Load(object sender, EventArgs e)
        {

        }

        private void chkMostrarTodos_OnChange(object sender, EventArgs e)
        {
            reLoad();
        }

        private void dgvCobros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Mostrar Detalles del Cobro en el panel Izquierdo
            executeDetalles();
        }
        private async void executeDetalles()
        {
            // Verificando la existencia de datos en el datagridview
            if (dgvCobros.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            int index = dgvCobros.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idCobro= Convert.ToInt32(dgvCobros.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview
            currentCobro = listaCobros.Find(x => x.idCobro == idCobro); //Buscando el registro especifico en la lista de registros

            //Mostrar Detalles del Cobro
            listaDetalleCobros = await cobroModel.detalleCobro(currentCobro.idCobro);

            // Mostrando el formulario de modificacion
            detalleCobroBindingSource.DataSource = listaDetalleCobros;
            dgvDetalleCobro.Refresh();

            //Colocar el total para cobrar
            lbTotal.Text = currentCobro.montoPagar;
            currentTotal = decimal.Parse(currentCobro.montoPagar);
            currentPagado = 0;
            //Sumar los pagos detalle
            for(int i = 0; i < listaDetalleCobros.Count; i++)
            {
                if (listaDetalleCobros[i].estado == 1) {
                    currentPagado += listaDetalleCobros[i].monto;
                }
                
            }
            lbSaldo.Text = (currentTotal - currentPagado).ToString();
        }

        private void btnNuevoDetalleCobro_Click(object sender, EventArgs e)
        {
            //Verificar que se haya seleccionado un regitro
            if (dgvCobros.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Nuevo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //Verificar que se inicio caja
            if (!ConfigModel.cajaIniciada)
            {
                MessageBox.Show("Ud. no inició caja", "Nuevo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //Nuevo Detalle Cobro
            FormDetalleCobroNuevo formDetalleCobroNuevo = new FormDetalleCobroNuevo(currentCobro);
            formDetalleCobroNuevo.ShowDialog();
            reLoad();
        }

        private void btnEliminarDetalleCobro_Click(object sender, EventArgs e)
        {
            //Verificar que se haya seleccionado un registro de CobroDetalle
            if (dgvDetalleCobro.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Anular", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //Verificar que se inicio caja
            if (!ConfigModel.cajaIniciada)
            {
                MessageBox.Show("Ud. no inició caja", "Anular", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            //Anular CobroDetalle
            anularCobroDetalle();
            reLoad();
        }

        private async void anularCobroDetalle()
        {
            try
            {
                //Verificar que la caja con la que se realizo el cobro sea la misma que la actual
                int index = dgvDetalleCobro.CurrentRow.Index; 
                int idDetalleCobro = Convert.ToInt32(dgvDetalleCobro.Rows[index].Cells[0].Value); 
                currentDetalleCobro = listaDetalleCobros.Find(x => x.idDetalleCobro == idDetalleCobro);
                if (currentDetalleCobro.idCajaSesion != ConfigModel.cajaSesion.idCajaSesion)
                {
                    MessageBox.Show("Error: Este ingreso lo realizó con otra caja y no podrá ser anulado", "Anular", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Verificar que haya dinero suficiente para hacer el procedimiento
                //http://localhost:8085/ad_meli/xcore/services.php/cierrecajaingresomenosegreso/mediopago/1/cajasesion/11                   
                
                //List<Moneda> monedas = await cajaModel.cierreCajaIngresoMenosEgreso(mediosDePagos[0].idMedioPago, ConfigModel.cajaSesion.idCajaSesion);
                //if (monedas[0].total < double.Parse(textMonto.Text))
                //{
                //    MessageBox.Show("No Hay dinero suficiente en la caja", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                //Anular Cobro Detalle
                Response response = await cobroModel.anularCobroDetalle(currentDetalleCobro);

                MessageBox.Show(response.msj, "Anular", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Anular", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
