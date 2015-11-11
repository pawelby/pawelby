namespace Pawelby.Services
{
    /// <summary>
    /// Coordinate service result class
    /// </summary>
    public class CoordServiceResult
    {
#region Properties
        /// <summary>
        /// Status
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
#endregion Properties
    }
}
