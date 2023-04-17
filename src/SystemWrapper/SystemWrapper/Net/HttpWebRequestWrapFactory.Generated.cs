#pragma warning disable 1591

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
    /// The implementation for the factory generating <see cref="SystemWrapper.Net.HttpWebRequestWrap" /> instances.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("DeveloperInTheFlow.FactoryGenerator", "1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public partial class HttpWebRequestWrapFactory : SystemInterface.Net.IHttpWebRequestFactory
    {
        #region Public Factory Methods

        public SystemInterface.Net.IHttpWebRequest Create(System.Uri requestUri)
        {
            return new SystemWrapper.Net.HttpWebRequestWrap(requestUri);
        }

        public SystemInterface.Net.IHttpWebRequest Create(string requestUriString)
        {
            return new SystemWrapper.Net.HttpWebRequestWrap(requestUriString);
        }

        #endregion
    }
}