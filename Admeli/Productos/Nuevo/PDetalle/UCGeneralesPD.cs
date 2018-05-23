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
using Entidad;
using System.Globalization;

namespace Admeli.Productos.Nuevo.PDetalle
{
    public partial class UCGeneralesPD : UserControl
    {
        public UnidadMedidaModel unidadMedidaModel = new UnidadMedidaModel();
        public MarcaModel marcaModel = new MarcaModel();
        public CategoriaModel categoriaModel = new CategoriaModel();
        public ProductoModel productoModel = new ProductoModel();
        public PresentacionModel presentacionModel = new PresentacionModel();
        private CategoriaProducto catProducto = new CategoriaProducto();
        private bool nombreValido { get; set; }
        private bool codigoValido { get; set; }
        private bool precioValido { get; set; }
        public bool lisenerKeyEvents { get; internal set; }
        private FormProductoNuevo formProductoNuevo;

        public UCGeneralesPD()
        {
            InitializeComponent();
        }

        public UCGeneralesPD(FormProductoNuevo formProductoNuevo)
        {
            InitializeComponent();
            this.formProductoNuevo = formProductoNuevo;
        }

        #region ================================ Root Load ================================
        private void UCGeneralesPD_Load(object sender, EventArgs e)
        {
            reLoad();
        }

        internal void reLoad()
        {
            cargarMarcas();
            cargarUnidadesMedida();
            cargarDatosModificar();
            cargarPresentacion();
            cargarCategorias();
        }
        #endregion
        private async void cargarCategorias() {
            if (!formProductoNuevo.nuevo)
            {
                catProducto = await categoriaModel.categoriaProducto(formProductoNuevo.currentIDProducto);
                List<Categoria> categoriasDelProducto = catProducto.todo;
                //Mostrar las categorias separadas por comas
                if (categoriasDelProducto.Count <= 0) { return; }
                string textoCategoria = "";
                foreach (Categoria categoria in categoriasDelProducto)
                {
                    textoCategoria += categoria.nombreCategoria + " ,";
                }
                textoCategoria = textoCategoria.Substring(0, textoCategoria.Length - 1);
                textCategoria.Text = textoCategoria;
            }
        }
        public void cambioTextoCategoria(string nuevoTexto)
        {
            textCategoria.Text = nuevoTexto;
        }
        #region ========================================== PAINT ==========================================
        private void UCGeneralesPD_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.bottomLine(panelHeader);
            drawShape.lineBorder(panel12, 157, 157, 157);
            drawShape.lineBorder(panel2, 157, 157, 157);
            drawShape.lineBorder(panelPresentacionBase, 157, 157, 157);
        }
        #endregion

        #region ============================= Loads =============================
        private void cargarDatosModificar()
        {
            if (formProductoNuevo.nuevo) return;

            textNombreProducto.Text = formProductoNuevo.currentProducto.nombreProducto;
            textCodigoProducto.Text = formProductoNuevo.currentProducto.codigoProducto;
            textPrecioCompra.Text = formProductoNuevo.currentProducto.precioCompra.ToString(ConfigModel.configuracionGeneral.formatoDecimales);
            textDescripcion.Text = formProductoNuevo.currentProducto.descripcionCorta;
            codigoValido = true;
            nombreValido = true;
            precioValido = true;
        }

        internal async void cargarMarcas()
        {
            formProductoNuevo.appLoadState(true);
            marcaBindingSource.DataSource = await marcaModel.marcas();
            if (!formProductoNuevo.nuevo) cbxMarcas.SelectedValue = formProductoNuevo.currentProducto.idMarca;
            formProductoNuevo.appLoadState(false);
        }
        internal async void cargarUnidadesMedida()
        {
            formProductoNuevo.appLoadState(true);
            unidadMedidaBindingSource.DataSource = await unidadMedidaModel.unimedidas();
            if (!formProductoNuevo.nuevo) cbxUnidadMedida.SelectedValue = formProductoNuevo.currentProducto.idUnidadMedida;
            formProductoNuevo.appLoadState(false);
        }
        internal async void cargarPresentacion()
        {
            formProductoNuevo.appLoadState(true);
            presentacionBindingSource.DataSource = await presentacionModel.presentacionesTodas(ConfigModel.sucursal.idSucursal);
            //if (!formProductoNuevo.nuevo) cbxUnidadMedida.SelectedValue = formProductoNuevo.currentProducto.idUnidadMedida;
            if (!formProductoNuevo.nuevo) cbxPresentacionBase.SelectedIndex = 0;
            formProductoNuevo.appLoadState(false);
        }
        #endregion

