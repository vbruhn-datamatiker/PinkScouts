using PSClassLib.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSClassLib.Model
{
    public class Scout : User
    {
        #region instance fields
        private int _age;

		#endregion

		#region Properties
	

		public int Age
		{
			get { return _age; }
			set { _age = value; }
		}

		#endregion
		#region Constructors
		public Scout() : base()
		{
			_age = 0;
		}
		public Scout(int id, string firstName, string lastName, bool isLeader, string email, byte[] password, int age) : base(id, firstName, lastName, isLeader, email, password)
		{
			_age = age;
		}
		#endregion

		#region methods

		public override string ToString()
		{
			return base.ToString() + $", alder : {_age}";
		}
		#endregion

	}
}
