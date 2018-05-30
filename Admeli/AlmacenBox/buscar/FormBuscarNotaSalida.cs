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


namespace Admeli.AlmacenBox.buscar
{
    public partial class FormBuscarNotaSalida : Form
    {


        // servicios necesarios

        AlmacenModel AlmacenModel = new AlmacenModel();
        ProductoModel productoModel = new ProductoModel();
        FechaModel fechaModel = new FechaModel();
        NotaSalidaModel NotaSalidaModel = new NotaSalidaModel();

        // objetos que cargan a un inicio

        private List<NotaSalidaR> listNotasalida { get; set; }

        // entidadades auxiliares

        private bool nuevo { get; set; }
        private string formato { get; set; }
        private int nroDecimales = 2;
        private FechaSistema fechaSistema { get; set; }



        //objetos en tiempo real
        public  NotaSalidaR currentNotaSalida { get; set; }
        FormRemisionNew formRemisionNew { get; set; }

        public FormBuscarNotaSalida()
        {
            InitializeComponent();
            this.nuevo = true;
            formato = "{0:n" + nroDecimales + "}";
           
        }
        public FormBuscarNotaSalida(FormRemisionNew formRemisionNew)
        {
            InitializeComponent();
            this.nuevo = true;
            formato = "{0:n" + nroDecimales + "}";
            this.formRemisionNew = formRemisionNew;
        }

        public FormBuscarNotaSalida(Compra currentCompra)
        {
            InitializeComponent();
            this.nuevo = false;
            formato = "{0:n" + nroDecimales + "}";
           
        }

        #region=======================metodos de apoyo
        private string darformato(object dato)
        {
            return string.Format(CultureInfo.GetCultureInfo("en-US"), this.formato, dato);
        }
        #endregion
        #region ================================ Root Load ================================

        private void FormNotaSalidaNew_Load(object sender, EventArgs e)
        {
            cargarNotaSalida();

        }
        private void reLoad()
        {
            
        }

        #endregion


        #region=========================estados=================  
        private void loadState(bool state)
        {
            appLoadState(state);
            this.Enabled = !state;
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


        #region ============================== Load ==============================


        private async void cargarNotaSalida()
        {
            loadState(true);
            try
            {
                listNotasalida = await NotaSalidaModel.nSalida(ConfigModel.currentIdAlmacen);

                notaSalidaRBindingSource.DataSource = listNotasalida;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Cargar Nota Salida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                loadState(false);
            }

        }
        #endregion

       

        private void btnfecha_Click(object sender, EventArgs e)
        {
           FormFecha fecha = new FormFecha();
           fecha.ShowDialog();
           BindingList<NotaSalidaR> filtered = new BindingList<NotaSalidaR>(listNotasalida.Where(obj => obj.RFecha>=fecha.desde && obj.RFecha <= fecha.hasta).ToList());
           notaSalidaRBindingSource.DataSource = filtered;
           dgvNotaSalida.Update();
        }

        private void btnfechasalida_Click(object sender, EventArgs e)
        {
            FormFecha fecha = new FormFecha();
            fecha.ShowDialog();
            BindingList<NotaSalidaR> filtered = new BindingList<NotaSalidaR>(listNotasalida.Where(obj => obj.RFechaSalida >= fecha.desde && obj.RFechaSalida <= fecha.hasta).ToList());
            notaSalidaRBindingSource.DataSource = filtered;
            dgvNotaSalida.Update();
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<NotaSalidaR> listNotasalidaNoNull = listNotasalida.Where(obj=>obj.rucDni!=null || obj.nombreCliente!=null).ToList();
                BindingList<NotaSalidaR> filtered = new BindingList<NotaSalidaR>(listNotasalidaNoNull.Where(obj => obj.rucDni.Contains(txtCliente.Text.Trim()) || obj.nombreCliente.ToUpper().Contains(txtCliente.Text.ToUpper())).ToList());
                notaSalidaRBindingSource.DataSource = filtered;
                dgvNotaSalida.Update();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtMotivo_TextChanged(object sender, EventArgs e)
        {
            BindingList<NotaSalidaR> filtered = new BindingList<NotaSalidaR>(listNotasalida.Where(obj => obj.descripcion.ToUpper().Contains(txtMotivo.Text.ToUpper().Trim()) || obj.motivo.ToUpper().Contains(txtMotivo.Text.ToUpper())).ToList());
            notaSalidaRBindingSource.DataSource = filtered;
            dgvNotaSalida.Update();
        }

        private void txtSeri_TextChanged(object sender, EventArgs e)
        {
            BindingList<NotaSalidaR> filtered = new BindingList<NotaSalidaR>(listNotasalida.Where(obj => obj.serie.Contains(txtSeri.Text.Trim()) || obj.correlativo.Contains(txtSeri.Text.Trim())).ToList());
            notaSalidaRBindingSource.DataSource = filtered;
            dgvNotaSalida.Update();
        }

        private void txtDestino_TextChanged(object sender, EventArgs e)
        {
            BindingList<NotaSalidaR> filtered = new BindingList<NotaSalidaR>(listNotasalida.Where(obj => obj.destino.Contains(txtDestino.Text.Trim() )).ToList());
            notaSalidaRBindingSource.DataSource = filtered;
            dgvNotaSalida.Update();
        }

        private void dgvNotaSalida_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            entrarGuiaremision();

        }

        private void entrarGuiaremision()
        {
            if (dgvNotaSalida.Rows.Count == 0)
            {
                MessageBox.Show("No hay un registro seleccionado", "nota salida", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int index = dgvNotaSalida.CurrentRow.Index; // Identificando la fila actual del datagridview
            int idNotaSalida = Convert.ToInt32(dgvNotaSalida.Rows[index].Cells[2].Value); // obteniedo el idRegistro del datagridview

            currentNotaSalida = listNotasalida.Find(x => x.idNotaSalida == idNotaSalida);
            if (formRemisionNew == null)
            {
                FormRemisionNew formRemisionNew = new FormRemisionNew(currentNotaSalida);
                formRemisionNew.ShowDialog();
                this.Close();
                this.FindForm();
            }
            else
            {
                this.Close();

            }
       


        }


    }
}
