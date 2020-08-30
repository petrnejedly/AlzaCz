namespace Alza.Infrastructure.SqlServer.Entities
{
    /// <summary>
    /// Product entity class (Infrastructure level).
    /// </summary>
    public class Product : Base
    {
        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product image uri.
        /// </summary>
        public string ImgUri { get; set; }

        /// <summary>
        /// Gets or sets the product price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the product description.
        /// </summary>
        public string Description { get; set; }
    }
}
