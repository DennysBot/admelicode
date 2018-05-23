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
using Newtonsoft.Json;

namespace Admeli.Ventas.Nuevo.Detalle
{
    public partial class UCClienteGeneral : UserControl
    {
        private FormClienteNuevo formClienteNuevo;

        private LocationModel locationModel = new LocationModel();
        private ProveedorModel proveedorModel = new ProveedorModel();
        private ClienteModel clienteModel = new ClienteModel();

        private DocumentoIdentificacionModel documentoIdentificacionModel = new DocumentoIdentificacionModel();
        private List<LabelUbicacion> labelUbicaciones { get; set; }
        private UbicacionGeografica ubicacionGeografica { get; set; }
        private SunatModel sunatModel = new SunatModel();       
        private DataSunat dataSunat;
        private RespuestaSunat respuestaSunat;
        public List<GrupoCliente> grupoClientes;      
        private List<DocumentoIdentificacion> documentoIdentificaciones;
        private string NroDocumento = "";
        public bool lisenerKeyEvents { get; internal set; }

        public Response rest { get; set; }
        public int nroMaximoCaracteres = 0;
        public int idTipoDocumento = 0;

        #region====================Construtor=============== 
        public UCClienteGeneral()
        {
            InitializeComponent();
        }

        public UCClienteGeneral(FormClienteNuevo formClienteNuevo)
        {
            InitializeComponent();
            this.formClienteNuevo = formClienteNuevo;

        }
        public UCClienteGeneral(FormClienteNuevo formClienteNuevo ,string NroDocumento, int idTipoDocumento)
        {
            InitializeComponent();
            this.formClienteNuevo = formClienteNuevo;
            this.NroDocumento = NroDocumento;
            this.idTipoDocumento = idTipoDocumento;


        }

        #endregion====================Construtor=============== 


        private void UCProveedorGeneral_Load(object sender, EventArgs e)
        {
            this.reLoad();
           

        }

        #region ================================= Loads =================================
        private void cargarDatosModificar()
        {
            if (!formClienteNuevo.nuevo)
            {
                textZipCode.Text = formClienteNuevo.currentCliente.telefono;
                textDireccion.Text = formClienteNuevo.currentCliente.direccion;
                textEmail.Text = formClienteNuevo.currentCliente.email;
                chkEstado.Checked = Convert.ToBoolean(formClienteNuevo.currentCliente.estado);
                textCelular.Text = formClienteNuevo.currentCliente.celular;
                textNIdentificacion.Text = formClienteNuevo.currentCliente.numeroDocumento;
                textTelefono.Text = formClienteNuevo.currentCliente.telefono;
                txtDatosEnvio.Text = formClienteNuevo.currentCliente.observacion;
                txtNombreCliente.Text = formClienteNuevo.currentCliente.nombreCliente;

             
                cbxSexo.Text = formClienteNuevo.currentCliente.sexo == "M" ? "Masculino" : "Femenino";
                cbxTipoGrupo.Text = formClienteNuevo.currentCliente.nombreGrupo;
            }
            else
            {
                textNIdentificacion.Text = NroDocumento;
               

                textNIdentificacion.Select();
                textNIdentificacion.Focus();
            }
        }

        internal async void reLoad()
        {
            await cargarPaises();
            crearNivelesPais();
            
            cargarGClientes();
            cargartiposDocumentos();
           
        }


        public async void cargarGClientes()
        {
            grupoClientes = await clienteModel.listarGrupoClienteIdGCNombreByActivos();
            grupoClienteCBindingSource.DataSource = grupoClientes;
            cargarDatosModificar();
        }
        private async void cargartiposDocumentos()
        {

            documentoIdentificaciones = await documentoIdentificacionModel.docIdentificacion();
            documentoIdentificacionBindingSource.DataSource = documentoIdentificaciones;
            cbxDocumento.SelectedIndex = -1;
            cbxDocumento.SelectedValue = documentoIdentificaciones[0].idDocumento;
            if (idTipoDocumento != 0)
            {
                cbxDocumento.SelectedValue = idTipoDocumento;
            }

            if (!formClienteNuevo.nuevo)
            {
                cbxDocumento.SelectedValue = formClienteNuevo.currentCliente.idDocumento;
            }
        }



        private async Task cargarPaises()
        {
            // cargando los paises
            paisBindingSource.DataSource = await locationModel.paises();

            // cargando la ubicacion geografica por defecto
            if (formClienteNuevo.nuevo)
            {
                ubicacionGeografica = await locationModel.ubigeoActual(ConfigModel.sucursal.idUbicacionGeografica);
            }
            else
            {
                ubicacionGeografica = await locationModel.ubigeoActual(formClienteNuevo.currentCliente.idUbicacionGeografica);
            }
            cbxPaises.SelectedValue = ubicacionGeografica.idPais;



        }
        #endregion


