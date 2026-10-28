using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Model;
using PSClassLib.Interface;

namespace PSClassLib.Repo
{
    public class ScheduleRepo : IRepo<Schedule>
    {
        #region instance fields

        private PSDBConnection _db;
        #endregion
        #region properties
        public PSDBConnection Db
        {
            get { return _db; }
            set { _db = value; }
        }
        #endregion
        #region constructors
        public ScheduleRepo()
        {

            _db = new PSDBConnection();
        }
        public ScheduleRepo(PSDBConnection dB)
        {

            _db = dB;
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
