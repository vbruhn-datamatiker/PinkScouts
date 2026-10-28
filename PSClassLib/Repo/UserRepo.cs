using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Interface;
using PSClassLib.Model;

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


        public User GetByEmail(string? userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Username cannot be null or empty.", nameof(userName));
            }

            // Connect to database, and select user 

            User user = null;

            //user = _repo.FirstOrDefault(u => u.Email == userName);

            return user ?? throw new KeyNotFoundException($"User with username '{userName}' not found.");
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
