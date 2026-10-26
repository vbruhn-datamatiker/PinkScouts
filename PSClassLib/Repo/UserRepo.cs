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



        #endregion

        #region Properties



        #endregion

        #region Constructors

   

        #endregion

        #region Methods


        public User GetByEmail(string? userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Username cannot be null or empty.", nameof(userName));
            }

            // Connect to database, and select user 

            User? user = _repo.FirstOrDefault(u => u.Email == userName);

            return user ?? throw new KeyNotFoundException($"User with username '{userName}' not found.");
        }

       
        #endregion




    }
}
