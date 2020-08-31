using Alza.Domain.Entities;
using Alza.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alza.Application.Web.Facades
{
    /// <summary>
    /// Product facade.
    /// </summary>
    public class ProductFacade
    {
        private readonly ProductService productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductFacade"/> class.
        /// </summary>
        /// <param name="productService">Product service</param>
        public ProductFacade(ProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// Gets a single product by it's id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Returns a single instance of the <see cref="Product"/> item.</returns>
        public async Task<Product> GetProductAsync(int id)
        {
            return await this.productService.GetProductAsync(id);
        }

        /// <summary>
        /// Gets a collection of all Products.
        /// </summary>
        /// <returns>Returns a collection of the <see cref="Product"/> items.</returns>
        public async Task<IList<Product>> GetProductsAsync()
        {
            return await this.productService.GetProductsAsync();
        }

        /// <summary>
        /// Gets a paged collection of Products.
        /// </summary>
        /// <param name="page">Current page number.</param>
        /// <param name="pageSize">Current page size.</param>
        /// <returns>Returns a paged collection of the <see cref="Product"/> items.</returns>
        public async Task<IList<Product>> GetProductsAsync(int page, int? pageSize)
        {
            return await this.productService.GetProductsAsync(page, pageSize);
        }

        /// <summary>
        /// Updates a description of the product specified by it's id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="description">The product description.</param>
        /// <returns>Returns a value indicating whether the product description was successfully updated or not.</returns>
        public async Task<bool> UpdateProductDescriptionAsync(int id, string description)
        {
            return await this.productService.UpdateProductDescriptionAsync(id, description);
        }
    }
}