using DatabaseAccess.Generic;
using Microsoft.Data.SqlClient;
using static DatabaseAccess.Generic.GenericSql;

namespace DatabaseAccess.User
{
    public class UserDb
    {
        private readonly GenericSql _genericSql;
        public UserDb()
        {
            _genericSql = new GenericSql();
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            SqlDataReader reader = _genericSql.Select("SELECT * FROM [dbo].[AspNetUsers]");

            while (reader.Read())
            {
                users.Add(new User
                {
                    UserId = reader.GetInt32(17),
                    FullName = reader.GetString(15) + " " + reader.GetString(16),
                    Email = reader.GetString(3)
                });
            }

            return users;

        }

        public class User
        {
            public int UserId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
        }
    }
}
