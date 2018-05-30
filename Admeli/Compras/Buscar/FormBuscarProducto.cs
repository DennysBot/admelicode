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


namespace Admeli.Productos.buscar
{
    public partial class FormBuscarProducto : Form
    {


        // servicios necesarios

        

        // objetos que cargan a un inicio

       
        private List<Producto> listProductos { get; set; }
       
        public Producto currentProducto { get; set; }

        private List<Producto> listProductosfiltrada { get; set; }

        private bool lisenerKeyEvents = true;

        public FormBuscarProducto(List<Producto> listProductos)
        {
            InitializeComponent();
            
            this.listProductos = listProductos;
        }


       

     
        #region ================================ Root Load ================================

        private void FormNotaSalidaNew_Load(object sender, EventArgs e)
        {
            reLoad();
            this.ParentChanged += ParentChange; // Evetno que se dispara cuando el padre cambia // Este eveto se usa para desactivar lisener key events de este modulo
            if (TopLevelControl is Form) // Escuchando los eventos del formulario padre
            {
                (TopLevelControl as Form).KeyPreview = true;
                TopLevelControl.KeyUp += TopLevelControl_KeyUp;
            }

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
                    txtMotivo.Focus();
                    break;


            }




        }
        #endregion





        private void btnAceptar_Click(object sender, EventArgs e)
        {
            cargarProducto();
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private string quitarC(string  texto)
        {


            string textoNormalizado = texto.Normalize(NormalizationForm.FormD);
            //coincide todo lo que no sean letras y números ascii o espacio
            //y lo reemplazamos por una cadena vacía.
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
            return reg.Replace(textoNormalizado, "");

        }
        private void txtMotivo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (listProductosfiltrada.Count > 0)
                {

                    cargarProducto();
                }

            }
            if (e.KeyCode == Keys.Down)
            {
                if (listProductosfiltrada.Count >0)
                {
                    dgvProductos.Focus();
                    dgvProductos.ClearSelection();
                    dgvProductos.Rows[0].Selected = true;
                    return;
                }
            }

            string textBuscar = txtMotivo.Text;
            if (textBuscar.Length == 0) return;
            string[] list = textBuscar.Split(' ');
            listProductosfiltrada = new List<Producto>();

            List<List<Producto>> matrizProducto = new List<List<Producto>>();


            for (int i = 0; i < list.Count(); i++)
            {
                string palabra = list[i];
                if (palabra != "")
                {
                    List<Producto> listventa = listProductos.Where(obj =>quitarC( obj.nombreProducto).ToUpper().Contains(quitarC(palabra.Trim()).ToUpper())).ToList();
                    matrizProducto.Add(listventa);
                }
            }

            List<Producto> listventa2 = matrizProducto[0];
            foreach (List<Producto> IN in matrizProducto)
            {

                listventa2 = listventa2.Intersect(IN).ToList();
            }
            listProductosfiltrada = listventa2;
            BindingList<Producto> filtered = new BindingList<Producto>(listProductosfiltrada);
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
                currentProducto = listProductosfiltrada.Find(x => x.idPresentacion == idpresentacion.ToString()); // Buscando la registro especifico en la lista de registros
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

        private void dgvProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                e.SuppressKeyPress = true;
                cargarProducto();
            }
        }
    }
}
