#region Usings
using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;
using Pawelby.Models;
using Pawelby.ViewModels;
#endregion

namespace Pawelby.Controllers.Api
{
    /// <summary>
    /// Trip controller
    /// </summary>
    [Authorize]
    [Route("api/trips")]
    public class TripController : Controller
    {
#region Private fields
        private IPawelbyRepository _repository;
        private ILogger<TripController> _logger;

#endregion
        /// <summary>
        /// Constructor of the Trip Controller
        /// </summary>
        /// <param name="repository">Repository</param>
        /// <param name="logger">Logger</param>
        public TripController(IPawelbyRepository repository, ILogger<TripController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Get all trips with stops for current user
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public JsonResult Get()
        {
            var trips = _repository.GetUserTripsWithStops(User.Identity.Name);
            var results = Mapper.Map<IEnumerable<TripViewModel>>(trips);
            return Json(results);
        }

        /// <summary>
        /// Post a new trip via trip view model
        /// </summary>
        /// <param name="vm">Trip view model</param>
        /// <returns></returns>
        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTrip = Mapper.Map<Trip>(vm);
                    newTrip.UserName = User.Identity.Name;

                    _logger.LogInformation("Attempting to save a new trip");
                    _repository.AddTrip(newTrip);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int) HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(newTrip));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new trip", ex);
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json(new {Message = ex.Message});
            }
            

            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState});

        }
    }
}
