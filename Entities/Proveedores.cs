using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Proveedores
    {
        private int? _idProveedor;
        private string _razonSocial;
        private string _direccion;
        private string _codigoPostal;
        private string _telefono;
        private string _contacto;

        public int? IdProveedor { get { return _idProveedor; } set { _idProveedor = value; } }
        public string RazonSocial { get { return _razonSocial; } set { _razonSocial = value; } }
        public string Direccion { get { return _direccion; } set { _direccion = value; } }
        public string CodigoPostal { get { return _codigoPostal; } set { _codigoPostal = value; } }
        public string Telefono { get { return _telefono; } set { _telefono = value; } }
        public string Contacto { get { return _contacto; } set { _contacto = value; } } 

        public Proveedores()
        {
        }
    }
}
