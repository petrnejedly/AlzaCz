namespace Alza.Web.Models
{
    /// <summary>
    /// An Error view model class.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the RequestId.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether to show the RequestId, based on whether the RequestId value is or is not null or empty.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
