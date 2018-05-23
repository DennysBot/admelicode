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
using Modelo;
using Entidad.Configuracion;
using Bunifu.Framework.UI;
using Entidad;

namespace Admeli.CajaBox
{
    public partial class UCIniciarCaja : UserControl
    {
        private FormPrincipal formPrincipal;
        public bool lisenerKeyEvents { get; set; }

        private CajaModel cajaModel = new CajaModel();
        private MonedaModel monedaModel = new MonedaModel();
        private MedioPagoModel medioPagoModel = new MedioPagoModel();
        private FechaModel fechaModel = new FechaModel();
        private ConfigModel configModel = new ConfigModel();

        private IngresoModel ingresoModel = new IngresoModel();

        private List<Moneda> monedas { get; set; }
        private MedioPago medioPago { get; set; }
        private CajaSesion currentCajaSesion { get; set; }
        private ObjectSaveCajaSesion saveCajaSesion { get; set; }

        #region =========================== Constructor ===========================
        public UCIniciarCaja()
        {
            InitializeComponent();

            lisenerKeyEvents = true; // Active lisener key events
        }

        public UCIniciarCaja(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;

            lisenerKeyEvents = true; // Active lisener key events
        }
        #endregion

        #region ============================== Paint and Decoration ==============================
        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panelContainer);
        } 
        #endregion

        #region ============================== Root Load ==============================
        private void UCIniciarCaja_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        internal void reLoad(bool refreshData = true)
        {
            if (refreshData)
            {
                cargarCajaSesion();
                cargarMonedas();
                cargarMediosPago();
                cargarFecha();
                // verificaciones
                verificarEstadoCaja();

                formPrincipal.cargarDatosAsideRight();
            }
        }
        #endregion

        #region ============================= Estados =============================
        private void loadState(bool state)
        {
            formPrincipal.appLoadState(state);
        } 
        #endregion

        #region =============================== Loads ===============================
        private async void cargarCajaSesion()
        {
            try
            {
                loadState(true);
                List<CajaSesion> list = await cajaModel.cajaSesion(ConfigModel.asignacionPersonal.idAsignarCaja);
                if (list.Count == 0) return;
                currentCajaSesion = list[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upps! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.formPrincipal.cargarDatosAsideRight();
                loadState(false);
            }
        }

        private async void cargarMediosPago()
        {
            try
            {
                loadState(true);
                List<MedioPago> list = await medioPagoModel.medioPagos();
                medioPago = list[0];

                dynamic currentFecha = await fechaModel.fechaSistema();
                dtpFechaInicio.Value = currentFecha.fecha;
                dtpFechaIngreso.Value = currentFecha.fecha;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upps! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadState(false);
            }
        }

        private async void cargarFecha()
        {
            try
            {
                loadState(true);
                dynamic currentFecha = await fechaModel.fechaSistema();
                dtpFechaInicio.Value = currentFecha.fecha;
                dtpFechaIngreso.Value = currentFecha.fecha;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upps! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadState(false);
            }
        }

        private async void cargarMonedas()
        {
            try
            {
                loadState(true);
                monedas = await monedaModel.monedas();

                // =========================================== Algoritmo para crear una grilla
                int columnas = 2;
                int x = 13;
                int y = 120;
                int items = monedas.Count % columnas;      // Detectar cuandos elementos hay en la ultima fila de la grilla
                int rowComplete = Convert.ToInt32(Math.Floor((decimal)(monedas.Count / columnas))) * columnas; // detectar cuantos fila esta lleno de  registros
                for (int i = 0; i < monedas.Count; i++) // for para las filas
                {
                    for (int j = 0; j < columnas; j++) // For para las columnas
                    {
                        if (i > rowComplete) // validacion
                        {
                            if (items == j) break; // salir de este for
                        }
                        this.createElement(panelContainer.Controls, x, y, monedas[i].moneda, "", monedas[i].idMoneda.ToString(),250);
                        i = (columnas - 1 == j) ? i : i + 1; // indice de registros aumento
                        x += 270; // cordenada x aumentado
                    }
                    y += 50; // cordenada y
                    x = 13; // cordenada x regresando al valor original
                }
                // ==============================================================================
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upps! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadState(false);
            }

        }

        private async void verificarEstadoCaja()
        {
            await configModel.loadCajaSesion(ConfigModel.asignacionPersonal.idAsignarCaja);
            if (ConfigModel.cajaSesion != null &&  ConfigModel.cajaIniciada)
            {
                if (ConfigModel.cajaSesion.idCajaSesion > 0)
                {
                    Validator.labelAlert(lblCajaEstado, 1, "Usted ya inicio caja.");
                    btnAceptar.Enabled = false;
                }
                else
                {
                    Validator.labelAlert(lblCajaEstado, 0, "No se inicio la caja");
                    btnAceptar.Enabled = true;
                }
            }
            else
            {
                Validator.labelAlert(lblCajaEstado, 0, "No se inicio la caja");
                btnAceptar.Enabled = true;
            }
        }

        #endregion

        #region ============================= Crear Elementos =============================
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
        private void createElement(Control.ControlCollection controls, int x, int y, string labelValue, string textBoxValue, string key = "", int whidt = 300, int height = 40, int gap = 10)
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
                Name = key,
                Padding = new System.Windows.Forms.Padding(2, 18, 5, 2),
                Size = new System.Drawing.Size(whidt, height),
                TabIndex = 9,
                TextAlign = System.Windows.Forms.HorizontalAlignment.Left,
                Text = textBoxValue
            };

            textBoxBF1.OnValueChanged += new EventHandler(this.textMoneda1_OnValueChanged);
            textBoxBF1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textMoneda1_KeyPress);

            controls.Add(titlefield);
            controls.Add(textBoxBF1);
        }


        #endregion

        #region ================================ Validaciones Tiempo Real ================================
        private void textMoneda1_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                string currentIDMoneda = ((BunifuMetroTextbox)sender).Name;
                if (currentIDMoneda != "")
                {
                    for (int i = 0; i < monedas.Count; i++)
                    {
                        if (monedas[i].idMoneda == Convert.ToInt32(currentIDMoneda))
                        {
                            string contentText = ((BunifuMetroTextbox)sender).Text;
                            monedas[i].monto = (contentText == "") ? 0 : Convert.ToInt32(contentText);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textMoneda1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }
        #endregion

        #region ============================ Save ============================
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            executeGuardar();
        }

        private async void executeGuardar()
        {
            try
            {
                // Cambiando de estado
                loadState(true);

                // Guardar la caja sesion
                crearObjetoCajaSesion();
                Response responseCaja = await cajaModel.guardar(saveCajaSesion);

                // Guardando las monedas
                foreach (Moneda money in monedas)
                {
                    if (money.monto == 0) continue;
                    money.fechaPago = dtpFechaIngreso.Value.ToString("yyyy-MM-dd HH':'mm':'ss");
                    money.observacion = textObservacion.Text;
                    money.idCaja = ConfigModel.asignacionPersonal.idCaja;
                    money.idCajaSesion = responseCaja.id;
                    money.idMedioPago = medioPago.idMedioPago;
                    money.medioPago = medioPago.nombre;
                    money.motivo = "INCIO CAJA";
                    money.numeroOperacion = "";
                    money.estado = 2;

                    Response response = await ingresoModel.guardarEnUno(money);
                }

                // Mensaje de confirmacion
                MessageBox.Show("Se inicio caja correctamente.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                // Recargar Datos despues de iniciar la caja
                await configModel.loadCajaSesion(ConfigModel.asignacionPersonal.idAsignarCaja);
                this.formPrincipal.cargarDatosAsideRight();
                loadState(false);
                this.reLoad();
            }
        }

        private void crearObjetoCajaSesion()
        {
            saveCajaSesion = new ObjectSaveCajaSesion();
            saveCajaSesion.idAsignarCaja = ConfigModel.asignacionPersonal.idAsignarCaja;
            saveCajaSesion.estado = 0;
        }
        #endregion
    }
    public class ObjectSaveCajaSesion
    {
        public int estado { get; set; }
        public int idAsignarCaja { get; set; }
    }
}
