#region Usings
    using System;
    using Microsoft.AspNet.Identity.EntityFramework;
#endregion

namespace Pawelby.Models
{
    /// <summary>
    /// User model
    /// </summary>
    public class PawelbyUser : IdentityUser
    {
        /// <summary>
        /// Date time of the first trips
        /// </summary>
        public DateTime FirstTrip { get; set; }
    }
}
