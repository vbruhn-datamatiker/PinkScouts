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

        private GenericRepo<User> _repo;

        #endregion

        #region Properties

        public GenericRepo<User> Repo { get; set; }

        #endregion

        #region Constructors

        public UserRepo()
        {
            _repo = new GenericRepo<User>();
        }

        public UserRepo (GenericRepo<User> repo)
        {
            _repo = repo;
        }



        #endregion

        #region Methods


        public User GetByEmail(string? userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Username cannot be null or empty.", nameof(userName));
            }

            User? user = _repo.FirstOrDefault(u => u.Email == userName);

            return user ?? throw new KeyNotFoundException($"User with username '{userName}' not found.");
        }

        #endregion




    }
}
