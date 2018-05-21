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
using Admeli.Productos;
using Admeli.Productos.Nuevo;
using System.Text.RegularExpressions;
using System.Globalization;
using Admeli.Herramientas.Detalle;

namespace Admeli.Herramientas
{
    public partial class UCInicializarStock : UserControl
    {
        private FormPrincipal formPrincipal;
        public bool lisenerKeyEvents { get; set; }

        private CategoriaModel categoriaModel = new CategoriaModel();
        private SucursalModel sucursalModel = new SucursalModel();
        private AlmacenModel almacenModel = new AlmacenModel();
        private ProductoModel productoModel = new ProductoModel();
        private StockModel stockModel = new StockModel();
        private Paginacion paginacion;
        private List<ProductoData> productos { get; set; }
        List<CombinacionStock> combinaciones { get; set; }
        List<CombinacionStock> combinacionesProducto { get; set; }
        private List<Sucursal> listSuc { get; set; }
        private List<Sucursal> listSucCargar { get; set; }
        private List<Almacen> listAlm { get; set; }
        private List<Almacen> listAlmCargar { get; set; }
        private ProductoData currentProdcuto { get; set; }
        TextBox txt { get; set; }
        int index = 0;
        #region ================================ CONSTRUCTOR ================================
        public UCInicializarStock()
        {
            InitializeComponent();

            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));
            ConfigModel.currentProductoCategoryStock.Add("id0", 0);
        }

        public UCInicializarStock(FormPrincipal formPrincipal)
        {
            InitializeComponent();
            this.formPrincipal = formPrincipal;

            lblSpeedPages.Text = ConfigModel.configuracionGeneral.itemPorPagina.ToString();     // carganto los items por página
            paginacion = new Paginacion(Convert.ToInt32(lblCurrentPage.Text), Convert.ToInt32(lblSpeedPages.Text));

            ConfigModel.currentProductoCategoryStock.Add("id0", 0);
        } 
        #endregion

        #region ================================= Paint =================================
        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panelContainer);
        }

        private void UCInicializarStock_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.lineBorder(panelSucursal);
            drawShape.lineBorder(panelAlmacen);
           
        }


        private void decorationDataGridView()
        {
            if (dataGridView.Rows.Count == 0) return;
            try
            {

                foreach (DataGridViewRow row in dataGridView.Rows)
                {

                  // obteniedo el idCategoria del datagridview
                    int idPresentacion = Convert.ToInt32(row.Cells["idPresentacionDataGridViewTextBoxColumn"].Value);
                    int idAlmacen = Convert.ToInt32(row.Cells["idAlmacenDataGridViewTextBoxColumn"].Value);

                    //ProductoData data = productos.Find(X => X.idPresentacion == idPresentacion && X.idAlmacen== idAlmacen);
                    combinacionesProducto = combinaciones.Where(X => X.idPresentacion == idPresentacion && X.idAlmacen == idAlmacen).ToList(); ;

                
                    if (combinacionesProducto.Count>0)
                    {
                        dataGridView.ClearSelection();
                        row.DefaultCellStyle.BackColor = Color.FromArgb(122,255,105);
                        row.DefaultCellStyle.ForeColor = Color.Green;
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Mostrar Combinaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            

        }
        #endregion

        #region ============================= ROOT LOAD =============================
        private void UCInicializarStock_Load(object sender, EventArgs e)
        {
            this.darFormatoDecimales();
            this.reLoad();

            // Preparando para los eventos de teclado
            this.ParentChanged += ParentChange; // Evetno que se dispara cuando el padre cambia // Este eveto se usa para desactivar lisener key events de este modulo
            if (TopLevelControl is Form) // Escuchando los eventos del formulario padre
            {
                (TopLevelControl as Form).KeyPreview = true;
                TopLevelControl.KeyUp += TopLevelControl_KeyUp;
            }
          
        }

        private void darFormatoDecimales()
        {
            //Compra,Impuesto,Utilidad,Venta,Stock
            dataGridView.Columns["precioCompraDataGridViewTextBoxColumn"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;
            dataGridView.Columns["precioConImpuestoDataGridViewTextBoxColumn"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;
            dataGridView.Columns["utilidadDataGridViewTextBoxColumn"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;
            dataGridView.Columns["precioVentaDataGridViewTextBoxColumn"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;
            dataGridView.Columns["stockDataGridViewTextBoxColumn"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;
            
            dataGridView.Columns["precioCompraDataGridViewTextBoxColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["precioConImpuestoDataGridViewTextBoxColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["utilidadDataGridViewTextBoxColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["precioVentaDataGridViewTextBoxColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["stockDataGridViewTextBoxColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        internal void reLoad(bool refreshData = true)
        {
            //Dar formato
            
            if (refreshData)
            {
                this.cargarCategorias(); // 
                this.cargarSucursales();
                this.cargarAlmacenes();
                this.cargarRegistros();
            }
            this.lisenerKeyEvents = true; // Active lisener key events
         
            
        }
        #endregion

        #region ======================== KEYBOARD ========================
        // Evento que se dispara cuando el padre cambia
        private void ParentChange(object sender, EventArgs e)
        {
            dataGridView.ReadOnly = true;
        }

        // Escuchando los Eventos de teclado
        private void TopLevelControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (!lisenerKeyEvents) return;
            switch (e.KeyCode)
            {
                case Keys.F3:
                    //executeNuevo();
                    break;
                case Keys.F4:
                    executeModificar();
                    break;
                case Keys.F5:
                    cargarRegistros();
                    break;
                case Keys.F7:
                    //executeAnular();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region ==================================== Load ====================================
        private async void cargarCategorias()
        {
            loadState(true);
            try
            {
                // Cargando las categorias desde el webservice
                List<Categoria> categoriaList = await categoriaModel.categoriasTodo();
                Categoria lastCategori = categoriaList.Last();//raiz
                categoriaList.Remove(lastCategori);

                // Cargando
                treeViewCategoria.Nodes.Clear(); // limpiando
                treeViewCategoria.Nodes.Add(lastCategori.idCategoria.ToString(), lastCategori.nombreCategoria); // Cargando categoria raiz

                treeViewCategoria.Nodes[0].Checked = true;
                List< TreeNode> listNode = new List<TreeNode>();
                itemNumber = 0;
                foreach (Categoria categoria in categoriaList)
                {
                    TreeNode aux = new TreeNode(categoria.nombreCategoria);
                    aux.Name = categoria.idCategoria.ToString();
                    listNode.Add(aux);
                   

                }
                    treeviewVista(categoriaList, treeViewCategoria.Nodes[0],listNode);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar categorias", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            loadState(false);
        }

        private Categoria primerElemento(List<Categoria> categoriaList)
        {

            Categoria lastCategori = categoriaList[0];//raiz
            categoriaList.Remove(lastCategori);
            return lastCategori;
        }
        private bool hijos(List<Categoria> categoriaList,Categoria padre)
        {
            bool tieneHijos = false;

            foreach (Categoria categoria in categoriaList)
            {
                if(categoria.padre == padre.nombreCategoria)
                {
                    tieneHijos = true;
                    break;

                }

            }
             return tieneHijos;
        }
        private TreeNode buscar(Categoria categoria,List<TreeNode> listNode)
        {
            TreeNode aux=new TreeNode();

            foreach( TreeNode node in listNode)
            {
                if (categoria.nombreCategoria==node.Text)
                {

                    aux = node;
                    break;
                }

            }

            return aux;

        }
        private TreeNode buscarPadre(Categoria categoria, List<TreeNode> listNode)
        {
            TreeNode aux = new TreeNode();

            foreach (TreeNode node in listNode)
            {
                if (categoria.padre==node.Text)
                {

                    aux = node;
                    break;
                }

            }

            return aux;

        }
        private void treeviewVista( List<Categoria> categoriaList,TreeNode padreNode, List<TreeNode> listNode)
        {
            if (categoriaList.Count== 0)
            {
                return;

            }
            else
            {
                    Categoria aux = primerElemento(categoriaList);
                    TreeNode auxTreeNode = buscar(aux, listNode);
                    TreeNode nodePadre = buscarPadre(aux, listNode);
                   if (hijos(categoriaList,aux))
                    {
                         auxTreeNode.ImageIndex = 1;
                    }

                    if (nodePadre.Text!="")
                    {
                        
                        nodePadre.Nodes.Add(auxTreeNode);
                        treeviewVista( categoriaList, padreNode, listNode);
                    }
                    else
                    {
                        
                        padreNode.Nodes.Add(auxTreeNode);
                        
                        treeviewVista(categoriaList, padreNode, listNode);

                    }
                        
                  
                
            }



        }
        

          
        private  void cargarSucursales()
        {
            // Cargando el combobox de personales
            loadState(true);
            try
            {
                listSuc = new List<Sucursal>();
                listSucCargar = new List<Sucursal>();
                listSuc = ConfigModel.listSucursales;
                Sucursal sucursal = new Sucursal();
                sucursal.idSucursal = 0;
                sucursal.nombre = "Todas";
                listSucCargar.Add(sucursal);
                listSucCargar.AddRange(listSuc);
                sucursalBindingSource.DataSource = listSucCargar;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar sucursales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private  void cargarAlmacenes()
        {
            // Cargando el combobox de personales
            loadState(true);
            try
            {
                listAlm = new List<Almacen>();
                listAlmCargar = new List<Almacen>();
                listAlm = ConfigModel.alamacenes;
                Almacen sucursal = new Almacen();
                sucursal.idAlmacen = 0;
                sucursal.nombre = "Todas";
                listAlmCargar.Add(sucursal);
                listAlmCargar.AddRange(listAlm);
                almacenBindingSource.DataSource = listAlmCargar;
                
                cbxSucursales.SelectedIndex = -1;
                cbxSucursales.SelectedValue = 0;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar sucursales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cargarRegistros()
        {
            loadState(true);
            try
            {
                Dictionary<string, int> list = new Dictionary<string, int>();
              
                
                Dictionary<string, int> sendList = (ConfigModel.currentProductoCategoryStock.Count == 0) ? list : ConfigModel.currentProductoCategoryStock;
                int sucursalId = (cbxSucursales.SelectedIndex == -1) ? 0 : Convert.ToInt32(cbxSucursales.SelectedValue);
                int almacenID = (cbxAlmacenes.SelectedIndex == -1) ? 0 : Convert.ToInt32(cbxAlmacenes.SelectedValue);
                RootObjectData rootObjectData=null;
                if (sendList.Count > 0)
                {
                    rootObjectData = await productoModel.stockHerramienta<RootObjectData>(sendList, almacenID, sucursalId, paginacion.currentPage, paginacion.speed);
                    // actualizando datos de páginacón
                    paginacion.itemsCount = rootObjectData.nro_registros;
                    paginacion.reload();

                    // Ingresando
                    productos = rootObjectData.productos;
                    productoDataBindingSource.DataSource = productos;
                    combinaciones = rootObjectData.combinacion; 
                    dataGridView.Refresh();

                    // Mostrando la paginacion
                    mostrarPaginado();

                    // Formato de celdas
                }

                else
                {
                    paginacion.itemsCount = 0;
                    paginacion.reload();

                    // Ingresando

                    productoDataBindingSource.DataSource=null;
                    dataGridView.Refresh();

                    // Mostrando la paginacion
                    mostrarPaginado();

                }
                decorationDataGridView();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {

                loadState(false);
                if(dataGridView.Rows.Count>0)
                    dataGridView.Rows[index].Selected = true;
            }
        }

        private async void cargarRegistrosBuscar()
        {
            loadState(true);

            dataGridView.ReadOnly = true;
            try
            {
                if (textBuscar.Text == "")
                {
                    cargarRegistros();
                    return;
                }

                Dictionary<string, int> list = new Dictionary<string, int>();
             
                Dictionary<string, int> sendList = (ConfigModel.currentProductoCategoryStock.Count == 0) ? list : ConfigModel.currentProductoCategoryStock;

                int sucursalId = (cbxSucursales.SelectedIndex == -1) ?0 : Convert.ToInt32(cbxSucursales.SelectedValue);
                int almacenID = (cbxAlmacenes.SelectedIndex == -1) ?0 : Convert.ToInt32(cbxAlmacenes.SelectedValue);
                RootObjectData rootObjectData = null;
              
               
                if (sendList.Count > 0)
                {
                     rootObjectData = await productoModel.stockHerramientaLike<RootObjectData>(sendList, textBuscar.Text.Trim(), almacenID, sucursalId, paginacion.currentPage, paginacion.speed);
                    // actualizando datos de páginacón
                    paginacion.itemsCount = rootObjectData.nro_registros;
                    paginacion.reload();

                    // Ingresando
                    productos = rootObjectData.productos;                   
                    productoDataBindingSource.DataSource = null;
                    productoDataBindingSource.DataSource = productos;
                    combinaciones = rootObjectData.combinacion;
                    dataGridView.Refresh();

                    // Mostrando la paginacion
                    mostrarPaginado();
                   
                    // Formato de celdas
                }

                else
                {
                    paginacion.itemsCount = 0;
                    paginacion.reload();

                    // Ingresando

                    productoDataBindingSource.DataSource = null;
                    dataGridView.Refresh();

                    // Mostrando la paginacion
                    mostrarPaginado();

                }
                decorationDataGridView();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar presentaciones", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadState(false);
                if(dataGridView.Rows.Count> 0)
                    dataGridView.Rows[index].Selected = true;
            }
        }
        #endregion

        #region ===================== Eventos Páginación =====================
        private void mostrarPaginado()
        {
            lblCurrentPage.Text = paginacion.currentPage.ToString();
            lblPageAllItems.Text = String.Format("{0} Registros", paginacion.itemsCount.ToString());
            lblPageCount.Text = paginacion.pageCount.ToString();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (lblCurrentPage.Text != "1")
            {
                paginacion.previousPage();
                cargarRegistrosBuscar();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (lblCurrentPage.Text != "1")
            {
                paginacion.firstPage();
                cargarRegistrosBuscar();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (lblPageCount.Text == "0") return;
            if (lblPageCount.Text != lblCurrentPage.Text)
            {
                paginacion.nextPage();
                cargarRegistrosBuscar();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (lblPageCount.Text == "0") return;
            if (lblPageCount.Text != lblCurrentPage.Text)
            {
                paginacion.lastPage();
                cargarRegistrosBuscar();
            }
        }

        private void lblSpeedPages_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                paginacion.speed = Convert.ToInt32(lblSpeedPages.Text);
                paginacion.currentPage = 1;
                cargarRegistrosBuscar();
            }
        }

        private void lblCurrentPage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                paginacion.reloadPage(Convert.ToInt32(lblCurrentPage.Text));
                cargarRegistrosBuscar();
            }
        }

        private void lblCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private void lblSpeedPages_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }
        #endregion

        #region ============================= Estados =============================
        private void loadState(bool state)
        {
            formPrincipal.appLoadState(state);
            panelNavigation.Enabled = !state;
            dataGridView.Enabled = !state;
        }
        #endregion

        #region ============================ Tools Events ============================
        private void cbxSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSucursales.SelectedIndex == -1)
                return;

            if ((int)cbxSucursales.SelectedValue == 0)
            {


                almacenBindingSource.DataSource = listAlmCargar;
                cbxAlmacenes.SelectedIndex = -1;
                cbxAlmacenes.SelectedIndex = 0;

            }
            else
            {
                List<Almacen> listA = new List<Almacen>();
                Almacen almacen = new Almacen();
                almacen.idAlmacen = 0;
                almacen.nombre = "Todas";
                listA.Add(almacen);


                List<Almacen> list = listAlm.Where(X => X.idSucursal == (int)cbxSucursales.SelectedValue).ToList();
                listA.AddRange(list);
                almacenBindingSource.DataSource = listA;
                cbxAlmacenes.SelectedIndex = -1;
                cbxAlmacenes.SelectedIndex = 0;

            }

        }

        private void cbxAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            paginacion.currentPage = 1;
            cargarRegistrosBuscar();
        }

        private void textBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                paginacion.currentPage = 1;
                cargarRegistrosBuscar();
            }
        }

        private void btnIngresos_Click(object sender, EventArgs e)
        {
            cargarRegistros();
        }
        #endregion

        #region ======================== Treeview control checked ========================

      


        private int itemNumber { get; set; }

        private void treeViewCategoria_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // OBteniendo los datos seleccionados del treeView y almacenando en un diccionary
            TreeNode mainNode = treeViewCategoria.Nodes[0];
            itemNumber = 0;
            
            ConfigModel.currentProductoCategoryStock.Clear();
            getRecursiveNodes(mainNode);
            paginacion.currentPage = 1;
            // cargando los registros
            cargarRegistrosBuscar();
        }
        public void getRecursiveNodes(TreeNode parentNode)
        {
            if (parentNode.Checked)
            {
                ConfigModel.currentProductoCategoryStock.Add("id" + itemNumber.ToString(), Convert.ToInt32(parentNode.Name));
                itemNumber++;
            }
            foreach (TreeNode subNode in parentNode.Nodes)
            {
                getRecursiveNodes(subNode);
            }
        }
        #endregion

        #region ======================= Control de isChecked en el treeview =======================
        private void CheckTreeViewNode(TreeNode node, Boolean isChecked)
        {
            foreach (TreeNode item in node.Nodes)
            {
                item.Checked = isChecked;

                if (item.Nodes.Count > 0)
                {
                    this.CheckTreeViewNode(item, isChecked);
                }
            }
        }

        private void treeViewCategoria_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckTreeViewNode(e.Node, e.Node.Checked);
        }
        #endregion


        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
            dataGridView.ReadOnly = false;

            foreach (DataGridViewRow R in dataGridView.Rows)
            {

                R.ReadOnly = true;
                foreach (DataGridViewCell C in R.Cells)
                {
                    C.Style.SelectionBackColor = R.DefaultCellStyle.SelectionBackColor;

                }
            }

            int idProducto = Convert.ToInt32(dataGridView.CurrentRow.Cells["idProductoDataGridViewTextBoxColumn"].Value); // obteniedo el idCategoria del datagridview
            int idAlmacen = Convert.ToInt32(dataGridView.CurrentRow.Cells["idAlmacenDataGridViewTextBoxColumn"].Value); // obteniedo el idCategoria del datagridview


            ProductoData data = productos.Find(X => X.idProducto == idProducto && X.idAlmacen== idAlmacen );
            combinacionesProducto = combinaciones.Where(X => X.idPresentacion == data.idPresentacion && X.idAlmacen == idAlmacen).ToList(); ;

            dataGridView.Rows[index].ReadOnly = false;

            dataGridView.Rows[index].Cells[1].ReadOnly = true;
            dataGridView.Rows[index].Cells[2].ReadOnly = true;
            dataGridView.Rows[index].Cells[3].ReadOnly = true;
            dataGridView.Rows[index].Cells[4].ReadOnly = true;
            dataGridView.Rows[index].Cells[7].ReadOnly = true;

            dataGridView.Rows[index].Cells[5].ReadOnly = false;
            dataGridView.Rows[index].Cells[5].Selected = true;
            dataGridView.Rows[index].Cells[5].Style.SelectionBackColor = Color.FromArgb(255,247,178);
           
            dataGridView.Rows[index].Cells[5].Style.SelectionForeColor = Color.Black;

            dataGridView.Rows[index].Cells[6].ReadOnly = false;
            dataGridView.Rows[index].Cells[6].Selected = true;
            dataGridView.Rows[index].Cells[6].Style.SelectionBackColor = Color.FromArgb(255, 247, 178);
            dataGridView.Rows[index].Cells[6].Style.SelectionForeColor = Color.Black;

            dataGridView.Rows[index].Cells[8].ReadOnly = false;
            dataGridView.Rows[index].Cells[8].Selected = true;
            dataGridView.Rows[index].Cells[8].Style.SelectionBackColor = Color.FromArgb(255, 247, 178);
            dataGridView.Rows[index].Cells[8].Style.SelectionForeColor = Color.Black;
            if (combinacionesProducto.Count == 0)
            {
                dataGridView.Rows[index].Cells[9].ReadOnly = false;
                dataGridView.Rows[index].Cells[9].Selected = true;
                dataGridView.Rows[index].Cells[9].Style.SelectionBackColor = Color.FromArgb(255, 247, 178);
                dataGridView.Rows[index].Cells[9].Style.SelectionForeColor = Color.Black;
            }
            else
            {
                dataGridView.Rows[index].Cells[9].ReadOnly = true;

            }

        }

        private void btnNuevoCategoria_Click(object sender, EventArgs e)
        {

            FormCategoriaNuevo formCategoria = new FormCategoriaNuevo();
            formCategoria.ShowDialog();
            cargarCategorias();
        }

        private void btnActualizarCategoria_Click(object sender, EventArgs e)
        {
            cargarCategorias();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FormProductoNuevo producto = new FormProductoNuevo();
            producto.ShowDialog();
            cargarRegistrosBuscar();

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            cargarRegistros();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            executeModificar();
        }

        private void executeModificar() // no cargar el form de producto
        {
         
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dataGridView.Rows[index].Cells["idPresentacionDataGridViewTextBoxColumn"].Value); // obteniedo el idRegistro del datagridview

            currentProdcuto = productos.Find(x => x.idPresentacion == idPresentacion); // Buscando la registro especifico en la lista de registros

            Producto auxProducto = new Producto();

            auxProducto.nombreProducto = currentProdcuto.nombreProducto;
            auxProducto.codigoProducto = currentProdcuto.codigoProducto;
            auxProducto.precioCompra = currentProdcuto.precioCompra;
            auxProducto.descripcionCorta = currentProdcuto.descripcionCorta;
            auxProducto.idProducto = currentProdcuto.idPresentacion;


            // Mostrando el formulario de modificacion
            FormProductoNuevo formProducto = new FormProductoNuevo(auxProducto);
            formProducto.ShowDialog();
            cargarRegistrosBuscar(); // recargando loas registros en el datagridview
            
        }

        
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          


        }

       

        private async void btnGuardar_Click_1(object sender, EventArgs e)
        {

            loadState(true);
            try
            {
                BindingSource bindingSource=    dataGridView.DataSource as BindingSource ;
                productos= bindingSource.DataSource as List<ProductoData>;
                ProductoStockGuardar productoStockGuardar = new ProductoStockGuardar();
                productoStockGuardar.datos = productos;
                Response response=  await stockModel.guardarproductosp(productoStockGuardar);
                if (response.id >0)
                {
                    MessageBox.Show("Mensaje: " + response.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

                loadState(false);
                cargarRegistrosBuscar();

            }




        }

        private void dataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //string aux = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            //Validator.isDecimal(e, aux);
        }

        private void dataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dataGridView.Rows.Count == 0)
            { 
            
                return;
            }

            string valor = dataGridView.CurrentCell.Value.ToString();
            Validator.isDecimal(e, valor);
        }

        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
           
           
        }

        

        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dataGridView.CurrentCell.ColumnIndex == 5 || dataGridView.CurrentCell.ColumnIndex == 6 || dataGridView.CurrentCell.ColumnIndex ==8 ||
                dataGridView.CurrentCell.ColumnIndex ==9 )
            {

                 txt = e.Control as TextBox;
               
                if (txt != null)
                {
                    txt.KeyPress -= new KeyPressEventHandler(dataGridview_KeyPress);
                    txt.KeyPress += new KeyPressEventHandler(dataGridview_KeyPress);
                    txt.TextChanged -= new EventHandler(dataGridview_text);
                    txt.TextChanged += new EventHandler(dataGridview_text);

                }

            }
        }
        private void dataGridview_KeyPress(object sender, KeyPressEventArgs e)
        {
            string texto = txt.Text;

            Validator.isDecimal(e, texto);
           
            
        }

        private void dataGridview_text(object sender, EventArgs e)
        {

            TextBox text = sender as TextBox;
            if(text.Text == "") return;
            int index = dataGridView.CurrentRow.Index;
            int y = dataGridView.CurrentCell.ColumnIndex;
            int idpresentacion = (int)dataGridView.Rows[index].Cells["idPresentacionDataGridViewTextBoxColumn"].Value;
            int idSucursal = (int)dataGridView.Rows[index].Cells["idSucursalDataGridViewTextBoxColumn"].Value;
            

            if(y == 5 || y == 8)
            {
                
                decimal dato = Convert.ToDecimal(text.Text);

                if (y == 5)// modificar precio de compra
                {
                    List<ProductoData> listPCompra = productos.Where(X => X.idPresentacion == idpresentacion).ToList();

                    foreach (ProductoData p in listPCompra)
                    {

                        p.precioCompra = dato;
                    }
                }


                if (y == 8)// modificar precio producto
                {

                    List<ProductoData> listPventa = productos.Where(X => X.idPresentacion == idpresentacion && X.idSucursal == idSucursal).ToList();
                    foreach (ProductoData p in listPventa)
                    {

                        p.precioVenta = dato;



                    }
                }
            }
           
            
            
            productoDataBindingSource.DataSource = productos;
            dataGridView.Refresh();

        }


        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {



        }
        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //string valor = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
          
        }

        private void dataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                if (dataGridView.Rows.Count == 0 || dataGridView.CurrentRow == null)
                {
                    MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
                int idPresentacion = Convert.ToInt32(dataGridView.Rows[index].Cells["idPresentacionDataGridViewTextBoxColumn"].Value);
                int idAlmacen = Convert.ToInt32(dataGridView.Rows[index].Cells["idAlmacenDataGridViewTextBoxColumn"].Value);
                ProductoData   data = productos.Find(X => X.idPresentacion == idPresentacion && X.idAlmacen== idAlmacen);
                combinacionesProducto = combinaciones.Where(X => X.idPresentacion == idPresentacion &&  X.idAlmacen== idAlmacen).ToList();
                if (combinacionesProducto.Count > 0)
                {
                    FormDetalleStock detalleStock = new FormDetalleStock(combinacionesProducto , data);
                    detalleStock.ShowDialog();

                }  
                cargarRegistrosBuscar();
                           
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cambiarNombreEliminar();
        }
        private void cambiarNombreEliminar()
        {
            if (dataGridView.Rows.Count == 0)
            {
                return;
            }
            int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idPresentacion = Convert.ToInt32(dataGridView.Rows[index].Cells["idPresentacionDataGridViewTextBoxColumn"].Value); // obteniedo el idRegistro del datagridview
            currentProdcuto = productos.Find(x => x.idPresentacion == idPresentacion); // Buscando la registro especifico en la lista de registros

            if (currentProdcuto.enUso == true)
            {
                btnEliminar.Text = " Desactivar (F6)";
            }
            else
            {
                btnEliminar.Text = " Eliminar (F6)";
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            executeEliminar();
        }

        private async void executeEliminar()
        {
            // Verificando la existencia de datos en el datagridview
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                loadState(true); // cambiando el estado
                int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview

                int idPresentacion = Convert.ToInt32(dataGridView.Rows[index].Cells["idPresentacionDataGridViewTextBoxColumn"].Value); // obteniedo el idRegistro del datagridview
                currentProdcuto = productos.Find(x => x.idPresentacion == idPresentacion); // Buscando la registro especifico en la lista de registros

                if (currentProdcuto.enUso == true)
                {
                    // Pregunta de seguridad de eliminacion
                    DialogResult dialog = MessageBox.Show("¿Está seguro de inhabilitar este registro?", "Inhabilitar",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dialog == DialogResult.No) { return; }
                    Producto sendProducto = new Producto(); //creando una instancia del objeto categoria
                    sendProducto.idProducto = idPresentacion;
                    Response response = await productoModel.inhabilitar(sendProducto); // Eliminando con el webservice correspondiente
                    MessageBox.Show(response.msj, "Inhabilitar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Pregunta de seguridad de eliminacion
                    DialogResult dialog = MessageBox.Show("¿Está seguro de eliminar este registro?", "Eliminar",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dialog == DialogResult.No) { return; }
                    Producto sendProducto = new Producto(); //creando una instancia del objeto categoria
                    sendProducto.idProducto = idPresentacion;
                    Response response = await productoModel.eliminar(sendProducto); // Eliminando con el webservice correspondiente
                    MessageBox.Show(response.msj, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                cargarRegistros(); // recargando el datagridview
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadState(false); // cambiando el estado
            }
        }
    }
}


public class RootObjectData
{
    public List<ProductoData> productos { get; set; }
    public List<CombinacionStock> combinacion { get; set; }
    public int nro_registros { get; set; }
}





