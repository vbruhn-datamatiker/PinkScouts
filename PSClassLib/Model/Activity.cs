using PSClassLib.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Repo;

namespace PSClassLib.Model
{
    public class Activity : IHasId 
    {
        #region instance fields
        private int _id;
		private string _activityName;
		private string _description;
		#endregion

		#region Properties
		public int Id
		{
			get { return _id; }
			set { _id = value; }
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

		#endregion

		#region Constructors
		public Activity()
		{
			_id = 0;
			_activityName = "";
			_description = "";
		}
		public Activity(int activityId, string activityName, string description)
		{
			_id = activityId;
			_activityName = activityName;
			_description = description;
		}
		#endregion

		#region methods
		public override string ToString()
		{
			return $"Id : {_id}, navn : {_activityName}, beskrivelse : {_description}";
		}
		#endregion

	}
}
