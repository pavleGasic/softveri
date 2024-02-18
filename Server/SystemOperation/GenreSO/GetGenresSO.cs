using Common.Domain;
using DBBroker;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class GetGenresSO : SystemOperationBase
    {
        protected override void ExecuteConcreteOperation(IEntity entity)
        {
            try
            {
                Genre genre = (Genre)entity;
                Result = genericRepository.GetAll(genre).Cast<Genre>().ToList();
            }
            catch
            {
                throw new Exception("System cannot get genres!");
            }
        }
    }
}
