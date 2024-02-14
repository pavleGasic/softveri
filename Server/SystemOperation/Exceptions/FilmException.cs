using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation.Exceptions
{
    internal class FilmException : Exception
    {
        public string ErrorMessage;
        public FilmException(string message) : base(message)
        {
            ErrorMessage = message;
        }
    }
}
