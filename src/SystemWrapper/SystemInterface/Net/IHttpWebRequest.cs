namespace SystemInterface.Net
{
    using System;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.IO;

    public interface IHttpWebRequest
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the value of the Accept header.
        /// </summary>
        string Accept { get; set; }

        /// <summary>
        ///     Gets the Uri that actually responded to the request.
        /// </summary>
        Uri Address { get; }

        /// <summary>
        ///     Enables or disables automatically following redirection responses.
        /// </summary>
        bool AllowAutoRedirect { get; set; }

        /// <summary>
        ///     Enables or disables buffering the data stream sent to the server.
        /// </summary>
        bool AllowWriteStreamBuffering { get; set; }

        // de WebRequest...
        //AuthenticationLevel AuthenticationLevel { get; set; }

        DecompressionMethods AutomaticDecompression { get; set; }

        /// <summary>
        ///     ClientCertificates - sets our certs for our reqest,
        ///     uses a hash of the collection to create a private connection
        ///     group, to prevent us from using the same Connection as
        ///     non-Client Authenticated requests.
        /// </summary>
        X509CertificateCollection ClientCertificates { get; set; }

        /// <summary>
        ///     Gets and sets the value of the Connection header. Setting null clears the header out.
        /// </summary>
        string Connection { get; set; }

        /// <summary>
        ///     ConnectionGroupName - used to control which group
        ///     of connections we use, by default should be null
        /// </summary>
        string ConnectionGroupName { get; set; }

        /// <summary>
        ///     Gets or sets the Content-Length header of the request.
        /// </summary>
        long ContentLength { get; set; }

        /// <summary>
        ///     Gets and sets the value of the Content-Type header. Null clears it out.
        /// </summary>
        string ContentType { get; set; }

        /// <summary>
        ///     Gets/Sets Deletegate used to signal us on Continue callback.
        /// </summary>
        HttpContinueDelegate ContinueDelegate { get; set; }

        CookieContainer CookieContainer { get; set; }

        /// <summary>
        ///     Provides authentication information for the request.
        /// </summary>
        ICredentials Credentials { get; set; }

        /// <summary>
        ///     Gets or sets the value of the Date header.
        /// </summary>
        DateTime Date { get; set; }

        /// <summary>
        ///     Gets or sets the value of the Expect header.
        /// </summary>
        string Expect { get; set; }

        /// <summary>
        ///     Returns <see langword='true'/> if a response has been received from the
        ///     server.
        /// </summary>
        bool HaveResponse { get; }

        /// <summary>
        ///     A collection of HTTP headers stored as name value pairs.
        /// </summary>
        WebHeaderCollection Headers { get; set; }

        string Host { get; set; }

        /// <summary>
        ///     Gets or sets the value of the If-Modified-Since header.
        /// </summary>
        DateTime IfModifiedSince { get; set; }

        /// <summary>
        ///     Gets or sets the value of the Keep-Alive header.
        /// </summary>
        bool KeepAlive { get; set; }

        int MaximumAutomaticRedirections { get; set; }

        int MaximumResponseHeadersLength { get; set; }

        /// <summary>
        ///     Gets or sets the media type header.
        /// </summary>
        string MediaType { get; set; }

        /// <summary>
        ///     Gets or sets the request method.
        ///     This method represents the initial origin Verb, this is unchanged/uneffected by redirects
        /// </summary>
        string Method { get; set; }

        /// <summary>
        ///     Gets or sets the value of Pipelined property.
        /// </summary>
        bool Pipelined { get; set; }

        /// <summary>
        ///     Enables or disables pre-authentication.
        /// </summary>
        bool PreAuthenticate { get; set; }

        /// <summary>
        ///     Gets and sets the HTTP protocol version used in this request.
        /// </summary>
        Version ProtocolVersion { get; set; }

        /// <summary>
        ///     Gets or sets the proxy information for a request.
        /// </summary>
        IWebProxy Proxy { get; set; }

        /// <summary>
        ///     Used to control the Timeout when calling Stream.Read (AND) Stream.Write.
        ///     Effects Streams returned from GetResponse().GetResponseStream() (AND) GetRequestStream().
        ///     Default is 5 mins.
        /// </summary>
        int ReadWriteTimeout { get; set; }

        /// <summary>
        ///     Gets or sets the value of the Referer header.
        /// </summary>
        string Referer { get; set; }

        /// <summary>
        ///     Gets the original Uri of the request.
        ///     This read-only propery returns the Uri for this request. The
        ///     Uri object was created by the constructor and is always non-null.
        ///
        ///     Note that it will always be the base Uri, and any redirects,
        ///     or such will not be indicated.
        /// </summary>
        Uri RequestUri { get; }

        /// <summary>
        ///     Enable and disable sending chunked data to the server.
        /// </summary>
        bool SendChunked { get; set; }

        /// <summary>
        ///     Gets the service point used for this request.  Looks up the ServicePoint for given Uri,
        ///     one isn't already created and assigned to this HttpWebRequest.
        /// </summary>
        ServicePoint ServicePoint { get; }

        /// <summary>
        ///     Timeout is set to 100 seconds by default.
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        ///     Gets or sets the value of the Transfer-Encoding header. Setting null clears it out.
        /// </summary>
        string TransferEncoding { get; set; }

        /// <summary>
        ///     Allows hi-speed NTLM connection sharing with keep-alive
        /// </summary>
        bool UnsafeAuthenticatedConnectionSharing { get; set; }

        /// <summary>
        ///     Allows us to use generic default credentials.
        /// </summary>
        bool UseDefaultCredentials { get; set; }

        /// <summary>
        ///     
        /// </summary>
        string UserAgent { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Adds a range header to the request for a specified range.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        void AddRange(int from,
                      int to);

        /// <summary>
        ///     Adds a range header to the request for a specified range.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        void AddRange(long from,
                      long to);

        /// <summary>
        ///     Adds a range header to a request for a specific
        ///     range from the beginning or end
        ///     of the requested data.
        ///     To add the range from the end pass negative value
        ///     To add the range from the some offset to the end pass positive value
        /// </summary>
        /// <param name="range"></param>
        void AddRange(int range);

        /// <summary>
        ///     Adds a range header to a request for a specific
        ///     range from the beginning or end
        ///     of the requested data.
        ///     To add the range from the end pass negative value
        ///     To add the range from the some offset to the end pass positive value
        /// </summary>
        /// <param name="range"></param>
        void AddRange(long range);

        void AddRange(string rangeSpecifier,
                      int from,
                      int to);

        void AddRange(string rangeSpecifier,
                      long from,
                      long to);

        void AddRange(string rangeSpecifier,
                      int range);

        void AddRange(string rangeSpecifier,
                      long range);

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
        IAsyncResult BeginGetRequestStream(AsyncCallback callback,
                                           object state);

        /// <summary>
        ///     Used to query for the Response of an HTTP Request using Async
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        IAsyncResult BeginGetResponse(AsyncCallback callback,
                                      object state);

        IStream EndGetRequestStream(IAsyncResult asyncResult);

        /// <summary>
        ///     Retreives the Request Stream after an Async operation has completed.
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        IStream EndGetRequestStream(IAsyncResult asyncResult,
                                    out TransportContext context);

        /// <summary>
        ///     Retreives the Response Result from an HTTP Result after an Async operation has completed.
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        IHttpWebResponse EndGetResponse(IAsyncResult asyncResult);

        IStream GetRequestStream();

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
        IStream GetRequestStream(out TransportContext context);

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
        IHttpWebResponse GetResponse();

        #endregion
    }
}