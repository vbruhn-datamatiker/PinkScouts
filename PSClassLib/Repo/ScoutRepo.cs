using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Interface;
using PSClassLib.Model;

namespace PSClassLib.Repo
{
	public class ScoutRepo : BasicDB, IRepo<Scout>
    {
		#region instance fields

		#endregion


		#region Properties


		#endregion

		#region Constructors
		public ScoutRepo(PSDBConnection db) : base(db)
        {

		}
        #endregion

        #region Methods




        // Do we need to insert a skill into scout here? Should be true, as scout contain skills. 
        // Where else is this needed? In the SkillRepo? We need to insert a skill into the scout, so we need to do it here.
        public Scout AddSkill(int skillId, int scoutId)
		{
            Scout scout = GetById(scoutId);
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
