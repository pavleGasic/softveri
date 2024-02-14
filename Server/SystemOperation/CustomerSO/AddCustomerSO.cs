using Common.Domain;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class AddCustomerSO : SystemOperationBase
    {
        protected override void ExecuteConcreteOperation(IEntity entity)
        {
            try
            {
                Customer customer = (Customer)entity;
                ValidationHelper.ValidateCustomer(customer);
                genericRepository.Add(customer);
                Result = customer;
            }
            catch
            {
                throw new Exception("System cannot create new customer!");
            }
        }
    }
}
