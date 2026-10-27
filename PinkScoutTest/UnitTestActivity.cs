using Microsoft.Data.SqlClient;
using PSClassLib.Model;
using PSClassLib.Repo;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PinkScoutTest
{
    public class UnitTestActivitiesDB : IDisposable
    {
        #region instance fields
        private readonly PSDBConnection _db;
        private readonly ActivityRepo _repo;
        private readonly SqlTransaction _transaction;

        #endregion


        // Setup of Test data for activities

#region Constructor
        public UnitTestActivitiesDB()
        {
            // setup of location connection to test data on. 

            // Update the local DB connection string here
            string conString = "";
            _db = new PSDBConnection(conString);

            // Open the connection using PSDBConnection
            _db.ConnectToDatabase();

            // Start a transaction to contain our mock data lifecycle
            _transaction = _db.Connection.BeginTransaction();

            // Initialize the system under test
            _repo = new ActivityRepo(_db);

            // Seed the mockup data directly inside this test setup
            CreateMockupData();

        }
        #endregion

        #region Methods

        private void CreateMockupData()
        {
            // We MUST pass the active transaction to these commands so they are isolated
            string insertQuery = @"
            INSERT INTO ACTIVITY (ActivityName, Description) 
            VALUES 
            (@Name1, @Desc1),
            (@Name2, @Desc2);";

            using (var command = new SqlCommand(insertQuery, _db.Connection, _transaction))
            {
                command.Parameters.AddWithValue("@Name1", "Mock Campfire Night");
                command.Parameters.AddWithValue("@Desc1", "Mocked Campfire Description");
                command.Parameters.AddWithValue("@Name2", "Mock First Aid Basics");
                command.Parameters.AddWithValue("@Desc2", "Mocked First Aid Description");

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Unit Tests


        // Testing ReadAll on activities
        [Fact]
        public void ReadAllActivities()
        {
            // Arrange & Act     
            List<Activity> activities = _repo.ReadAll();

            // Assert
            Assert.NotNull(activities);
            Assert.Contains("Mock Campfire Night", activities[1].ActivityName);
            Assert.Contains("Mock First Aid Basics", activities[2].ActivityName);
            Assert.Equal(2, activities.Count);
        }



        public void Dispose()
        {
            // Roll back the transaction. 
            // This instantly deletes the mockup data from the local DB.
            _transaction.Rollback();
            _transaction.Dispose();

            // Safely disconnect from the Database.
            _db.DisconnectFromDatabase();
        }

        #endregion
    }
}
