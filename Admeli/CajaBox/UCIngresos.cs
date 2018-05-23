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
using Admeli.CajaBox.Nuevo;

namespace Admeli.CajaBox
{
    public partial class UCIngresos : UserControl
    {
        private FormPrincipal formPrincipal;
        public bool lisenerKeyEvents { get; set; }

        private List<Ingreso> ingresos { get; set; }
        private Ingreso currentIngreso { get; set; }

        private Paginacion paginacion;
        private IngresoModel ingresoModel = new IngresoModel();
        private PersonalModel personalModel = new PersonalModel();
        private SucursalModel sucursalModel = new SucursalModel();
        private ConfigModel configModel = new ConfigModel();

        #region ========================= CONSTRUCTORS =========================
        public UCIngresos()
        {
            InitializeComponent();

            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
        }

        public UCIngresos(FormPrincipal formPrincipal)
        {
            this.formPrincipal = formPrincipal;
            InitializeComponent();

            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
        } 
        #endregion

        #region ============================ Root load ============================
        private void UCIngresos_Load(object sender, EventArgs e)
        {
            this.darFormatoDecimales();
            this.reLoad();

            // Preparando para los eventos de teclado
            this.ParentChanged += ParentChange; // Evetno que se dispara cuando el padre cambia // Este eveto se usa para desactivar lisener key events de este modulo
            if (TopLevelControl is Form) // Escuchando los eventos del formulario padre
            {
                (TopLevelControl as Form).KeyPreview = true;
                TopLevelControl.KeyUp += TopLevelControl_KeyUp;
            }
        }

        private void darFormatoDecimales()
        {
            dataGridView.Columns["monto"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;
            dataGridView.Columns["monto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        internal void reLoad(bool refreshData = true)
        {
            if (refreshData)
            {
                //bloq
                verificarCaja(); // Verificar caja

                cargarComponentes();
                cargarSucursales();
                cargarPersonales();
                cargarRegistros();
                formPrincipal.cargarDatosAsideRight();
                //desblo
            }
            // Active lisener key events
            lisenerKeyEvents = true;
        }
        #endregion

        #region ======================= Loads =======================
        private void cargarComponentes()
        {
            loadState(true);

            // Cargando el combobox ce estados
            DataTable table = new DataTable();
            table.Columns.Add("idEstado", typeof(string));
            table.Columns.Add("estado", typeof(string));

            table.Rows.Add("todos", "Todos los estados");
            table.Rows.Add("0", "Anulados");
            table.Rows.Add("1", "Activos");

            cbxEstados.DataSource = table;
            cbxEstados.DisplayMember = "estado";
            cbxEstados.ValueMember = "idEstado";
            cbxEstados.SelectedIndex = 0;
        }

        private async void cargarSucursales()
        {
            try
            {
                sucursalBindingSource.DataSource = await sucursalModel.listarSucursalesActivos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarPersonales()
        {
            try
            {
                personalBindingSource.DataSource = await personalModel.listarPersonalAlmacen(ConfigModel.sucursal.idSucursal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarRegistros()
        {
            loadState(true);
            try
            {
                int personalId = (cbxPersonales.SelectedIndex == -1) ? PersonalModel.personal.idPersonal : Convert.ToInt32(cbxPersonales.SelectedValue);
                int sucursalId = (cbxSucursales.SelectedIndex == -1) ? ConfigModel.sucursal.idSucursal : Convert.ToInt32(cbxSucursales.SelectedValue);
                string estado = (cbxEstados.SelectedIndex == -1) ? "todos" : cbxEstados.SelectedValue.ToString();
                int idCajaSesion = 0;// ConfigModel.cajaSesion.idCajaSesion;

                RootObject<Ingreso> ingresoRoot = await ingresoModel.ingresos(sucursalId, personalId, idCajaSesion, estado, paginacion.currentPage, paginacion.speed);

                // actualizando datos de páginacón
                paginacion.itemsCount = ingresoRoot.nro_registros;
                paginacion.reload();

                // Ingresando
                ingresos = ingresoRoot.datos;
                ingresoBindingSource.DataSource = ingresos;
                dataGridView.Refresh();
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
                this.formPrincipal.cargarDatosAsideRight();
                loadState(false);
            }
        }
        #endregion

        #region ============================== Verificar caja ==============================
        private async void verificarCaja()
        {
            await configModel.loadCajaSesion(ConfigModel.asignacionPersonal.idAsignarCaja);
            if (ConfigModel.cajaIniciada)
            {
                btnNuevo.Enabled = true;
                btnAnular.Enabled = true;
                lblCajaEstado.Visible = false;
            }
            else
            {
                btnNuevo.Enabled = false;
                btnAnular.Enabled = false;
                lblCajaEstado.Visible = true;
                Validator.labelAlert(lblCajaEstado, 0, "No se inició la caja");
            }
        } 
        #endregion

        #region =========================== Decoration ===========================
        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panelContainer);
        }

        private void decorationDataGridView()
        {
            if (dataGridView.Rows.Count == 0) return;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                int idIngreso = Convert.ToInt32(row.Cells[0].Value); // obteniedo el idCategoria del datagridview

                currentIngreso = ingresos.Find(x => x.idIngreso == idIngreso); // Buscando la categoria en las lista de categorias
                if (currentIngreso.estado == 0)
                {
                    dataGridView.ClearSelection();
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 224);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(250, 5, 73);
                }
            }
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
                    if (!ConfigModel.cajaIniciada) break;
                    executeNuevo();
                    break;
                case Keys.F4:
                    //if (!ConfigModel.cajaIniciada) break;
                    //executeModificar();
                    break;
                case Keys.F5:
                    cargarRegistros();
                    break;
                case Keys.F6:
                    //if (!ConfigModel.cajaIniciada) break;
                    //executeEliminar();
                    break;
                case Keys.F7:
                    if (!ConfigModel.cajaIniciada) break;
                    executeAnular();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region =========================== Estados ===========================
        private void loadState(bool state)
        {
            formPrincipal.appLoadState(state);
            panelNavigation.Enabled = !state;
            panelCrud.Enabled = !state;
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            executeModificar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            executeEliminar();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            executeAnular();
        }

        private void executeNuevo()
        {
            FormIngresoNuevo formIngreso = new FormIngresoNuevo();
            formIngreso.ShowDialog();
            cargarRegistros();
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
            int idIngreso = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview

            currentIngreso = ingresos.Find(x => x.idIngreso == idIngreso); // Buscando la registro especifico en la lista de registros

            // Mostrando el formulario de modificacion
            FormIngresoNuevo formIngreso = new FormIngresoNuevo(currentIngreso);
            formIngreso.ShowDialog();
            cargarRegistros(); // recargando loas registros en el datagridview
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
                currentIngreso = new Ingreso(); //creando una instancia del objeto categoria
                currentIngreso.idIngreso = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview

                loadState(true); // cambiando el estado
                Response response = await ingresoModel.eliminar(currentIngreso); // Eliminando con el webservice correspondiente
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
                currentIngreso = new Ingreso(); //creando una instancia del objeto correspondiente
                currentIngreso.idIngreso = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview

                // Comprobando si el registro ya esta desactivado
                if (ingresos.Find(x => x.idIngreso == currentIngreso.idIngreso).estado == 0)
                {
                    MessageBox.Show("Este registro ya esta desactivado", "Desactivar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // Procediendo con las desactivacion
                Response response = await ingresoModel.desactivar(currentIngreso);
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

    }
}
