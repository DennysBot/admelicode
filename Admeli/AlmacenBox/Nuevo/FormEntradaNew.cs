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
using Entidad;
using Entidad.Configuracion;
using Modelo;
using Newtonsoft.Json;

namespace Admeli.AlmacenBox.Nuevo
{
    public partial class FormEntradaNew : Form
    {

        // objetos Necesarios para Guardar y modificar una nota entrada
        //==para verificacion
        ComprobarNota comprobarNota { get; set; }

        List<List<int>> listint { get; set; }
        //===
        AlmacenNEntrada almacenNEntrada { get; set; }
        CompraEntradaGuardar compraEntradaGuardar { get; set; }
        Dictionary<string, double> dictionary { get; set; }
        Dictionary<string, CargaCompraSinNota> DetallesNota  { get; set; }

        object object4 { get; set; }
        object object5 { get; set; }
        object object6 { get; set; }
        object object7 { get; set; }
        List<object> listElementosNotaEntrada { get; set; }
        // servicios necesarios
        AlmacenModel AlmacenModel = new AlmacenModel();
        ProductoModel productoModel = new ProductoModel();
        PresentacionModel presentacionModel = new PresentacionModel();
        FechaModel fechaModel = new FechaModel();
        CompraModel compraModel = new CompraModel();
        NotaEntradaModel entradaModel = new NotaEntradaModel();

        AlternativaModel alternativaModel = new AlternativaModel();
        // objetos que cargan a un inicio
        private  List<Producto > listProducto { get; set; }
        private List<Presentacion> listPresentacion { get; set; }
        private List<Almacen> listAlmacenEntrada { get; set; }
        private List<Almacen> listAlmacenSalida { get; set; }
        private List<AlmacenCorrelativo> listCorrelativoA { get; set; }
        private List<CargaCompraSinNota> listcargaCompraSinNota { get; set; }// detalles de compra
          


        // entidadades auxiliares

        private bool  nuevo { get; set; }
        private string formato { get; set; }
        private int nroDecimales = 2;
        private FechaSistema fechaSistema { get; set; }

        private CompraNEntrada currentCompraNEntrada { get; set; }

        //objetos en tiempo real

        private Almacen currentAlmacen { get; set; }
       private AlmacenCorrelativo currentCorrelativoA { get; set; }
       private Producto currentProducto { get; set; }
       private Presentacion currentPresentacion { get; set; }

        private NotaEntrada currentNotaEntrada { get; set; }
        private CargaCompraSinNota currentDetalleNEntrada { get; set; }



        private int numberOfItemsPerPage = 0;
        private int numberOfItemsPrintedSoFar = 0;

        List<FormatoDocumento> listformato;

        private int indice = 0;
        private int filtradoAlmacen = 0;
        public FormEntradaNew()
        {
            InitializeComponent();
            this.nuevo = true;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();
      

        }
        public FormEntradaNew(NotaEntrada currentNotaEntrada)
        {
            InitializeComponent();
            this.nuevo = false;
            formato = "{0:n" + nroDecimales + "}";
            cargarFechaSistema();
            this.currentNotaEntrada = currentNotaEntrada;

        }

        #region=======================metodos de apoyo
        private string darformato(object dato)
        {
            return string.Format(CultureInfo.GetCultureInfo("en-US"), this.formato, dato);
        }

        private double toDouble(string texto)
        {
            return double.Parse(texto, CultureInfo.GetCultureInfo("en-US")); ;
        }


        #endregion
        #region ================================ Root Load ================================

        private void FormNotaSalidaNew_Load(object sender, EventArgs e)
        {
            if (nuevo == true)
                this.reLoad();
            else
            {
                this.reLoad();
                cargarDatosNotaEntrada();
            
            }

        }
        private void reLoad()
        {
            cargarObjetos();
            cargarAlmacenes();
            cargarProductos();
            cargarFormatoDocumento();
        }

        #endregion

        #region ============================== Load ==============================

