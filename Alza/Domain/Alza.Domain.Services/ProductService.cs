using Alza.Domain.Abstractions.Repositories;
using Alza.Domain.Entities;
using System.Collections.Generic;

namespace Alza.Domain.Services
{
    /// <summary>
    /// Product service.
    /// </summary>
    public class ProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMockedProductRepository mockedProductRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">Product repository.</param>
        /// <param name="mockedProductRepository">Mocked product repository.</param>
        public ProductService(IProductRepository productRepository, IMockedProductRepository mockedProductRepository)
        {
            this.productRepository = productRepository;
            this.mockedProductRepository = mockedProductRepository;
        }

        /// <summary>
        /// Gets a single product by it's id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <param name="useMockedData">Use mocked data instead of database data.</param>
        /// <returns>Returns a single instance of the <see cref="Product"/> item.</returns>
        public Product GetProduct(int id, bool useMockedData = false)
        {
            return (useMockedData) ? this.mockedProductRepository.GetProduct(id) : this.productRepository.GetProduct(id);
        }

        /// <summary>
        /// Gets a collection of all Products.
        /// </summary>
        /// <param name="useMockedData">Use mocked data instead of database data.</param>
        /// <returns>Returns a collection of the <see cref="Product"/> items.</returns>
        public IList<Product> GetProducts(bool useMockedData = false)
        {
            return (useMockedData) ? this.mockedProductRepository.GetProducts() : this.productRepository.GetProducts();
        }

        /// <summary>
        /// Gets a paged collection of Products.
        /// </summary>
        /// <param name="page">Current page number.</param>
        /// <param name="pageSize">Current page size.</param>
        /// <param name="useMockedData">Use mocked data instead of database data.</param>
        /// <returns>Returns a paged collection of the <see cref="Product"/> items.</returns>
        public IList<Product> GetProducts(int page, int pageSize, bool useMockedData = false)
        {
            return (useMockedData) ? this.mockedProductRepository.GetProducts(page, pageSize) : this.productRepository.GetProducts(page, pageSize);
        }

        /// <summary>
        /// Updates the product (product description).
        /// </summary>
        /// <param name="product">The product to be updated.</param>
        /// <param name="useMockedData">Update mocked data instead of database data.</param>
        /// <returns>Returns a value indicating whether the product was successfully updated or not.</returns>
        public bool UpdateProduct(Product product, bool useMockedData = false)
        {
            return (useMockedData) ? false : this.productRepository.UpdateProduct(product);
        }
    }
}