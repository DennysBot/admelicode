using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidad.Configuracion;
using Newtonsoft.Json;
using Entidad;
using System.Globalization;
using Bunifu.Framework.UI;
using Modelo;
using System.Threading;
using System.Drawing.Printing;

namespace Admeli.Configuracion.Modificar
{


    public partial class FormDiseñoComprobantes : Form
    {


        List<DiseñoDocumento> listData;
        public Panel panel4;
        Panel centrado;
        List<ResizeableControl> listaElemtos;
        ResizeableControl rc;
        DiseñoDocumento diseño;
        DiseñoDocumento original;
        List<FormatoDocumento>  formato;
        String json;
        bool redi = false;
        Label aux;
        PictureBox cuadro;

        List<vineta> vinetas = new List<vineta>();

        List<vineta> listLabel = new List<vineta>();// label adiciolanes "texto"

        List<vineta> listGridField = new List<vineta>();
        vineta detalleBtn;
        DataGridView detalle; 
        bool moverCuadro = false;
        int posicionX, posicionY;
        int flag = 0;

        // menu para los controles 

        ContextMenuStrip contextMenuStrip { get; set; }
        public FormDiseñoComprobantes()
        {
            InitializeComponent();
            
        }

        protected System.Drawing.Color colorFondo(string color)
        {
            System.Drawing.Color resultado = new System.Drawing.Color();
            if (color.IndexOf("#") == -1)
                return Color.AliceBlue;
            if (color.IndexOf("#") != -1)
                color = color.Replace("#", "");

            int R = int.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int G = int.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int B = int.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            return resultado = System.Drawing.Color.FromArgb(R, G, B);
        }
        public FormDiseñoComprobantes(DiseñoDocumento diseño)
        {

            InitializeComponent();
           
            //
            //centrado.Location = new Point(centrado.Location.X + panel3.Location.X, centrado.Location.Y + panel3.Location.Y);
            
            panel4 = new Panel();
            panel4.AutoSize = false;
            
            listaElemtos = new List<ResizeableControl>();
            detalle = new DataGridView();
           
            detalle.RowHeadersVisible = false;
            this.diseño = diseño;
            cuadro = new PictureBox();
            detalleBtn = new vineta();
            detalleBtn.nombre = "Detalle";
            crearLabels();
            crearListGrid();

            
            formato =JsonConvert.DeserializeObject<List<FormatoDocumento>>(this.diseño.formatoDocumento);
            
            if (formato!=null && exitePagina())
                agregarElementos();
          
            cargarNoSeleccionados();
            designarEventos();
        }

        public FormDiseñoComprobantes(DiseñoDocumento diseño, List<DiseñoDocumento> listData)
        {

            InitializeComponent();




            contextMenuStrip = new ContextMenuStrip();
            //contextMenuStrip.ad
            this.listData = listData;

            //
            original = diseño;
            cargarMenuModelos();
            //centrado.Location = new Point(centrado.Location.X + panel3.Location.X, centrado.Location.Y + panel3.Location.Y);

            panel4 = new Panel();
            panel4.AutoSize = false;

            listaElemtos = new List<ResizeableControl>();
            detalle = new DataGridView();

            detalle.RowHeadersVisible = false;
            this.diseño = diseño;
            cuadro = new PictureBox();
            detalleBtn = new vineta();
            detalleBtn.nombre = "Detalle";
            crearLabels();
            crearListGrid();


            formato = JsonConvert.DeserializeObject<List<FormatoDocumento>>(this.diseño.formatoDocumento);

            if (formato != null && exitePagina())
                agregarElementos();

            cargarNoSeleccionados();
            designarEventos();
        }



        private void cargarMenuModelos()
        {
            ToolStripMenuItem menu = new ToolStripMenuItem();

            menu.Text = "Clonar Modelos";
            menu.Size = new Size(48, 24);
            this.menuStrip1.Items.Add(menu);

            foreach (DiseñoDocumento v in listData)
            {
                ToolStripMenuItem aux=new ToolStripMenuItem();
                aux.Text = v.nombre;
                aux.Name = v.nombre;
                aux.Click += new System.EventHandler(this.modelosMenuItem_Click);

                menu.DropDownItems.Add(aux );
            } 


            

            
        }

        public void modelosMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem aux = sender as ToolStripMenuItem;
            foreach(DiseñoDocumento v in listData)
            {
                if (v.nombre == aux.Name)
                {
                    diseño = v;
                }
            }

            // eliminamos todo 
            panel4.Controls.Clear();
            panel3.Controls.Clear();

            vinetas.Clear();

            listLabel.Clear();// label adiciolanes "texto"
            listGridField.Clear();
            panel1.Controls.Clear();
            panel4 = new Panel();
            panel4.AutoSize = false;

            listaElemtos = new List<ResizeableControl>();
            detalle = new DataGridView();

            detalle.RowHeadersVisible = false;
            this.diseño = diseño;
            cuadro = new PictureBox();
            detalleBtn = new vineta();
            detalleBtn.nombre = "Detalle";
            crearLabels();
            crearListGrid();


            formato = JsonConvert.DeserializeObject<List<FormatoDocumento>>(this.diseño.formatoDocumento);

            if (formato != null && exitePagina())
                agregarElementos();

            cargarNoSeleccionados();
            designarEventos();
            // reload();

            //agregarElementos();
        }
        private void  crearLabels()
        {

            switch(diseño.idTipoDocumento)
            {
                case 1:
                    crearLabels1();
                    break;
                case 2:
                    crearLabels2();
                    break;
                case 3:
                    crearLabels3();
                    break;
                case 4:
                    crearLabels4();
                    break;
                case 5:
                    crearLabels5();
                    break;
                case 6:
                    crearLabels6();
                    break;
                case 7:
                    crearLabels7();
                    break;
                case 8:
                    crearLabels8();
                    break;
                case 9:
                    crearLabels9();
                    break;
                case 10:
                    crearLabels10();
                    break;
            }
            
        }
        #region================
        private void crearLabels1()
        {

            vineta aux = new vineta();//
            aux = new vineta();
            aux.label.Text = "Fecha Emision";
            aux.nombre = "Fecha Emision";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre/Razón Cliente";
            aux.nombre = "Nombre/Razón Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Dirección Cliente";
            aux.nombre = "Dirección Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);

       

            
            /*
            nombre = new Label();
            nombre.Text = "Nombre/Razón Cliente";


            fechaEmision = new Label();
            fechaEmision.Text = "Fecha Emision";

            direccionCliente = new Label();
            direccionCliente.Text = "Direccion Cliente";*/


            // anaranjado

            aux = new vineta();
            aux.label.Text = "Serie-Correlativo";
            aux.nombre = "Serie-Correlativo";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Empresa";
            aux.nombre = "Nombre Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Dirección Empresa";
            aux.nombre = "Dirección Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);

            aux = new vineta();
            aux.label.Text = "Documento Empresa";
            aux.nombre = "Documento Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Documento";
            aux.nombre = "Nombre Documento";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Descripción Empresa";
            aux.nombre = "Descripción Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);


            /*
            serieCorrelativo = new Label();
            serieCorrelativo.Text = "Serie Correlativo";
            nombreEmpresa = new Label();
            nombreEmpresa.Text = "nombre Empresa";
            direccionEmpresa = new Label();
            direccionEmpresa.Text = "Direccion Empresa";
            nombreDocumento = new Label();
            nombreEmpresa.Text = "Nombre Empresa";
            */

            //celeste

            aux = new vineta();
            aux.label.Text = "Impuesto";
            aux.label.BackColor = colorFondo("#0AEDD6");
            //aux.label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.vineta_MouseDown);
            aux.nombre = "Impuesto";
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Sub Total";
            aux.nombre = "Sub Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Texto Total";
            aux.nombre = "Texto Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Total";
            aux.nombre = "Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            /*
            impuesto = new Label();
            impuesto.Text = "Impuesto";
            subTotal = new Label();
            subTotal.Text = "Sub Total";
            textoTotal = new Label();
            textoTotal.Text = "Texto Total";
            total = new Label();
            total.Text = "Total";

            */
        }
        private void crearLabels2()
        {

            vineta aux = new vineta();//
            aux = new vineta();
            aux.label.Text = "Fecha Emision";
            aux.nombre = "Fecha Emision";
            aux.label.Text = "Fecha Vencimiento";
            aux.nombre = "Fecha Vencimiento";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre/Razón Cliente";
            aux.nombre = "Nombre/Razón Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Dirección Cliente";
            aux.nombre = "Dirección Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Documento Cliente";
            aux.nombre = "Documento Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Moneda";
            aux.nombre = "Moneda";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);




