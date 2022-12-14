using DatabaseAccess.FieldOfStudy;
using DatabaseAccess.FieldOfStudy.Models;
using DatabaseAccess.Generic;
using DatabaseAccess.Institution;
using DatabaseAccess.Institution.Models;
using DatabaseAccess.User.Models;
using Microsoft.Data.SqlClient;
using System;
using static DatabaseAccess.Generic.GenericSql;

namespace DatabaseAccess.User
{
    public class UserDb
    {
        private readonly GenericSql _genericSql;

        public UserDb(GenericSql genericSql)
        {
            _genericSql = genericSql;
        }

        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();
            SqlDataReader reader = _genericSql.Select("SELECT UserId, FirstName, LastName, Email FROM [dbo].[AspNetUsers]");

            while (reader.Read())
            {
                users.Add(new UserModel
                {
                    UserId = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3)
                });
            }

            return users;
        }

        public void UpdateUser(UserModel model)
        {
            SqlDataReader reader = _genericSql.ExecuteSproc("sp_UpdateUser", new List<ParameterModel> { 
                new ParameterModel {Parameter = "@UserId", Value = model.UserId.ToString()},
                new ParameterModel {Parameter = "@FirstName", Value = model.FirstName},
                new ParameterModel {Parameter = "@LastName", Value = model.LastName}
            });

        }

        public UserModel GetUser(int userId)
        {
            UserModel user = new UserModel();
            SqlDataReader reader = _genericSql.Select("SELECT UserId, FirstName, LastName, Email FROM [dbo].[AspNetUsers] WHERE UserId = " + userId);

            while (reader.Read())
            {
                user.UserId = reader.GetInt32(0);
                user.FirstName = reader.GetString(1);
                user.LastName = reader.GetString(2);
                user.Email = reader.GetString(3);
            }

            return user;
        }

        public int GetUserId(string userToken)
        {
            SqlDataReader reader = _genericSql.Select("SELECT UserId FROM dbo.AspNetUsers WHERE Id = '" + userToken + "'");
            int userId = 0;

            while (reader.Read())
            {
                userId = reader.GetInt32(0);
            }

            return userId;
        }

        public List<UserMinimalModel> GetMinimalUserList()
        {
            List<UserMinimalModel> users = new List<UserMinimalModel>();
            SqlDataReader reader = _genericSql.ExecuteSproc("sp_GetUsersInstitutionsAndFieldsOfStudy", new List<ParameterModel>());

            while (reader.Read())
            {
                users.Add(new UserMinimalModel
                {
                    UserId = reader.GetInt32(0),
                    FullName = reader.GetString(1) + " " + reader.GetString(2),
                    Email = reader.GetString(3),
                    Institutions = reader.IsDBNull(4) ? "Ingen institutioner tilknyttet" : reader.GetString(4),
                    FieldsOfStudy = reader.IsDBNull(5) ? "Ingen studieretninger tilknyttet" : reader.GetString(5)
                });
            }


            return users;
        }
    }
}
