using Microsoft.AspNetCore.Mvc;

namespace Alza.Web.Controllers.v2
{
    /// <summary>
    /// Product Web API controller class (v2)
    /// </summary>
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> (v2) class.
        /// </summary>
        public ProductController()
        {

        }

        /// <summary>
        /// Index action.
        /// </summary>
        /// <returns>Returns current ApiVersion.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Api version 2.0");
        }
    }
}
