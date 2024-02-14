using Common.Domain;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class AddActorSO : SystemOperationBase
    {
        protected override void ExecuteConcreteOperation(IEntity entity)
        {
            try
            {
                Actor actor = (Actor)entity;
                ValidationHelper.ValidateActor(actor);
                genericRepository.Add(actor);
                Result = actor;
            }
            catch
            {
                throw new Exception("System cannot create new actor!");
            }
        }
    }
}
