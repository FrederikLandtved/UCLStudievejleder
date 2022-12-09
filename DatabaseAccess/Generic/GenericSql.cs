using DatabaseAccess.User;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DatabaseAccess.Generic
{
    public class GenericSql
    {
        private string connectionString = "Server=tcp:ucldataserver.database.windows.net,1433;Initial Catalog=UCLDataPROD;Persist Security Info=False;User ID=azureuser;Password=Stefan$ebastianJacobMia;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public SqlDataReader Select(string query)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    
                    return reader;
                    connection.Close();
                }

                return null;

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong.");
                throw;
            }
        }

        public SqlDataReader ExecuteSproc(string sproc, List<ParameterModel> parameters)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                using (SqlCommand command = new SqlCommand(sproc, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if(parameters.Any())
                    {
                        foreach (ParameterModel param in parameters)
                            command.Parameters.Add(new SqlParameter(param.Parameter, param.Value));
                    }

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    return reader;
                    connection.Close();
                }

                return null;

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong.");
                throw;
            }
        }

        public void Insert(string query, List<ParameterModel> parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        foreach(ParameterModel model in parameters)
                            command.Parameters.AddWithValue(model.Parameter, model.Value);

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

        public class ParameterModel
        {
            public string Parameter { get; set; }
            public string Value { get; set; }
        }
    }
}
