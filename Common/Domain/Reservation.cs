using Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class Reservation : IEntity
    {
        [IncludeInParameters("Update,Delete")]
        public int ReservationId { get; set; }
        [IncludeInParameters("Insert")]
        public DateTime DateFrom { get; set; }
        [IncludeInParameters("Insert")]
        public DateTime DateTo { get; set; }
        [IncludeInParameters("Insert")]
        public double TotalPrice { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        [IncludeInParameters("Insert,Update")]
        [Browsable(false)]
        public string Status => ReservationStatus.ToString();
        public Worker Worker { get; set; }
        [IncludeInParameters("Select,Insert")]
        [Browsable(false)]
        public int WorkerId => Worker!=null? Worker.WorkerId : -1;
        [IncludeInParameters("Select,Insert")]
        [Browsable(false)]
        public int CustomerId => Customer != null ? Customer.CustomerId : -1;
        public Customer Customer { get; set; }
        public List<ReservationItem> ReservationItems { get; set; }
        public string CustomerName => $"{Customer.FirstName} {Customer.LastName}";

        [Browsable(false)]
        public string TableName => "Reservation";
        [Browsable(false)]
        public string IDName => "ReservationId";
        [Browsable(false)]
        public string TableAlias => "r";
        [Browsable(false)]
        public string InsertValues => "@DateFrom, @DateTo, @Status, @TotalPrice, @WorkerId, @CustomerId";
        [Browsable(false)]
        public string SelectValues => "r.*, c.FirstName, c.LastName, c.Email, ri.SerialNumber, ri.ItemPrice, f.* ";
        [Browsable(false)]
        public string UpdateValues => "Status=@Status";
        [Browsable(false)]
        public string JoinTable => "JOIN Customer c on(r.CustomerId = c.CustomerId) JOIN ReservationItem ri on (r.ReservationId = ri.ReservationId) JOIN Film f on(f.FilmId = ri.FilmId) ";
        [Browsable(false)]
        public string JoinCondition => "";
        [Browsable(false)]
        public string UpdateCondition => "ReservationId=@ReservationId";
        [Browsable(false)]
        public string DeleteQuery => " ReservationId=@ReservationId";

        [Browsable(false)]
        public string OrderBy => "r.status";
        public List<IEntity> GetReaderList(SqlDataReader reader)
        {
            Dictionary<int, IEntity> reservationDictionary = new Dictionary<int, IEntity>();
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
                        ((Reservation)reservationDictionary[reservationId]).ReservationItems.Add(item);
                    }
                }
            }
            finally { reader.Close(); }
            return reservationDictionary.Select(kvp => kvp.Value).ToList();
        }
    }
}
