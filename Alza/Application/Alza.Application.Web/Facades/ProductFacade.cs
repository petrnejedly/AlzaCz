using Alza.Domain.Entities;
using Alza.Domain.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Alza.Application.Web.Facades
{
    /// <summary>
    /// Product facade.
    /// </summary>
    public class ProductFacade
    {
        private readonly ProductService productService;
        private readonly IConfiguration configuration;
        private static bool UseMockData = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductFacade"/> class.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="productService">Product service</param>
        public ProductFacade(IConfiguration configuration, ProductService productService)
        {
            this.configuration = configuration;
            this.productService = productService;
            bool.TryParse(configuration["UseMockData"], out UseMockData);
        }

        /// <summary>
        /// Gets a single product by it's id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Returns a single instance of the <see cref="Product"/> item.</returns>
        public Product GetProduct(int id)
        {
            return this.productService.GetProduct(id, UseMockData);
        }

        /// <summary>
        /// Gets a collection of all Products.
        /// </summary>
        /// <returns>Returns a collection of the <see cref="Product"/> items.</returns>
        public IList<Product> GetProducts()
        {
            return this.productService.GetProducts(UseMockData);
        }

        /// <summary>
        /// Gets a paged collection of Products.
        /// </summary>
        /// <param name="page">Current page number.</param>
        /// <param name="pageSize">Current page size.</param>
        /// <returns>Returns a paged collection of the <see cref="Product"/> items.</returns>
        public IList<Product> GetProducts(int page, int pageSize)
        {
            return this.productService.GetProducts(page, pageSize, UseMockData);
        }

        /// <summary>
        /// Updates the product (product description).
        /// </summary>
        /// <param name="product">The product to be updated.</param>
        /// <returns>Returns a value indicating whether the product was successfully updated or not.</returns>
        public bool UpdateProduct(Product product)
        {
            return this.productService.UpdateProduct(product, UseMockData);
        }
    }
}