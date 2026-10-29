using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Model;
using PSClassLib.Interface;

namespace PSClassLib.Repo
{
    public class ScheduleRepo : BasicDB, IRepo<Schedule>
    {
        #region instance fields

        #endregion
        #region properties

        #endregion
        #region constructors

        public ScheduleRepo(PSDBConnection dB) : base(dB)
        {

        }


        #endregion

        #region methods 

        public Schedule Create(Schedule item)
        {
            throw new NotImplementedException();
        }

        public Schedule Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Schedule Update(Schedule item, int id)
        {
            throw new NotImplementedException();
        }

        public List<Schedule> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Schedule GetById(int id)
        {
            throw new NotImplementedException();
        }


        #endregion



    }
}
