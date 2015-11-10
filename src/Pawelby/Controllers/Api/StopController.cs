#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;
using Pawelby.Models;
using Pawelby.Services;
using Pawelby.ViewModels;
#endregion

namespace Pawelby.Controllers.Api
{
    /// <summary>
    /// Stop Controller
    /// </summary>
    [Authorize]
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
#region Private Fields
        private ILogger<StopController> _logger;
        private IPawelbyRepository _repository;
        private CoordService _coordService;
#endregion

        /// <summary>
        /// Constructor of the stop controller, using DI
        /// </summary>
        /// <param name="repository">Repository</param>
        /// <param name="logger">Logger</param>
        /// <param name="coordService">Coordinate Server</param>
        public StopController(IPawelbyRepository repository, ILogger<StopController> logger, CoordService coordService)
        {
            _repository = repository;
            _logger = logger;
            _coordService = coordService;
        }

        /// <summary>
        /// Get stops by trimName
        /// </summary>
        /// <param name="tripName">Name of the trip</param>
        /// <returns>Json result</returns>
        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try
            {
                var results = _repository.GetTripByName(tripName, User.Identity.Name);

                if (results == null)
                {
                    return Json(null);
                }

                return Json(Mapper.Map<IEnumerable<StopViewModel>>(results.Stops.OrderBy(s => s.Order)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get stops for trip {tripName}", ex);
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json("Error occured finding trip name");
            }

        }

        /// <summary>
        /// Post a new stop in a trip
        /// </summary>
        /// <param name="tripName">Name of the trip</param>
        /// <param name="vm">StopViewModel</param>
        /// <returns></returns>
        public async Task<JsonResult> Post(string tripName, [FromBody] StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Map to the Entity
                    var newStop = Mapper.Map<Stop>(vm);


                    // Looking up Geocoordinates
                    var coordResult = await _coordService.Lookup(newStop.Name);

                    if (!coordResult.Success)
                    {
                        Response.StatusCode = (int) HttpStatusCode.BadRequest;
                        Json(coordResult.Message);
                    }

                    newStop.Latitude = coordResult.Latitude;
                    newStop.Longitude = coordResult.Longitude;

                    // Save to the Database
                    _repository.AddStop(tripName, User.Identity.Name, newStop);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int) HttpStatusCode.Created;
                        return Json(Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new stop", ex);
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json("Failed to save new stops");
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Validation failed on new stop");
        }
    }
}
