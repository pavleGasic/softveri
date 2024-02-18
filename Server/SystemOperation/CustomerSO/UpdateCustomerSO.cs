using Common.Domain;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class UpdateCustomerSO : SystemOperationBase
    {
        protected override void ExecuteConcreteOperation(IEntity entity)
        {
            try
            {
                Customer customer = (Customer)entity;
                ValidationHelper.ValidateCustomer(customer);
                genericRepository.Update(customer);
                Result = entity;
            }
            catch (Exception)
            {
                throw new Exception("System cannot update customer");
            }
        }
    }
}
