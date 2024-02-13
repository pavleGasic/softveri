using System;

namespace Common.Domain
{
    [Serializable]
    public class ReservationItem
    {
        public int SerialNumber { get; set; }
        public double Price { get; set; }
        public Film Film { get; set; }
    }
}
