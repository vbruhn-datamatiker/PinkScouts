using Microsoft.Data.SqlClient;
using PSClassLib.Interface;
using PSClassLib.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Db.ConnectToDatabase();

            string sql = "SELECT * FROM USER WHERE UserId = @UserId";

            SqlCommand cmd = new SqlCommand(sql, Db.Connection);
            cmd.Parameters.AddWithValue("@UserId", id);
            SqlDataReader reader = cmd.ExecuteReader();
            User user = null;

            if (reader.Read())
            {
                int userId = (int)reader["UserId"];
                string firstName = (string)reader["FirstName"];
                string lastName = (string)reader["LastName"];
                bool isLeader = (bool)reader["IsLeader"];
                string email = (string)reader["Email"];
                byte[] password = (byte[])reader["Password"];

                user = new User(userId, firstName, lastName, isLeader, email, password);
            }
            else
            {
                Db.DisconnectFromDatabase();
                throw new Exception($"Bruger med {id} findes ikke.");
            }
            Db.DisconnectFromDatabase();
            return user;
        }

        public List<User> ReadAll()
        {
            List<User> userList = new List<User>();
            _db.ConnectToDatabase();

            string sql = "SELECT * FROM USER";
            SqlCommand cmd = new SqlCommand(sql, Db.Connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int userId = (int)reader["UserId"];
                string firstName = (string)reader["FirstName"];
                string lastName = (string)reader["LastName"];
                bool isLeader = (bool)reader["IsLeader"];
                string email = (string)reader["Email"];
                byte[] password = (byte[])reader["Password"];

              User  user = new User(userId, firstName, lastName, isLeader, email, password);
            
              userList.Add(user);
            }

            Db.DisconnectFromDatabase();
            return userList;
        }

        public User Create(User user)
        {
            Db.ConnectToDatabase();
            
            string sql = $"INSERT into User (FirstName, LastName, IsLeader,Email,Password) OUTPUT INSERTED.UserId VALUES (@FirstName, @LastName, @IsLeader, @Email, @Password)";

            SqlCommand cmd = new SqlCommand(sql, Db.Connection);

            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@IsLeader", user.IsLeader);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);

            int id = (int)cmd.ExecuteScalar();

            if (id == 0)
            {
                throw new KeyNotFoundException($"Bruger med {user.Id} findes ikke");
            }

            user.Id = id;

            _db.DisconnectFromDatabase();

            return user;

        }

        public User Remove(int idToRemove)
        {
            User user = GetById(idToRemove);
            _db.ConnectToDatabase();
            string sql = $"DELETE FROM USER WHERE UserId = @UserId";

            SqlCommand cmd = new SqlCommand(sql, _db.Connection);

            cmd.Parameters.AddWithValue("@UserId", idToRemove);

            int row = cmd.ExecuteNonQuery();
            _db.DisconnectFromDatabase();

            if (row == 1)
            {
                return user;
            }
            else
            {
                throw new KeyNotFoundException($"User med {idToRemove} findes ikke");
            }

        }

        public User Update(User updatedUser, int id)
        {
            _db.ConnectToDatabase();

            string sql = "UPDATE USER SET FirstName = @FirstName, LastName = @LastName, IsLeader = @IsLeader, Email = @Email, Password = @Password WHERE UserId = @UserId";
            SqlCommand cmd = new SqlCommand(sql, _db.Connection);

            cmd.Parameters.AddWithValue("@FirstName", updatedUser.FirstName);
            cmd.Parameters.AddWithValue("@LastName", updatedUser.LastName);
            cmd.Parameters.AddWithValue("@IsLeader", updatedUser.IsLeader);
            cmd.Parameters.AddWithValue("@Email", updatedUser.Email);
            cmd.Parameters.AddWithValue("@Password", updatedUser.Password);
            cmd.Parameters.AddWithValue("@UserId", id);

            updatedUser.Id = id; // Ensure the updated user has the correct ID

            int row = cmd.ExecuteNonQuery();
            _db.DisconnectFromDatabase();

            if (row == 1)
            {
                return updatedUser;
            }
            else
            {
                throw new KeyNotFoundException($"Bruger med {id} findes ikke");
            }

        }


        #endregion




    }
}
