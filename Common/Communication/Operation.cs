using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Communication
{
    public enum Operation
    {
        Login,
        Register,
        AddCustomer,
        GetCustomers,
        UpdateCustomer,
        AddActor,
        GetGenres,
        GetActors,
        AddFilm,
        GetFilms,
        DeleteFilm,
        AddReservation,
        GetReservations,
        UpdateReservationStatus,
        DeleteReservation
    }
}
