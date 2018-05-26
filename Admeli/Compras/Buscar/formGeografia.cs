using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;
using Entidad.Location;
using Entidad;
using Admeli.Componentes;
using Admeli.Compras.Nuevo;



namespace Admeli.Compras.buscar
{
    public partial class formGeografia : Form
    {
       

        private LocationModel locationModel = new LocationModel();
        private ProveedorModel proveedorModel = new ProveedorModel();

        private List<LabelUbicacion> labelUbicaciones { get; set; }
        public  UbicacionGeografica ubicacionGeografica { get; set; }
        private SunatModel sunatModel=new SunatModel();
        private bool bandera;
   
        private FormProveedorNuevo formProveedorNuevo;
        public int idUbicacionGeografia { get; set; }
        
        public string cadena = "";
        public formGeografia()
        {
            InitializeComponent();

            
        }

        public formGeografia(FormProveedorNuevo formProveedorNuevo)
        {
            InitializeComponent();
            this.formProveedorNuevo = formProveedorNuevo;
        }

        private void UCProveedorGeneral_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        #region ================================= Loads =================================
        

        internal async void reLoad()
        {
            try
            {
                await cargarPaises();
                crearNivelesPais();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "reload Cargar Paises", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private async Task cargarPaises()
        {
            try
            {
                // cargando los paises
                paisBindingSource.DataSource = await locationModel.paises();

                // cargando la ubicacion geografica por defecto
                ubicacionGeografica = await locationModel.ubigeoActual(ConfigModel.sucursal.idUbicacionGeografica);
                cbxPaises.SelectedValue = ubicacionGeografica.idPais;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "cargar paises", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        } 
        #endregion


        #region ================== Formando los niveles de cada pais ==================
        private async void crearNivelesPais()
        {
            try
            {
                
                labelUbicaciones = await locationModel.labelUbicacion(Convert.ToInt32(cbxPaises.SelectedValue));
                ocultarNiveles(); // Ocultando todo los niveles

                // Mostrando los niveles uno por uno
                if (labelUbicaciones.Count >= 1)
                {
                    lblNivel1.Visible = true;
                    lblNivel1.Text = labelUbicaciones[0].denominacion;

                    cbxNivel1.Visible = true;
                }

                if (labelUbicaciones.Count >= 2)
                {
                    lblNivel2.Visible = true;
                    lblNivel2.Text = labelUbicaciones[1].denominacion;

                    cbxNivel2.Visible = true;
                }

                if (labelUbicaciones.Count >= 3)
                {
                    lblNivel3.Visible = true;
                    lblNivel3.Text = labelUbicaciones[2].denominacion;

                    cbxNivel3.Visible = true;
                }

                // Cargar el primer nivel de la localizacion
                cargarNivel1();

            }
            catch (Exception)
            {
                // MessageBox.Show(ex.Message);
            }
           
        }

        private void ocultarNiveles()
        {
            lblNivel1.Visible = false;
            lblNivel2.Visible = false;
            lblNivel3.Visible = false;

            cbxNivel1.Visible = false;
            cbxNivel2.Visible = false;
            cbxNivel3.Visible = false;

            cbxNivel1.SelectedIndex = -1;
            cbxNivel2.SelectedIndex = -1;
            cbxNivel3.SelectedIndex = -1;
        }

        private async void cargarNivel1()
        {
            try
            {
                // No cargar directo al comobobox esto causara que el evento SelectedIndexChange de forma automatica
                if (labelUbicaciones.Count < 1) return;
               
                nivel1BindingSource.DataSource = await locationModel.nivel1(Convert.ToInt32(cbxPaises.SelectedValue));
                if (ubicacionGeografica.idNivel1 > 0)
                {
                    cbxNivel1.SelectedValue = ubicacionGeografica.idNivel1;
                }
                else
                {
                    cbxNivel1.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upps! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                
                desactivarNivelDesde(2);
            }
        }

        private async void cargarNivel2()
        {
            try
            {
                if (labelUbicaciones.Count < 2) return;
             
                nivel2BindingSource.DataSource = await locationModel.nivel2(Convert.ToInt32(cbxNivel1.SelectedValue));
                if (ubicacionGeografica.idNivel2 > 0)
                {
                    cbxNivel2.SelectedValue = ubicacionGeografica.idNivel2;
                }
                else
                {
                    cbxNivel2.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upps! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                desactivarNivelDesde(3);
                
            }
        }

        private async void cargarNivel3()
        {
            try
            {
                if (labelUbicaciones.Count < 3) return;
               
                nivel3BindingSource.DataSource = await locationModel.nivel3(Convert.ToInt32(cbxNivel2.SelectedValue));
                if (ubicacionGeografica.idNivel3 > 0)
                {
                    cbxNivel3.SelectedValue = ubicacionGeografica.idNivel3;
                }
                else
                {
                    cbxNivel3.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upps! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
               
                desactivarNivelDesde(4);
            }
        }
        private async void cargarNivel4()
        {
            try
            {
                if (labelUbicaciones.Count < 4) return;
              

                /*
                nivel4BindingSource.DataSource = await locationModel.nivel4(Convert.ToInt32(cbxNivel3.SelectedValue));
                cbxNivel4.SelectedIndex = -1;
                 * if (ubicacionGeografica.idNivel4 > 0)
                {
                    cbxNivel4.SelectedValue = ubicacionGeografica.idNivel4;
                }
                else
                {
                    cbxNivel4.SelectedIndex = -1;
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upps! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
               
            }
        }

        private void desactivarNivelDesde(int n)
        {
            cbxNivel1.Enabled = true;
            cbxNivel2.Enabled = true;
            cbxNivel3.Enabled = true;

            if (n < 2) cbxNivel1.Enabled = false;
            if (n < 3) cbxNivel2.Enabled = false;
            if (n < 4) cbxNivel3.Enabled = false;
        }

        #endregion

        private void cbxPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            crearNivelesPais();
        }

        private void cbxNivel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarNivel2();
        }

        private void cbxNivel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarNivel3();
        }

        #region ========================== SAVE AND UPDATE ===========================
        

        

        

        private bool validarCampos()
        {
           
            errorProvider1.Clear();

            switch (labelUbicaciones.Count)
            {
                case 1:
                    if (cbxNivel1.SelectedIndex == -1)
                    {
                        errorProvider1.SetError(cbxNivel1, "No se seleccionó ningún elemento");
                        cbxNivel1.Focus();
                        return false;
                    }
                    errorProvider1.Clear();
                    break;
                case 2:
                    if (cbxNivel2.SelectedIndex == -1)
                    {
                        errorProvider1.SetError(cbxNivel2, "No se seleccionó ningún elemento");
                        cbxNivel2.Focus();
                        return false;
                    }
                    errorProvider1.Clear();
                    break;
                case 3:
                    if (cbxNivel3.SelectedIndex == -1)
                    {
                        errorProvider1.SetError(cbxNivel3, "No se seleccionó ningún elemento");
                        cbxNivel3.Focus();
                        return false;
                    }
                    errorProvider1.Clear();
                    break;
                default:
                    break;
            }

           
            errorProvider1.Clear();


            /*
            if (Validator.)
            {
                errorProvider1.SetError(cbxTipoProveedor, "Elija almenos uno");
                cbxTipoProveedor.Focus();
                return false;
            }
            errorProvider1.Clear();
            */
            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           
        }
        #endregion

        private void textTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        private async  void btnAceptar_Click(object sender, EventArgs e)
        {

            if (cbxNivel1.SelectedIndex == -1)
                return;


                Nivel3 pais3 = null;
            Pais pais  = cbxPaises.Items[cbxPaises.SelectedIndex] as Pais;
            Nivel1 pais1 = cbxNivel1.Items[cbxNivel1.SelectedIndex] as Nivel1;
            Nivel2 pais2 = cbxNivel2.Items[cbxNivel2.SelectedIndex] as Nivel2;
            if (cbxNivel3.SelectedIndex != -1)
            {
               pais3 = cbxNivel3.Items[cbxNivel3.SelectedIndex] as Nivel3;

            }
            
            cadena = "";
            if (pais != null)
            {

                cadena += pais.nombre+" - ";
                ubicacionGeografica.idPais = pais.idPais;
            }
            if (pais1 != null)
            {

                cadena += pais1.nombre+" - ";
                ubicacionGeografica.idNivel1 = pais1.idNivel1;
            }
            else
            {
                ubicacionGeografica.idNivel1 = 0;
            }
            if (pais2 != null )
            {

                cadena += pais2.nombre + " - ";
                ubicacionGeografica.idNivel2 = pais2.idNivel2;

            }
            else

            {
                ubicacionGeografica.idNivel2 = 0;
            }
            if (pais3 != null)
            {

                cadena += pais3.nombre;
                ubicacionGeografica.idNivel3= pais3.idNivel3;
            }
            else
            {

                cadena = cadena.Substring(0, cadena.Length - 3);
                ubicacionGeografica.idNivel3 = 0;
            }
            Response respuesta=   await locationModel.guardarUbigeo(ubicacionGeografica);
            idUbicacionGeografia = respuesta.id;

            this.Close();

        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
