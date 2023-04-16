namespace SystemInterface.Net
{
    using System;

    /// <summary>
    ///     Factory to create a new <see cref="IHttpWebRequest"/> instance.
    /// </summary>
    public interface IHttpWebRequestFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new <see cref="IHttpWebRequest"/> instance passing an uri.
        /// </summary>
        /// <param name="requestUri">
        ///     The request uri.
        /// </param>
        /// <returns>
        ///     The <see cref="IHttpWebRequest"/>.
        /// </returns>
        IHttpWebRequest Create(Uri requestUri);

        /// <summary>
        ///     Creates a new <see cref="IHttpWebRequest"/> instance passing a string uri.
        /// </summary>
        /// <param name="requestUriString">
        ///     The request uri string.
        /// </param>
        /// <returns>
        ///     The <see cref="IHttpWebRequest"/>.
        /// </returns>
        IHttpWebRequest Create(string requestUriString);

        #endregion
    }
}