using Common.Domain;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class DeleteFilmSO : SystemOperationBase
    {
        protected override void ExecuteConcreteOperation(IEntity entity)
        {
            try
            {
                Film film = (Film)entity;
                genericRepository.Delete(film, film.DeleteQuery);
                Result = film;
            }
            catch (SqlException)
            {
                throw new Exception("Cannot delete film if it exist in some reservation item!");
            }
            catch (Exception)
            {
                throw new Exception("System cannot delete film!");
            }
        }
    }
}
