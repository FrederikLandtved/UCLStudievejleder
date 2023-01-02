using Microsoft.Data.SqlClient;
string connectionString = "CONNECTION STRING HERE";

//try
//{
//    string connectionString = "CONNECTION STRING HERE";
//    SqlConnection connection = new SqlConnection(connectionString);

//    string sql = "SELECT * FROM [dbo].[Question]";

//    using (SqlCommand command = new SqlCommand(sql, connection))
//    {
//        connection.Open();
//        using (SqlDataReader reader = command.ExecuteReader())
//        {
//            while (reader.Read())
//            {
//                Question question = new Question
//                {
//                    Id = reader.GetInt32(0),
//                    QuestionString = reader.GetString(1),
//                    QuestionTypeId = reader.GetInt32(2)
//                };

//                Console.WriteLine(question.Id + question.QuestionString + question.QuestionTypeId);
//            }
//        }
//        connection.Close();
//    }

//}
//catch (Exception)
//{
//    Console.WriteLine("Something went wrong.");
//    throw;
//}

using (SqlConnection connection = new SqlConnection(connectionString))
{
    string query = "INSERT INTO dbo.[UserHasInstitution] (UserId, InstitutionId) VALUES (@userId, @institutionId)";

    using (SqlCommand command = new SqlCommand(query, connection))
    {
        command.Parameters.AddWithValue("@userId", 2);
        command.Parameters.AddWithValue("@institutionId", 5);

        connection.Open();
        int result = command.ExecuteNonQuery();

        // Check Error
        if (result < 0)
            Console.WriteLine("Error inserting data into Database!");
    }
}

public class Question
{
    public int Id { get; set; }
    public int QuestionTypeId { get; set; }
    public string QuestionString { get; set; }
}