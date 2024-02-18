using Common.Domain;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class LoginSO : SystemOperationBase
    {
        protected override void ExecuteConcreteOperation(IEntity entity)
        {
            try
            {
                Worker worker = (Worker)entity;
                Result = genericRepository.Get(worker, $"Username=@Username AND Password=@HashedPassword");
            }
            catch 
            {
                throw new Exception("Server side login error.");
            }
        }
    }
}
