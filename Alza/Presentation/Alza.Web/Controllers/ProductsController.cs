using AutoMapper;
using Alza.Application.Web.Facades;
using Alza.Application.Web.Models;
using Alza.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace Alza.Web.Controllers
{
    /// <summary>
    /// Product Web API controller class.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductFacade productFacade;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly ILogger<ProductsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productFacade">An instance of the Product facade.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="mapper">An instance of the Automapper class.</param>
        /// <param name="logger">The logger.</param>
        public ProductsController(ProductFacade productFacade, IConfiguration configuration, IMapper mapper, ILogger<ProductsController> logger)
        {
            this.productFacade = productFacade;
            this.configuration = configuration;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// Gets a single product by it's id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Returns a single instance of the <see cref="ProductViewModel"/> item.</returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Gets a single product by it's id (v1 and v2).", Type = typeof(IActionResult))]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
            {
                return this.BadRequest(new ApiResponseViewModel() { Message = "The product id must be greater than zero." });
            }

            Product product = new Product();

            try
            {
                product = await this.productFacade.GetProductAsync(id);
            }
            catch (Exception eX)
            {
                this.logger.LogError(eX.Message, eX);
            }

            if (product == null || product.Id == 0)
            {
                return this.NotFound(new ApiResponseViewModel() { Message = $"The product with id {id} was not found." });
            }

            ProductViewModel productViewModel = this.mapper.Map<ProductViewModel>(product, opts =>
            {
                opts.Items["ImageUrlPrefix"] = configuration["ImageUrlPrefix"];
                opts.Items["ImageNull"] = configuration["ImageNull"];
            });

            return this.Ok(productViewModel);
        }

        /// <summary>
        /// Gets a collection of all Products.
        /// </summary>
        /// <returns>Returns a collection of the <see cref="ProductViewModel"/> items.</returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Gets a collection of all products (v1).", Type = typeof(IActionResult))]
        public async Task<IActionResult> Get()
        {
            IList<Product> products = new List<Product>();

            try
            {
                products = await this.productFacade.GetProductsAsync();
            }
            catch (Exception eX)
            {
                this.logger.LogError(eX.Message, eX);
            }

            IList<ProductViewModel> productViewModels = this.mapper.Map<IEnumerable<ProductViewModel>>(products, opts =>
            {
                opts.Items["ImageUrlPrefix"] = configuration["ImageUrlPrefix"];
                opts.Items["ImageNull"] = configuration["ImageNull"];
            }).ToList();

            if (products.Count == 0)
            {
                return this.NoContent();
            }

            return this.Ok(productViewModels.ToArray());
        }

        /// <summary>
        /// Gets a paged collection of Products.
        /// </summary>
        /// <returns>Returns a collection of the <see cref="ProductViewModel"/> items.</returns>
        [HttpGet("{page}/{pageSize}")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Gets a paged collection of products (v2).", Type = typeof(IActionResult))]
        public async Task<IActionResult> Get([FromRoute] int page, [FromRoute] int? pageSize)
        {
            IList<Product> products = new List<Product>();

            try
            {
                products = await this.productFacade.GetProductsAsync(page, pageSize);
            }
            catch (Exception eX)
            {
                this.logger.LogError(eX.Message, eX);
            }

            IList<ProductViewModel> productViewModels = this.mapper.Map<IEnumerable<ProductViewModel>>(products, opts =>
            {
                opts.Items["ImageUrlPrefix"] = configuration["ImageUrlPrefix"];
                opts.Items["ImageNull"] = configuration["ImageNull"];
            }).ToList();

            if (products.Count == 0)
            {
                return this.NoContent();
            }

            return this.Ok(productViewModels.ToArray());
        }

        /// <summary>
        /// Updates a description of the product specified by it's id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="description">The product description.</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Updates the product (product description).", Type = typeof(IActionResult))]
        public async Task<IActionResult> UpdateDescription([FromRoute] int id, [FromBody]string description)
        {
            if (id < 1)
            {
                return this.BadRequest(new ApiResponseViewModel() { Message = "The product id must be greater than zero." });
            }

            bool returnSucces = false;

            try
            {
                returnSucces = await this.productFacade.UpdateProductDescriptionAsync(id, description);
            }
            catch (Exception eX)
            {
                this.logger.LogError(eX.Message, eX);
            }

            if (!returnSucces)
            {
                return this.Problem(title: "Description not updated", detail: "The description has not been updated.");
            }

            return this.Ok(new ApiResponseViewModel() { Message = $"The description has been successfully updated." });
        }
    }
}
