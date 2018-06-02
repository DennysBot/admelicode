using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admeli.Configuracion.Modificar
{
    class vineta : IEquatable<vineta> , IComparable<vineta>
    {
        public Label label { get; set; }
        public string nombre { get; set; }
        public int usado { get; set; }
        public int posicionX { get; set; }
        public int posicionY { get; set; }
        public bool mover { get; set; }
        public int inicialX { get; set; }
        public int inicialY { get; set; }
        public bool redimensionar { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public int  posicion { get; set; }
        public vineta()
        {
            this.usado = 0;// 0  no utilizado, 2 usadada en comprobante ,-1 no usadado esta abajo arriba,1usado arriba
            label = new Label();
            label.AutoSize = false;
            mover = false;

        }

        public override string ToString()
        {
            return "ID: " + posicion + "   Name: " + nombre;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            vineta objAsPart = obj as vineta;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public int SortByNameAscending(string name1, string name2)
        {

            return name1.CompareTo(name2);
        }

        // Default comparer for Part type.
        public int CompareTo(vineta comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart == null)
                return 1;

            else
                return this.posicion.CompareTo(comparePart.posicion);
        }
        public override int GetHashCode()
        {
            return posicion;
        }
        public bool Equals(vineta other)
        {
            if (other == null) return false;
            return (this.posicion.Equals(other.posicion));
        }
    }
}
