using Microsoft.Data.SqlClient;
using PSClassLib.Model;
using PSClassLib.Repo;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PinkScoutTest
{
    public class UnitTestActivitiesDB 
    {
        private ActivityRepo _repository;
        private PSDBConnection _db;

        private UnitTestActivitiesDB()
        {
            // Replace with your actual test database connection string
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PinkScoutsTest;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30"; 
            _db = new PSDBConnection(connectionString);
            _repository = new ActivityRepo(_db);

            // Setup/Seed your test database state here before each test runs
            SetupTestDatabase();
        }

        [Fact]
        public void ReadAll_ActivitiesFromDatabase_ReturnsAllActivities()
        {
            List<PSClassLib.Model.Activity> result = _repository.ReadAll();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }


        [Fact]
        public void GetById_WhenActivityExists_ReturnsActivity()
        {
            // Arrange: Insert a dummy activity into your test DB manually or via script
            int existingId = 1;

            // Act
            PSClassLib.Model.Activity result = _repository.GetById(existingId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingId, result.Id);
            Assert.Equal("Test Activity", result.ActivityName);
        }

        [Fact]
        public void GetById_WhenActivityDoesNotExist_ThrowsException()
        {
            // Arrange
            int nonExistingId = 99999;

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _repository.GetById(nonExistingId));
            Assert.Contains($"aktivitet med {nonExistingId} findes ikke.", exception.Message);
        }

        private void SetupTestDatabase()
        {
            // connect to the test database and set up the necessary state for testing
            _db.ConnectToDatabase();


            // Clear any existing data and insert mock data for activities
            string sql = @"
            TRUNCATE TABLE ACTIVITY;
        
            SET IDENTITY_INSERT ACTIVITY ON;

            INSERT INTO ACTIVITY (ActivityId, ActivityName, Description) VALUES 
            (1, 'Running', 'Outdoor running session in the park.'),
            (2, 'Swimming', 'Indoor lap swimming at the local pool.'),
            (3, 'Yoga', 'Stretching and mindfulness practice.');

            SET IDENTITY_INSERT ACTIVITY OFF;
            ";

            SqlCommand cmd = new SqlCommand(sql, _db.Connection);

            // Execute the commands
            cmd.ExecuteNonQuery();


            // Disconnect from the database after setup
            _db.DisconnectFromDatabase();
        }
    }
}
