namespace SystemWrapper.ServiceModel
{
    using System;
    using System.ServiceModel;
    using SystemInterface.ServiceModel;

    /// <summary>
    ///     Wraps a WCF channel.
    /// </summary>
    /// <typeparam name="TService">
    ///     The type of the service.
    /// </typeparam>
    public class ChannelWrap<TService> : IChannelWrap<TService>
    {
        #region Fields

        /// <summary>
        ///     The client channel that is given with the constructor.
        /// </summary>
        private readonly IClientChannel channel;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChannelWrap{TService}"/> class.
        /// </summary>
        /// <param name="client">
        ///     The client channel.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Thrown if the client channel is not of the expected service type.
        /// </exception>
        protected internal ChannelWrap(IClientChannel client)
        {
            if (!(client is TService))
            {
                throw new ArgumentException(string.Format("client channel doesn't implement interface {0}", typeof(TService).Name));
            }

            this.channel = client;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the channel service.
        /// </summary>
        public TService Service
        {
            get
            {
                return (TService)this.channel;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(true);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.channel.State != CommunicationState.Faulted)
                {
                    this.channel.Dispose();
                }
                else
                {
                    this.channel.Abort();
                }
            }
        }

        #endregion
    }
}