        private void btnAddMarca_Click(object sender, EventArgs e)
        {
            FormMarcaNuevo formMarca = new FormMarcaNuevo();
            formMarca.ShowDialog();
            this.cargarMarcas();
        }

        private void btnAddUnidadMedida_Click(object sender, EventArgs e)
        {
            FormUnidadMedidaNuevo formUnidad = new FormUnidadMedidaNuevo();
            formUnidad.ShowDialog();
            this.cargarUnidadesMedida();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormElegirCategoria formElegir = new FormElegirCategoria(formProductoNuevo,this, formProductoNuevo.currentIDProducto);
            formElegir.ShowDialog();
            //FormElegirCategoria formElegir = new FormElegirCategoria();
            //formElegir.ShowDialog();
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Validando los campos

            if (!codigoValido || !nombreValido || !precioValido)
            {
                MessageBox.Show("Datos incorrectos", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ejecutando el guardado
            cargarObjeto();

            // Ejecutando el gurdado
            formProductoNuevo.executeGuardar();
        }

        private void appLoadState(bool estado)
        {
            btnAceptar.Enabled = !estado;
            this.Enabled=!estado;
        }
        private void cargarObjeto()
        {
            if(formProductoNuevo.nuevo) formProductoNuevo.currentProducto = new Producto();
           // if (!formProductoNuevo.nuevo) formProductoNuevo.currentProducto.idProducto = 1; // Llenar el id categoria cuando este en esTAdo modificar
            if (formProductoNuevo.nuevo)
            {
                formProductoNuevo.currentProducto.cantidadFraccion = false;
                formProductoNuevo.currentProducto.codigoBarras = "";
                formProductoNuevo.currentProducto.controlSinStock = "sin_stock";

                formProductoNuevo.currentProducto.descripcionLarga = "";

                formProductoNuevo.currentProducto.enCategoriaEstrella = false;
                formProductoNuevo.currentProducto.enPortada = false;
                formProductoNuevo.currentProducto.enUso = false;

                formProductoNuevo.currentProducto.keywords = "";

                formProductoNuevo.currentProducto.limiteMaximo = "0";
                formProductoNuevo.currentProducto.limiteMinimo = "0";
                formProductoNuevo.currentProducto.mostrarPrecioWeb = true;
                formProductoNuevo.currentProducto.mostrarVideo = true;
                formProductoNuevo.currentProducto.mostrarWeb = true;

                formProductoNuevo.currentProducto.urlVideo = "";
                formProductoNuevo.currentProducto.ventaVarianteSinStock = false;
            }
            else
            {
                formProductoNuevo.currentProducto.idProducto = formProductoNuevo.currentIDProducto;
            }

            formProductoNuevo.currentProducto.codigoProducto = textCodigoProducto.Text;
            formProductoNuevo.currentProducto.descripcionCorta = textDescripcion.Text;

            formProductoNuevo.currentProducto.estado = chkActivoProducto.Checked;
            formProductoNuevo.currentProducto.idMarca = Convert.ToInt32(cbxMarcas.SelectedValue);
            formProductoNuevo.currentProducto.idUnidadMedida = Convert.ToInt32(cbxUnidadMedida.SelectedValue);

            formProductoNuevo.currentProducto.nombreMarca = cbxMarcas.Text;
            formProductoNuevo.currentProducto.nombreProducto = textNombreProducto.Text;
            formProductoNuevo.currentProducto.nombreUnidad = cbxUnidadMedida.Text;

            formProductoNuevo.currentProducto.precioCompra = Decimal.Parse(textPrecioCompra.Text);
        }

        #region ================================ Validation ===============================
        private void validarPrecio()
        {
            if (textPrecioCompra.Text.Trim() == "")
            {
                Validator.textboxValidateColor(textPrecioCompra, 0);
                errorProvider1.SetError(textPrecioCompra, "Campo obligatorio");
                this.precioValido = false;
                return;
            }
            Validator.textboxValidateColor(textPrecioCompra, 1);
            errorProvider1.Clear();
            this.precioValido = true;
        }

        private async void validarProductoNombreCodigo()
        {
            // Validando si el campo esta vacia
            if (textNombreProducto.Text.Trim() == "")
            {
                Validator.textboxValidateColor(textNombreProducto, 0);
                errorProvider1.SetError(textNombreProducto, "Campo obligatorio");
                this.nombreValido = false;
                return;
            }

            // Creando el objeto para enviar
            Producto np = new Producto();
            np.nombre = textNombreProducto.Text;
            np.idProducto = (formProductoNuevo.nuevo) ? 0 : formProductoNuevo.currentProducto.idProducto;

            // validando si el codigo del producto existe
            List<Producto> list = await productoModel.validarProducto(np);
            if (list.Count > 0)
            {
                errorProvider1.SetError(textNombreProducto, "Ya existe un producto con el mismo nombre.");
                Validator.textboxValidateColor(textNombreProducto, 0);
                this.nombreValido = false;
                return;
            }

            // Dando el formato adecuado cuando paso toda las validaciones
            Validator.textboxValidateColor(textNombreProducto, 1);
            errorProvider1.Clear();
            this.nombreValido = true;
        }

        private async void validarProductoNombre()
        {
            // Validando si el campo esta vacia
            if (textNombreProducto.Text.Trim() == "")
            {
                Validator.textboxValidateColor(textNombreProducto, 0);
                errorProvider1.SetError(textNombreProducto, "Campo obligatorio");
                this.nombreValido = false;
                return;
            }

            // Creando el objeto para enviar
            Producto np = new Producto();
            np.nombre = textNombreProducto.Text;
            np.idProducto = (formProductoNuevo.nuevo) ? 0 : formProductoNuevo.currentIDProducto;

            // validando si el codigo del producto existe
            List<Producto> list = await productoModel.validarProducto(np);
            if (list.Count > 0)
            {
                errorProvider1.SetError(textNombreProducto, "Ya existe un producto con el mismo nombre.");
                Validator.textboxValidateColor(textNombreProducto, 0);
                this.nombreValido = false;
                return;
            }

            // Dando el formato adecuado cuando paso toda las validaciones
            Validator.textboxValidateColor(textNombreProducto, 1);
            errorProvider1.Clear();
            this.nombreValido = true;
        }

        private async void validarProductoCodigo()
        {
            try
            {
                // Validando si el campo esta vacia
                if (textCodigoProducto.Text.Trim() == "")
                {
                    Validator.textboxValidateColor(textCodigoProducto, 0);
                    errorProvider1.SetError(textCodigoProducto, "Campo obligatorio");
                    this.codigoValido = false;
                    return;
                }

                // Creando el objeto para enviar
                Producto np = new Producto();
                np.codigo = textCodigoProducto.Text;
                np.idProducto = (formProductoNuevo.nuevo) ? 0 : formProductoNuevo.currentIDProducto;

                // validando si el codigo del producto existe
                List<Producto> list = await productoModel.validarProducto(np);
                if (list.Count > 0)
                {
                    errorProvider1.SetError(textCodigoProducto, "Ya existe un producto con el mismo código.");
                    Validator.textboxValidateColor(textCodigoProducto, 0);
                    this.codigoValido = false;
                    return;
                }

                // Dando el formato adecuado cuando paso toda las validaciones
                Validator.textboxValidateColor(textCodigoProducto, 1);
                errorProvider1.Clear();
                this.codigoValido = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /**
         * ======================================================================
         *  ---- Eventos de las validaciones en timepo real
         * 
         * */
        private void textCodigoProducto_Validated(object sender, EventArgs e)
        {
            validarProductoCodigo();
        }

        private void textNombreProducto_Validated(object sender, EventArgs e)
        {
            validarProductoNombre();
        }

        private void textPrecioCompra_Validated(object sender, EventArgs e)
        {
            validarPrecio();
        }

        private void textPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isDecimal(e, textPrecioCompra.Text);
        }
        #endregion

        private void btnGuardarSalir_Click(object sender, EventArgs e)
        {
            // Validando los campos
            if (!codigoValido || !nombreValido || !precioValido)
            {
                MessageBox.Show("Datos incorrectos", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ejecutando el guardado
            cargarObjeto();

            // Ejecutando el gurdado
            formProductoNuevo.executeGuardarSalir();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            formProductoNuevo.executeCerrar();
        }

        private void textCodigoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
            //evitarespacios
            if (char.IsSeparator(e.KeyChar))
            {  e.Handled = true;  }

            else
            {  e.Handled = false;  }
        }
    }
}
