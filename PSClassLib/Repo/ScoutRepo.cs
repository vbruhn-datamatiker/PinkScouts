using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Model;

namespace PSClassLib.Repo
{
	public class ScoutRepo
	{
		#region instance fields

		private PSDBConnection _db;
		#endregion

		#region Properties

		public PSDBConnection DB 
		{ 
			get { return _db; } 
			set { _db = value; }
		}
		#endregion

		#region Constructors
		public ScoutRepo()
		{
            _db = new PSDBConnection();
        }
		public ScoutRepo(PSDBConnection db)
		{
			_db = db;
		}
		#endregion

		#region Methods

		public Scout Create(Scout scout)
		{
			return scout;
		}


		// Do we need to insert a skill into scout here? Should be true, as scout contain skills.
		public Scout InsertSkill(Scout scout, Skill skill)
		{
			return scout;
		}
		#endregion




	}
}
