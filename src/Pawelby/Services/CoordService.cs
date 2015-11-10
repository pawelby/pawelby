#region Usings
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Framework.Logging;
    using Newtonsoft.Json.Linq;
#endregion

namespace Pawelby.Services
{
    /// <summary>
    /// Service for getting coordinates 
    /// </summary>
    public class CoordService
    {
#region Private fields
        private ILogger<CoordService> _logger;
#endregion

        /// <summary>
        /// Constructor of the coordinate service
        /// </summary>
        /// <param name="logger"></param>
        public CoordService(ILogger<CoordService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Lookup coordinates
        /// </summary>
        /// <param name="location">Location</param>
        /// <returns>Coordinate service result</returns>
        public async Task<CoordServiceResult> Lookup(string location)
        {
            var result = new CoordServiceResult()
            {
                Success = false,
                Message = "Undeterminated failure while looking up coordinates"
            };

            //Lookup Coordinates
            var bingKey = Startup.Configuration["AppSettings:BingKey"];
            var encodedName = WebUtility.UrlEncode(location);
            var url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={bingKey}";

            var client = new HttpClient();

            var json = await client.GetStringAsync(url);

            var results = JObject.Parse(json);
            var resources = results["resourceSets"][0]["resources"];
            if (!resources.HasValues)
            {
                result.Message = $"Could not find '{location}' as a location";
            }
            else
            {
                var confidence = (string) resources[0]["confidence"];
                if (confidence != "High")
                {
                    result.Message = $"Could not find a confident match for '{location}' as a location";
                }
                else
                {
                    var coords = resources[0]["geocodePoints"][0]["coordinates"];
                    result.Latitude = (double) coords[0];
                    result.Longitude = (double) coords[1];
                    result.Success = true;
                    result.Message = "Success";
                }
            }

            return result;
        }
    }

}
