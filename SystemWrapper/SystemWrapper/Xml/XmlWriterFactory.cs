
#pragma warning disable 1584,1711,1572,1581,1580

namespace SystemWrapper.Xml
{
    using System.IO;
    using System.Text;
    using System.Xml;

    using SystemInterface.Xml;

    /// <summary>
    ///     The factory responsible for the creation of an instance of <see cref="XmlWriter"/> class.
    /// </summary>
    public class XmlWriterFactory : IXmlWriterFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the specified filename.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="outputFileName">The file to which you want to write. The <see cref="T:System.Xml.XmlWriter"/> creates a file at the specified path and writes to it in XML 1.0 text syntax. The <paramref name="outputFileName"/> must be a file system path.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="url"/> value is null.</exception>
        public IXmlWriter Create(string outputFileName)
        {
            return new XmlWriterWrap(outputFileName);
        }

        /// <summary>
        /// Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the filename and <see cref="T:System.Xml.XmlWriterSettings"/> object.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="outputFileName">The file to which you want to write. The <see cref="T:System.Xml.XmlWriter"/> creates a file at the specified path and writes to it in XML 1.0 text syntax. The <paramref name="outputFileName"/> must be a file system path.</param>
        /// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings"/> object used to configure the new <see cref="T:System.Xml.XmlWriter"/> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings"/> with default settings is used.If the <see cref="T:System.Xml.XmlWriter"/> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)"/> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings"/> property to obtain an <see cref="T:System.Xml.XmlWriterSettings"/> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter"/> object has the correct output settings.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="url"/> value is null.</exception>
        public IXmlWriter Create(string outputFileName,
                                 XmlWriterSettings settings)
        {
            return new XmlWriterWrap(outputFileName, settings);
        }

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the specified stream.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="output">The stream to which you want to write. The <see cref="T:System.Xml.XmlWriter"/> writes XML 1.0 text syntax and appends it to the specified stream.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="stream"/> value is null.</exception>
        public IXmlWriter Create(Stream output)
        {
            return new XmlWriterWrap(output);
        }

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the stream and <see cref="T:System.Xml.XmlWriterSettings"/> object.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="output">The stream to which you want to write. The <see cref="T:System.Xml.XmlWriter"/> writes XML 1.0 text syntax and appends it to the specified stream.</param>
        /// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings"/> object used to configure the new <see cref="T:System.Xml.XmlWriter"/> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings"/> with default settings is used.If the <see cref="T:System.Xml.XmlWriter"/> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)"/> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings"/> property to obtain an <see cref="T:System.Xml.XmlWriterSettings"/> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter"/> object has the correct output settings.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="stream"/> value is null.</exception>
        public IXmlWriter Create(Stream output,
                                 XmlWriterSettings settings)
        {
            return new XmlWriterWrap(output, settings);
        }

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the specified <see cref="T:System.IO.TextWriter"/>.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="output">The <see cref="T:System.IO.TextWriter"/> to which you want to write. The <see cref="T:System.Xml.XmlWriter"/> writes XML 1.0 text syntax and appends it to the specified <see cref="T:System.IO.TextWriter"/>.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="text"/> value is null.</exception>
        public IXmlWriter Create(TextWriter output)
        {
            return new XmlWriterWrap(output);
        }

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the <see cref="T:System.IO.TextWriter"/> and <see cref="T:System.Xml.XmlWriterSettings"/> objects.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="output">The <see cref="T:System.IO.TextWriter"/> to which you want to write. The <see cref="T:System.Xml.XmlWriter"/> writes XML 1.0 text syntax and appends it to the specified <see cref="T:System.IO.TextWriter"/>.</param>
        /// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings"/> object used to configure the new <see cref="T:System.Xml.XmlWriter"/> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings"/> with default settings is used.If the <see cref="T:System.Xml.XmlWriter"/> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)"/> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings"/> property to obtain an <see cref="T:System.Xml.XmlWriterSettings"/> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter"/> object has the correct output settings.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="text"/> value is null.</exception>
        public IXmlWriter Create(TextWriter output,
                                 XmlWriterSettings settings)
        {
            return new XmlWriterWrap(output, settings);
        }

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the specified <see cref="T:System.Text.StringBuilder"/>.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="output">The <see cref="T:System.Text.StringBuilder"/> to which to write to. Content written by the <see cref="T:System.Xml.XmlWriter"/> is appended to the <see cref="T:System.Text.StringBuilder"/>.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="builder"/> value is null.</exception>
        public IXmlWriter Create(StringBuilder output)
        {
            return new XmlWriterWrap(output);
        }

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the <see cref="T:System.Text.StringBuilder"/> and <see cref="T:System.Xml.XmlWriterSettings"/> objects.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="output">The <see cref="T:System.Text.StringBuilder"/> to which to write to. Content written by the <see cref="T:System.Xml.XmlWriter"/> is appended to the <see cref="T:System.Text.StringBuilder"/>.</param>
        /// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings"/> object used to configure the new <see cref="T:System.Xml.XmlWriter"/> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings"/> with default settings is used.If the <see cref="T:System.Xml.XmlWriter"/> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)"/> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings"/> property to obtain an <see cref="T:System.Xml.XmlWriterSettings"/> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter"/> object has the correct output settings.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="builder"/> value is null.</exception>
        public IXmlWriter Create(StringBuilder output,
                                 XmlWriterSettings settings)
        {
            return new XmlWriterWrap(output, settings);
        }

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the specified <see cref="T:System.Xml.XmlWriter"/> object.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.XmlWriter"/> object that is wrapped around the specified <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="output">The <see cref="T:System.Xml.XmlWriter"/> object that you want to use as the underlying writer.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="writer"/> value is null.</exception>
        public IXmlWriter Create(XmlWriter output)
        {
            return new XmlWriterWrap(output);
        }

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the specified <see cref="T:System.Xml.XmlWriter"/> and <see cref="T:System.Xml.XmlWriterSettings"/> objects.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.XmlWriter"/> object that is wrapped around the specified <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="output">The <see cref="T:System.Xml.XmlWriter"/> object that you want to use as the underlying writer.</param>
        /// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings"/> object used to configure the new <see cref="T:System.Xml.XmlWriter"/> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings"/> with default settings is used.If the <see cref="T:System.Xml.XmlWriter"/> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)"/> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings"/> property to obtain an <see cref="T:System.Xml.XmlWriterSettings"/> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter"/> object has the correct output settings.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="writer"/> value is null.</exception>
        public IXmlWriter Create(XmlWriter output,
                                 XmlWriterSettings settings)
        {
            return new XmlWriterWrap(output, settings);
        }

        #endregion
    }
}