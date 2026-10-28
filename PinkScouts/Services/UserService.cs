using PSClassLib;
using PSClassLib.Interface;
using PSClassLib.Model;
using PSClassLib.Repo;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


namespace PSClassLib
{
    public class UserService
    {
        #region instance fields

        private readonly IRepo<User> _repo;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public UserService(IRepo<User> repo)
        {
            _repo = repo;
        }
        #endregion

        #region methods



        public User ValidateUser(int userId, string password)
        {

                User user = _repo.GetById(userId);
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    byte[] passwordCode = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
                if (user.Password.SequenceEqual(passwordCode))
                    return user;
                }       
                throw new UnauthorizedAccessException("Invalid password.");
        }


        #endregion

    }
}
