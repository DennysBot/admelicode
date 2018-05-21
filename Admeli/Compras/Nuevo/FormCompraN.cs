using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.Componentes;
using Admeli.Compras.Buscar;
using Admeli.Productos.Nuevo;
using Entidad;
using Entidad.Configuracion;
using Modelo;
using Newtonsoft.Json;

namespace Admeli.Compras.Nuevo
{
    public partial class FormCompraN : Form
    {
        PagoC pagoC;
        CompraC compraC;
        List<DetalleC> detalleC;
        PagocompraC pagocompraC;
        List<DatoNotaEntradaC> datoNotaEntradaC;
        NotaentradaC notaentrada;
        compraTotal compraTotal;

        // para modificar compra
        List<CompraModificar> list;
        List<CompraRecuperar> datosProveedor;

        // datos de proveedores
        List<Proveedor> ListProveedores = new List<Proveedor>();

        // objetos para cargar informacion necesaria
        private MonedaModel monedaModel = new MonedaModel();
        private TipoDocumentoModel tipoDocumentoModel = new TipoDocumentoModel();
        private ProductoModel productoModel = new ProductoModel();
        private AlternativaModel alternativaModel = new AlternativaModel();
        private PresentacionModel presentacionModel = new PresentacionModel();
        private FechaModel fechaModel = new FechaModel();
        private CompraModel compraModel = new CompraModel();
        private MedioPagoModel medioPagoModel = new MedioPagoModel();
        private AlmacenModel almacenModel = new AlmacenModel();
        private CompraModel compra = new CompraModel();
        private ProveedorModel proveedormodel = new ProveedorModel();
        private ImpuestoModel ImpuestoModel = new ImpuestoModel();

        private CajaModel cajaModel = new CajaModel();
        /// Sus datos se cargan al abrir el formulario
        private List<Moneda> monedas { get; set; }
        private List<TipoDocumento> tipoDocumentos { get; set; }
        private FechaSistema fechaSistema { get; set; }
        private List<Producto> productos { get; set; }
        private List<MedioPago> medioPagos { get; set; }

        /// Llenan los datos en las interacciones en el formulario 
      
        private List<Presentacion> presentaciones { get; set; }
        private List<Proveedor> proveedores { get; set; }
        private List<Compra> comprasAll { get; set; }

        /// Se preparan para realizar la compra de productos
        private Producto currentProducto { get; set; }
        private Proveedor currentProveedor { get; set; }
        private Compra currentCompra { get; set; } // notaEntrada,pago,pagoCompra
        private OrdenCompra currentOrdenCompra { get; set; }    
        private Presentacion currentPresentacion { get; set; }
        private NotaEntrada currentNotaEntrada { get; set; }
        private Pago currentPago { get; set; }
        private PagoCompra currentPagoCompra { get; set; }
        private AlmacenComra currentAlmacenCompra { get; set; }
        private DetalleC currentDetalleCompra { get; set; }
        private List<AlmacenComra> Almacen { get; set; }
        bool nuevo { get; set; }
        int nroDecimales = 2;
        string formato { get; set; }
       
        private double subTotal = 0;
        private double Descuento = 0;
        private double impuesto = 0;
        private double total = 0;


        #region ================================ Constructor================================
        public FormCompraN()
        {
            InitializeComponent();
            this.nuevo = true;
            cargarFechaSistema();
            pagoC = new PagoC();
            compraC = new CompraC();
            detalleC = new List<DetalleC>();
            pagocompraC = new PagocompraC();
            datoNotaEntradaC = new List<DatoNotaEntradaC>();
            notaentrada = new NotaentradaC();
            compraTotal = new compraTotal();
            formato = "{0:n" + nroDecimales + "}";

            cargarResultadosIniciales();

            ComprasAll();

        }
        private async void  ComprasAll()
        {
            comprasAll = await compraModel.comprasAll();
        }
        private void cargarResultadosIniciales()
        {


            lbSubtotal.Text = "s/" + ". " + darformato(0);
            lbDescuentoCompras.Text = "s/" + ". " + darformato(0);
            lbImpuesto.Text = "s/" + ". " + darformato(0);
            lbTotalCompra.Text = "s/" + ". " + darformato(0);

        }


        private string darformato(object dato)
        {
            return string.Format(CultureInfo.GetCultureInfo("en-US"), this.formato, dato);
        }

        private double toDouble(string texto)
        {
            return double.Parse(texto, CultureInfo.GetCultureInfo("en-US")); ;
        }

        public FormCompraN(Compra currentCompra)
        {
            InitializeComponent();
            this.currentCompra = currentCompra;
            this.nuevo = false;
            cargarFechaSistema();
            pagoC = new PagoC();
            compraC = new CompraC();
            detalleC = new List<DetalleC>();
            pagocompraC = new PagocompraC();
            datoNotaEntradaC = new List<DatoNotaEntradaC>();
            notaentrada = new NotaentradaC();
            compraTotal = new compraTotal();
            //datos del proveedor no editables
          
            btnImportarOrdenCompra.Visible = false;
            formato = "{0:n" + nroDecimales + "}";
            


        }


        #endregion ================================ Constructor================================
        #region ================================ Root Load ================================
        private void FormCompraNew_Load(object sender, EventArgs e)
        {
            if (nuevo == true)
                this.reLoad();
            else
            {
                this.reLoad();
                listarDetalleCompraByIdCompra();
                listarDatosProveedorCompra();


                btnComprar.Text = "Modificar compra";
            }

            AddButtonColumn();

        }

