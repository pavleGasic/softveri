using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class DeleteFilmSO : SystemOperationBase
    {
        private readonly Film film;
        public int Result { get; private set; }

        public DeleteFilmSO(Film film)
        {
            this.film = film;
        }

        protected override void ExecuteConcreteOperation()
        {
            Result = broker.DeleteFilm(film);
        }
    }
}
