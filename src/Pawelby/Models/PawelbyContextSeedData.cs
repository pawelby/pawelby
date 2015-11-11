#region Usings
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
#endregion

namespace Pawelby.Models
{
    /// <summary>
    /// Class for filling data in the database
    /// </summary>
    public class PawelbyContextSeedData
    {
#region Private Fields
        private PawelbyContext _context;
        private UserManager<PawelbyUser> _userManager;
#endregion

        /// <summary>
        /// Constructor of the seeding data class
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="userManager">User manager</param>
        public PawelbyContextSeedData(PawelbyContext context, UserManager<PawelbyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Ensure data filling
        /// </summary>
        /// <returns></returns>
        public async Task EnsureSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync("poltest@pawelby.com") == null)
            {
                //Add the user.
                var newUser = new PawelbyUser()
                {
                    UserName = "poltest",
                    Email = "poltest@pawelby.com"
                };

                await _userManager.CreateAsync(newUser, "Possword6!");
            }

            if (!_context.Trips.Any())
            {
                //Add new Data
                var euTrip = new Trip()
                {
                    Name = "EU Trip",
                    Created = DateTime.UtcNow,
                    UserName = "poltest",
                    Stops = new List<Stop>()
                    {
						new Stop() { Name = "Vilnius", Arrival = new DateTime(2013, 5, 8), Latitude = 54.68938650, Longitude = 25.28002430, Order = 0 },
						new Stop() { Name = "Eindhoven", Arrival = new DateTime(2013, 5, 9), Latitude = 51.441642, Longitude = 5.4697225, Order = 1},
						new Stop() { Name = "Amsterdam", Arrival = new DateTime(2013, 5, 9), Latitude = 52.3702157, Longitude = 4.8951679, Order = 2},
                        new Stop() { Name = "Eindhoven", Arrival = new DateTime(2013, 5, 13), Latitude = 51.441642, Longitude = 5.4697225, Order = 3},
						new Stop() { Name = "Warsaw", Arrival = new DateTime(2013, 5, 13), Latitude = 52.2296756, Longitude = 21.0122287, Order = 4}
                    }
                };

                _context.Trips.Add(euTrip);
				_context.Stops.AddRange(euTrip.Stops);

                var indianTrip = new Trip()
                {
                    Name = "Indian Trip",
                    Created = DateTime.UtcNow,
                    UserName = "poltest",
                    Stops = new List<Stop>()
                    {
						new Stop() { Name = "Kiev", Arrival = new DateTime(2014, 2, 13), Latitude = 50.4501, Longitude = 30.5234, Order = 0 },
                        new Stop() { Name = "Dubai", Arrival = new DateTime(2014, 2, 14), Latitude = 25.271139, Longitude = 55.307485, Order = 1 },
                        new Stop() { Name = "Delhi", Arrival = new DateTime(2014, 2, 14), Latitude = 28.635308, Longitude = 77.22496, Order = 2 },
                        new Stop() { Name = "Panaji, Goa", Arrival = new DateTime(2014, 2, 16), Latitude = 15.495602, Longitude = 73.825209, Order = 3 },
                        new Stop() { Name = "Delhi", Arrival = new DateTime(2014, 2, 26), Latitude = 28.635308, Longitude = 77.22496, Order = 4 },
                        new Stop() { Name = "Dubai", Arrival = new DateTime(2014, 2, 28), Latitude = 25.271139, Longitude = 55.307485, Order = 5 },
                        new Stop() { Name = "Kiev", Arrival = new DateTime(2014, 3, 1), Latitude = 50.4501, Longitude = 30.5234, Order = 6 },
                    }
                };

                _context.Trips.Add(indianTrip);
				_context.Stops.AddRange(indianTrip.Stops);

                _context.SaveChanges();
            }
        }
    }
}