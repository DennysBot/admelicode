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
using Entidad;
using Entidad.Configuracion;

namespace Admeli
{
    public partial class FormConfigInicial : Form
    {
        public FormLogin formLogin { get; set; }
        private SucursalModel sucursalModel = new SucursalModel();
        private ConfigModel configModel = new ConfigModel();
        private List<Almacen> listAlmacenes { get; set; }
        private List<PuntoDeVenta> listpuntos { get; set; }
        private int nLoads { get; set; }
        public FormConfigInicial()
        {
            InitializeComponent();
            nLoads = 0;
            listAlmacenes = new List<Almacen>();
            listpuntos = new List<PuntoDeVenta>();
        }

        public FormConfigInicial(FormLogin formLogin)
        {
            this.formLogin = formLogin;
            InitializeComponent();
            nLoads = 0;
            listAlmacenes = new List<Almacen>();
            listpuntos = new List<PuntoDeVenta>();

        }

        private async void FormConfigInicial_Shown(object sender, EventArgs e)
        {


        }

        private async void btnContinuar_Click(object sender, EventArgs e)
        {

            try
            {
                btnContinuar.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                if (validarCampos())
                {


                    // cargar componentes desde el webservice
                    await cargarComponente();

                    // esperar a que cargen todo los web service
                    await Task.Run(() =>
                    {
                        while (true)
                        {
                            Thread.Sleep(50);
                            if (nLoads >= 4) // IMPORTANTE IMPORTANTE el numero tiene que ser igual al numero de web service que se este llamando
                            {
                                break;
                            }
                        }
                    });

                  

                    // Estableciendo el almacen y punto de venta al personal asignado
                    ConfigModel.currentIdAlmacen = Convert.ToInt32(cbxAlmacenes.SelectedValue.ToString());
                    
                    ConfigModel.currentPuntoVenta = cbxPuntosVenta.SelectedValue!=null ? Convert.ToInt32(cbxPuntosVenta.SelectedValue.ToString()):0;

                    // Mostrando el formulario principal
                    this.Hide();
                    FormPrincipal formPrincipal = new FormPrincipal(this.formLogin);
                    formPrincipal.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "configuracion Inicial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                progressbar.Value = 0;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnContinuar.Enabled = true;
            }


        }



        private async Task cargarComponente()
        {
            loadState("asignacion del personal");
            await configModel.loadAsignacionPersonales(PersonalModel.personal.idPersonal, ConfigModel.sucursal.idSucursal);
            this.nLoads++;
            loadCajaSesion();
            loadPuntoDeVenta();
            loadAlmacenes();
            // await configModel.loadCierreIngresoEgreso(1, ConfigModel.cajaSesion.idCajaSesion); // Falta Buscar de donde viene el primer parametro
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


        private void loadState(string message)
        {
            progressbar.Value += 10;
            lblProgress.Text = String.Format("Cargando {0}", message);
        }

        private bool validarCampos()
        {
            if (cbxAlmacenes.Text.Trim() == "")
            {
                errorProvider1.SetError(cbxAlmacenes, "Campo obligatorio");

                cbxAlmacenes.Focus();
                return false;
            }
            errorProvider1.Clear();

          

            return true;
        }










        #region =============================== Paint ===============================
        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panel2);
            drawShape.lineBorder(panel3);
        }
        #endregion

        private void btnCLose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormConfigInicial_Load(object sender, EventArgs e)
        {
            cargar();
        }

        public async void cargar() {

            btnContinuar.Enabled = false;
            cbxPuntosVenta.Enabled = false;
            cbxAlmacenes.Enabled = false;
            await configModel.loadAlmacenes(PersonalModel.personal.idPersonal, 0);
            listAlmacenes = ConfigModel.alamacenes;


            foreach (Almacen A in listAlmacenes)
            {
                A.nombreSucursal = ConfigModel.listSucursales.Find(X => X.idSucursal == A.idSucursal).nombre;

            }

            await configModel.loadPuntoDeVenta(PersonalModel.personal.idPersonal, 0);
            listpuntos = ConfigModel.puntosDeVenta;


            cbxPuntosVenta.Enabled = true;
            cbxAlmacenes.Enabled = true;
            if (listAlmacenes.Count == 0)
            {        
                foreach(Sucursal S in        ConfigModel.listSucursales)
                {

                    Almacen almacen = new Almacen();
                    almacen.idAlmacen = 0;
                    almacen.idSucursal = S.idSucursal;
                    almacen.nombre = "Ninguno ";
                    almacen.nombreSucursal =S.nombre;
                    listAlmacenes.Add(almacen);
                    



                }
                
            }

            almacenBindingSource.DataSource = listAlmacenes;
            cbxAlmacenes.SelectedIndex = -1;
          

            puntoDeVentaBindingSource.DataSource = listpuntos;
            cbxPuntosVenta.SelectedIndex = -1; 

            cbxAlmacenes.SelectedIndex = 0;
            btnContinuar.Enabled = true;
            btnContinuar.Focus();
        }

        private void cbxAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAlmacenes.SelectedIndex == -1) return;
            if (listAlmacenes.Count > 0)
            {
                Almacen almacen = null;
                if ((int)cbxAlmacenes.SelectedValue == 0)
                {
                    almacen = listAlmacenes[cbxAlmacenes.SelectedIndex];

                }
                else
                {

                    almacen = listAlmacenes.Find(X => X.idAlmacen == (int)cbxAlmacenes.SelectedValue);
                }
                
                ConfigModel.sucursal=ConfigModel.listSucursales.Find(X => X.idSucursal == almacen.idSucursal);
                List<PuntoDeVenta> list = listpuntos.Where(X => X.idSucursal == ConfigModel.sucursal.idSucursal).ToList();
                if (list.Count == 0)
                {

                    cbxPuntosVenta.SelectedIndex = -1;
                    puntoDeVentaBindingSource.DataSource = null;

                    puntoDeVentaBindingSource.DataSource = list;
                    cbxPuntosVenta.Text = "no hay punto de venta";


                }
                else
                {

                    puntoDeVentaBindingSource.DataSource = list;
                }


            }
           
          

           
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panel2, 157, 157, 157);
            drawShape.lineBorder(panel3, 157, 157, 157);
        }

        private void cbxPuntosVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxPuntosVenta.SelectedIndex==-1)
            if (listAlmacenes.Count == 0)
            {
                    int idSucursal = listpuntos.Find(X => X.idPuntoVenta == (int)cbxPuntosVenta.SelectedValue).idSucursal;
                    ConfigModel.sucursal= ConfigModel.listSucursales.Find(X => X.idSucursal == idSucursal);
            }
        }
    }
}
