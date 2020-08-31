using Alza.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alza.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Product repository interface.
    /// </summary>
    public interface IProductRepository : IBaseRepository
    {
        /// <summary>
        /// Gets a Product by it's id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns></returns>
        Task<Product> GetProduct(int id);

        /// <summary>
        /// Gets a collection of all Products.
        /// </summary>
        /// <returns>Returns a collection of the <see cref="Product"/> items.</returns>
        Task<IList<Product>> GetProducts();

        /// <summary>
        /// Gets a paged collection of Products.
        /// </summary>
        /// <param name="page">Current page number.</param>
        /// <param name="pageSize">Current page size.</param>
        /// <returns>Returns a paged collection of the <see cref="Product"/> items.</returns>
        Task<IList<Product>> GetProducts(int page, int? pageSize);

        /// <summary>
        /// Updates a description of the product specified by it's id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="description">The product description.</param>
        /// <returns>Returns a value indicating whether the product description was successfully updated or not.</returns>
        public Task<bool> UpdateProductDescription(int id, string description);
    }
}
