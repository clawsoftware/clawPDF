namespace SystemWrapper.Net
{
    using System;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.Attributes;
    using SystemInterface.IO;
    using SystemInterface.Net;

    using SystemWrapper.IO;

    /// <summary>
    ///     Wrapper for <see cref="T:System.Net.HttpWebRequest"/> class.
    /// </summary>
    [GenerateFactory(typeof(IHttpWebRequestFactory))]
    public class HttpWebRequestWrap : IHttpWebRequest
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpWebRequestWrap"/> class.
        /// </summary>
        /// <param name="requestUri">
        ///     The request uri.
        /// </param>
        public HttpWebRequestWrap(Uri requestUri)
        {
            this.Initialize(requestUri);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpWebRequestWrap"/> class.
        /// </summary>
        /// <param name="requestUriString">
        ///     The request uri string.
        /// </param>
        public HttpWebRequestWrap(string requestUriString)
        {
            this.Initialize(requestUriString);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the value of the Accept header.
        /// </summary>
        public string Accept
        {
            get
            {
                return this.HttpWebRequestInstance.Accept;
            }

            set
            {
                this.HttpWebRequestInstance.Accept = value;
            }
        }

        /// <summary>
        ///     Gets the Uri that actually responded to the request.
        /// </summary>
        public Uri Address
        {
            get
            {
                return this.HttpWebRequestInstance.Address;
            }
        }

        /// <summary>
        ///     Enables or disables automatically following redirection responses.
        /// </summary>
        public bool AllowAutoRedirect
        {
            get
            {
                return this.HttpWebRequestInstance.AllowAutoRedirect;
            }

            set
            {
                this.HttpWebRequestInstance.AllowAutoRedirect = value;
            }
        }

        /// <summary>
        ///     Enables or disables buffering the data stream sent to the server.
        /// </summary>
        public bool AllowWriteStreamBuffering
        {
            get
            {
                return this.HttpWebRequestInstance.AllowWriteStreamBuffering;
            }

            set
            {
                this.HttpWebRequestInstance.AllowWriteStreamBuffering = value;
            }
        }

        public DecompressionMethods AutomaticDecompression
        {
            get
            {
                return this.HttpWebRequestInstance.AutomaticDecompression;
            }

            set
            {
                this.HttpWebRequestInstance.AutomaticDecompression = value;
            }
        }

        /// <summary>
        ///     ClientCertificates - sets our certs for our reqest,
        ///     uses a hash of the collection to create a private connection
        ///     group, to prevent us from using the same Connection as
        ///     non-Client Authenticated requests.
        /// </summary>
        public X509CertificateCollection ClientCertificates
        {
            get
            {
                return this.HttpWebRequestInstance.ClientCertificates;
            }

            set
            {
                this.HttpWebRequestInstance.ClientCertificates = value;
            }
        }

        /// <summary>
        ///     Gets and sets the value of the Connection header. Setting null clears the header out.
        /// </summary>
        public string Connection
        {
            get
            {
                return this.HttpWebRequestInstance.Connection;
            }

            set
            {
                this.HttpWebRequestInstance.Connection = value;
            }
        }

        /// <summary>
        ///     ConnectionGroupName - used to control which group
        ///     of connections we use, by default should be null
        /// </summary>
        public string ConnectionGroupName
        {
            get
            {
                return this.HttpWebRequestInstance.ConnectionGroupName;
            }

            set
            {
                this.HttpWebRequestInstance.ConnectionGroupName = value;
            }
        }

        /// <summary>
        ///     Gets or sets the Content-Length header of the request.
        /// </summary>
        public long ContentLength
        {
            get
            {
                return this.HttpWebRequestInstance.ContentLength;
            }

            set
            {
                this.HttpWebRequestInstance.ContentLength = value;
            }
        }

        /// <summary>
        ///     Gets and sets the value of the Content-Type header. Null clears it out.
        /// </summary>
        public string ContentType
        {
            get
            {
                return this.HttpWebRequestInstance.ContentType;
            }

            set
            {
                this.HttpWebRequestInstance.ContentType = value;
            }
        }

        /// <summary>
        ///     Gets/Sets Deletegate used to signal us on Continue callback.
        /// </summary>
        public HttpContinueDelegate ContinueDelegate
        {
            get
            {
                return this.HttpWebRequestInstance.ContinueDelegate;
            }

            set
            {
                this.HttpWebRequestInstance.ContinueDelegate = value;
            }
        }

        public CookieContainer CookieContainer
        {
            get
            {
                return this.HttpWebRequestInstance.CookieContainer;
            }

            set
            {
                this.HttpWebRequestInstance.CookieContainer = value;
            }
        }

        /// <summary>
        ///     Provides authentication information for the request.
        /// </summary>
        public ICredentials Credentials
        {
            get
            {
                return this.HttpWebRequestInstance.Credentials;
            }

            set
            {
                this.HttpWebRequestInstance.Credentials = value;
            }
        }

        /// <summary>
        ///     Gets or sets the value of the Date header.
        /// </summary>
        public DateTime Date
        {
            get
            {
                return this.HttpWebRequestInstance.Date;
            }

            set
            {
                this.HttpWebRequestInstance.Date = value;
            }
        }

        /// <summary>
        ///     Gets or sets the value of the Expect header.
        /// </summary>
        public string Expect
        {
            get
            {
                return this.HttpWebRequestInstance.Expect;
            }

            set
            {
                this.HttpWebRequestInstance.Expect = value;
            }
        }

        /// <summary>
        ///     Returns <see langword='true'/> if a response has been received from the
        ///     server.
        /// </summary>
        public bool HaveResponse
        {
            get
            {
                return this.HttpWebRequestInstance.HaveResponse;
            }
        }

        /// <summary>
        ///     A collection of HTTP headers stored as name value pairs.
        /// </summary>
        public WebHeaderCollection Headers
        {
            get
            {
                return this.HttpWebRequestInstance.Headers;
            }

            set
            {
                this.HttpWebRequestInstance.Headers = value;
            }
        }

        public string Host
        {
            get
            {
                return this.HttpWebRequestInstance.Host;
            }

            set
            {
                this.HttpWebRequestInstance.Host = value;
            }
        }

        public HttpWebRequest HttpWebRequestInstance { get; private set; }

        /// <summary>
        ///     Gets or sets the value of the If-Modified-Since header.
        /// </summary>
        public DateTime IfModifiedSince
        {
            get
            {
                return this.HttpWebRequestInstance.IfModifiedSince;
            }

            set
            {
                this.HttpWebRequestInstance.IfModifiedSince = value;
            }
        }

        /// <summary>
        ///     Gets or sets the value of the Keep-Alive header.
        /// </summary>
        public bool KeepAlive
        {
            get
            {
                return this.HttpWebRequestInstance.KeepAlive;
            }

            set
            {
                this.HttpWebRequestInstance.KeepAlive = value;
            }
        }

        public int MaximumAutomaticRedirections
        {
            get
            {
                return this.HttpWebRequestInstance.MaximumAutomaticRedirections;
            }

            set
            {
                this.HttpWebRequestInstance.MaximumAutomaticRedirections = value;
            }
        }

        public int MaximumResponseHeadersLength
        {
            get
            {
                return this.HttpWebRequestInstance.MaximumResponseHeadersLength;
            }

            set
            {
                this.HttpWebRequestInstance.MaximumResponseHeadersLength = value;
            }
        }

        /// <summary>
        ///     Gets or sets the media type header.
        /// </summary>
        public string MediaType
        {
            get
            {
                return this.HttpWebRequestInstance.MediaType;
            }

            set
            {
                this.HttpWebRequestInstance.MediaType = value;
            }
        }

        /// <summary>
        ///     Gets or sets the request method.
        ///     This method represents the initial origin Verb, this is unchanged/uneffected by redirects
        /// </summary>
        public string Method
        {
            get
            {
                return this.HttpWebRequestInstance.Method;
            }

            set
            {
                this.HttpWebRequestInstance.Method = value;
            }
        }

        /// <summary>
        ///     Gets or sets the value of Pipelined property.
        /// </summary>
        public bool Pipelined
        {
            get
            {
                return this.HttpWebRequestInstance.Pipelined;
            }

            set
            {
                this.HttpWebRequestInstance.Pipelined = value;
            }
        }

        /// <summary>
        ///     Enables or disables pre-authentication.
        /// </summary>
        public bool PreAuthenticate
        {
            get
            {
                return this.HttpWebRequestInstance.PreAuthenticate;
            }

            set
            {
                this.HttpWebRequestInstance.PreAuthenticate = value;
            }
        }

        /// <summary>
        ///     Gets and sets the HTTP protocol version used in this request.
        /// </summary>
        public Version ProtocolVersion
        {
            get
            {
                return this.HttpWebRequestInstance.ProtocolVersion;
            }

            set
            {
                this.HttpWebRequestInstance.ProtocolVersion = value;
            }
        }

        /// <summary>
        ///     Gets or sets the proxy information for a request.
        /// </summary>
        public IWebProxy Proxy
        {
            get
            {
                return this.HttpWebRequestInstance.Proxy;
            }

            set
            {
                this.HttpWebRequestInstance.Proxy = value;
            }
        }

        /// <summary>
        ///     Used to control the Timeout when calling Stream.Read (AND) Stream.Write.
        ///     Effects Streams returned from GetResponse().GetResponseStream() (AND) GetRequestStream().
        ///     Default is 5 mins.
        /// </summary>
        public int ReadWriteTimeout
        {
            get
            {
                return this.HttpWebRequestInstance.ReadWriteTimeout;
            }

            set
            {
                this.HttpWebRequestInstance.ReadWriteTimeout = value;
            }
        }

        /// <summary>
        ///     Gets or sets the value of the Referer header.
        /// </summary>
        public string Referer
        {
            get
            {
                return this.HttpWebRequestInstance.Referer;
            }

            set
            {
                this.HttpWebRequestInstance.Referer = value;
            }
        }

        /// <summary>
        ///     Gets the original Uri of the request.
        ///     This read-only propery returns the Uri for this request. The
        ///     Uri object was created by the constructor and is always non-null.
        ///
        ///     Note that it will always be the base Uri, and any redirects,
        ///     or such will not be indicated.
        /// </summary>
        public Uri RequestUri
        {
            get
            {
                return this.HttpWebRequestInstance.RequestUri;
            }
        }

        /// <summary>
        ///     Enable and disable sending chunked data to the server.
        /// </summary>
        public bool SendChunked
        {
            get
            {
                return this.HttpWebRequestInstance.SendChunked;
            }

            set
            {
                this.HttpWebRequestInstance.SendChunked = value;
            }
        }

        /// <summary>
        ///     Gets the service point used for this request.  Looks up the ServicePoint for given Uri,
        ///     one isn't already created and assigned to this HttpWebRequest.
        /// </summary>
        public ServicePoint ServicePoint
        {
            get
            {
                return this.HttpWebRequestInstance.ServicePoint;
            }
        }

        /// <summary>
        ///     Timeout is set to 100 seconds by default.
        /// </summary>
        public int Timeout
        {
            get
            {
                return this.HttpWebRequestInstance.Timeout;
            }

            set
            {
                this.HttpWebRequestInstance.Timeout = value;
            }
        }

        /// <summary>
        ///     Gets or sets the value of the Transfer-Encoding header. Setting null clears it out.
        /// </summary>
        public string TransferEncoding
        {
            get
            {
                return this.HttpWebRequestInstance.TransferEncoding;
            }

            set
            {
                this.HttpWebRequestInstance.TransferEncoding = value;
            }
        }

        /// <summary>
        ///     Allows hi-speed NTLM connection sharing with keep-alive
        /// </summary>
        public bool UnsafeAuthenticatedConnectionSharing
        {
            get
            {
                return this.HttpWebRequestInstance.UnsafeAuthenticatedConnectionSharing;
            }

            set
            {
                this.HttpWebRequestInstance.UnsafeAuthenticatedConnectionSharing = value;
            }
        }

        /// <summary>
        ///     Allows us to use generic default credentials.
        /// </summary>
        public bool UseDefaultCredentials
        {
            get
            {
                return this.HttpWebRequestInstance.UseDefaultCredentials;
            }

            set
            {
                this.HttpWebRequestInstance.UseDefaultCredentials = value;
            }
        }

        /// <summary>
        ///     Gets or sets the value of the User-Agent header.
        /// </summary>
        public string UserAgent
        {
            get
            {
                return this.HttpWebRequestInstance.UserAgent;
            }

            set
            {
                this.HttpWebRequestInstance.UserAgent = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Adds a range header to the request for a specified range.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void AddRange(int @from,
                             int to)
        {
            this.HttpWebRequestInstance.AddRange(from, to);
        }

        /// <summary>
        ///     Adds a range header to the request for a specified range.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void AddRange(long @from,
                             long to)
        {
            this.HttpWebRequestInstance.AddRange(from, to);
        }

        /// <summary>
        ///     Adds a range header to a request for a specific
        ///     range from the beginning or end
        ///     of the requested data.
        ///     To add the range from the end pass negative value
        ///     To add the range from the some offset to the end pass positive value
        /// </summary>
        /// <param name="range"></param>
        public void AddRange(int range)
        {
            this.HttpWebRequestInstance.AddRange(range);
        }

        /// <summary>
        ///     Adds a range header to a request for a specific
        ///     range from the beginning or end
        ///     of the requested data.
        ///     To add the range from the end pass negative value
        ///     To add the range from the some offset to the end pass positive value
        /// </summary>
        /// <param name="range"></param>
        public void AddRange(long range)
        {
            this.HttpWebRequestInstance.AddRange(range);
        }

        public void AddRange(string rangeSpecifier,
                             int @from,
                             int to)
        {
            this.HttpWebRequestInstance.AddRange(rangeSpecifier, from, to);
        }

        public void AddRange(string rangeSpecifier,
                             long @from,
                             long to)
        {
            this.HttpWebRequestInstance.AddRange(rangeSpecifier, from, to);
        }

        public void AddRange(string rangeSpecifier,
                             int range)
        {
            this.HttpWebRequestInstance.AddRange(rangeSpecifier, range);
        }

        public void AddRange(string rangeSpecifier,
                             long range)
        {
            this.HttpWebRequestInstance.AddRange(rangeSpecifier, range);
        }

        /// <summary>
        ///     Retreives the Request Stream from an HTTP Request uses an Async
        ///     operation to do this, and the result is retrived async.
        ///  
        ///     Async operations include work in progess, this call is used to retrieve
        ///     results by pushing the async operations to async worker thread on the callback.
        ///     There are many open issues involved here including the handling of possible blocking
        ///     within the bounds of the async worker thread or the case of Write and Read stream
        ///     operations still blocking.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginGetRequestStream(AsyncCallback callback,
                                                  object state)
        {
            return this.HttpWebRequestInstance.BeginGetRequestStream(callback, state);
        }

        /// <summary>
        ///     Used to query for the Response of an HTTP Request using Async
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginGetResponse(AsyncCallback callback,
                                             object state)
        {
            return this.HttpWebRequestInstance.BeginGetResponse(callback, state);
        }

        public IStream EndGetRequestStream(IAsyncResult asyncResult)
        {
            return new StreamWrap(this.HttpWebRequestInstance.EndGetRequestStream(asyncResult));
        }

        /// <summary>
        ///     Retreives the Request Stream after an Async operation has completed.
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IStream EndGetRequestStream(IAsyncResult asyncResult,
                                           out TransportContext context)
        {
            return new StreamWrap(this.HttpWebRequestInstance.EndGetRequestStream(asyncResult, out context));
        }

        /// <summary>
        ///     Retreives the Response Result from an HTTP Result after an Async operation has completed.
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        public IHttpWebResponse EndGetResponse(IAsyncResult asyncResult)
        {
            return new HttpWebResponseWrap(this.HttpWebRequestInstance.EndGetResponse(asyncResult));
        }

        public IStream GetRequestStream()
        {
            return new StreamWrap(this.HttpWebRequestInstance.GetRequestStream());
        }

        /// <summary>
        ///     Gets a <see cref='System.IO.Stream'/> that the application can use to write request data.
        ///     This property returns a stream that the calling application can write on.
        ///     This property is not settable.  Getting this property may cause the
        ///     request to be sent, if it wasn't already. Getting this property after
        ///     a request has been sent that doesn't have an entity body causes an
        ///     exception to be thrown.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IStream GetRequestStream(out TransportContext context)
        {
            return new StreamWrap(this.HttpWebRequestInstance.GetRequestStream(out context));
        }

        /// <summary>
        ///     Returns a response from a request to an Internet resource.
        ///     The response property. This property returns the WebResponse for this
        ///     request. This may require that a request be submitted first.
        ///
        ///     The idea is that we look and see if a request has already been
        ///     submitted. If one has, we'll just return the existing response
        ///     (if it's not null). If we haven't submitted a request yet, we'll
        ///     do so now, possible multiple times while we handle redirects
        ///     etc.
        /// </summary>
        /// <returns></returns>
        public IHttpWebResponse GetResponse()
        {
            return new HttpWebResponseWrap(this.HttpWebRequestInstance.GetResponse());
        }

        #endregion

        #region Methods

        private void Initialize(Uri requestUri)
        {
            this.HttpWebRequestInstance = (HttpWebRequest)WebRequest.Create(requestUri);
        }

        private void Initialize(string requestUriString)
        {
            this.HttpWebRequestInstance = (HttpWebRequest)WebRequest.Create(requestUriString);
        }

        #endregion
    }
}