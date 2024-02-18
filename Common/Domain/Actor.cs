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
    public class Actor : IEntity
    {
        public int ActorId { get; set; }
        [IncludeInParameters("Insert")]
        public string Name { get; set; }
        public Gender? Gender { get; set; }
        [IncludeInParameters("Insert")]
        [Browsable(false)]
        public string GenderString => Gender?.ToString()??"None";
        public string RoleName { get; set; }
        [Browsable(false)]
        public string TableName => "Actor";
        [Browsable(false)]
        public string IDName => "ActorId";
        [Browsable(false)]
        public string TableAlias => "a";
        [Browsable(false)]
        public string InsertValues => "@Name, @GenderString";
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
                entityList.Add(new Actor()
                {
                    ActorId = (int)reader["ActorId"],
                    Gender = (Gender)Enum.Parse(typeof(Gender), (string)reader["Gender"]),
                    Name = (string)reader["Name"]
                });
            }
            return entityList;
        }
    }
}
