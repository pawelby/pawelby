#region Usings
    using Microsoft.AspNet.Authorization;
    using Microsoft.AspNet.Mvc;
    using Pawelby.Services;
    using Pawelby.ViewModels;
#endregion

namespace Pawelby.Controllers.Web
{
    /// <summary>
    /// App Controller
    /// </summary>
    public class AppController : Controller
    {

#region Private fields
        private IMailService _mailService;
#endregion

        /// <summary>
        /// Constructor of App Controller
        /// </summary>
        /// <param name="service">Mail service</param>
        public AppController(IMailService service)
        {
            _mailService = service;
        }

        /// <summary>
        /// Index action
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            return View();
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];

                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("", "Невозможно отправить email, проблемы конфигурации");
                }

                if (_mailService.SendMail(email,
                    email,
                    $"Contact Page from {model.Name} ({model.Email})",
                    model.Message))
                {
                    ModelState.Clear();

                    ViewBag.Message = "Сообщение отправлено. Спасибо";
                }
            }

            return View();
        }
    }
}
