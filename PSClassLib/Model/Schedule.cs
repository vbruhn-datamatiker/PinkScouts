using PSClassLib.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSClassLib.Model
{
    public class Schedule : IHasId
    {

        #region Instance fields

        private int _id;
        private Activity _activity;
        private DateTime _start;
        private DateTime _end;

        #endregion

        #region Properties
        public int Id { get { return _id; } set { _id = value; } }
        public Activity Activity { get { return _activity; } set { _activity = value; } }
        public DateTime Start { get { return _start; } set { _start = value; } }
        public DateTime End { get { return _end; } set {_end = value; } }

        #endregion

        #region Constructors

        public Schedule() 
        {
            _id = 0;
            _activity = new Activity();
            _start = DateTime.Now;
            _end = DateTime.Now;
    
        
        }
        public Schedule(int id, Activity activity, DateTime start, DateTime end)
        {
            _id = id;
            _activity = activity;
            _start = start;
            _end = end;
        }


        #endregion


        #region methods

        public override string ToString()
        {
            return $"Aktivitet: {Activity}, Start tid: {Start}, Slut tid: {End}";
        }
        #endregion 
    }
}
