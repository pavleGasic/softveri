using Common.Domain;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class GetCustomersSO : SystemOperationBase
    {
        protected override void ExecuteConcreteOperation(IEntity entity)
        {
            try
            {
                Customer actor = (Customer)entity;
                Result = genericRepository.Find(actor, "CONCAT(FirstName, ' ', LastName) LIKE @SearchFilter ").Cast<Customer>().ToList();
            }catch 
            {
                throw new Exception("System cannot get customers!");

            }
        }
    }
}
