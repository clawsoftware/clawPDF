namespace SystemWrapper.ServiceModel
{
    using System.Security.Cryptography.X509Certificates;
    using System.ServiceModel;

    using SystemInterface.ServiceModel;

    /// <summary>
    ///     This class wraps a channel factory in order to be mock-able.
    /// </summary>
    public class ChannelWrapFactory : IChannelWrapFactory
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the service factory.
        /// </summary>
        public ChannelFactory ServiceFactory { get; set; }

        /// <summary>
        ///     Gets or sets the certificate which is used for authentication.
        /// </summary>
        public X509Certificate2 Certificate { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Creates the channel using a new <see cref="ChannelFactory&lt;TService&gt;"/> and the given <see cref="endpointConfigurationName"/>.
        /// </summary>
        /// <param name="endpointConfigurationName">
        ///     The string that gives the name of the endpoint configuration.
        /// </param>
        /// <typeparam name="TService">
        ///     The type of the service.
        /// </typeparam>
        /// <returns>
        ///     The new <see cref="ChannelWrap&lt;TService&gt;"/> that corresponds to the <see cref="endpointConfigurationName"/>.
        /// </returns>
        public IChannelWrap<TService> CreateChannel<TService>(string endpointConfigurationName) where TService : class
        {
            if (this.ServiceFactory == null)
            {
                this.ServiceFactory = this.CreateChannelFactory<TService>(endpointConfigurationName);
            }

// ReSharper disable once PossibleNullReferenceException
            this.ServiceFactory.Credentials.ClientCertificate.Certificate = this.Certificate;

            return new ChannelWrap<TService>((IClientChannel)((ChannelFactory<TService>)this.ServiceFactory).CreateChannel());
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Creates the underlying channel factory.
        ///     Called by <see cref="CreateChannel{TService}"/> if the <see cref="ServiceFactory"/> wasn't previously set.
        /// </summary>
        /// <param name="endpointConfigurationName"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        protected virtual ChannelFactory<TService> CreateChannelFactory<TService>(string endpointConfigurationName) where TService : class
        {
            return new ChannelFactory<TService>(endpointConfigurationName);
        }

        #endregion
    }
}