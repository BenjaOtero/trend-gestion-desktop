using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class ArticulosItems
    {
        private int? _idItem;
        private string _descripcion;
        private string _descripcionWeb;
        private int? _activoWeb;

        public int? IdItem { get { return _idItem; } set { _idItem = value; } }
        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public string DescripcionWeb { get { return _descripcionWeb; } set { _descripcionWeb = value; } }
        public int? ActivoWeb { get { return _activoWeb; } set { _activoWeb = value; } }


        public ArticulosItems()
        {
        }
    }
}
