using Common.Domain;
using DBBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class AddFilmSO : SystemOperationBase
    {
        private readonly Film film;
        public int Result { get; private set; }

        public AddFilmSO(Film film)
        {
            this.film = film;
        }

        protected override void ExecuteConcreteOperation()
        {
            Result = broker.AddFilm(film);
        }
    }
}
