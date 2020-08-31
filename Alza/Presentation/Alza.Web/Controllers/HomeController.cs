using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Alza.Web.Models;
using Microsoft.Extensions.Configuration;

namespace Alza.Web.Controllers
{
    /// <summary>
    /// Home controller class.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this.configuration = configuration;
        }

        /// <summary>
        /// An index action.
        /// </summary>
        /// <returns>Returns a view.</returns>
        public IActionResult Index()
        {
            bool.TryParse(this.configuration["UseMockData"], out bool useMockData);
            ViewData.Add("UseMockData", (useMockData) ? "Yes, currently using mock data." : "No, currently using live data.");

            return View();
        }

        /// <summary>
        /// A privacy action.
        /// </summary>
        /// <returns>Returns a view.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// An error action.
        /// </summary>
        /// <returns>Returns a view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
