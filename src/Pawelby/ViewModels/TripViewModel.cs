#region Usings
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
#endregion

namespace Pawelby.ViewModels
{
    /// <summary>
    /// Trip view model
    /// </summary>
    public class TripViewModel
    {
#region Properties
        /// <summary>
        /// Id of the trip
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the trip
        /// </summary>
        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(255, MinimumLength = 5)]
        public string Name { get; set; }

        /// <summary>
        /// Created date of the trip
        /// </summary>
        public DateTime Created { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Stops of the trip
        /// </summary>
        public IEnumerable<StopViewModel> Stops { get; set; }
#endregion

    }
}
