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
using Bunifu.Framework.UI;
using Entidad;
using Entidad.Configuracion;
using Modelo;

namespace Admeli.CajaBox.Nuevo
{
    public partial class FormArquearCaja : Form
    {
        private CierreCaja currentCierreCaja;
        private CajaSesion currentCajaSesion;
        private int currentIdCajaCierre;
        private MedioPagoModel medioPagoModel = new MedioPagoModel();
        private IngresoModel ingresoModel = new IngresoModel();
        private ConfigModel configModel = new ConfigModel();
        private FechaModel fechaModel = new FechaModel();
        private MonedaModel monedaModel = new MonedaModel();
        private CierreCajaModel cierreCajaModel = new CierreCajaModel();
        private DenominacionModel denominacionModel = new DenominacionModel();
        private CajaModel cajaModel = new CajaModel();

        private List<MedioPago> medioPagos { get; set; }
        private List<Moneda> ingresoMenosEgreso { get; set; }
        private List<Ingreso> ingresos { get; set; }
        private List<Moneda> monedasActivas { get; set; }
        private bool nuevo { get; set; }

        private int currentIdCajaSesion { get; set; }
        private saveObject currentSaveObject { get; set; }

        // variables de los elementos en el aside
        private int x = 13, y = 10;

        #region =========================== Constructor ===========================
        public FormArquearCaja()
        {
            InitializeComponent();
            currentCierreCaja = new CierreCaja() { idCierreCaja = 0 };

            this.nuevo = true;
        }

        public FormArquearCaja(CierreCaja currentCierreCaja)
        {
            InitializeComponent();
            this.currentCierreCaja = currentCierreCaja;
            this.currentIdCajaSesion = currentCierreCaja.idCajaSesion;
            this.nuevo = false;
            this.btnAceptar.Enabled = false;
        }

        public FormArquearCaja(CajaSesion currentCajaSesion)
        {
            InitializeComponent();
            this.currentCajaSesion = currentCajaSesion;
            this.currentIdCajaSesion = currentCajaSesion.idCajaSesion;
            currentCierreCaja = new CierreCaja() { idCierreCaja = 0 };
            this.nuevo = true;
        }
        #endregion

        #region ============================= Root Load =============================
        private void FormArquearCaja_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        private void reLoad()
        {
            //Cargar Medio Pago
            this.cargarMedioPago(); // Aqui tambien se carga IngresosMenosEgresos
            //Cargar Montos Inicio por Moneda
            this.cargarMontosInicio();
            //Cargar la CajaSesion (Para la fecha de inicio)
            this.cargarFechaCajaSesion();
            //Cargar fecha y hora del  sistema
            this.cargarFecha();
            //Cargar las monedas activas
            this.cargarMoneda();
            
            //Cargar Denominaciones de cada moneda

            //this.cargarCajaSesion();
        }
        #endregion

