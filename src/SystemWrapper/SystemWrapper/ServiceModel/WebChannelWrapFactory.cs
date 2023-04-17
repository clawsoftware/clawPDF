namespace SystemWrapper.ServiceModel
{
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using SystemInterface.ServiceModel;

    /// <summary>
    ///     This class wraps a channel factory in order to be mock-able.
    /// </summary>
    public class WebChannelWrapFactory : ChannelWrapFactory,
                                         IWebChannelWrapFactory
    {
        #region Methods

        /// <summary>
        ///     Creates the underlying channel factory.
        ///     Called by <see cref="ChannelWrapFactory.CreateChannel{TService}"/> if the <see cref="ChannelWrapFactory.ServiceFactory"/> wasn't previously set.
        /// </summary>
        /// <param name="endpointConfigurationName"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        protected override ChannelFactory<TService> CreateChannelFactory<TService>(string endpointConfigurationName)
        {
            return new WebChannelFactory<TService>(endpointConfigurationName);
        }

        #endregion Methods
    }
}
