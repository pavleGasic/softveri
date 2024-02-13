using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class UpdateCustomerSO : SystemOperationBase
    {
        private readonly Customer customer;
        public int Result {  get; private set; }

        public UpdateCustomerSO(Customer customer)
        {
            this.customer = customer;
        }
        protected override void ExecuteConcreteOperation()
        {
            Result = broker.UpdateCustomer(customer);
        }
    }
}
