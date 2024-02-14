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
    public class Genre : IEntity
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        [Browsable(false)]
        public string TableName => "Genre";
        [Browsable(false)]
        public string IDName => "GenreId";
        [Browsable(false)]
        public string TableAlias => "g";
        [Browsable(false)]
        public string InsertValues => "";
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
            List<IEntity> entityList = new List<IEntity>();
            while (reader.Read())
            {
                entityList.Add(new Genre()
                {
                    GenreId = (int)reader["GenreId"],
                    GenreName = (string)reader["GenreName"]
                });
            }
            return entityList;
        }
    }
}
