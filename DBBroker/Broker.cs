using Common.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DBBroker
{
    public class Broker
    {
        private DBConnection _connection;

        public Broker()
        {
            _connection = new DBConnection();
        }
        #region Connection operation
        public void OpenConnection()
        {
            _connection.OpenConnection();
        }

        public void CloseConnection()
        {
            _connection.CloseConnection();
        }

        public void BeginTransaction()
        {
            _connection.BeginTransaction();
        }

        public void CommitTransaction() 
        {
            _connection.Commit();
        }
        public void RollbackTransaction() 
        {
            _connection.Rollback();
        }
        #endregion

        public Worker Login(Worker worker)
        {
            SqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandText = @"SELECT * FROM Worker where username=@un and password=@pass";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@un", worker.Username);
            sqlCommand.Parameters.AddWithValue("@pass", DBHelper.HashPassword(worker.Password));
            SqlDataReader reader = sqlCommand.ExecuteReader();
            try
            {
                if (reader.Read()) 
                {
                    worker.FirstName = (string)reader["firstname"];
                    worker.LastName = (string)reader["lastname"];
                    worker.WorkerId = (int)reader["workerid"];
                    return worker;
                }
            }finally { reader.Close(); }
            return null;
        }

        public int Register(Worker worker)
        {
            SqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandText = @"INSERT INTO Worker OUTPUT inserted.workerId VALUES (@fn, @ln, @un, @pass)";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@fn", worker.FirstName);
            sqlCommand.Parameters.AddWithValue("@ln", worker.LastName);
            sqlCommand.Parameters.AddWithValue("@un", worker.Username);
            sqlCommand.Parameters.AddWithValue("@pass", DBHelper.HashPassword(worker.Password));
            return (int)sqlCommand.ExecuteScalar();
        }

        public int AddCustomer(Customer customer)
        {
            SqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandText = @"INSERT INTO Customer OUTPUT inserted.customerId VALUES (@fn, @ln, @email)";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@fn", customer.FirstName);
            sqlCommand.Parameters.AddWithValue("@ln", customer.LastName);
            sqlCommand.Parameters.AddWithValue("@email", customer.Email);
            object result = sqlCommand.ExecuteScalar();
            return result != null ? (int)result : -1;
        }

        public List<Customer> GetCustomers(string search)
        {
            SqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandText = @"SELECT * FROM Customer WHERE CONCAT(firstname , ' ', lastname) LIKE @sr OR CONCAT(firstname, lastname) LIKE @sr ORDER BY lastname;";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@sr", "%" + search + "%");
            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<Customer> list = new List<Customer>();
            try
            {
                while (reader.Read())
                {
                    var listItem = new Customer()
                    {
                        CustomerId = (int)reader["CustomerId"],
                        Email = (string)reader["Email"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"]
                    };
                    list.Add(listItem);
                }
            }
            finally { reader.Close(); }
            return list;
        }

        public int UpdateCustomer(Customer customer)
        {
            SqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandText = @"UPDATE Customer SET firstname=@fn, lastname=@ln, email=@email OUTPUT inserted.customerid WHERE customerId=@cid";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@fn", customer.FirstName);
            sqlCommand.Parameters.AddWithValue("@ln", customer.LastName);
            sqlCommand.Parameters.AddWithValue("@email", customer.Email);
            sqlCommand.Parameters.AddWithValue("@cid", customer.CustomerId);
            object result = sqlCommand.ExecuteScalar();
            return result != null ? (int)result : -1;
        }

        public int AddActor(Actor actor)
        {
            SqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandText = @"INSERT INTO Actor OUTPUT inserted.actorId VALUES (@name, @gender)";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@name", actor.Name);
            sqlCommand.Parameters.AddWithValue("@gender", actor.Gender.ToString());
            object result = sqlCommand.ExecuteScalar();
            return result != null ? (int)result : -1;
        }

        public List<Genre> GetGenres()
        {
            SqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM Genre;";
            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<Genre> list = new List<Genre>();
            try
            {
                while (reader.Read())
                {
                    var listItem = new Genre()
                    {
                        GenreId = (int)reader["GenreId"],
                        GenreName = (string)reader["GenreName"]
                    };
                    list.Add(listItem);
                }
            }
            finally { reader.Close(); }
            return list;
        }

        public List<Actor> GetActors()
        {
            SqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM Actor;";
            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<Actor> list = new List<Actor>();
            try
            {
                while (reader.Read())
                {
                    var listItem = new Actor()
                    {
                        ActorId = (int)reader["ActorId"],
                        Gender = (Gender)Enum.Parse(typeof(Gender),(string)reader["Gender"]),
                        Name = (string)reader["Name"]
                    };
                    list.Add(listItem);
                }
            }
            finally { reader.Close(); }
            return list;
        }

        public int AddFilm(Film film)
        {
            SqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandText = @"INSERT INTO Film OUTPUT inserted.filmId VALUES (@title, @quantity, @genreId, @imageUrl, @prize)";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@title", film.Title);
            sqlCommand.Parameters.AddWithValue("@quantity", film.Quantity);
            sqlCommand.Parameters.AddWithValue("@genreId", film.Genre.GenreId);
            sqlCommand.Parameters.AddWithValue("@imageUrl", film.ImageUrl);
            sqlCommand.Parameters.AddWithValue("@prize", film.PricePerDay);
            object result = sqlCommand.ExecuteScalar();
            if(result == null)
            {
                return -1;
            }
            int filmId = (int)result;

            foreach(var actorRole in film.Actors)
            {
                sqlCommand = _connection.CreateCommand();
                sqlCommand.CommandText = @"INSERT INTO FilmRole OUTPUT inserted.actorId VALUES (@actorId, @filmId, @roleName)";
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@actorId", actorRole.ActorId);
                sqlCommand.Parameters.AddWithValue("@filmId", filmId);
                sqlCommand.Parameters.AddWithValue("@roleName", actorRole.RoleName);
                if(sqlCommand.ExecuteScalar() == null)
                {
                    return -1;
                }
            }
            return filmId;
        }

        public List<Film> GetFilms(string search)
        {
            SqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandText = "SELECT f.*, fr.actorId, fr.roleName, g.genreName, a.Name, a.Gender FROM Film f LEFT JOIN FilmRole fr ON f.filmId = fr.filmId JOIN Genre g ON (f.genreId = g.genreId) JOIN Actor a ON (a.ActorId = fr.ActorId) WHERE f.Title LIKE @sr UNION SELECT f.*, fr.actorId, fr.roleName, g.genreName, NULL, NULL FROM Film f LEFT JOIN FilmRole fr ON (f.FilmId = fr.FilmId) JOIN Genre g ON (f.GenreId = g.GenreId) WHERE fr.ActorId IS NULL AND f.Title LIKE @sr;";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@sr", "%" + search + "%");
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Dictionary<int, Film> filmDictionary = new Dictionary<int, Film>();
            try
            {
                while (reader.Read())
                {
                    int filmId = (int)reader["filmId"];
                    if ( !filmDictionary.ContainsKey(filmId) )
                    {
                        Film film = new Film()
                        {
                            FilmId = filmId,
                            Genre = new Genre()
                            {
                                GenreId = (int)reader["genreId"],
                                GenreName = (string)reader["genreName"]
                            },
                            Title = (string)reader["title"],
                            ImageUrl = (string)reader["imageUrl"],
                            Quantity = (int)reader["quantity"],
                            Actors = new List<Actor>(),
                            PricePerDay = (double)reader["PricePerDay"]
                        };
                        filmDictionary.Add(filmId, film);
                    }
                    int actorId = reader["actorId"] as int? ?? -1;
                    if (actorId != -1)
                    {
                        Actor actor = new Actor()
                        {
                            ActorId = actorId,
                            Gender = (Gender)Enum.Parse(typeof(Gender), (string)reader["Gender"]),
                            Name = (string)reader["name"],
                            RoleName = (string)reader["roleName"]
                        };
                        filmDictionary[filmId].Actors.Add(actor);
                    }                  
                }
            }
            finally { reader.Close(); }
            return filmDictionary.Select(kvp => kvp.Value).ToList();
        }

        public int DeleteFilm(Film film)
        {
            SqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM Film OUTPUT deleted.filmId WHERE filmId = @filmId";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@filmId", film.FilmId);
            object result = sqlCommand.ExecuteScalar();
            return result != null ? (int)result : -1;
        }

        public int AddReservation(Reservation reservation)
        {
            SqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandText = @"INSERT INTO Reservation OUTPUT inserted.reservationId VALUES (@dateFrom, @dateTo, @status, @prize, @workerId, @customerId)";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@dateFrom", reservation.DateFrom);
            sqlCommand.Parameters.AddWithValue("@dateTo", reservation.DateTo);
            sqlCommand.Parameters.AddWithValue("@status", reservation.ReservationStatus.ToString());
            sqlCommand.Parameters.AddWithValue("@prize", reservation.TotalPrice);
            sqlCommand.Parameters.AddWithValue("@workerId", reservation.Worker.WorkerId);
            sqlCommand.Parameters.AddWithValue("@customerId", reservation.Customer.CustomerId);
            object result = sqlCommand.ExecuteScalar();
            if (result == null)
            {
                return -1;
            }
            int reservationId = (int)result;

            foreach (var reservationItem in reservation.ReservationItems)
            {
                sqlCommand = _connection.CreateCommand();
                sqlCommand.CommandText = @"INSERT INTO ReservationItem OUTPUT inserted.reservationId VALUES (@sno, @rId, @itPrize, @fId)";
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@sno", reservationItem.SerialNumber);
                sqlCommand.Parameters.AddWithValue("@rId", reservationId);
                sqlCommand.Parameters.AddWithValue("@itPrize", reservationItem.Price);
                sqlCommand.Parameters.AddWithValue("@fId", reservationItem.Film.FilmId);
                if (sqlCommand.ExecuteScalar() == null)
                {
                    return -1;
                }
                sqlCommand = _connection.CreateCommand();
                sqlCommand.CommandText = @"UPDATE Film SET Quantity=@q OUTPUT inserted.filmId WHERE FilmId=@id";
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@q", reservationItem.Film.Quantity-1);
                sqlCommand.Parameters.AddWithValue("@id", reservationItem.Film.FilmId);
                if (sqlCommand.ExecuteScalar() == null)
                {
                    return -1;
                }
            }
            return reservationId;
        }

        public List<Reservation> GetReservations(Worker worker)
        {
            SqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandText = @"Select r.*, c.Firstname, c.Lastname, c.Email, ri.SerialNumber, ri.ItemPrice, f.* FROM Reservation r join Customer c on(r.CustomerId = c.CustomerId) join ReservationItem ri on (r.ReservationId = ri.ReservationId) JOIN Film f on(f.FilmId = ri.FilmId) where r.WorkerId = @wId order by r.status";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@wId", worker.WorkerId);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Dictionary<int, Reservation> reservationDictionary = new Dictionary<int, Reservation>();
            try
            {
                while (reader.Read())
                {
                    int reservationId = (int)reader["reservationId"];
                    if (!reservationDictionary.ContainsKey(reservationId))
                    {
                        Reservation reservation = new Reservation()
                        {
                            ReservationId = reservationId,
                            DateFrom = (DateTime)reader["dateFrom"],
                            DateTo = (DateTime)reader["dateTo"],
                            ReservationStatus = (ReservationStatus)Enum.Parse(typeof(ReservationStatus), (string)reader["status"]),
                            TotalPrice = (double)reader["Price"],
                            Customer = new Customer
                            {
                                CustomerId = (int)reader["customerId"],
                                Email = (string)reader["email"],
                                FirstName = (string)reader["firstName"],
                                LastName = (string)reader["lastName"],
                            },
                            Worker = worker,
                            ReservationItems = new List<ReservationItem>()
                        };
                        reservationDictionary.Add(reservationId, reservation);
                    }
                    int serialNo = reader["serialNumber"] as int? ?? -1;
                    if (serialNo != -1)
                    {
                        ReservationItem item = new ReservationItem()
                        {
                            SerialNumber = serialNo,
                            Price = (double)reader["itemPrice"],
                            Film = new Film
                            {
                                FilmId = (int)reader["filmId"],
                                ImageUrl = (string)reader["imageUrl"],
                                PricePerDay = (double)reader["pricePerDay"],
                                Quantity = (int)reader["quantity"],
                                Title = (string)reader["title"],
                            } 
                        };
                        reservationDictionary[reservationId].ReservationItems.Add(item);
                    }
                }
            }
            finally { reader.Close(); }
            return reservationDictionary.Select(kvp => kvp.Value).ToList();
        }

        public int UpdateReservationStatus(Reservation reservation)
        {
            SqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandText = @"UPDATE Reservation SET Status=@rstat OUTPUT inserted.reservationId WHERE reservationId=@rid";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@rstat", reservation.ReservationStatus.ToString());
            sqlCommand.Parameters.AddWithValue("@rid", reservation.ReservationId);
            object result = sqlCommand.ExecuteScalar();
            if(reservation.ReservationStatus == ReservationStatus.Returned)
            {
                foreach(var reservationItem in reservation.ReservationItems)
                {
                    sqlCommand = _connection.CreateCommand();
                    sqlCommand.CommandText = @"UPDATE Film SET Quantity=@q OUTPUT inserted.filmId WHERE filmId=@fid";
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@q", reservationItem.Film.Quantity + 1);
                    sqlCommand.Parameters.AddWithValue("@fid", reservationItem.Film.FilmId);
                    if(sqlCommand.ExecuteScalar() == null)
                    {
                        return -1;
                    }
                }
            }
            return result != null ? (int)result : -1;
        }

        public int DeleteReservation(Reservation reservation)
        {
            SqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM Reservation OUTPUT deleted.reservationId WHERE ReservationId=@id";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@id", reservation.ReservationId);
            object result = sqlCommand.ExecuteScalar();
            return result != null ? (int)result : -1;
        }
    }
}
