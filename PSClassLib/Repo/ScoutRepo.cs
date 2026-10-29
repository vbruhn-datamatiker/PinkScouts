using Microsoft.Data.SqlClient;
using PSClassLib.Interface;
using PSClassLib.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSClassLib.Repo
{
	public class ScoutRepo : BasicDB, IRepo<Scout>
    {
		#region instance fields

		#endregion


		#region Properties


		#endregion

		#region Constructors
		public ScoutRepo(PSDBConnection db) : base(db)
        {

		}
        #endregion

        #region Methods




        // Do we need to insert a skill into scout here? Should be true, as scout contain skills. 
        // Where else is this needed? In the SkillRepo? We need to insert a skill into the scout, so we need to do it here.
        public Scout AddSkill(int skillId, int scoutId)
		{
            Scout scout = GetById(scoutId);
            return scout;
		}



        public Scout Create(Scout scout)
        {
            return scout;
        }

        public Scout Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Scout Update(Scout item, int id)
        {
            throw new NotImplementedException();
        }

        public List<Scout> ReadAll()
        {
            List<Scout> scoutList = new List<Scout>();
            _db.ConnectToDatabase();

            // Scout only has UserID and Age - so needs to be joined with user
            string sql = "SELECT u.UserId, u.FirstName, u.LastName, u.IsLeader, u.Email, s.Age " +
                         "FROM SCOUT s JOIN [USER] u ON u.UserId = s.UserId";
            SqlCommand cmd = new SqlCommand(sql, Db.Connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Scout scout = new Scout();
                scout.Id = (int)reader["UserId"];
                scout.FirstName = (string)reader["FirstName"];
                scout.LastName = (string)reader["LastName"];
                scout.IsLeader = (bool)reader["IsLeader"];
                scout.Email = (string)reader["Email"];
                scout.Age = (int)reader["Age"];
                scout.Skills = new List<Skill>(); // Initialize the Skills list


                scoutList.Add(scout);
            }
            Db.DisconnectFromDatabase();
            return scoutList;
        }

        public Scout GetById(int id)
        {
            throw new NotImplementedException();
        }
        #endregion




    }
}
