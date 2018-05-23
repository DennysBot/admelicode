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
using Admeli.Compras;
using Admeli.Configuracion.Modificar;
using Admeli.NavDarck;
using Admeli.Navigation;
using Admeli.Productos;
using Admeli.Ventas;
using Entidad;
using Entidad.Configuracion;
using Modelo;
 
namespace Admeli
{
    public partial class FormPrincipal : Form
    {

        private UCCompras uCCompras;
        private UCTiendaRoot uCTiendaRoot { get; set; }
        private UCMessageRoot uCMessageRoot { get; set; }
        private UCConfigRoot uCConfigRoot { get; set; }
        // private UCOTro uCTiendaRoot { get; set; }


        private UCConfigNav uCConfigNav;
        private UCHome uCHome;

        // Formularios
        private FormLogin formLogin;

        // Accessos directos
        private UCVentas uCVentas;
        private UCListadoProducto uCListadoProducto;
       
        

        // Modelos
        private SucursalModel sucursalModel = new SucursalModel();
        private ConfigModel configModel = new ConfigModel();
        private PersonalModel personalModel = new PersonalModel();
        private SunatModel sunatModel = new SunatModel();
        private CajaModel cajaModel = new CajaModel();

        //datos generales usados por todos los uc
        public static Asignacion asignacion;

        // Metodos
        private bool notCloseApp { get; set; }

        #region ============================ CONSTRUCTORS ============================
        public FormPrincipal()
        {
            InitializeComponent();
        }

        public FormPrincipal(FormLogin formLogin)
        {
            InitializeComponent();
            this.formLogin = formLogin;
        }
        #endregion

        #region ============================ ROOT LOAD ============================
        private void FormPrueba_Load(object sender, EventArgs e)
        {
            //Establecer formato de decimales
            this.fijarFormatoDecimal();
            this.reLoad();
           
            string almacen = ConfigModel.alamacenes.Find(X => X.idAlmacen == ConfigModel.currentIdAlmacen).nombre;

            PuntoDeVenta puntoDeVenta = ConfigModel.puntosDeVenta.Find(X => X.idPuntoVenta == ConfigModel.currentPuntoVenta);
            string punto = puntoDeVenta==null ? "":" - " + puntoDeVenta.nombre;
            this.Text += almacen + punto + " - " + ConfigModel.sucursal.nombre;
            this.lbinfo.Text = almacen + punto;


        }
        private void fijarFormatoDecimal()
        {
            int nroDec = ConfigModel.configuracionGeneral.numeroDecimales;
            string formato = "0.";
            for(int i = 0; i < nroDec; i++)
            {
                formato += "#";
            }
            ConfigModel.configuracionGeneral.formatoDecimales = formato;
        }

        private void reLoad()
        {
            /// mostrando el panel por defecto
            togglePanelMain("compras2");
            lblUserName.Text = PersonalModel.personal.usuario.ToUpper();
            lblDocumento.Text = String.Format("{0}", PersonalModel.personal.numeroDocumento);
            /// Foto Del Usuario

            /// Panel Aside por defecto
            toggleRootMenu("tienda");

            // Cargando datos en el panel derecho
            cargarDatosAsideRight();
            cargarAsignacion();
        }

        public async void cargarDatosAsideRight()
        {
            lblName.Text = PersonalModel.personal.nombres;
            lblLastName.Text = PersonalModel.personal.apellidos;
            lblDNI.Text = PersonalModel.personal.numeroDocumento;
            lblUsuario.Text = PersonalModel.personal.nombres;
            lblDocumentType.Text = PersonalModel.personal.tipoDocumento;

            lblSucursal.Text = ConfigModel.sucursal.nombre;

            // datos dinamicos
            int y = lblTipoCambio.Location.Y + 50;
            List<TipoCambioMoneda> tipoCambios = ConfigModel.tipoCambioMonedas;
            foreach (TipoCambioMoneda cambio in tipoCambios)
            {
                createElements(y, cambio);
                y += 22;
            }

            y = lbTotalEfectivo.Location.Y + 50;
           
            if (ConfigModel.cajaSesion != null && ConfigModel.cajaIniciada)
            {
                panelTotal.Controls.Clear();
                foreach (Moneda  M in   await cajaModel.cierreCajaIngresoMenosEgreso(1, ConfigModel.cajaSesion.idCajaSesion)){

                    createElementsMoneda(y, M);
                    y += 22;

                }

            }
            else
            {
                
                panelTotal.Controls.Clear();
            }
           

        }

