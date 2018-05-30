using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.AlmacenBox.buscar;
using Admeli.Componentes;
using Admeli.Properties;
using Admeli.Ventas.buscar;
using Entidad;
using Entidad.Configuracion;
using Modelo;
using Newtonsoft.Json;

namespace Admeli.AlmacenBox.Nuevo
{
    public partial class FormNotaSalidaNew : Form
    {
        // objetos Necesarios para Guardar y modificar una nota entrada
        //==para verificacion
        ComprobarNotaSalida comprobarNota { get; set; }

        List<List<object>> listint { get; set; }
        //===
        AlmacenSalida almacenSalida { get; set; }
        VentaSalida ventaSalida { get; set; }
        Dictionary<string, double> dictionary { get; set; }
        Dictionary<string, DetalleNotaSalida> DetallesNotaSalida { get; set; }

        object object4 { get; set; }
        object object5 { get; set; }
        object object6 { get; set; }
        object object7 { get; set; }
        List<object> listElementosNotaSalida { get; set; }



        // objetos Necesarios para Guardar y modificar una nota entrada
        //==para verificacion
        ComprobarNota comprobarNotaE { get; set; }

        List<List<int>> listintE { get; set; }
        //===
        AlmacenNEntrada almacenNEntrada { get; set; }
        CompraEntradaGuardar compraEntradaGuardar { get; set; }
        Dictionary<string, double> dictionaryE { get; set; }
        Dictionary<string, DetalleNotaSalida> DetallesNota { get; set; }

        object object4E { get; set; }
        object object5E { get; set; }
        object object6E { get; set; }
        object object7E { get; set; }
        List<object> listElementosNotaEntrada { get; set; }










        // servicios necesarios

        AlmacenModel AlmacenModel = new AlmacenModel();
        ProductoModel productoModel = new ProductoModel();
        FechaModel fechaModel = new FechaModel();
        NotaSalidaModel notaSalidaModel = new NotaSalidaModel();
        PresentacionModel presentacionModel = new PresentacionModel();

        AlternativaModel alternativaModel = new AlternativaModel();


        NotaEntradaModel entradaModel = new NotaEntradaModel();
        // objetos que cargan a un inicio
        private  List<ProductoVenta> listProducto { get; set; }
        private List<Presentacion> listPresentacion { get; set; }
        private List<Almacen> listAlmacenOrigen { get; set; }
        private List<Almacen> listAlmacenDestino { get; set; }
        private List<AlmacenCorrelativo> listCorrelativoA { get; set; }
        private List<DetalleNotaSalida> listDetalleNotaSalida { get; set; }

        // entidadades auxiliares

        private bool  nuevo { get; set; }
        private string formato { get; set; }
        private int nroDecimales = ConfigModel.configuracionGeneral.numeroDecimales;
        private FechaSistema fechaSistema { get; set; }



        //objetos en tiempo real

       private Almacen currentAlmacen { get; set; }
       private AlmacenCorrelativo currentCorrelativoA { get; set; }
       private ProductoVenta currentProducto { get; set; }
      

        private VentasNSalida currentVenta { get; set; }


        private NotaSalida currentNotaSalida { get; set; }


        private DetalleNotaSalida currentdetalleNotaSalida { get; set; }
        int indice = 0;

        private int numberOfItemsPerPage = 0;
        private int numberOfItemsPrintedSoFar = 0;

        List<FormatoDocumento> listformato;

        private int filtradoAlmacen = 0;


        private int HpanelVenta = 0;
        private int WpanelVenta = 0;
        private int HpanelDatosV = 0;
        private int WpanelDatosV = 0;

        private int XbtnImportar = 0;
        private int YbtnImportar = 0;
        private int XbtnQuitar = 0;
        private int YbtnQuitar = 0;
        private bool lisenerKeyEvents = true;

        public FormNotaSalidaNew()
        {
            InitializeComponent();
            this.nuevo = true;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();
            btnQuitar.Enabled = false;
            recuperarDatosInicialesControles();
                           
        }


        public FormNotaSalidaNew(NotaSalida currentNotaSalida)
        {
            InitializeComponent();
            this.nuevo = false;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();
            btnQuitar.Enabled = false;
            btnImportarVenta.Enabled = false;
            this.currentNotaSalida = currentNotaSalida;
        }

        #region=======================metodos de apoyo
        private string darformato(object dato)
        {
            return string.Format(CultureInfo.GetCultureInfo("en-US"), this.formato, dato);
        }

        private double toDouble(string texto)
        {

            if (texto == "")
            {

                return 0;
            }
            return double.Parse(texto, CultureInfo.GetCultureInfo("en-US")); ;
        }

        private void recuperarDatosInicialesControles()
        {
            //de los botones
            XbtnImportar = btnImportarVenta.Location.X;
            YbtnImportar = btnImportarVenta.Location.Y;

            XbtnQuitar = btnQuitar.Location.X;
            YbtnQuitar = btnQuitar.Location.Y;          
            // paneles
            HpanelVenta = plDventa.Height;
            WpanelVenta = plDventa.Width;
           
            HpanelDatosV = panel6.Height;
            WpanelDatosV = panel6.Width;
        }
        #endregion
        #region ================================ Root Load ================================

