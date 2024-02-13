using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double TotalPrice { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public Worker Worker { get; set; }
        public Customer Customer { get; set; }
        public List<ReservationItem> ReservationItems { get; set; }
        public string CustomerName => $"{Customer.FirstName} {Customer.LastName}";

    }
}