            /*
            nombre = new Label();
            nombre.Text = "Nombre/Razón Cliente";


            fechaEmision = new Label();
            fechaEmision.Text = "Fecha Emision";

            direccionCliente = new Label();
            direccionCliente.Text = "Direccion Cliente";*/


            // anaranjado

            aux = new vineta();
            aux.label.Text = "Serie-Correlativo";
            aux.nombre = "Serie-Correlativo";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Empresa";
            aux.nombre = "Nombre Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Dirección Empresa";
            aux.nombre = "Dirección Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);

            aux = new vineta();
            aux.label.Text = "Documento Empresa";
            aux.nombre = "Documento Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Documento";
            aux.nombre = "Nombre Documento";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Moneda";
            aux.nombre = "Moneda";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Descripción Empresa";
            aux.nombre = "Descripción Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);

            /*
            serieCorrelativo = new Label();
            serieCorrelativo.Text = "Serie Correlativo";
            nombreEmpresa = new Label();
            nombreEmpresa.Text = "nombre Empresa";
            direccionEmpresa = new Label();
            direccionEmpresa.Text = "Direccion Empresa";
            nombreDocumento = new Label();
            nombreEmpresa.Text = "Nombre Empresa";
            */

            //celeste

            aux = new vineta();
            aux.label.Text = "Impuesto";
            aux.label.BackColor = colorFondo("#0AEDD6");
            //aux.label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.vineta_MouseDown);
            aux.nombre = "Impuesto";
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Sub Total";
            aux.nombre = "Sub Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Texto Total";
            aux.nombre = "Texto Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Total";
            aux.nombre = "Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            /*
            impuesto = new Label();
            impuesto.Text = "Impuesto";
            subTotal = new Label();
            subTotal.Text = "Sub Total";
            textoTotal = new Label();
            textoTotal.Text = "Texto Total";
            total = new Label();
            total.Text = "Total";

