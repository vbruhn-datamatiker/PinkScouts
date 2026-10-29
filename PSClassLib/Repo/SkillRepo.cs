using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Interface;
using PSClassLib.Model;

namespace PSClassLib.Repo
{
	public class SkillRepo : BasicDB, IRepo<Skill>
	{
        #region instance fields

        #endregion

        #region Properties


        #endregion

        #region Constructors
        #endregion




        public SkillRepo(PSDBConnection db) : base(db)
        {

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
