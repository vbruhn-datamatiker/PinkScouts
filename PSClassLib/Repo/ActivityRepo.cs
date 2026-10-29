using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Model;
using Microsoft.Data.SqlClient;
using System.ComponentModel.Design;
using PSClassLib.Interface;


namespace PSClassLib.Repo
{
	public class ActivityRepo : IRepo<Activity>
	{
		#region instance fields

		private PSDBConnection _db = new PSDBConnection();
		#endregion

		#region Properties
		
		public PSDBConnection Db
		{
			get { return _db; }
			set { _db = value; }
		}
		#endregion

		#region Constructors
		public ActivityRepo()
		{
		
			_db = new PSDBConnection();
		}
		public ActivityRepo(PSDBConnection dB)
		{
		
			_db = dB;
		}
		#endregion

		#region Methods
		public Activity GetById(int id)
		{
			_db.ConnectToDatabase();

			string sql = "SELECT * FROM ACTIVITY WHERE ActivityId = @ActivityId";

			SqlCommand cmd = new SqlCommand(sql, _db.Connection);
            cmd.Parameters.AddWithValue("@ActivityId", id);
            SqlDataReader reader = cmd.ExecuteReader();
			Activity? activity = null;

			if (reader.Read())
			{
				int ActivityId = (int)reader["ActivityId"];
				string ActivityName = (string)reader["ActivityName"];
				string Description = (string)reader["Description"];

				activity = new Activity(ActivityId, ActivityName, Description);
			}
			else
			{
				_db.DisconnectFromDatabase();
				throw new Exception($"aktivitet med {id} findes ikke.");
			}
			_db.DisconnectFromDatabase();
			return activity;
		}

		

		public Activity Create(Activity activity)
		{
			_db.ConnectToDatabase();

			string sql = $"INSERT into * (ActivityId, ActivityName, Description) VALUES (@ActivityId, @ActivityName, @Description)";

			SqlCommand cmd = new SqlCommand(sql, _db.Connection);
			cmd.Parameters.AddWithValue("@ActivityId", activity.Id);
			cmd.Parameters.AddWithValue("@ActivityName", activity.ActivityName);
			cmd.Parameters.AddWithValue("@Description", activity.Description);

			int row = cmd.ExecuteNonQuery();

			if (row == 0)
			{
				throw new KeyNotFoundException($"Aktivitet med {activity.Id} findes ikke");
			}
			_db.DisconnectFromDatabase();
			return activity;
		}



		public List<Activity> ReadAll()
		{
			List<Activity> activityList = new List<Activity>();
			_db.ConnectToDatabase();

			string sql = "SELECT * FROM ACTIVITY";
			SqlCommand cmd = new SqlCommand(sql, _db.Connection);
			SqlDataReader reader = cmd.ExecuteReader();

			while(reader.Read())
			{
				int ActivityId = (int)reader["ActivityId"];
				string ActivityName = (string)reader["ActivityName"];
				string Description = (string)reader["Description"];


				Activity activity = new Activity(ActivityId, ActivityName, Description);

				activityList.Add(activity);
			}
			_db.DisconnectFromDatabase();
			return activityList;
		}

		public Activity Update(Activity updatedActivity, int id)
		{
			Activity activity = GetById(id);
			_db.ConnectToDatabase();

			string sql = $"update .........";
			SqlCommand cmd = new SqlCommand(sql, _db.Connection);

			cmd.Parameters.AddWithValue("@ActivityName", updatedActivity.ActivityName);
			cmd.Parameters.AddWithValue("@Description", updatedActivity.Description);

			int row = cmd.ExecuteNonQuery();
			_db.DisconnectFromDatabase();

			if (row == 1)
			{
				return updatedActivity;
			}
			else
			{
				throw new KeyNotFoundException($"Akvitivtet med {id} findes ikke");
			}

		}

		public Activity Remove(int idToRemove)
		{
			Activity activity = GetById(idToRemove);
			_db.ConnectToDatabase();
			string sql = $"DELETE FROM ACTIVITY WHERE ActivityId = @ActivityId";

			SqlCommand cmd = new SqlCommand(sql, _db.Connection);

			cmd.Parameters.AddWithValue("@ActivityId", idToRemove);

			int row = cmd.ExecuteNonQuery();
			_db.DisconnectFromDatabase();

			if(row == 1){
				return activity;
			}
			else
			{
				throw new KeyNotFoundException($"Aktivitet med {idToRemove} findes ikke");
			}

		}
		

		#endregion

	}
}
