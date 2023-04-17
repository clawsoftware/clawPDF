namespace SystemInterface.Security.Certificate
{
    /// <summary>
    /// This interface wraps the X509Chain creation in order to be mock-able.
    /// </summary>
    public interface IX509ChainFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// Wraps the X509Chain creation method.
        /// </summary>
        /// <returns>
        /// The <see cref="IX509Chain"/>.
        /// </returns>
        IX509Chain Create();

        #endregion
    }
}