using System.ComponentModel.DataAnnotations;

namespace PlayerRatings.ViewModels.Home
{
    public class ContactViewModel
    {
        [Required]
        public string ClientContact { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