        #region ================================ Load ================================
        private async void cargarMedioPago()
        {
            try
            {
                medioPagos = await medioPagoModel.medioPagos();
                this.cargarIngresoMenosEgreso();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Medio Pagos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarMontosInicio()
        {
            try
            {
                ingresos = await ingresoModel.ingresos(currentIdCajaSesion); // Listas
                int columnas = 3; // Indicar numero de columnas de la grilla

                this.createLabel(x - 5, this.y, "Monto inicio caja");
                this.y += 25;

                // =========================================== Algoritmo para crear una grilla
                int items = ingresos.Count % columnas;      // Detectar cuandos elementos hay en la ultima fila de la grilla
                int rowComplete = Convert.ToInt32(Math.Floor((decimal)(ingresos.Count / columnas))) * columnas; // detectar cuantos fila esta lleno de  registros
                for (int i = 0; i < ingresos.Count; i++) // for para las filas
                {
                    for (int j = 0; j < columnas; j++) // For para las columnas
                    {
                        if (i > rowComplete) // validacion
                        {
                            if (items == j) break; // salir de este for
                        }
                        this.createElement(panelAside.Controls, x, this.y, ingresos[i].moneda, ingresos[i].monto.ToString(ConfigModel.configuracionGeneral.formatoDecimales));
                        i = (columnas - 1 == j) ? i : i + 1; // indice de registros aumento
                        x += 170; // cordenada x aumentado
                    }
                    this.y += 50; // cordenada y
                    x = 13; // cordenada x regresando al valor original
                }
                // ==============================================================================
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Ingresos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarFecha()
        {
            try
            {
                FechaSistema date = await fechaModel.fechaSistema();
                lblFechaFin.Text = date.fecha.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Fecha", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarMoneda()
        {
            try
            {
                monedasActivas = await monedaModel.monedas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar medios pago", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cargarFechaCajaSesion()
        {
            try
            {
                if (nuevo)
                {
                    lblFechInicio.Text = currentCajaSesion.fechaInicio.ToString();
                }
                else
                {
                    lblFechInicio.Text=currentCierreCaja.fechaInicio.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar caja sesion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarIngresoMenosEgreso()
        {
            try
            {
                /// 
                int medioPagoID = medioPagos.First<MedioPago>().idMedioPago;
                ingresoMenosEgreso = await cierreCajaModel.ingresoMenosEgreso(medioPagoID, currentIdCajaSesion);

                this.createLabel(x - 5, this.y, "Cálculo efectivo");
                this.y += 25;

                /// ==================================================================
                /// crear los campos textbox
                foreach (Moneda moneda in ingresoMenosEgreso)
                {
                    this.createElement(panelAside.Controls, x, y, "Teorico " + moneda.moneda, moneda.total.ToString());
                    this.createElement(panelAside.Controls, x + 170, y, "Real " + moneda.moneda, "0");   // Falta 
                    this.createElement(panelAside.Controls, x + 340, y, "Descuadre " + moneda.moneda, "0");   // Falta 
                    this.y += 50;
                }

                /// ==================================================================
                /// crar loas tab pages
                foreach (Moneda moneda in ingresoMenosEgreso)
                {
                    /// ===================================================================
                    /// denominaciones de las monedas
                    List<Denominacion> denominaciones = await denominacionModel.denominacionMoneda(moneda.idMoneda,currentIdCajaSesion);
                    createTabPage(moneda.moneda, moneda.idMoneda.ToString(), denominaciones);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "IngresoMenosEgreso ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion


        /// <summary>
        /// Crear BuniFuTextBox use este metodo para crear campos dinamicos
        /// </summary>
        /// <param name="controls">Control padre al que se agregara los elementos </param>
        /// <param name="x">posicion en el eje X</param>
        /// <param name="y">posicion en el eje Y</param>
        /// <param name="labelValue">titulo del campo</param>
        /// <param name="textBoxValue">valor del campo</param>
        /// <param name="key">Clave para identificar el elemento</param>
        /// <param name="whidt">ancho del elemento</param>
        /// <param name="height">alto del elemento</param>
        /// <param name="gap">separacion de los elementos</param>
        private void createElement(Control.ControlCollection controls, int x, int y, string labelValue, string textBoxValue, string key = "", int whidt = 160, int height = 40, int gap = 10)
        {
            Label titlefield = new Label()
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.Color.DimGray,
                Location = new System.Drawing.Point(x + 3, y + 3),
                Margin = new System.Windows.Forms.Padding(2, 0, 2, 0),
                Name = "label1111",
                Size = new System.Drawing.Size(59, 14),
                TabIndex = 8,
                Text = labelValue
            };

            BunifuMetroTextbox textBoxBF1 = new BunifuMetroTextbox()
            {
                BackColor = System.Drawing.Color.White,
                BorderColorFocused = System.Drawing.Color.DodgerBlue,
                BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157))))),
                BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157))))),
                BorderThickness = 1,
                Cursor = System.Windows.Forms.Cursors.IBeam,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64))))),
                isPassword = false,
                Location = new System.Drawing.Point(x, y),
                Margin = new System.Windows.Forms.Padding(4),
                Name = "textBox1111",
                Padding = new System.Windows.Forms.Padding(2, 18, 5, 2),
                Size = new System.Drawing.Size(whidt, height),
                TabIndex = 9,
                TextAlign = System.Windows.Forms.HorizontalAlignment.Left,
                Text = textBoxValue,
                Enabled = false
            };
            controls.Add(titlefield);
            controls.Add(textBoxBF1);
        }

        /// <summary>
        /// Crear un label
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="content"></param>
        private void createLabel(int x, int y, string content)
        {
            Label titlefield = new Label()
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.Color.FromArgb(76, 76, 76),
                Location = new System.Drawing.Point(x + 3, y + 3),
                Margin = new System.Windows.Forms.Padding(2, 0, 2, 0),
                Name = "label1111",
                Size = new System.Drawing.Size(59, 14),
                TabIndex = 8,
                Text = content
            };
            this.panelAside.Controls.Add(titlefield);
        }

        private void createGrid()
        {

        }

        /// <summary>
        /// Agregar un tab page
        /// </summary>
        /// <param name="titlePage"></param>
        /// <param name="idTitlePage"></param>
        private void createTabPage(string titlePage, string idTitlePage, List<Denominacion> denominaciones)
        {
            TabPage tabPage = new TabPage()
            {
                Location = new System.Drawing.Point(4, 34),
                Name = idTitlePage.Trim(),
                Padding = new System.Windows.Forms.Padding(3),
                Size = new System.Drawing.Size(550, 208),
                TabIndex = 1,
                Text = titlePage,
                UseVisualStyleBackColor = true,
            };
            this.tabControlMonedas.Controls.Add(tabPage);
            //Agreagar los text box de Monto 
            int ordenadas = 13, absisas = 13;
            this.createElement(tabPage.Controls, ordenadas, absisas, "Monto", "0");
            absisas += 50;
            foreach (Denominacion denominacion in denominaciones)
            {
                this.createElement(tabPage.Controls, ordenadas, absisas, denominacion.nombre, denominacion.valor);
                absisas += 50;
            }
        }

        #region ================================ SAVE AND UPDATE ================================

        private async void executeGuardar()
        {
            bloquear(true);
            if (!validarCampos()) { bloquear(false); return; }
            try
            {   
                List<Moneda> listResponse = await cajaModel.verificarActividad(currentIdCajaSesion);// lista las actividad  pero no lo utiliza chilca lo arregalara
                
                createObject();
                if (nuevo)
                {
                    Response saveResponse = await cierreCajaModel.cierreCaja(currentSaveObject);
                    currentIdCajaCierre = saveResponse.id;
                    int counter = 1;
                    //Guardar detalles del Cierre para cada moneda
                    foreach (Moneda moneda in ingresoMenosEgreso)
                    {
                        //Falta Especificar las denominaciones por cada moneda
                        SaveObjectCierreCajaDetalle currentCierreCajaDetalle= createObjetoDetalleCobro(moneda.idMoneda);
                        Response saveResponseDetalle = await cierreCajaModel.cierreCajaDetalle(currentCierreCajaDetalle);
                    }
                    MessageBox.Show(saveResponse.msj + counter + "Registros guardado", "Cerrar Caja ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bloquear(false);
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                await configModel.loadCajaSesion(ConfigModel.asignacionPersonal.idAsignarCaja);
                bloquear(false);
            }
        }

        private SaveObjectCierreCajaDetalle createObjetoDetalleCobro(int idMoneda)
        {
            SaveObjectCierreCajaDetalle obj = new SaveObjectCierreCajaDetalle();
            obj.estado = 1;
            obj.idCajaSesion = currentIdCajaSesion;
            obj.idCierreCaja = currentIdCajaCierre;
            obj.idMedioPago = 1;
            obj.idMoneda = idMoneda;
            obj.monto = "0";
            obj.valores = new string[0];
            return obj;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            executeGuardar();
        }

        private void createObject()
        {
            if (nuevo)
            {
                // creando objecto cierrecaja
                currentSaveObject = new saveObject();
                currentSaveObject.estado = 1;
                DateTime dt = DateTime.Parse(lblFechInicio.Text);
                currentSaveObject.fechaInicio = dt.ToString("yyyy-MM-dd HH':'mm':'ss");
                currentSaveObject.idCajaSesion =currentIdCajaSesion;
                currentSaveObject.nombre = ConfigModel.sucursal.nombre;
                currentSaveObject.nombres = PersonalModel.personal.nombres;
                currentSaveObject.observacion = textObservacion.Text;

                // creando objeto cierre caja detalle
            }
        }

        private bool validarCampos()
        {
            return true;
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
            btnAceptar.Enabled = !state;
        }
        public void bloquear(bool state)
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
            this.Enabled = !state;
        }
        #endregion

    }

    public class saveObject
    {
        public int estado { get; set; }
        public string fechaInicio { get; set; }
        public int idCajaSesion { get; set; }
        public string nombre { get; set; }
        public string nombres { get; set; }
        public string observacion { get; set; }
    }
    public class SaveObjectCierreCajaDetalle
    {
        public int estado { get; set; }
        public int idCajaSesion { get; set; }
        public int idCierreCaja { get; set; }
        public int idMedioPago { get; set; }
        public int idMoneda { get; set; }
        public string monto { get; set; }
        public string[] valores { get; set; }
    }
}
