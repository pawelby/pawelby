#region Usings
    using System.ComponentModel.DataAnnotations;
#endregion

namespace Pawelby.ViewModels
{
    /// <summary>
    /// Contact view model
    /// </summary>
    public class ContactViewModel
    {
#region Properties
        /// <summary>
        /// Name of the contact
        /// </summary>
        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(255, MinimumLength = 5)]
        public string Name { get; set; }

        /// <summary>
        /// Email of the contact
        /// </summary>
        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress]
        public string Email { get; set;  }

        /// <summary>
        /// Message 
        /// </summary>
        [Required(ErrorMessage = "Сообщение обязательно")]
        [StringLength(1024, MinimumLength = 5)]
        public string Message { get; set; }
#endregion
    }
}
