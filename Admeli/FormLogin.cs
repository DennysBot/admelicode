using Admeli.Componentes;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//es un codigo de comentario

namespace Admeli
{
    public partial class FormLogin : Form
    {
        private PersonalModel personalModel = new PersonalModel();
        private FormPrincipal formHome { get; set; }
        private FormPrincipal formHomeDarck { get; set; }

        private SucursalModel sucursalModel = new SucursalModel();
        private ConfigModel configModel = new ConfigModel();

        private int nLoads { get; set; }
        private int currentLoad { get; set; }

        public FormLogin()
        {
            InitializeComponent();
            //Configuracion de decimales
            this.configuracionRegional();
            this.nLoads = 0;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            //nuevo
            try
            {
                btnLogin.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                if (validarCampos())
                {
                    await personalModel.loginPersonal(textUsuario.Text, textPassword.Text);
                   
                    // cargar componentes desde el webservice
                    await cargarComponente();

                    // esperar a que cargen todo los web service
                    await Task.Run(() =>
                    {
                        while (true)
                        {
                            Thread.Sleep(50);
                            if (nLoads >= 10) // IMPORTANTE IMPORTANTE el numero tiene que ser igual al numero de web service que se este llamando
                            {
                                break;
                            }
                        }
                    });

                    // Mostrar el formulario dependiendo de la cantidad de puntos de venta y almacenes


                    if (ConfigModel.listSucursales.Count == 1)
                    {


                        if (ConfigModel.puntosDeVenta.Count > 1 || ConfigModel.alamacenes.Count > 1)
                        {
                            //Ocultar este formulario
                            this.Hide();
                            FormConfigInicial formConfig = new FormConfigInicial(this);
                            formConfig.Show();
                        }
                        else
                        {

                            // Estableciendo el almacen y punto de venta al personal asignado
                            // este ya esta definido arriba

                            if (ConfigModel.puntosDeVenta.Count > 0) { ConfigModel.currentPuntoVenta = ConfigModel.puntosDeVenta[0].idAsignarPuntoVenta; }
                            else
                            {
                                ConfigModel.currentPuntoVenta = 0; 
                            }
                            if (ConfigModel.alamacenes.Count > 0) { ConfigModel.currentIdAlmacen = ConfigModel.alamacenes[0].idAlmacen; }
                            else
                            {

                                ConfigModel.currentIdAlmacen = 0;
                            }
                            //

                            // Ocultar este formulario
                            this.Hide();

                            // Mostrar el formulario principal
                            //formHomeDarck = new FormPrincipal(this);
                            //formHomeDarck.Show();
                            formHome = new FormPrincipal(this);
                            formHome.Show();
                            //FormPrueba formPrueba = new FormPrueba();
                            //formPrueba.Show();
                        }



                    }
                    else
                    {

                        if(ConfigModel.listSucursales.Count > 1)
                        {
                            this.Hide();
                            FormConfigInicial formConfig = new FormConfigInicial(this);
                            formConfig.Show();

                        }
                        else
                        {
                            MessageBox.Show(" no hay sucursales ", "Login Personal", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }

                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Login Personal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                progressbar.Value = 0;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnLogin.Enabled = true;
            }
        }

        private async Task cargarComponente()
        {

            loadDatosGenerales();

            await configModel.loadSucursalPersonal(PersonalModel.personal.idPersonal);
            this.nLoads++;
            loadState("sucursales");

            await configModel.loadAsignacionPersonales(PersonalModel.personal.idPersonal, ConfigModel.sucursal.idSucursal);
            this.nLoads++;
            loadState("asignacion del personal");

            loadConfiGeneral();

            loadMonedas();

            loadTipoCambioMonedas();

            loadTipoDocumento();

            loadAlmacenes();

            loadPuntoDeVenta();

            loadCajaSesion();

            // await configModel.loadCierreIngresoEgreso(1, ConfigModel.cajaSesion.idCajaSesion); // Falta Buscar de donde viene el primer parametro
        }
        private async void loadDatosGenerales()
        {
            await configModel.loadDatosGenerales();
            this.nLoads++;
            loadState("datos generales");
        }

        private async void loadConfiGeneral()
        {
            await configModel.loadConfiGeneral();
            this.nLoads++;
            loadState("configuracion general");
        }

        private async void loadCajaSesion()
        {
            await configModel.loadCajaSesion(ConfigModel.asignacionPersonal.idAsignarCaja);
            this.nLoads++;
            loadState("caja session");
        }

        private async void loadPuntoDeVenta()
        {
            await configModel.loadPuntoDeVenta(PersonalModel.personal.idPersonal, ConfigModel.sucursal.idSucursal);
            this.nLoads++;
            loadState("puntos de venta");
        }

        private async void loadAlmacenes()
        {
            await configModel.loadAlmacenes(PersonalModel.personal.idPersonal, ConfigModel.sucursal.idSucursal);
            this.nLoads++;
            loadState("almacenes");
        }

        private async void loadTipoDocumento()
        {
            await configModel.loadTipoDocumento();
            this.nLoads++;
            loadState("tipos de documentos");
        }

        private async void loadTipoCambioMonedas()
        {
            await configModel.loadTipoCambioMonedas();
            this.nLoads++;
            loadState("tipos de cambio");
        }

        private async void loadMonedas()
        {
            await configModel.loadMonedas();
            this.nLoads++;
            loadState("monedas");
        }

        private void loadState(string message)
        {
            progressbar.Value += 10;
            lblProgress.Text = String.Format("Cargando {0}", message);
        }

        private bool validarCampos()
        {
            if (textUsuario.Text.Trim() == "")
            {
                errorProvider1.SetError(textUsuario, "Campo obligatorio");
                Validator.textboxValidateColor(textUsuario, 2);
                textUsuario.Focus();
                return false;
            }
            errorProvider1.Clear();

            if (textPassword.Text.Trim() == "")
            {
                errorProvider1.SetError(textPassword, "Campo obligatorio");
                Validator.textboxValidateColor(textPassword, 2);
                textPassword.Focus();
                return false;
            }
            errorProvider1.Clear();

            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
        private void configuracionRegional()
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-PE");
            culture.NumberFormat.CurrencyDecimalSeparator = ".";
            culture.NumberFormat.CurrencyGroupSeparator = "";
            culture.NumberFormat.NumberDecimalSeparator = ".";
            culture.NumberFormat.NumberGroupSeparator = "";
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
        }
    }
}
