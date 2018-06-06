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
using Admeli.AlmacenBox.fecha;
using Admeli.AlmacenBox.Nuevo;
using Admeli.Componentes;
using Entidad;
using Entidad.Configuracion;
using Modelo;


namespace Admeli.Herramientas.Detalle
{
    public partial class FormDetalleStock : Form
    {

        
        VarianteModel varianteModel = new VarianteModel(); 
        List<CombinacionStock> list { get; set; }

        ProductoData productoData { get; set; }

        TextBox txt { get; set; }
        public FormDetalleStock( List<CombinacionStock> list , ProductoData productoData)
        {
            InitializeComponent();
            this.list = list;
            this.productoData = productoData;
        }


        public FormDetalleStock(Compra currentCompra)
        {
            InitializeComponent();
         
           
        }

    
        #region ================================ Root Load ================================

        private void FormNotaSalidaNew_Load(object sender, EventArgs e)
        {
            reLoad();
        }
        private void reLoad()
        {
            cargarCombinaciones();
            cargarDetalles();
            cargarPrecioVenta();
            darFormatoDecimales();
        }





        #endregion

        #region ============================== Load ==============================

        private void darFormatoDecimales()
        {

            dgvCombinacion.Columns["PrecioVentaTotal"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;
            dgvCombinacion.Columns["stock"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;
            dgvCombinacion.Columns["precio"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;

            dgvCombinacion.Columns["PrecioVentaTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCombinacion.Columns["stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCombinacion.Columns["precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }
        private  void cargarCombinaciones()
        {

            try
            {

                cargarPrecioVenta();

                combinacionStockBindingSource.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Nota Salida Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void cargarDetalles()
        {

            try
            {
                lbProducto.Text += ": " + productoData.nombreProducto + " - " + productoData.almacen; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void cargarPrecioVenta()
        {

            try
            {
                foreach( CombinacionStock combinacionStock in list)
                {

                    combinacionStock.precioVenta = productoData.precioVenta;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Nota Salida Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        #endregion






        private void dgvNotaSalida_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCombinacion.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int index = dgvCombinacion.CurrentRow.Index; // Identificando la fila actual del datagridview
            dgvCombinacion.ReadOnly = false;
            foreach (DataGridViewRow R in dgvCombinacion.Rows)
            {

                R.ReadOnly = true;
            }
            dgvCombinacion.Rows[index].ReadOnly = false;
            dgvCombinacion.Rows[index].Cells[1].ReadOnly = true;
            dgvCombinacion.Rows[index].Cells[2].ReadOnly = true;
            dgvCombinacion.Rows[index].Cells[3].ReadOnly = false;
            dgvCombinacion.Rows[index].Cells[4].ReadOnly = false;           
        }

        private void entrarGuiaremision()
        {
            

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private async  void btnAceptar_Click(object sender, EventArgs e)
        {
            Bloqueo.bloquear(this, true);
            try
            {
                BindingSource bindingSource = dgvCombinacion.DataSource as BindingSource;
                list = bindingSource.DataSource as List<CombinacionStock>;
                CombinacioneGuaradar combinacioneGuaradar = new CombinacioneGuaradar();
                combinacioneGuaradar.datos = list;
                combinacioneGuaradar.idAlmacen = productoData.idAlmacen;
                combinacioneGuaradar.idProducto = productoData.idProducto;
                ResponseStock response = await varianteModel.modificarStockCombinacion(combinacioneGuaradar);
                MessageBox.Show("Mensaje: " + response.msj, "Stock Combinaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Bloqueo.bloquear(this, false);
                this.Close();
            }

            catch ( Exception ex)
            {

                MessageBox.Show("Mensaje: " + ex.Message, "Stock Combinaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Bloqueo.bloquear(this, false);
            }

          
        }

        private void dgvCombinacion_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvCombinacion.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dgvCombinacion.CurrentCell.ColumnIndex ==3 || dgvCombinacion.CurrentCell.ColumnIndex ==4 )
            {
                txt = e.Control as TextBox;
                if (txt != null)
                {
                    txt.KeyPress -= new KeyPressEventHandler(dataGridview_KeyPress);
                    txt.KeyPress += new KeyPressEventHandler(dataGridview_KeyPress);
                }
            }
        }

        private void dataGridview_KeyPress(object sender, KeyPressEventArgs e)
        {
            string texto = txt.Text;

            Validator.isDecimal(e, texto);

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }




    
}