            */
        }
        private void crearLabels3()
        {

            vineta aux = new vineta();//
            aux = new vineta();
            aux.label.Text = "Fecha Emision";
            aux.nombre = "Fecha Emision";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);

            aux = new vineta();
            aux.label.Text = "Fecha pago";
            aux.nombre = "Fecha pago";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);

            aux = new vineta();
            aux.label.Text = "Nombre/Razón Cliente";
            aux.nombre = "Nombre/Razón Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Dirección Cliente";
            aux.nombre = "Dirección Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Documento Cliente";
            aux.nombre = "Documento Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Moneda";
            aux.nombre = "Moneda";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);





            // anaranjado

            aux = new vineta();
            aux.label.Text = "Serie-Correlativo";
            aux.nombre = "Serie-Correlativo";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Empresa";
            aux.nombre = "Nombre Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Dirección Empresa";
            aux.nombre = "Dirección Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);

            aux = new vineta();
            aux.label.Text = "Documento Empresa";
            aux.nombre = "Documento Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Documento";
            aux.nombre = "Nombre Documento";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);

            aux = new vineta();
            aux.label.Text = "Descripción Empresa";
            aux.nombre = "Descripción Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);

            /*
            serieCorrelativo = new Label();
            serieCorrelativo.Text = "Serie Correlativo";
            nombreEmpresa = new Label();
            nombreEmpresa.Text = "nombre Empresa";
            direccionEmpresa = new Label();
            direccionEmpresa.Text = "Direccion Empresa";
            nombreDocumento = new Label();
            nombreEmpresa.Text = "Nombre Empresa";
            */

            //celeste

            aux = new vineta();
            aux.label.Text = "Impuesto";
            aux.label.BackColor = colorFondo("#0AEDD6");
            //aux.label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.vineta_MouseDown);
            aux.nombre = "Impuesto";
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Sub Total";
            aux.nombre = "Sub Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Texto Total";
            aux.nombre = "Texto Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Total";
            aux.nombre = "Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            /*
            impuesto = new Label();
            impuesto.Text = "Impuesto";
            subTotal = new Label();
            subTotal.Text = "Sub Total";
            textoTotal = new Label();
            textoTotal.Text = "Texto Total";
            total = new Label();
            total.Text = "Total";

            */
        }

        private void crearLabels4()
        {

            vineta aux = new vineta();//
            aux = new vineta();
            aux.label.Text = "Fecha Emision";
            aux.nombre = "Fecha Emision";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Moneda";
            aux.nombre = "Moneda";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre/Razón Cliente";
            aux.nombre = "Nombre/Razón Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Dirección Cliente";
            aux.nombre = "Dirección Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);




            /*
            nombre = new Label();
            nombre.Text = "Nombre/Razón Cliente";


            fechaEmision = new Label();
            fechaEmision.Text = "Fecha Emision";

            direccionCliente = new Label();
            direccionCliente.Text = "Direccion Cliente";*/


            // anaranjado

            aux = new vineta();
            aux.label.Text = "Serie-Correlativo";
            aux.nombre = "Serie-Correlativo";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Empresa";
            aux.nombre = "Nombre Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Dirección Empresa";
            aux.nombre = "Dirección Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);

            aux = new vineta();
            aux.label.Text = "Documento Empresa";
            aux.nombre = "Documento Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Documento";
            aux.nombre = "Nombre Documento";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Descripción Empresa";
            aux.nombre = "Descripción Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);


            /*
            serieCorrelativo = new Label();
            serieCorrelativo.Text = "Serie Correlativo";
            nombreEmpresa = new Label();
            nombreEmpresa.Text = "nombre Empresa";
            direccionEmpresa = new Label();
            direccionEmpresa.Text = "Direccion Empresa";
            nombreDocumento = new Label();
            nombreEmpresa.Text = "Nombre Empresa";
            */

            //celeste

            aux = new vineta();
            aux.label.Text = "Impuesto";
            aux.label.BackColor = colorFondo("#0AEDD6");
            //aux.label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.vineta_MouseDown);
            aux.nombre = "Impuesto";
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Sub Total";
            aux.nombre = "Sub Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Texto Total";
            aux.nombre = "Texto Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Total";
            aux.nombre = "Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            /*
            impuesto = new Label();
            impuesto.Text = "Impuesto";
            subTotal = new Label();
            subTotal.Text = "Sub Total";
            textoTotal = new Label();
            textoTotal.Text = "Texto Total";
            total = new Label();
            total.Text = "Total";

            */
        }

        private void crearLabels5()
        {

            vineta aux = new vineta();//
            aux = new vineta();
            aux.label.Text = "Fecha Emision";
            aux.nombre = "Fecha Emision";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre/Razón Cliente";
            aux.nombre = "Nombre/Razón Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Dirección Cliente";
            aux.nombre = "Dirección Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Documento Cliente";
            aux.nombre = "Dirección Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            /*
            nombre = new Label();
            nombre.Text = "Nombre/Razón Cliente";


            fechaEmision = new Label();
            fechaEmision.Text = "Fecha Emision";

            direccionCliente = new Label();
            direccionCliente.Text = "Direccion Cliente";*/


            // anaranjado

            aux = new vineta();
            aux.label.Text = "Serie-Correlativo";
            aux.nombre = "Serie-Correlativo";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Empresa";
            aux.nombre = "Nombre Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Dirección Empresa";
            aux.nombre = "Dirección Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);

            aux = new vineta();
            aux.label.Text = "Documento Empresa";
            aux.nombre = "Documento Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Documento";
            aux.nombre = "Nombre Documento";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);



            /*
            serieCorrelativo = new Label();
            serieCorrelativo.Text = "Serie Correlativo";
            nombreEmpresa = new Label();
            nombreEmpresa.Text = "nombre Empresa";
            direccionEmpresa = new Label();
            direccionEmpresa.Text = "Direccion Empresa";
            nombreDocumento = new Label();
            nombreEmpresa.Text = "Nombre Empresa";
            */

            //celeste

            aux = new vineta();
            aux.label.Text = "Impuesto";
            aux.label.BackColor = colorFondo("#0AEDD6");
            //aux.label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.vineta_MouseDown);
            aux.nombre = "Impuesto";
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Sub Total";
            aux.nombre = "Sub Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Texto Total";
            aux.nombre = "Texto Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Total";
            aux.nombre = "Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            /*
            impuesto = new Label();
            impuesto.Text = "Impuesto";
            subTotal = new Label();
            subTotal.Text = "Sub Total";
            textoTotal = new Label();
            textoTotal.Text = "Texto Total";
            total = new Label();
            total.Text = "Total";

            */
        }
        private void crearLabels6()
        {

            vineta aux = new vineta();//
            aux = new vineta();
            aux.label.Text = "Fecha Emision";
            aux.nombre = "Fecha Emision";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre/Razón Cliente";
            aux.nombre = "Nombre/Razón Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Dirección Cliente";
            aux.nombre = "Dirección Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Documento Cliente";
            aux.nombre = "Dirección Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            /*
            nombre = new Label();
            nombre.Text = "Nombre/Razón Cliente";


            fechaEmision = new Label();
            fechaEmision.Text = "Fecha Emision";

            direccionCliente = new Label();
            direccionCliente.Text = "Direccion Cliente";*/


            // anaranjado

            aux = new vineta();
            aux.label.Text = "Serie-Correlativo";
            aux.nombre = "Serie-Correlativo";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Empresa";
            aux.nombre = "Nombre Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Dirección Empresa";
            aux.nombre = "Dirección Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);

            aux = new vineta();
            aux.label.Text = "Documento Empresa";
            aux.nombre = "Documento Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Documento";
            aux.nombre = "Nombre Documento";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);



            /*
            serieCorrelativo = new Label();
            serieCorrelativo.Text = "Serie Correlativo";
            nombreEmpresa = new Label();
            nombreEmpresa.Text = "nombre Empresa";
            direccionEmpresa = new Label();
            direccionEmpresa.Text = "Direccion Empresa";
            nombreDocumento = new Label();
            nombreEmpresa.Text = "Nombre Empresa";
            */

            //celeste

            aux = new vineta();
            aux.label.Text = "Impuesto";
            aux.label.BackColor = colorFondo("#0AEDD6");
            //aux.label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.vineta_MouseDown);
            aux.nombre = "Impuesto";
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Sub Total";
            aux.nombre = "Sub Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Texto Total";
            aux.nombre = "Texto Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Total";
            aux.nombre = "Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            /*
            impuesto = new Label();
            impuesto.Text = "Impuesto";
            subTotal = new Label();
            subTotal.Text = "Sub Total";
            textoTotal = new Label();
            textoTotal.Text = "Texto Total";
            total = new Label();
            total.Text = "Total";

            */
        }
        private void crearLabels7()
        {


  
            vineta aux = new vineta();//
            aux = new vineta();
            aux.label.Text = "Nro documento compra";
            aux.nombre = "Nro documento compra";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Proveedor";
            aux.nombre = "Proveedor";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Nro documento proveedor";
            aux.nombre = "Nro documento proveedor";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Fecha Emision";
            aux.nombre = "Fecha Emision";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);


            aux = new vineta();//
            aux.label.Text = "Fecha entrada";
            aux.nombre = "Fecha entrada";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);

            aux = new vineta();//
            aux.label.Text = "Almacén";
            aux.nombre = "Almacén";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);

            aux = new vineta();//
            aux.label.Text = "observación";
            aux.nombre = "observación";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
           


            // anaranjado

            aux = new vineta();
            aux.label.Text = "Serie-Correlativo";
            aux.nombre = "Serie-Correlativo";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Empresa";
            aux.nombre = "Nombre Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Dirección Empresa";
            aux.nombre = "Dirección Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);

            aux = new vineta();
            aux.label.Text = "Documento Empresa";
            aux.nombre = "Documento Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Documento";
            aux.nombre = "Nombre Documento";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Descripción Empresa";
            aux.nombre = "Descripción Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);


            //celeste
            /*
            aux = new vineta();
            aux.label.Text = "Impuesto";
            aux.label.BackColor = colorFondo("#0AEDD6");
            //aux.label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.vineta_MouseDown);
            aux.nombre = "Impuesto";
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Sub Total";
            aux.nombre = "Sub Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Texto Total";
            aux.nombre = "Texto Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Total";
            aux.nombre = "Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            /*
            impuesto = new Label();
            impuesto.Text = "Impuesto";
            subTotal = new Label();
            subTotal.Text = "Sub Total";
            textoTotal = new Label();
            textoTotal.Text = "Texto Total";
            total = new Label();
            total.Text = "Total";

            */
        }
        private void crearLabels8()
        {



            vineta aux = new vineta();//
            aux = new vineta();
            aux.label.Text = "Nro documento venta";
            aux.nombre = "Nro documento venta";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre/Razon Cliente";
            aux.nombre = "Nombre/Razon Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Documento Cliente";
            aux.nombre = "Documento Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Dirección destino";
            aux.nombre = "Dirección destino";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);

            aux = new vineta();//
            aux.label.Text = "Fecha Emision";
            aux.nombre = "Fecha Emision";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Fecha Salida";
            aux.nombre = "Fecha Salida";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Motivo salida";
            aux.nombre = "Motivo salida";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "descripción";
            aux.nombre = "descripción";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);



            // anaranjado

            aux = new vineta();
            aux.label.Text = "Serie-Correlativo";
            aux.nombre = "Serie-Correlativo";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Empresa";
            aux.nombre = "Nombre Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Dirección Empresa";
            aux.nombre = "Dirección Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Documento Empresa";
            aux.nombre = "Documento Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Descripción Empresa";
            aux.nombre = "Descripción Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Documento";
            aux.nombre = "Nombre Documento";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);




            //celeste
            /*
            aux = new vineta();
            aux.label.Text = "Impuesto";
            aux.label.BackColor = colorFondo("#0AEDD6");
            //aux.label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.vineta_MouseDown);
            aux.nombre = "Impuesto";
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Sub Total";
            aux.nombre = "Sub Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Texto Total";
            aux.nombre = "Texto Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Total";
            aux.nombre = "Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            /*
            impuesto = new Label();
            impuesto.Text = "Impuesto";
            subTotal = new Label();
            subTotal.Text = "Sub Total";
            textoTotal = new Label();
            textoTotal.Text = "Texto Total";
            total = new Label();
            total.Text = "Total";

            */
        }
        private void crearLabels9()
        {



            vineta aux = new vineta();//
            aux = new vineta();
            aux.label.Text = "Origen";
            aux.nombre = "Origen";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nro documento nota salida";
            aux.nombre = "Nro documento nota salida";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Empresa de transporte";
            aux.nombre = "Empresa de transporte";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Motivo de traslado";
            aux.nombre = "Motivo de traslado";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);

            aux = new vineta();//
            aux.label.Text = "Fecha Emision";
            aux.nombre = "Fecha Emision";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Marca y placa";
            aux.nombre = "Marca y placa";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Observación";
            aux.nombre = "Observación";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Licencia de conducir";
            aux.nombre = "Licencia de conducir";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);

            aux = new vineta();//
            aux.label.Text = "Dirección de origen";
            aux.nombre = "Dirección de origen";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);

            aux = new vineta();//
            aux.label.Text = "Destino";
            aux.nombre = "Destino";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);

            aux = new vineta();//
            aux.label.Text = "Direccón destino";
            aux.nombre = "Direccón destino";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);



            // anaranjado

            aux = new vineta();
            aux.label.Text = "Serie-Correlativo";
            aux.nombre = "Serie-Correlativo";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Empresa";
            aux.nombre = "Nombre Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Dirección Empresa";
            aux.nombre = "Dirección Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Documento Empresa";
            aux.nombre = "Documento Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Descripción Empresa";
            aux.nombre = "Descripción Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Documento";
            aux.nombre = "Nombre Documento";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);




            //celeste
            /*
            aux = new vineta();
            aux.label.Text = "Impuesto";
            aux.label.BackColor = colorFondo("#0AEDD6");
            //aux.label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.vineta_MouseDown);
            aux.nombre = "Impuesto";
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Sub Total";
            aux.nombre = "Sub Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Texto Total";
            aux.nombre = "Texto Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Total";
            aux.nombre = "Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            /*
            impuesto = new Label();
            impuesto.Text = "Impuesto";
            subTotal = new Label();
            subTotal.Text = "Sub Total";
            textoTotal = new Label();
            textoTotal.Text = "Texto Total";
            total = new Label();
            total.Text = "Total";

            */
        }
        private void crearLabels10()
        {

            vineta aux = new vineta();//
            aux = new vineta();
            aux.label.Text = "Fecha Emision";
            aux.nombre = "Fecha Emision";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre/Razón Cliente";
            aux.nombre = "Nombre/Razón Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Dirección Cliente";
            aux.nombre = "Dirección Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            aux = new vineta();//
            aux.label.Text = "Documento Cliente";
            aux.nombre = "Dirección Cliente";
            aux.label.BackColor = colorFondo("#0AED24");
            vinetas.Add(aux);
            /*
            nombre = new Label();
            nombre.Text = "Nombre/Razón Cliente";


            fechaEmision = new Label();
            fechaEmision.Text = "Fecha Emision";

            direccionCliente = new Label();
            direccionCliente.Text = "Direccion Cliente";*/


            // anaranjado

            aux = new vineta();
            aux.label.Text = "Serie-Correlativo";
            aux.nombre = "Serie-Correlativo";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Empresa";
            aux.nombre = "Nombre Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Dirección Empresa";
            aux.nombre = "Dirección Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);

            aux = new vineta();
            aux.label.Text = "Documento Empresa";
            aux.nombre = "Documento Empresa";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Nombre Documento";
            aux.nombre = "Nombre Documento";
            aux.label.BackColor = colorFondo("#EDA50A");
            vinetas.Add(aux);



            /*
            serieCorrelativo = new Label();
            serieCorrelativo.Text = "Serie Correlativo";
            nombreEmpresa = new Label();
            nombreEmpresa.Text = "nombre Empresa";
            direccionEmpresa = new Label();
            direccionEmpresa.Text = "Direccion Empresa";
            nombreDocumento = new Label();
            nombreEmpresa.Text = "Nombre Empresa";
            */

            //celeste

            aux = new vineta();
            aux.label.Text = "Impuesto";
            aux.label.BackColor = colorFondo("#0AEDD6");
            //aux.label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.vineta_MouseDown);
            aux.nombre = "Impuesto";
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Sub Total";
            aux.nombre = "Sub Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Texto Total";
            aux.nombre = "Texto Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            aux = new vineta();
            aux.label.Text = "Total";
            aux.nombre = "Total";
            aux.label.BackColor = colorFondo("#0AEDD6");
            vinetas.Add(aux);
            /*
            impuesto = new Label();
            impuesto.Text = "Impuesto";
            subTotal = new Label();
            subTotal.Text = "Sub Total";
            textoTotal = new Label();
            textoTotal.Text = "Texto Total";
            total = new Label();
            total.Text = "Total";

            */
        }

        #endregion================

        private void crearListGrid()
        {

            switch (diseño.idTipoDocumento)
            {
                case 1:
                    crearListGrid1();
                    break;
                case 2:
                    crearListGrid2();
                    break;
                case 3:
                    crearListGrid3();
                    break;
                case 4:
                    crearListGrid4();
                    break;
                case 5:
                    crearListGrid5();
                    break;
                case 6:
                    crearListGrid6();
                    break;
                case 7:
                    crearListGrid7();
                    break;
                case 8:
                    crearListGrid8();
                    break;
                case 9:
                    crearListGrid9();
                    break;
                case 10:
                    crearListGrid10();
                    break;
            }
        }

        #region==================
        private void crearListGrid1()
        {



            //para el texto
            vineta aux1 = new vineta();
            aux1.label.Text = "";
            aux1.nombre = "";
            listLabel.Add(aux1);
            //

            vineta aux = new vineta();
            aux.label.Text = "codigoProducto";
            aux.nombre = "codigoProducto";
            listGridField.Add(aux);

            aux = new vineta();
            aux.label.Text = "nombreCombinacion";
            aux.nombre = "nombreCombinacion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "cantidad";
            aux.nombre = "cantidad";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "nombrePresentacion";
            aux.nombre = "nombrePresentacion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "descripcion";
            aux.nombre = "descripcion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "nombreMarca";
            aux.nombre = "nombreMarca";
            listGridField.Add(aux);

            aux = new vineta();
            aux.label.Text = "precioUnitario";
            aux.nombre = "precioUnitario";
            listGridField.Add(aux);
           
            aux = new vineta();
            aux.label.Text = "descuento";
            aux.nombre = "descuento";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "total";
            aux.nombre = "total";
            listGridField.Add(aux);
        }
        private void crearListGrid2()
        {



            //para el texto
            vineta aux1 = new vineta();
            aux1.label.Text = "";
            aux1.nombre = "";
            listLabel.Add(aux1);
            //

            vineta aux = new vineta();
            aux.label.Text = "codigoProducto";
            aux.nombre = "codigoProducto";
            listGridField.Add(aux);

            aux = new vineta();
            aux.label.Text = "nombreCombinacion";
            aux.nombre = "nombreCombinacion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "cantidad";
            aux.nombre = "cantidad";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "nombrePresentacion";
            aux.nombre = "nombrePresentacion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "descripcion";
            aux.nombre = "descripcion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "nombreMarca";
            aux.nombre = "nombreMarca";
            listGridField.Add(aux);

            aux = new vineta();
            aux.label.Text = "precioUnitario";
            aux.nombre = "precioUnitario";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "descuento";
            aux.nombre = "descuento";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "precioVenta";
            aux.nombre = "precioVenta";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "total";
            aux.nombre = "total";
            listGridField.Add(aux);
        }
        private void crearListGrid3()
        {
            //para el texto
            vineta aux1 = new vineta();
            aux1.label.Text = "";
            aux1.nombre = "";
            listLabel.Add(aux1);
            //

            vineta aux = new vineta();
            aux.label.Text = "codigoProducto";
            aux.nombre = "codigoProducto";
            listGridField.Add(aux);

            aux = new vineta();
            aux.label.Text = "nombreCombinacion";
            aux.nombre = "nombreCombinacion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "cantidad";
            aux.nombre = "cantidad";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "nombrePresentacion";
            aux.nombre = "nombrePresentacion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "descripcion";
            aux.nombre = "descripcion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "nombreMarca";
            aux.nombre = "nombreMarca";
            listGridField.Add(aux);

            aux = new vineta();
            aux.label.Text = "precioUnitario";
            aux.nombre = "precioUnitario";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "descuento";
            aux.nombre = "descuento";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "precioVenta";
            aux.nombre = "precioVenta";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "total";
            aux.nombre = "total";
            listGridField.Add(aux);
        }
        private void crearListGrid4()
        {
            //para el texto
            vineta aux1 = new vineta();
            aux1.label.Text = "";
            aux1.nombre = "";
            listLabel.Add(aux1);
            //

            vineta aux = new vineta();
            aux.label.Text = "codigoProducto";
            aux.nombre = "codigoProducto";
            listGridField.Add(aux);

            aux = new vineta();
            aux.label.Text = "nombreCombinacion";
            aux.nombre = "nombreCombinacion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "cantidad";
            aux.nombre = "cantidad";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "nombrePresentacion";
            aux.nombre = "nombrePresentacion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "descripcion";
            aux.nombre = "descripcion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "nombreMarca";
            aux.nombre = "nombreMarca";
            listGridField.Add(aux);

            aux = new vineta();
            aux.label.Text = "precioUnitario";
            aux.nombre = "precioUnitario";
            listGridField.Add(aux);
           
            aux = new vineta();
            aux.label.Text = "precioVenta";
            aux.nombre = "precioVenta";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "total";
            aux.nombre = "total";
            listGridField.Add(aux);
        }
        //nota credito
        private void crearListGrid5() { }
        //nota debito
        private void crearListGrid6() { }


        private void crearListGrid7()
        {
            //para el texto
            vineta aux1 = new vineta();
            aux1.label.Text = "";
            aux1.nombre = "";
            listLabel.Add(aux1);
            //

            vineta aux = new vineta();
            aux.label.Text = "codigoProducto";
            aux.nombre = "codigoProducto";
            listGridField.Add(aux);

            aux = new vineta();
            aux.label.Text = "nombreCombinacion";
            aux.nombre = "nombreCombinacion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "cantidad";
            aux.nombre = "cantidad";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "nombrePresentacion";
            aux.nombre = "nombrePresentacion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "descripcion";
            aux.nombre = "descripcion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "nombreMarca";
            aux.nombre = "nombreMarca";
            listGridField.Add(aux);

            aux = new vineta();
            aux.label.Text = "cantidadUnitario";
            aux.nombre = "cantidadUnitario";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "cantidadRecibida";
            aux.nombre = "cantidadRecibida";
            listGridField.Add(aux);

            aux = new vineta();
            aux.label.Text = "presentacion";
            aux.nombre = "presentacion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "variante";
            aux.nombre = "variante";
            listGridField.Add(aux);
        }
        private void crearListGrid8()
        {
            //para el texto
            vineta aux1 = new vineta();
            aux1.label.Text = "";
            aux1.nombre = "";
            listLabel.Add(aux1);
            //

            vineta aux = new vineta();
            aux.label.Text = "codigoProducto";
            aux.nombre = "codigoProducto";
            listGridField.Add(aux);

            aux = new vineta();
            aux.label.Text = "nombreCombinacion";
            aux.nombre = "nombreCombinacion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "cantidad";
            aux.nombre = "cantidad";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "nombrePresentacion";
            aux.nombre = "nombrePresentacion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "descripcion";
            aux.nombre = "descripcion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "nombreMarca";
            aux.nombre = "nombreMarca";
            listGridField.Add(aux);

            aux = new vineta();
            aux.label.Text = "cantidadUnitario";
            aux.nombre = "cantidadUnitario";
            listGridField.Add(aux);
            aux = new vineta();
         

            aux = new vineta();
            aux.label.Text = "presentacion";
            aux.nombre = "presentacion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "variante";
            aux.nombre = "variante";
            listGridField.Add(aux);
        }
        private void crearListGrid9()
        {
            //para el texto
            vineta aux1 = new vineta();
            aux1.label.Text = "";
            aux1.nombre = "";
            listLabel.Add(aux1);
            //

            vineta aux = new vineta();
            aux.label.Text = "codigoProducto";
            aux.nombre = "codigoProducto";
            listGridField.Add(aux);

           
            aux = new vineta();
            aux.label.Text = "cantidad";
            aux.nombre = "cantidad";
            listGridField.Add(aux);
           
            aux = new vineta();
            aux.label.Text = "descripcion";
            aux.nombre = "descripcion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "nombreMarca";
            aux.nombre = "nombreMarca";
            listGridField.Add(aux);

            aux = new vineta();
            aux.label.Text = "cantidadUnitario";
            aux.nombre = "cantidadUnitario";
            listGridField.Add(aux);
            aux = new vineta();


            aux = new vineta();
            aux.label.Text = "presentacion";
            aux.nombre = "presentacion";
            listGridField.Add(aux);
            aux = new vineta();
            aux.label.Text = "variante";
            aux.nombre = "variante";
            listGridField.Add(aux);
        }
        private void crearListGrid10() { }
        #endregion==================



        private void darFontControl (Label d, string[] formatos )
        {
            if (formatos.Count() > 0)
            {
                

            }
            

        }
        public void agregarElementos()
        {
            


            foreach ( FormatoDocumento doc in formato)
            {
                string tipo = doc.tipo;
                switch (tipo)
                {
                    case "Label":
                        vineta aux = buscarVineta(doc.value, vinetas);

                        if (aux == null)
                        {
                            aux = new vineta();
                        }
                        string[] formato = doc.formato.Split('/');
                        darFontControl(aux.label, formato);
                        aux.label.Text = doc.value;
                        aux.label.ContextMenuStrip = this.contextMenuStrip1;
                        aux.usado = 3;
                        aux.nombre = doc.value;
                        aux.label.AutoSize = false;
                        // string colorcode = form.color;
                        //int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
                        aux.label.BackColor = colorFondo(doc.color);
                        aux.label.Location = new System.Drawing.Point(doc.x, doc.y);
                        aux.label.Size = new System.Drawing.Size((int)doc.w, (int)doc.h);

                        //   aux.label.



                        aux.label.TabIndex = 5;

                        this.panel4.Controls.Add(aux.label);
                        ResizeableControl rc = new ResizeableControl(aux.label);

                        listaElemtos.Add(rc);


                        break;
                    case "ListGrid":
                        detalle.Location = new Point(doc.x, doc.y);

                        detalle.Name = doc.formato;

                        detalle.Size = new Size((int)doc.w, (int)doc.h);

                        //detalle.col
                        detalle.ContextMenuStrip = this.contextMenuStrip1;
                        this.detalle.DoubleClick += new System.EventHandler(this.detalle_DoubleClick);
                        this.detalle.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);

                        detalleBtn.usado = 3;
                        this.panel4.Controls.Add(detalle);
                        rc = new ResizeableControl(detalle, 1);
                        listaElemtos.Add(rc);
                        break;
                    case "ListGridField":
                        vineta aux1 = buscarVineta(doc.formato, listGridField);
                        aux1.usado = 3;
                        // falta los tamños

                        detalle.Columns.Add(doc.formato, doc.value);

                        detalle.Columns[doc.formato].Width = (int)doc.w;

                        break;
                    case "Img":

                        // \Recursos\Darck

                        //string imagen = "E:\\code\\AdmeliWin\\Recursos\\Darck\\box_gray_icon.png";
                      //  cuadro.Image = Image.FromFile(imagen);
                        cuadro.Location = new Point(doc.x, doc.y);
                        cuadro.BackColor = colorFondo(doc.color);
                        cuadro.ContextMenuStrip = contextMenuStrip1;
                       
                        cuadro.Size = new Size((int)doc.w, (int)doc.h);
                        this.panel4.Controls.Add(cuadro);
                        rc = new ResizeableControl(cuadro);
                        listaElemtos.Add(rc);
                        break;
                   


                }
                
            }
        }
        private bool exitePagina()
        {
            

            for(int i= formato.Count-1; i >= 0; i--)
            {
                if (formato[i].tipo == "Pagina")
                {
                    panel4.BackColor = Color.White;
                    panel4.Width = (int)formato[i].w;
                    panel4.Height = (int)formato[i].h;
                    panel4.Location = new Point(formato[i].x+panel3.Location.X, formato[i].y+panel3.Location.Y);
                    panel4.Name = formato[i].value;

                    
                    //panel4.Anchor = AnchorStyles.Bottom;
                    panel3.Controls.Add(panel4);
                    return true;
                }
                
            }
                return false;
        }
        private void cargarNoSeleccionados()
        {
            
            int Y = 10;
            Button bunifuThinButton = new Button();

            bunifuThinButton.BackColor = System.Drawing.Color.LightGray;
            bunifuThinButton.Text = "texto";
            bunifuThinButton.Cursor = System.Windows.Forms.Cursors.Hand;
            bunifuThinButton.Font = new System.Drawing.Font("Century Gothic", 7.5f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            bunifuThinButton.ForeColor = System.Drawing.Color.DarkBlue;
            bunifuThinButton.FlatAppearance.BorderColor = Color.SlateGray;
            bunifuThinButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bunifuThinButton.Location = new System.Drawing.Point(20, Y);
            bunifuThinButton.Margin = new System.Windows.Forms.Padding(5);
            bunifuThinButton.Size = new System.Drawing.Size(160, 30);
            bunifuThinButton.TabIndex = 5;
            bunifuThinButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;



            
            this.panel1.Controls.Add(bunifuThinButton);

            Y += 43;
            foreach (vineta v1 in listGridField)
            {
                
                    Button bunifuThinButton21 = new Button();

                    bunifuThinButton21.BackColor = v1.usado == 3 ?System.Drawing.Color.LightGray: System.Drawing.Color.LightSlateGray;
                    v1.usado = v1.usado == 3?3:1;
                    bunifuThinButton21.Text = v1.nombre;
                    bunifuThinButton21.Cursor = System.Windows.Forms.Cursors.Hand;
                    bunifuThinButton21.Font = new System.Drawing.Font("Century Gothic", 7.5f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    bunifuThinButton21.ForeColor = System.Drawing.Color.DarkBlue;
                    bunifuThinButton21.FlatAppearance.BorderColor = Color.SlateGray;
                    bunifuThinButton21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    bunifuThinButton21.Location = new System.Drawing.Point(20, Y);
                    bunifuThinButton21.Margin = new System.Windows.Forms.Padding(5);
                    bunifuThinButton21.Size = new System.Drawing.Size(160, 30);
                    bunifuThinButton21.TabIndex = 5;
                    bunifuThinButton21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    Y += 33;
                    this.panel1.Controls.Add(bunifuThinButton21);
            }

            Y += 10;
      
            foreach(vineta v in vinetas)
            {
                if (v.usado!=3)
                {
                    Button bunifuThinButton21 = new Button();
                    v.usado = -1;
                    bunifuThinButton21.BackColor = System.Drawing.Color.LightGray;
                    bunifuThinButton21.Text =v.nombre;
                    bunifuThinButton21.Cursor = System.Windows.Forms.Cursors.Hand;
                    bunifuThinButton21.Font = new System.Drawing.Font("Century Gothic", 7.5f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    bunifuThinButton21.ForeColor = System.Drawing.Color.DarkBlue;
                    bunifuThinButton21.FlatAppearance.BorderColor = Color.SlateGray;
                    bunifuThinButton21.FlatStyle= System.Windows.Forms.FlatStyle.Flat;
                    bunifuThinButton21.Location = new System.Drawing.Point(20, Y);
                    bunifuThinButton21.Margin = new System.Windows.Forms.Padding(5);           
                    bunifuThinButton21.Size = new System.Drawing.Size(160,30);
                    bunifuThinButton21.TabIndex = 5;
                    bunifuThinButton21.BringToFront();
                    bunifuThinButton21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    Y += 33;
                    this.panel1.Controls.Add(bunifuThinButton21);

                }

            }
            Y += 10;
            if (detalleBtn.usado != 3)
            {
                Button bunifuThinButton21 = new Button();
                detalleBtn.usado = 2;
                bunifuThinButton21.BackColor = System.Drawing.Color.LightGray;
                bunifuThinButton21.Text = detalleBtn.nombre;
                bunifuThinButton21.Cursor = System.Windows.Forms.Cursors.Hand;
                bunifuThinButton21.Font = new System.Drawing.Font("Century Gothic", 7.5f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                bunifuThinButton21.ForeColor = System.Drawing.Color.DarkBlue;
                bunifuThinButton21.FlatAppearance.BorderColor = Color.SlateGray;
                bunifuThinButton21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                bunifuThinButton21.Location = new System.Drawing.Point(20, Y);
                bunifuThinButton21.Margin = new System.Windows.Forms.Padding(5);
                bunifuThinButton21.Size = new System.Drawing.Size(160, 30);
                bunifuThinButton21.TabIndex = 5;
                bunifuThinButton21.BringToFront();
                bunifuThinButton21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                Y += 33;
                this.panel1.Controls.Add(bunifuThinButton21);

            }



        }

        private void reload()
        {
            panel1.Controls.Clear();
            cargarNoSeleccionados();
            designarEventos();

        }
        private vineta buscarVineta(string buscar,List<vineta> listVineta)
        {

            foreach(vineta v in listVineta)
            {
                if (buscar == v.nombre)
                {
                    return v;
                }

            }
            return null;
        }


        private void designarEventos()
        {
            foreach(Control c in this.panel1.Controls)
            {
                Button c1 = c as Button;
                vineta aux=buscarVineta(c1.Text, vinetas);
                if (aux == null)
                {
                    aux=buscarVineta(c1.Text, listGridField);
                    if (aux == null)
                    {
                        aux= buscarVineta("", listLabel);

                        if(aux==null)

                            aux = detalleBtn;
                        else
                        {
                            
                            aux.usado = 1;
                        }
                           
                    }
                }
                if (aux.usado == 1)
                {
                    c.MouseDown += new System.Windows.Forms.MouseEventHandler(this.field_MouseDown);
                    c.MouseMove += new System.Windows.Forms.MouseEventHandler(this.field_MouseMove);
                    c.MouseUp += new System.Windows.Forms.MouseEventHandler(this.field_MouseUp);
                }
                else
                    if (aux.usado == -1)
                        {
                            c.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labels_MouseDown);
                            c.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labels_MouseMove);
                            c.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labels_MouseUp);
                        }
                    else {
                        if (aux.usado == 2) { 
                            c.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGrid_MouseDown);
                            c.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGrid_MouseMove);
                            c.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGrid_MouseUp);

                        }
                }


                //listGripfield

            }
        }


        private void FormDiseñoComprobantes_Load(object sender,EventArgs e)
        {
            
        }

      
        /*obsoleto
        private void vineta_MouseDown(object sender, MouseEventArgs e)
        {
            
            Label label = sender as Label;

            
            vineta aux=buscarVineta(label.Text,vinetas);
            
            aux.posicionX = e.X;
            aux.posicionY = e.Y;
            int x = label.Width;
            int y = label.Height;
            //bool uno = esta1(0, x, 0,y,e.X, e.Y);
            if (!redi)
            {
                aux.mover = true;


            }
            else
            {
                aux.redimensionar = true;
                Thread.Sleep(10000);
            }
                
           
        }
        private void vineta_MouseMove(object sender, MouseEventArgs e)
        {

            
            Label label = sender as Label;
            vineta aux = buscarVineta(label.Text, vinetas);
            if (aux.mover)
            {
                aux.label.Location = new Point(aux.label.Location.X + e.X - aux.posicionX, aux.label.Location.Y + e.Y - aux.posicionY);

                this.Refresh();

            }
            else
                if (aux.redimensionar)
                 {
                        RedimensionaLados(aux.label, e.X, e.Y);
                       // aux.label.Location = new Point(aux.label.Location.X + e.X - aux.posicionX, aux.label.Location.Y + e.Y - aux.posicionY);

                        //aux.label.Size = new Size(aux.label.Width + aux.posicionX-e.X, aux.label.Height +aux.posicionX -e.Y );
                       
                   }
        }

        public void RedimensionaLados(Label label, int X, int Y)
        {
            // arriba 
            int V1 = Label.MousePosition.X-(panel3.Location.X+panel4.Location.X);
            int V2 = Label.MousePosition.Y-(panel3.Location.Y + panel4.Location.Y);
            // int vX=

            if (Y < 0)
            {
                Y= Math.Abs(Y);
              //  Thread.Sleep(10000);
            }
                
             
            else
                Y = -Y;
            
                
            int nuevox = label.Width;
            int nuevoy = label.Height  + Y;
            label.Size = new Size(nuevox,nuevoy);
            label.Location = new Point(label.Location.X,Y);
           
            // abajo
            //derecha
            //izquierda
        }

        private void vineta_MouseUp(object sender, MouseEventArgs e)
        {
           
               
            Label label = sender as Label;
            vineta aux = buscarVineta(label.Text, vinetas);
            
             label.Cursor = Cursors.Default;
            if (redi && aux.redimensionar)
            {   redi = false;
                //label.Location = new Point(label.Location.X, e.Y);

            }
            aux.mover = false;
            aux.redimensionar = false;
           
        }

        private void cuadro_MouseDown(object sender , MouseEventArgs e)
        {
            moverCuadro = true;
            posicionX = e.X;
            posicionY = e.Y;

        }
   
        private void cuadro_MouseMove(object sender, MouseEventArgs e)
        {
            if (moverCuadro)
            {
                cuadro.Location = new Point(cuadro.Location.X+e.X-posicionX,cuadro.Location.Y+e.Y-posicionY);
                Refresh();

            }

        }
        private void cuadro_MouseUp(object sender, EventArgs e)
        {
            moverCuadro = false;

        }

        */

        private void labels_MouseDown(object sender, MouseEventArgs e)
        {
            Button c1 = sender as Button;
            vineta aux = buscarVineta(c1.Text, vinetas);

            aux.posicionX = e.X;
            aux.posicionY = e.Y;

            aux.inicialX = c1.Location.X;
            aux.inicialY = c1.Location.Y;
            c1.Cursor = Cursors.Hand;
            aux.mover = true;

        }

        private void labels_MouseMove(object sender, MouseEventArgs e)
        {
            Button c1 = sender as Button;
            vineta aux = buscarVineta(c1.Text, vinetas);
            if (aux.mover)
            {
                c1.Location = new Point(c1.Location.X + e.X - posicionX, c1.Location.Y + e.Y - posicionY);
                Refresh();

            }

        }
        private void labels_MouseUp(object sender, EventArgs e)
        {
            Button c1 = sender as Button;
            vineta aux = buscarVineta(c1.Text, vinetas);
            
           
           
           // aux.label.BackColor = Color.Black;
            aux.label.Location = new Point(c1.Location.X - (panel3.Location.X + panel4.Location.X), c1.Location.Y - panel4.Location.Y);

    
            aux.label.AutoSize = false;

            panel4.Controls.Add(aux.label);
          

            aux.usado = 3;// esta en el panel1 ahora
            panel1.Controls.Remove(c1); // el boton se elimina de panel1
            c1.Location = new Point(aux.inicialX, aux.inicialY);
            rc = new ResizeableControl(aux.label);
            listaElemtos.Add(rc);
            aux.mover = false;
            reload();
        }

        private void field_MouseDown(object sender, MouseEventArgs e)
        {
            Button c1 = sender as Button;
            vineta aux = buscarVineta(c1.Text, listGridField);
            if (aux == null)
                aux = buscarVineta("",listLabel);
            aux.posicionX = e.X;
            aux.posicionY = e.Y;

            aux.inicialX = c1.Location.X;
            aux.inicialY = c1.Location.Y;
            c1.Cursor = Cursors.Hand;
            aux.mover = true;

        }

        private void field_MouseMove(object sender, MouseEventArgs e)
        {
            Button c1 = sender as Button;
            vineta aux = buscarVineta(c1.Text, listGridField);
            if( aux==null)
                aux = buscarVineta("", listLabel);
            if (aux.mover)
            {
                c1.Location = new Point(c1.Location.X + e.X - posicionX, c1.Location.Y + e.Y - posicionY);
                Refresh();

            }
           

        }
        private void field_MouseUp(object sender, MouseEventArgs e)
        {
            Button c1 = sender as Button;
            vineta aux = buscarVineta(c1.Text, listGridField);

            if (aux == null)
            {
                String name = Microsoft.VisualBasic.Interaction.InputBox(null, "mensaje", "escriba el nombre del nuevo campo");
                aux = buscarVineta("", listLabel);
                if (name != "")
                {
                    aux.label.BackColor = colorFondo("#FFE6C5");
                    aux.label.Location = new Point(c1.Location.X - (panel3.Location.X + panel4.Location.X), c1.Location.Y - panel4.Location.Y);

                    aux.nombre = name;
                    aux.label.AutoSize = false;
                    aux.label.Text = name;
                    c1.Location = new Point(aux.inicialX, aux.inicialY);
                    panel4.Controls.Add(aux.label);
                    vineta aux1 = new vineta();
                    aux1.label.Text = "";
                    aux1.nombre = "";
                    listLabel.Add(aux1);
                    aux.mover = false;
                    rc = new ResizeableControl(aux.label);
                    listaElemtos.Add(rc);
                    reload();

                }



            }
            else { 
                bool uno = esta1(detalle.Location.X, detalle.Width, detalle.Location.Y, detalle.Height, c1.Location.X - (panel3.Location.X + panel4.Location.X), c1.Location.Y - panel4.Location.Y);

                if (esta(detalle.Location.X, detalle.Width, detalle.Location.Y, detalle.Height, c1.Location.X - (panel3.Location.X + panel4.Location.X), c1.Location.Y - panel4.Location.Y))
                {
                    if (!buscarColumna(c1.Text))
                    {
                        detalle.Columns.Add(c1.Text, c1.Text);
                        aux.usado = 3;
                    }
                }
                if (uno)
                    detalle.Cursor = Cursors.Default;
                c1.Location = new Point(aux.inicialX, aux.inicialY);

                aux.mover = false;
                reload();
            }
        }
        
        private void dataGrid_MouseDown(object sender, MouseEventArgs e)
        {
            Button c1 = sender as Button;
            detalleBtn.mover = true;
            detalleBtn.posicionX = e.X;
            detalleBtn.posicionY = e.Y;
            detalleBtn.inicialX = c1.Location.X;
            detalleBtn.inicialY = c1.Location.Y;
            c1.Cursor = Cursors.Hand;
            
        }
        private void dataGrid_MouseMove(object sender, MouseEventArgs e)
        {

            Button c1 = sender as Button;
           
            if (detalleBtn.mover)
            {
                c1.Location = new Point(c1.Location.X + e.X - posicionX, c1.Location.Y + e.Y - posicionY);
                Refresh();

            }
        }
        private void dataGrid_MouseUp(object sender, MouseEventArgs e)
        {
            Button c1 = sender as Button;
            detalle.Location = new Point(c1.Location.X - (panel3.Location.X + panel4.Location.X), c1.Location.Y - panel4.Location.Y);

            panel4.Controls.Add(detalle);
            detalleBtn.usado = 3;
            panel1.Controls.Remove(c1);
            //c1.Location = new Point(detalleBtn.inicialX, detalleBtn.inicialY);
            detalleBtn.mover = false;
            rc = new ResizeableControl(detalle);
            listaElemtos.Add(rc);
            //actualizar estado de 
            reload();
        }
        private void dataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            
            detalleBtn.mover = true;
            detalleBtn.posicionX = e.X;
            detalleBtn.posicionY = e.Y;
    
        }
        private void dataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if (detalleBtn.mover)
            {
                detalle.Location = new Point(detalle.Location.X + e.X - posicionX, detalle.Location.Y + e.Y - posicionY);
                Refresh();

            }

        }
        private void dataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            detalleBtn.mover = false;
            reload();

        }

        
        private bool buscarColumna(string column)
        {
            foreach (DataGridViewColumn c in detalle.Columns)
            {
                if (c.Name == column)
                    return true;
            }
            return false;
        }


        private string AHex(Color color)
        {

          

            string hex = color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
            return "#" + hex;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //
           FormatoDoc nuevo = new FormatoDoc();
            
            nuevo.idTipoDocumento= diseño.idTipoDocumento;
           Redimensionar redi = new Redimensionar();
            redi.idTipoDocumento = diseño.idTipoDocumento;
            redi.redimensionarModelo = Convert.ToInt16( diseño.redimensionarModelo);
            redi.bordeDetalle = 0;
            
            
            List<FormatoDocumento> listFormato = new List<FormatoDocumento>();

            // las texto del documentos
            foreach(vineta v in vinetas)
            {
                if (v.usado == 3)
                {
                    string dd = JsonConvert.SerializeObject(v.label.Font.Clone());
                    FormatoDocumento nuevoFormato = new FormatoDocumento();
                    nuevoFormato.color = AHex(v.label.BackColor);
                    nuevoFormato.formato = "";
                    nuevoFormato.tipo = "Label";
                    nuevoFormato.h = v.label.Height;
                    nuevoFormato.w = v.label.Width;
                    nuevoFormato.x = v.label.Location.X;
                    nuevoFormato.y = v.label.Location.Y;
                    nuevoFormato.value = v.label.Text;
                    listFormato.Add(nuevoFormato);

                }
            }
            FormatoDocumento nuevo1 = new FormatoDocumento();

            nuevo1.color = AHex(detalle.BackgroundColor);
            nuevo1.formato = "";
            nuevo1.tipo = "ListGrid";
            nuevo1.h = detalle.Height;
            nuevo1.w = detalle.Width;
            nuevo1.x = detalle.Location.X;
            nuevo1.y = detalle.Location.Y;
            nuevo1.value = detalle.Name; 
            listFormato.Add(nuevo1);

            foreach (vineta v1 in listGridField)
            {
                if (v1.usado == 3)
                {
                    FormatoDocumento nuevoFormato = new FormatoDocumento();
                    nuevoFormato.color = AHex(v1.label.BackColor);

                    
                    nuevoFormato.formato = v1.label.Text;
                    nuevoFormato.tipo = "ListGridField";
                    nuevoFormato.h = v1.label.Height;
                    nuevoFormato.w = detalle.Columns[v1.nombre].Width; 
                    nuevoFormato.x = v1.label.Location.X;
                    nuevoFormato.y = v1.label.Location.Y;
                    nuevoFormato.value = v1.nombre;
                    if (detalle.Columns[v1.nombre].Visible)
                        listFormato.Add(nuevoFormato);

                }
            }


            FormatoDocumento nuevo2 = new FormatoDocumento();
            foreach (vineta v1 in listLabel)
            {
                if (v1.nombre != "")
                {
                    FormatoDocumento nuevoFormato = new FormatoDocumento();
                    nuevoFormato.color = AHex(v1.label.BackColor);
                    nuevoFormato.formato = v1.label.Text;
                    nuevoFormato.tipo = "Label";
                    nuevoFormato.h = v1.label.Height;
                    nuevoFormato.w = v1.label.Width;
                    nuevoFormato.x = v1.label.Location.X;
                    nuevoFormato.y = v1.label.Location.Y;
                    nuevoFormato.value = v1.nombre;
                    listFormato.Add(nuevoFormato);

                }

            }
            // label adicionales



            FormatoDocumento nuevoCuadro = new FormatoDocumento();

            if (panel4.Controls.Contains(cuadro))
            {
                nuevoCuadro.color = AHex(cuadro.BackColor);
                nuevoCuadro.formato = "";
                nuevoCuadro.tipo = "Img";
                nuevoCuadro.h = cuadro.Height;
                nuevoCuadro.w = cuadro.Width;
                nuevoCuadro.x = cuadro.Location.X;
                nuevoCuadro.y = cuadro.Location.Y;
                nuevoCuadro.value = "Imagen";
                listFormato.Add(nuevoCuadro);
            }
           
           // pagina

            FormatoDocumento nuevoFormato1 = new FormatoDocumento();
            nuevoFormato1.color = AHex(panel4.BackColor);
            nuevoFormato1.formato = panel4.Text;
            nuevoFormato1.tipo = "Pagina";
            nuevoFormato1.h = panel4.Height;
            nuevoFormato1.w = panel4.Width;
            nuevoFormato1.x = 0;
            nuevoFormato1.y = 0;
            nuevoFormato1.value = "Pagina";
            listFormato.Add(nuevoFormato1);          
            nuevo.formatoDocumento = JsonConvert.SerializeObject(listFormato);
            modificarformato(nuevo);
            redimensionar(redi);
        }
        public async void modificarformato( FormatoDoc nuevo)
        {
            try
            {

              TipoDocumentoModel aux=new TipoDocumentoModel();
              Response response= await aux.modificarFormato(nuevo);
                if (response.id > 0)
                {

                    MessageBox.Show(response.msj+" satisfactoriamente , el formato se cargara al reiniciar sesión", "Guardar Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

            }
            catch(Exception ex )
            {
                MessageBox.Show("Error: " + ex.Message, "Guardar Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            
           

        }
        public async void redimensionar(Redimensionar nuevo)
        {
            TipoDocumentoModel aux = new TipoDocumentoModel();
            await aux.redimensionar(nuevo);

        }
        private bool esta(int x1 , int x2 ,int y1,int y2,int posicionX, int posicionY)
        {
            int finalX = x1 + x2;
            int finalY = y1 + y2;


            if (posicionX>= x1 && posicionX<= finalX)
                if(posicionY>=y1 && posicionY<=finalY)
                return true;
            return false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            redimensionarPagina myForm = new redimensionarPagina(this);
            myForm.Show(this);

            // Determine if the form is modal.
            
        }

        private void PrintPage(object o, PrintPageEventArgs e)
        {
            // Printng logic
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            vinetas.Clear();
            crearLabels();

            reload();

        }

        private void facturaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // eliminamos todo 
            panel4.Controls.Clear();
            panel3.Controls.Clear();

            vinetas.Clear();

            listLabel.Clear();// label adiciolanes "texto"
            listGridField.Clear();
            panel1.Controls.Clear();
            diseño = original;

            panel4 = new Panel();
            panel4.AutoSize = false;

            listaElemtos = new List<ResizeableControl>();
            detalle = new DataGridView();

            detalle.RowHeadersVisible = false;
            this.diseño = diseño;
            cuadro = new PictureBox();
            detalleBtn = new vineta();
            detalleBtn.nombre = "Detalle";
            crearLabels();
            crearListGrid();


            formato = JsonConvert.DeserializeObject<List<FormatoDocumento>>(this.diseño.formatoDocumento);

            if (formato != null && exitePagina())
                agregarElementos();

            cargarNoSeleccionados();
            designarEventos();
        }

        private bool esta1(int x1, int x2, int y1, int y2, int posicionX, int posicionY)
        {
            int finalX = x1 + x2;
            int finalY = y1 + y2;
            if (posicionY == y1)
                if (posicionX + x1 <= finalX)
                    return true;

            if (posicionX == finalX)
            {
                if (posicionY + y1 <= finalY)
                    return true;
            }
            if(posicionY==finalY)
                if (posicionX+x1<=finalX)
                {
                    return true;
                }

            if (posicionX == x1)
            {
                if (posicionY + y1 <= finalY)
                   return true;
            }

           
            return false;
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem dd = sender as ToolStripMenuItem;

            ContextMenuStrip contextMenuStrip = (ContextMenuStrip)dd.Owner;
            Control control = contextMenuStrip.SourceControl;



            vineta Aux = vinetas.Find(X => X.nombre == control.Text);
            if (Aux != null)
            {
                Aux.usado = 0;

            }
            Aux = listLabel.Find(X => X.nombre == control.Text);
            if (Aux != null)
            {
                Aux.usado = 0;

            }
            this.panel4.Controls.Remove(control);
            panel1.Controls.Clear();
            cargarNoSeleccionados();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowColor = true;
            fontDialog.ShowApply = true;
            fontDialog.ShowEffects = true;
            fontDialog.ShowHelp = true;
            fontDialog.MinSize = 7;
            fontDialog.MaxSize = 40;
            


            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ToolStripMenuItem dd = sender as ToolStripMenuItem;

                ContextMenuStrip contextMenuStrip = (ContextMenuStrip)dd.Owner;
                Control control = contextMenuStrip.SourceControl;
                control.Font = fontDialog.Font;
                control.ForeColor = fontDialog.Color;

            }
        }

        private void detalle_DoubleClick(object sender, EventArgs e)
        {
            if (flag == 0)
            {

                rc = new ResizeableControl(detalle);
                flag++;
            }
            else
            {

                detalle.MouseDown -= new System.Windows.Forms.MouseEventHandler(rc.mControl_MouseDown);
                detalle.MouseUp -= new System.Windows.Forms.MouseEventHandler(rc.mControl_MouseUp);
                detalle.MouseMove -= new System.Windows.Forms.MouseEventHandler(rc.mControl_MouseMove);
                detalle.MouseLeave -= new System.EventHandler(rc.mControl_MouseLeave);
                flag = 0;
            }
            
        }


        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Desea eliminar  esta columna?", sender.ToString(),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialog == DialogResult.No)
            {

             
                return;
            }
            detalle.Columns[e.ColumnIndex].Visible = false;
            
        }
    }
}
