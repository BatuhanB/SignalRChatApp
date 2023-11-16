using System.ComponentModel.DataAnnotations;

namespace SignalRChatApp.Client.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "This field can not be empty!")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "This field can not be empty!")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "This field can not be empty!")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "This field can not be empty!")]
        [EmailAddress(ErrorMessage = "Incorrect email format!")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "This field can not be empty!")]
        public required string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password does not match!")]
        [Required(ErrorMessage = "This field can not be empty!")]
        public required string PasswordConfirm { get; set; }
    }
}
