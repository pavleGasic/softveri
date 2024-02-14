using Common.Domain;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class RegisterSO : SystemOperationBase
    {
        protected override void ExecuteConcreteOperation(IEntity entity)
        {
            try
            {
                Worker worker = (Worker)entity;
                ValidationHelper.ValidateLogin(worker);
                genericRepository.Add(worker);
                Result = worker;
            }
            catch
            {
                throw new Exception("System cannot create new worker!");
            }
        }
    }
}
