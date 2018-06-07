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
using Entidad.Configuracion;
using Entidad.Location;
using Entidad;
using Admeli.Componentes;

namespace Admeli.Configuracion
{
    public partial class UCDatosEmpresa : UserControl
    {
        private FormPrincipal formPrincipal;
        public bool lisenerKeyEvents { get; set; }

        private ConfigModel configModel = new ConfigModel();
        private LocationModel locationModel = new LocationModel();

        private DatosGenerales datosGenerales { get; set; }
        private ConfiguracionGeneral configuracionGeneral { get; set; }
        private List<LabelUbicacion> labelUbicaciones { get; set; }
        private UbicacionGeografica ubicacionGeografica { get; set; }

        private Pais pais { get; set; }

        #region ========================= Constructor =========================
        public UCDatosEmpresa()
        {
            InitializeComponent();
            lisenerKeyEvents = true; // Active lisener key events
        }

        public UCDatosEmpresa(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;
            lisenerKeyEvents = true; // Active lisener key events
        }
        #endregion

        #region ============================ Loads ============================
        private void UCDatosEmpresa_Load(object sender, EventArgs e)
        {
            reLoad();
        }

        private async Task cargarPaises()
        {
            try
            {
                // cargando los paises
                paisBindingSource.DataSource = await locationModel.paises();

                // cargando la ubicacion geografica por defecto
                ubicacionGeografica = await locationModel.ubigeoActual(ConfigModel.datosGenerales.idUbicacionGeografica);
                cbxPaises.SelectedValue = ubicacionGeografica.idPais;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, " Cargar Paises", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        internal async void reLoad(bool refreshData = true)
        {
            try
            {
                if (refreshData)
                {
                    loadStateApp(true);
                    await cargarPaises();
                    crearNivelesPais();
                    // Cargando los datos generales
                    datosGenerales = await configModel.getDatosGenerales();

                    // Cargando la configuracion general
                    configuracionGeneral = await configModel.getConfiGeneral();

                    lisenerKeyEvents = true; // Active lisener key events

                    // MOstrando los datos en los campos
                    mostrarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, " reload Cargar Paises", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void mostrarDatos()
        {
            textNombreEmpresa.Text = datosGenerales.razonSocial;
            textNumeroIdentificacion.Text = datosGenerales.ruc;
            textEmail.Text = datosGenerales.email;
            textDireccion.Text = datosGenerales.direccion;
            textFormaDePago.Text = datosGenerales.cuentaBancaria;

            textItemPagina.Text = configuracionGeneral.itemPorPagina.ToString();
            textNumeroDigitos.Text = configuracionGeneral.numeroDecimales.ToString();
            textPorcentajeUtilidad.Text = configuracionGeneral.porcentajeUtilidad;
            chkArquearMarcador.Checked = configuracionGeneral.arquearMarcador;
            textVersionSistema.Text = configuracionGeneral.version;
            textNombreEmpresa.Padding = new Padding(3, 10, 20, 2);
        }
        #endregion

        #region ==================== Estados =====================
        private void loadStateApp(bool state)
        {
            if (state)
            {
                formPrincipal.appLoadState(state);
                Cursor.Current = Cursors.WaitCursor;
            }
            else
            {
                formPrincipal.appLoadState(state);
                Cursor.Current = Cursors.Default;
            }
            this.Enabled = !state;
        }
        #endregion

        #region ================== Formando los niveles de cada pais ==================
        private async void crearNivelesPais()
        {
            try
            {
                loadStateApp(true);
                labelUbicaciones = await locationModel.labelUbicacion(Convert.ToInt32(cbxPaises.SelectedValue));
                ocultarNiveles(); // Ocultando todo los niveles

                // Mostrando los niveles uno por uno
                if (labelUbicaciones.Count >= 1)
                {
                    lblNivel1.Text = labelUbicaciones[0].denominacion;
                    panelLevel1.Visible = true;
                }

                if (labelUbicaciones.Count >= 2)
                {
                    lblNivel2.Text = labelUbicaciones[1].denominacion;
                    panelLevel2.Visible = true;
                }

                if (labelUbicaciones.Count >= 3)
                {
                    panelLevel3.Visible = true;
                    lblNivel3.Text = labelUbicaciones[2].denominacion;
                }

                // Cargar el primer nivel de la localizacion
                cargarNivel1();

            }
            catch (Exception){}
        }

        private void ocultarNiveles()
        {
            panelLevel1.Visible = false;
            panelLevel2.Visible = false;
            panelLevel3.Visible = false;

            cbxNivel1.SelectedIndex = -1;
            cbxNivel2.SelectedIndex = -1;
            cbxNivel3.SelectedIndex = -1;
        }

        private async void cargarNivel1()
        {
            try
            {
                // No cargar directo al comobobox esto causara que el evento SelectedIndexChange de forma automatica
                if (labelUbicaciones.Count < 1) return;
                loadStateApp(true);
                nivel1BindingSource.DataSource = await locationModel.nivel1(Convert.ToInt32(cbxPaises.SelectedValue));
                if (ubicacionGeografica.idNivel1 > 0)
                {
                    cbxNivel1.SelectedValue = ubicacionGeografica.idNivel1;
                }
                else
                {
                    cbxNivel1.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upps! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadStateApp(false);
                desactivarNivelDesde(2);
            }
        }

        private async void cargarNivel2()
        {
            try
            {
                if (labelUbicaciones.Count < 2) return;
                loadStateApp(true);
                nivel2BindingSource.DataSource = await locationModel.nivel2(Convert.ToInt32(cbxNivel1.SelectedValue));
                if (ubicacionGeografica.idNivel2 > 0)
                {
                    cbxNivel2.SelectedValue = ubicacionGeografica.idNivel2;
                }
                else
                {
                    cbxNivel2.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upps! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadStateApp(false);
                desactivarNivelDesde(3);
            }
        }

        private async void cargarNivel3()
        {
            try
            {
                if (labelUbicaciones.Count < 3) return;
                loadStateApp(true);
                nivel3BindingSource.DataSource = await locationModel.nivel3(Convert.ToInt32(cbxNivel2.SelectedValue));
                if (ubicacionGeografica.idNivel3 > 0)
                {
                    cbxNivel3.SelectedValue = ubicacionGeografica.idNivel3;
                }
                else
                {
                    cbxNivel3.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upps! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadStateApp(false);
                desactivarNivelDesde(4);
            }
        }

        private void desactivarNivelDesde(int n)
        {
            cbxNivel1.Enabled = true;
            cbxNivel2.Enabled = true;
            cbxNivel3.Enabled = true;

            if (n < 2) cbxNivel1.Enabled = false;
            if (n < 3) cbxNivel2.Enabled = false;
            if (n < 4) cbxNivel3.Enabled = false;
        }
        #endregion

        #region  ======================= Eventos cargar paises =======================
        private void cbxNivel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarNivel2();
        }

        private void cbxNivel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarNivel3();
        }

        private void cbxPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            crearNivelesPais();
        }
        #endregion

        #region ============================ Guardar ============================
        private void actualizarObjetoDatos()
        {
            // Actualizando los datos generales
            datosGenerales.razonSocial = textNombreEmpresa.Text;
            datosGenerales.ruc = textNumeroIdentificacion.Text;
            datosGenerales.email = textEmail.Text;
            datosGenerales.direccion = textDireccion.Text;
            datosGenerales.cuentaBancaria = textFormaDePago.Text;

            // Ubicacion geografica
            ubicacionGeografica.idPais = (cbxPaises.SelectedIndex == -1) ? ubicacionGeografica.idPais : Convert.ToInt32(cbxPaises.SelectedValue);
            ubicacionGeografica.idNivel1 = (cbxNivel1.SelectedIndex == -1) ? ubicacionGeografica.idNivel1 : Convert.ToInt32(cbxNivel1.SelectedValue);
            ubicacionGeografica.idNivel2 = (cbxNivel2.SelectedIndex == -1) ? ubicacionGeografica.idNivel2 : Convert.ToInt32(cbxNivel2.SelectedValue);
            ubicacionGeografica.idNivel3 = (cbxNivel3.SelectedIndex == -1) ? ubicacionGeografica.idNivel3 : Convert.ToInt32(cbxNivel3.SelectedValue);
        }
        private void actualizarObjetoConfiguracion()
        {
            // Actualizando las configuraciones generales
            configuracionGeneral.numeroDecimales = Convert.ToInt32(textNumeroDigitos.Text);
            configuracionGeneral.itemPorPagina = Convert.ToInt32(textItemPagina.Text);
            configuracionGeneral.porcentajeUtilidad = textPorcentajeUtilidad.Text;
            configuracionGeneral.arquearMarcador = chkArquearMarcador.Checked;
            configuracionGeneral.version = textVersionSistema.Text;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!validarCamposDatos()) { return; }
            executeGuardarDatos();

        }
        private bool validarCamposDatos()
        {
            errorProvider1.Clear();
            if (textNombreEmpresa.Text.Trim() == "")
            {
                errorProvider1.SetError(textNombreEmpresa, "Campo obligatorio");
                return false;
            }
            if (textNumeroIdentificacion.Text.Trim() == "")
            {
                errorProvider1.SetError(textNumeroIdentificacion, "Campo obligatorio");
                return false;
            }
            if (textEmail.Text.Trim() == "")
            {
                errorProvider1.SetError(textEmail, "Campo obligatorio");
                return false;
            }
            if (cbxPaises .SelectedIndex == -1)
            {
                errorProvider1.SetError(panelLevelPais, "Campo obligatorio");
                return false;
            }
            if (cbxNivel1.SelectedIndex == -1)
            {
                errorProvider1.SetError(panelLevel1, "Campo obligatorio");
                return false;
            }
            if (textDireccion .Text.Trim() == "")
            {
                errorProvider1.SetError(textDireccion, "Campo obligatorio");
                return false;
            }
            return true;
        }
        private bool validarCamposConfiguracion()
        {
            errorProvider1.Clear();
            if (textNumeroDigitos.Text.Trim() == "")
            {
                errorProvider1.SetError(textNumeroDigitos, "Campo obligatorio");
                return false;
            }
            if (textItemPagina.Text.Trim() == "")
            {
                errorProvider1.SetError(textItemPagina, "Campo obligatorio");
                return false;
            }
            if (textPorcentajeUtilidad.Text.Trim() == "")
            {
                errorProvider1.SetError(textPorcentajeUtilidad, "Campo obligatorio");
                return false;
            }
            if (textVersionSistema.Text.Trim() == "")
            {
                errorProvider1.SetError(textVersionSistema, "Campo obligatorio");
                return false;
            }
            return true;
        }

        private async void executeGuardarDatos()
        {
            loadStateApp(true);
            try
            {
                actualizarObjetoDatos();
                Response responseDG = await configModel.guardarDatosGenerales(ubicacionGeografica, datosGenerales);
                MessageBox.Show(responseDG.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadStateApp(false);
            }

        }
        private async void executeGuardarConfiguracion()
        {
            loadStateApp(true);
            try
            {
                actualizarObjetoConfiguracion();
                Response responseCG = await configModel.guardarConfigGeneral(configuracionGeneral);
                MessageBox.Show(responseCG.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadStateApp(false);
            }

        }
        #endregion

        #region =========================== Validacion Tiempo Real ===========================
        private void textNombreEmpresa_Validated(object sender, EventArgs e)
        {
            if (textNombreEmpresa.Text.Trim() == "")
            {
                errorProvider1.SetError(textNombreEmpresa, "Campo obligatorio");
                Validator.textboxValidateColor(textNombreEmpresa, 0);
                return;
            }
            errorProvider1.Clear();
            Validator.textboxValidateColor(textNombreEmpresa, 1);
        }

        private void textEmail_Validated(object sender, EventArgs e)
        {
            if (textEmail.Text.Trim() != "")
            {
                if (!Validator.IsValidEmail(textEmail.Text))
                {
                    errorProvider1.SetError(textEmail, "Email invválido");
                    Validator.textboxValidateColor(textEmail, 0);
                    return;
                }
            }
            errorProvider1.Clear();
            Validator.textboxValidateColor(textEmail, 1);
        }

        private void textNumeroIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private void textNumeroDigitos_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private void textItemPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }
        #endregion

        #region ======================== PAINT ========================
        private void panelLevelPais_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panelLevelPais, 157, 157, 157);
        }

        private void panelLevel1_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panelLevel1, 157, 157, 157);
        }

        private void panelLevel2_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panelLevel2, 157, 157, 157);
        }

        private void panelLevel3_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panelLevel3, 157, 157, 157);
        }
        #endregion

        private void btnSuburFoto_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardarConfiguracion_Click(object sender, EventArgs e)
        {
            if (!validarCamposConfiguracion()) { return; }
            executeGuardarConfiguracion();
        }
    }
}
