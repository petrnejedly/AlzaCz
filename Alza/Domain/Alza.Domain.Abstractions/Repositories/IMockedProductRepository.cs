using System.Collections.Generic;
using Alza.Domain.Entities;

namespace Alza.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Mocked product repository interface.
    /// </summary>
    public interface IMockedProductRepository : IBaseRepository
    {
        /// <summary>
        /// Gets a mocked Product by it's id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetProduct(int id);

        /// <summary>
        /// Gets a collection of mocked Products.
        /// </summary>
        /// <returns>Returns a collection of the <see cref="Product"/> items.</returns>
        IList<Product> GetProducts();

        /// <summary>
        /// Gets a paged collection of mocked Products.
        /// </summary>
        /// <param name="page">Current page number.</param>
        /// <param name="pageSize">Current page size.</param>
        /// <returns>Returns a collection of the <see cref="Product"/> items, paged.</returns>
        IList<Product> GetProducts(int page, int? pageSize);

        /// <summary>
        /// Updates the mocked product (mocked product description).
        /// </summary>
        /// <param name="product">The mocked product to be updated.</param>
        /// <returns>Returns a value indicating whether the mocked product was successfully updated or not.</returns>
        public bool UpdateProduct(Product product);
    }
}