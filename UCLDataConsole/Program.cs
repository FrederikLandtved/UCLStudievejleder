using Microsoft.Data.SqlClient;

try
{
    string connectionString = "Server=tcp:ucldataserver.database.windows.net,1433;Initial Catalog=UCLDataPROD;Persist Security Info=False;User ID=azureuser;Password=Stefan$ebastianJacobMia;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
    SqlConnection connection = new SqlConnection(connectionString);

    string sql = "SELECT * FROM [dbo].[Question]";

    using (SqlCommand command = new SqlCommand(sql, connection))
    {
        connection.Open();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Question question = new Question
                {
                    Id = reader.GetInt32(0),
                    QuestionString = reader.GetString(1),
                    QuestionTypeId = reader.GetInt32(2)
                };

                Console.WriteLine(question.Id + question.QuestionString + question.QuestionTypeId);
            }
        }
        connection.Close();
    }

}
catch (Exception)
{
    Console.WriteLine("Something went wrong.");
    throw;
}

public class Question
{
    public int Id { get; set; }
    public int QuestionTypeId { get; set; }
    public string QuestionString { get; set; }
}