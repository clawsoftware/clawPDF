namespace SystemWrapper.Security.Certificate
{
    using SystemInterface.Security.Certificate;

    /// <summary>
    /// This class wraps a X509Chain in order to be mock-able.
    /// </summary>
    public class X509ChainFactory : IX509ChainFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// Wraps the X509Chain creation method.
        /// </summary>
        /// <returns>
        /// The <see cref="IX509Chain"/>.
        /// </returns>
        public IX509Chain Create()
        {
            return new X509ChainWrap();
        }

        #endregion
    }
}