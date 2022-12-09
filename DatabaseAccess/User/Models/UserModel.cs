using DatabaseAccess.FieldOfStudy.Models;
using DatabaseAccess.Institution.Models;

namespace DatabaseAccess.User.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<FieldOfStudyModel> FieldOfStudies { get; set; } = new List<FieldOfStudyModel>();
        public List<InstitutionModel> Institutions { get; set; } = new List<InstitutionModel>();
    }

    public class UserMinimalModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Institutions { get; set; }
        public string FieldsOfStudy { get; set; }
    }
}
