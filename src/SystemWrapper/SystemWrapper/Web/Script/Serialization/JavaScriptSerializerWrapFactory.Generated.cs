#pragma warning disable 1591

namespace SystemWrapper.Web.Script.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.Script.Serialization;

    using SystemInterface.Attributes;
    using SystemInterface.Web.Script.Serialization;

    /// <summary>
    /// The implementation for the factory generating <see cref="SystemWrapper.Web.Script.Serialization.JavaScriptSerializerWrap" /> instances.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("DeveloperInTheFlow.FactoryGenerator", "1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public partial class JavaScriptSerializerWrapFactory : SystemInterface.Web.Script.Serialization.IJavaScriptSerializerFactory
    {
        #region Public Factory Methods

        public SystemInterface.Web.Script.Serialization.IJavaScriptSerializer Create()
        {
            return new SystemWrapper.Web.Script.Serialization.JavaScriptSerializerWrap();
        }

        public SystemInterface.Web.Script.Serialization.IJavaScriptSerializer Create(System.Web.Script.Serialization.JavaScriptTypeResolver resolver)
        {
            return new SystemWrapper.Web.Script.Serialization.JavaScriptSerializerWrap(resolver);
        }

        #endregion
    }
}