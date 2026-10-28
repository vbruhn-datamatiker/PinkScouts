using Microsoft.Data.SqlClient;
using PSClassLib.Interface;
using PSClassLib.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSClassLib.Repo
{
    public class UserRepo : BasicDB, IRepo<User>
    {
        #region instance fields


        #endregion

        #region Properties
    
		#endregion

		#region Constructors


        public UserRepo(PSDBConnection db) : base(db)
        {

		}

        public User Create(User item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods


        public User GetByEmail(string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Username cannot be null or empty.", nameof(email));
            }

            // Connect to database, and select user 

            Db.ConnectToDatabase();

            string sql = "SELECT * FROM USER WHERE Email = @Email";

            SqlCommand cmd = new SqlCommand(sql, Db.Connection);
            SqlDataReader reader = cmd.ExecuteReader();
            User? user = null;

            if (reader.Read())
            {
                int id = (int)reader["UserId"];
                string firstName = (string)reader["FirstName"];
                string lastName = (string)reader["LastName"];
                bool isLeader = (bool)reader["IsLeader"];
                byte[] password = (byte[])reader["Password"];

                Db.DisconnectFromDatabase();
                return user = new User(id, firstName, lastName, isLeader, email, password);
            }
            else
            {
                Db.DisconnectFromDatabase();
                throw new KeyNotFoundException($"Bruger med mail '{email}' kunne ikke findes.");
            }

            
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> ReadAll()
        {
            throw new NotImplementedException();
        }

        public User Remove(int id)
        {
            throw new NotImplementedException();
        }

        public User Update(User item, int id)
        {
            throw new NotImplementedException();
        }


        #endregion




    }
}
