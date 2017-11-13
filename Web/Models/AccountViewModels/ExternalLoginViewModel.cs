using System.ComponentModel.DataAnnotations;

namespace SimpleWorkTimeTracker.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
