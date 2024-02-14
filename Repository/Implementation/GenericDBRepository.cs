using Repository.DBConnection;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class GenericDBRepository : IDBRepository<IEntity>
    {
        public void Add(IEntity entity)
        {
            using(SqlCommand cmd = CreateSqlCommand($"INSERT INTO {entity.TableName} OUTPUT inserted.{entity.IDName} VALUES ({entity.InsertValues})"))
            {
                foreach (var parameter in CreateParameters(entity, "Insert"))
                {
                    cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                }
                object pkValue = cmd.ExecuteScalar();
                entity.GetType().GetProperty(entity.IDName).SetValue(entity, pkValue);
            }
        }
        public void Delete(IEntity entity, string condition)
        {
            using (SqlCommand cmd = CreateSqlCommand($"DELETE FROM {entity.TableName} OUTPUT deleted.{entity.IDName} WHERE {condition}"))
            {
                foreach (var parameter in CreateParameters(entity, "Delete"))
                {
                    cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                }
                object pkValue = cmd.ExecuteScalar();
                if (pkValue != null)
                {
                    entity.GetType().GetProperty(entity.IDName).SetValue(entity, pkValue);
                }
                else
                {
                    throw new Exception($"Database error while deleting in {entity.TableName}");
                }
            }
        }

        public List<IEntity> Find(IEntity entity, string condition)
        {
            return ExecuteSelectQuery(entity, condition);
        }

        public IEntity Get(IEntity entity, string condition)
        {
            List<IEntity> result = ExecuteSelectQuery(entity, condition);
            return result.FirstOrDefault();
        }

        public List<IEntity> GetAll(IEntity entity)
        {
            return ExecuteSelectQuery(entity, string.Empty);
        }
        public void Update(IEntity entity)
        {
            using (SqlCommand cmd = CreateSqlCommand($"UPDATE {entity.TableName} SET {entity.UpdateValues} WHERE {entity.UpdateCondition}"))
            {
                foreach (var parameter in CreateParameters(entity, "Update"))
                {
                    cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                }
                if (cmd.ExecuteNonQuery() != 1) throw new Exception($"Database error while updating: {entity.TableName}")   ;
            }
        }

        #region helper methods

        private SqlCommand CreateSqlCommand(string query)
        {
            SqlCommand cmd = DBConnectionFactory.Instance.GetDBConnection().CreateCommand();
            cmd.CommandText = query;
            return cmd;
        }
        private List<IEntity> ExecuteSelectQuery(IEntity entity, string condition)
        {
            using (SqlCommand cmd = CreateSqlCommand($"SELECT {entity.SelectValues} FROM {entity.TableName} {entity.TableAlias} {entity.JoinTable}")) 
            {
                cmd.CommandText += !string.IsNullOrEmpty(condition) ? $"WHERE {condition}" : "";
                cmd.CommandText += !string.IsNullOrEmpty(entity.OrderBy) ? $"ORDER BY {entity.OrderBy}" : "";
                foreach(var parameter in CreateParameters(entity, "Select"))
                {
                    cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                }
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return entity.GetReaderList(reader);
                }
            }
        }

        private IEnumerable<SqlParameter> CreateParameters(IEntity entity, string operation)
        {
            var parameters = new List<SqlParameter>();

            foreach (PropertyInfo property in entity.GetType().GetProperties())
            {
                var attr = (IncludeInParametersAttribute)Attribute.GetCustomAttribute(property, typeof(IncludeInParametersAttribute));
                if (attr != null && attr.Operation.ToLower().Contains(operation.ToLower()))
                {
                    parameters.Add(new SqlParameter($"@{property.Name}", property.GetValue(entity)));
                }
            }

            return parameters;
        }
        #endregion

        public void OpenConnection()
        {
            DBConnectionFactory.Instance.GetDBConnection().OpenConnection();
        }
        public void CloseConnection()
        {
            DBConnectionFactory.Instance.GetDBConnection().CloseConnection();
        }
        public void BeginTransaction()
        {
            DBConnectionFactory.Instance.GetDBConnection().BeginTransaction();
        }
        public void Commit()
        {
            DBConnectionFactory.Instance.GetDBConnection().Commit();
        }
        public void Rollback()
        {
            DBConnectionFactory.Instance.GetDBConnection().Rollback();
        }
    }
}
