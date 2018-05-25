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
using Admeli.Compras.Nuevo;
using Modelo;
using Entidad;
using Newtonsoft.Json;

namespace Admeli.Compras
{
    public partial class UCOrdenCompraProveedor : UserControl
    {
        private FormPrincipal formPrincipal;
        public bool lisenerKeyEvents { get; set; }

        private List<OrdenCompra> ordenCompras { get; set; }
        private OrdenCompra currentOrdenCompra { get; set; }
        public Sucursal sucursal;
        private Paginacion paginacion;
        private OrdenCompraModel ordenCompraModel = new OrdenCompraModel();
        private PersonalModel personalModel = new PersonalModel();
        private SucursalModel sucursalModel = new SucursalModel();

        #region ============================= CONSTRUCTOR =============================
        public UCOrdenCompraProveedor()
        {
            InitializeComponent();

            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
        }

        public UCOrdenCompraProveedor(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;

            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
        }
        #endregion

        #region ==================================== PAINT AND DECORATION ====================================
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
                int idOrdenCompra = Convert.ToInt32(row.Cells[0].Value); // obteniedo el idCategoria del datagridview

                currentOrdenCompra = ordenCompras.Find(x => x.idOrdenCompra == idOrdenCompra); // Buscando la categoria en las lista de categorias
                if (currentOrdenCompra.estado == 0)
                {
                    dataGridView.ClearSelection();
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 224);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(250, 5, 73);
                }
            }
        } 
        #endregion

        #region ======================================= ROOT LOAD =======================================
        private void UCOrdenCompraProveedor_Load(object sender, EventArgs e)
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
                cargarPersonales();
                cargarSucursales();
                cargarRegistros();
            }
            lisenerKeyEvents = true; // Active lisener key events
        } 
        #endregion

        #region =========================== Estados ===========================
        private void loadState(bool state)
        {
            panelNavigation.Enabled = !state;
            panelCrud.Enabled = !state;
            dataGridView.Enabled = !state;
        }
        #endregion

        #region ======================= Loads =======================
        private async void cargarSucursales()
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
                //Si el personal logeado es gerente o administrador debe listar a todos los personales
                if (ConfigModel.asignacionPersonal.idPuntoGerencia != 0 || ConfigModel.asignacionPersonal.idPuntoAdministracion != 0)
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
                //personalBindingSource.DataSource = await personalModel.listarPersonalAlmacen(ConfigModel.sucursal.idSucursal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarRegistros()
        {
            formPrincipal.appLoadState(true);
            loadState(true);
            try
            {
                int personalId = (cbxPersonales.SelectedIndex == -1) ? PersonalModel.personal.idPersonal : Convert.ToInt32(cbxPersonales.SelectedValue);
                int sucursalId = (cbxSucursales.SelectedIndex == -1) ? ConfigModel.sucursal.idSucursal : Convert.ToInt32(cbxSucursales.SelectedValue);

                RootObject<OrdenCompra> rootData  = await ordenCompraModel.ocompras(sucursalId, personalId, paginacion.currentPage, paginacion.speed);

                // actualizando datos de páginacón
                paginacion.itemsCount = rootData.nro_registros;
                paginacion.reload();

                // Ingresando
                ordenCompras = rootData.datos;
                ordenCompraBindingSource.DataSource = rootData.datos;
                dataGridView.Refresh();

                // Mostrando la paginacion
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
                formPrincipal.appLoadState(false);
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

        #region ============================= CRUD =============================
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
            int i = cbxPersonales.SelectedIndex;
            Personal personal = personalBindingSource.List[i] as Personal;
            FormOrdenCompraNew formOrdenCompraNew = new FormOrdenCompraNew();
            formOrdenCompraNew.ShowDialog();
            cargarRegistros();

        }

        private void executeModificar()
        {
            // Verificando la existencia de datos en el datagridview
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idOrdenCompra = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview

            currentOrdenCompra = ordenCompras.Find(x => x.idOrdenCompra == idOrdenCompra); // Buscando la registro especifico en la lista de registros

            // Mostrando el formulario de modificacion
            int i = cbxPersonales.SelectedIndex;
            Personal personal = personalBindingSource.List[i] as Personal;
            FormOrdenCompraNew formOrdenCompraNew = new FormOrdenCompraNew(currentOrdenCompra);
            formOrdenCompraNew.ShowDialog();
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
                currentOrdenCompra = new OrdenCompra(); //creando una instancia del objeto categoria
                currentOrdenCompra.idOrdenCompra = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview

                loadState(true); // cambiando el estado

                OrdenCompraElimnar elimnarorden = new OrdenCompraElimnar();
                elimnarorden.idCompra = currentOrdenCompra.idCompra;
                elimnarorden.idOrdenCompra = currentOrdenCompra.idOrdenCompra;
                elimnarorden.idPago = currentOrdenCompra.idPago;
                
                Response response = await ordenCompraModel.eliminar(elimnarorden); // Eliminando con el webservice correspondiente
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
                int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
                int idOrdenCompra = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idRegistro del datagridview

                currentOrdenCompra = ordenCompras.Find(x => x.idOrdenCompra == idOrdenCompra); // Buscando la registro especifico en la lista de registros
                //currentOrdenCompra.estado = 0;
                //currentOrdenCompra.idCajaSesion = ConfigModel.cajaSesion.idCajaSesion;

                // Procediendo con las desactivacion
                Response response = await ordenCompraModel.anular(currentOrdenCompra);
                MessageBox.Show(response.msj, "Anular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarRegistros(); // recargando los registros en el datagridview
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Anular", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}
