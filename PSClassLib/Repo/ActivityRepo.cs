using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Model;

namespace PSClassLib.Repo
{
	internal class ActivityRepo
	{
		#region instance fields
		private List<Activity> _activities;
		#endregion

		#region Properties
		public List<Activity> Activities
		{
			get { return _activities; }
			set { _activities = value; }
		}
		#endregion

		#region Constructors
		public ActivityRepo()
		{
			_activities = new List<Activity>();
		}
		public ActivityRepo(List<Activity> activities)
		{
			_activities = activities;
		}
		#endregion

		#region Methods
		#endregion
	}
}
