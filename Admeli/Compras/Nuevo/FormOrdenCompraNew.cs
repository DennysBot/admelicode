using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Admeli.Componentes;
using Admeli.Compras.buscar;
using Admeli.Compras.Buscar;
using Admeli.Productos.buscar;
using Admeli.Productos.Nuevo;
using Entidad;
using Entidad.Configuracion;
using Entidad.Location;
using Modelo;


namespace Admeli.Compras.Nuevo
{
    public partial class FormOrdenCompraNew : Form
    {


        //variables para realizar  un orden de compra ordenCompra

        private CompraOrden compraA { get; set; }
        private PagoOrden pagoA { get; set; }
        private OrdenCompraOrden ordenCompraA { get; set; }
        private List<DetalleOrden> detalleA { get; set; }
        private OrdenCompraTotal compraTotal { get; set; }


        //webservice utilizados
        private MonedaModel monedaModel = new MonedaModel();
        private TipoDocumentoModel tipoDocumentoModel = new TipoDocumentoModel();
        private DocCorrelativoModel docCorrelativoModel = new DocCorrelativoModel();
        private ProductoModel productoModel = new ProductoModel();
        private AlternativaModel alternativaModel = new AlternativaModel();
        private PresentacionModel presentacionModel = new PresentacionModel();
        private FechaModel fechaModel = new FechaModel();
        private CompraModel compraModel = new CompraModel();
        private OrdenCompraModel ordenCompraModel = new OrdenCompraModel();
        private MedioPagoModel medioPagoModel = new MedioPagoModel();
        private ImpuestoModel impuestoModel = new ImpuestoModel();
        private LocationModel locationModel = new LocationModel();
        private ProveedorModel proveedorModel = new ProveedorModel();







        /// Sus datos se cargan al abrir el formulario
        private List<Moneda> monedas { get; set; }
        private List<TipoDocumento> tipoDocumentos { get; set; }
        private FechaSistema fechaSistema { get; set; }
        private List<Producto> productos { get; set; }
        private List<MedioPago> medioPagos { get; set; }
        private List<OrdenCompraImpuesto> ordenCompraImpuestos { get; set; }
        private List<OrdenCompraModificar> ordenCompraModificar { get; set; }
        private List<Proveedor> proveedores { get; set; }


        public UbicacionGeografica CurrentUbicacionGeografica;

        private List<DetalleOrden> detalleModificar { get; set; }

        /// Llenan los datos en las interacciones en el formulario 
        private List<Presentacion> presentaciones { get; set; }
        private Producto currentProducto { get; set; }
        private Proveedor currentProveedor { get; set; }
        private Presentacion currentPresentacion { get; set; }
        private OrdenCompra currentOrdenCompra { get; set; }

        private DetalleOrden currentDetalleOrden { get; set; }
        private int currentIdOrden { get; set; }
        private Sucursal_correlativo sucursal_correlativo = new Sucursal_correlativo();
        /// Se preparan para realizar la compra de productos
       
        // notaEntrada,pago,pagoCompra
        private NotaEntrada currentNotaEntrada { get; set; }
        private Pago currentPago { get; set; }
        public PagoCompra currentPagoCompra { get; set; }
        public Sucursal idSucursal { get; set; }
        public Personal personal;
        public int nroNuevo = 0;
       
        private bool nuevo { get; set; }

        private int idPresentacionDatagriview = 0;    
        int nroDecimales = 2;
        string formato { get; set; }
        private int nroCaracteres = 0;
        private double subTotal = 0;
        private double Descuento = 0;
        private double impuesto = 0;
        private double total = 0;
        private bool lisenerKeyEvents =true;
        public FormOrdenCompraNew()
        {
            InitializeComponent();


       
           
            this.nuevo = true;
            cargarFechaSistema();
            compraA = new CompraOrden();
            pagoA = new PagoOrden();
            ordenCompraA = new OrdenCompraOrden();
            detalleA = new List<DetalleOrden>();
            compraTotal = new OrdenCompraTotal();          
            formato = "{0:n" + nroDecimales + "}";
            
            currentIdOrden = 0;
            cargarResultadosIniciales();

        }

        private void cargarResultadosIniciales()
        {


            lbSubtotal.Text = "s/" + ". " + darformato(0);
            lbDescuentoCompras.Text = "s/" + ". " + darformato(0);
            lbImpuesto.Text = "s/" + ". " + darformato(0);
            lbTotal.Text = "s/" + ". " + darformato(0);

        }
        private string darformato(object dato)
        {
            return string.Format(CultureInfo.GetCultureInfo("en-US"), this.formato, dato);
        }

