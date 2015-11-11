#region Usings
    using System;
#endregion

namespace Pawelby.Models
{
    /// <summary>
    /// Stop model
    /// </summary>
    public class Stop
    {
#region Properties
        /// <summary>
        /// Id of the stop
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the stop
        /// </summary>
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
        /// Arrival date of the stop
        /// </summary>
        public DateTime Arrival { get; set; }
        /// <summary>
        /// Stop order in the trip
        /// </summary>
        public int Order { get; set; }
#endregion
    }
}
