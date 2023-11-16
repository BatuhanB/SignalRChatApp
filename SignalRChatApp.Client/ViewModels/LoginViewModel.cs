using System.ComponentModel.DataAnnotations;

namespace SignalRChatApp.Client.ViewModels
{
    public sealed class LoginViewModel
    {
        [Required(ErrorMessage = "This field can not be empty!")]
        [EmailAddress(ErrorMessage = "Incorrect email format!")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "This field can not be empty!")]
        public required string Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
