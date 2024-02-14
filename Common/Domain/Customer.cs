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
    public class Customer : IEntity
    {
        [IncludeInParameters("Update")]
        public int CustomerId { get; set; }
        [IncludeInParameters("Insert,Update")]
        public string FirstName { get; set; }
        [IncludeInParameters("Insert,Update")]
        public string LastName { get; set; }
        [IncludeInParameters("Insert,Update")]
        public string Email { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        [Browsable(false)]
        public string TableName => "Customer";
        [Browsable(false)]
        public string IDName => "CustomerId";
        [Browsable(false)]
        public string TableAlias => "c";
        [Browsable(false)]
        public string InsertValues => "@FirstName, @LastName, @Email";
        [Browsable(false)]
        public string SelectValues => "*";
        [Browsable(false)]
        public string UpdateValues => "FirstName=@FirstName, LastName=@LastName, Email=@Email";
        [Browsable(false)]
        public string JoinTable => "";
        [Browsable(false)]
        public string JoinCondition => "";
        [Browsable(false)]
        public string UpdateCondition => "CustomerId=@CustomerId";
        [Browsable(false)]
        public string OrderBy => "LastName ASC";
        [Browsable(false)]
        [IncludeInParameters("Select")]
        public string SearchFilter { get; set; }
        public List<IEntity> GetReaderList(SqlDataReader reader)
        {
            List<IEntity> entityList = new List<IEntity>();
            while (reader.Read())
            {
                entityList.Add(new Customer()
                {
                    CustomerId = (int)reader["CustomerId"],
                    Email = (string)reader["Email"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"]
                });
            }
            return entityList;
        }
    }
}
