using DatabaseAccess.FieldOfStudy;
using DatabaseAccess.Institution;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using UCLStudievejlederApp.Models.Generic;
using UCLStudievejlederApp.Validation;

namespace UCLStudievejlederApp.Models.User
{
    public class RegisterUserModel
    {
        [Required(ErrorMessage = "Email er påkrævet.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Fornavn er påkrævet.")]
        [Display(Name = "Fornavn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternavn er påkrævet.")]
        [Display(Name = "Efternavn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password er påkrævet.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekræft password")]
        [Compare("Password", ErrorMessage = "Passwords matcher ikke.")]
        public string ConfirmPassword { get; set; }

        [MinimumSelected(1, ErrorMessage = "Vælg venligst mindst 1 institution")]
        public List<UCLSelectModel>? AllInstitutions { get; set; } = new List<UCLSelectModel>();

        [MinimumSelected(1, ErrorMessage = "Vælg venligst mindst 1 studieretning")]
        public List<UCLSelectModel>? AllFieldsOfStudy { get; set; } = new List<UCLSelectModel>();

        public string? SuccessMessage { get; set; }
    }
}
