using PSClassLib.Interface;
using PSClassLib.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSClassLib.Model
{
    public class Scout : User
    {
        #region instance fields
        private int _age;
		private List<Skill> _skills;
		#endregion

		#region Properties
	

		public int Age
		{
			get { return _age; }
			set { _age = value; }
		}
		public List<Skill> Skills 
		{  
			get { return _skills; } 
			set { _skills = value; } 
		}

		#endregion
		#region Constructors
		public Scout() : base()
		{
			_age = 0;
			_skills = new List<Skill>();
		}
		public Scout(int id, string firstName, string lastName, bool isLeader, string email, byte[] password, int age, List<Skill> skills) : base(id, firstName, lastName, isLeader, email, password)
		{
			_age = age;
			_skills = skills;
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
