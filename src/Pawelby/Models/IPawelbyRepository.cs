#region Usings
    using System.Collections.Generic;
#endregion

namespace Pawelby.Models
{
    /// <summary>
    /// Interface of the project repository
    /// </summary>
    public interface IPawelbyRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
        void AddTrip(Trip newTrip);
        bool SaveAll();
        Trip GetTripByName(string tripName, string username);
        void AddStop(string tripName, string username, Stop newStop);
        IEnumerable<Trip> GetUserTripsWithStops(string name);
    }
}