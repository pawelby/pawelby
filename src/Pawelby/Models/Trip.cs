#region Usings
    using System;
    using System.Collections.Generic;
#endregion

namespace Pawelby.Models
{
    /// <summary>
    /// Trip model
    /// </summary>
    public class Trip
    {
#region Properties
        /// <summary>
        /// Id of the trip
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the trip
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Created date of the trip
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Username of the trip
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Stops collection of the trip
        /// </summary>
        public ICollection<Stop> Stops { get; set; }
#endregion
    }
}
