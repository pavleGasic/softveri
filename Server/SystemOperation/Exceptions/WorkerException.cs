using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation.Exceptions
{
    internal class WorkerException : Exception
    {
        public string ErrorMessage;
        public WorkerException(string message) : base(message)
        {
            ErrorMessage = message;
        }
    }
}
