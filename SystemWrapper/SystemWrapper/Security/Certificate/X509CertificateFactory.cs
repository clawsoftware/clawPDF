namespace SystemWrapper.Security.Certificate
{
    using System.IO;
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.Security.Certificate;

    /// <summary>
    /// Factory Creating a X509Certificate to avoid bugg in X509Certificate that does not delete tmp files
    /// </summary>
    public class X509CertificateFactory : IX509CertificateFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// Creating an X509Certificate from a byte array
        /// </summary>
        /// <param name="certificateAsBytes">
        /// The certificate as bytes.
        /// </param>
        /// <param name="password">
        /// The password needed to access private key. Can be null if the certificate specified as bytes does not require password decryption.
        /// </param>
        /// <returns>
        /// X509Certificate of the given byte Array
        /// </returns>
        public X509Certificate Create(byte[] certificateAsBytes,
                                      string password = null)
        {
            var filename = Path.GetTempFileName();
            try
            {
                File.WriteAllBytes(filename, certificateAsBytes);

                if (password == null)
                {
                    return new X509Certificate(filename);
                }

                return new X509Certificate(filename, password);
            }
            finally
            {
                File.Delete(filename);
            }
        }

        #endregion
    }
}