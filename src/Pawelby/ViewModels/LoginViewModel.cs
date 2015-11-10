#region Usings
    using System.ComponentModel.DataAnnotations;
#endregion

namespace Pawelby.ViewModels
{
    /// <summary>
    /// Login view model
    /// </summary>
    public class LoginViewModel
    {
#region Properties
        /// <summary>
        /// Username
        /// </summary>
        [Required(ErrorMessage = "Логин обязателен")]
        public string Username { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Пароль обязателен")]
        public string Password { get; set; }
#endregion
    }
}
