using Common.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ClientCommunication
{
    internal static class CommunicationUtils
    {
        public static T ParseResponse<T>(this Response response)
        {
            if (response.Exception == null)
            {
                return (T)response.Result;
            }
            else
            {
                throw response.Exception;
            }
        }
    }
}
