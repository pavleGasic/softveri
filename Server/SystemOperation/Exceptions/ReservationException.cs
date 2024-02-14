using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation.Exceptions
{
    internal class ReservationException : Exception
    {
        public string ErrorMessage;
        public ReservationException(string message) : base(message)
        {
            ErrorMessage = message;
        }
    }
}
