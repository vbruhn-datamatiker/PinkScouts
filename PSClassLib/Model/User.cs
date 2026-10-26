using PSClassLib.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSClassLib.Model
{
    public class User : IHasId
    {
        #region instance fields
        private int _id;
        private string _firstName;
        private string _lastName;
        private bool _isLeader;
        private string _email;
        private byte[] _password;
        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public bool IsLeader
        {
            get { return _isLeader; }
            set { _isLeader = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public byte[] Password
        {
            get { return _password; }
            set { _password = value; }
        }



        #endregion

        #region Constructors

        public User()
        {
            _id = 0;
            _firstName = string.Empty;
            _lastName = string.Empty;
            _isLeader = false;
            _email = string.Empty;
            _password = null;
        }

        public User(int id, string firstName, string lastName, bool isLeader, string email, byte[] password)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _isLeader = isLeader;
            _email = email;
            _password = password;
        }

        #endregion

        #region methods

        public override string ToString()
        {
            string role = "Spejder";
            if (IsLeader)
            {
                role = "Leder";
            }

            return $"Navn: {FirstName} {LastName}, Email: {Email}, rolle: {role}";
        }

        #endregion

    }
}