        private double toDouble(string texto)
        {
            return double.Parse(texto, CultureInfo.GetCultureInfo("en-US")); ;
        }
        public FormOrdenCompraNew(OrdenCompra currentOrdenCompra)
        {
            InitializeComponent();     
            this.currentOrdenCompra = currentOrdenCompra;
            this.currentIdOrden = currentOrdenCompra.idOrdenCompra;
            this.nuevo = false;
            compraA = new CompraOrden();
            pagoA = new PagoOrden();
            ordenCompraA = new OrdenCompraOrden();
            detalleA = new List<DetalleOrden>();
            compraTotal = new OrdenCompraTotal();
            

            if (currentOrdenCompra.estadoCompra != 8)
            {

                btnComprarOrdenCompra.Enabled = false;
            }
                      
            formato = "{0:n" + nroDecimales + "}";
            detalleModificar = new List<DetalleOrden>();

        }
        #region ================================ Root Load ================================
        private void FormCompraNew_Load(object sender, EventArgs e)
        {


            if (nuevo == true)
            {
                this.reLoad();
                cargarCorrelactivo();
                cargarubigeoActual(ConfigModel.sucursal.idUbicacionGeografica);
            }
            else
            {
                this.reLoad();
                this.cargarOrden();
                cargarImpuesto();
                cargarubigeoActual(ConfigModel.sucursal.idUbicacionGeografica);
                cargarProductos();
                            
            }

            AddButtonColumn();

            btnModificar.Enabled = false;

            this.ParentChanged += ParentChange; // Evetno que se dispara cuando el padre cambia // Este eveto se usa para desactivar lisener key events de este modulo
            if (TopLevelControl is Form) // Escuchando los eventos del formulario padre
            {
                (TopLevelControl as Form).KeyPreview = true;
                TopLevelControl.KeyUp += TopLevelControl_KeyUp;
            }
        }
        private void reLoad()
        {

            cargarProveedores(nuevo);
            cargarMonedas();        
            cargarFechaSistema();
            cargarProductos();                   
            cargarPresentacion();
           
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
                    txtRuc.Focus();
                    break;
                case Keys.F4:
                    btnBuscarLugar.Focus();
                    break;
                case Keys.F5:
                    ComprarOrden();
                    break;
                case Keys.F7:
                    buscarProducto();
                    break;
                default:
                    if (e.Control && e.KeyValue == 13)
                    {
                        hacerOrdenCompra();
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




        #region ============================== Load ==============================




        private async void cargarProveedores(bool nuevo)
        {

            loadState(true);
            try
            {   proveedores = await proveedorModel.listaProveedores();
                proveedorBindingSource.DataSource = proveedores;
                cbxProveedor.SelectedIndex = -1;
                if (!nuevo)
                {
                    currentProveedor = proveedores.Find(X => X.idProveedor == currentOrdenCompra.idProveedor);

                    txtComprobante.Text = "ORDEN DE COMPRA";
                    txtSerie.Text = currentOrdenCompra.serie;
                    txtCorrelativo.Text = currentOrdenCompra.correlativo;
                    cbxProveedor.Text = currentProveedor.razonSocial;
                    cbxProveedor.Enabled = false;
                    txtDireccionProveedor.Text = currentProveedor.direccion;                 
                    txtRuc.Text = currentProveedor.ruc;
                    txtRuc.Enabled = false;
                    txtObservaciones.Text = currentOrdenCompra.observacion;

                }
              

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadState(false);

            }
            
           

        }
        private async void cargarOrden()
        {
            try
            {
                ordenCompraModificar = await ordenCompraModel.dcomprasordencompra(currentIdOrden);
               
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar Orden", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (nuevo == false)
            {
                cargardatagriw();
            }


        }

        private void cargardatagriw()
        {
            try
            {
                DetalleOrden detalleCompra;
                if (detalleA == null) detalleA = new List<DetalleOrden>();
                foreach (OrdenCompraModificar o in ordenCompraModificar)
                {

                    detalleCompra = new DetalleOrden();

                    detalleCompra.cantidad = o.cantidad;
                    detalleCompra.cantidadUnitaria = o.cantidadUnitaria;
                    detalleCompra.codigoProducto = o.codigoProducto;
                    detalleCompra.descripcion = o.descripcion;
                    detalleCompra.descuento = o.descuento;
                    detalleCompra.estado = o.estado;
                    detalleCompra.idCombinacionAlternativa = o.idCombinacionAlternativa;
                    detalleCompra.idCompra = o.idCompra;
                    detalleCompra.idDetalleCompra = o.idDetalleCompra;
                    detalleCompra.idPresentacion = o.idPresentacion;
                    detalleCompra.idProducto = o.idProducto;
                    detalleCompra.idSucursal = o.idSucursal;
                    detalleCompra.nombreCombinacion = o.nombreCombinacion;
                    detalleCompra.nombreMarca = o.nombreMarca;
                    detalleCompra.nombrePresentacion = o.nombrePresentacion;
                    detalleCompra.nro = o.nro;
                    detalleCompra.precioUnitario = o.precioUnitario;
                    detalleCompra.total = o.total;

                    // agrgando un nuevo item a la lista
                    detalleA.Add(detalleCompra);

                    // Refrescando la tabla

                }

            }

            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar datagriw", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }




            detalleModificar.AddRange(detalleA);
            detalleOrdenBindingSource.DataSource = null;
            detalleOrdenBindingSource.DataSource = detalleA;
            //dataGridView.Refresh();
            // Calculo de totales y subtotales
            calculoSubtotal();
            calcularDescuento();

            decorationDataGridView();

        }


        private async void cargarImpuesto()
        {
            try
            {

                ordenCompraImpuestos = await impuestoModel.impcompraproductoordencompra(currentOrdenCompra.idOrdenCompra, ConfigModel.sucursal.idSucursal);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar Impuesto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private async void cargarubigeoActual(int idUbicacionGeografica)
        {

            try
            {
                CurrentUbicacionGeografica = await locationModel.ubigeoActual(idUbicacionGeografica);

                txtLugarEntrega.Text = CurrentUbicacionGeografica.nombreP + " - " + CurrentUbicacionGeografica.nombreN1;
                if (CurrentUbicacionGeografica.nombreN2 != "")
                {
                    txtLugarEntrega.Text += " - " + CurrentUbicacionGeografica.nombreN2;
                    if (CurrentUbicacionGeografica.nombreN3 != "")
                    {
                        txtLugarEntrega.Text += " - " + CurrentUbicacionGeografica.nombreN3;

                    }
                }

            }
           
             catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar ubigeo actual", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private async void cargarCorrelactivo()
        {

            try
            {
                List<Sucursal_correlativo> list = await docCorrelativoModel.listarNroDocumentoSucursal(1, ConfigModel.sucursal.idSucursal);
                sucursal_correlativo = list[0];
                txtComprobante.Text = "ORDEN DE COMPRA";
                txtSerie.Text = sucursal_correlativo.serie;
                txtCorrelativo.Text = sucursal_correlativo.correlativoActual;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar Correlactivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }



        private void AddButtonColumn()
        {

            try
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
                }

                dgvDetalleOrdenCompra.Columns.Add(buttons);



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "add btn eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Error: " + ex.Message, "cargar monedas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

      

        private async void cargarFechaSistema()
        {
            try
            {
                if (!nuevo)
                {
                    dtpFechaEntrega.Value = currentOrdenCompra.fecha.date;
                    txtDireccionEntrega.Text = currentOrdenCompra.direccion;
                }
                else
                {

                    fechaSistema = await fechaModel.fechaSistema();
                    dtpFechaEntrega.Value = fechaSistema.fecha;

                }
               
                //dtpFechaPago.Value = fechaSistema.fecha;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar fecha del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Error: " + ex.Message, "cargar Productos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Error: " + ex.Message, "cargar Presentacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        #endregion

        #region=========== METODOS DE APOYO EN EL CALCULO

        public void buscarProducto()
        {
            loadState(true);
            try
            {
                FormBuscarProducto formBuscarProducto = new FormBuscarProducto(productos);
                formBuscarProducto.ShowDialog();

                if (formBuscarProducto.currentProducto != null)
                {


                    currentProducto = formBuscarProducto.currentProducto;


                    cbxCodigoProducto.SelectedValue = formBuscarProducto.currentProducto.idProducto;

                }





            }
            catch (Exception ex)

            {
                MessageBox.Show("Error: " + ex.Message, "Productos ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {

                loadState(false);
            }

        }
        private async void calculoSubtotal()
        {
            try
            {

            if (cbxTipoMoneda.SelectedValue == null)
                return;          
            double subTotalLocal = 0;
            foreach (DetalleOrden item in detalleA)
            {
                if(item.estado==1)
                subTotalLocal += item.total;

            }


            Moneda moneda = monedas.Find(X => X.idMoneda == (int)cbxTipoMoneda.SelectedValue);
            this.subTotal = subTotalLocal;

            lbSubtotal.Text = moneda.simbolo + ". " + darformato(subTotalLocal);
            // determinar impuesto de cada producto
            double impuestoTotal = 0;

            
            double impuesto = 0;
            foreach (DetalleOrden item in detalleA)
            {
                List<Impuesto> list = await impuestoModel.impuestoProductoSucursal(item.idPresentacion, ConfigModel.sucursal.idSucursal);
                double impuestolocal = 0;
                double porcentual = 0;
                double efectivo = 0;
                foreach (Impuesto I in list)
                {

                    if (item.estado == 1)
                    {

                        if(I.porcentual)
                            porcentual += I.valorImpuesto;
                        else
                        {
                            efectivo += I.valorImpuesto;
                        }

                    }
                  
                }
                
            double i1 = item.cantidad*item.precioUnitario * porcentual / 100;
           
            i1 += efectivo;

                impuesto += i1;

            }
           

            this.impuesto = impuesto;
            
            lbImpuesto.Text = moneda.simbolo + ". " + darformato(impuesto);

            // determinar impuesto de cada producto
            this.total = impuesto + subTotalLocal;
            lbTotal.Text = moneda.simbolo + ". " + darformato(total);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "calcular subTotal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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

           
                if (cbxCodigoProducto.SelectedIndex == -1) return; /// Validación
                try
                {
                if (!btnModificar.Enabled)
                    txtCantidad.Text = currentDetalleOrden == null ? darformato(1) : darformato(currentDetalleOrden.cantidad);
                    txtDescuento.Text = currentDetalleOrden == null ? darformato(0) : darformato(currentDetalleOrden.descuento);
                     double precioCompra = currentDetalleOrden == null ? (double)currentProducto.precioCompra : currentDetalleOrden.precioUnitario;
                     double cantidadUnitario = 1;
                     double precioUnidatio = precioCompra * cantidadUnitario;
                     txtPrecioUnitario.Text = darformato(precioUnidatio);

            
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "determinar precio unitario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            
           

        }

        private void calcularDescuento()
        {


            try
            {

                if (cbxTipoMoneda.SelectedValue == null)
                    return;

                double descuentoTotal = 0;
                Moneda moneda = monedas.Find(X => X.idMoneda == (int)cbxTipoMoneda.SelectedValue);

                // calcular el descuento total
                foreach (DetalleOrden C in detalleA)
                {
                    // calculamos el decuento para cada elemento

                    if (C.estado == 1)
                    {
                        double total = C.precioUnitario * C.cantidad;
                        double descuentoC = total - C.total;
                        descuentoTotal += descuentoC;
                    }

                }
                this.Descuento = descuentoTotal;

                lbDescuentoCompras.Text = moneda.simbolo + ". " + darformato(descuentoTotal);



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Calcular descuento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }


        private bool exitePresentacion(int idPresentacion)
        {
            foreach (DetalleOrden C in detalleA)
            {
                if (C.idPresentacion == idPresentacion)
                    return true;
            }

            return false;

        }

        private void decorationDataGridView()
        {

            try
            {

                if (dgvDetalleOrdenCompra.Rows.Count == 0) return;

                foreach (DataGridViewRow row in dgvDetalleOrdenCompra.Rows)
                {
                    int idPresentacion = Convert.ToInt32(row.Cells[1].Value); // obteniedo el idCategoria del datagridview

                    DetalleOrden aux = detalleA.Find(x => x.idPresentacion == idPresentacion); // Buscando la categoria en las lista de categorias
                    if (aux.estado == 0 || aux.estado == 9)
                    {
                        dgvDetalleOrdenCompra.ClearSelection();
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 224);
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(250, 5, 73);
                    }
                }

            }
            
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Calcular descuento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        private void limpiarCamposProducto()
        {
            cbxCodigoProducto.SelectedIndex = -1;
            cbxDescripcion.SelectedIndex = -1;
            cbxDescripcion.Text = "";

            cbxVariacion.SelectedIndex = -1;
            txtCantidad.Text = "";
            txtDescuento.Text = "";
            txtPrecioUnitario.Text = "";
            txtTotalProducto.Text = "";
        }

        private DetalleOrden buscarElemento(int idPresentacion, int idCombinacion)
        {
            return detalleA.Find(x => x.idPresentacion == idPresentacion && x.idCombinacionAlternativa == idCombinacion);
        }


        #endregion

        #region=========== Eventos ========================

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

        private void dgvDetalleCompra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            int y = e.ColumnIndex;
            if (dgvDetalleOrdenCompra.Columns[y].Name == "acciones")
                {
                    if (dgvDetalleOrdenCompra.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay un registro seleccionado", "eliminar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (nuevo)
                    {
                        int index = dgvDetalleOrdenCompra.CurrentRow.Index; // Identificando la fila actual del datagridview
                        int idPresentacion = Convert.ToInt32(dgvDetalleOrdenCompra.Rows[index].Cells[1].Value);
                        int idCombinacion = Convert.ToInt32(dgvDetalleOrdenCompra.Rows[index].Cells[2].Value);




                         // obteniedo el idRegistro del datagridview
                        DetalleOrden aux = buscarElemento(idPresentacion, idCombinacion); 
                        dgvDetalleOrdenCompra.Rows.RemoveAt(index);

                        detalleA.Remove(aux);

                        calculoSubtotal();
                        calcularDescuento();
                    }
                    else
                    {
                        int index = dgvDetalleOrdenCompra.CurrentRow.Index;
                        int idPresentacion = Convert.ToInt32(dgvDetalleOrdenCompra.Rows[index].Cells[1].Value);
                        int idCombinacion = Convert.ToInt32(dgvDetalleOrdenCompra.Rows[index].Cells[2].Value);
                        // obteniedo el idRegistro del datagridview
                        DetalleOrden aux = buscarElemento(idPresentacion, idCombinacion);
                        aux.estado = 9;                       
                        dgvDetalleOrdenCompra.ClearSelection();
                        dgvDetalleOrdenCompra.Rows[index].DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 224);
                        dgvDetalleOrdenCompra.Rows[index].DefaultCellStyle.ForeColor = Color.FromArgb(250, 5, 73);
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

        private void dgvDetalleCompra_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificando la existencia de datos en el datagridview
            if (dgvDetalleOrdenCompra.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int index = dgvDetalleOrdenCompra.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dgvDetalleOrdenCompra.Rows[index].Cells[1].Value);
            int idCombinacion= Convert.ToInt32(dgvDetalleOrdenCompra.Rows[index].Cells[2].Value);




            // obteniedo el idRegistro del datagridview
            currentDetalleOrden = buscarElemento(idPresentacion, idCombinacion); // Buscando la registro especifico en la lista de registros
            cbxCodigoProducto.Text = currentDetalleOrden.codigoProducto;
            cbxDescripcion.Text = currentDetalleOrden.descripcion;
            cbxVariacion.Text = currentDetalleOrden.nombreCombinacion;
            cbxDescripcion.Text = currentDetalleOrden.descripcion;
            txtCantidad.Text =darformato(currentDetalleOrden.cantidad);
            txtPrecioUnitario.Text = darformato(currentDetalleOrden.precioUnitario);
            txtDescuento.Text = darformato(currentDetalleOrden.descuento);
            txtTotalProducto.Text = darformato(currentDetalleOrden.total);
            btnAgregar.Enabled = false;
            btnModificar.Enabled = true;

            cbxCodigoProducto.Enabled = false;

            cbxDescripcion.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            currentDetalleOrden.cantidad =toDouble(txtCantidad.Text);
            currentDetalleOrden.precioUnitario = toDouble(txtPrecioUnitario.Text);
            currentDetalleOrden.total =toDouble(txtTotalProducto.Text);
            currentDetalleOrden.descuento= toDouble(txtDescuento.Text);
            detalleCompraBindingSource.DataSource = null;
            detalleCompraBindingSource.DataSource = detalleA;
            dgvDetalleOrdenCompra.Refresh();
            // Calculo de totales y subtotales
            calculoSubtotal();
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;


            cbxCodigoProducto.Enabled = true;
           
            cbxDescripcion.Enabled = true;
            limpiarCamposProducto();
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

            try
            {

            if (seleccionado)
            {




                if (detalleA == null) detalleA = new List<DetalleOrden>();
                DetalleOrden detalleCompra = new DetalleOrden();

                DetalleOrden find = buscarElemento(Convert.ToInt32(currentProducto.idPresentacion), (int)cbxVariacion.SelectedValue);


                if (find!=null)
                {

                    MessageBox.Show("Este dato ya fue agregado", "presentacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                }
                // Creando la lista
                detalleCompra.cantidad = toDouble(txtCantidad.Text);
                /// Busqueda presentacion

                detalleCompra.cantidadUnitaria = toDouble( txtCantidad.Text);
                detalleCompra.codigoProducto = cbxCodigoProducto.Text.Trim();
                detalleCompra.descripcion = cbxDescripcion.Text.Trim();
                detalleCompra.descuento = toDouble(txtDescuento.Text.Trim());
                detalleCompra.estado = 1;
                detalleCompra.idCombinacionAlternativa = Convert.ToInt32(cbxVariacion.SelectedValue);
                detalleCompra.idCompra = 0;
                detalleCompra.idDetalleCompra = 0;
                detalleCompra.idPresentacion = Convert.ToInt32(currentProducto.idPresentacion);
                detalleCompra.idProducto = Convert.ToInt32(cbxCodigoProducto.SelectedValue);
                detalleCompra.idSucursal = ConfigModel.sucursal.idSucursal;
                detalleCompra.nombreCombinacion = cbxVariacion.Text;
                detalleCompra.nombreMarca = currentProducto.nombreMarca;
                detalleCompra.nombrePresentacion = currentProducto.nombreProducto;
                detalleCompra.nro = 1;
                detalleCompra.precioUnitario = toDouble(txtPrecioUnitario.Text.Trim());
                detalleCompra.total = toDouble(txtTotalProducto.Text.Trim());
                // agrgando un nuevo item a la lista
                detalleA.Add(detalleCompra);
                // Refrescando la tabla
                detalleOrdenBindingSource.DataSource = null;
                detalleOrdenBindingSource.DataSource = detalleA;
                dgvDetalleOrdenCompra.Refresh();
                // Calculo de totales y subtotales e impuestos
                calculoSubtotal();
                calcularDescuento();
                limpiarCamposProducto();

                decorationDataGridView();

            }
            else
            {

                MessageBox.Show("Error: elemento no seleccionado", "agregar Elemento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            }
            catch (Exception  ex)
            {

                MessageBox.Show("Error: " + ex.Message, "agregar", MessageBoxButtons.OK, MessageBoxIcon.Warning);


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
                    currentProducto = productos.Find(x => x.idProducto == idProducto);
                    cbxDescripcion.SelectedValue = currentProducto.idPresentacion;
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
                    currentProducto = productos.Find(x => x.idPresentacion == idPresentacion.ToString());
                    cbxCodigoProducto.SelectedValue = currentProducto.idProducto;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }


        }

     

        private async void cargarAlternativas(int tipo)
        {
            if (cbxCodigoProducto.SelectedIndex == -1) return; /// validacion
            try {
                List<AlternativaCombinacion> alternativaCombinacion = await alternativaModel.cAlternativa31(Convert.ToInt32( currentProducto.idPresentacion));
                alternativaCombinacionBindingSource.DataSource = alternativaCombinacion;
                if (btnModificar.Enabled)
                {

                    txtCantidad.Text = darformato(currentDetalleOrden.cantidad);
                    txtPrecioUnitario.Text = darformato(currentDetalleOrden.precioUnitario);
                    txtTotalProducto.Text = darformato(currentDetalleOrden.total);

                }
                /// calculos
                calcularPrecioUnitario(tipo);
                calcularTotal();
            }                                                  /// cargando las alternativas del producto
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private  void btnComprar_Click(object sender, EventArgs e)
        {

            hacerOrdenCompra();
        } 
        


        private async void hacerOrdenCompra()
        {
            if (nroNuevo != 1)
            {
                //pago
                pagoA.estado = 1;// 8 si
                pagoA.estadoPago = 1;//ver que significado
                                     // Moneda aux = monedaBindingSource.;


                Moneda currentMoneda = monedas.Find(X => X.idMoneda == (int)cbxTipoMoneda.SelectedValue);
                //  Moneda aux = monedaBindingSource.List[i] as Moneda;
                pagoA.idMoneda = currentMoneda.idMoneda;
                pagoA.idPago = currentOrdenCompra != null ? currentOrdenCompra.idPago : 0;
                pagoA.motivo = "COMPRA";
                pagoA.saldo = this.total;
                pagoA.valorPagado = 0;
                pagoA.valorTotal = this.total;
                // compra
                string date = String.Format("{0:u}", dtpFechaEntrega.Value);
                date = date.Substring(0, date.Length - 1);
                compraA.descuento = this.Descuento;//CAMBIAR SEGUN DATOS

                if (currentProveedor == null)
                {
                    //validar 
                    MessageBox.Show("no hay ningun proveedor seleccionado", "proveedor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRuc.Focus();
                    return;


                }
                if(detalleA==null || detalleA.Count == 0)
                {
                    MessageBox.Show("no hay ningun proveedor seleccionado", "proveedor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cbxCodigoProducto.Focus();
                    return;

                    
                }


                compraA.direccion = currentProveedor.direccion;
                compraA.direccionEntrega = txtDireccionEntrega.Text;
                compraA.estado = 8;//es una orden de compra que no ha sido asignado a una compra
                compraA.fechaFacturacion = date; // la fecha data en  dptfecha Entrega
                compraA.fechaPago = date;
                compraA.formaPago = "EFECTIVO";
                compraA.idCompraValor = currentOrdenCompra != null ? currentOrdenCompra.idCompra : 0;

                compraA.idPersonal = PersonalModel.personal.idPersonal;
                compraA.idProveedor = currentProveedor.idProveedor;
                compraA.idSucursal = ConfigModel.sucursal.idSucursal;
                compraA.idTipoDocumento = 1;// orden compra

                compraA.moneda = cbxTipoMoneda.Text;// ver si es correcto

                compraA.numeroDocumento = "";//
                compraA.observacion = txtObservaciones.Text;
                compraA.plazoEntrega = date; // ver si es correcto
                compraA.rucDni = currentProveedor.ruc;
                compraA.subTotal = this.subTotal;
                compraA.tipoCambio = Convert.ToInt32(txtTipoCambio.Text);
                compraA.tipoCompra = "Con productos";
                compraA.total = this.total;
                compraA.ubicacion = txtLugarEntrega.Text;
                compraA.nombreProveedor = currentProveedor.razonSocial;

                //orden de compra
                ordenCompraA.ubicacion = txtLugarEntrega.Text;
                ordenCompraA.total = total;
                ordenCompraA.estado = 1;
                ordenCompraA.direccionEntrega = txtDireccionEntrega.Text;
                ordenCompraA.moneda = currentMoneda.moneda;
                ordenCompraA.observacion = txtObservaciones.Text;
                ordenCompraA.tipoCambio = Convert.ToInt32(currentMoneda.tipoCambio);
                ordenCompraA.formaPago = "EFECTIVO";
                ordenCompraA.nombreProveedor = currentProveedor.razonSocial;
                ordenCompraA.rucDni = currentProveedor.ruc;
                ordenCompraA.direccion = currentProveedor.direccion;
                ordenCompraA.plazoEntrega = date;
                ordenCompraA.idCompraValor = currentOrdenCompra != null ? currentOrdenCompra.idCompra : 0;//algunas dudas sobre este dato
                ordenCompraA.numeroDocumento = "";
                ordenCompraA.idProveedor = currentProveedor.idProveedor;
                ordenCompraA.tipoCompra = "con productos";
                ordenCompraA.subTotal = this.subTotal;

                ordenCompraA.estado = 1;
                ordenCompraA.idPersonal = PersonalModel.personal.idPersonal;
                ordenCompraA.idTipoDocumento = 1;// orden compra
                ordenCompraA.idSucursal = ConfigModel.sucursal.idSucursal;
                ordenCompraA.fechaFacturacion = date;
                ordenCompraA.fechaPago = date;
                ordenCompraA.idUbicacionGeografica = CurrentUbicacionGeografica.idUbicacionGeografica;
                ordenCompraA.idOrdenCompra = currentOrdenCompra != null ? currentOrdenCompra.idOrdenCompra : 0;

                compraTotal.compra = compraA;
                compraTotal.detalle = detalleA;
                compraTotal.ordencompra = ordenCompraA;
                compraTotal.pago = pagoA;
                //

                await ordenCompraModel.guardar(compraTotal);


                if (nuevo)
                {
                    MessageBox.Show("Datos Guardados", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    nroNuevo = 1;
                }
                else
                    MessageBox.Show("Datos  modificador", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                btnComprar.Enabled = false;


        }
        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {

            BuscarProveedor buscarProveedor = new BuscarProveedor();
            buscarProveedor.ShowDialog();

            currentProveedor = buscarProveedor.currentProveedor;


            //cargando datas del proveedor
            txtRuc.Text = currentProveedor.ruc;
            cbxProveedor.SelectedValue = currentProveedor.idProveedor;
            txtDireccionProveedor.Text = currentProveedor.direccion;

           


        }

        private async void txtRuc_TextChanged_1(object sender, EventArgs e)
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
                    List<Proveedor> proveedores = await proveedorModel.buscarPorDni(nroDocumento);
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
                    proveedores = await proveedorModel.listaProveedores();
                    proveedorBindingSource.DataSource = null;

                    proveedorBindingSource.DataSource = proveedores;
                    Response response = formProveedorNuevo.uCProveedorGeneral.response;
                    if (response != null)
                        if (response.id > 0)
                        {
                            currentProveedor  = proveedores.Find(X => X.idProveedor == response.id);
                            txtDireccionProveedor.Text = currentProveedor.direccion;
                            cbxProveedor.Text = currentProveedor.razonSocial;
                        }
                }
            }


        }

        private void cbxProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxProveedor.SelectedIndex == -1) return;

            currentProveedor = proveedores.Find(X => X.idProveedor == (int)cbxProveedor.SelectedValue);

            txtRuc.Text = currentProveedor.ruc;
            cbxProveedor.SelectedValue = currentProveedor.idProveedor;
            txtDireccionProveedor.Text = currentProveedor.direccion;
        }

        private void btnBuscarLugar_Click(object sender, EventArgs e)
        {
            formGeografia formGeografia = new formGeografia();

            formGeografia.ShowDialog();

            CurrentUbicacionGeografica = formGeografia.ubicacionGeografica;
            txtLugarEntrega.Text = formGeografia.cadena;

            txtDireccionEntrega.Focus();
        }

        private void txtLugarEntrega_DoubleClick(object sender, EventArgs e)
        {
            txtLugarEntrega.Multiline = true;
        }

        private void btnModificar_EnabledChanged(object sender, EventArgs e)
        {
            if(btnModificar.Enabled)
                this.btnModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(103)))), ((int)(((byte)(178)))));
            else
                btnModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(88)))), ((int)(((byte)(152)))));       
        }

        private void btnAgregar_EnabledChanged(object sender, EventArgs e)
        {

            if(btnModificar.Enabled)
                this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            else
                
                btnAgregar.BackColor= System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(139)))), ((int)(((byte)(23)))));

        }

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

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            FormProductoNuevo formProductoNuevo = new FormProductoNuevo();
            formProductoNuevo.ShowDialog();
            cargarProductos();
            cargarPresentacion();
        }

        private  void btnComprarOrdenCompra_Click(object sender, EventArgs e)
        {
            ComprarOrden();

        }

        private async void ComprarOrden()
        {

            if (currentOrdenCompra != null)
            {

                CompraOrdenCompra compra = new CompraOrdenCompra();
                compra.estado = 1;// 
                compra.formaPago = "EFECTIVO";

                int i = cbxTipoMoneda.SelectedIndex;
                compra.idMoneda = monedas[i].idMoneda;
                compra.idOrdenCompra = currentOrdenCompra.idOrdenCompra;
                compra.moneda = monedas[i].moneda;
                compra.subTotal = subTotal;
                compra.tipoCambio = Convert.ToInt32(monedas[i].tipoCambio);
                compra.total = total;

                try
                {

                    await ordenCompraModel.comprarOrdenCompra(compra);


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message, "consulta sunat", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

                MessageBox.Show("Orden de compra realizada", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnComprar.Enabled = false;
            }

            else
                MessageBox.Show("no exite orden de compra", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isDecimal(e, txtRuc.Text);
        }


        #endregion=========== Eventos ========================

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbxCodigoProducto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void cbxDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void cbxVariacion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void txtCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void txtPrecioUnitario_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void txtDescuento_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void txtTotalProducto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAgregar.Focus();
        }

        private void btnAgregar_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
                cbxCodigoProducto.Focus();
        }

        private void txtRuc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void cbxProveedor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void txtDireccionProveedor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnBuscarLugar.Focus();
        }

        private void btnBuscarLugar_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void btnBuscarLugar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void txtDireccionEntrega_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void cbxTipoMoneda_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void dtpFechaEntrega_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnComprarOrdenCompra.Focus();
        }

        private void btnComprarOrdenCompra_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void txtObservaciones_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl((Control)sender, true, true, true, true);
        }
    }
}
