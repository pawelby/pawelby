#region Usings
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Data.Entity;
    using Microsoft.Framework.Logging;
#endregion

namespace Pawelby.Models
{
    /// <summary>
    /// The main repository of the project
    /// </summary>
    public class PawelbyRepository : IPawelbyRepository
    {
#region Private fields
        private PawelbyContext _context;
        private ILogger<PawelbyRepository> _logger;
#endregion

        /// <summary>
        /// Constructor of the repository of the project
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="logger">Logger</param>
        public PawelbyRepository(PawelbyContext context, ILogger<PawelbyRepository> logger)
        {
            _context = context;
            _logger = logger;
        } 

        /// <summary>
        /// Get all trips
        /// </summary>
        /// <returns>Trips</returns>
        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return _context.Trips.OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips from database", ex);
                return null;
            }
           
        }

        /// <summary>
        /// Get all trips with stops
        /// </summary>
        /// <returns>Trips with stops</returns>
        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return _context.Trips
                .Include(t => t.Stops)
                .OrderBy(t => t.Name)
                .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips with stops from database", ex);
                return null;
            }
            
        }

        /// <summary>
        /// Add a new trip
        /// </summary>
        /// <param name="newTrip">New trip</param>
        public void AddTrip(Trip newTrip)
        {
            _context.Add(newTrip);
        }

        /// <summary>
        /// Save all changes in the database
        /// </summary>
        /// <returns>True if success</returns>
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// Get trip by name
        /// </summary>
        /// <param name="tripName">Name of the trip</param>
        /// <param name="username">Username</param>
        /// <returns></returns>
        public Trip GetTripByName(string tripName, string username)
        {
            return _context.Trips.Include(t => t.Stops)
                .Where(t => t.Name == tripName && t.UserName == username)
                .FirstOrDefault();
        }

        /// <summary>
        /// Add a new stop of the trip
        /// </summary>
        /// <param name="tripName">Name of the trip</param>
        /// <param name="username">Username</param>
        /// <param name="newStop">A new stop</param>
        public void AddStop(string tripName, string username, Stop newStop)
        {
            var theTrip = GetTripByName(tripName, username);
            if (theTrip.Stops.Count > 0)
            {
                newStop.Order = theTrip.Stops.Max(s => s.Order) + 1;
            }
            theTrip.Stops.Add(newStop);
            _context.Stops.Add(newStop);
        }

        /// <summary>
        /// Get user trips with stops
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Trip</returns>
        public IEnumerable<Trip> GetUserTripsWithStops(string name)
        {
            try
            {
                return _context.Trips
                .Include(t => t.Stops)
                .OrderBy(t => t.Name)
                .Where(t => t.UserName == name)
                .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips with stops from database for current user", ex);
                return null;
            }
        }
    }
}
