using Common.Domain;
using DBBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class GetFilmsSO : SystemOperationBase
    {
        private readonly string search;

        public List<Film> Result { get; private set; }

        public GetFilmsSO(string search)
        {
            this.search = search;
        }
        protected override void ExecuteConcreteOperation()
        {
            Result = broker.GetFilms(search);
        }
    }
}
