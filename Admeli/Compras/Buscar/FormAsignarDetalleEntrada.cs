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
using Admeli.AlmacenBox.fecha;
using Admeli.AlmacenBox.Nuevo;
using Admeli.Componentes;
using Entidad;
using Entidad.Configuracion;
using Modelo;


namespace Admeli.Compras.buscar
{
    public partial class FormAsignarDetalleEntrada : Form
    {


        // servicios necesarios



        // objetos que cargan a un inicio



        public List<DatoNotaEntradaC> list { get; set; }
        TextBox txt;
        string stockActual = "0";
        public  bool cancelar = false;
        public FormAsignarDetalleEntrada(List<DatoNotaEntradaC> list)
        {
            InitializeComponent();
            
            this.list = list;
        }


       

     
        #region ================================ Root Load ================================

        private void FormNotaSalidaNew_Load(object sender, EventArgs e)
        {
            reLoad();

        }
        private void reLoad()
        {
            cargarNotaSalidaDetalle();

            darFormatoDecimales();
        }





        #endregion

        #region ============================== Load ==============================




        private  void cargarNotaSalidaDetalle()
        {
            loadState(true);
            try
            {
               
                datoNotaEntradaCBindingSource.DataSource = list;

                foreach (DatoNotaEntradaC nota in list)
                {

                    nota.cantidad = (int)nota.stock;
                }

             
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Nota Salida Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {

                loadState(false);

            }

        }


        private void darFormatoDecimales()
        {
           
            dgvProductos.Columns["stockGuardar"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;
            dgvProductos.Columns["stock"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;
            dgvProductos.Columns["stockCompraRestante"].DefaultCellStyle.Format = ConfigModel.configuracionGeneral.formatoDecimales;

        }
        #endregion


        #region=========================estados=================  
        private void loadState(bool state)
        {
            appLoadState(state);
            this.Enabled = true;
        }

        public void appLoadState(bool state)
        {
            if (state)
            {
                progressStatus.Style = ProgressBarStyle.Marquee;
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                progressStatus.Style = ProgressBarStyle.Blocks;
                this.Cursor = Cursors.Default;
            }
        }
        #endregion=========================estados=====================




        private string quitarC(string texto)
        {


            string textoNormalizado = texto.Normalize(NormalizationForm.FormD);
            //coincide todo lo que no sean letras y números ascii o espacio
            //y lo reemplazamos por una cadena vacía.
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
            return reg.Replace(textoNormalizado, "");

        }



        private void btnAceptar_Click(object sender, EventArgs e)
        {
            cargarProducto();
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {




        }

        private void txtMotivo_KeyUp(object sender, KeyEventArgs e)
        {

           


           
        }

    


        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            seleccionarProducto();
        }


        public void seleccionarProducto()
        {
            if (dgvProductos.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int index = dgvProductos.CurrentRow.Index; // Identificando la fila actual del datagridview
            dgvProductos.ReadOnly = false;
            foreach (DataGridViewRow R in dgvProductos.Rows)
            {

                R.ReadOnly = true;
            }
            dgvProductos.Rows[index].ReadOnly = false;

            dgvProductos.Rows[index].Cells[0].ReadOnly = true;
            dgvProductos.Rows[index].Cells[1].ReadOnly = true;
            dgvProductos.Rows[index].Cells[2].ReadOnly = true;
            dgvProductos.Rows[index].Cells[3].ReadOnly = true;
            dgvProductos.Rows[index].Cells[4].ReadOnly = true;
            dgvProductos.Rows[index].Cells["stock"].ReadOnly = false;
          
            dgvProductos.Rows[index].Cells[6].ReadOnly = true;
            dgvProductos.Rows[index].Cells[7].ReadOnly = false;


            //stockActual = dgvProductos.Rows[dgvProductos.CurrentCell.RowIndex].Cells["stockTotal"].Value.ToString();

        }

        private void cargarProducto()
        {

            Bloqueo.bloquear(this, true);
            try
            {
                //BindingSource bindingSource = dgvProductos.DataSource as BindingSource;
                //list = bindingSource.DataSource as List<DatosNotaSalidaVenta>;
                this.Close();
            }

            catch (Exception ex)
            {

                MessageBox.Show("Mensaje: " + ex.Message, "Stock Combinaciones", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                Bloqueo.bloquear(this, false);

            }
        }
        private void btnsalir_Click(object sender, EventArgs e)
        {

            this.cancelar = true;
            this.Close();
        }

        private void txtMotivo_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvProductos_Enter(object sender, EventArgs e)
        {

        }

        private void FormBuscarProducto_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void FormBuscarProducto_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void FormBuscarProducto_Enter(object sender, EventArgs e)
        {
          
        }

        private void dgvProductos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvProductos.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        
            if (dgvProductos.CurrentCell.ColumnIndex == 5)
            {
               
                txt = e.Control as TextBox;
                if (txt != null)
                {
                    txt.KeyPress -= new KeyPressEventHandler(dataGridview_KeyPress);
                    txt.KeyPress += new KeyPressEventHandler(dataGridview_KeyPress);
                    txt.TextChanged-= new EventHandler(dataGridview_TextChanged);
                    txt.TextChanged += new EventHandler(dataGridview_TextChanged);
                }
            }
        }
        private void dataGridview_KeyPress(object sender, KeyPressEventArgs e)
        {
            string texto = txt.Text;

            Validator.isDecimal(e, texto);

        }

        public  void dataGridview_TextChanged(object sender, EventArgs e)
        {


            int index = dgvProductos.CurrentCell.RowIndex;
            if (dgvProductos.CurrentCell.ColumnIndex != 5) return;
            string dato = txt.Text;
            if (dato == "") return;

            int idPresentacion = Convert.ToInt32(dgvProductos.Rows[index].Cells["idPresentacion"].Value);
            int idCombinacion = Convert.ToInt32(dgvProductos.Rows[index].Cells["idCombinacionAlternativa"].Value);
            int idAlmacen = Convert.ToInt32(dgvProductos.Rows[index].Cells["idAlmacen"].Value);
            DatoNotaEntradaC datosNota = list.Find(X => X.idPresentacion == idPresentacion && X.idCombinacionAlternativa == idCombinacion && X.idAlmacen == idAlmacen);
            decimal datoOriginal = datosNota.stockTotal;

            decimal datoDatagriw = Decimal.Parse(dato);
            decimal stockResultante = datoOriginal + datoDatagriw;


            datosNota.stockGuardar = stockResultante;
            datosNota.stock = datoDatagriw;
            datosNota.cantidad = Convert.ToInt32(datoDatagriw);



            List<DatoNotaEntradaC> listAux = list.Where(X => X.idPresentacion == idPresentacion && X.idCombinacionAlternativa == idCombinacion).ToList();

            decimal suma = listAux.Sum(X => X.stock);
            decimal cantidadRestante = listAux[0].stockCompra - suma;



            foreach (DatoNotaEntradaC NS in listAux)
            {
                if (cantidadRestante >= 0)
                {
                   
                    NS.stockCompraRestante = cantidadRestante;
                }
                else
                {
                    
                    NS.stockCompraRestante = 0;
                    if (NS.idAlmacen == idAlmacen)
                    {
                        decimal stockResultante1 = datoDatagriw + cantidadRestante;


                        decimal stockResultante2 = datoOriginal + stockResultante1;


                        NS.stockGuardar = stockResultante2;
                        NS.cantidad = (int)stockResultante1;
                        txt.Text = stockResultante1.ToString();
                        txt.Select(); ;
                        NS.stock = stockResultante1;
                       

                    }
                   
                }
            

            }



            
            datoNotaEntradaCBindingSource.DataSource = list;
            dgvProductos.Refresh();
            



        }

        private void dgvProductos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText =
        dgvProductos.Columns[e.ColumnIndex].HeaderText;

            // Abort validation if cell is not in the CompanyName column.
            if (!headerText.Equals("Cantidad")) return;

            // Confirm that the cell is not empty.
            if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                dgvProductos.Rows[e.RowIndex].ErrorText =
                    " celda vacia - cantidad ";
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;

            }
        }
    }
}
