namespace SystemInterface.Net
{
    using System;
    using System.Net;

    using SystemInterface.IO;

    public interface IHttpWebResponse
    {
        #region Public Properties

        string CharacterSet { get; }

        /// <summary>
        ///     Gets the method used to encode the body of the response.
        /// </summary>
        String ContentEncoding { get; }

        /// <summary>
        ///     Gets the lenght of the content returned by the request.
        /// </summary>
        long ContentLength { get; }

        /// <summary>
        ///     Gets the content type of the response.
        /// </summary>
        string ContentType { get; }

        CookieCollection Cookies { get; set; }

        /// <summary>
        ///     Gets the headers associated with this response from the server.
        /// </summary>
        WebHeaderCollection Headers { get; }

        bool IsMutuallyAuthenticated { get; }

        /// <summary>
        ///     Gets the last date and time that the content of the response was modified.
        /// </summary>
        DateTime LastModified { get; }

        /// <summary>
        ///     Gets the value of the method used to return the response.
        /// </summary>
        string Method { get; }

        /// <summary>
        ///     Gets the version of the HTTP protocol used in the response.
        /// </summary>
        Version ProtocolVersion { get; }

        /// <summary>
        ///     Gets the Uniform Resource Indentifier (Uri) of the resource that returned the response.
        /// </summary>
        Uri ResponseUri { get; }

        /// <summary>
        ///     Gets the name of the server that sent the response.
        /// </summary>
        string Server { get; }

        /// <summary>
        ///     Gets a number indicating the status of the response.
        /// </summary>
        HttpStatusCode StatusCode { get; }

        /// <summary>
        ///     Gets the status description returned with the response.
        /// </summary>
        string StatusDescription { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Closes the Response after the use.
        ///     This causes the read stream to be closed.
        /// </summary>
        void Close();

        /// <summary>
        ///     Gets a specified header value returned with the response.
        /// </summary>
        /// <param name="headerName"></param>
        /// <returns></returns>
        string GetResponseHeader(string headerName);

        /// <summary>
        ///     Gets the stream used for reading the body of the response from the server.
        /// </summary>
        /// <returns></returns>
        IStream GetResponseStream();

        #endregion
    }
}