        private async void cargarAsignacion()
        {
            try
            {
                asignacion = await personalModel.asignar(PersonalModel.personal.idPersonal, ConfigModel.sucursal.idSucursal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar asignacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region =========================== TOGGLE PANEL MAIN ===========================
        internal void togglePanelMain(string panelName)
        {
            this.panelMain.Controls.Clear();
            switch (panelName)
            {
                case "home":
                    if (this.uCHome == null)
                    {
                        this.uCHome = new Admeli.UCHome(this);
                        this.panelMain.Controls.Add(uCHome);
                        this.uCHome.Dock = System.Windows.Forms.DockStyle.Fill;
                        this.uCHome.Location = new System.Drawing.Point(0, 0);
                        this.uCHome.Name = "uCHome";
                        this.uCHome.Size = new System.Drawing.Size(250, 776);
                        this.uCHome.TabIndex = 0;
                    }
                    else
                    {
                        this.panelMain.Controls.Add(uCHome);
                    }
                    this.lblTitlePage.Text = "Home - "; /// Titulo en el encabezado
                    break;
                case "compras2":
                    if (this.uCCompras == null)
                    {
                        this.uCCompras = new UCCompras(this);
                        this.panelMain.Controls.Add(uCCompras);
                        this.uCCompras.Dock = System.Windows.Forms.DockStyle.Fill;
                        this.uCCompras.Location = new System.Drawing.Point(0, 0);
                        this.uCCompras.Name = "uCCompras";
                        this.uCCompras.Size = new System.Drawing.Size(250, 776);
                        this.uCCompras.TabIndex = 0;
                    }
                    else
                    {
                        this.panelMain.Controls.Add(uCCompras);
                    }
                    this.lblTitlePage.Text = "Compras - Compra"; /// Titulo en el encabezado
                    break;
                case "ventas2":
                    if (this.uCVentas == null)
                    {
                        this.uCVentas = new UCVentas(this);
                        this.panelMain.Controls.Add(uCVentas);
                        this.uCVentas.Dock = System.Windows.Forms.DockStyle.Fill;
                        this.uCVentas.Location = new System.Drawing.Point(0, 0);
                        this.uCVentas.Name = "uCVentas";
                        this.uCVentas.Size = new System.Drawing.Size(250, 776);
                        this.uCVentas.TabIndex = 0;
                    }
                    else
                    {
                        this.panelMain.Controls.Add(uCVentas);
                    }
                    this.lblTitlePage.Text = "Ventas - Venta"; /// Titulo en el encabezado
                    break;
                case "productos2":
                    if (this.uCListadoProducto == null)
                    {
                        this.uCListadoProducto = new UCListadoProducto(this);
                        this.panelMain.Controls.Add(uCListadoProducto);
                        this.uCListadoProducto.Dock = System.Windows.Forms.DockStyle.Fill;
                        this.uCListadoProducto.Location = new System.Drawing.Point(0, 0);
                        this.uCListadoProducto.Name = "uCListadoProducto";
                        this.uCListadoProducto.Size = new System.Drawing.Size(250, 776);
                        this.uCListadoProducto.TabIndex = 0;
                    }
                    else
                    {
                        this.panelMain.Controls.Add(uCListadoProducto);
                    }
                    this.lblTitlePage.Text = "Productos - Listar"; /// Titulo en el encabezado
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region ========================= TOGGLE MENU LEFT ROOTS =========================
        internal void toggleRootMenu(string panelName)
        {
            this.panelAsideMain.Controls.Clear();
            switch (panelName)
            {
                case "tienda":
                    if (this.uCTiendaRoot == null)
                    {
                        this.uCTiendaRoot = new UCTiendaRoot(this);
                        this.panelAsideMain.Controls.Add(uCTiendaRoot);
                        this.uCTiendaRoot.Dock = System.Windows.Forms.DockStyle.Fill;
                        this.uCTiendaRoot.Location = new System.Drawing.Point(0, 0);
                        this.uCTiendaRoot.Name = "uCTiendaRoot";
                        this.uCTiendaRoot.Size = new System.Drawing.Size(250, 776);
                        this.uCTiendaRoot.TabIndex = 0;
                    }
                    else
                    {
                        this.panelAsideMain.Controls.Add(uCTiendaRoot);
                    }
                    this.lblTitlePage.Text = "Tienda - "; /// Titulo en el encabezado
                    break;
                case "config":
                    if (this.uCConfigRoot == null)
                    {
                        this.uCConfigRoot = new UCConfigRoot(this);
                        this.panelAsideMain.Controls.Add(uCConfigRoot);
                        this.uCConfigRoot.Dock = System.Windows.Forms.DockStyle.Fill;
                        this.uCConfigRoot.Location = new System.Drawing.Point(0, 0);
                        this.uCConfigRoot.Name = "uCConfigRoot";
                        this.uCConfigRoot.Size = new System.Drawing.Size(250, 776);
                        this.uCConfigRoot.TabIndex = 0;
                    }
                    else
                    {
                        this.panelAsideMain.Controls.Add(uCConfigRoot);
                    }
                    this.lblTitlePage.Text = "Configuracion - "; /// Titulo en el encabezado
                    break;
                case "message":
                    if (this.uCMessageRoot == null)
                    {
                        this.uCMessageRoot = new UCMessageRoot(this);
                        this.panelAsideMain.Controls.Add(uCMessageRoot);
                        this.uCMessageRoot.Dock = System.Windows.Forms.DockStyle.Fill;
                        this.uCMessageRoot.Location = new System.Drawing.Point(0, 0);
                        this.uCMessageRoot.Name = "uCMessageRoot";
                        this.uCMessageRoot.Size = new System.Drawing.Size(250, 776);
                        this.uCMessageRoot.TabIndex = 0;
                    }
                    else
                    {
                        this.panelAsideMain.Controls.Add(uCMessageRoot);
                    }
                    this.lblTitlePage.Text = "Mensageria - "; /// Titulo en el encabezado
                    break;
                default:
                    break;
            }
        }

        private void btnTienda_Click(object sender, EventArgs e)
        {
            toggleRootMenu("tienda");
        }

        private void btnMessage_Click(object sender, EventArgs e)
        {
            toggleRootMenu("message");
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            toggleRootMenu("config");
        }

        #endregion

        #region ==================== TOGLLE PANEL MENU RIGHT AND LEFT ====================
        private void btnToggleMenuRigth_Click(object sender, EventArgs e)
        {
            if (panelMenuRight.Size.Width > 1)
            {
                panelMenuRight.Size = new Size(0, 700);
            }
            else
            {
                panelMenuRight.Size = new Size(224, 700);
            }
        }

        private void btnToggleMenu_Click(object sender, EventArgs e)
        {
            ocultarMenuLeft();
        }

        public void ocultarMenuLeft()
        {

            if (panelAsideContainer.Size.Width > 60)
            {
                panelAsideContainer.Size = new Size(58, 700);
            }
            else
            {
                panelAsideContainer.Size = new Size(250, 700);
            }
        }


        public void showMenuLeft()
        {
            if (panelAsideContainer.Size.Width < 100)
            {
                panelAsideContainer.Size = new Size(250, 700);
            }
        }
        public void hideMenuRight()
        {
            if (panelMenuRight.Size.Width > 1)
            {
                panelMenuRight.Size = new Size(0, 700);
            }
        }
        #endregion

        #region =============================== EVENTS ===============================
        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            if (this.FormBorderStyle == FormBorderStyle.None)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnCompra2_Click(object sender, EventArgs e)
        {
            togglePanelMain("compras2");
        }
        private void btnVentaTocuh_Click(object sender, EventArgs e)
        {
            FormVentaTouch ventaTouch = new FormVentaTouch();
            ventaTouch.ShowDialog();
        }

        private void btnVenta2_Click(object sender, EventArgs e)
        {
            togglePanelMain("ventas2");
        }

        private void btnProductos2_Click(object sender, EventArgs e)
        {
            togglePanelMain("productos2");
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            togglePanelMain("home");
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!notCloseApp)
            {
                Application.Exit();
            }
        }
        #endregion

        #region =============================== SATATES ===============================
        public void appLoadState(bool state)
        {
            if (state)
            {
                progressBarApp.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                progressBarApp.Style = ProgressBarStyle.Blocks;
            }
        }
        #endregion

        #region ==================== Create Dynamic Elements ====================
        private void createElements(int y, TipoCambioMoneda param)
        {
            /// 
            /// lblEfectivoName
            /// 
            Label lblEfectivoName = new System.Windows.Forms.Label()
            {
                AutoSize = true,
                Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = Color.FromArgb(84, 110, 122),
                Location = new System.Drawing.Point(13, y),
                Name = "lblEfectivoName",
                Size = new System.Drawing.Size(44, 16),
                TabIndex = 10,
                Text = param.moneda,
            };

            /// 
            /// lblEfectivoValue
            /// 
            Label lblEfectivoValue = new System.Windows.Forms.Label()
            {
                AutoSize = true,
                ForeColor = System.Drawing.SystemColors.ControlDarkDark,
                Location = new System.Drawing.Point(150, y),
                Name = "lblEfectivoValue",
                Size = new System.Drawing.Size(65, 13),
                TabIndex = 11,
                Text = String.Format("{0:0.00}", param.cambio)
            };

            /// 
            /// Add Controls
            /// 
            panelMenuRight.Controls.Add(lblEfectivoName);
            panelMenuRight.Controls.Add(lblEfectivoValue);
        }
        private void createElementsMoneda(int y, Moneda param)
        {
            /// 
            /// lblEfectivoName
            /// 
            Label lblEfectivoName = new System.Windows.Forms.Label()
            {
                AutoSize = true,
                Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = Color.FromArgb(84, 110, 122),
                Location = new System.Drawing.Point(13, y),
                Name = "lblMoneda",
                Size = new System.Drawing.Size(44, 16),
                TabIndex = 10,
                Text = param.moneda
            };

            /// 
            /// lblEfectivoValue
            /// 
            Label lblEfectivoValue = new System.Windows.Forms.Label()
            {
                AutoSize = true,
                ForeColor = System.Drawing.SystemColors.ControlDarkDark,
                Location = new System.Drawing.Point(150, y),
                Name = "lbtotal",
                Size = new System.Drawing.Size(65, 13),
                TabIndex = 11,
                Text = String.Format("{0:0.00}", param.total)
            };

            /// 
            /// Add Controls
            /// 
            panelTotal.Controls.Add(lblEfectivoName);
            panelTotal.Controls.Add(lblEfectivoValue);
        }
        #endregion

        #region ================================ PAINT ================================
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.bottomLine(panel3);
        }

        private void panelMenuRight_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.leftLine(panelMenuRight);
        }
        #endregion

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.panelMain.Dock = DockStyle.None;
            ResizeableControl resizeableControl = new ResizeableControl(this.panelMain);
            //uCTiendaRoot.Size = new Size(95,this.panelAsideMain.Size.Width);
        }
    }
}
