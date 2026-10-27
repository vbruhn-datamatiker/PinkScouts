using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Model;
using Microsoft.Data.SqlClient;
using System.ComponentModel.Design;


namespace PSClassLib.Repo
{
	public class ActivityRepo
	{
		#region instance fields
		private List<Activity> _activities;
		public PSDBConnection db = new PSDBConnection();
		#endregion

		#region Properties
		public List<Activity> Activities
		{
			get { return _activities; }
			set { _activities = value; }
		}
		public PSDBConnection Db
		{
			get { return db; }
			set { db = value; }
		}
		#endregion

		#region Constructors
		public ActivityRepo()
		{
			_activities = new List<Activity>();
			db = new PSDBConnection();
		}
		public ActivityRepo(List<Activity> activities, PSDBConnection dB)
		{
			_activities = activities;
			db = dB;
		}
		#endregion

		#region Methods
		public Activity GetById(string id)
		{
			db.ConnectToDatabase();

			string sql = "SELECT * FROM Activities WHERE ActivityId = @ActivityId";

			SqlCommand cmd = new SqlCommand(sql, db.Connection);

			SqlDataReader reader = cmd.ExecuteReader();
			Activity? activity = null;

			if (reader.Read())
			{
				int ActivityId = (int)reader["ActivityId"];
				string ActivityName = (string)reader["ActivityName"];
				string Description = (string)reader["Description"];
				DateTime dateTime = (DateTime)reader["DateTime"];
				int participants = (int)reader["Participants"];

				activity = new Activity(ActivityId, ActivityName, Description, dateTime, participants);
			}
			else
			{
				db.DisconnectFromDatabase();
				throw new Exception($"aktivitet med {id} findes ikke.");
			}
			db.DisconnectFromDatabase();
			return activity;
		}

		public List<Activity> ReadAll()
		{
			List<Activity> activityList = new List<Activity>();
			db.ConnectToDatabase();

			string sql = "SELECT * FROM Activities";
			SqlCommand cmd = new SqlCommand(sql, db.Connection);
			SqlDataReader reader = cmd.ExecuteReader();

			while(reader.Read())
			{
				int ActivityId = (int)reader["ActivityId"];
				string ActivityName = (string)reader["ActivityName"];
				string Description = (string)reader["Description"];
				DateTime DateTime = (DateTime)reader["DateTime"];
				int Participants = (int)reader["Participants"];

				Activity activity = new Activity(ActivityId, ActivityName, Description, DateTime, Participants);

				activityList.Add(activity);
			}
			db.DisconnectFromDatabase();
			return activityList;
		}
		public Activity CreateActivity()
		{
			db.ConnectToDatabase();

			string sql = $"INSERT into ";


		}
		#endregion

	}
}
