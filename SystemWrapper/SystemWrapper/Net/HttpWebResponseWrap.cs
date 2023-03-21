namespace SystemWrapper.Net
{
    using System;
    using System.IO;
    using System.Net;

    using SystemInterface.Attributes;
    using SystemInterface.IO;
    using SystemInterface.Net;

    using SystemWrapper.IO;

    /// <summary>
    ///     Wrapper for <see cref="T:System.Net.HttpWebResponse"/> class.
    /// </summary>
    [GenerateFactory(typeof(IHttpWebResponseFactory))]
    public class HttpWebResponseWrap : IHttpWebResponse
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpWebResponseWrap"/> class.
        /// </summary>
        /// <param name="webResponse">
        ///     The web response.
        /// </param>
        public HttpWebResponseWrap(WebResponse webResponse)
        {
            this.HttpWebResponseInstance = (HttpWebResponse)webResponse;
        }

        #endregion

        #region Public Properties

        public string CharacterSet
        {
            get
            {
                return this.HttpWebResponseInstance.CharacterSet;
            }
        }

        /// <summary>
        ///     Gets the method used to encode the body of the response.
        /// </summary>
        public string ContentEncoding
        {
            get
            {
                return this.HttpWebResponseInstance.ContentEncoding;
            }
        }

        /// <summary>
        ///     Gets the lenght of the content returned by the request.
        /// </summary>
        public long ContentLength
        {
            get
            {
                return this.HttpWebResponseInstance.ContentLength;
            }
        }

        /// <summary>
        ///     Gets the content type of the response.
        /// </summary>
        public string ContentType
        {
            get
            {
                return this.HttpWebResponseInstance.ContentType;
            }
        }

        public CookieCollection Cookies
        {
            get
            {
                return this.HttpWebResponseInstance.Cookies;
            }

            set
            {
                this.HttpWebResponseInstance.Cookies = value;
            }
        }

        /// <summary>
        ///     Gets the headers associated with this response from the server.
        /// </summary>
        public WebHeaderCollection Headers
        {
            get
            {
                return this.HttpWebResponseInstance.Headers;
            }
        }

        /// <summary>
        ///     Gets the http web response instance.
        /// </summary>
        public HttpWebResponse HttpWebResponseInstance { get; private set; }

        public bool IsMutuallyAuthenticated
        {
            get
            {
                return this.HttpWebResponseInstance.IsMutuallyAuthenticated;
            }
        }

        /// <summary>
        ///     Gets the last date and time that the content of the response was modified.
        /// </summary>
        public DateTime LastModified
        {
            get
            {
                return this.HttpWebResponseInstance.LastModified;
            }
        }

        /// <summary>
        ///     Gets the value of the method used to return the response.
        /// </summary>
        public string Method
        {
            get
            {
                return this.HttpWebResponseInstance.Method;
            }
        }

        /// <summary>
        ///     Gets the version of the HTTP protocol used in the response.
        /// </summary>
        public Version ProtocolVersion
        {
            get
            {
                return this.HttpWebResponseInstance.ProtocolVersion;
            }
        }

        /// <summary>
        ///     Gets the Uniform Resource Indentifier (Uri) of the resource that returned the response.
        /// </summary>
        public Uri ResponseUri
        {
            get
            {
                return this.HttpWebResponseInstance.ResponseUri;
            }
        }

        /// <summary>
        ///     Gets the name of the server that sent the response.
        /// </summary>
        public string Server
        {
            get
            {
                return this.HttpWebResponseInstance.Server;
            }
        }

        /// <summary>
        ///     Gets a number indicating the status of the response.
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get
            {
                return this.HttpWebResponseInstance.StatusCode;
            }
        }

        /// <summary>
        ///     Gets the status description returned with the response.
        /// </summary>
        public string StatusDescription
        {
            get
            {
                return this.HttpWebResponseInstance.StatusDescription;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Closes the Response after the use.
        ///     This causes the read stream to be closed.
        /// </summary>
        public void Close()
        {
            this.HttpWebResponseInstance.Close();
        }

        /// <summary>
        ///     Gets a specified header value returned with the response.
        /// </summary>
        /// <param name="headerName"></param>
        /// <returns></returns>
        public string GetResponseHeader(string headerName)
        {
            return this.HttpWebResponseInstance.GetResponseHeader(headerName);
        }

        /// <summary>
        ///     Gets the stream used for reading the body of the response from the server.
        /// </summary>
        /// <returns></returns>
        public IStream GetResponseStream()
        {
            return new StreamWrap(this.HttpWebResponseInstance.GetResponseStream());
        }

        #endregion
    }
}