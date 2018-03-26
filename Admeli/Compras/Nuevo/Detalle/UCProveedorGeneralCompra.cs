﻿using System;
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

namespace Admeli.Compras.Nuevo.Detalle
{
    public partial class UCProveedorGeneralCompra : UserControl
    {
        private FormCompraNuevo1 formCompraNuevo1;
        private LocationModel locationModel = new LocationModel();
        private ProveedorModel proveedorModel = new ProveedorModel();

        private List<LabelUbicacion> labelUbicaciones { get; set; }
        private UbicacionGeografica ubicacionGeografica { get; set; }
        private SunatModel sunatModel=new SunatModel();
        private bool bandera;
        private DataSunat dataSunat;
        private RespuestaSunat respuestaSunat;

        public UCProveedorGeneralCompra()
        {
            InitializeComponent();
        }

       
        public UCProveedorGeneralCompra(FormCompraNuevo1 formCompraNuevo1)
        {
            InitializeComponent();
            this.formCompraNuevo1 = formCompraNuevo1;
        }
        private void UCProveedorGeneral_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        #region ================================= Loads =================================
        private void cargarDatosModificar()
        {
            if (!formCompraNuevo1.nuevo)
            {
                textActividadPrincipal.Text = formCompraNuevo1.currentProveedor.actividadPrincipal;
                textDireccion.Text = formCompraNuevo1.currentProveedor.direccion;
                textEmail.Text = formCompraNuevo1.currentProveedor.email;
                chkEstado.Checked = Convert.ToBoolean(formCompraNuevo1.currentProveedor.estado);
                textNombreEmpresa.Text = formCompraNuevo1.currentProveedor.razonSocial;
                textNIdentificacion.Text = formCompraNuevo1.currentProveedor.ruc;
                textTelefono.Text = formCompraNuevo1.currentProveedor.telefono;
                cbxTipoProveedor.Text = formCompraNuevo1.currentProveedor.tipoProveedor;
            }
        }

        internal async void reLoad()
        {
            await cargarPaises();
            crearNivelesPais();
            cargarDatosModificar();
        }

        private async Task cargarPaises()
        {
            // cargando los paises
            paisBindingSource.DataSource = await locationModel.paises();

            // cargando la ubicacion geografica por defecto
            if (formCompraNuevo1.nuevo)
            {
                ubicacionGeografica = await locationModel.ubigeoActual(ConfigModel.sucursal.idUbicacionGeografica);
            }
            else
            {
                ubicacionGeografica = await locationModel.ubigeoActual(formCompraNuevo1.currentProveedor.idUbicacionGeografica);
            }
            cbxPaises.SelectedValue = ubicacionGeografica.idPais;
        } 
        #endregion


