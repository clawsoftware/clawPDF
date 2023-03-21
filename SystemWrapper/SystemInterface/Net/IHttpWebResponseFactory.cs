namespace SystemInterface.Net
{
    using System.Net;

    /// <summary>
    ///     Factory to create a new <see cref="IHttpWebResponse"/> instance.
    /// </summary>
    public interface IHttpWebResponseFactory
    {
        /// <summary>
        ///     Creates a new <see cref="IHttpWebResponse"/> instance passing a web response.
        /// </summary>
        /// <param name="webResponse">
        ///     The web response.
        /// </param>
        /// <returns>
        ///     The <see cref="IHttpWebResponse"/>.
        /// </returns>
        IHttpWebResponse Create(WebResponse webResponse);
    }
}