        private void FormNotaSalidaNew_Load(object sender, EventArgs e)
        {
            if (nuevo == true)
            {
                this.reLoad();
                ocultarControlesVenta();
            }
                
            else
            {
                this.reLoad();
                cargarNotaSalida();
                if (Convert.ToInt32(currentNotaSalida.idVenta) == 0)
                {
                    ocultarControlesVenta();

                }

                // 
                panelNotaEntrada.Size = new Size(0,0);
                tableLayoutPanel11.ColumnCount= 4;

            }
            this.ParentChanged += ParentChange; // Evetno que se dispara cuando el padre cambia // Este eveto se usa para desactivar lisener key events de este modulo
            if (TopLevelControl is Form) // Escuchando los eventos del formulario padre
            {
                (TopLevelControl as Form).KeyPreview = true;
                TopLevelControl.KeyUp += TopLevelControl_KeyUp;
            }

        }
        private void reLoad()
        {
            cargarAlmacenes();
            cargarProductos();          
            cargarObjetos();
            cargarFormatoDocumento();
          
        }

        #endregion

        #region ============================== Load ==============================


        private  void ocultarControlesVenta()
        {
            plDventa.Size = new Size(0, 0);
            int h = HpanelDatosV - HpanelVenta;
            panel6.Size = new Size(WpanelDatosV, h);
            //subir los botones
            int Y = YbtnImportar - HpanelVenta;
            btnImportarVenta.Location = new
                 Point(XbtnImportar, Y);
            btnQuitar.Location = new
                 Point(XbtnQuitar, Y);

        }
        private void mostrarControlesVenta()
        {
            //de los botones
         


            plDventa.Size = new Size(WpanelVenta, HpanelVenta);
            panel6.Size = new Size(WpanelDatosV, HpanelDatosV);
            btnImportarVenta.Location = new
                Point(XbtnImportar, YbtnImportar);
            btnQuitar.Location = new
                Point(XbtnQuitar, YbtnQuitar);
        }

