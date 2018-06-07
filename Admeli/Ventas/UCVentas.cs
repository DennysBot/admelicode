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
using Entidad.Configuracion;

namespace Admeli.Ventas
{
    public partial class UCVentas : UserControl
    {
        private FormPrincipal formPrincipal;
        public bool lisenerKeyEvents { get; set; }

        private List<Venta> ventas { get; set; }
        private Venta currentVenta { get; set; }

        private Paginacion paginacion;
        private PersonalModel personalModel = new PersonalModel();
        private VentaModel ventaModel = new VentaModel();
        private SucursalModel sucursalModel = new SucursalModel();
        private PuntoVentaModel puntoVentaModel = new PuntoVentaModel();

        #region =================================== CONSTRUCTOR ===================================
        public UCVentas()
        {
            InitializeComponent();

            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
        }

        public UCVentas(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;

            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
        }
        #endregion

        #region ==================================== ROOT LOAD ====================================
        private void UCVentas_Load(object sender, EventArgs e)
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
                cargarComponentes();
                cargarPersonales();
                cargarSucursales();
                cargarRegistros();
                cargarPuntosVenta(0);
            }
            lisenerKeyEvents = true; // Active lisener key events
        }
        #endregion

        #region ================================ PAINT AND DECORATION ================================
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

        private void panel12_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panel12);
        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panel13);
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panel11);
        }

        private void decorationDataGridView()
        {
            // Verificando la existencia de datos en el datagridview
            if (dataGridView.Rows.Count == 0) return;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                int idVenta = Convert.ToInt32(row.Cells[0].Value); // obteniedo el idCategoria del datagridview
                if (idVenta > 0) { 
                    Venta venta = ventas.Find(x => x.idVenta == idVenta); // Buscando la categoria en las lista de categorias
                    if (venta.estado == 0)
                    {
                        dataGridView.ClearSelection();
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 224);
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(250, 5, 73);
                    }
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

            cbxEstados.DataSource = table;
            cbxEstados.DisplayMember = "estado";
            cbxEstados.ValueMember = "idEstado";
            //cbxEstados.ComboBox.SelectedIndex = 0;
        }

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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarPuntosVenta( int idSucursal)
        {
            try
            {
                List<PuntoDeVenta> listaPuntoVenta = new List<PuntoDeVenta>();
                PuntoDeVenta puntoVenta = new PuntoDeVenta();
                puntoVenta.idPuntoVenta = 0;
                puntoVenta.nombre = "Todos los puntos de venta";
                listaPuntoVenta.Add(puntoVenta);
                listaPuntoVenta.AddRange(ConfigModel.puntosDeVenta);
                puntoDeVentaBindingSource.DataSource = listaPuntoVenta;
                //puntoDeVentaBindingSource.DataSource=await  puntoVentaModel.puntoVentasyTodos(idSucursal);
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
                int puntoVentaId = (cbxPuntosVenta.SelectedIndex == -1) ? ConfigModel.currentPuntoVenta : Convert.ToInt32(cbxPuntosVenta.SelectedValue);
                RootObject<Venta> rootData = await ventaModel.ventas(sucursalId, puntoVentaId,  personalId, estado, paginacion.currentPage, paginacion.speed);
                // actualizando datos de páginacón
                paginacion.itemsCount = rootData.nro_registros;
                paginacion.reload();
                // Ingresando
                ventas = rootData.datos;
                
                ventaBindingSource.DataSource = ventas;
                dataGridView.Refresh();
                // Mostrando la paginacion
                mostrarPaginado();
                // formato de celdas
                decorationDataGridView();
                textBuscar.Text = "";
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
            //FormVentaNuevo1 formVentaNuevo = new FormVentaNuevo1(ConfigModel.sucursal, personalBindingSource.List[0] as Personal );
            FormVentaNewR formVentaNuevo = new FormVentaNewR();
            formVentaNuevo.ShowDialog();
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
            int idVenta = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview

            currentVenta = ventas.Find(x => x.idVenta == idVenta); // Buscando la categoria en las lista de categorias

          
           FormVentaNewR formVentaNuevo = new FormVentaNewR(currentVenta);
            formVentaNuevo.ShowDialog();
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
                currentVenta = new Venta(); //creando una instancia del objeto categoria
                currentVenta.idVenta = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview

                loadState(true); // cambiando el estado
                Response response = await ventaModel.eliminar(currentVenta); // Eliminando con el webservice correspondiente
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
                currentVenta = new Venta(); //creando una instancia del objeto categoria
                currentVenta.idVenta = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview

                // Comprobando si la categoria ya esta desactivado
                if (ventas.Find(x => x.idVenta == currentVenta.idVenta).estado == 0)
                {
                    MessageBox.Show("Este registro ya esta desactivado", "Desactivar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                VentaAnular ventaAnular = new VentaAnular();
                ventaAnular.idVenta = currentVenta.idVenta;
                ventaAnular.idCajaSesion = ConfigModel.cajaSesion!=null ? ConfigModel.cajaSesion.idCajaSesion :0;
                // Procediendo con las desactivacion
                Response response = await ventaModel.desactivar(ventaAnular);
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

        private void panelNavigation_Paint(object sender, PaintEventArgs e)
        {

        }


        private void panelTools_Paint(object sender, PaintEventArgs e)

        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            cargarRegistros();
        }

        private  async void cbxSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSucursales.SelectedIndex == -1) return;

            if ((int)cbxSucursales.SelectedValue == 0)
            {

                List<PuntoDeVenta> listaPuntoVenta = new List<PuntoDeVenta>();
                PuntoDeVenta puntoVenta = new PuntoDeVenta();
                puntoVenta.idPuntoVenta = 0;
                puntoVenta.nombre = "Todos los puntos de venta";
                listaPuntoVenta.Add(puntoVenta);
                listaPuntoVenta.AddRange(ConfigModel.puntosDeVenta);
                puntoDeVentaBindingSource.DataSource = listaPuntoVenta;
                cbxPuntosVenta.SelectedIndex = -1;
                cbxPuntosVenta.SelectedValue = 0;
            }
            else
            {
                List<PuntoDeVenta> listaPuntoVenta = new List<PuntoDeVenta>();
                PuntoDeVenta puntoVenta = new PuntoDeVenta();
                puntoVenta.idPuntoVenta = 0;
                puntoVenta.nombre = "Todos los puntos de venta";
                listaPuntoVenta.Add(puntoVenta);

                List<PuntoDeVenta> lista = ConfigModel.puntosDeVenta.Where(X => X.idSucursal == (int)cbxSucursales.SelectedValue).ToList();
                listaPuntoVenta.AddRange(lista);
                puntoDeVentaBindingSource.DataSource = listaPuntoVenta;
                cbxPuntosVenta.SelectedIndex = -1;
                cbxPuntosVenta.SelectedValue = 0;

            }
            //puntoDeVentaBindingSource.DataSource = await puntoVentaModel.puntoVentasyTodos((int)cbxSucursales.SelectedValue);

        }

        private void textBuscar_OnValueChanged(object sender, EventArgs e)
        {
            BindingList<Venta> filtered = new BindingList<Venta>(ventas.Where(obj => obj.nombreCliente.Trim().ToUpper().Contains(textBuscar.Text.Trim().ToUpper())).ToList());
            ventaBindingSource.DataSource = filtered;
            dataGridView.Update();

        }
    }
}
