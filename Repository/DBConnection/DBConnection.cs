﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DBConnection
{
    public class DBConnection
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        public DBConnection()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbFilmClub"].ConnectionString);
        }

        public void OpenConnection()
        {
            _connection.Open();
        }

        public void CloseConnection()
        {
            _connection.Close();
        }
        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }
        public void Rollback()
        {
            _transaction.Rollback();
        }
        public SqlCommand CreateCommand()
        {
            return new SqlCommand("", _connection, _transaction);
        }
        public bool IsReady()
        {
            return (_connection != null && _connection.State != ConnectionState.Closed);
        }
    }
}
