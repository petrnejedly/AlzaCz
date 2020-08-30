using System.Collections.Generic;
using Alza.Domain.Entities;

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
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetProduct(int id);

        /// <summary>
        /// Gets a collection of Products.
        /// </summary>
        /// <returns>Returns a collection of the <see cref="Product"/> items.</returns>
        IList<Product> GetProducts();

        /// <summary>
        /// Gets a paged collection of Products.
        /// </summary>
        /// <param name="page">Current page number.</param>
        /// <param name="pageSize">Current page size.</param>
        /// <returns>Returns a paged collection of the <see cref="Product"/> items, paged.</returns>
        IList<Product> GetProducts(int page, int pageSize);
    }
}
