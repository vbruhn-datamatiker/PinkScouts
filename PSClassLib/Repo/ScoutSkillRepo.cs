using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Interface;
using PSClassLib.Model;

namespace PSClassLib.Repo
{
    public class ScoutSkillRepo : BasicDB, IRepo<Scout>
    {
        #region instance fields

  

        #endregion
        #region properties



        #endregion
        #region constructors

        public ScoutSkillRepo(PSDBConnection db) : base(db)
        {

        }

        #endregion


        #region methods
        public Scout Create(Scout item)
        {
            throw new NotImplementedException();
        }

        public Scout GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Scout> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Scout Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Scout Update(Scout item, int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
