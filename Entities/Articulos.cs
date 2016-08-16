using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Articulos
    {
        private string _idArticulo;
        private int? _idItem;
        private string _idGenero;
        private int? _idColor;
        private int? _idAlicuota;
        private string _talle;
        private int? _idProveedor;
        private string _descripcion;
        private string _descripcionWeb;
        private decimal? _precioCosto;
        private decimal? _precioPublico;
        private decimal? _precioMayor;
        private DateTime? _fecha;
        private string _imagen;
        private string _imagenBack;
        private string _imagenColor;
        private int? _activoWeb;
        private int? _nuevoWeb;
        private string _proveedor;

        public string IdArticulo { get { return _idArticulo; } set { _idArticulo = value; } }
        public int? IdItem { get { return _idItem; } set { _idItem = value; } }
        public int? IdColor { get { return _idColor; } set { _idColor = value; } }
        public int? IdAlicuota { get { return _idAlicuota; } set { _idAlicuota = value; } }
        public string IdGenero { get { return _idGenero; } set { _idGenero = value; } }
        public string Talle { get { return _talle; } set { _talle = value; } }
        public int? IdProveedor { get { return _idProveedor; } set { _idProveedor = value; } }
        public string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public string DescripcionWeb { get { return _descripcionWeb; } set { _descripcionWeb = value; } }
        public decimal? PrecioCosto { get { return _precioCosto; } set { _precioCosto = value; } }
        public decimal? PrecioPublico { get { return _precioPublico; } set { _precioPublico = value; } }
        public decimal? PrecioMayor { get { return _precioMayor; } set { _precioMayor = value; } }
        public DateTime? Fecha { get { return _fecha; } set { _fecha = value; } }
        public string Imagen { get { return _imagen; } set { _imagen = value; } }
        public string ImagenBack { get { return _imagenBack; } set { _imagenBack = value; } }
        public string ImagenColor { get { return _imagenColor; } set { _imagenColor = value; } }
        public int? ActivoWeb { get { return _activoWeb; } set { _activoWeb = value; } }
        public int? NuevoWeb { get { return _nuevoWeb; } set { _nuevoWeb = value; } }
        public string Proveedor { get { return _proveedor; } set { _proveedor = value; } }

        public Articulos()
        {
        }

    }
}
