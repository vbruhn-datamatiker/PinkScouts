using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Interface;
using PSClassLib.Model;

namespace PSClassLib.Repo
{
    public class UserRepo
    {
        #region instance fields
        private List <User> _users;

		#endregion

		#region Properties
        public List<User> Users
		{
			get { return _users; }
			set { _users = value; }
		}

		#endregion

		#region Constructors

        public UserRepo()
		{
			_users = new List<User>();
		}
        public UserRepo(List<User> users)
        { 
        _users = users;
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

       
        #endregion




    }
}
