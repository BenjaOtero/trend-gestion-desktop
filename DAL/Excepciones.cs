using System;

namespace DAL
{
    public class ServidorMysqlInaccesibleException : Exception
    {
        public ServidorMysqlInaccesibleException()
        {
        }

        public ServidorMysqlInaccesibleException(string message)
            : base(message)
        {
        }

        public ServidorMysqlInaccesibleException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
