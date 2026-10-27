using Microsoft.Data.SqlClient;
using PSClassLib.Model;
using PSClassLib.Repo;

namespace PinkScoutTest
{
    public class UnitTestActivitiesDB
    {
        #region instance fields
        private readonly PSDBConnection _dB;
        private readonly ActivityRepo _repo;
        private readonly int _testId;
        #endregion


        // Setup of Test data for activities


        public UnitTestActivitiesDB()
        {
            // setup of location connection to test data on. 

            string conString = "";
            _dB = new PSDBConnection(conString);

            // setup of new test data:
            _dB.ConnectToDatabase();

            SqlCommand cmd = new SqlCommand("DELETE FROM ACTIVITIES", _dB.Connection);


        }


        [Fact]
        public void Test1()
        {

        }
    }
}
