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
    internal class AddFilmSO : SystemOperationBase
    {
        protected override void ExecuteConcreteOperation(IEntity entity)
        {
            try
            {
                Film film = (Film)entity;
                ValidationHelper.ValidateFilm(film);
                genericRepository.Add(film);
                foreach(var actor in film.Actors)
                {
                    FilmRole filmRole = new FilmRole()
                    {
                        FilmId = film.FilmId,
                        ActorId = actor.ActorId,
                        RoleName = actor.RoleName,
                    };
                    genericRepository.Add(filmRole);
                }
                Result = film;
            }
            catch
            {
                throw new Exception("System cannot create new film!");
            }
        }
    }
}
