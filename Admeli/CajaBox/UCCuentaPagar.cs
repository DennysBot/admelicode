using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;
using Admeli.Componentes;
using Entidad;
using Admeli.CajaBox.Nuevo;

namespace Admeli.CajaBox
{
    public partial class UCCuentaPagar : UserControl
    {
        public bool lisenerKeyEvents { get; set; }
        private FormPrincipal formPrincipal;
        private PagoModel cuentaspagarModel = new PagoModel();
        private Paginacion paginacion;
        private DatoCuentaPagar currentCuentaPagar { get; set; }
        private List<DatoCuentaPagar> listaCuentaPagar { get; set; }
        private DatosdeCuentasPagar datosconvertidoscuentaspagar { get; set; }

        #region =================================== CONSTRUCTOR ===================================
        public UCCuentaPagar()
        {
            InitializeComponent();
            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));

        }

        public UCCuentaPagar(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
            this.reLoad();

        }
        #endregion

        #region ==================================== ROOT LOAD ====================================
        internal void reLoad(bool refreshData = true)
        {
            if (refreshData)
            {
                cargarRegistros();
            }
            lisenerKeyEvents = true; // Active lisener key events
        }
        #endregion

        private async void cargarRegistros()
        {
            loadState(true);
            try
            {
                datosconvertidoscuentaspagar = new DatosdeCuentasPagar();

                if (chkTodoProveedor.Checked)
                {
                    datosconvertidoscuentaspagar = await cuentaspagarModel.Cuentasporpagar(1, paginacion.currentPage, paginacion.speed);
                }
                else
                {
                    //datosconvertidoscuentaspagar = await cuentaspagarModel.Cuentasporpagar(0, paginacion.currentPage, paginacion.speed);
                    datosconvertidoscuentaspagar = await cuentaspagarModel.Cuentasporpagar(0, 1,10);
                }
                if (datosconvertidoscuentaspagar.nro_registros == 0)
                {
                    datoCuentaPagarBindingSource.DataSource = null;
                    dgvCuentasPagar.Refresh();
                    return;
                }

                paginacion.itemsCount = datosconvertidoscuentaspagar.nro_registros;
                paginacion.reload();
                listaCuentaPagar = datosconvertidoscuentaspagar.datos;

                datoCuentaPagarBindingSource.DataSource = listaCuentaPagar;
                dgvCuentasPagar.Refresh();

                // Mostrando la paginacion
                mostrarPaginado();
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.formPrincipal.cargarDatosAsideRight();
                loadState(false);
            }

        }

        #region ===================== Eventos Páginación =====================
        private void mostrarPaginado()
        {
            lblCurrentPage.Text = paginacion.currentPage.ToString();
            lblPageAllItems.Text = String.Format("{0} Registros", paginacion.itemsCount.ToString());
            lblPageCount.Text = paginacion.pageCount.ToString();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (lblCurrentPage.Text != "1")
            {
                paginacion.previousPage();
                cargarRegistros();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (lblCurrentPage.Text != "1")
            {
                paginacion.firstPage();
                cargarRegistros();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (lblPageCount.Text == "0") return;
            if (lblPageCount.Text != lblCurrentPage.Text)
            {
                paginacion.nextPage();
                cargarRegistros();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (lblPageCount.Text == "0") return;
            if (lblPageCount.Text != lblCurrentPage.Text)
            {
                paginacion.lastPage();
                cargarRegistros();
            }
        }

        private void lblSpeedPages_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                paginacion.speed = Convert.ToInt32(lblSpeedPages.Text);
                paginacion.currentPage = 1;
                cargarRegistros();
            }
        }

        private void lblCurrentPage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                paginacion.reloadPage(Convert.ToInt32(lblCurrentPage.Text));
                cargarRegistros();
            }
        }

        private void lblCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private void lblSpeedPages_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }
        #endregion

        #region =========================== Estados ===========================
        private void loadState(bool state)
        {
            formPrincipal.appLoadState(state);
            panelNavigation.Enabled = !state;
            panelCuerpo.Enabled = !state;
            panelTools.Enabled = !state;
            panelCrud.Enabled = !state;
        }

        #endregion

        private async void buscarRegistros(string nombreProveedor)
        {
            loadState(true);
            try
            {
                datosconvertidoscuentaspagar = new DatosdeCuentasPagar();
                if (chkTodoProveedor.Checked)
                {
                    datosconvertidoscuentaspagar = await cuentaspagarModel.buscarProveedorCuentasPorPagar(nombreProveedor,1,paginacion.currentPage,paginacion.speed);
                }
                else
                {
                    datosconvertidoscuentaspagar = await cuentaspagarModel.buscarProveedorCuentasPorPagar(nombreProveedor, 0, paginacion.currentPage, paginacion.speed);
                }

                if (datosconvertidoscuentaspagar.nro_registros == 0) return;

                paginacion.itemsCount = datosconvertidoscuentaspagar.nro_registros;
                paginacion.reload();
                listaCuentaPagar = datosconvertidoscuentaspagar.datos;

                datoCuentaPagarBindingSource.DataSource = listaCuentaPagar;
                dgvCuentasPagar.Refresh();
                // Mostrando la paginacion
                mostrarPaginado();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadState(false);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            cargarRegistros();
        }

        private void textBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Presiono 'Enter'
            if (e.KeyChar == '\r')
            {
                if (textBuscar.Text.Trim() != "")
                {
                    buscarRegistros(textBuscar.Text);
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            executeModificar();
        }

        private void dgvCuentasCobrar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            executeModificar();
        }

        private void executeModificar()
        {
            // Verificando la existencia de datos en el datagridview
            if (dgvCuentasPagar.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dgvCuentasPagar.CurrentRow.Index; // Identificando la fila actual del dgv
            int idProveedor = Convert.ToInt32(dgvCuentasPagar.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview
            
            currentCuentaPagar = listaCuentaPagar.Find(x => x.idProveedor == idProveedor); //Buscando el registro especifico en la lista de registros

            // Mostrando el formulario de modificacion
            FormCuentaPagar formCuentaPagar = new FormCuentaPagar(currentCuentaPagar);
            formCuentaPagar.ShowDialog();
        }

        private void chkTodoProveedor_OnChange(object sender, EventArgs e)
        {
            cargarRegistros();
        }
    }
}
