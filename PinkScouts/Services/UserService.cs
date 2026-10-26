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

        private readonly UserRepo _repo;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public UserService(UserRepo repo)
        {
            _repo = repo;
        }
        #endregion

        #region methods



        public bool ValidateUser(string userName, string password)
        {
            try
            {
                User user = _repo.GetByEmail(userName);

                using (SHA256 mySHA256 = SHA256.Create())
                {
                    byte[] passwordCode = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(password));

                    return user?.Password.SequenceEqual(passwordCode) ?? false;
                }
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }


        #endregion

    }
}
