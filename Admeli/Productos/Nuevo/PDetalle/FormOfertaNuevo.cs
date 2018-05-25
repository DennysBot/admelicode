using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.Configuracion.Nuevo;
using Entidad;
using Modelo;

namespace Admeli.Productos.Nuevo.PDetalle
{
    public partial class FormOfertaNuevo : Form
    {
        private FormProductoNuevo formProductoNuevo;
        private Oferta currentOferta { get; set; }
        private OfertaG currentOfertaEnv { get; set; }
        private int currentIDOferta { get; set; }
        private bool nuevo { get; set; }

        private SucursalModel sucursalModel = new SucursalModel();
        private GrupoClienteModel grupoClienteModel = new GrupoClienteModel();
        private ProductoModel productoModel = new ProductoModel();
        private PresentacionModel presentacionModel = new PresentacionModel();
        private OfertaModel ofertaModel = new OfertaModel();

        #region ================================ Constructor ================================
        public FormOfertaNuevo()
        {
            InitializeComponent();
        }

        public FormOfertaNuevo(FormProductoNuevo formProductoNuevo)
        {
            InitializeComponent();
            this.formProductoNuevo = formProductoNuevo;
            this.currentOferta = new Oferta();
            this.nuevo = true;
            currentOferta.idSucursal = ConfigModel.sucursal.idSucursal;
            currentOferta.idAfectoProducto = 0;
        }

        public FormOfertaNuevo(FormProductoNuevo formProductoNuevo, Oferta currentOferta)
        {
            InitializeComponent();
            this.formProductoNuevo = formProductoNuevo;
            this.currentOferta = currentOferta;
            this.currentIDOferta = currentOferta.idOfertaProductoGrupo;
            this.nuevo = false;
        }
        #endregion

        #region ================================== Root load ==================================
        private void FormOfertaNuevo_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        internal void reLoad()
        {
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;

            cargarDatosModificar();
            cargarProducto21();            
            cargarGrupoCliente();
            cargarSucursales();
        } 
        #endregion

        #region ================================ Loads ================================
        private void cargarDatosModificar()
        {
            if (nuevo) return;
            textCodigoOferta.Text = currentOferta.codigo;
            textDescuento.Text = currentOferta.descuento;
            dtpFechaInicio.Value = currentOferta.fechaInicio.date;
            dtpFechaFin.Value = currentOferta.fechaFin.date;
            chkEstado.Checked = Convert.ToBoolean(currentOferta.estado);
        }
        private async void cargarSucursales()
        {
            try
            {
                sucursalBindingSource.DataSource = await sucursalModel.sucursalesPrecio(formProductoNuevo.currentIDProducto);
                cbxSucursal.SelectedValue = currentOferta.idSucursal;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarGrupoCliente()
        {
            try
            {
                grupoClienteBindingSource.DataSource = await grupoClienteModel.gclientes21();
                if (nuevo) { cbxGrupoCliente.SelectedIndex = 0; }
                else { cbxGrupoCliente.SelectedValue = currentOferta.idGrupoCliente; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarProducto21()
        {
            try
            {
                presentacionBindingSource.DataSource = await presentacionModel.productos21(formProductoNuevo.currentIDProducto);
                //productoBindingSource.DataSource = await productoModel.productos21(formProductoNuevo.currentIDProducto);
                cbxProductoAfecto.SelectedValue = currentOferta.idAfectoProducto;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        } 
        #endregion

        #region ====================== Form Add Registers ======================
        private void btnAddGrupoCliente_Click(object sender, EventArgs e)
        {
            FormGrupoClienteNuevo formGrupoCliente = new FormGrupoClienteNuevo();
            formGrupoCliente.ShowDialog();
        }

        private void btnAddSucursal_Click(object sender, EventArgs e)
        {
            FormSucursalNuevo formSucursal = new FormSucursalNuevo();
            formSucursal.ShowDialog();
        } 
        #endregion

        #region ========================== SAVE AND UPDATE ===========================
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            executeGuardar();
        }
        private async void executeGuardar()
        {
            bloquear(true);
            if (!validarCampos()) { bloquear(false); return; }
            try
            {
                crearObjetoSucursal();
                if (nuevo)
                {
                    Response response = await ofertaModel.guardar(currentOfertaEnv);
                    MessageBox.Show(response.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Response response = await ofertaModel.modificar(currentOfertaEnv);
                    MessageBox.Show(response.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bloquear(false);
        }

        private void crearObjetoSucursal()
        {
            currentOfertaEnv = new OfertaG();
            if (!nuevo)
            {
                currentOfertaEnv.idOfertaProductoGrupo = currentIDOferta; // Llenar el id categoria cuando este en esdo modificar
            }
            currentOfertaEnv.codigo = textCodigoOferta.Text;
            currentOfertaEnv.descuento = textDescuento.Text;
            currentOfertaEnv.estado = Convert.ToInt32(chkEstado.Checked);
            currentOfertaEnv.fechaFin = dtpFechaFin.Value.ToString("yyyy-MM-dd HH':'mm':'ss");
            currentOfertaEnv.fechaInicio = dtpFechaInicio.Value.ToString("yyyy-MM-dd HH':'mm':'ss");
            currentOfertaEnv.idAfectoProducto = (int)cbxProductoAfecto.SelectedValue;
            currentOfertaEnv.idGrupoCliente = (int)cbxGrupoCliente.SelectedValue;
            currentOfertaEnv.idPresentacion = formProductoNuevo.currentIDProducto;
            currentOfertaEnv.idSucursal = (int)cbxSucursal.SelectedValue;
            currentOfertaEnv.tipo = "Particular";
        }

        private bool validarCampos()
        {
            if (textCodigoOferta.Text == "")
            {
                errorProvider1.SetError(textCodigoOferta, "Este campo esta bacía");
                textCodigoOferta.Focus();
                return false;
            }
            errorProvider1.Clear();

            if (textDescuento.Text == "")
            {
                errorProvider1.SetError(textDescuento, "Este campo esta bacía");
                textDescuento.Focus();
                return false;
            }
            errorProvider1.Clear();
            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        public void bloquear(bool state)
        {
            if (state){ Cursor.Current = Cursors.WaitCursor; }
            else{ Cursor.Current = Cursors.Default; }
            this.Enabled = !state;
        }
    }
}
