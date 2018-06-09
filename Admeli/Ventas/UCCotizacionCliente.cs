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
using Admeli.Ventas.Nuevo;
using Modelo;
using Entidad;
using Admeli.Properties;

namespace Admeli.Ventas
{
    public partial class UCCotizacionCliente : UserControl
    {
        private FormPrincipal formPrincipal;
        public bool lisenerKeyEvents { get; set; }

        private Cotizacion currentCotizacion { get; set; }
        private List<Cotizacion> cotizaciones { get; set; }

        private Paginacion paginacion;
        private CotizacionModel cotizacionModel = new CotizacionModel();
        private PersonalModel personalModel = new PersonalModel();
        private SucursalModel sucursalModel = new SucursalModel();
        private bool inactivo = false;
        private bool normal = false;
        private bool reservado = true;
        private bool Realizada = false;

        #region ================================ CONSTRUCTOR ================================
        public UCCotizacionCliente()
        {
            InitializeComponent();

            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
        }

        public UCCotizacionCliente(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;

            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
        } 
        #endregion

        #region =================================== ROOT LOAD ===================================
        private void UCCotizacionCliente_Load(object sender, EventArgs e)
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
                cargarPersonales();
                cargarRegistros();
            }
            lisenerKeyEvents = true; // Active lisener key events
        }
        #endregion

        #region ================================== PAINT ==================================
        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panelContainer);
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panel10);
        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panel13);
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

        #region =========================== Decoration ===========================
        private void decorationDataGridView()
        {
            /*
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                var estado = dataGridView.Rows[i].Cells.get.Value.ToString();
                dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.DeepPink;
            }*/
        }
        #endregion

        #region ======================= Loads =======================
        private void cargarSucursales()
        {
            try
            {
                List<Sucursal> listSucCargar = new List<Sucursal>();
                List<Sucursal> listSuc = ConfigModel.listSucursales;
                Sucursal sucursal = new Sucursal();
                sucursal.idSucursal = 0;
                sucursal.nombre = "Todas las sucursales";
                listSucCargar.Add(sucursal);
                listSucCargar.AddRange(listSuc);
                sucursalBindingSource.DataSource = listSucCargar;                
                cbxSucursales.SelectedValue = 0;

                //sucursalBindingSource.DataSource = await sucursalModel.listarSucursalesActivos();
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
                //Si el personal logeado es gerente o administrador debe listar a todos los cliente
                if(ConfigModel.asignacionPersonal.idPuntoGerencia != 0 || ConfigModel.asignacionPersonal.idPuntoAdministracion != 0)
                {
                    personalBindingSource.DataSource = await personalModel.listarPersonalAlmacen(ConfigModel.sucursal.idSucursal);
                }
                else
                {
                    List<Personal> listaPersonal = new List<Personal>();
                    Personal personal = new Personal();
                    personal.idPersonal = PersonalModel.personal.idPersonal;
                    personal.nombres = PersonalModel.personal.nombres;
                    listaPersonal.Add(personal);
                    personalBindingSource.DataSource = listaPersonal;
                    cbxPersonales.SelectedIndex = 0;
                }
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

                RootObject<Cotizacion> rootData = await cotizacionModel.cotizaciones(sucursalId, personalId, paginacion.currentPage, paginacion.speed);

                // actualizando datos de páginacón
                paginacion.itemsCount = rootData.nro_registros;
                paginacion.reload();

                // Ingresando
                cotizaciones = rootData.datos;
                cotizacionBindingSource.DataSource = cotizaciones;
                dataGridView.Refresh();
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

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            executeAnular();
        }

        private void executeNuevo()
        {
            FormCotizacionaNew formCotizacionNuevo = new FormCotizacionaNew();
            formCotizacionNuevo.ShowDialog();
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
            int idCotizacion = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview

            currentCotizacion = cotizaciones.Find(x => x.idCotizacion == idCotizacion); // Buscando la categoria en las lista de categorias

            // Mostrando el formulario de modificacion
            FormCotizacionaNew formCotizacionNuevo = new FormCotizacionaNew(currentCotizacion);
            formCotizacionNuevo.ShowDialog();
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
                currentCotizacion = new Cotizacion(); //creando una instancia del objeto categoria
                currentCotizacion.idCotizacion = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview

                loadState(true); // cambiando el estado
                Response response = await cotizacionModel.desactivar(currentCotizacion); // Eliminando con el webservice correspondiente
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

            // Pregunta de seguridad de anular
            DialogResult dialog = MessageBox.Show("¿Está seguro de anular este registro?", "Anular",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialog == DialogResult.No) return;

            try
            {
                int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
                currentCotizacion = new Cotizacion(); //creando una instancia del objeto categoria
                currentCotizacion.idCotizacion = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview

                // Comprobando si la categoria ya esta desactivado
                if (cotizaciones.Find(x => x.idCotizacion == currentCotizacion.idCotizacion).estado == 0)
                {
                    MessageBox.Show("Este registro ya esta desactivado", "Desactivar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // Procediendo con las desactivacion
                Response response = await cotizacionModel.desactivar(currentCotizacion);
                MessageBox.Show(response.msj, "Desactivar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarRegistros(); // recargando los registros en el datagridview
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnInactivo_Click(object sender, EventArgs e)
        {
            if (!inactivo)
            {
                Image image = Resources.check;
                btnInactivo.Image = image;
                inactivo = true;
               
                this.btnInactivo.BackColor = Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(43)))), ((int)(((byte)(33)))));
                this.btnInactivo.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(31)))), ((int)(((byte)(25)))));
                this.btnInactivo.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(31)))), ((int)(((byte)(25)))));


            }
            else
            {

                Image image = Resources.verificar;
                btnInactivo.Image = image;
                inactivo = false;
                this.btnInactivo.BackColor = Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(31)))), ((int)(((byte)(25)))));
                this.btnInactivo.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(43)))), ((int)(((byte)(33)))));
                this.btnInactivo.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(43)))), ((int)(((byte)(33)))));

            }

        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            if (!normal)
            {
                Image image = Resources.check;
                btnNormal.Image = image;
                normal = true;               
                this.btnNormal.BackColor = Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(108)))), ((int)(((byte)(51)))));
                this.btnNormal.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(78)))), ((int)(((byte)(37)))));
                this.btnNormal.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(78)))), ((int)(((byte)(37)))));

            }
            else
            {

                Image image = Resources.verificar;
                btnNormal.Image = image;
                normal = false;
               
                this.btnNormal.BackColor = Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(78)))), ((int)(((byte)(37)))));
                this.btnNormal.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(108)))), ((int)(((byte)(51)))));
                this.btnNormal.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(108)))), ((int)(((byte)(51)))));


            }
        }

        private void btnReservada_Click(object sender, EventArgs e)
        {
            if (!reservado)
            {
                Image image = Resources.check;
                btnReservada.Image = image;
                reservado = true;
              
                this.btnReservada.BackColor = Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(36)))));
                this.btnReservada.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(29)))));
                this.btnReservada.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(29)))));

            }
            else
            {
            
                Image image = Resources.verificar;
                btnReservada.Image = image;
                reservado = false;
                this.btnReservada.BackColor = Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(29)))));
                this.btnReservada.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(36)))));
                this.btnReservada.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(36)))));

            }
        }

        private void btnRealizada_Click(object sender, EventArgs e)
        {
            if (!Realizada)
            {
                Image image = Resources.check;
                btnRealizada.Image = image;
                Realizada = true;
               
              
                this.btnRealizada.BackColor = Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(196)))), ((int)(((byte)(34)))));
                this.btnRealizada.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(161)))), ((int)(((byte)(28)))));
                this.btnRealizada.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(161)))), ((int)(((byte)(28)))));

            }
            else
            {

                Image image = Resources.verificar;
                btnRealizada.Image = image;
                Realizada = false;
                this.btnRealizada.BackColor = Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(161)))), ((int)(((byte)(28)))));
                this.btnRealizada.FlatAppearance.MouseDownBackColor = Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(196)))), ((int)(((byte)(34)))));
                this.btnRealizada.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(196)))), ((int)(((byte)(34)))));

            }
        }
    }
}