        private void cargarFormatoDocumento()
        {


            TipoDocumento tipoDocumento = ConfigModel.tipoDocumento.Find(X => X.idTipoDocumento == 7);// nota entrada
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
        private async void cargarDatosNotaEntrada()
        {
            try
            {
                //datos

                if (currentNotaEntrada.idCompra != "0")
                {
                    txtNroDocumento.Text = currentNotaEntrada.numeroDocumento;
                    txtNombreProveedor.Text = currentNotaEntrada.nombreProveedor;
                    txtDocumentoProveedor.Text = currentNotaEntrada.rucDni;
                    currentCompraNEntrada = new CompraNEntrada();
                    currentCompraNEntrada.idCompra = Convert.ToInt32(currentNotaEntrada.idCompra);
                }

                // serie
                txtSerie.Text = currentNotaEntrada.serie;
                txtCorrelativo.Text = currentNotaEntrada.correlativo;
                cbxAlmacenEntrada.SelectedValue = currentNotaEntrada.idAlmacen;
                dtpFechaEntrega.Value = currentNotaEntrada.fechaEntrada.date;
                txtObservaciones.Text = currentNotaEntrada.observacion;
                chbxEntrega.Checked = currentNotaEntrada.estadoEntrega == 1 ? true : false;
                // cargar detalles de la nota
                listcargaCompraSinNota = await entradaModel.cargarDetallesNota(currentNotaEntrada.idNotaEntrada);
                cargaCompraSinNotaBindingSource.DataSource = listcargaCompraSinNota;
                btnImportarCompra.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Caragar Datos Nota Entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void cargarObjetos()
        {   
            //==
             comprobarNota = new ComprobarNota();
             listint = new List<List<int>>();
            //===
             almacenNEntrada = new AlmacenNEntrada();
             compraEntradaGuardar = new CompraEntradaGuardar();
             dictionary = new Dictionary<string, double>();
             DetallesNota = new Dictionary<string, CargaCompraSinNota>();
             object4 = new object();
             object5 = new object();
             object6 = new object();
             object7 = new object();
             listElementosNotaEntrada = new List<object>();

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnQuitar.Enabled = false;
        }
        private async void cargarAlmacenes()
        {
            //try
            //{
            //    listAlmacen = await AlmacenModel.almacenesAsignados(ConfigModel.sucursal.idSucursal, PersonalModel.personal.idPersonal);
            //    almacenBindingSource.DataSource = listAlmacen;
            //    cbxAlmacen.SelectedIndex = 0;
            //    currentAlmacen = listAlmacen[0];
            //    if (nuevo)
            //        cargarDocCorrelativo(currentAlmacen.idAlmacen);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message, "Cargar Almacenes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}


            try
            {
              
                listAlmacenEntrada = await AlmacenModel.almacenesAsignados(0, PersonalModel.personal.idPersonal);//  para todos las asignadas al personal

                almacenBindingSource.DataSource = listAlmacenEntrada;
                cbxAlmacenEntrada.SelectedIndex = -1;

                listAlmacenSalida = new List<Almacen>();
                Almacen almacen = new Almacen();
                almacen.idAlmacen = 0;
                almacen.nombre = "Ninguno";
                listAlmacenSalida.Add(almacen);
                listAlmacenSalida.AddRange(listAlmacenEntrada);

                almacenBindingSource1.DataSource = listAlmacenSalida;
                currentAlmacen = listAlmacenEntrada.Find(X => X.idAlmacen == ConfigModel.currentIdAlmacen);
                cbxAlmacenEntrada.SelectedValue = currentAlmacen.idAlmacen;
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

        }
        private async void cargarDocCorrelativo(int idAlmacen)
        {
            try
            {
                if (nuevo)
                {
                    listCorrelativoA = await AlmacenModel.DocCorrelativoAlmacen(idAlmacen, 7);
                    currentCorrelativoA = listCorrelativoA[0];
                    txtSerie.Text = currentCorrelativoA.serie;
                    txtCorrelativo.Text = currentCorrelativoA.correlativoActual;

                }
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
                listProducto = await productoModel.productos();
                productoBindingSource.DataSource = listProducto;
                cbxCodigoProducto.SelectedIndex = -1;
                cbxDescripcion.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar productos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        #region ================= METODOS DE APOYO ===================
        private CargaCompraSinNota buscarElemento(int idPresentacion, int idCombinacion)
        {

            try
            {
                return listcargaCompraSinNota.Find(x => x.idPresentacion == idPresentacion && x.idCombinacionAlternativa == idCombinacion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar fechas del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }


        }

        #endregion====================================================

        //eventos



        private async void btnImportarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                FormBuscarCompra formBuscarCompra = new FormBuscarCompra();
                formBuscarCompra.ShowDialog();

                currentCompraNEntrada = formBuscarCompra.currentCompraNEntrada;

                if (currentCompraNEntrada != null)
                {
                    // cargar informacion
                    txtNroDocumento.Text = currentCompraNEntrada.numeroDocumento;
                    txtNombreProveedor.Text = currentCompraNEntrada.nombreProveedor;
                    txtDocumentoProveedor.Text = currentCompraNEntrada.rucDni;
                    listcargaCompraSinNota = await entradaModel.CargarCompraSinNota(currentCompraNEntrada.idCompra);
                    cargaCompraSinNotaBindingSource.DataSource = null;
                    cargaCompraSinNotaBindingSource.DataSource = listcargaCompraSinNota;

                    btnQuitar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "btnImportarCompra_Click", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void cbxCodigoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (cbxCodigoProducto.SelectedIndex == -1) return;
            txtCantidad.Text =  "1" ;
            txtCantidadRecibida.Text = "1" ;

            cargarProductoDetalle(0);
        }
        private void cbxDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbxDescripcion.SelectedIndex == -1) return;
            txtCantidad.Text = "1";
            txtCantidadRecibida.Text = "1";
            cargarProductoDetalle(1);
        }

        private async  void cargarPrecio()
        {
            try
            {


            }
            catch(Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message, "cargar Precio de Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }



        // metodos usados por lo eventos
        private void cargarProductoDetalle(int tipo)
        {

            
            if (tipo == 0)
            {

                if (cbxCodigoProducto.SelectedIndex == -1) return;
                try
                {
                    /// Buscando el producto seleccionado
                 
                   
                    int idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);
                    currentProducto = listProducto.Find(x => x.idProducto == idProducto);
                    cbxDescripcion.SelectedValue = currentProducto.idPresentacion;                               
                    cargarAlternativas(tipo);

                    cargarPrecio();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "cargar producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                if (cbxDescripcion.SelectedIndex == -1) return;
                try
                {
                    /// Buscando el producto seleccionado
                    /// 
                    int idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                    currentProducto = listProducto.Find(x => Convert.ToInt32( x.idPresentacion) == idPresentacion);
                    cbxCodigoProducto.SelectedValue = currentProducto.idProducto;               
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "cargar presentacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }


        }


        private async void cargarPresentaciones(int idProducto, int tipo)
        {
            try
            {
                List<Presentacion> presentaciones = await presentacionModel.presentacionVentas(idProducto);
                currentPresentacion = presentaciones[0];

                cbxDescripcion.Text = currentPresentacion.nombrePresentacion;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Presentaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarAlternativas(int tipo)
        {
            if (cbxCodigoProducto.SelectedIndex == -1) return; /// validacion
            try
            {
                List<AlternativaCombinacion> alternativaCombinacion = await alternativaModel.cAlternativa31(Convert.ToInt32(cbxCodigoProducto.SelectedValue));
                alternativaCombinacionBindingSource.DataSource = alternativaCombinacion;                              
            }                                                  /// cargando las alternativas del producto
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar combinaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
       

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //validando campos          
            if (txtCantidad.Text == "")
            {
                txtCantidad.Text = "0";
            }
            if (txtCantidadRecibida.Text == "")
            {
                txtCantidadRecibida.Text = "0";
            }

            bool seleccionado = false;
            if (cbxCodigoProducto.SelectedValue != null)
                seleccionado = true;
            if (cbxDescripcion.SelectedValue != null)
                seleccionado = true;

            if (seleccionado)
            {
                if (listcargaCompraSinNota == null) listcargaCompraSinNota = new List<CargaCompraSinNota>();
                CargaCompraSinNota detalleNota = new CargaCompraSinNota();

              

                currentDetalleNEntrada = buscarElemento(Convert.ToInt32(cbxDescripcion.SelectedValue),(int)cbxVariacion.SelectedValue);
                if (currentDetalleNEntrada != null)
                {

                    MessageBox.Show("Este dato ya fue agregado", "presentacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }

               
                // Creando la lista
                detalleNota.cantidad = toDouble(txtCantidad.Text.Trim());
                /// Busqueda presentacion
                detalleNota.cantidadUnitaria = toDouble(txtCantidad.Text.Trim());
                detalleNota.codigoProducto = currentProducto.codigoProducto;
                detalleNota.descripcion = cbxDescripcion.Text.Trim();                           
                detalleNota.idCombinacionAlternativa = Convert.ToInt32(cbxVariacion.SelectedValue);
               
                detalleNota.idNotaEntrada = 0;
                detalleNota.idDetalleNotaEntrada = 0;
                detalleNota.idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                detalleNota.idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);              
                detalleNota.nombreCombinacion = cbxVariacion.Text;
                detalleNota.nombreMarca = currentProducto.nombreMarca;
                detalleNota.nombrePresentacion = currentProducto.nombreProducto;
                detalleNota.cantidadRecibida = toDouble(txtCantidadRecibida.Text.Trim());
                detalleNota.estado = 1;



                // datos para la nota de entrada








                // agrgando un nuevo item a la lista
                listcargaCompraSinNota.Add(detalleNota);
                // Refrescando la tabla
                cargaCompraSinNotaBindingSource.DataSource = null;
                cargaCompraSinNotaBindingSource.DataSource = listcargaCompraSinNota;
                dgvDetalleNota.Refresh();
                // Calculo de totales y subtotales e impuestos
              
                limpiarCamposProducto();

              
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
            cbxVariacion.SelectedIndex = -1;
            txtCantidad.Text = "";
            txtCantidadRecibida.Text = "";
         
            currentProducto = null;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            // comprobamos la nota             
            comprobarNota.idCompra = currentCompraNEntrada != null ? currentCompraNEntrada.idCompra : 0;
            comprobarNota.idNotaEntrada = currentNotaEntrada !=null ? currentNotaEntrada.idNotaEntrada: 0;// en modificar puede variar         
            almacenNEntrada.estadoEntrega = chbxEntrega.Checked? 1 : 0 ;

            almacenNEntrada.idNotaEntrada = currentNotaEntrada != null ? currentNotaEntrada.idNotaEntrada : 0; ;
            string date1 = String.Format("{0:u}", dtpFechaEntrega.Value);
            date1 = date1.Substring(0, date1.Length - 1);
            almacenNEntrada.fechaEntrada = date1;// probar con el otro 
            almacenNEntrada.idAlmacen = (int)cbxAlmacenEntrada.SelectedValue;
            almacenNEntrada.idPersonal = PersonalModel.personal.idPersonal;
            almacenNEntrada.idTipoDocumento = 7;// nota de entrada
            almacenNEntrada.observacion = txtObservaciones.Text;
            
            compraEntradaGuardar.idCompra = currentCompraNEntrada != null ? currentCompraNEntrada.idCompra:0;
                            
            int numert = 0;
            foreach (CargaCompraSinNota detalle in listcargaCompraSinNota)
            {
               
                DetallesNota.Add("id" + numert, detalle);

                dictionary.Add("id" + numert, detalle.cantidadRecibida);
                numert++;
                List<int> listaux = new List<int>();
                listaux.Add(detalle.idProducto);
                listaux.Add(detalle.idCombinacionAlternativa);

                int cantidad = Convert.ToInt32(detalle.cantidad,CultureInfo.GetCultureInfo("en-US"));
                listaux.Add(cantidad);
                listint.Add(listaux);

            }
            comprobarNota.dato = listint;
            try
            {
                ResponseNota responseNota = await entradaModel.verifcar(comprobarNota);

                if (responseNota.cumple == 1)
                {
                    listElementosNotaEntrada.Add(almacenNEntrada);
                    listElementosNotaEntrada.Add(compraEntradaGuardar);
                    listElementosNotaEntrada.Add(DetallesNota);
                    listElementosNotaEntrada.Add(dictionary);
                    listElementosNotaEntrada.Add(object4);
                    listElementosNotaEntrada.Add(object5);
                    listElementosNotaEntrada.Add(object6);
                    listElementosNotaEntrada.Add(object7);
                    ResponseNotaGuardar notaGuardar = null;
                    if (nuevo)
                    {
                        notaGuardar = await entradaModel.guardar(listElementosNotaEntrada);

                    }
                    else
                    {
                        notaGuardar = await entradaModel.modificar(listElementosNotaEntrada);
                    }

                    if (notaGuardar.id > 0)
                    {
                        MessageBox.Show(notaGuardar.msj, "guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();


                    }

                }

                else
                {

                    MessageBox.Show("no se tiene suficente stock para los productos", "verificar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dictionary.Clear();
                    DetallesNota.Clear();
                    listint.Clear();
                    responseNota = new ResponseNota();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "guardar ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void dgvDetalleNota_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificando la existencia de datos en el datagridview
            if (dgvDetalleNota.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            indice = dgvDetalleNota.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleNota.Rows[indice].Cells[3].Value);
            int idCombinacion = Convert.ToInt32(dgvDetalleNota.Rows[indice].Cells[4].Value);
            // obteniedo el idRegistro del datagridview
            currentDetalleNEntrada = buscarElemento(idPresentacion,idCombinacion); // Buscando la registro especifico en la lista de registros
            
            cbxCodigoProducto.SelectedValue = currentDetalleNEntrada.idProducto;
            cbxDescripcion.SelectedValue = currentDetalleNEntrada.idPresentacion.ToString(); ;
            cbxVariacion.Text = currentDetalleNEntrada.nombreCombinacion;
            txtCantidad.Text = Convert.ToInt32(currentDetalleNEntrada.cantidad).ToString();
            txtCantidadRecibida.Text =Convert.ToInt32(currentDetalleNEntrada.cantidadRecibida).ToString();

            btnModificar.Enabled = true;
            btnAgregar.Enabled = false;
            btnEliminar.Enabled = true;
            cbxCodigoProducto.Enabled = false;
            cbxDescripcion.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleNota.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            currentDetalleNEntrada.cantidad = toDouble(txtCantidad.Text);
            currentDetalleNEntrada.cantidadUnitaria = toDouble(txtCantidad.Text);
            currentDetalleNEntrada.cantidadRecibida= toDouble(txtCantidadRecibida.Text);
            currentDetalleNEntrada.nombreCombinacion = cbxVariacion.Text;
            currentDetalleNEntrada.idCombinacionAlternativa =(int) cbxVariacion.SelectedValue;
            detalleCompraBindingSource.DataSource = null;
            detalleCompraBindingSource.DataSource = listcargaCompraSinNota;
            dgvDetalleNota.Refresh();
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled= false;
            cbxCodigoProducto.Enabled = true;
            cbxDescripcion.Enabled = true;
            indice = 0;
            limpiarCamposProducto();


        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
           
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            cbxCodigoProducto.Enabled = true;
            cbxDescripcion.Enabled = true;
            
            dgvDetalleNota.Rows.RemoveAt(indice);
            listcargaCompraSinNota.Remove(currentDetalleNEntrada);
            limpiarCamposProducto();
            indice = 0;
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
             currentCompraNEntrada = null;
                // cargar informacion
             txtNroDocumento.Text = "";
             txtNombreProveedor.Text = "";
             txtDocumentoProveedor.Text = "";               
              cargaCompraSinNotaBindingSource.DataSource = null;
              dgvDetalleNota.Refresh();
               btnQuitar.Enabled = false;
          
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            FormatoDocumento doc = listformato.Last();
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("tamaño pagina", (int)doc.w, (int)doc.h);

            // pre visualizacion
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
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

            if (listcargaCompraSinNota == null) listcargaCompraSinNota = new List<CargaCompraSinNota>();



            for (int i = numberOfItemsPrintedSoFar; i < listcargaCompraSinNota.Count; i++)
            {
                numberOfItemsPerPage++;

                if (numberOfItemsPerPage <= 2)
                {
                    numberOfItemsPrintedSoFar++;

                    if (numberOfItemsPrintedSoFar <= listcargaCompraSinNota.Count)
                    {

                        if (dictionary.ContainsKey("codigoProducto"))
                        {

                            point1 = dictionary["codigoProducto"];
                            e.Graphics.DrawString(listcargaCompraSinNota[i].codigoProducto, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }

                        if (dictionary.ContainsKey("nombreCombinacion"))
                        {
                            point1 = dictionary["nombreCombinacion"];
                            e.Graphics.DrawString(listcargaCompraSinNota[i].nombreCombinacion, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));

                        }
                        if (dictionary.ContainsKey("cantidad"))
                        {
                            point1 = dictionary["cantidad"];
                            e.Graphics.DrawString(darformato(listcargaCompraSinNota[i].cantidad), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }
                        if (dictionary.ContainsKey("cantidadcantidadUnitaria"))
                        {
                            point1 = dictionary["cantidadcantidadUnitaria"];
                            e.Graphics.DrawString(darformato(listcargaCompraSinNota[i].cantidadUnitaria), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }
                        if (dictionary.ContainsKey("cantidadRecibida"))
                        {
                            point1 = dictionary["cantidadRecibida"];
                            e.Graphics.DrawString(darformato(listcargaCompraSinNota[i].cantidadRecibida), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }
                        if (dictionary.ContainsKey("nombrePresentacion"))
                        {
                            point1 = dictionary["nombrePresentacion"];
                            e.Graphics.DrawString(listcargaCompraSinNota[i].nombrePresentacion, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
                        }
                        if (dictionary.ContainsKey("descripcion"))
                        {
                            point1 = dictionary["descripcion"];
                            e.Graphics.DrawString(listcargaCompraSinNota[i].descripcion, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));

                        }
                        if (dictionary.ContainsKey("nombreMarca"))
                        {
                            point1 = dictionary["nombreMarca"];
                            e.Graphics.DrawString(listcargaCompraSinNota[i].nombreMarca, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(point1.X, YI));
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

        private void dgvDetalleNota_KeyUp(object sender, KeyEventArgs e)
        {



        }

        private void cbxCodigoProducto_KeyUp(object sender, KeyEventArgs e)
        {



        }

        private void cbxAlmacenEntrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAlmacenEntrada.SelectedIndex == -1) return;
            if (filtradoAlmacen == 0)
                filtradoAlmacen = 1;
            int idAlmacen = (int)cbxAlmacenEntrada.SelectedValue;

            if (filtradoAlmacen == 1)
            {

                BindingList<Almacen> filtered = new BindingList<Almacen>(listAlmacenSalida.Where(obj => obj.idAlmacen != idAlmacen).ToList());
                cbxAlmacenSalida.DataSource = filtered;
                cbxAlmacenSalida.Update();
                filtradoAlmacen = 0;

            }
        }

        private void cbxAlmacenSalida_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxAlmacenSalida.SelectedIndex == -1) return;
            if (filtradoAlmacen == 0)
                filtradoAlmacen = 2;
            if (filtradoAlmacen == 2)
            {
                int idAlmacen = (int)cbxAlmacenSalida.SelectedValue;
                
               
                BindingList<Almacen> filtered = new BindingList<Almacen>(listAlmacenEntrada.Where(obj => obj.idAlmacen != idAlmacen).ToList());
                cbxAlmacenEntrada.DataSource = filtered;
                cbxAlmacenEntrada.Update();
                filtradoAlmacen = 0;
            }
        }
    }
}
