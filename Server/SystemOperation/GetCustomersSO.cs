using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class GetCustomersSO : SystemOperationBase
    {
        private readonly string search;
        public List<Customer> Result { get; private set; }

        public GetCustomersSO(string search)
        {
            this.search = search;
        }
        protected override void ExecuteConcreteOperation()
        {
            Result = broker.GetCustomers(search);
        }
    }
}
