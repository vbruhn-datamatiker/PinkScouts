using Microsoft.Data.SqlClient;
using PSClassLib.Model;
using PSClassLib.Repo;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PinkScoutTest
{
    public class UnitTestActivitiesDB 
    {
        private readonly ActivityRepo _repository;
        private readonly PSDBConnection _db;

        public UnitTestActivitiesDB()
        {

            _db = new PSDBConnection("YourTestDatabaseConnectionStringHere");
            _repository = new ActivityRepo(_db);

            // Setup/Seed your test database state here before each test runs
            SetupTestDatabase();
        }

        [Fact]
        public void GetById_WhenActivityExists_ReturnsActivity()
        {
            // Arrange: Insert a dummy activity into your test DB manually or via script
            int existingId = 1;

            // Act
            Activity result = _repository.GetById(existingId);

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


            // Clear existing data
            string clearSql = "DELETE FROM Activity";

            
            SqlCommand cmd1 = new SqlCommand(clearSql, _db.Connection);

            // Insert a dummy activity for testing
            string insertSql = "INSERT INTO Activity (ActivityId, ActivityName, Description) VALUES (1, 'Test Activity', 'This is a test activity')";
            SqlCommand cmd2 = new SqlCommand(insertSql, _db.Connection);
            
            // Execute the commands
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();

            // Disconnect from the database after setup
            _db.DisconnectFromDatabase();
        }
    }
}
