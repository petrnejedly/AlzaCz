using Alza.Domain.Abstractions.Repositories;
using Alza.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alza.Domain.Services
{
    /// <summary>
    /// Product service.
    /// </summary>
    public class ProductService
    {
        private readonly IProductRepository productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">Product repository.</param>
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Gets a single product by it's id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Returns a single instance of the <see cref="Product"/> item.</returns>
        public async Task<Product> GetProduct(int id)
        {
            return await this.productRepository.GetProduct(id);
        }

        /// <summary>
        /// Gets a collection of all Products.
        /// </summary>
        /// <returns>Returns a collection of the <see cref="Product"/> items.</returns>
        public async Task<IList<Product>> GetProducts()
        {
            return await this.productRepository.GetProducts();
        }

        /// <summary>
        /// Gets a paged collection of Products.
        /// </summary>
        /// <param name="page">Current page number.</param>
        /// <param name="pageSize">Current page size.</param>
        /// <returns>Returns a paged collection of the <see cref="Product"/> items.</returns>
        public async Task<IList<Product>> GetProducts(int page, int? pageSize)
        {
            return await this.productRepository.GetProducts(page, pageSize);
        }

        /// <summary>
        /// Updates a description of the product specified by it's id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="description">The product description.</param>
        /// <returns>Returns a value indicating whether the product description was successfully updated or not.</returns>
        public async Task<bool> UpdateProductDescription(int id, string description)
        {
            return await this.productRepository.UpdateProductDescription(id, description);
        }
    }
}