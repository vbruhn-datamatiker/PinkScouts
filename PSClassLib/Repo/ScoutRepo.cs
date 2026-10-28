using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Interface;
using PSClassLib.Model;

namespace PSClassLib.Repo
{
	public class ScoutRepo : IRepo<Scout>
    {
		#region instance fields

		private PSDBConnection _db;
		#endregion

		#region Properties

		public PSDBConnection Db 
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




        // Do we need to insert a skill into scout here? Should be true, as scout contain skills. 
        // Where else is this needed? In the SkillRepo? We need to insert a skill into the scout, so we need to do it here.
        public Scout InsertSkill(Scout scout, Skill skill)
		{
			return scout;
		}



        public Scout Create(Scout scout)
        {
            return scout;
        }

        public Scout Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Scout Update(Scout item, int id)
        {
            throw new NotImplementedException();
        }

        public List<Scout> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Scout GetById(int id)
        {
            throw new NotImplementedException();
        }
        #endregion




    }
}
