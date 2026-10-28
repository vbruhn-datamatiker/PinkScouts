using Microsoft.Data.SqlClient;
using PSClassLib.Model;
using PSClassLib.Repo;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PinkScoutTest
{
    public class UnitTestScoutsDB : IDisposable
    {
        #region instance fields
        private readonly PSDBConnection _db;
        private readonly ScoutRepo _repo;
        private readonly SqlTransaction _transaction;


        // track IDs
        private int _mockScoutUserId1;
        private int _mockScoutUserId2;

        #endregion


        // Setup of Test data for activities

        #region Constructor
        public UnitTestScoutsDB()
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
            _repo = new ScoutRepo(_db);

            // Seed the mockup data directly inside this test setup
            CreateMockupData();

        }
        #endregion

        #region Methods

        private void CreateMockupData()
        {
            // 1. Script to create a USER and instantly grab its generated identity ID
            string insertUserSql = @"
            INSERT INTO [USER] (FirstName, LastName, IsLeader, Email, [Password]) 
            VALUES (@FirstName, @LastName, @IsLeader, @Email, @Password);
            SELECT SCOPE_IDENTITY();"; // Returns the ID just created

            // 2. Script to insert into the SCOUT sub-entity table using that identity ID
            string insertScoutSql = @"
            INSERT INTO SCOUT (UserId, Age) 
            VALUES (@UserId, @Age);";

            // --- SEED FIRST MOCK SCOUT ---
            using (var cmdUser = new SqlCommand(insertUserSql, _db.Connection, _transaction))
            {
                cmdUser.Parameters.AddWithValue("@FirstName", "John");
                cmdUser.Parameters.AddWithValue("@LastName", "Doe");
                cmdUser.Parameters.AddWithValue("@IsLeader", false);
                cmdUser.Parameters.AddWithValue("@Email", "john.doe@scouts.org");
                cmdUser.Parameters.AddWithValue("@Password", "HashedPassword123");

                // ExecuteScalar retrieves the single value returned by SELECT SCOPE_IDENTITY()
                _mockScoutUserId1 = Convert.ToInt32(cmdUser.ExecuteScalar());
            }

            using (var cmdScout = new SqlCommand(insertScoutSql, _db.Connection, _transaction))
            {
                cmdScout.Parameters.AddWithValue("@UserId", _mockScoutUserId1);
                cmdScout.Parameters.AddWithValue("@Age", 14);
                cmdScout.ExecuteNonQuery();
            }

            // --- SEED SECOND MOCK SCOUT ---
            using (var cmdUser = new SqlCommand(insertUserSql, _db.Connection, _transaction))
            {
                cmdUser.Parameters.AddWithValue("@FirstName", "Jane");
                cmdUser.Parameters.AddWithValue("@LastName", "Smith");
                cmdUser.Parameters.AddWithValue("@IsLeader", false);
                cmdUser.Parameters.AddWithValue("@Email", "jane.smith@scouts.org");
                cmdUser.Parameters.AddWithValue("@Password", "AnotherHashedPassword456");

                _mockScoutUserId2 = Convert.ToInt32(cmdUser.ExecuteScalar());
            }

            using (var cmdScout = new SqlCommand(insertScoutSql, _db.Connection, _transaction))
            {
                cmdScout.Parameters.AddWithValue("@UserId", _mockScoutUserId2);
                cmdScout.Parameters.AddWithValue("@Age", 16);
                cmdScout.ExecuteNonQuery();
            }
        }

        #endregion

        #region Unit Tests


        // Testing ReadAll on scouts
        [Fact]
        public void ReadAllScouts()
        {
            // Arrange & Act     
            List<Scout> scouts = _repo.ReadAll();
            // Assert
            Assert.NotNull(scouts);
            Assert.Contains(scouts, s => s.FirstName == "John" && s.LastName == "Doe");
            Assert.Contains(scouts, s => s.FirstName == "Jane" && s.LastName == "Smith");
            Assert.Equal(2, scouts.Count); 
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
