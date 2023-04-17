#pragma warning disable 1591

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
    /// The implementation for the factory generating <see cref="SystemWrapper.Net.HttpWebResponseWrap" /> instances.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("DeveloperInTheFlow.FactoryGenerator", "1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public partial class HttpWebResponseWrapFactory : SystemInterface.Net.IHttpWebResponseFactory
    {
        #region Public Factory Methods

        public SystemInterface.Net.IHttpWebResponse Create(System.Net.WebResponse webResponse)
        {
            return new SystemWrapper.Net.HttpWebResponseWrap(webResponse);
        }

        #endregion
    }
}