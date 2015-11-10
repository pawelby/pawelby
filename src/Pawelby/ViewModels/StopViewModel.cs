#region Usings
    using System;
    using System.ComponentModel.DataAnnotations;
#endregion

namespace Pawelby.ViewModels
{
    /// <summary>
    /// Stop view model
    /// </summary>
    public class StopViewModel
    {
#region Properties
        /// <summary>
        /// Id of the stop
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the stop
        /// </summary>
        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(255, MinimumLength = 5)]
        public string Name { get; set; }

        /// <summary>
        /// Longitude of the stop
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Latitude of the stop
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Arrival of the stop
        /// </summary>
        [Required(ErrorMessage = "Дата обязательна")]
        public DateTime Arrival { get; set; }
#endregion

    }
}
