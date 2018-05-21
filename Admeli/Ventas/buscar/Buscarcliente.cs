using Entidad;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admeli.Ventas.Buscar
{
    public partial class Buscarcliente : Form
    {
        

        private ClienteModel clienteModel = new ClienteModel();
        private RootObject<Cliente> clienteRoot;
        public Cliente currentCliente { get; set; }
        public List<Cliente> clientes { get; set; }

        public Buscarcliente()
        {
            InitializeComponent();
        }

        #region==============Root Load=============
        private void BuscarProveedor_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        private void reLoad()
        {
            cargarClienteLike();
        }
        #endregion==============Root Load=============
        #region==============Load=============
        private async void cargarClienteLike()
        {
            loadState(true);
            try
            {
                
                if (textBuscar.Text == "")
                {
                    clienteRoot = await clienteModel.clientes(1, 2000);
                }
                else
                {
                    clienteRoot = await clienteModel.buscarClientesLike(textBuscar.Text,1, 2000);
                }              
                clientes = clienteRoot.datos;
               clienteBindingSource1.DataSource = clientes;
                dataGridView.Refresh();
               //  mostrarPaginado();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar Clientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadState(false);
            }
        }

        private void loadState(bool state)
        {
            if (state)
            {
                Cursor.Current = Cursors.WaitCursor;
                progressBarApp.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                Cursor.Current = Cursors.Default;
                progressBarApp.Style = ProgressBarStyle.Blocks;
            }
        }
        #endregion==============Load=============


        #region==============eventos=============
        private void textBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cargarClienteLike();
            }
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            seleccionarCliente();
        }
        private void seleccionarCliente()
        {
            if(dataGridView.Rows.Count == 0) return;
            try
            {
                int index = dataGridView.CurrentRow.Index; // Identificando la fila actual del datagridview
                int idProveedor = Convert.ToInt32(dataGridView.Rows[index].Cells[0].Value); // obteniedo el idCategoria del datagridview
                currentCliente = clientes.Find(x => x.idCliente == idProveedor); // Buscando la registro especifico en la lista de registros
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message, "Error cliente", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }



        #endregion==============eventos=============

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            seleccionarCliente();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
