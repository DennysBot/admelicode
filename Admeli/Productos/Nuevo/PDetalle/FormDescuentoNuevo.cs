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
    public partial class FormDescuentoNuevo : Form
    {
        private FormProductoNuevo formProductoNuevo;

        private DescuentoEnviar currentDescuento { get; set; }
        private int currentIDDescuento { get; set; }
        private bool nuevo { get; set; }

        private SucursalModel sucursalModel = new SucursalModel();
        private GrupoClienteModel grupoClienteModel = new GrupoClienteModel();
        private ProductoModel productoModel = new ProductoModel();
        private DescuentoModel descuentoModel = new DescuentoModel();
        private PresentacionModel presentacionModel = new PresentacionModel();

        #region ====================== Constructor ======================
        public FormDescuentoNuevo()
        {
            InitializeComponent();
        }

        public FormDescuentoNuevo(FormProductoNuevo formProductoNuevo)
        {
            InitializeComponent();
            this.formProductoNuevo = formProductoNuevo;
            this.currentDescuento = new DescuentoEnviar();
            currentDescuento.idSucursal = ConfigModel.sucursal.idSucursal;
            currentDescuento.idAfectoProducto = 0;
            nuevo = true;
        }

        public FormDescuentoNuevo(FormProductoNuevo formProductoNuevo, Descuento currentDescuentoR)
        {
            InitializeComponent();
            this.formProductoNuevo = formProductoNuevo;
            this.currentDescuento = new DescuentoEnviar();
            this.currentDescuento.cantidadMaxima = currentDescuentoR.cantidadMaxima.ToString(ConfigModel.configuracionGeneral.formatoDecimales);
            this.currentDescuento.cantidadMinima = currentDescuentoR.cantidadMinima.ToString(ConfigModel.configuracionGeneral.formatoDecimales);
            this.currentDescuento.codigo = currentDescuentoR.codigo;
            this.currentDescuento.descuento = currentDescuentoR.descuento.ToString(ConfigModel.configuracionGeneral.formatoDecimales);
            this.currentDescuento.estado = currentDescuentoR.estado;
            this.currentDescuento.fechaFin = currentDescuentoR.SFechaFin;
            this.currentDescuento.fechaInicio = currentDescuentoR.SFechaInicio;
            this.currentDescuento.idAfectoProducto= currentDescuentoR.idAfectoProducto;
            this.currentDescuento.idDescuentoProductoGrupo = currentDescuentoR.idDescuentoProductoGrupo;
            this.currentDescuento.idGrupoCliente = currentDescuentoR.idGrupoCliente;
            this.currentDescuento.idPresentacion = currentDescuentoR.idPresentacion;
            this.currentDescuento.idSucursal = currentDescuentoR.idSucursal;
            nuevo = false;
        }
        #endregion

        #region ========================== Root load ==========================
        private void FormDescuentoNuevo_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        private void reLoad()
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
            textCodigo.Text = currentDescuento.codigo;
            textMaximaVenta.Text = currentDescuento.cantidadMaxima;
            textMinimaVenta.Text = currentDescuento.cantidadMinima;
            textDescuento.Text = currentDescuento.descuento;
            chkEstado.Checked = Convert.ToBoolean(currentDescuento.estado);
            dtpFechaInicio.Value = Convert.ToDateTime(currentDescuento.fechaInicio);
            dtpFechaFin.Value = Convert.ToDateTime(currentDescuento.fechaFin);
        }

        private async void cargarSucursales()
        {
            try
            {
                sucursalBindingSource.DataSource = await sucursalModel.sucursalesPrecio(formProductoNuevo.currentIDProducto);
                cbxSucursal.SelectedValue = currentDescuento.idSucursal;
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
                else { cbxGrupoCliente.SelectedValue = currentDescuento.idGrupoCliente; }
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
                cbxProductoAfecto.SelectedValue = currentDescuento.idAfectoProducto;
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

                    Response response = await descuentoModel.guardar(currentDescuento);
                    MessageBox.Show(response.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Response response = await descuentoModel.modificar(currentDescuento);
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
            string dateFin = String.Format("{0:u}", dtpFechaFin.Value);
            dateFin = dateFin.Substring(0, dateFin.Length - 1);
            string dateInicio = String.Format("{0:u}", dtpFechaInicio.Value);
            dateInicio = dateInicio.Substring(0, dateInicio.Length - 1);

            currentDescuento.cantidadMaxima = textMaximaVenta.Text.Trim();
            currentDescuento.cantidadMinima = textMinimaVenta.Text.Trim();
            currentDescuento.codigo = textCodigo.Text.Trim();
            currentDescuento.descuento = textDescuento.Text.Trim();
            currentDescuento.estado = Convert.ToInt32(chkEstado.Checked);
            currentDescuento.fechaFin = dateFin;
            currentDescuento.fechaInicio = dateInicio;
            currentDescuento.idAfectoProducto = (int)cbxProductoAfecto.SelectedValue;
            currentDescuento.idGrupoCliente = (int)cbxGrupoCliente.SelectedValue;
            currentDescuento.idPresentacion = formProductoNuevo.currentIDProducto;
            currentDescuento.idSucursal = (int)cbxSucursal.SelectedValue;
            currentDescuento.tipo = "Particular";
        }

        private bool validarCampos()
        {
            if (textCodigo.Text == "")
            {
                errorProvider1.SetError(textCodigo, "Este campo esta vacía");
                textCodigo.Focus();
                return false;
            }
            errorProvider1.Clear();

            if (textMaximaVenta.Text == "")
            {
                errorProvider1.SetError(textMaximaVenta, "Este campo esta vacía");
                textMaximaVenta.Focus();
                return false;
            }
            errorProvider1.Clear();

            if (textMinimaVenta.Text == "")
            {
                errorProvider1.SetError(textMinimaVenta, "Este campo esta vacía");
                textMinimaVenta.Focus();
                return false;
            }
            errorProvider1.Clear();

            if (textDescuento.Text == "")
            {
                errorProvider1.SetError(textDescuento, "Este campo esta vacía");
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
            if (state)
            {
                Cursor.Current = Cursors.WaitCursor;
            }
            else
            {
                Cursor.Current = Cursors.Default;
            }
            this.Enabled = !state;
        }

    }
}
