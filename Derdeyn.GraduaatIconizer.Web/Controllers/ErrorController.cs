using Derdeyn.GraduaatIconizer.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Derdeyn.GraduaatIconizer.Web.Controllers
{
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index(int? statusCode)
        {
            ViewBag.NavItem = "Home";

            var errormodel = new ErrorViewModel
            {
                StatusCode = statusCode,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View("Error", errormodel);
        }
    }
}