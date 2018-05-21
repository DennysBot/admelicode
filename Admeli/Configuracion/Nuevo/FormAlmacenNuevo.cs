using Admeli.Componentes;
using Entidad;
using Entidad.Location;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admeli.Configuracion.Nuevo
{
    public partial class FormAlmacenNuevo : Form
    {
        private SucursalModel sucursalModel = new SucursalModel();
        private AlmacenModel almacenModel = new AlmacenModel();
        private LocationModel locationModel = new LocationModel();

        private List<LabelUbicacion> labelUbicaciones { get; set; }
        private UbicacionGeografica ubicacionGeografica { get; set; }

        private int currentIDAlmacen { get; set; }
        private bool nuevo { get; set; }
        private Almacen currentAlmacen { get; set; }


        public FormAlmacenNuevo()
        {
            InitializeComponent();
            this.nuevo = true;
        }

        public FormAlmacenNuevo(Almacen currentAlmacen)
        {
            InitializeComponent();
            this.currentAlmacen = currentAlmacen;
            this.currentIDAlmacen = currentAlmacen.idAlmacen;
            this.nuevo = false;
            mostrarDatosModificar();
        }

        private void mostrarDatosModificar()
        {
            textNombreAlmacen.Text = currentAlmacen.nombre;
            textDirecionAlmacen.Text = currentAlmacen.direccion;
            chkPrincipalAlmacen.Checked = currentAlmacen.principal;
            chkActivoAlmacen.Checked = Convert.ToBoolean(currentAlmacen.estado);
            
        }

        #region ======================= Paint =======================
        private void panelFooter_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.topLine(panelFooter);
        }

        private void FormAlmacenNuevo_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panelLevelPais, 157, 157, 157);
            drawShape.lineBorder(panelLevel1, 157, 157, 157);
            drawShape.lineBorder(panelLevel2, 157, 157, 157);
            drawShape.lineBorder(panelLevel3, 157, 157, 157);
            drawShape.lineBorder(panel2, 157, 157, 157);
        }
        #endregion

        #region ======================== Load Root ========================
        private void FormAlmacenNuevo_Load(object sender, EventArgs e)
        {
            loadRootData();
        }

        private async void loadRootData()
        {
            try
            {
                cargarComponentes1();
                await cargarPaises();
                crearNivelesPais();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "loadRootData", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region =========================== Load ===========================
        private async void cargarComponentes1()
        {
            try
            {
                sucursalBindingSource.DataSource = await sucursalModel.sucursales();
                if (!nuevo)
                {

                    cbxSucursal.SelectedValue = currentAlmacen.idSucursal;

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargarComponentes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task cargarPaises()
        {
            try
            {
                // cargando los paises
                paisBindingSource.DataSource = await locationModel.paises();

                // cargando la ubicacion geografica por defecto
                if (nuevo)
                {
                    ubicacionGeografica = await locationModel.ubigeoActual(ConfigModel.sucursal.idUbicacionGeografica);
                }
                else
                {
                    ubicacionGeografica = await locationModel.ubigeoActual(currentAlmacen.idUbicacionGeografica);
                }
                cbxPaises.SelectedValue = ubicacionGeografica.idPais;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargarPaises", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            catch (Exception)
            {
                // MessageBox.Show(ex.Message);
            }
            finally
            {
                loadStateApp(false);
            }
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
                desactivarNivelDesde(3);
                loadStateApp(false);
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

        #region ==================== Estados =====================
        private void loadStateApp(bool state)
        {
            if (state)
            {
                progressBarApp.Style = ProgressBarStyle.Marquee;
                Cursor.Current = Cursors.WaitCursor;
            }
            else
            {
                progressBarApp.Style = ProgressBarStyle.Blocks;
                Cursor.Current = Cursors.Default;
            }


        }
        #endregion


        private void btnSucursalNuevo_Click(object sender, EventArgs e)
        {
            FormSucursalNuevo sucursalNuevo = new FormSucursalNuevo();
            sucursalNuevo.ShowDialog();
            cargarComponentes1();
        }

        #region ========================== SAVE AND UPDATE ===========================
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            guardarSucursal();
        }
        
        private async void guardarSucursal()
        {
            Bloqueo.bloquear(this, true);
            if (!validarCampos()) { Bloqueo.bloquear(this, false); return; }
            try
            {
                // Validacion
                int almacenID = (nuevo) ? 0 : currentIDAlmacen;
                int sucursalID = Convert.ToInt32(cbxSucursal.SelectedValue);

                List<Almacen> listAlmacenes = await almacenModel.verificarAlmacen(textNombreAlmacen.Text.Trim(), sucursalID, almacenID);
                if (listAlmacenes.Count > 0)
                {
                    errorProvider1.SetError(textNombreAlmacen, "Ya existe un almacén con mismo nombre.");
                    Validator.textboxValidateColor(textNombreAlmacen, 0);
                    return;
                }
                errorProvider1.Clear();
                Validator.textboxValidateColor(textNombreAlmacen, 1);

                // Procediendo con el guardado
                crearObjetoSucursal();
                if (nuevo)
                {
                    Response response = await almacenModel.guardar(ubicacionGeografica, currentAlmacen);
                    MessageBox.Show(response.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Response response = await almacenModel.modificar(ubicacionGeografica, currentAlmacen);
                    MessageBox.Show(response.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Bloqueo.bloquear(this, false);
            }
        }

        private void crearObjetoSucursal()
        {
            currentAlmacen = new Almacen();

            if (!nuevo) currentAlmacen.idAlmacen = currentIDAlmacen; // Llenar el id categoria cuando este en esdo modificar

            currentAlmacen.idSucursal = Convert.ToInt32(cbxSucursal.SelectedValue);
            currentAlmacen.nombreSucursal = cbxSucursal.Text;
            currentAlmacen.nombre = textNombreAlmacen.Text;
            currentAlmacen.direccion = textDirecionAlmacen.Text;
            currentAlmacen.principal = chkPrincipalAlmacen.Checked;
            currentAlmacen.estado = Convert.ToInt32(chkActivoAlmacen.Checked);
            currentAlmacen.tieneRegistros = "0";



            // Ubicacion geografica
            ubicacionGeografica.idPais = (cbxPaises.SelectedIndex == -1) ? ubicacionGeografica.idPais : Convert.ToInt32(cbxPaises.SelectedValue);
            ubicacionGeografica.idNivel1 = (cbxNivel1.SelectedIndex == -1) ? ubicacionGeografica.idNivel1 : Convert.ToInt32(cbxNivel1.SelectedValue);
            ubicacionGeografica.idNivel2 = (cbxNivel2.SelectedIndex == -1) ? ubicacionGeografica.idNivel2 : Convert.ToInt32(cbxNivel2.SelectedValue);
            ubicacionGeografica.idNivel3 = (cbxNivel3.SelectedIndex == -1) ? ubicacionGeografica.idNivel3 : Convert.ToInt32(cbxNivel3.SelectedValue);
        }

        private bool validarCampos()
        {

            if (textNombreAlmacen.Text.Trim() == "")
            {
                errorProvider1.SetError(textNombreAlmacen, "Campo obligatorio");
                Validator.textboxValidateColor(textNombreAlmacen, 0);
                return false;
            }
            errorProvider1.Clear();
            Validator.textboxValidateColor(textNombreAlmacen, 1);

            switch (labelUbicaciones.Count)
            {
                case 1:
                    if (cbxNivel1.SelectedIndex == -1)
                    {
                        errorProvider1.SetError(cbxNivel1, "No se seleccionó ningún elemento");
                        cbxNivel1.Focus();
                        return false;
                    }
                    errorProvider1.Clear();
                    break;
                case 2:
                    if (cbxNivel2.SelectedIndex == -1)
                    {
                        errorProvider1.SetError(cbxNivel2, "No se seleccionó ningún elemento");
                        cbxNivel2.Focus();
                        return false;
                    }
                    errorProvider1.Clear();
                    break;
                case 3:
                    if (cbxNivel3.SelectedIndex == -1)
                    {
                        errorProvider1.SetError(cbxNivel3, "No se seleccionó ningún elemento");
                        cbxNivel3.Focus();
                        return false;
                    }
                    errorProvider1.Clear();
                    break;
                default:
                    break;
            }

            if (textDirecionAlmacen.Text == "")
            {
                errorProvider1.SetError(textDirecionAlmacen, "Este campo esta bacía");
                textDirecionAlmacen.Focus();
                return false;
            }
            errorProvider1.Clear();

            if (textDirecionAlmacen.Text.Trim() == "")
            {
                errorProvider1.SetError(textDirecionAlmacen, "Campo obligatorio");
                Validator.textboxValidateColor(textDirecionAlmacen, 0);
                return false;
            }
            errorProvider1.Clear();
            Validator.textboxValidateColor(textDirecionAlmacen, 1);
            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region =============================== Validacion Tiempo Real ===============================
        private async void textNombreAlmacen_Validated(object sender, EventArgs e)
        {
            if (textNombreAlmacen.Text.Trim() == "")
            {
                errorProvider1.SetError(textNombreAlmacen, "Campo obligatorio");
                Validator.textboxValidateColor(textNombreAlmacen, 0);
                return;
            }
            errorProvider1.Clear();
            Validator.textboxValidateColor(textNombreAlmacen, 1);

            ////
            try
            {
                int almacenID = (nuevo) ? 0 : currentIDAlmacen;
                int sucursalID = Convert.ToInt32(cbxSucursal.SelectedValue);

                List<Almacen> listAlmacenes = await almacenModel.verificarAlmacen(textNombreAlmacen.Text, sucursalID, almacenID);
                if (listAlmacenes.Count > 0)
                {
                    errorProvider1.SetError(textNombreAlmacen, "Ya existe un almacén con mismo nombre.");
                    Validator.textboxValidateColor(textNombreAlmacen, 0);
                    return;
                }
                errorProvider1.Clear();
                Validator.textboxValidateColor(textNombreAlmacen, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textDirecionAlmacen_Validated(object sender, EventArgs e)
        {
            if (textDirecionAlmacen.Text.Trim() == "")
            {
                errorProvider1.SetError(textDirecionAlmacen, "Campo obligatorio");
                Validator.textboxValidateColor(textDirecionAlmacen, 0);
                return;
            }
            errorProvider1.Clear();
            Validator.textboxValidateColor(textDirecionAlmacen, 1);
        }
        #endregion

        private void FormAlmacenNuevo_Shown(object sender, EventArgs e)
        {
            cbxSucursal.Focus();
        }

        private void textNombreAlmacen_OnValueChanged(object sender, EventArgs e)
        {

        }
    }
}