            private DetalleNotaSalida buscarElemento(int idPresentacion, int idCombinacion)
        {

            try
            {
                return listDetalleNotaSalida.Find(x => x.idPresentacion == idPresentacion && x.idCombinacionAlternativa == idCombinacion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar fechas del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }


        }










        private void cargarFormatoDocumento()
        {


            TipoDocumento tipoDocumento = ConfigModel.tipoDocumento.Find(X => X.idTipoDocumento == 8);// nota salida
            listformato = JsonConvert.DeserializeObject<List<FormatoDocumento>>(tipoDocumento.formatoDocumento);
            foreach (FormatoDocumento f in listformato)
            {
                string textoNormalizado = f.value.Normalize(NormalizationForm.FormD);
                //coincide todo lo que no sean letras y números ascii o espacio
                //y lo reemplazamos por una cadena vacía.
                Regex reg = new Regex("[^a-zA-Z0-9 ]");
                f.value = reg.Replace(textoNormalizado, "");
                f.value = f.value.Replace(" ", "");

            }
            string info = JsonConvert.SerializeObject(listformato);
        }
        private async void cargarNotaSalida()
        {

            //datos

            if (currentNotaSalida.idVenta != "0")
            {
                txtNroDocumentoVenta.Text = currentNotaSalida.numeroDocumento;
                txtNombreCliente.Text = currentNotaSalida.nombreCliente;
                txtDocumentoCliente.Text = currentNotaSalida.rucDni;

            }

            // serie
            txtSerie.Text = currentNotaSalida.serie;
            txtCorrelativo.Text = currentNotaSalida.correlativo;
            cbxAlmacen.SelectedValue = currentNotaSalida.idAlmacen;
            dtpFechaEntrega.Value = currentNotaSalida.fechaSalida.date;
            txtMotivo.Text = currentNotaSalida.motivo;
            txtDescripcion.Text = currentNotaSalida.descripcion;
            txtDireccionDestino.Text = currentNotaSalida.destino;

            string estado = "";
            switch (currentNotaSalida.estadoEnvio)
            {
                case 0:
                    estado = "Pendiente";
                    break;
                case 1:
                    estado = "Revisado";
                    break;
                case 2:
                    estado = "Enviado";
                    break;


            }
            cbxEstado.Text = estado;
            try
            {
                // cargar detalles de la nota
                listDetalleNotaSalida = await notaSalidaModel.cargarDetallesNota(currentNotaSalida.idNotaSalida);

                detalleNotaSalidaBindingSource.DataSource = listDetalleNotaSalida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Detalles de la Nota", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }




        private void cargarObjetos()
        {
            ////==
            comprobarNota = new ComprobarNotaSalida();
            listint = new List<List<object>>();
            ////===
            almacenSalida = new AlmacenSalida();
            ventaSalida = new VentaSalida();

            dictionary = new Dictionary<string, double>();
            DetallesNotaSalida = new Dictionary<string, DetalleNotaSalida>();
            object4 = new object();
            object5 = new object();
            object6 = new object();
            object7 = new object();
            listElementosNotaSalida = new List<object>();

           
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            // creando objetos para la nota de entrada

            //==
            comprobarNotaE = new ComprobarNota();
            listintE = new List<List<int>>();
            //===
            almacenNEntrada = new AlmacenNEntrada();
            compraEntradaGuardar = new CompraEntradaGuardar();
            dictionaryE = new Dictionary<string, double>();
            DetallesNota = new Dictionary<string, DetalleNotaSalida>();
            object4E = new object();
            object5E = new object();
            object6E = new object();
            object7E = new object();
            listElementosNotaEntrada = new List<object>();
            if (nuevo)
            {
                cbxEstado.SelectedIndex = 2;
            }


        }
        private async void cargarAlmacenes()
        {
            loadState(true);
            try
            {
                //listAlmacen = await AlmacenModel.almacenesAsignados(ConfigModel.sucursal.idSucursal, PersonalModel.personal.idPersonal);

                //listAlmacen = await AlmacenModel.almacenesAsignados(ConfigModel.sucursal.idSucursal, PersonalModel.personal.idPersonal);//  para una sucursal
             


                listAlmacenOrigen = await AlmacenModel.almacenesAsignados(0, PersonalModel.personal.idPersonal);//  para todos las asignadas al personal

                almacenBindingSource.DataSource = listAlmacenOrigen;
                cbxAlmacen.SelectedIndex = -1;
               
                listAlmacenDestino = new List<Almacen>();
                Almacen almacen = new Almacen();
                almacen.idAlmacen = 0;
                almacen.nombre = "Ninguno";
                listAlmacenDestino.Add(almacen);
                listAlmacenDestino.AddRange(listAlmacenOrigen);

                almacenBindingSource1.DataSource = listAlmacenDestino;
                currentAlmacen = listAlmacenOrigen.Find(X=>X.idAlmacen== ConfigModel.currentIdAlmacen);
                cbxAlmacen.SelectedValue = currentAlmacen.idAlmacen;
                cbxAlmacenEntrada.SelectedIndex = -1;
                cbxAlmacenEntrada.SelectedValue = 0;
                if (nuevo)
                {
                    cargarDocCorrelativo(currentAlmacen.idAlmacen);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Almacenes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (listProducto != null)
                    loadState(true);

            }

        }
        private async void cargarDocCorrelativo(int idAlmacen)
        {
            try
            {
                listCorrelativoA = await AlmacenModel.DocCorrelativoAlmacen(idAlmacen);
                currentCorrelativoA = listCorrelativoA[0];
                txtSerie.Text = currentCorrelativoA.serie;
                txtCorrelativo.Text = currentCorrelativoA.correlativoActual;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Doc Correlativo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarProductos()
        {
            try
            {
                listProducto  = await productoModel.productos(ConfigModel.sucursal.idSucursal, ConfigModel.currentIdAlmacen );
                productoVentaBindingSource.DataSource = listProducto;
                cbxCodigoProducto.SelectedIndex = -1;
                cbxDescripcion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (listAlmacenOrigen != null)
                    loadState(false);

            }
        }

        
        private async void cargarFechaSistema()
        {
            try
            {
                if (!nuevo) return;
                fechaSistema = await fechaModel.fechaSistema();
                dtpFechaEntrega.Value = fechaSistema.fecha;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion


        #region ======================== KEYBOARD ========================
        // Evento que se dispara cuando el padre cambia
        private void ParentChange(object sender, EventArgs e)
        {
            // cambiar la propiedad de lisenerKeyEvents de este modulo
            if (lisenerKeyEvents) lisenerKeyEvents = false;
        }

        // Escuchando los Eventos de teclado
        private void TopLevelControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (!lisenerKeyEvents) return;
            switch (e.KeyCode)
            {
                case Keys.F2: // productos
                    cbxCodigoProducto.Focus();
                    break;
                case Keys.F3:
                    cbxAlmacen.Focus();
                    break;
                case Keys.F4:
                    cbxAlmacenEntrada.Focus();
                    break;
                case Keys.F5:
                    ImportarVenta();
                    break;
                case Keys.F7:
                    buscarProducto();
                    break;
                default:
                    if (e.Control && e.KeyValue == 13)
                    {
                        hacerNota();
                    }

                    break;
            }




        }
        #endregion
        #region =========================== Estados ===========================

        public void appLoadState(bool state)
        {
            if (state)
            {

                progresStatus.Visible = state;
                progresStatus.Style = ProgressBarStyle.Marquee;
                Cursor = Cursors.WaitCursor;
            }
            else
            {
                progresStatus.Style = ProgressBarStyle.Blocks;
                progresStatus.Visible = state;

                Cursor = Cursors.Default;
            }
        }
        private void loadState(bool state)
        {

            appLoadState(state);
            this.Enabled = !state;
        }
        #endregion


        private void btnImportarVenta_Click(object sender, EventArgs e)
        {
            ImportarVenta();
        }
        private void ImportarVenta()
        {
            FormBuscarVentas formBuscarVentas = new FormBuscarVentas();
            formBuscarVentas.ShowDialog();
            if (formBuscarVentas.currentVenta != null)
            {
                currentVenta = formBuscarVentas.currentVenta;
                txtNroDocumentoVenta.Text = currentVenta.numeroDocumento;
                txtDocumentoCliente.Text = currentVenta.rucDni;
                txtNombreCliente.Text = currentVenta.nombreCliente;
                cargarDetalle(currentVenta.idVenta);
                mostrarControlesVenta();
                btnQuitar.Enabled = true;
            }

        }
        public void buscarProducto()
        {
            try
            {
                FormBuscarProducto formBuscarProducto = new FormBuscarProducto(listProducto);
                formBuscarProducto.ShowDialog();

                if (formBuscarProducto.currentProducto != null)
                {


                    currentProducto = formBuscarProducto.currentProducto;
                    if (currentProducto.precioVenta == null)
                    {

                        currentProducto = null;
                        MessageBox.Show("Error:  producto seleccionado no tiene precio de venta, no se puede seleccionar", "Buscar Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;

                    }

                    cbxCodigoProducto.SelectedValue = formBuscarProducto.currentProducto.idProducto;

                }





            }
            catch (Exception ex)

            {
                MessageBox.Show("Error: " + ex.Message, "Productos ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }
        private  async void  cargarDetalle(int idVenta)
        {
            try
            {
                listDetalleNotaSalida = await notaSalidaModel.cargarDetalleNotaSalida(idVenta);
                detalleNotaSalidaBindingSource.DataSource = null;
                detalleNotaSalidaBindingSource.DataSource = listDetalleNotaSalida;
                dgvDetalleNotaSalida.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void cargarProductoDetalle(int tipo)
        {

           
            
            
                if (cbxCodigoProducto.SelectedIndex == -1) return;
                try
                {
                    /// Buscando el producto seleccionado
                 
                    int idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);
                    currentProducto = listProducto.Find(x => x.idProducto == idProducto);
                    cbxDescripcion.SelectedValue = currentProducto.idPresentacion;

                                 
                    cargarAlternativas(tipo);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            
          


        }


       

        private async void cargarAlternativas(int tipo)
        {
            if (cbxCodigoProducto.SelectedIndex == -1) return; /// validacion
            loadState(true);
            try
            {
                List<AlternativaCombinacion> alternativaCombinacion = await alternativaModel.cAlternativa31(currentProducto.idPresentacion);
                alternativaCombinacionBindingSource.DataSource = alternativaCombinacion;
            }                                                  /// cargando las alternativas del producto
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            loadState(false);
        }
       
        private void cbxCodigoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCodigoProducto.SelectedIndex == -1) return;
            txtCantidad.Text = "1";
           
            cargarProductoDetalle(0);
        }

        private void cbxDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDescripcion.SelectedIndex == -1) return;
            txtCantidad.Text = "1";
            int idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
            currentProducto = listProducto.Find(x => x.idPresentacion == idPresentacion);
            cbxCodigoProducto.SelectedValue = currentProducto.idProducto;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //validando campos          
            if (txtCantidad.Text == "")
            {
                txtCantidad.Text = "0";
            }
            

            bool seleccionado = false;
            if (cbxCodigoProducto.SelectedValue != null)
                seleccionado = true;
            if (cbxDescripcion.SelectedValue != null)
                seleccionado = true;

            if (seleccionado)
            {
                if (listDetalleNotaSalida == null) listDetalleNotaSalida = new List<DetalleNotaSalida>();
                DetalleNotaSalida detalleNota = new DetalleNotaSalida();

                try
                {

                    DetalleNotaSalida find = buscarElemento(Convert.ToInt32(cbxDescripcion.SelectedValue), (int)cbxVariacion.SelectedValue);
                    if (find != null)
                    {

                        MessageBox.Show("Este dato ya fue agregado", "presentacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;

                    }


                    // Creando la lista
                    detalleNota.cantidad = toDouble(txtCantidad.Text.Trim());
                    detalleNota.cantidadUnitaria = toDouble(txtCantidad.Text.Trim());
                    detalleNota.codigoProducto = currentProducto.codigoProducto;
                    detalleNota.descripcion = cbxDescripcion.Text.Trim();
                    detalleNota.idCombinacionAlternativa = Convert.ToInt32(cbxVariacion.SelectedValue);
                    detalleNota.idNotaSalida = 0;
                    detalleNota.idDetalleNotaSalida = 0;
                    detalleNota.idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                    detalleNota.idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);
                    detalleNota.nombreCombinacion = cbxVariacion.Text;
                    detalleNota.nombreMarca = currentProducto.nombreMarca;
                    detalleNota.nombrePresentacion = currentProducto.nombreProducto;
                    detalleNota.estado = 1;
                    detalleNota.idDetalleVenta = 0;
                    detalleNota.ventaVarianteSinStock = currentProducto.ventaVarianteSinStock;
                    detalleNota.idVenta = currentVenta == null ? 0 : currentVenta.idVenta;
                    detalleNota.nro = 1;
                    detalleNota.precioEnvio = 0;
                    detalleNota.descuento = 0;// ver en detalle al guardar
                    detalleNota.total = toDouble(txtCantidad.Text.Trim()) *toDouble(currentProducto.precioVenta);
                    detalleNota.alternativas = "";// falta ver este detalle
                    detalleNota.idNotaSalida = currentNotaSalida != null ? currentNotaSalida.idNotaSalida : 0;
                    detalleNota.nombrePresentacion = currentProducto.nombreProducto;
                    // agrgando un nuevo item a la lista

                    // DETALLE NOTA DE ENTRADA
                    detalleNota.cantidadRecibida = toDouble(txtCantidadRecibida.Text);
                    detalleNota.idDetalleNotaEntrada = 0;
                    detalleNota.idNotaEntrada = 0;

                    listDetalleNotaSalida.Add(detalleNota);
                    // DETALLE NOTA DE ENTRADA


                    // Refrescando la tabla
                    detalleNotaSalidaBindingSource.DataSource = null;
                    detalleNotaSalidaBindingSource.DataSource = listDetalleNotaSalida;
                    dgvDetalleNotaSalida.Refresh();
                    // Calculo de totales y subtotales e impuestos



                    limpiarCamposProducto();



                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error: "+ ex.Message, "agregar Elemento", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
               

               
            }
            else
            {

                MessageBox.Show("Error: elemento no seleccionado", "agregar Elemento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           

        }


        private void limpiarCamposProducto()
        {
            cbxCodigoProducto.SelectedIndex = -1;
            cbxDescripcion.SelectedIndex = -1;
            cbxDescripcion.Text = "";
            cbxVariacion.SelectedIndex = -1;
            txtCantidad.Text = "";
            txtCantidadRecibida.Text = "";
            currentProducto = null;
            currentdetalleNotaSalida = null;
          
        }

        private void dgvDetalleNotaSalida_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            if (dgvDetalleNotaSalida.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            indice = dgvDetalleNotaSalida.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleNotaSalida.Rows[indice].Cells[0].Value);
            int idCombinacion = Convert.ToInt32(dgvDetalleNotaSalida.Rows[indice].Cells[1].Value);// obteniedo el idRegistro del datagridview
            currentdetalleNotaSalida = buscarElemento(idPresentacion, idCombinacion); // Buscando la registro especifico en la lista de registros

            cbxCodigoProducto.SelectedValue = currentdetalleNotaSalida.idProducto;
            cbxDescripcion.SelectedValue = currentdetalleNotaSalida.idPresentacion;
            cbxVariacion.Text = currentdetalleNotaSalida.nombreCombinacion;
            txtCantidad.Text = darformato( currentdetalleNotaSalida.cantidad);
            txtCantidadRecibida.Text = darformato(currentdetalleNotaSalida.cantidadRecibida);
            btnModificar.Enabled = true;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = true;
            cbxDescripcion.Enabled = false;
            cbxCodigoProducto.Enabled = false;

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            
            currentProducto = listProducto.Find(X => X.idPresentacion == currentdetalleNotaSalida.idPresentacion);
            currentdetalleNotaSalida.cantidad = toDouble(txtCantidad.Text);
            currentdetalleNotaSalida.cantidadUnitaria = toDouble(txtCantidad.Text);
            currentdetalleNotaSalida.total = toDouble(txtCantidad.Text) *toDouble( currentProducto.precioVenta);
            currentdetalleNotaSalida.nombreCombinacion = cbxVariacion.Text;
            currentdetalleNotaSalida.idCombinacionAlternativa =(int) cbxVariacion.SelectedValue;
            currentdetalleNotaSalida.cantidadRecibida = toDouble(txtCantidadRecibida.Text);

            detalleNotaSalidaBindingSource.DataSource = null;
            detalleNotaSalidaBindingSource.DataSource = listDetalleNotaSalida;
            cbxCodigoProducto.Refresh();
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            cbxDescripcion.Enabled = true;
            cbxCodigoProducto.Enabled = true;
            indice = 0;
            limpiarCamposProducto();


        }

      
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            hacerNota();
        }
        private async void hacerNota()
        {
            // comprobamos la nota 


            if (listDetalleNotaSalida == null)
            {
                listDetalleNotaSalida = new List<DetalleNotaSalida>();
                return;
            }
            if (listDetalleNotaSalida.Count == 0)
            {

                MessageBox.Show("no hay productos seleccionados", "Detalle de Nota", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxCodigoProducto.Focus();
                return;
            }

            ResponseNotaGuardar notaGuardar = null;
            ResponseNotaGuardar notaGuardarE = null;
            ResponseNotaSalida responseNotaSalida = null;
            bool modificar = false;
            loadState(true);
            if (nuevo)
            {
                comprobarNota.idVenta = currentVenta != null ? currentVenta.idVenta : 0;

            }
            else
            {
                comprobarNota.idVenta = currentNotaSalida != null ? Convert.ToInt32(currentNotaSalida.idVenta) : 0;

            }
            comprobarNota.idNotaSalida = currentNotaSalida != null ? currentNotaSalida.idNotaSalida : 0;
            comprobarNota.idAlmacen = (int)cbxAlmacen.SelectedValue;

            almacenSalida.descripcion = txtDescripcion.Text;
            almacenSalida.destino = txtDireccionDestino.Text;
            almacenSalida.idNotaSalida = currentNotaSalida != null ? currentNotaSalida.idNotaSalida : 0;

            int estado = 0;
            switch (cbxEstado.Text)
            {
                case "Pendiente":
                    estado = 0;
                    break;
                case "Revisado":
                    estado = 1;
                    break;
                case "Enviado":
                    estado = 2;
                    break;


            }

            almacenSalida.estadoEnvio = estado;
            string date1 = String.Format("{0:u}", dtpFechaEntrega.Value);
            date1 = date1.Substring(0, date1.Length - 1);
            almacenSalida.fechaSalida = date1;
            almacenSalida.idAlmacen = (int)cbxAlmacen.SelectedValue;
            almacenSalida.idPersonal = PersonalModel.personal.idPersonal;
            almacenSalida.idTipoDocumento = 8;//nota salida
            almacenSalida.motivo = txtMotivo.Text;

            int numert = 0;
            foreach (DetalleNotaSalida detalle in listDetalleNotaSalida)
            {
                List<object> listaux = new List<object>();

                listaux.Add(detalle.idProducto);
                listaux.Add(detalle.idCombinacionAlternativa);
                int cantidad = Convert.ToInt32(detalle.cantidad, CultureInfo.GetCultureInfo("en-US"));
                listaux.Add(cantidad);
                listaux.Add(detalle.ventaVarianteSinStock);
                listint.Add(listaux);

                DetallesNotaSalida.Add("id" + numert, detalle);

                dictionary.Add("id" + numert, detalle.cantidadUnitaria);
                numert++;

            }
            comprobarNota.dato = listint;
            try
            {
                responseNotaSalida = await notaSalidaModel.verifcar(comprobarNota);

                if (responseNotaSalida.cumple.cumple == 1 && responseNotaSalida.abastece.abastece == 1)
                {


                    listElementosNotaSalida.Add(almacenSalida);
                    listElementosNotaSalida.Add(ventaSalida);
                    listElementosNotaSalida.Add(DetallesNotaSalida);
                    listElementosNotaSalida.Add(dictionary);
                    listElementosNotaSalida.Add(object4);
                    listElementosNotaSalida.Add(object5);
                    listElementosNotaSalida.Add(object6);
                    listElementosNotaSalida.Add(object7);

                    if (nuevo)
                    {
                        notaGuardar = await notaSalidaModel.guardar(listElementosNotaSalida);


                    }
                    else
                    {
                        notaGuardar = await notaSalidaModel.modificar(listElementosNotaSalida);
                        modificar = true;
                    }



                }
                else
                {

                    MessageBox.Show(" no cumple" + " exite: " + responseNotaSalida.abastece.cantidades + "  producto: " + responseNotaSalida.abastece.productos, "verificar Nota Salida", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    dictionary.Clear();
                    DetallesNotaSalida.Clear();
                    listint.Clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "guardar Nota Salida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dictionary.Clear();
                DetallesNotaSalida.Clear();
                listint.Clear();
            }

            // para generar la nota de entrada


            if (nuevo && (int)cbxAlmacenEntrada.SelectedValue != 0 && notaGuardar != null)
            {

                comprobarNotaE.idCompra = 0;//
                comprobarNotaE.idNotaEntrada = 0;// en modificar puede variar         
                almacenNEntrada.estadoEntrega = chbxEntrega.Checked ? 1 : 0;

                almacenNEntrada.idNotaEntrada = 0;
                date1 = String.Format("{0:u}", dtpFechaEntrega.Value);
                date1 = date1.Substring(0, date1.Length - 1);
                almacenNEntrada.fechaEntrada = date1;// probar con el otro 
                almacenNEntrada.idAlmacen = (int)cbxAlmacenEntrada.SelectedValue;
                almacenNEntrada.idPersonal = PersonalModel.personal.idPersonal;
                almacenNEntrada.idTipoDocumento = 7;// nota de entrada
                almacenNEntrada.observacion = txtDescripcion.Text;

                compraEntradaGuardar.idCompra = 0;

                numert = 0;
                foreach (DetalleNotaSalida detalle in listDetalleNotaSalida)
                {

                    DetallesNota.Add("id" + numert, detalle);

                    dictionaryE.Add("id" + numert, detalle.cantidadRecibida);
                    numert++;
                    List<int> listaux = new List<int>();
                    listaux.Add(detalle.idProducto);
                    listaux.Add(detalle.idCombinacionAlternativa);

                    int cantidad = Convert.ToInt32(detalle.cantidad, CultureInfo.GetCultureInfo("en-US"));
                    listaux.Add(cantidad);
                    listintE.Add(listaux);

                }
                comprobarNotaE.dato = listintE;
                ResponseNota responseNota = null;
                try
                {
                    responseNota = await entradaModel.verifcar(comprobarNotaE);

                    if (responseNota.cumple == 1)
                    {
                        listElementosNotaEntrada.Add(almacenNEntrada);
                        listElementosNotaEntrada.Add(compraEntradaGuardar);
                        listElementosNotaEntrada.Add(DetallesNota);
                        listElementosNotaEntrada.Add(dictionaryE);
                        listElementosNotaEntrada.Add(object4E);
                        listElementosNotaEntrada.Add(object5E);
                        listElementosNotaEntrada.Add(object6E);
                        listElementosNotaEntrada.Add(object7E);
                        notaGuardarE = null;
                        if (nuevo)
                        {
                            notaGuardarE = await entradaModel.guardar(listElementosNotaEntrada);

                        }
                        else
                        {
                            notaGuardarE = await entradaModel.modificar(listElementosNotaEntrada);
                        }

                        if (notaGuardarE.id > 0)
                        {
                            MessageBox.Show(notaGuardar.msj + " " + "corectamente", "guardar Nota Entrada - " + listAlmacenOrigen.Find(X => X.idAlmacen == (int)cbxAlmacenEntrada.SelectedValue).nombre, MessageBoxButtons.OK, MessageBoxIcon.Information);


                        }

                    }

                    else
                    {

                        MessageBox.Show("no se tiene suficente stock para los productos", "verificar Nota Entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dictionaryE.Clear();
                        DetallesNota.Clear();
                        listintE.Clear();
                        responseNota = new ResponseNota();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Guardar Nota  de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dictionaryE.Clear();
                    DetallesNota.Clear();
                    listintE.Clear();
                    responseNota = new ResponseNota();
                }





            }



            if (notaGuardar != null)
            {

                if (notaGuardar.id > 0)
                {
                    if (!modificar)
                    {

                        DialogResult dialog = MessageBox.Show("guardado correctamente,  ¿Desea hacer la guia de remision?", "Nota de Salida - " + listAlmacenOrigen.Find(X => X.idAlmacen == (int)cbxAlmacen.SelectedValue).nombre,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dialog == DialogResult.No)
                        {

                            this.Close();
                            return;
                        }
                        try
                        {

                        List<NotaSalidaR> listNotasalida3 = await notaSalidaModel.nSalida((int)cbxAlmacen.SelectedValue);

                        FormRemisionNew formRemisionNew = new FormRemisionNew(listNotasalida3.Find(X => X.idNotaSalida == notaGuardar.id));
                        formRemisionNew.ShowDialog();
                        this.Close();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "cargar guia de remision", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                        // currentNotaSalida= 
                       

                    }
                    else
                    {

                        MessageBox.Show(notaGuardar.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();

                    }


                }
                else
                {
                    MessageBox.Show(notaGuardar.msj, "guardar Nota Salida", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                }


            }

            // para generar la nota de entrada

            // comprobamos la nota             

            loadState(false);
        }



        private void btnQuitar_Click(object sender, EventArgs e)
        {

            currentNotaSalida = null;
            // cargar informacion
            txtNroDocumentoVenta.Text = "";
            txtNombreCliente.Text = "";
            txtDocumentoCliente.Text = "";
            detalleNotaSalidaBindingSource.DataSource = null;
            dgvDetalleNotaSalida.Refresh();
            btnQuitar.Enabled = false;

            ocultarControlesVenta();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            cbxDescripcion.Enabled = true;
            cbxCodigoProducto.Enabled = true;
            dgvDetalleNotaSalida.Rows.RemoveAt(indice);
            listDetalleNotaSalida.Remove(currentdetalleNotaSalida);
            limpiarCamposProducto();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            int X = 0;
            int Y = 0;
            int XI = 0;


            Dictionary<string, Point> dictionary = new Dictionary<string, Point>();
            foreach (FormatoDocumento doc in listformato)
            {


                string tipo = doc.tipo;

                switch (tipo)
                {
                    case "Label":

                        int v = 0;
                        if (this.Controls.Find("txt" + doc.value, true).Count() > 0)
                            if (((this.Controls.Find("txt" + doc.value, true).First() as TextBox) != null))
                            {
                                TextBox textBox = this.Controls.Find("txt" + doc.value, true).First() as TextBox;
                                e.Graphics.DrawString(textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));
                                v++;
                            }
                        if (this.Controls.Find("cbx" + doc.value, true).Count() > 0)
                            if (((this.Controls.Find("cbx" + doc.value, true).First() as ComboBox) != null))
                            {
                                ComboBox textBox = this.Controls.Find("cbx" + doc.value, true).First() as ComboBox;
                                e.Graphics.DrawString(textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));
                                v++;
                            }
                        if (this.Controls.Find("dtp" + doc.value, true).Count() > 0)
                            if (((this.Controls.Find("dtp" + doc.value, true).First() as DateTimePicker) != null))
                            {
                                DateTimePicker textBox = this.Controls.Find("dtp" + doc.value, true).First() as DateTimePicker;
                                e.Graphics.DrawString(textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));
                                v++;
                            }


                        if (v == 0)
                        {

                            switch (doc.value)
                            {
                                case "SerieCorrelativo":

                                    e.Graphics.DrawString(txtSerie.Text + "-" + txtCorrelativo.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));


                                    break;
                                case "DescripcionEmpresa":

                                    e.Graphics.DrawString(ConfigModel.datosGenerales.razonSocial, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));


                                    break;

                                case "DireccionEmpresa":

                                    e.Graphics.DrawString(ConfigModel.datosGenerales.direccion, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));


                                    break;
                                case "DocumentoEmpresa":

                                    e.Graphics.DrawString(ConfigModel.datosGenerales.ruc, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));


                                    break;
                                case "NombreEmpresa":

                                    e.Graphics.DrawString(ConfigModel.datosGenerales.razonSocial, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x, doc.y));


                                    break;




                            }




                        }

                        break;
                    case "ListGrid":
                        X = (int)doc.x;
                        Y = (int)doc.y;
                        XI = X;
                        break;
                    case "ListGridField":

                        e.Graphics.DrawString(doc.value, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(XI, Y));
                        dictionary.Add(doc.value, new Point(XI, Y));

                        //int YI = Y+30;
                        //foreach(DetalleV V in  detalleVentas)
                        //{
                        //    e.Graphics.DrawString(V.cantidad, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(XI, YI));
                        //    YI += 30;
                        //}
                        XI += X + (int)(doc.w);




                        break;
                    case "Img":

                        Image image = Resources.logo1;

                        e.Graphics.DrawImage(image, doc.x, doc.y, (int)doc.w, (int)doc.h);

                        break;

                }


            }

            Point point = dictionary["codigoProducto"];
            int YI = point.Y + 30;
            Point point1 = new Point();

            if (listDetalleNotaSalida == null) listDetalleNotaSalida = new List<DetalleNotaSalida>();



            for (int i = numberOfItemsPrintedSoFar; i < listDetalleNotaSalida.Count; i++)
            {
                numberOfItemsPerPage++;

                if (numberOfItemsPerPage <= 2)
                {
                    numberOfItemsPrintedSoFar++;

                    if (numberOfItemsPrintedSoFar <= listDetalleNotaSalida.Count)
                    {

                        if (dictionary.ContainsKey("codigoProducto"))
                        {

                            point1 = dictionary["codigoProducto"];
                            e.Graphics.DrawString(listDetalleNotaSalida[i].codigoProducto, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }

                        if (dictionary.ContainsKey("nombreCombinacion"))
                        {
                            point1 = dictionary["nombreCombinacion"];
                            e.Graphics.DrawString(listDetalleNotaSalida[i].nombreCombinacion, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));

                        }
                        if (dictionary.ContainsKey("cantidad"))
                        {
                            point1 = dictionary["cantidad"];
                            e.Graphics.DrawString(darformato( listDetalleNotaSalida[i].cantidad), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }
                        if (dictionary.ContainsKey("cantidadcantidadUnitaria"))
                        {
                            point1 = dictionary["cantidadcantidadUnitaria"];
                            e.Graphics.DrawString(darformato(listDetalleNotaSalida[i].cantidadUnitaria), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }

                        if (dictionary.ContainsKey("nombrePresentacion"))
                        {
                            point1 = dictionary["nombrePresentacion"];
                            e.Graphics.DrawString(listDetalleNotaSalida[i].nombrePresentacion, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }
                        if (dictionary.ContainsKey("descripcion"))
                        {
                            point1 = dictionary["descripcion"];
                            e.Graphics.DrawString(listDetalleNotaSalida[i].descripcion, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));

                        }
                        if (dictionary.ContainsKey("nombreMarca"))
                        {
                            point1 = dictionary["nombreMarca"];
                            e.Graphics.DrawString(listDetalleNotaSalida[i].nombreMarca, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }
                       

                        if (dictionary.ContainsKey("total"))
                        {
                            point1 = dictionary["total"];


                            e.Graphics.DrawString(darformato(listDetalleNotaSalida[i].total), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }
                       
                        YI += 30;



                    }
                    else
                    {
                        e.HasMorePages = false;
                    }
                }
                else
                {
                    numberOfItemsPerPage = 0;
                    e.HasMorePages = true;
                    return;
                }
            }



            numberOfItemsPerPage = 0;
            numberOfItemsPrintedSoFar = 0;

            foreach (FormatoDocumento doc in listformato)
            {


                string tipo = doc.tipo;

                switch (tipo)
                {
                    case "Label":


                        if (this.Controls.Find("lb" + doc.value, true).Count() > 0)
                            if (((this.Controls.Find("lb" + doc.value, true).First() as Label) != null))
                            {
                                Label textBox = this.Controls.Find("lb" + doc.value, true).First() as Label;
                                if (doc.value == "Total")
                                {
                                    e.Graphics.DrawString(doc.value + ": " + textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x - 5, doc.y));


                                }
                                else
                                    e.Graphics.DrawString(doc.value + ": " + textBox.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(doc.x - 31, doc.y));
                            }

                        break;
                }
            }

            numberOfItemsPerPage = 0;
            numberOfItemsPrintedSoFar = 0;




        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            FormatoDocumento doc = listformato.Last();
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("tamaño pagina", (int)doc.w, (int)doc.h);

            // pre visualizacion
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
            {

        }

        private void splitter2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cbxAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAlmacen.SelectedIndex == -1) return;
            if (filtradoAlmacen == 0)
                filtradoAlmacen = 1;
            int idAlmacen = (int)cbxAlmacen.SelectedValue;

            if (filtradoAlmacen == 1)
            {
                int idAlmacenS = (int)cbxAlmacenEntrada.SelectedValue;
                BindingList<Almacen> filtered = new BindingList<Almacen>(listAlmacenDestino.Where(obj => obj.idAlmacen !=idAlmacen).ToList());
                cbxAlmacenEntrada.DataSource = filtered;
                cbxAlmacenEntrada.Update();
                filtradoAlmacen = 0;

                if (idAlmacen != idAlmacenS)
                {
                    cbxAlmacenEntrada.SelectedValue = idAlmacenS;
                }



            }
          
        }

        private void cbxAlmacenEntrada_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbxAlmacenEntrada.SelectedIndex == -1) return;
            if (filtradoAlmacen == 0)
                filtradoAlmacen = 2;
            if (filtradoAlmacen == 2)
            {
                int idAlmacen = (int)cbxAlmacenEntrada.SelectedValue;
                int idAlmacenS = (int)cbxAlmacen.SelectedValue;


                if (idAlmacen == 0)
                {
                    txtCantidadRecibida.Enabled = false;
                    label1.Enabled = false;

                    dgvDetalleNotaSalida.Columns["cantidadRecibida"].Visible=false;


                }
                else
                {
                    Almacen almacen1 = listAlmacenOrigen.Find(X=>X.idAlmacen== idAlmacen);
                    Almacen almacen2= listAlmacenOrigen.Find(X => X.idAlmacen == idAlmacenS);
                    if (almacen1.idSucursal == almacen2.idSucursal)
                    {
                        chbxEntrega.Checked = true;
                        chbxEntrega.Enabled = true;
                        cbxEstado.SelectedIndex = 2;
                        cbxEstado.Enabled = true;
                    }
                    else
                    {
                        chbxEntrega.Checked = false;
                        chbxEntrega.Enabled = false;
                        cbxEstado.SelectedIndex = 0;
                        cbxEstado.Enabled = false;
                    }
                    txtCantidadRecibida.Enabled = true;
                    label1.Enabled = true;
                    dgvDetalleNotaSalida.Columns["cantidadRecibida"].Visible = true;
                }
                BindingList<Almacen> filtered = new BindingList<Almacen>(listAlmacenOrigen.Where(obj => obj.idAlmacen != idAlmacen).ToList());
                cbxAlmacen.DataSource = filtered;
                cbxAlmacen.Update();

                if (idAlmacen != idAlmacenS)
                {
                    cbxAlmacen.SelectedValue = idAlmacenS;
                }

                filtradoAlmacen = 0;
            }
         
        }

        private void txtCantidadRecibida_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "")
                txtCantidadRecibida.Text = darformato(0);

            txtCantidadRecibida.Text = txtCantidad.Text;
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private void txtCantidadRecibida_TextChanged(object sender, EventArgs e)
        {

            double cantidadRecibida = 0;
            double cantidad = 0;

            if (txtCantidadRecibida.Text != "")
            {
                cantidadRecibida = toDouble(txtCantidadRecibida.Text);
            }           
            if (txtCantidad.Text != "")
            {
                cantidad = toDouble(txtCantidad.Text);
            }
            if(cantidadRecibida> cantidad)
            {
                txtCantidadRecibida.Text =  cantidad.ToString();
            }
        }

        private void cbxCodigoProducto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cbxDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cbxVariacion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (txtCantidadRecibida.Enabled)
                    this.SelectNextControl((Control)sender, true, true, true, true);
                else
                    btnAgregar.Focus();
            }
        }

        private void txtCantidadRecibida_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                btnAgregar.Focus();
            }
        }

        private void btnAgregar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cbxAlmacen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void dtpFechaEntrega_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dtpFechaEntrega_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cbxEstado_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtDireccionDestino_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtMotivo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cbxAlmacenEntrada_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void chbxEntrega_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }
    }
}
