using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.Componentes;
using Modelo;
using Entidad;
using Admeli.AlmacenBox.Nuevo;
using Admeli.AlmacenBox.buscar;

namespace Admeli.AlmacenBox
{
    public partial class UCGuiaRemision : UserControl
    {
        private SucursalModel sucursalModel = new SucursalModel();
        private AlmacenModel almacenModel = new AlmacenModel();
        private GuiaRemisionModel guiaRemisionModel = new GuiaRemisionModel();

        private GuiaRemision currentGuiaRemision { get; set; }
        private List<GuiaRemision> guiaRemisiones { get; set; }

        private FormPrincipal formPrincipal;
        public bool lisenerKeyEvents { get; set; }

        private List<Sucursal> listaSucursalCargar { get; set; }
        private List<Almacen> listaAlmacen { get; set; }
        private Paginacion paginacion;

        #region ============================= Constructor =============================
        public UCGuiaRemision()
        {
            InitializeComponent();

            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
        }

        public UCGuiaRemision(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;

            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
        } 
        #endregion

        #region ================================= Paint And Decoration =================================
        private void UCGuiaRemision_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panel10, 221, 225, 228);
            drawShape.lineBorder(panel11, 221, 225, 228);
            drawShape.lineBorder(panel12, 221, 225, 228);
        }

        private void decorationDataGridView()
        {
            if (dataGridView.Rows.Count == 0) return;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                int idGuiaRemision = Convert.ToInt32(row.Cells[0].Value); // obteniedo el idCategoria del datagridview

                currentGuiaRemision = guiaRemisiones.Find(x => x.idGuiaRemision == idGuiaRemision); // Buscando la categoria en las lista de categorias
                if (currentGuiaRemision.estado == 0)
                {
                    dataGridView.ClearSelection();
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 224);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(250, 5, 73);
                }
            }
        }
        #endregion

        #region ================================ Root Load ================================
        private void UCGuiaRemision_Load(object sender, EventArgs e)
        {
            this.reLoad();

            // Preparando para los eventos de teclado
            this.ParentChanged += ParentChange; // Evetno que se dispara cuando el padre cambia // Este eveto se usa para desactivar lisener key events de este modulo
            if (TopLevelControl is Form) // Escuchando los eventos del formulario padre
            {
                (TopLevelControl as Form).KeyPreview = true;
                TopLevelControl.KeyUp += TopLevelControl_KeyUp;
            }
        }

        internal void reLoad(bool refreshData = true)
        {
            if (refreshData)
            {
                cargarSucursales();
                cargarAlmacenes();
                cargarTipo();
                cargarRegistros();
            }
            lisenerKeyEvents = true; // Active lisener key events
        }
        #endregion

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
                case Keys.F3:
                    executeNuevo();
                    break;
                case Keys.F4:
                    executeModificar();
                    break;
                case Keys.F5:
                    cargarRegistros();
                    break;
                case Keys.F6:
                    executeEliminar();
                    break;
                case Keys.F7:
                    executeAnular();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region ================================= Loads =================================
        private async void cargarRegistros()
        {
            loadState(true);
            try
            {
                int sucursalId = (cbxSucursales.SelectedIndex == -1) ? ConfigModel.sucursal.idSucursal : Convert.ToInt32(cbxSucursales.SelectedValue);
                int almacenId = (cbxAlmacenes.SelectedIndex == -1) ? ConfigModel.currentIdAlmacen : Convert.ToInt32(cbxAlmacenes.SelectedValue);
                int estado = (cbxEstados.SelectedIndex == -1) ? 0 : Convert.ToInt32(cbxEstados.SelectedValue);

                RootObject<GuiaRemision> rootData = await guiaRemisionModel.notaEntradas(sucursalId, almacenId, estado, paginacion.currentPage, paginacion.speed);
                if (rootData.nro_registros == 0) {

                    paginacion.itemsCount = rootData.nro_registros;
                    guiaRemisionBindingSource.DataSource = null;
                    dataGridView.Refresh();
                    return;

                }
              

                // actualizando datos de páginacón
                paginacion.itemsCount = rootData.nro_registros;
                paginacion.reload();

                // Ingresando
                guiaRemisiones = rootData.datos;
                guiaRemisionBindingSource.DataSource = guiaRemisiones;
                dataGridView.Refresh();

                // Mostrando el páginado
                mostrarPaginado();

                // Formato de celdas
                decorationDataGridView();
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

        private async void cargarSucursales()
        {
            try
            {
                listaSucursalCargar = await sucursalModel.listarSucursalesActivos();
                sucursalBindingSource.DataSource = listaSucursalCargar;
                cbxSucursales.SelectedValue = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarAlmacenes()
        {
            try
            {
                listaAlmacen = new List<Almacen>();
                //almacenBindingSource.DataSource = await almacenModel.almacenesPorSucursales(ConfigModel.sucursal.idSucursal);
                listaAlmacen = await almacenModel.almacenesPorSucursales(0);
                almacenBindingSource.DataSource = listaAlmacen;
                cbxAlmacenes.SelectedValue = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cargarTipo()
        {
            // Cargando el combobox ce estados
            DataTable table = new DataTable();
            table.Columns.Add("idEstado", typeof(string));
            table.Columns.Add("estado", typeof(string));

            table.Rows.Add("3", "Pendiente");
            table.Rows.Add("2", "Revisado");
            table.Rows.Add("1", "Enviado");
            cbxEstados.DataSource = table;
            cbxEstados.DisplayMember = "estado";
            cbxEstados.ValueMember = "idEstado";
        }
        #endregion

        #region =========================== Estados ===========================
        private void loadState(bool state)
        {
            formPrincipal.appLoadState(state);
            panelNavigation.Enabled = !state;
            panelCrud.Enabled = !state;
            panelTools.Enabled = !state;
            dataGridView.Enabled = !state;
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

        #region ==================== CRUD ====================
        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            executeModificar();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            cargarRegistros();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            cargarRegistros();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            executeNuevo();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            executeEliminar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            executeModificar();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            executeAnular();
        }

        private void executeNuevo()
        {
            

            FormBuscarNotaSalida formGuiaRemision = new FormBuscarNotaSalida( );
            formGuiaRemision.ShowDialog();
            //this.reLoad();
        }

        private void executeModificar()
        {
            // Verificando la existencia de datos en el datagridview
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idGuiaRemision = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview

            currentGuiaRemision = guiaRemisiones.Find(x => x.idGuiaRemision == idGuiaRemision); // Buscando la registro especifico en la lista de registros

            //Mostrando el formulario de modificacion
           FormRemisionNew formGuiaRemision = new FormRemisionNew(currentGuiaRemision);
            formGuiaRemision.ShowDialog();
            this.reLoad(); // Recargando los registros
        }

        private async void executeEliminar()
        {
            // Verificando la existencia de datos en el datagridview
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Pregunta de seguridad de eliminacion
            DialogResult dialog = MessageBox.Show("¿Está seguro de eliminar este registro?", "Eliminar",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialog == DialogResult.No) return;


            try
            {
                int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
                currentGuiaRemision = new GuiaRemision(); //creando una instancia del objeto categoria
                currentGuiaRemision.idGuiaRemision = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview

                loadState(true); // cambiando el estado
                Response response = await guiaRemisionModel.eliminar(currentGuiaRemision); // Eliminando con el webservice correspondiente
                MessageBox.Show(response.msj, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarRegistros(); // recargando el datagridview
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadState(false); // cambiando el estado
            }
        }

        private async void executeAnular()
        {
            // Verificando la existencia de datos en el datagridview
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Desactivar o anular", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                loadState(true);
                int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
                currentGuiaRemision = new GuiaRemision(); //creando una instancia del objeto correspondiente
                currentGuiaRemision.idGuiaRemision = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview

                // Comprobando si el registro ya esta desactivado
                if (guiaRemisiones.Find(x => x.idGuiaRemision == currentGuiaRemision.idGuiaRemision).estado == 0)
                {
                    MessageBox.Show("Este registro ya esta desactivado", "Desactivar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // Procediendo con las desactivacion
                Response response = await currentGuiaRemision.anular(currentGuiaRemision);
                MessageBox.Show(response.msj, "Desactivar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarRegistros(); // recargando los registros en el datagridview
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadState(false);
            }
        }

        #endregion

        private void cbxSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSucursales.SelectedIndex == -1)
                return;

            if ((int)cbxSucursales.SelectedValue == 0)
            {
                almacenBindingSource.DataSource = listaAlmacen;
                cbxAlmacenes.SelectedIndex = -1;
                cbxAlmacenes.SelectedValue = 0;
            }
            else
            {
                List<Almacen> listA = new List<Almacen>();
                Almacen almacen = new Almacen();
                almacen.idAlmacen = 0;
                almacen.nombre = "Todos los almacenes";
                listA.Add(almacen);


                List<Almacen> list = listaAlmacen.Where(X => X.idSucursal == (int)cbxSucursales.SelectedValue).ToList();
                listA.AddRange(list);
                almacenBindingSource.DataSource = listA;
                cbxAlmacenes.SelectedIndex = -1;
                cbxAlmacenes.SelectedValue = 0;
            }
        }
    }
}
