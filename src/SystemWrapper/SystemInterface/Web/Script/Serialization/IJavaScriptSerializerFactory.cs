namespace SystemInterface.Web.Script.Serialization
{
    using System.Web.Script.Serialization;

    /// <summary>
    ///     Factory that creates a new <see cref="IJavaScriptSerializer"/> instance.
    /// </summary>
    public interface IJavaScriptSerializerFactory
    {
        /// <summary>
        ///     Creates a new instance of the <see cref='IJavaScriptSerializer'/> class.
        /// </summary>
        /// <returns>
        ///     The <see cref="IJavaScriptSerializer"/>.
        /// </returns>
        IJavaScriptSerializer Create();

        /// <summary>
        ///     Creates a new instance of the <see cref="IJavaScriptSerializer"/> class with the specified resolver.
        /// </summary>
        /// <param name="resolver">
        ///     The resolver.
        /// </param>
        /// <returns>
        ///     The <see cref="IJavaScriptSerializer"/>.
        /// </returns>
        IJavaScriptSerializer Create(JavaScriptTypeResolver resolver);
    }
}