        private void reLoad()
        {
            cargarMonedas();
            cargarTipoDocumento();
            cargarFechaSistema();
            cargarProductos();
            cargarMedioPago();
            cargarAlmacen();
            cargarPresentacion();

            cargarProveedores();
            int i = ConfigModel.cajaSesion != null ? ConfigModel.cajaSesion.idCajaSesion : 0;
            if (i == 0)
            {

                chbxPagarCompra.Enabled = false;
                chbxPagarCompra.Checked = false;
            }
          
        }
        #endregion

        #region ============================== Load ==============================   


        private async void cargarDinero() {





        }
        private async void cargarProveedores()
        {


            try
            {
                proveedores = await proveedormodel.listaProveedores();
                proveedorBindingSource.DataSource = proveedores;
                if (!nuevo)
                {
                    currentProveedor = proveedores.Find(X => X.idProveedor == currentCompra.idProveedor);

                    cbxTipoDocumento.SelectedValue = currentCompra.idTipoDocumento;
                 
                    cbxProveedor.Text = currentProveedor.razonSocial;
                    cbxProveedor.Enabled = false;
                    txtDireccionProveedor.Text = currentProveedor.direccion;
                    txtRuc.Text = currentProveedor.ruc;
                    txtRuc.Enabled = false;
                    txtObservaciones.Text = currentCompra.observacion;


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar Proveedores2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
        private void AddButtonColumn()
        {
            DataGridViewButtonColumn buttons = new DataGridViewButtonColumn();
            {
                buttons.HeaderText = "Acciones";
                buttons.Text = "X";
                buttons.UseColumnTextForButtonValue = true;
                //buttons.AutoSizeMode =
                //   DataGridViewAutoSizeColumnMode.AllCells;
                buttons.FlatStyle = FlatStyle.Popup;
                buttons.CellTemplate.Style.BackColor = Color.Red;
                buttons.CellTemplate.Style.ForeColor = Color.White;

                buttons.Name = "acciones";

                //buttons.DisplayIndex = 0;
            }

            dgvDetalleCompra.Columns.Add(buttons);

        }
        private async void listarDetalleCompraByIdCompra()
        {

            try
            {
                list = await compra.dCompras(currentCompra.idCompra);
                // cargar datos correpondienetes
                if (detalleC == null) detalleC = new List<DetalleC>();
                foreach (CompraModificar C in list)
                {
                    DetalleC aux = new DetalleC();
                    aux.idCompra = C.idCompra;
                    aux.cantidad = C.cantidad;
                    aux.cantidadUnitaria = C.cantidadUnitaria;
                    aux.codigoProducto = C.codigoProducto;
                    aux.descripcion = C.descripcion;
                    aux.descuento = C.descuento;
                    aux.estado = C.estado;
                    aux.idCombinacionAlternativa = C.idCombinacionAlternativa;
                    aux.idDetalleCompra = C.idDetalleCompra;
                    aux.idPresentacion = C.idPresentacion;
                    aux.idProducto = C.idProducto;
                    aux.idSucursal = C.idSucursal;
                    aux.nombreCombinacion = C.nombreCombinacion;
                    aux.nombreMarca = C.nombreMarca;
                    aux.nombrePresentacion = C.nombrePresentacion;
                    aux.nro = C.nro;
                    aux.precioUnitario = C.precioUnitario;
                    aux.total = C.total;
                    detalleC.Add(aux);


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }

            // Refrescando la tabla
            detalleCBindingSource.DataSource = null;
            detalleCBindingSource.DataSource = detalleC;
            dgvDetalleCompra.Refresh();

            // Calculo de totales y subtotales
            calculoSubtotal();
            calcularDescuento();
        }

        private async void listarDatosProveedorCompra()
        {
            try
            {

                datosProveedor = await compraModel.Compras(currentCompra.idCompra);
                cbxProveedor.Text = datosProveedor[0].nombreProveedor;
                cbxProveedor.Enabled = false;
                txtDireccionProveedor.Text = datosProveedor[0].direccion;
                dtpFechaEmision.Value = datosProveedor[0].fechaFacturacion.date;
                dtpFechaPago.Value = datosProveedor[0].fechaPago.date;
                // textTotal.Text = Convert.ToString(datosProveedor[0].total);
                cbxTipoMoneda.Text = datosProveedor[0].moneda;
                txtTipoCambio.Text = "1";
                txtObservaciones.Text = currentCompra.observacion;
                txtNroOrdenCompra.Text = currentCompra.nroOrdenCompra;
                txtNroDocumento.Enabled = false; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }




        }
        private async void cargarMonedas()
        {
            try
            {
                monedas = await monedaModel.monedas();
                cbxTipoMoneda.DataSource = monedas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarTipoDocumento()
        {
            try
            {
                tipoDocumentos = await tipoDocumentoModel.tipoDocumentoVentas();
                cbxTipoDocumento.DataSource = tipoDocumentos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarFechaSistema()
        {
            try
            {
                if (!nuevo) return;
                fechaSistema = await fechaModel.fechaSistema();
                dtpFechaEmision.Value = fechaSistema.fecha;
                dtpFechaPago.Value = fechaSistema.fecha;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarProductos()
        {
            try
            {
                productos = await productoModel.productos();
                productoBindingSource.DataSource = productos;
                cbxCodigoProducto.SelectedIndex = -1;
                cbxDescripcion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // para el cbx de descripcion
        private async void cargarPresentacion()
        {                                                                                                    /// Cargar las precentaciones
            try
            {
                presentaciones = await presentacionModel.presentacionesTodas(ConfigModel.sucursal.idSucursal);
                presentacionBindingSource.DataSource = presentaciones;
                cbxDescripcion.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarMedioPago()
        {
            try
            {
                medioPagos = await medioPagoModel.medioPagos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarAlmacen()
        {

            Almacen = await almacenModel.almacenesCompra(ConfigModel.sucursal.idSucursal, PersonalModel.personal.idPersonal);
            currentAlmacenCompra = Almacen[0];
        }

        #endregion

        #region=========== METODOS DE APOYO EN EL CALCULO


        private async void calculoSubtotal()
        {
            if (cbxTipoMoneda.SelectedValue == null)
                return;
            double subTotalLocal = 0;
            foreach (DetalleC item in detalleC)
            {
                subTotalLocal += item.total;

            }
            Moneda moneda = monedas.Find(X => X.idMoneda == (int)cbxTipoMoneda.SelectedValue);
            this.subTotal = subTotalLocal;

            lbSubtotal.Text = moneda.simbolo + ". " + darformato(subTotalLocal);
            // determinar impuesto de cada producto
          

            double impuesto = 0;
            foreach (DetalleC item in detalleC)
            {
                List<Impuesto> list = await ImpuestoModel.impuestoProductoSucursal(item.idPresentacion, ConfigModel.sucursal.idSucursal);
               
                double porcentual = 0;
                double efectivo = 0;
                foreach (Impuesto I in list)
                {

                    if (item.estado == 1)
                    {

                        if (I.porcentual)
                            porcentual += I.valorImpuesto;
                        else
                        {
                            efectivo += I.valorImpuesto;
                        }

                    }

                }

                double i1 = item.cantidad * item.precioUnitario * porcentual / 100;

                i1 += efectivo;

                impuesto += i1;

            }


            this.impuesto = impuesto;

            lbImpuesto.Text = moneda.simbolo + ". " + darformato(impuesto);

            // determinar impuesto de cada producto
            this.total = impuesto + subTotalLocal;
            lbTotalCompra.Text = moneda.simbolo + ". " + darformato(total);
        }

        /// <summary>
        /// Calcular Total
        /// </summary>
        private void calcularTotal()
        {
            try
            {
                if (txtCantidad.Text.Trim() == "") txtTotalProducto.Text = "0";
                if (txtPrecioUnitario.Text.Trim() == "" || txtCantidad.Text.Trim() == "" || txtDescuento.Text.Trim() == "") return; /// Validación

                double precioUnidario = double.Parse(txtPrecioUnitario.Text, CultureInfo.GetCultureInfo("en-US"));
                double cantidad = double.Parse(txtCantidad.Text, CultureInfo.GetCultureInfo("en-US"));
                double descuento = double.Parse(txtDescuento.Text, CultureInfo.GetCultureInfo("en-US"));
                double total = (precioUnidario * cantidad) - (descuento / 100) * (precioUnidario * cantidad);

                txtTotalProducto.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), formato, total);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Calcular total", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Calcular Precio Unitario
        /// </summary>
        private void calcularPrecioUnitario(int tipo)
        {

            if (tipo == 0)
            {
                if (cbxCodigoProducto.SelectedIndex == -1) return; /// Validación
                try
                {
                    if (cbxDescripcion.SelectedIndex == -1)
                    {
                        txtPrecioUnitario.Text = currentProducto.precioCompra.ToString(ConfigModel.configuracionGeneral.formatoDecimales);
                    }
                    else
                    {
                        // Buscar presentacion elegida
                        int idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                        Presentacion findPresentacion = presentaciones.Find(x => x.idPresentacion == idPresentacion);

                        // Realizando el calculo

                        double precioCompra = (double)currentProducto.precioCompra;
                        double cantidadUnitario = (double)(findPresentacion.cantidadUnitaria);
                        double precioUnidatio = precioCompra * cantidadUnitario;

                        // Imprimiendo valor
                        txtPrecioUnitario.Text = String.Format(CultureInfo.GetCultureInfo("en-US"), formato, precioUnidatio);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "determinar precio unitario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                if (cbxDescripcion.SelectedIndex == -1) return; /// Validación
                try
                {

                    // Buscar presentacion elegida
                    int idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                    Presentacion findPresentacion = presentaciones.Find(x => x.idPresentacion == idPresentacion);
                    Producto findProducto = productos.Find(x => x.idProducto == findPresentacion.idProducto);
                    cbxCodigoProducto.Text = findProducto.codigoProducto;
                    // Realizando el calculo
                    double precioCompra = double.Parse(findProducto.precioCompra.ToString(ConfigModel.configuracionGeneral.formatoDecimales));
                    double cantidadUnitario = double.Parse(findPresentacion.cantidadUnitaria.ToString(ConfigModel.configuracionGeneral.formatoDecimales));
                    double precioUnidatio = precioCompra * cantidadUnitario;

                    // Imprimiendo valor
                    txtPrecioUnitario.Text = String.Format(CultureInfo.GetCultureInfo("en-US"), formato, precioUnidatio);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "determinar precio unitario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



            }

        }



        private void cargarProductoDetalle(int tipo)
        {
            if (tipo == 0)
            {

                if (cbxCodigoProducto.SelectedIndex == -1) return;
                try
                {
                    /// Buscando el producto seleccionado
                    int idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);
                    currentProducto = productos.Find(x => x.idProducto == idProducto);
                    cargarPresentaciones(idProducto, tipo);
                    cargarAlternativas(tipo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                if (cbxDescripcion.SelectedIndex == -1) return;
                try
                {
                    /// Buscando el producto seleccionado
                    int idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                    currentPresentacion = presentaciones.Find(x => x.idPresentacion == idPresentacion);

                    cargarPresentacionDescripcion(tipo);
                    cargarAlternativas(tipo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }


        }

        private void cargarPresentacionDescripcion(int tipo)
        {
            cbxDescripcion.Text = currentPresentacion.descripcion;
            txtCantidad.Text = "1";
            txtDescuento.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), formato, 0);
            calcularPrecioUnitario(tipo);
            calcularTotal();

        }
        private  void cargarPresentaciones(int idProducto, int tipo)
        {

            try
            {
                currentPresentacion = presentaciones.Find(X=>X.idProducto== idProducto);
                cbxDescripcion.SelectedValue = currentPresentacion.idPresentacion;
                txtCantidad.Text = "1";
                txtDescuento.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), formato, 0);
                calcularPrecioUnitario(tipo);
                calcularTotal();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar Presentacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
         
        }

        private async void cargarAlternativas(int tipo)
        {
            if (cbxCodigoProducto.SelectedIndex == -1) return; /// validacion
                                                               /// cargando las alternativas del producto
            List<AlternativaCombinacion> alternativaCombinacion = await alternativaModel.cAlternativa31(Convert.ToInt32(cbxCodigoProducto.SelectedValue));
            alternativaCombinacionBindingSource.DataSource = alternativaCombinacion;
            /// calculos
            calcularPrecioUnitario(tipo);
            calcularTotal();
        }


        private void calcularDescuento()
        {
            if (cbxTipoMoneda.SelectedValue == null)
                return;

            double descuentoTotal = 0;
            Moneda moneda = monedas.Find(X => X.idMoneda == (int)cbxTipoMoneda.SelectedValue);

            // calcular el descuento total
            foreach (DetalleC C in detalleC)
            {
                // calculamos el decuento para cada elemento
                double total = C.precioUnitario * C.cantidad;
                double descuentoC = total - C.total;
                descuentoTotal += descuentoC;
            }
            this.Descuento = descuentoTotal;

            lbDescuentoCompras.Text = moneda.simbolo + ". " + darformato(descuentoTotal);

        }


        private bool exitePresentacion(int idPresentacion)
        {
            foreach (DetalleC C in detalleC)
            {
                if (C.idPresentacion == idPresentacion)
                    return true;
            }

            return false;

        }
        private void decorationDataGridView()
        {
            if (dgvDetalleCompra.Rows.Count == 0) return;

            foreach (DataGridViewRow row in dgvDetalleCompra.Rows)
            {
                int idPresentacion = Convert.ToInt32(row.Cells[3].Value); // obteniedo el idCategoria del datagridview

                DetalleC aux = detalleC.Find(x => x.idPresentacion == idPresentacion); // Buscando la categoria en las lista de categorias
                if (aux.estado == 0 || aux.estado == 9)
                {
                    dgvDetalleCompra.ClearSelection();
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 224);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(250, 5, 73);
                }
            }
        }

        private void limpiarCamposProducto()
        {
            cbxCodigoProducto.SelectedIndex = -1;
            cbxDescripcion.SelectedIndex = -1;

            cbxVariacion.SelectedIndex = -1;
            txtCantidad.Text = "";
            txtDescuento.Text = "";
            txtPrecioUnitario.Text = "";
            txtTotalProducto.Text = "";
        }



        private DetalleC buscarElemento(int idPresentacion, int idCombinacion)
        {
            return detalleC.Find(x => x.idPresentacion == idPresentacion && x.idCombinacionAlternativa == idCombinacion);
        }


        #endregion

        // comenzando eventos

        #region ================================ eventos ================================
        private void cbxCodigoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarProductoDetalle(0);
        }

        private void cbxDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarProductoDetalle(1);
        }



        // validaciones
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private void txtPrecioUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isDecimal(e, txtPrecioUnitario.Text);
        }

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isDecimal(e, txtDescuento.Text);
        }

        private void txtTotalProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isDecimal(e, txtTotalProducto.Text);
        }



        // metodos usados por lo eventos
      

        private async void txtRUC_TextChanged(object sender, EventArgs e)
        {
            String aux = txtRuc.Text;

            int nroCarateres = aux.Length;
            bool exiteProveedor = false;
            if (nroCarateres == 11)
            {

                try
                {


                    Ruc nroDocumento = new Ruc();
                    nroDocumento.nroDocumento = aux;
                    List<Proveedor> proveedores = await proveedormodel.buscarPorDni(nroDocumento);
                    if (proveedores.Count > 0)
                    {
                        currentProveedor = proveedores[0];
                        if (currentProveedor != null)
                            exiteProveedor = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "consulta sunat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (exiteProveedor)
                {
                    // llenamos los dato con el current proveerdor

                    txtRuc.Text = currentProveedor.ruc;
                    txtDireccionProveedor.Text = currentProveedor.direccion;
                    cbxProveedor.Text = currentProveedor.razonSocial;

                }
                else
                {
                    //llenamos los datos en FormproveerdorNuevo
                    FormProveedorNuevo formProveedorNuevo = new FormProveedorNuevo(aux);
                    formProveedorNuevo.ShowDialog();
                    proveedores = await proveedormodel.listaProveedores();
                    proveedorBindingSource.DataSource = null;
                    proveedorBindingSource.DataSource = proveedores;
                    Response response = formProveedorNuevo.uCProveedorGeneral.response;
                    if (response != null)
                        if (response.id > 0)
                        {
                            currentProveedor = proveedores.Find(X => X.idProveedor == response.id);
                            txtDireccionProveedor.Text = currentProveedor.direccion;
                            cbxProveedor.Text = currentProveedor.razonSocial;
                       }
                }
            }

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            calcularTotal();
        }

        private void txtPrecioUnitario_TextChanged(object sender, EventArgs e)
        {
            calcularTotal();
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            calcularTotal();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            //validando campos
            if (txtPrecioUnitario.Text == "")
            {
                txtPrecioUnitario.Text = "0";
            }
            if (txtDescuento.Text == "")
            {

                txtDescuento.Text = "0";
            }
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
                if (detalleC == null) detalleC = new List<DetalleC>();
                DetalleC detalleCompra = new DetalleC();

                DetalleC find =buscarElemento(Convert.ToInt32(cbxDescripcion.SelectedValue), (int)cbxVariacion.SelectedValue);


                if (find!=null)
                {

                    MessageBox.Show("Este dato ya fue agregado", "presentacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }
                // Creando la lista
                detalleCompra.cantidad = Int32.Parse(txtCantidad.Text.Trim(), CultureInfo.GetCultureInfo("en-US"));
                /// Busqueda presentacion
                Presentacion findPresentacion = presentaciones.Find(x => x.idPresentacion == Convert.ToInt32(cbxDescripcion.SelectedValue));
                detalleCompra.cantidadUnitaria = toDouble( txtCantidad.Text);
                detalleCompra.codigoProducto = cbxCodigoProducto.Text.Trim();
                detalleCompra.descripcion = cbxDescripcion.Text.Trim();
                detalleCompra.descuento = Convert.ToDouble(txtDescuento.Text.Trim(), CultureInfo.GetCultureInfo("en-US"));
                detalleCompra.estado = 1;
                detalleCompra.idCombinacionAlternativa = Convert.ToInt32(cbxVariacion.SelectedValue);
                detalleCompra.idCompra = 0;
                detalleCompra.idDetalleCompra = 0;
                detalleCompra.idPresentacion = Convert.ToInt32(cbxDescripcion.SelectedValue);
                detalleCompra.idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);
                detalleCompra.idSucursal = ConfigModel.sucursal.idSucursal;
                detalleCompra.nombreCombinacion = cbxVariacion.Text;
                detalleCompra.nombreMarca = currentProducto.nombreMarca;
                detalleCompra.nombrePresentacion = cbxDescripcion.Text;
                detalleCompra.nro = 1;
                detalleCompra.precioUnitario = double.Parse(txtPrecioUnitario.Text.Trim(), CultureInfo.GetCultureInfo("en-US"));
                detalleCompra.total = double.Parse(txtTotalProducto.Text.Trim(), CultureInfo.GetCultureInfo("en-US"));
                // agrgando un nuevo item a la lista
                detalleC.Add(detalleCompra);
                // Refrescando la tabla
                detalleCBindingSource.DataSource = null;
                detalleCBindingSource.DataSource = detalleC;
                dgvDetalleCompra.Refresh();
                // Calculo de totales y subtotales e impuestos
                calculoSubtotal();
                calcularDescuento();
                limpiarCamposProducto();

            }
            else
            {

                MessageBox.Show("Error: elemento no seleccionado", "agregar Elemento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }


       
        private void dgvDetalleCompra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int y = e.ColumnIndex;

            if (dgvDetalleCompra.Columns[y].Name == "acciones")
            {
                if (dgvDetalleCompra.Rows.Count == 0)
                {
                    MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (nuevo)
                {
                    int index = dgvDetalleCompra.CurrentRow.Index; // Identificando la fila actual del datagridview
                    int idPresentacion = Convert.ToInt32(dgvDetalleCompra.Rows[index].Cells[3].Value);
                    int idCombinacion = Convert.ToInt32(dgvDetalleCompra.Rows[index].Cells[1].Value);

                    // obteniedo el idRegistro del datagridview
                    DetalleC aux =buscarElemento(idPresentacion, idCombinacion);

                    dgvDetalleCompra.Rows.RemoveAt(index);

                    detalleC.Remove(aux);

                    calculoSubtotal();
                    calcularDescuento();
                }
                else
                {
                    int index = dgvDetalleCompra.CurrentRow.Index;
                    int idPresentacion = Convert.ToInt32(dgvDetalleCompra.Rows[index].Cells[3].Value);
                    int idCombinacion = Convert.ToInt32(dgvDetalleCompra.Rows[index].Cells[1].Value);
                    // obteniedo el idRegistro del datagridview
                    DetalleC aux = buscarElemento(idPresentacion, idCombinacion);
                    aux.estado = 9;

                    dgvDetalleCompra.ClearSelection();
                    dgvDetalleCompra.Rows[index].DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 224);
                    dgvDetalleCompra.Rows[index].DefaultCellStyle.ForeColor = Color.FromArgb(250, 5, 73);

                    decorationDataGridView();
                    calculoSubtotal();
                    calcularDescuento();


                }

                btnAgregar.Enabled = true;
                btnModificar.Enabled = false;
               

                cbxCodigoProducto.Enabled = true;
                cbxDescripcion.Enabled = true;

                limpiarCamposProducto();
            }
        }

        private  void dgvDetalleCompra_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificando la existencia de datos en el datagridview
            if (dgvDetalleCompra.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dgvDetalleCompra.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleCompra.Rows[index].Cells[3].Value); // obteniedo el idRegistro del datagridview
            int idCombinacion = Convert.ToInt32(dgvDetalleCompra.Rows[index].Cells[1].Value); // obteniedo el idRegistro del datagridview
            currentDetalleCompra = buscarElemento(  idPresentacion, idCombinacion); // Buscando la registro especifico en la lista de registros
            cbxCodigoProducto.Text = currentDetalleCompra.codigoProducto;
            cbxDescripcion.Text = currentDetalleCompra.descripcion;
            cbxVariacion.Text = currentDetalleCompra.nombreCombinacion;
            cbxDescripcion.Text = currentDetalleCompra.nombrePresentacion;
            txtCantidad.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), formato, currentDetalleCompra.cantidad);
            txtPrecioUnitario.Text = String.Format(CultureInfo.GetCultureInfo("en-US"), formato, currentDetalleCompra.precioUnitario);
            txtDescuento.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), formato, currentDetalleCompra.descuento);
            txtTotalProducto.Text = string.Format(CultureInfo.GetCultureInfo("en-US"), formato, currentDetalleCompra.total);
            btnModificar.Enabled = true;
            btnAgregar.Enabled = false;
            cbxCodigoProducto.Enabled = false;
            cbxDescripcion.Enabled = false;

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            currentDetalleCompra.cantidad = toDouble(txtCantidad.Text);
            currentDetalleCompra.cantidadUnitaria = toDouble(txtCantidad.Text);
            currentDetalleCompra.precioUnitario = Convert.ToDouble(txtPrecioUnitario.Text, CultureInfo.GetCultureInfo("en-US"));
            currentDetalleCompra.total = double.Parse(txtTotalProducto.Text, CultureInfo.GetCultureInfo("en-US"));
            detalleCompraBindingSource.DataSource = null;
            detalleCompraBindingSource.DataSource = detalleC;
            dgvDetalleCompra.Refresh();
            // Calculo de totales y subtotales
            calculoSubtotal();
            calcularDescuento();
            btnModificar.Enabled = false;
            cbxCodigoProducto.Enabled = true;
            cbxDescripcion.Enabled = true;
            btnAgregar.Enabled = true;
            limpiarCamposProducto();
        }


      

        private async void btnComprar_Click(object sender, EventArgs e)
        {

            if(currentProveedor==null)
            {


                MessageBox.Show("no hay ningun proveedor seleccionado", "proveedor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }


            //pago
            pagoC.estado = 1;// activo
            pagoC.estadoPago = 1;//ver que significado
            Moneda moneda = monedas.Find(X => X.idMoneda == (int)cbxTipoMoneda.SelectedValue);
            pagoC.idMoneda = moneda.idMoneda;
            pagoC.idPago = currentCompra != null ? currentCompra.idPago : 0;
            pagoC.motivo = "COMPRA";
            pagoC.saldo = chbxPagarCompra.Checked ? 0 : this.total;//  
            pagoC.valorPagado = chbxPagarCompra.Checked ? this.total : 0;//           
            pagoC.valorTotal = this.total;//        
            // compra
            string date1 = String.Format("{0:u}", dtpFechaEmision.Value);
            date1 = date1.Substring(0, date1.Length - 1);
            string date = String.Format("{0:u}", dtpFechaPago.Value);
            date = date.Substring(0, date.Length - 1);
            compraC.idCompra = currentCompra != null ? currentCompra.idCompra : 0; ;
            compraC.numeroDocumento = "0";//lo textNordocumento
            compraC.rucDni =  currentProveedor.ruc;
            compraC.direccion = currentProveedor.direccion;
            
            compraC.formaPago = "EFECTIVO";
            compraC.fechaPago = date;
            compraC.fechaFacturacion = date1;
            compraC.descuento = lbDescuentoCompras.Text.Trim() != "" ? this.Descuento: 0;//
            compraC.tipoCompra = "Con productos";        
            compraC.subTotal = lbSubtotal.Text.Trim() != "" ? this.subTotal : 0;           
            compraC.total = this.total;
            compraC.observacion = txtObservaciones.Text;
            compraC.estado = 1;
            compraC.idProveedor =  currentProveedor.idProveedor ;
            compraC.nombreProveedor =  currentProveedor.razonSocial ;
            compraC.idPago = currentCompra != null ? currentCompra.idPago : 0; ;
            compraC.idPersonal = PersonalModel.personal.idPersonal;
            compraC.tipoCambio = 1;
            int j = cbxTipoDocumento.SelectedIndex;
            TipoDocumento aux = cbxTipoDocumento.Items[j] as TipoDocumento;
            compraC.idTipoDocumento = (aux.idTipoDocumento);
            compraC.idSucursal = ConfigModel.sucursal.idSucursal;
            compraC.nombreLabel = aux.nombreLabel;
            compraC.vendedor = PersonalModel.personal.nombres;
            compraC.nroOrdenCompra = txtNroOrdenCompra.Text.Trim();           
            compraC.moneda = moneda.moneda;
            compraC.idCompra = currentCompra != null ? currentCompra.idCompra : 0;           
            //detalle
            foreach (DetalleC detalle in detalleC)
            {
                
                DatoNotaEntradaC notaEntrada = new DatoNotaEntradaC();
                notaEntrada.idProducto = detalle.idProducto;
                notaEntrada.cantidad = detalle.cantidad;
                notaEntrada.idCombinacionAlternativa = detalle.idCombinacionAlternativa;
                notaEntrada.idAlmacen = currentAlmacenCompra.idAlmacen;
                notaEntrada.descripcion = detalle.descripcion;
                datoNotaEntradaC.Add(notaEntrada);
            }
            pagocompraC.idCaja = FormPrincipal.asignacion.idCaja;
            pagocompraC.idPago = currentCompra != null ? currentCompra.idPago : 0; ;
            pagocompraC.moneda = moneda.moneda;
            pagocompraC.idMoneda = moneda.idMoneda;
            pagocompraC.idMedioPago = medioPagos[0].idMedioPago;
            pagocompraC.idCajaSesion = ConfigModel.cajaSesion != null ? ConfigModel.cajaSesion.idCajaSesion : 0;
            pagocompraC.pagarCompra = chbxPagarCompra.Checked == true ? 1 : 0;

            notaentrada.datoNotaEntrada = datoNotaEntradaC;
            notaentrada.generarNotaEntrada = chbxNotaEntrada.Checked == true ? 1 : 0;
            notaentrada.idCompra = currentCompra != null ? currentCompra.idPago : 0; ;
            notaentrada.idTipoDocumento =(int) cbxTipoDocumento.SelectedValue;
            notaentrada.idPersonal = PersonalModel.personal.idPersonal;
            compraTotal = new compraTotal();
            compraTotal.detalle = detalleC;
            compraTotal.compra = compraC;
            compraTotal.notaentrada = notaentrada;
            compraTotal.pago = pagoC;
            compraTotal.pagocompra = pagocompraC;

            try
            {

                 await compraModel.ralizarCompra(compraTotal);


            }
            catch ( Exception ex)
            {
                MessageBox.Show("error:  " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }


            if (nuevo)
            {
                MessageBox.Show("Datos Guardados", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnComprar.Enabled = false;

            }
            else
            MessageBox.Show("Datos  modificador", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;



        }

       

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            BuscarProveedor buscarProveedor = new BuscarProveedor();
            buscarProveedor.ShowDialog();         
            currentProveedor = buscarProveedor.currentProveedor;
            if (currentProveedor != null)
            { 
                //cargando datas del proveedor
                txtRuc.Text = currentProveedor.ruc;
                cbxProveedor.SelectedValue = currentProveedor.idProveedor;
                txtDireccionProveedor.Text = currentProveedor.direccion;
            }                    
        }

        private void cbxProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentProveedor = proveedores.Find(X => X.idProveedor == (int)cbxProveedor.SelectedValue);
            cbxProveedor.SelectedValue = currentProveedor.idProveedor;
            txtRuc.Text = currentProveedor.ruc;           
            txtDireccionProveedor.Text = currentProveedor.direccion;

        }

       

        private void btnImportarOrdenCompra_Click(object sender, EventArgs e)
        {

            try
            {

                buscarOrden buscarOrden = new buscarOrden();
                buscarOrden.ShowDialog();
                OrdenCompraSinComprar aux = buscarOrden.currentOrdenCompra;
                // datos del proveedor
                if (aux != null)
                {
                    txtNroOrdenCompra.Text = aux.serie + " - " + aux.correlativo;
                    currentProveedor = proveedores.Find(X => X.ruc == aux.rucDni);               
                    txtDireccionProveedor.Text = currentProveedor.direccion;
                    cbxProveedor.SelectedValue = currentProveedor.idProveedor;              
                    currentCompra = comprasAll.Find(X=>X.idCompra==aux.idCompra);
                    txtObservaciones.Text = currentCompra.observacion;
                    detalleCompraBindingSource.DataSource = null;
                    dgvDetalleCompra.Refresh();
                    if (detalleC != null)
                        detalleC.Clear();// limpiamos la lista de detalle productos
                    detalleC = new List<DetalleC>();

                   
                  
                    this.reLoad();
                    listarDetalleCompraByIdCompra();
                    listarDatosProveedorCompra();
                    // Calculo de totales y subtotales
                    calculoSubtotal();
                    calcularDescuento();                
                }
            }
            catch(Exception ex)
            {

                MessageBox.Show("Error: "+ ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }



        }

       

        

        private  async void btnComprar_Click_2(object sender, EventArgs e)
        {
            if (currentProveedor == null)
            {


                MessageBox.Show("no hay ningun proveedor seleccionado", "proveedor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }
            int i = ConfigModel.cajaSesion != null ? ConfigModel.cajaSesion.idCajaSesion : 0;
            if (i > 0)
            {

                int idCompra = currentCompra != null ? currentCompra.idCompra : 0; 
                List<DineroCompra>  list=        await  cajaModel.totalCajaCompra(medioPagos[0].idMedioPago, ConfigModel.cajaSesion.idCajaSesion, idCompra);// unico medio de pago
                DineroCompra dineroCompra = list.Find(X => X.idMoneda == (int)cbxTipoMoneda.SelectedValue);

                if ((dineroCompra.total - total) < 0)
                {
                    MessageBox.Show("no exite sufciente dinero ", "caja ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }

            }

            if (currentAlmacenCompra == null)
            {

                MessageBox.Show("Almacen no asignado ", "Almacen ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;

            } 
          




            //pago
            pagoC.estado = 1;// activo
            pagoC.estadoPago = 1;//ver que significado
            Moneda moneda = monedas.Find(X => X.idMoneda == (int)cbxTipoMoneda.SelectedValue);
            pagoC.idMoneda = moneda.idMoneda;
            pagoC.idPago = currentCompra != null ? currentCompra.idPago : 0;
            pagoC.motivo = "COMPRA";
            pagoC.saldo = chbxPagarCompra.Checked ? 0 : this.total;//  
            pagoC.valorPagado = chbxPagarCompra.Checked ? this.total : 0;//           
            pagoC.valorTotal = this.total;//        
            // compra
            string date1 = String.Format("{0:u}", dtpFechaEmision.Value);
            date1 = date1.Substring(0, date1.Length - 1);
            string date = String.Format("{0:u}", dtpFechaPago.Value);
            date = date.Substring(0, date.Length - 1);
            compraC.idCompra = currentCompra != null ? currentCompra.idCompra : 0; ;
            compraC.numeroDocumento = "0";//lo textNordocumento
            compraC.rucDni = currentProveedor.ruc;
            compraC.direccion = currentProveedor.direccion;

            compraC.formaPago = "EFECTIVO";
            compraC.fechaPago = date;
            compraC.fechaFacturacion = date1;
            compraC.descuento = lbDescuentoCompras.Text.Trim() != "" ? this.Descuento : 0;//
            compraC.tipoCompra = "Con productos";
            compraC.subTotal = lbSubtotal.Text.Trim() != "" ? this.subTotal : 0;
            compraC.total = this.total;
            compraC.observacion = txtObservaciones.Text;
            compraC.estado = 1;
            compraC.idProveedor = currentProveedor.idProveedor;
            compraC.nombreProveedor = currentProveedor.razonSocial;
            compraC.idPago = currentCompra != null ? currentCompra.idPago : 0; ;
            compraC.idPersonal = PersonalModel.personal.idPersonal;
            compraC.tipoCambio = 1;
            int j = cbxTipoDocumento.SelectedIndex;
            TipoDocumento aux = cbxTipoDocumento.Items[j] as TipoDocumento;
            compraC.idTipoDocumento = (aux.idTipoDocumento);
            compraC.idSucursal = ConfigModel.sucursal.idSucursal;
            compraC.nombreLabel = aux.nombreLabel;
            compraC.vendedor = PersonalModel.personal.nombres;
            compraC.nroOrdenCompra = txtNroOrdenCompra.Text.Trim();
            compraC.moneda = moneda.moneda;
            compraC.idCompra = currentCompra != null ? currentCompra.idCompra : 0;
            //detalle
            foreach (DetalleC detalle in detalleC)
            {

                DatoNotaEntradaC notaEntrada = new DatoNotaEntradaC();
                notaEntrada.idProducto = detalle.idProducto;
                notaEntrada.cantidad = detalle.cantidad;
                notaEntrada.idCombinacionAlternativa = detalle.idCombinacionAlternativa;
                notaEntrada.idAlmacen = currentAlmacenCompra.idAlmacen;
                notaEntrada.descripcion = detalle.nombrePresentacion;
                datoNotaEntradaC.Add(notaEntrada);
            }
            pagocompraC.idCaja = FormPrincipal.asignacion.idCaja;
            pagocompraC.idPago = currentCompra != null ? currentCompra.idPago : 0; ;
            pagocompraC.moneda = moneda.moneda;
            pagocompraC.idMoneda = moneda.idMoneda;
            pagocompraC.idMedioPago = medioPagos[0].idMedioPago;
            pagocompraC.idCajaSesion = ConfigModel.cajaSesion != null ? ConfigModel.cajaSesion.idCajaSesion : 0;
            pagocompraC.pagarCompra = chbxPagarCompra.Checked == true ? 1 : 0;

            notaentrada.datoNotaEntrada = datoNotaEntradaC;
            notaentrada.generarNotaEntrada = chbxNotaEntrada.Checked == true ? 1 : 0;
            notaentrada.idCompra = currentCompra != null ? currentCompra.idPago : 0; ;
            notaentrada.idTipoDocumento = 7;// nota de entrada
            notaentrada.idPersonal = PersonalModel.personal.idPersonal;
            compraTotal = new compraTotal();
            compraTotal.detalle = detalleC;
            compraTotal.compra = compraC;
            compraTotal.notaentrada = notaentrada;
            compraTotal.pago = pagoC;
            compraTotal.pagocompra = pagocompraC;

            try
            {

                await compraModel.ralizarCompra(compraTotal);


            }
            catch (Exception ex)
            {
                MessageBox.Show("error:  " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }


            if (nuevo)
            {
                MessageBox.Show("Datos Guardados", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnComprar.Enabled = false;

            }
            else
                MessageBox.Show("Datos  modificador", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;

        }
        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {

            try
            {
                FormProductoNuevo formProductoNuevo = new FormProductoNuevo();
                formProductoNuevo.ShowDialog();
                cargarProductos();
                cargarPresentacion();
                btnModificar.Enabled = false;
                cbxCodigoProducto.Enabled = true;
                cbxDescripcion.Enabled = true;
                btnAgregar.Enabled = true;
                limpiarCamposProducto();
            }


            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Nuevo Producto ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        #endregion ================================ eventos ================================

        private void btnActulizar_Click(object sender, EventArgs e)
        {
            cargarProductos();
            cargarPresentacion();
            btnModificar.Enabled = false;
            cbxCodigoProducto.Enabled = true;
            cbxDescripcion.Enabled = true;
            btnAgregar.Enabled = true;
            limpiarCamposProducto();
        }
    }
}