        #region ================== Formando los niveles de cada pais ==================
        private async void crearNivelesPais()
        {
            try
            {
                formCompraNuevo1.loadStateApp(true);
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
            finally
            {
                formCompraNuevo1.loadStateApp(false);
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
                formCompraNuevo1.loadStateApp(true);
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
                formCompraNuevo1.loadStateApp(false);
                desactivarNivelDesde(2);
            }
        }

        private async void cargarNivel2()
        {
            try
            {
                if (labelUbicaciones.Count < 2) return;
                formCompraNuevo1.loadStateApp(true);
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
                formCompraNuevo1.loadStateApp(false);
            }
        }

        private async void cargarNivel3()
        {
            try
            {
                if (labelUbicaciones.Count < 3) return;
                formCompraNuevo1.loadStateApp(true);
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
                formCompraNuevo1.loadStateApp(false);
                desactivarNivelDesde(4);
            }
        }
        private async void cargarNivel4()
        {
            try
            {
                if (labelUbicaciones.Count < 4) return;
                formCompraNuevo1.loadStateApp(true);

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
                formCompraNuevo1.loadStateApp(false);
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
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            guardarSucursal();
        }

        private async void guardarSucursal()
        {
            if (!validarCampos()) return;
            try
            {
                crearObjetoSucursal();
                if (formCompraNuevo1.nuevo)
                {
                    Response response = await proveedorModel.guardar(ubicacionGeografica, formCompraNuevo1.currentProveedor);
                    MessageBox.Show(response.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Response response = await proveedorModel.modificar(ubicacionGeografica, formCompraNuevo1.currentProveedor);
                    MessageBox.Show(response.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.formCompraNuevo1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void crearObjetoSucursal()
        {
            formCompraNuevo1.currentProveedor = new Proveedor();

            if (!formCompraNuevo1.nuevo) formCompraNuevo1.currentProveedor.idProveedor = formCompraNuevo1.currentIDProveedor; // Llenar el id categoria cuando este en esdo modificar

            formCompraNuevo1.currentProveedor.actividadPrincipal = textActividadPrincipal.Text;
            formCompraNuevo1.currentProveedor.direccion = textDireccion.Text;
            formCompraNuevo1.currentProveedor.email = textEmail.Text;
            formCompraNuevo1.currentProveedor.estado = Convert.ToInt32(chkEstado.Checked);
            formCompraNuevo1.currentProveedor.razonSocial = textNombreEmpresa.Text;
            formCompraNuevo1.currentProveedor.ruc = textNIdentificacion.Text;
            formCompraNuevo1.currentProveedor.telefono = textTelefono.Text;
            formCompraNuevo1.currentProveedor.tipoProveedor = cbxTipoProveedor.Text;

            // Ubicacion geografica
            ubicacionGeografica.idPais = (cbxPaises.SelectedIndex == -1) ? ubicacionGeografica.idPais : Convert.ToInt32(cbxPaises.SelectedValue);
            ubicacionGeografica.idNivel1 = (cbxNivel1.SelectedIndex == -1) ? ubicacionGeografica.idNivel1 : Convert.ToInt32(cbxNivel1.SelectedValue);
            ubicacionGeografica.idNivel2 = (cbxNivel2.SelectedIndex == -1) ? ubicacionGeografica.idNivel2 : Convert.ToInt32(cbxNivel2.SelectedValue);
            ubicacionGeografica.idNivel3 = (cbxNivel3.SelectedIndex == -1) ? ubicacionGeografica.idNivel3 : Convert.ToInt32(cbxNivel3.SelectedValue);
        }

        private bool validarCampos()
        {
            if (textNombreEmpresa.Text == "")
            {
                errorProvider1.SetError(textNombreEmpresa, "Este campo esta bacía");
                textNombreEmpresa.Focus();
                return false;
            }
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

            if (textNIdentificacion.Text == "")
            {
                errorProvider1.SetError(textNIdentificacion, "Este campo esta bacía");
                textNIdentificacion.Focus();
                return false;
            }
            errorProvider1.Clear();

            if (cbxTipoProveedor.SelectedIndex == -1)
            {
                errorProvider1.SetError(cbxTipoProveedor, "Elija almenos uno");
                cbxTipoProveedor.Focus();
                return false;
            }
            errorProvider1.Clear();

            if (!Validator.IsValidEmail(textEmail.Text) && textEmail.Text != "")
            {
                errorProvider1.SetError(textEmail, "Dirección de correo electrónico inválido");
                textEmail.Focus();
                return false;
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
            this.formCompraNuevo1.Close();
        }
        #endregion

        private void textTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isNumber(e);
        }

        // TAREA hacer los cambios en todos los formularios de clientes y proveedores ver lo de paises 
        private async void textNIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            String aux = textNIdentificacion.Text;

            int nroCarateres=aux.Length;

            if(nroCarateres==11 || nroCarateres==8)
                if ((int)e.KeyChar == (int)Keys.Enter)
                {

                    try
                    {
                        
                        respuestaSunat = await sunatModel.obtenerDatos(aux);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "consulta sunat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                   // Ver(aux);
                    
                }
            if (respuestaSunat != null)
            {
                
                dataSunat = respuestaSunat.result;
                textNIdentificacion.Text = dataSunat.RUC;
                textTelefono.Text = dataSunat.Telefono.Substring(1, dataSunat.Telefono.Length-1);
                textNombreEmpresa.Text = dataSunat.RazonSocial;
                textActividadPrincipal.Text = dataSunat.Oficio;
                

                textDireccion.Text = concidencias(dataSunat.Direccion);
                //cbxPaises.Text = concidencias(dataSunat.Pais);


                respuestaSunat = null;
            
            }
           

        }
        //metodo para cargar la coleccion de datos para el autocomplete
        

        //private string concidenciapais(string pais)
        //{
        //    int lenght = pais.Length;
        //    int i1 = pais.LastIndexOf('-');

        //    string ff = pais.Substring(0, i1);
        //    i1 = ff.LastIndexOf('-');
        //    string ff1 = ff.Substring(0, i1);

            
        //    i1 = ff1.LastIndexOf(' ');
        //    string hhh = ff1.Substring(0, i1);
        //    i1 = hhh.LastIndexOf(' ');
        //    string hh1 = hhh.Substring(0, i1);
        //    return hh1;
        //}

        private string concidencias(string direccion)
        {
            int lenght = direccion.Length;
            if (lenght > 20)
            {
                int i = direccion.LastIndexOf('-');


                string ff = direccion.Substring(0, i);
                i = ff.LastIndexOf('-');
                string ff1 = ff.Substring(0, i);


                i = ff1.LastIndexOf(' ');
                string hhh = ff1.Substring(0, i);
                i = hhh.LastIndexOf(' ');
                string hh1 = hhh.Substring(0, i);
                return hh1;
            }
            else
                return "";



        }
        public async void Ver(string aux)
        {
            try
            {
                respuestaSunat = await sunatModel.obtenerDatos(aux);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "consulta sunat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        
    }
}