using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PSClassLib.Model
{
    public class PSDBConnection
    {
        #region instance fields

        SqlConnection _connection;

        // readonly because we don't want to change the connection string after it's set. We want to keep it secure and not allow it to be modified.
        private readonly string _connectionString;

        #endregion

        #region constructors

        public PSDBConnection()
        {

            //Setting up our secret connection string

            var config = new ConfigurationBuilder()

                .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
                .Build();


            _connectionString = config["RollatorbandensSecretString"];


        }

        public PSDBConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion

        #region Properties

        public SqlConnection Connection
        {
            get {  return _connection; }

        }



        #endregion

        #region Methods

        public void ConnectToDatabase()
        {

            _connection = new SqlConnection(_connectionString);

            _connection.Open();

        }

        public void DisconnectFromDatabase()
        {
            _connection.Close();
        }

        #endregion



    }
}
