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


namespace Admeli.Ventas.buscar
{
    public partial class FormBuscarProducto : Form
    {


        // servicios necesarios

        

        // objetos que cargan a un inicio

       
        private List<ProductoVenta> listProductos { get; set; }
       
        public ProductoVenta currentProducto { get; set; }

        private List<ProductoVenta> listProductosfiltrada { get; set; }



        public FormBuscarProducto(List<ProductoVenta> listProductos)
        {
            InitializeComponent();
            
            this.listProductos = listProductos;
        }


       

     
        #region ================================ Root Load ================================

        private void FormNotaSalidaNew_Load(object sender, EventArgs e)
        {
            reLoad();

        }
        private void reLoad()
        {
            cargarNotaSalidaDetalle();


        }





        #endregion

        #region ============================== Load ==============================




        private  void cargarNotaSalidaDetalle()
        {
            loadState(true);
            try
            {
               
                productoVentaBindingSource.DataSource = listProductos;
                listProductosfiltrada = listProductos;
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

       
       

        
        

      
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            cargarProducto();
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtMotivo_KeyUp(object sender, KeyEventArgs e)
        {

           

            string textBuscar = txtMotivo.Text;
            if (textBuscar.Length == 0) return;
            string[] list = textBuscar.Split(' ');
            listProductosfiltrada = new List<ProductoVenta>();

            List<List<ProductoVenta>> matrizProducto = new List<List<ProductoVenta>>();


            for (int i = 0; i < list.Count(); i++)
            {
                string palabra = list[i];
                if (palabra != "")
                {
                    List<ProductoVenta> listventa = listProductos.Where(obj => obj.nombreProducto.ToUpper().Contains(palabra.Trim().ToUpper())).ToList();
                    matrizProducto.Add(listventa);
                }
            }

            List<ProductoVenta> listventa2 = matrizProducto[0];
            foreach (List<ProductoVenta> IN in matrizProducto)
            {

                listventa2 = listventa2.Intersect(IN).ToList();
            }
            listProductosfiltrada = listventa2;
            BindingList<ProductoVenta> filtered = new BindingList<ProductoVenta>(listProductosfiltrada);
            productoVentaBindingSource.DataSource = filtered;
            dgvProductos.Update();
        }

        private void Filtrar(string val)
    {
       //
       // Usamos una consulta Linq para filtrar los valores de la lista usando la
       // función StartWith enviandole el valor ingresado en el control TextBox.
      // 
     // where l.Nombre.ToLower() ToLower() se encarga de convertir los valores de la propiedad Nombre a minusculas
      //.StartsWith(val.ToLower()) el valor contenido en el parametro val lo convertimos a minuscula para
     // estandarizar la busqueda
      //
     var query = from l in listProductos where l.nombreProducto.ToLower().StartsWith(val.ToLower()) select l;
            //
            //
            // Establecemos la propiedad DataSource del DGV usando el resultado de la consulta Linq
            //
            dgvProductos.DataSource = query.ToList();
            //
           
   }



    private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            cargarProducto();
        }


        public void cargarProducto()
        {
            if (dgvProductos.Rows.Count == 0) return;
            try
            {
                int index = dgvProductos.CurrentRow.Index; // Identificando la fila actual del datagridview
                int idpresentacion = Convert.ToInt32(dgvProductos.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview
                currentProducto = listProductosfiltrada.Find(x => x.idPresentacion == idpresentacion); // Buscando la registro especifico en la lista de registros
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message, "Error producto", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMotivo_OnValueChanged(object sender, EventArgs e)
        {

        }
    }
}
