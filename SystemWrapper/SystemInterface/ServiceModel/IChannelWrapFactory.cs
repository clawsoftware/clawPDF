namespace SystemInterface.ServiceModel
{
    using System.Security.Cryptography.X509Certificates;
    using System.ServiceModel;

    /// <summary>
    ///     This interface wraps the channel creation in order to be mock-able.
    /// </summary>
    public interface IChannelWrapFactory
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the service factory.
        /// </summary>
        ChannelFactory ServiceFactory { get; set; }

        /// <summary>
        ///     Gets or sets the certificate which is used for authentication.
        /// </summary>
        X509Certificate2 Certificate { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Wraps the channel creation method.
        /// </summary>
        /// <param name="enpointConfigurationName">
        ///     The string that gives the name of the endpoint configuration.
        /// </param>
        /// <returns>
        ///     A channel wrap.
        /// </returns>
        IChannelWrap<TService> CreateChannel<TService>(string enpointConfigurationName) where TService : class;

        #endregion
    }
}