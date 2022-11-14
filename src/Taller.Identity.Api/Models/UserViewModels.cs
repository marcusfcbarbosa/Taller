using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Taller.Identity.Api.Models
{
    public class UserRegistration
    {
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "Field {0} is in invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "Field {0} must be between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string PasswordConfirmation { get; set; }
    }
    public class UserLogin
    {
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "Field {0} is in invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "Field {0} must be between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }
    }
    public class UsuarioRespostaLogin
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UsuarioToken { get; set; }
    }

    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UsuarioClaim> Claims { get; set; }
    }

    public class UsuarioClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
