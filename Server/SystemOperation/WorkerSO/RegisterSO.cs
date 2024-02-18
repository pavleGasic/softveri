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
                ValidationHelper.ValidateRegister(worker);
                genericRepository.Add(worker);
                Result = worker;
            }
            catch(Exception ex) 
            {
                if(ex.Message.Contains("Cannot insert duplicate key row in object 'dbo.Worker' with unique index"))
                {
                    throw new Exception("There is more accounts with this username, please change it and try again!");
                }
                else
                {
                    throw new Exception("System cannot create new worker!");
                }
            }
        }
    }
}
