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


namespace Admeli.Productos.Detalle
{
    public partial class FormDetalleStock : Form
    {

        
        VarianteModel varianteModel = new VarianteModel(); 
        List<CombinacionStock> list { get; set; }

        Producto producto { get; set; }

        TextBox txt { get; set; }
        public FormDetalleStock( List<CombinacionStock> list , Producto producto)
        {
            InitializeComponent();
            this.list = list;
            this.producto = producto;
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
                lbProducto.Text += ": " + producto.nombreProducto + " - " + producto.nombreAlmacen; 
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

                    combinacionStock.precioVenta = producto.precioVenta;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Nota Salida Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        #endregion






     
        

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }




    
}
