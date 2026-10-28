using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Interface;
using PSClassLib.Model;

namespace PSClassLib.Repo
{
	public class SkillRepo : IRepo<Skill>
	{
        #region instance fields
        private PSDBConnection _db = new PSDBConnection();
        #endregion

        #region Properties

        public PSDBConnection Db
        {
            get { return _db; }
            set { _db = value; }
        }

        #endregion

        #region Constructors
        #endregion
        public SkillRepo()
		{
			_db = new PSDBConnection();
        }
		public SkillRepo(PSDBConnection db)
		{
			_db = db;
		}


        #region Methods
        public Skill Create(Skill item)
        {
            throw new NotImplementedException();
        }

        public Skill GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Skill> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Skill Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Skill Update(Skill item, int id)
        {
            throw new NotImplementedException();
        }


        #endregion


    }
}
