using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class FormasPago
    {
        private int? _idForma;
        private string _descripcion;

        public int? IdForma { get { return _idForma; } set { _idForma = value; } }
        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } } 
        
        public FormasPago()
        {
        }

    }
}
