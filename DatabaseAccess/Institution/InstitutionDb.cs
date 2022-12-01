using Microsoft.Data.SqlClient;

namespace DatabaseAccess.Institution
{
    public class InstitutionDb
    {
        string connectionString = "Server=tcp:ucldataserver.database.windows.net,1433;Initial Catalog=UCLDataPROD;Persist Security Info=False;User ID=azureuser;Password=Stefan$ebastianJacobMia;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";


        public List<Institution> GetAllInstitutions()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                List<Institution> allInstitutions = new List<Institution>();

                string sql = "SELECT * FROM [dbo].[Institution]";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Institution institution = new Institution();
                            institution.InstitutionId = reader.GetInt32(0);
                            institution.Name = reader.GetString(1);

                            allInstitutions.Add(institution);   
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

            return new List<Institution>();

        }
        public void LinkUserToInstitution(int userId, int institutionId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO dbo.[UserHasInstitution] (UserId, InstitutionId) VALUES (@userId, @institutionId)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@institutionId", institutionId);

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

    public class Institution : UCLListItem
    {
        public int InstitutionId { get; set; }
        public string Name { get; set; }
    }


    public class UCLListItem
    {
        public bool IsSelected { get; set; }
    }
}
