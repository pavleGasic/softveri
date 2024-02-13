using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class Film
    {
        public int FilmId { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public Genre Genre { get; set; }
        public List<Actor> Actors { get; set; }
        public string ImageUrl { get; set; }
        public double PricePerDay { get; set; }

    }
}
