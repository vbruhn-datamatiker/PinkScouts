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

		public PSDBConnection db = new PSDBConnection();
		#endregion

		#region Properties
		
		public PSDBConnection Db
		{
			get { return db; }
			set { db = value; }
		}
		#endregion

		#region Constructors
		public ActivityRepo()
		{
		
			db = new PSDBConnection();
		}
		public ActivityRepo(PSDBConnection dB)
		{
		
			db = dB;
		}
		#endregion

		#region Methods
		public Activity GetById(int id)
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

		

		public Activity CreateActivity(Activity activity)
		{
			db.ConnectToDatabase();

			string sql = $"INSERT into * (ActivityId, ActivityName, Description, DateTime, Participants) VALUES (@ActivityId, @ActivityName, @Description, @DateTime, @Participants)";

			SqlCommand cmd = new SqlCommand(sql, db.Connection);
			cmd.Parameters.AddWithValue("@ActivityId", activity.ActivityId);
			cmd.Parameters.AddWithValue("@ActivityName", activity.ActivityName);
			cmd.Parameters.AddWithValue("@Description", activity.Description);
			cmd.Parameters.AddWithValue("@DateTime", activity.DateTime);
			cmd.Parameters.AddWithValue("@Participants", activity.Participants);

			int row = cmd.ExecuteNonQuery();

			if (row == 0)
			{
				throw new KeyNotFoundException($"Aktivitet med {activity.ActivityId} findes ikke");
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

		public Activity UpdateActivity(int id, Activity updatedActivity)
		{
			Activity activity = GetById(id);
			db.ConnectToDatabase();

			string sql = $"update .........";
			SqlCommand cmd = new SqlCommand(sql, db.Connection);

			cmd.Parameters.AddWithValue("@ActivityName", updatedActivity.ActivityName);
			cmd.Parameters.AddWithValue("@Description", updatedActivity.Description);
			cmd.Parameters.AddWithValue("@DateTime", updatedActivity.DateTime);
			cmd.Parameters.AddWithValue("@Participants", updatedActivity.Participants);

			int row = cmd.ExecuteNonQuery();
			db.DisconnectFromDatabase();

			if (row == 1)
			{
				return updatedActivity;
			}
			else
			{
				throw new KeyNotFoundException($"Akvitivtet med {id} findes ikke");
			}

		}

		public Activity DeleteActivity(int idToRemove)
		{
			Activity activity = GetById(idToRemove);
			db.ConnectToDatabase();
			string sql = $"DELETE FROM Activites WHERE ActivityId = {idToRemove}";

			SqlCommand cmd = new SqlCommand(sql, db.Connection);

			cmd.Parameters.AddWithValue("@ActivityId", idToRemove);

			int row = cmd.ExecuteNonQuery();
			db.DisconnectFromDatabase();

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
