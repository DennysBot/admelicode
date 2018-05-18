using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class RootObject<T>
    {
        public int nro_registros { get; set; }
        public List<T> datos { get; set; }
    }

    public class RootObject<T,K>
    {
        public int nro_registros { get; set; }
        public List<T> datos { get; set; }
        public List<K> combinacion { get; set; }
    }
}
