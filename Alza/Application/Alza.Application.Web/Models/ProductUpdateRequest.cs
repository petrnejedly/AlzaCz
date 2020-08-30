using System.ComponentModel.DataAnnotations;

namespace Alza.Application.Web.Models
{
    /// <summary>
    /// Product update request model.
    /// </summary>
    public class ProductUpdateRequest
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Required(ErrorMessage = "Identifier is required.")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the product description.
        /// </summary>
        public string Description { get; set; }
    }
}
