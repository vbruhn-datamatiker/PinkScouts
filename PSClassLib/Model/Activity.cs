using PSClassLib.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSClassLib.Model
{
    public class Activity : IHasId 
    {
        #region instance fields
        private int _activityId;
		private string _activityName;
		private string _description;
		private DateTime dateTime;
		private int _participants;
		#endregion

		#region Properties
		public int ActivityId
		{
			get { return _activityId; }
			set { _activityId = value; }
		}
		public string ActivityName
		{
			get { return _activityName; }
			set { _activityName = value; }
		}
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}
		public DateTime DateTime
		{
			get { return dateTime; }
			set { dateTime = value; }
		}
		public int Participants
		{
			get { return _participants; }
			set { _participants = value; }
		}
		#endregion

		#region Constructors
		public Activity()
		{
			_activityId = 0;
			_activityName = "";
			_description = "";
			dateTime = DateTime.Now;
			_participants = 0;
		}
		public Activity(int activityId, string activityName, string description, DateTime dateTime, int participants)
		{
			_activityId = activityId;
			_activityName = activityName;
			_description = description;
			this.dateTime = dateTime;
			_participants = participants;
		}
		#endregion

		#region methods
		public override string ToString()
		{
			return $"Id : {_activityId}, navn : {_activityName}, beskrivelse : {_description}, dato og tid : {dateTime}, deltagere : {_participants}";
		}
		#endregion

	}
}
