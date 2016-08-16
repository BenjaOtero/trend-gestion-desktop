using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Colores
    {
        private int? _idColor;
        private string _descripcion;
        private string _hexCOL;

        public int? IdColor { get { return _idColor; } set { _idColor = value; } }
        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public string HexCOL { get { return _hexCOL; } set { _hexCOL = value; } }

        public Colores()
        {
        }
    }
}
