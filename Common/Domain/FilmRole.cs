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
    public class FilmRole : IEntity
    {
        [IncludeInParameters("Insert")]
        public int FilmId { get; set; }
        [IncludeInParameters("Insert")]
        public int ActorId { get; set; }
        [IncludeInParameters("Insert")]
        public string RoleName { get; set; }

        [Browsable(false)]
        public string TableName => "FilmRole";
        [Browsable(false)]
        public string IDName => "FilmId";
        [Browsable(false)]
        public string TableAlias => "";
        [Browsable(false)]
        public string InsertValues => "@ActorId, @FilmId, @RoleName";
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
            return null;
        }
    }
}
