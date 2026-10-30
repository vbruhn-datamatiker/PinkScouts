using System;
using System.Collections.Generic;
using System.Text;
using PSClassLib.Model;
using Microsoft.Data.SqlClient;
using System.ComponentModel.Design;
using PSClassLib.Interface;


namespace PSClassLib.Repo
{
	public class ActivityRepo : BasicDB, IRepo<Activity>
	{
		#region instance fields

		#endregion

		#region Properties
		
		#endregion


        #region Constructors
		public ActivityRepo(PSDBConnection dB) : base(dB)
		{
		}
		#endregion

		#region Methods
		public Activity GetById(int id)
		{
			Db.ConnectToDatabase();

			string sql = "SELECT * FROM Activity WHERE ActivityId = @ActivityId";

			SqlCommand cmd = new SqlCommand(sql, Db.Connection);
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
				Db.DisconnectFromDatabase();
				throw new Exception($"aktivitet med {id} findes ikke.");
			}
			Db.DisconnectFromDatabase();
			return activity;
		}

		

		public Activity Create(Activity activity)
		{
			Db.ConnectToDatabase();
			// we have removed the ActivityId since we generate this in through the database
			string sql = $"INSERT into Activity (ActivityName, Description) VALUES (@ActivityName, @Description)";

			SqlCommand cmd = new SqlCommand(sql, Db.Connection);
		
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
			SqlCommand cmd = new SqlCommand(sql, Db.Connection);
			SqlDataReader reader = cmd.ExecuteReader();

			while(reader.Read())
			{
				int ActivityId = (int)reader["ActivityId"];
				string ActivityName = (string)reader["ActivityName"];
				string Description = (string)reader["Description"];


				Activity activity = new Activity(ActivityId, ActivityName, Description);

				activityList.Add(activity);
			}
			Db.DisconnectFromDatabase();
			return activityList;
		}

		public Activity Update(Activity updatedActivity, int id)
		{
			Activity activity = GetById(id);
			_db.ConnectToDatabase();

			string sql = $"UPDATE into Activity (ActivityName, Description) VALUES (@ActivityName, @Description)";
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
			string sql = $"DELETE FROM Activity WHERE ActivityId = {idToRemove}";

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
