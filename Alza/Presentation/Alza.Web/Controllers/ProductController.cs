using AutoMapper;
using Alza.Application.Web.Facades;
using Alza.Application.Web.Models;
using Alza.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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
    public class ProductController : ControllerBase
    {
        private readonly ProductFacade productFacade;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productFacade">An instance of the Product facade.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="mapper">An instance of the Automapper class.</param>
        public ProductController(ProductFacade productFacade, IConfiguration configuration, IMapper mapper)
        {
            this.productFacade = productFacade;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets a single product by it's id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Returns a single instance of the <see cref="Product"/> item.</returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Gets a single product by it's id (v1 and v2).", Type = typeof(IActionResult))]
        public IActionResult Get(int id)
        {
            Product product = this.productFacade.GetProduct(id);

            ProductViewModel productViewModel = (product != null) ? this.mapper.Map<ProductViewModel>(product, opts =>
            {
                opts.Items["ImageUrlPrefix"] = configuration["ImageUrlPrefix"];
                opts.Items["ImageNull"] = configuration["ImageNull"];
            }) : new ProductViewModel();

            return Ok(productViewModel);
        }

        /// <summary>
        /// Gets a collection of all products.
        /// </summary>
        /// <returns>Returns a collection of the <see cref="ProductViewModel"/> items.</returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Gets a collection of all products (v1).", Type = typeof(IActionResult))]
        public IActionResult Get()
        {
            IList<Product> products = this.productFacade.GetProducts();

            IList<ProductViewModel> productViewModels = (products != null && products.Any()) ? this.mapper.Map<IEnumerable<ProductViewModel>>(products, opts =>
            {
                opts.Items["ImageUrlPrefix"] = configuration["ImageUrlPrefix"];
                opts.Items["ImageNull"] = configuration["ImageNull"];
            }).ToList() : new List<ProductViewModel>();

            return Ok(productViewModels.ToArray());
        }

        /// <summary>
        /// Gets a collection of all products.
        /// </summary>
        /// <returns>Returns a collection of the <see cref="ProductViewModel"/> items.</returns>
        [HttpGet("{page}/{pageSize}")]
        [MapToApiVersion("2.0")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Gets a collection of all products (v2).", Type = typeof(IActionResult))]
        public IActionResult Get(int page, int? pageSize)
        {
            IList<Product> products = this.productFacade.GetProducts(page, pageSize);

            IList<ProductViewModel> productViewModels = (products != null && products.Any()) ? this.mapper.Map<IEnumerable<ProductViewModel>>(products, opts =>
            {
                opts.Items["ImageUrlPrefix"] = configuration["ImageUrlPrefix"];
                opts.Items["ImageNull"] = configuration["ImageNull"];
            }).ToList() : new List<ProductViewModel>();

            return Ok(productViewModels.ToArray());
        }

        /// <summary>
        /// Updates the product (product description).
        /// </summary>
        /// <param name="productUpdateRequest">An instance of the <see cref="ProductUpdateRequest" />.</param>
        /// <returns></returns>
        [HttpPut]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Updates the product (product description).", Type = typeof(IActionResult))]
        public IActionResult Update([FromBody]ProductUpdateRequest productUpdateRequest)
        {
            if (productUpdateRequest == null)
            {
                return this.BadRequest("The ProductUpdateRequest is null.");
            }

            if (productUpdateRequest.Id == 0)
            {
                return this.BadRequest("The product id is 0.");
            }

            Product product = this.productFacade.GetProduct(productUpdateRequest.Id);
            if (product == null)
            {
                return this.BadRequest($"The product with id {productUpdateRequest.Id} does not seem to exist.");
            }

            product.Description = productUpdateRequest.Description;

            bool returnSucces = this.productFacade.UpdateProduct(product);

            if (!returnSucces)
            {
                return this.Problem(title: "Description not updated", detail: "The product description was not updated.");
            }

            return this.Ok(productUpdateRequest);
        }
    }
}
