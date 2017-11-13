using System.ComponentModel.DataAnnotations;

namespace SimpleWorkTimeTracker.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
