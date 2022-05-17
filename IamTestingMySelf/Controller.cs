using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace IamTestingMySelf
{
    class Controller
    {
        private SqlConnection sqlConnection = new SqlConnection();


        public void Connect(string connectionString)
        {
            if (connectionString == String.Empty || connectionString == null)
                throw new Exception("Неверная строка подключения");
            else
                sqlConnection.ConnectionString = connectionString;
        }

        public string GetMachine
        { get => this.sqlConnection.WorkstationId; }

        
        public void RunProcedure(string sqlExpression)
        {

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Connection.Open();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = sqlExpression;
            int i = sqlCommand.ExecuteNonQuery();
            sqlCommand.Connection.Close();
        }

        public void DeleteData(string sqlExpression = "DROP TABLE Products, Categories")
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Connection.Open();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = sqlExpression;
        }

    }
}
