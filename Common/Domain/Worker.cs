using Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class Worker : IEntity
    {
        public int WorkerId { get; set; }
        [IncludeInParameters("Insert")]
        public string FirstName { get; set; }
        [IncludeInParameters("Insert")]
        public string LastName { get; set; }
        [IncludeInParameters("Insert, Select")]
        public string Username { get; set; }
        public string Password { get; set; }
        [IncludeInParameters("Insert, Select")]
        [Browsable(false)]
        public string HashedPassword => HashPassword(Password);

        [Browsable(false)]
        public string TableName => "Worker";
        [Browsable(false)]
        public string IDName => "WorkerId";
        [Browsable(false)]
        public string TableAlias => "w";
        [Browsable(false)]
        public string InsertValues => "@FirstName, @LastName, @Username, @HashedPassword";
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
                entityList.Add(new Worker()
                {
                    WorkerId = (int)reader["WorkerId"],
                    FirstName = (string)reader["Firstname"],
                    LastName = (string)reader["Lastname"],
                    Username = (string)reader["Username"]
                });
            }
            return entityList;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
