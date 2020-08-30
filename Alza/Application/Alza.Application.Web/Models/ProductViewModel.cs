using System.ComponentModel.DataAnnotations;

namespace Alza.Application.Web.Models
{
    /// <summary>
    /// Product entity (view model) class (Application level).
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Required(ErrorMessage = "Identifier is required.")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product name is required.")]
        [StringLength(200, ErrorMessage = "Product name can't be longer than 200 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the product image uri.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product image uri is required.")]
        [StringLength(200, ErrorMessage = "Product image uri can't be longer than 200 characters")]
        public string ImgUri { get; set; }

        /// <summary>
        /// Gets or sets the product price.
        /// </summary>
        [Required(ErrorMessage = "Product price is required.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the product description.
        /// </summary>
        public string Description { get; set; }
    }
}