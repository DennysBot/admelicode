using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;
using Entidad;
using Admeli.Componentes;
using Admeli.CajaBox.Nuevo;

namespace Admeli.CajaBox
{
    public partial class UCCuentaCobrar : UserControl
    {
        private FormPrincipal formPrincipal;
        public bool lisenerKeyEvents { get; set; }
        private CobroModel cuentascobrarModel = new CobroModel();
        private Paginacion paginacion;
        private List<DatoCuentaCobrar> listaCuentaCobrar { get; set; }
        private DatoCuentaCobrar currentCuentaCobrar { get; set; }
        private DatosDescuentosOfertas currentDatosCuentaCobrar { get; set; }
        private DatosdeCuentasCobrar datosconvertidoscuentascobrar { get; set; }

      
        #region =================================== CONSTRUCTOR ===================================
        public UCCuentaCobrar()
        {
            InitializeComponent();
            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));

        }

        public UCCuentaCobrar(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;
            //lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
            this.reLoad();
            // Preparando para los eventos de teclado
            this.ParentChanged += ParentChange; // Evetno que se dispara cuando el padre cambia // Este eveto se usa para desactivar lisener key events de este modulo
            if (TopLevelControl is Form) // Escuchando los eventos del formulario padre
            {
                (TopLevelControl as Form).KeyPreview = true;
                TopLevelControl.KeyUp += TopLevelControl_KeyUp;
            }
        }
        #endregion

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

        #region ================================ PAINT AND DECORATION ================================

        private void decorationDataGridView()
        {
            //Verificando la existencia de datos en el datagridview
            if (dataGridView.Rows.Count == 0) return;

            //foreach (DataGridViewRow row in dataGridView.Rows)
            //{
            //    string codigo = Convert.ToString(row.Cells[0].Value); // obteniedo el idCategoria del datagridview

            //    DatosDescuentosOfertas dDescuentosOfertas = listaCuentaCobrar.Find(x => x.idCliente == codigo); // Buscando la categoria en las lista de categorias
            //    if (dDescuentosOfertas != null)
            //    {
            //        if (dDescuentosOfertas.estado == 0)
            //        {
            //            dataGridView.ClearSelection();
            //            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 224);
            //            row.DefaultCellStyle.ForeColor = Color.FromArgb(250, 5, 73);
            //        }
            //    }
            //}


        }
        #endregion

        #region =========================== Estados ===========================
        private void loadState(bool state)
        {
            formPrincipal.appLoadState(state);
            panelNavigation.Enabled = !state;
            dataGridView.Enabled = !state;
            panel1.Enabled = !state;
            panelCrud.Enabled = !state;
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

        #region ======================= Loads =======================
        private void cargarComponentes()
        {
            // Cargando el combobox ce estados
            DataTable table = new DataTable();
            table.Columns.Add("idEstado", typeof(string));
            table.Columns.Add("estado", typeof(string));

            table.Rows.Add("todos", "Todos los estados");
            table.Rows.Add("0", "Anulados");
            table.Rows.Add("1", "Activos");

            //cbxEstados.DataSource = table;
            //cbxEstados.DisplayMember = "estado";
            //cbxEstados.ValueMember = "idEstado";
            //cbxEstados.ComboBox.SelectedIndex = 0;
        }

        private async void cargarRegistros()
        {
            loadState(true);
            try
            {
                datosconvertidoscuentascobrar = new DatosdeCuentasCobrar();

                if (chkTodoCliente.Checked)
                {
                    datosconvertidoscuentascobrar = await cuentascobrarModel.Cuentasporcobrar(ConfigModel.sucursal.idSucursal, 1, paginacion.currentPage, paginacion.speed);
                }
                else
                {
                    datosconvertidoscuentascobrar = await cuentascobrarModel.Cuentasporcobrar(ConfigModel.sucursal.idSucursal, 0, paginacion.currentPage, paginacion.speed);
                }
                if (datosconvertidoscuentascobrar.nro_registros == 0)
                {
                    datoCuentaCobrarBindingSource.DataSource = null;
                    dataGridView.Refresh();
                    return;
                }

                paginacion.itemsCount = datosconvertidoscuentascobrar.nro_registros;
                paginacion.reload();
                listaCuentaCobrar = datosconvertidoscuentascobrar.datos;
                
                datoCuentaCobrarBindingSource.DataSource = listaCuentaCobrar;
                dataGridView.Refresh();

                // Mostrando la paginacion
                mostrarPaginado();

                // formato de celdas
                decorationDataGridView();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.formPrincipal.cargarDatosAsideRight();
                loadState(false);
            }

        }
        #endregion
        
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            cargarRegistros();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            executeModificar();
        }

        private void dgvCuentasCobrar_DoubleClick(object sender, EventArgs e)
        {
            //Desacativado, se usa otro evento
            //Modificar CuetaPorCobrar
            //FormCuentaCobrar formCuentaCobrar = new FormCuentaCobrar();
            //formCuentaCobrar.Show();
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
        private async void buscarRegistros(string nombreCliente)
        {
            loadState(true);
            try
            {
                datosconvertidoscuentascobrar = new DatosdeCuentasCobrar();
                if (chkTodoCliente.Checked)
                {
                    datosconvertidoscuentascobrar = await cuentascobrarModel.buscarClienteCuentasPorCobrar(nombreCliente, ConfigModel.sucursal.idSucursal, 1, paginacion.currentPage, paginacion.speed);
                }
                else
                {
                    datosconvertidoscuentascobrar = await cuentascobrarModel.buscarClienteCuentasPorCobrar(nombreCliente, ConfigModel.sucursal.idSucursal, 0, paginacion.currentPage, paginacion.speed);
                }
                
                if (datosconvertidoscuentascobrar.nro_registros == 0) return;

                paginacion.itemsCount = datosconvertidoscuentascobrar.nro_registros;
                paginacion.reload();
                listaCuentaCobrar = datosconvertidoscuentascobrar.datos;

                datoCuentaCobrarBindingSource.DataSource = listaCuentaCobrar;
                dataGridView.Refresh();

                // Mostrando la paginacion
                mostrarPaginado();

                // formato de celdas
                decorationDataGridView();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.formPrincipal.cargarDatosAsideRight();
                loadState(false);
            }
        }

        private void dgvCuentasCobrar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            executeModificar();
            
        }
        private void executeModificar()
        {
            // Verificando la existencia de datos en el datagridview
            if (dgvCuentasCobrar.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dgvCuentasCobrar.CurrentRow.Index; // Identificando la fila actual del dgv
            int idCliente = Convert.ToInt32(dgvCuentasCobrar.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview

            currentCuentaCobrar = listaCuentaCobrar.Find(x => x.idCliente == idCliente); //Buscando el registro especifico en la lista de registros

            // Mostrando el formulario de modificacion
            FormCuentaCobrar formCuentaCobrar = new FormCuentaCobrar(currentCuentaCobrar);
            formCuentaCobrar.ShowDialog();
        }

        #region ======================== KEYBOARD ========================
        // Evento que se dispara cuando el padre cambia
        private void ParentChange(object sender, EventArgs e)
        {
            // cambiar la propiedad de lisenerKeyEvents de este modulo
            if (lisenerKeyEvents) lisenerKeyEvents = false;
        }

        // Escuchando los Eventos de teclado
        private void TopLevelControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (!lisenerKeyEvents) return;
            switch (e.KeyCode)
            {
                case Keys.F4:
                    executeModificar();
                    break;
                case Keys.F5:
                    cargarRegistros();
                    break;
                default:
                    break;
            }
        }
        #endregion

        private void chkTodoCliente_OnChange(object sender, EventArgs e)
        {
            cargarRegistros();
        }
    }
}
