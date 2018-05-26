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

namespace Admeli.Configuracion.Modificar
{
    public partial class redimensionarPagina : Form
    {
        FormDiseñoComprobantes formDiseño;


        int  altura;
        int  ancho;
        int nroDecimales = 2;
        string formato = "";
        public redimensionarPagina()
        {
            InitializeComponent();
        }
        public redimensionarPagina(FormDiseñoComprobantes formDiseño)
        {
            InitializeComponent();
            comboBox1.Items.Add("A4");
            comboBox1.Items.Add("A5");
            comboBox1.Items.Add("A6");
            comboBox1.Items.Add("usuario");
            comboBox1.SelectedIndex = -1;
            
            this.formDiseño = formDiseño;
            formato = "{0:n" + nroDecimales + "}";

        }

        private string darformato(object dato)
        {
            return string.Format(CultureInfo.GetCultureInfo("en-US"), this.formato, dato);
        }
        private string darformatoGuardar(object dato)
        {

            string var1 = string.Format(CultureInfo.GetCultureInfo("en-US"), this.formato, dato);
            var1 = var1.Replace(",", "");
            return var1;
        }

        private double toDouble(string texto)
        {

            if (texto == "")
            {
                return 0;
            }
            return double.Parse(texto, CultureInfo.GetCultureInfo("en-US")); ;
        }
        private int toEntero(string texto)
        {
            if (texto == "")
            {
                return 0;
            }
            return Int32.Parse(texto, CultureInfo.GetCultureInfo("en-US")); ;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) return;

            
            ComboBox aux = sender as ComboBox;
            int i= aux.SelectedIndex;
            switch (i)
            {
                case 0:// a4
                    txtAltura.Text = darformato("29.7");
                    txtAncho.Text = darformato("21");

                    txtAltura.Text = darformato("29.7");
                    txtAncho.Text = darformato("21");

                    //txtAltura.Text = darformato("21");
                    //txtAncho.Text = darformato("29.7");
                    altura = 1754;//842;
                    ancho = 1240;//595;


                    break;
                case 1://a5
                    txtAltura.Text = darformato("21") ;
                    txtAncho.Text = darformato("14.8") ;
                    altura = 1240; //595;
                    ancho = 874;// 420;

                    //  1.748
                    break;
                case 2://a6
                    txtAltura.Text = darformato("14.8");
                    txtAncho.Text = darformato("10.5") ;
                    altura = 874;// 420;
                    ancho = 591;// 298;
                    //1.748  1.240
                    break;
                case 3:
                    txtAltura.Text = "";
                    txtAncho.Text = "";
                    break;

            }
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {

            Double d1 = Convert.ToDouble(txtAltura.Text);
            Double d2 = Convert.ToDouble(txtAncho.Text);
            int w= (int)( d1/ 0.0264583333333334D);
            int h= (int)(d2 / 0.0264583333333334D);
          
            int i = comboBox1.SelectedIndex;


            if (i == 0 || i==3)
            {
                formDiseño.panel4.Width = (int)(Convert.ToDouble(txtAncho.Text) / 0.0264583333333334D);
                formDiseño.panel4.Height = (int)(Convert.ToDouble(txtAltura.Text) / 0.0264583333333334D);
                txtAltura.Text = "";
                txtAncho.Text = "";

            }
            else
            {
                formDiseño.panel4.Width = (int)(Convert.ToDouble(txtAltura.Text) / 0.0264583333333334D);
                formDiseño.panel4.Height = (int)(Convert.ToDouble(txtAncho.Text) / 0.0264583333333334D);
                txtAltura.Text = "";
                txtAncho.Text = "";


            }
           

            this.Close();
        }
    }
}
