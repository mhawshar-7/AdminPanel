using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Web.Models
{
    public class UserViewModel : IValidatableObject
    {
        public string? Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]        
        public string? ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // For create (no Id) require password
            if (string.IsNullOrEmpty(Id))
            {
                if (string.IsNullOrWhiteSpace(Password))
                {
                    yield return new ValidationResult("Password is required for new users", new[] { nameof(Password) });
                }
            }
        }
    }
}
