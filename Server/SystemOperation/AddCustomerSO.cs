using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class AddCustomerSO : SystemOperationBase
    {
        private readonly Customer customer;

        public int Result { get; private set; }

        public AddCustomerSO(Customer customer)
        {
            this.customer = customer;
        }

        protected override void ExecuteConcreteOperation()
        {
            Result = broker.AddCustomer(customer);
        }
    }
}
