using Microsoft.Data.SqlClient;

namespace DatabaseAccess.FieldOfStudy
{
    public class FieldOfStudyDb
    {
        string connectionString = "Server=tcp:ucldataserver.database.windows.net,1433;Initial Catalog=UCLDataPROD;Persist Security Info=False;User ID=azureuser;Password=Stefan$ebastianJacobMia;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";


        public List<FieldOfStudy> GetAllFieldsOfStudy()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                List<FieldOfStudy> allInstitutions = new List<FieldOfStudy>();

                string sql = "SELECT * FROM [dbo].[FieldOfStudy]";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FieldOfStudy fieldOfStudy = new FieldOfStudy();
                            fieldOfStudy.FieldOfStudyId = reader.GetInt32(0);
                            fieldOfStudy.Name = reader.GetString(1);

                            allInstitutions.Add(fieldOfStudy);   
                        }
                    }
                    connection.Close();
                }

                return allInstitutions;

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong.");
                throw;
            }

        }
        public void LinkUserToFieldOfStudy(int userId, int fieldOfStudyId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO dbo.[UserHasFieldOfStudy] (UserId, FieldOfStudyId) VALUES (@userId, @fieldOfStudyId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@fieldOfStudyId", fieldOfStudyId);

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                            Console.WriteLine("Error inserting data into Database!");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class FieldOfStudy
    {
        public int FieldOfStudyId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
