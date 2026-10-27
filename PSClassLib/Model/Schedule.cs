using System;
using System.Collections.Generic;
using System.Text;

namespace PSClassLib.Model
{
    public class Schedule
    {

        #region Instance fields

        private Activity _activity;
        private DateTime _start;
        private DateTime _end;

        #endregion

        #region Properties
        public Activity Activity { get { return _activity; } set { _activity = value; } }
        public DateTime Start { get { return _start; } set { _start = value; } }
        public DateTime End { get { return _end; } set {_end = value; } }

        #endregion

        #region Constructors

        public Schedule() 
        { 
            _activity = new Activity();
            _start = DateTime.Now;
            _end = DateTime.Now;
        
        }
        public Schedule(Activity activity, DateTime start, DateTime end)
        {
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
