using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.Componentes;
using Entidad;
using Modelo;

namespace Admeli.CajaBox.Nuevo
{
    public partial class FormIngresoNuevo : Form
    {
        private bool nuevo { get; set; }
        private IngresoModel ingresoModel = new IngresoModel();
        private MonedaModel monedaModel = new MonedaModel();
        private MedioPagoModel medioPagoModel = new MedioPagoModel();
        private CajaModel cajaModel = new CajaModel();

        private Ingreso currentIngreso { get; set; }
        private SaveObject currentSaveObject { get; set; }
        private List<MedioPago> mediosDePagos { get; set; }

        public FormIngresoNuevo()
        {
            InitializeComponent();
            dtpFechaPago.Value = DateTime.Now;
            this.nuevo = true;
        }

        public FormIngresoNuevo(Ingreso currentIngreso)
        {
            this.currentIngreso = currentIngreso;
            this.nuevo = false;
            btnAceptar.Enabled = false;
        }

        private void FormIngresoNuevo_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        private void reLoad()
        {
            this.cargarMonedas();
            this.cargarCorrelativo();
            this.cargarMediosPago();

            // Verificacion del estado de caja
            this.verificarCaja();
        }

        #region =========================== Loads ===========================
        private async void cargarMonedas()
        {
            try
            {
                monedaBindingSource.DataSource = await monedaModel.monedas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarMediosPago()
        {
            try
            {
                mediosDePagos = await medioPagoModel.medioPagos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        struct CorrelativoData
        {
            public string correlativoActual { get; set; }
            public string serie { get; set; }
        }

        private async void cargarCorrelativo()
        {
            try
            {
                CorrelativoData response = await cajaModel.correlativoSerie<CorrelativoData>(ConfigModel.asignacionPersonal.idCaja, 1);
                textNOperacion.Text = String.Format("{0} - {1}", response.serie, response.correlativoActual);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region ================================= Validator ====================================
        private void verificarCaja()
        {
            if (ConfigModel.cajaIniciada)
            {
                lblCajaEstado.Visible = false;
            }
            else
            {
                Validator.labelAlert(lblCajaEstado, 0, "No se inició la caja");
                lblCajaEstado.Visible = true;
                btnAceptar.Enabled = false;
            }
        }
        #endregion

        #region ========================== SAVE AND UPDATE ===========================
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            guardarIngreso();
        }
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
        private async void guardarIngreso()
        {
            bloquear(true);
            if (!validarCampos()) { bloquear(false); return; }
            try
            {
                if (nuevo)
                {
                    crearObjetoIngreso();
                    Response response = await ingresoModel.guardarEnUno(currentSaveObject);
                    MessageBox.Show(response.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //No se puede modificar
                    //Response response = await ingresoModel.modificarEnUno(currentSaveObject);
                    //MessageBox.Show(response.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                bloquear(false);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bloquear(false);
            }
        }

        private void crearObjetoIngreso()
        {
            currentSaveObject = new SaveObject();
            currentSaveObject.estado = 1;
            currentSaveObject.fechaPago = dtpFechaPago.Value.ToString("yyyy-MM-dd HH':'mm':'ss");
            currentSaveObject.idCaja = ConfigModel.asignacionPersonal.idCaja;
            currentSaveObject.idCajaSesion = ConfigModel.cajaSesion.idCajaSesion;
            currentSaveObject.idMedioPago = mediosDePagos[0].idMedioPago;
            currentSaveObject.idMoneda = Convert.ToInt32(cbxMoneda.SelectedValue);
            currentSaveObject.medioPago = mediosDePagos[0].nombre;
            currentSaveObject.moneda = cbxMoneda.Text;
            currentSaveObject.monto = textMonto.Text;
            currentSaveObject.motivo = textMotivo.Text;
            currentSaveObject.numeroOperacion = textNOperacion.Text;
            currentSaveObject.observacion = textObcervacion.Text;
            currentSaveObject.personal = PersonalModel.personal.nombres;

            // currentSaveObject
            currentSaveObject.idMoneda = Convert.ToInt32(cbxMoneda.SelectedValue);

        }

        private bool validarCampos()
        {
            if (textNOperacion.Text == "")
            {
                errorProvider1.SetError(textNOperacion, "Este campo esta vacía");
                textNOperacion.Focus();
                return false;
            }
            errorProvider1.Clear();

            // validacion monto
            if (textMonto.Text.Trim() == "")
            {
                errorProvider1.SetError(textMonto, "Campo obligatorio");
                Validator.textboxValidateColor(textMonto, 0);
                return false;
            }
            errorProvider1.Clear();
            Validator.textboxValidateColor(textMonto, 1);

            // Toda las validaciones correctas
            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region ========================= Decoration and value defaults =========================
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panel2, 157, 157, 157);
        }
        #endregion

        #region ============================ Validacion timpo real ============================
        private void textMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isDecimal(e, textMonto.Text);
        }
        private void textMonto_Validated(object sender, EventArgs e)
        {
            if (textMonto.Text.Trim() == "")
            {
                errorProvider1.SetError(textMonto, "Campo obligatorio");
                Validator.textboxValidateColor(textMonto, 0);
                return;
            }
            errorProvider1.Clear();
            Validator.textboxValidateColor(textMonto, 1);
        }

        private void textMotivo_Validated(object sender, EventArgs e)
        {
        }
        #endregion

        private void FormIngresoNuevo_Shown(object sender, EventArgs e)
        {
            textMonto.Focus();
        }
    }
    class SaveObject
    {
        // public int idIngreso { get; set; }
        public int estado { get; set; }
        public string fechaPago { get; set; }
        public int idCaja { get; set; }
        public int idCajaSesion { get; set; }
        public int idMedioPago { get; set; }
        public int idMoneda { get; set; }
        public string medioPago { get; set; }
        public string moneda { get; set; }
        public string monto { get; set; }
        public string motivo { get; set; }
        public string numeroOperacion { get; set; }
        public string observacion { get; set; }
        public string personal { get; set; }
    }

}
