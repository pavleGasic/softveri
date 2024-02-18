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
    internal class GetFilmsSO : SystemOperationBase
    {
        protected override void ExecuteConcreteOperation(IEntity entity)
        {
            try
            {
                Film film = (Film)entity;
                Result = genericRepository.Find(film, "f.Title LIKE @SearchFilter ").Cast<Film>().ToList();
            }
            catch 
            {
                throw new Exception("System cannot get films!");
            }
        }
    }
}
