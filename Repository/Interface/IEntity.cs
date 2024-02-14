using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IncludeInParametersAttribute : Attribute
    {
        public string Operation { get; set; }
        public IncludeInParametersAttribute(string operation)
        {
            Operation = operation;
        }
    }
    public interface IEntity
    {
        string TableName {  get; }
        string IDName {  get; }
        string TableAlias { get; }
        string InsertValues { get; }
        string SelectValues { get; }
        string UpdateValues { get; }
        string UpdateCondition {  get; }
        string JoinTable { get; }
        string JoinCondition { get; }
        string OrderBy { get; }
        List<IEntity> GetReaderList(SqlDataReader reader);
    }
}
