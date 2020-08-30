namespace Alza.Application.Web.Helpers
{
    /// <summary>
    /// String helper class.
    /// </summary>
    public static class StringHelper
    {
        #region public static string CombineUrl(string uri1, string uri2)
        /// <summary>
        /// Combines two strings into an URL address (or part of an URL address).
        /// </summary>
        /// <param name="uri1"></param>
        /// <param name="uri2"></param>
        /// <returns></returns>
        public static string CombineUrl(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return string.Format("{0}/{1}", uri1, uri2);
        }
        #endregion
    }
}
