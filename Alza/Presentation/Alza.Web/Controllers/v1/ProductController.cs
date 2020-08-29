using Microsoft.AspNetCore.Mvc;

namespace Alza.Web.Controllers.v1
{
    /// <summary>
    /// Product Web API controller class (v1)
    /// </summary>
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> (v1) class.
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
            return Ok("Api version 1.0");
        }
    }
}
