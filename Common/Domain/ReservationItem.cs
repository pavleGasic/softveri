using Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;

namespace Common.Domain
{
    [Serializable]
    public class ReservationItem : IEntity
    {
        [IncludeInParameters("Insert")]
        public int SerialNumber { get; set; }
        [Browsable(false)]
        [IncludeInParameters("Insert")]
        public int ReservationId { get; set; }
        [IncludeInParameters("Insert")]
        public double Price { get; set; }
        public Film Film { get; set; }
        [Browsable(false)]
        [IncludeInParameters("Insert")]
        public int FilmId => Film != null ? Film.FilmId : -1;
        [Browsable(false)]
        public string TableName => "ReservationItem";
        [Browsable(false)]
        public string IDName => "SerialNumber";
        [Browsable(false)]
        public string TableAlias => "ri";
        [Browsable(false)]
        public string InsertValues => " @SerialNumber, @ReservationId, @Price, @FilmId ";
        [Browsable(false)]
        public string SelectValues => "*";
        [Browsable(false)]
        public string UpdateValues => "";
        [Browsable(false)]
        public string JoinTable => "";
        [Browsable(false)]
        public string JoinCondition => "";
        [Browsable(false)]
        public string UpdateCondition => "";

        [Browsable(false)]
        public string OrderBy => "";
        public List<IEntity> GetReaderList(SqlDataReader reader)
        {
            var list = new List<IEntity>();
            while (reader.Read())
            {
                list.Add(new ReservationItem()
                {
                    Price = (double)reader["itemPrice"],
                    Film = new Film
                    {
                        FilmId = (int)reader["filmId"],
                        ImageUrl = (string)reader["imageUrl"],
                        PricePerDay = (double)reader["pricePerDay"],
                        Quantity = (int)reader["quantity"],
                        Title = (string)reader["title"],
                    }
                }
                );
            }
            return list;
        }
    }
}