        #region ================== Formando los niveles de cada pais ==================
        private async void crearNivelesPais()
        {
            try
            {
                formClienteNuevo.loadStateApp(true);
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
                formClienteNuevo.loadStateApp(false);
                textNIdentificacion.Focus();
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
                formClienteNuevo.loadStateApp(true);
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
                formClienteNuevo.loadStateApp(false);
                desactivarNivelDesde(2);
            }
        }

        private async void cargarNivel2()
        {
            try
            {
                if (labelUbicaciones.Count < 2) return;
                formClienteNuevo.loadStateApp(true);
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
                formClienteNuevo.loadStateApp(false);
            }
        }

        private async void cargarNivel3()
        {
            try
            {
                if (labelUbicaciones.Count < 3) return;
                formClienteNuevo.loadStateApp(true);
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
                formClienteNuevo.loadStateApp(false);
                desactivarNivelDesde(4);
            }
        }
        private async void cargarNivel4()
        {
            try
            {
                if (labelUbicaciones.Count < 4) return;
                formClienteNuevo.loadStateApp(true);

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
                formClienteNuevo.loadStateApp(false);
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
        private async void btnAceptar_Click(object sender, EventArgs e)
        {

            // GUARDAR UBICACion 

            if (formClienteNuevo.nuevo)
            { 
                UbicacionGeograficaG UG = new UbicacionGeograficaG();

                int i = cbxPaises.SelectedIndex;
                UG.idPais = (int)cbxPaises.SelectedValue;
                UG.idNivel1 = (int)cbxNivel1.SelectedValue;
                UG.idNivel2 = (int)cbxNivel2.SelectedValue;
                if (cbxNivel3.IsAccessible)
                    UG.idNivel3 = (int)cbxNivel3.SelectedValue;
                Response respuesta = await locationModel.guardarUbigeo(UG);

                ClienteG CG = new ClienteG();


                DocumentoIdentificacion documentoIdentificacion = documentoIdentificaciones.Find(X => X.idDocumento == (int)cbxDocumento.SelectedValue);
                CG.celular = textCelular.Text;
                CG.direccion = textDireccion.Text;
                CG.email = textEmail.Text;
                CG.esEventual = false;
                CG.estado = chkEstado.Checked ? 1 : 0;
                CG.idDocumento = (int)cbxDocumento.SelectedValue;
                CG.idGrupoCliente = (int)cbxTipoGrupo.SelectedValue;
                CG.idUbicacionGeografica = respuesta.id;
                CG.nombre = cbxDocumento.Text;
                CG.nombreCliente = txtNombreCliente.Text;
                CG.nombreGrupo = cbxTipoGrupo.Text;
                CG.nroVentasCotizaciones = 0;
                CG.numeroDocumento = textNIdentificacion.Text;
                CG.observacion = txtDatosEnvio.Text;
                CG.sexo = cbxSexo.Text;
                CG.telefono = textTelefono.Text;


                CG.tipoDocumento = documentoIdentificacion.tipoDocumento;// 
                CG.zipCode = textZipCode.Text;
                rest = await clienteModel.guardar(CG);
                if (rest.id > 0)
                {
                    MessageBox.Show(rest.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.formClienteNuevo.rest = rest;
                    this.formClienteNuevo.Close();

                }
                else
                {
                    MessageBox.Show("error en guardar" + rest.msj, "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }

            else
            {
                UbicacionGeograficaG UG = new UbicacionGeograficaG();

                int i = cbxPaises.SelectedIndex;
                UG.idPais = (int)cbxPaises.SelectedValue;
                UG.idNivel1 = (int)cbxNivel1.SelectedValue;

                if (cbxNivel2.IsAccessible)
                    UG.idNivel2 = (int)cbxNivel2.SelectedValue;
                if (cbxNivel3.IsAccessible)
                    UG.idNivel3 = (int)cbxNivel3.SelectedValue;
                Response respuesta = await locationModel.guardarUbigeo(UG);

                Response respuesta1 = await locationModel.guardarUbigeo(UG);

                Cliente CG = new Cliente();

                DocumentoIdentificacion documentoIdentificacion = documentoIdentificaciones.Find(X => X.idDocumento == (int)cbxDocumento.SelectedValue);

                CG.idCliente = formClienteNuevo.currentCliente.idCliente;
                CG.celular = textCelular.Text;
                CG.direccion = textDireccion.Text;
                CG.email = textEmail.Text;
                CG.esEventual = false;
                CG.estado = chkEstado.Checked ? 1 : 0;
                CG.idDocumento = (int)cbxDocumento.SelectedValue;
                CG.idGrupoCliente = (int)cbxTipoGrupo.SelectedValue;
                CG.idUbicacionGeografica = respuesta.id;
                CG.nombre = cbxDocumento.Text;
                CG.nombreCliente = txtNombreCliente.Text;
                CG.nombreGrupo = cbxTipoGrupo.Text;
                CG.nroVentasCotizaciones = "0";
                CG.numeroDocumento = textNIdentificacion.Text;
                CG.observacion = txtDatosEnvio.Text;
                CG.sexo = cbxSexo.Text;
                CG.telefono = textTelefono.Text;
                CG.tipoDocumento = CG.tipoDocumento = documentoIdentificacion.tipoDocumento;;// es detalle q falta aclarar
                CG.zipCode = textZipCode.Text;
                rest = await clienteModel.modificar(CG);
                if (rest.id > 0)
                {
                    MessageBox.Show(rest.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.formClienteNuevo.Close();

                }
                else
                {
                    MessageBox.Show("error en Modificar " + rest.msj, "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        
        }

   

        private bool validarCampos()
        {
            if (textCelular.Text == "")
            {
                errorProvider1.SetError(textCelular, "Este campo esta vacío");
                textCelular.Focus();
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
                errorProvider1.SetError(textNIdentificacion, "Este campo esta vacío");
                textNIdentificacion.Focus();
                return false;
            }
            errorProvider1.Clear();

            if (cbxTipoGrupo.SelectedIndex == -1)
            {
                errorProvider1.SetError(cbxTipoGrupo, "Elija al menos uno");
                cbxTipoGrupo.Focus();
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
            this.formClienteNuevo.Close();
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
            Validator.isNroDocumento(e,  nroCarateres,nroMaximoCaracteres);


            if (nroCarateres==nroMaximoCaracteres)               
                 {
                        if (e.KeyChar == (char)Keys.Enter)


                {
                            try
                            {
                                this.formClienteNuevo.loadStateApp(true);
                        //respuestaSunat
                                 respuestaSunat = await sunatModel.obtenerDatos(aux);
                            }
                            catch (Exception ex)
                            {
                                JsonReaderException ex1 = new JsonReaderException(); ;
                                if(Object.ReferenceEquals(ex.GetType(), ex1.GetType())){

                                     MessageBox.Show("tiempo de respuesta terminado", "consulta sunat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                else
                                {
                                    MessageBox.Show("Error: " + ex.Message, "consulta sunat", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                }
                               
                            }
                           
                        }
                       // Ver(aux);
                    
                }
            try
            {

                if (respuestaSunat != null)
                {
                    if (respuestaSunat.success)
                    {
                        dataSunat = respuestaSunat.result;
                        if(dataSunat.RUC.Length==nroMaximoCaracteres)
                            textNIdentificacion.Text = dataSunat.RUC;
                        textCelular.Text = dataSunat.Telefono.Substring(1, dataSunat.Telefono.Length - 1);
                        txtNombreCliente.Text = dataSunat.RazonSocial;
                        textDireccion.Text = concidencias(dataSunat.Direccion);
                        respuestaSunat = null;

                    }
                    else
                    {
                        this.formClienteNuevo.loadStateApp(false);
                        MessageBox.Show("Error: " + " no exite en la sunat", "consulta sunat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        respuestaSunat = null;
                    }
                }

            }
            catch( Exception  ex)
            {
                MessageBox.Show("Error: " + ex.Message , "cargando datos sunat", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }
            finally
            {
                this.formClienteNuevo.loadStateApp(false);
            }
                
              
            
                

        }


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

        private void btnGrupoNuevo_Click(object sender, EventArgs e)
        {
           this.formClienteNuevo.togglePanelMain("Nuevorupo");      
           
        }

        private void cbxDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxDocumento.SelectedIndex==-1)return;

             DocumentoIdentificacion documentoIdentificacion=  documentoIdentificaciones.Find(X => X.idDocumento == (int)cbxDocumento.SelectedValue);
            nroMaximoCaracteres = documentoIdentificacion.numeroDigitos;

            if (documentoIdentificacion.tipoDocumento == "Jurídico")
            {
                cbxSexo.Visible = false;
                lbsexo.Visible = false;


            }
            else
            {
                cbxSexo.Visible = true;
                lbsexo.Visible = true;


            }
           
          
        }

        private async void textNIdentificacion_OnValueChanged(object sender, EventArgs e)
        {

            //if (formClienteNuevo.nuevo)
            //{
               


            //}
           
           
        }

        private void txtNombreCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validator.isString(e);
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            
            this.formClienteNuevo.Close();
        }
    }
}
