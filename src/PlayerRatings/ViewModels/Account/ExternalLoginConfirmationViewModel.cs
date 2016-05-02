using System.ComponentModel.DataAnnotations;

namespace PlayerRatings.ViewModels.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        public string DisplayName { get; set; }
    }
}
