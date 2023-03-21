
#pragma warning disable 1584,1711,1572,1581,1580

namespace SystemWrapper.Xml
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.XPath;

    using SystemInterface.Xml;

    /// <summary>
    ///     Wrapper of the <see cref="XmlWriter"/> class.
    /// </summary>
    public class XmlWriterWrap : IXmlWriter
    {
        #region Fields

        private readonly XmlWriter instance;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the specified filename.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="outputFileName">The file to which you want to write. The <see cref="T:System.Xml.XmlWriter"/> creates a file at the specified path and writes to it in XML 1.0 text syntax. The <paramref name="outputFileName"/> must be a file system path.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="url"/> value is null.</exception>
        public XmlWriterWrap(string outputFileName)
        {
            this.instance = XmlWriter.Create(outputFileName);
        }

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the filename and <see cref="T:System.Xml.XmlWriterSettings"/> object.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="outputFileName">The file to which you want to write. The <see cref="T:System.Xml.XmlWriter"/> creates a file at the specified path and writes to it in XML 1.0 text syntax. The <paramref name="outputFileName"/> must be a file system path.</param>
        /// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings"/> object used to configure the new <see cref="T:System.Xml.XmlWriter"/> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings"/> with default settings is used.If the <see cref="T:System.Xml.XmlWriter"/> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)"/> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings"/> property to obtain an <see cref="T:System.Xml.XmlWriterSettings"/> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter"/> object has the correct output settings.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="url"/> value is null.</exception>
        public XmlWriterWrap(string outputFileName,
                             XmlWriterSettings settings)
        {
            this.instance = XmlWriter.Create(outputFileName, settings);
        }

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the specified stream.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="output">The stream to which you want to write. The <see cref="T:System.Xml.XmlWriter"/> writes XML 1.0 text syntax and appends it to the specified stream.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="stream"/> value is null.</exception>
        public XmlWriterWrap(Stream output)
        {
            this.instance = XmlWriter.Create(output);
        }

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the stream and <see cref="T:System.Xml.XmlWriterSettings"/> object.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="output">The stream to which you want to write. The <see cref="T:System.Xml.XmlWriter"/> writes XML 1.0 text syntax and appends it to the specified stream.</param>
        /// <param name="settings">The <see cref="T:System.Xml.XmlWriterSettings"/> object used to configure the new <see cref="T:System.Xml.XmlWriter"/> instance. If this is null, a <see cref="T:System.Xml.XmlWriterSettings"/> with default settings is used.If the <see cref="T:System.Xml.XmlWriter"/> is being used with the <see cref="M:System.Xml.Xsl.XslCompiledTransform.Transform(System.String,System.Xml.XmlWriter)"/> method, you should use the <see cref="P:System.Xml.Xsl.XslCompiledTransform.OutputSettings"/> property to obtain an <see cref="T:System.Xml.XmlWriterSettings"/> object with the correct settings. This ensures that the created <see cref="T:System.Xml.XmlWriter"/> object has the correct output settings.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="stream"/> value is null.</exception>
        public XmlWriterWrap(Stream output,
                             XmlWriterSettings settings)
        {
            this.instance = XmlWriter.Create(output, settings);
        }

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the specified <see cref="T:System.IO.TextWriter"/>.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="output">The <see cref="T:System.IO.TextWriter"/> to which you want to write. The <see cref="T:System.Xml.XmlWriter"/> writes XML 1.0 text syntax and appends it to the specified <see cref="T:System.IO.TextWriter"/>.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="text"/> value is null.</exception>
        public XmlWriterWrap(TextWriter output)
        {
            this.instance = XmlWriter.Create(output);
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
        public XmlWriterWrap(TextWriter output,
                             XmlWriterSettings settings)
        {
            this.instance = XmlWriter.Create(output, settings);
        }

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the specified <see cref="T:System.Text.StringBuilder"/>.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="output">The <see cref="T:System.Text.StringBuilder"/> to which to write to. Content written by the <see cref="T:System.Xml.XmlWriter"/> is appended to the <see cref="T:System.Text.StringBuilder"/>.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="builder"/> value is null.</exception>
        public XmlWriterWrap(StringBuilder output)
        {
            this.instance = XmlWriter.Create(output);
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
        public XmlWriterWrap(StringBuilder output,
                             XmlWriterSettings settings)
        {
            this.instance = XmlWriter.Create(output, settings);
        }

        /// <summary>
        ///     Creates a new <see cref="T:System.Xml.XmlWriter"/> instance using the specified <see cref="T:System.Xml.XmlWriter"/> object.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Xml.XmlWriter"/> object that is wrapped around the specified <see cref="T:System.Xml.XmlWriter"/> object.
        /// </returns>
        /// <param name="output">The <see cref="T:System.Xml.XmlWriter"/> object that you want to use as the underlying writer.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="writer"/> value is null.</exception>
        public XmlWriterWrap(XmlWriter output)
        {
            this.instance = XmlWriter.Create(output);
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
        public XmlWriterWrap(XmlWriter output,
                             XmlWriterSettings settings)
        {
            this.instance = XmlWriter.Create(output, settings);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the <see cref="T:System.Xml.XmlWriterSettings"/> object used to create this <see cref="T:System.Xml.XmlWriter"/> instance.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Xml.XmlWriterSettings"/> object used to create this writer instance. If this writer was not created using the <see cref="Overload:System.Xml.XmlWriter.Create"/> method, this property returns null.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public XmlWriterSettings Settings
        {
            get
            {
                return this.instance.Settings;
            }
        }

        /// <summary>
        ///     When overridden in a derived class, gets the state of the writer.
        /// </summary>
        /// <returns>
        ///  One of the <see cref="T:System.Xml.WriteState"/> values.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public WriteState WriteState
        {
            get
            {
                return this.instance.WriteState;
            }
        }

        /// <summary>
        ///     Gets the original <see cref="XmlWriter"/>.
        /// </summary>
        public XmlWriter Writer
        {
            get
            {
                return this.instance;
            }
        }

        /// <summary>
        ///     When overridden in a derived class, gets the current xml:lang scope.
        /// </summary>
        /// <returns>
        ///     The current xml:lang scope.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public string XmlLang
        {
            get
            {
                return this.instance.XmlLang;
            }
        }

        /// <summary>
        ///     When overridden in a derived class, gets an <see cref="T:System.Xml.XmlSpace"/> representing the current xml:space scope.
        /// </summary>
        /// <returns>
        ///     An XmlSpace representing the current xml:space scope.Value Meaning NoneThis is the default if no xml:space scope exists.DefaultThe current scope is xml:space="default".PreserveThe current scope is xml:space="preserve".
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public XmlSpace XmlSpace
        {
            get
            {
                return this.instance.XmlSpace;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     When overridden in a derived class, closes this stream and the underlying stream.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">A call is made to write more output after Close has been called or the result of this call is an invalid XML document.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void Close()
        {
            this.instance.Close();
        }

        /// <summary>
        ///     Releases all resources used by the current instance of the <see cref="T:System.Xml.XmlWriter"/> class.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates whether or not unmanaged resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            this.instance.Dispose();
        }

        /// <summary>
        /// When overridden in a derived class, flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void Flush()
        {
            this.instance.Flush();
        }

        /// <summary>
        ///     When overridden in a derived class, returns the closest prefix defined in the current namespace scope for the namespace URI.
        /// </summary>
        /// <returns>
        ///     The matching prefix or null if no matching namespace URI is found in the current scope.
        /// </returns>
        /// <param name="ns">The namespace URI whose prefix you want to find.</param><exception cref="T:System.ArgumentException"><paramref name="ns"/> is either null or String.Empty.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public string LookupPrefix(string ns)
        {
            return this.instance.LookupPrefix(ns);
        }

        /// <summary>
        ///     When overridden in a derived class, writes an attribute with the specified local name, namespace URI, and value.
        /// </summary>
        /// <param name="localName">The local name of the attribute.</param><param name="ns">The namespace URI to associate with the attribute.</param><param name="value">The value of the attribute.</param><exception cref="T:System.InvalidOperationException">The state of writer is not WriteState.Element or writer is closed. </exception><exception cref="T:System.ArgumentException">The xml:space or xml:lang attribute value is invalid. </exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteAttributeString(string localName,
                                         string ns,
                                         string value)
        {
            this.instance.WriteAttributeString(localName, ns, value);
        }

        /// <summary>
        ///     When overridden in a derived class, writes out the attribute with the specified local name and value.
        /// </summary>
        /// <param name="localName">The local name of the attribute.</param><param name="value">The value of the attribute.</param><exception cref="T:System.InvalidOperationException">The state of writer is not WriteState.Element or writer is closed. </exception><exception cref="T:System.ArgumentException">The xml:space or xml:lang attribute value is invalid. </exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteAttributeString(string localName,
                                         string value)
        {
            this.instance.WriteAttributeString(localName, value);
        }

        /// <summary>
        ///     When overridden in a derived class, writes out the attribute with the specified prefix, local name, namespace URI, and value.
        /// </summary>
        /// <param name="prefix">The namespace prefix of the attribute.</param><param name="localName">The local name of the attribute.</param><param name="ns">The namespace URI of the attribute.</param><param name="value">The value of the attribute.</param><exception cref="T:System.InvalidOperationException">The state of writer is not WriteState.Element or writer is closed. </exception><exception cref="T:System.ArgumentException">The xml:space or xml:lang attribute value is invalid. </exception><exception cref="T:System.Xml.XmlException">The <paramref name="localName"/> or <paramref name="ns"/> is null. </exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteAttributeString(string prefix,
                                         string localName,
                                         string ns,
                                         string value)
        {
            this.instance.WriteAttributeString(prefix, localName, ns, value);
        }

        /// <summary>
        ///     When overridden in a derived class, writes out all the attributes found at the current position in the <see cref="T:System.Xml.XmlReader"/>.
        /// </summary>
        /// <param name="reader">The XmlReader from which to copy the attributes.</param><param name="defattr">true to copy the default attributes from the XmlReader; otherwise, false.</param><exception cref="T:System.ArgumentNullException"><paramref name="reader"/> is null. </exception><exception cref="T:System.Xml.XmlException">The reader is not positioned on an element, attribute or XmlDeclaration node. </exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteAttributes(XmlReader reader,
                                    bool defattr)
        {
            this.instance.WriteAttributes(reader, defattr);
        }

        /// <summary>
        ///     When overridden in a derived class, encodes the specified binary bytes as Base64 and writes out the resulting text.
        /// </summary>
        /// <param name="buffer">Byte array to encode.</param><param name="index">The position in the buffer indicating the start of the bytes to write.</param><param name="count">The number of bytes to write.</param><exception cref="T:System.ArgumentNullException"><paramref name="buffer"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> or <paramref name="count"/> is less than zero. -or-The buffer length minus <paramref name="index"/> is less than <paramref name="count"/>.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteBase64(byte[] buffer,
                                int index,
                                int count)
        {
            this.instance.WriteBase64(buffer, index, count);
        }

        /// <summary>
        ///     When overridden in a derived class, encodes the specified binary bytes as BinHex and writes out the resulting text.
        /// </summary>
        /// <param name="buffer">Byte array to encode.</param><param name="index">The position in the buffer indicating the start of the bytes to write.</param><param name="count">The number of bytes to write.</param><exception cref="T:System.ArgumentNullException"><paramref name="buffer"/> is null.</exception><exception cref="T:System.InvalidOperationException">The writer is closed or in error state.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> or <paramref name="count"/> is less than zero. -or-The buffer length minus <paramref name="index"/> is less than <paramref name="count"/>.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteBinHex(byte[] buffer,
                                int index,
                                int count)
        {
            this.instance.WriteBinHex(buffer, index, count);
        }

        /// <summary>
        ///     When overridden in a derived class, writes out a &lt;![CDATA[...]]&gt; block containing the specified text.
        /// </summary>
        /// <param name="text">The text to place inside the CDATA block.</param><exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteCData(string text)
        {
            this.instance.WriteCData(text);
        }

        /// <summary>
        ///     When overridden in a derived class, forces the generation of a character entity for the specified Unicode character value.
        /// </summary>
        /// <param name="ch">The Unicode character for which to generate a character entity.</param><exception cref="T:System.ArgumentException">The character is in the surrogate pair character range, 0xd800 - 0xdfff.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteCharEntity(char ch)
        {
            this.instance.WriteCharEntity(ch);
        }

        /// <summary>
        ///     When overridden in a derived class, writes text one buffer at a time.
        /// </summary>
        /// <param name="buffer">Character array containing the text to write.</param><param name="index">The position in the buffer indicating the start of the text to write.</param><param name="count">The number of characters to write.</param><exception cref="T:System.ArgumentNullException"><paramref name="buffer"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> or <paramref name="count"/> is less than zero.-or-The buffer length minus <paramref name="index"/> is less than <paramref name="count"/>; the call results in surrogate pair characters being split or an invalid surrogate pair being written.</exception><exception cref="T:System.ArgumentException">The <paramref name="buffer"/> parameter value is not valid.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteChars(char[] buffer,
                               int index,
                               int count)
        {
            this.instance.WriteChars(buffer, index, count);
        }

        /// <summary>
        ///     When overridden in a derived class, writes out a comment &lt;!--...--&gt; containing the specified text.
        /// </summary>
        /// <param name="text">Text to place inside the comment.</param><exception cref="T:System.ArgumentException">The text would result in a non-well-formed XML document.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteComment(string text)
        {
            this.instance.WriteComment(text);
        }

        /// <summary>
        ///     When overridden in a derived class, writes the DOCTYPE declaration with the specified name and optional attributes.
        /// </summary>
        /// <param name="name">The name of the DOCTYPE. This must be non-empty.</param><param name="pubid">If non-null it also writes PUBLIC "pubid" "sysid" where <paramref name="pubid"/> and <paramref name="sysid"/> are replaced with the value of the given arguments.</param><param name="sysid">If <paramref name="pubid"/> is null and <paramref name="sysid"/> is non-null it writes SYSTEM "sysid" where <paramref name="sysid"/> is replaced with the value of this argument.</param><param name="subset">If non-null it writes [subset] where subset is replaced with the value of this argument.</param><exception cref="T:System.InvalidOperationException">This method was called outside the prolog (after the root element). </exception><exception cref="T:System.ArgumentException">The value for <paramref name="name"/> would result in invalid XML.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteDocType(string name,
                                 string pubid,
                                 string sysid,
                                 string subset)
        {
            this.instance.WriteDocType(name, pubid, sysid, subset);
        }

        /// <summary>
        ///     Writes an element with the specified local name and value.
        /// </summary>
        /// <param name="localName">The local name of the element.</param><param name="value">The value of the element.</param><exception cref="T:System.ArgumentException">The <paramref name="localName"/> value is null or an empty string.-or-The parameter values are not valid.</exception><exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteElementString(string localName,
                                       string value)
        {
            this.instance.WriteElementString(localName, value);
        }

        /// <summary>
        ///     Writes an element with the specified local name, namespace URI, and value.
        /// </summary>
        /// <param name="localName">The local name of the element.</param><param name="ns">The namespace URI to associate with the element.</param><param name="value">The value of the element.</param><exception cref="T:System.ArgumentException">The <paramref name="localName"/> value is null or an empty string.-or-The parameter values are not valid.</exception><exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteElementString(string localName,
                                       string ns,
                                       string value)
        {
            this.instance.WriteElementString(localName, ns, value);
        }

        /// <summary>
        ///     Writes an element with the specified prefix, local name, namespace URI, and value.
        /// </summary>
        /// <param name="prefix">The prefix of the element.</param><param name="localName">The local name of the element.</param><param name="ns">The namespace URI of the element.</param><param name="value">The value of the element.</param><exception cref="T:System.ArgumentException">The <paramref name="localName"/> value is null or an empty string.-or-The parameter values are not valid.</exception><exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteElementString(string prefix,
                                       string localName,
                                       string ns,
                                       string value)
        {
            this.instance.WriteElementString(prefix, localName, ns, value);
        }

        /// <summary>
        ///     When overridden in a derived class, closes the previous <see cref="M:System.Xml.XmlWriter.WriteStartAttribute(System.String,System.String)"/> call.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteEndAttribute()
        {
            this.instance.WriteEndAttribute();
        }

        /// <summary>
        ///     When overridden in a derived class, closes any open elements or attributes and puts the writer back in the Start state.
        /// </summary>
        /// <exception cref="T:System.ArgumentException">The XML document is invalid.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteEndDocument()
        {
            this.instance.WriteEndDocument();
        }

        /// <summary>
        ///     When overridden in a derived class, closes one element and pops the corresponding namespace scope.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">This results in an invalid XML document.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteEndElement()
        {
            this.instance.WriteEndElement();
        }

        /// <summary>
        ///     When overridden in a derived class, writes out an entity reference as &amp;name;.
        /// </summary>
        /// <param name="name">The name of the entity reference.</param><exception cref="T:System.ArgumentException"><paramref name="name"/> is either null or String.Empty.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteEntityRef(string name)
        {
            this.instance.WriteEntityRef(name);
        }

        /// <summary>
        ///     When overridden in a derived class, closes one element and pops the corresponding namespace scope.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteFullEndElement()
        {
            this.instance.WriteFullEndElement();
        }

        /// <summary>
        ///     When overridden in a derived class, writes out the specified name, ensuring it is a valid name according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).
        /// </summary>
        /// <param name="name">The name to write.</param><exception cref="T:System.ArgumentException"><paramref name="name"/> is not a valid XML name; or <paramref name="name"/> is either null or String.Empty.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteName(string name)
        {
            this.instance.WriteName(name);
        }

        /// <summary>
        ///     When overridden in a derived class, writes out the specified name, ensuring it is a valid NmToken according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).
        /// </summary>
        /// <param name="name">The name to write.</param><exception cref="T:System.ArgumentException"><paramref name="name"/> is not a valid NmToken; or <paramref name="name"/> is either null or String.Empty.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteNmToken(string name)
        {
            this.instance.WriteNmToken(name);
        }

        /// <summary>
        ///     When overridden in a derived class, copies everything from the reader to the writer and moves the reader to the start of the next sibling.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> to read from.</param><param name="defattr">true to copy the default attributes from the XmlReader; otherwise, false.</param><exception cref="T:System.ArgumentNullException"><paramref name="reader"/> is null.</exception><exception cref="T:System.ArgumentException"><paramref name="reader"/> contains invalid characters.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteNode(XmlReader reader,
                              bool defattr)
        {
            this.instance.WriteNode(reader, defattr);
        }

        /// <summary>
        ///     Copies everything from the <see cref="T:System.Xml.XPath.XPathNavigator"/> object to the writer. The position of the <see cref="T:System.Xml.XPath.XPathNavigator"/> remains unchanged.
        /// </summary>
        /// <param name="navigator">The <see cref="T:System.Xml.XPath.XPathNavigator"/> to copy from.</param><param name="defattr">true to copy the default attributes; otherwise, false.</param><exception cref="T:System.ArgumentNullException"><paramref name="navigator"/> is null.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteNode(XPathNavigator navigator,
                              bool defattr)
        {
            this.instance.WriteNode(navigator, defattr);
        }

        /// <summary>
        ///     When overridden in a derived class, writes out a processing instruction with a space between the name and text as follows: &lt;?name text?&gt;.
        /// </summary>
        /// <param name="name">The name of the processing instruction.</param><param name="text">The text to include in the processing instruction.</param><exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document.<paramref name="name"/> is either null or String.Empty.This method is being used to create an XML declaration after <see cref="M:System.Xml.XmlWriter.WriteStartDocument"/> has already been called. </exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteProcessingInstruction(string name,
                                               string text)
        {
            this.instance.WriteProcessingInstruction(name, text);
        }

        /// <summary>
        ///     When overridden in a derived class, writes out the namespace-qualified name. This method looks up the prefix that is in scope for the given namespace.
        /// </summary>
        /// <param name="localName">The local name to write.</param><param name="ns">The namespace URI for the name.</param><exception cref="T:System.ArgumentException"><paramref name="localName"/> is either null or String.Empty.<paramref name="localName"/> is not a valid name. </exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteQualifiedName(string localName,
                                       string ns)
        {
            this.instance.WriteQualifiedName(localName, ns);
        }

        /// <summary>
        ///     When overridden in a derived class, writes raw markup manually from a character buffer.
        /// </summary>
        /// <param name="buffer">Character array containing the text to write.</param><param name="index">The position within the buffer indicating the start of the text to write.</param><param name="count">The number of characters to write.</param><exception cref="T:System.ArgumentNullException"><paramref name="buffer"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> or <paramref name="count"/> is less than zero. -or-The buffer length minus <paramref name="index"/> is less than <paramref name="count"/>.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteRaw(char[] buffer,
                             int index,
                             int count)
        {
            this.instance.WriteRaw(buffer, index, count);
        }

        /// <summary>
        ///     When overridden in a derived class, writes raw markup manually from a string.
        /// </summary>
        /// <param name="data">String containing the text to write.</param><exception cref="T:System.ArgumentException"><paramref name="data"/> is either null or String.Empty.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteRaw(string data)
        {
            this.instance.WriteRaw(data);
        }

        /// <summary>
        ///     Writes the start of an attribute with the specified local name and namespace URI.
        /// </summary>
        /// <param name="localName">The local name of the attribute.</param><param name="ns">The namespace URI of the attribute.</param><exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteStartAttribute(string localName,
                                        string ns)
        {
            this.instance.WriteStartAttribute(localName, ns);
        }

        /// <summary>
        ///     When overridden in a derived class, writes the start of an attribute with the specified prefix, local name, and namespace URI.
        /// </summary>
        /// <param name="prefix">The namespace prefix of the attribute.</param><param name="localName">The local name of the attribute.</param><param name="ns">The namespace URI for the attribute.</param><exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections. </exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteStartAttribute(string prefix,
                                        string localName,
                                        string ns)
        {
            this.instance.WriteStartAttribute(prefix, localName, ns);
        }

        /// <summary>
        ///     Writes the start of an attribute with the specified local name.
        /// </summary>
        /// <param name="localName">The local name of the attribute.</param><exception cref="T:System.InvalidOperationException">The writer is closed.</exception><exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteStartAttribute(string localName)
        {
            this.instance.WriteStartAttribute(localName);
        }

        /// <summary>
        ///     When overridden in a derived class, writes the XML declaration with the version "1.0".
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteStartDocument()
        {
            this.instance.WriteStartDocument();
        }

        /// <summary>
        ///     When overridden in a derived class, writes the XML declaration with the version "1.0" and the standalone attribute.
        /// </summary>
        /// <param name="standalone">If true, it writes "standalone=yes"; if false, it writes "standalone=no".</param><exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor. </exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteStartDocument(bool standalone)
        {
            this.instance.WriteStartDocument(standalone);
        }

        /// <summary>
        ///     When overridden in a derived class, writes the specified start tag and associates it with the given namespace.
        /// </summary>
        /// <param name="localName">The local name of the element.</param><param name="ns">The namespace URI to associate with the element. If this namespace is already in scope and has an associated prefix, the writer automatically writes that prefix also.</param><exception cref="T:System.InvalidOperationException">The writer is closed.</exception><exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteStartElement(string localName,
                                      string ns)
        {
            this.instance.WriteStartElement(localName, ns);
        }

        /// <summary>
        ///     When overridden in a derived class, writes the specified start tag and associates it with the given namespace and prefix.
        /// </summary>
        /// <param name="prefix">The namespace prefix of the element.</param><param name="localName">The local name of the element.</param><param name="ns">The namespace URI to associate with the element.</param><exception cref="T:System.InvalidOperationException">The writer is closed.</exception><exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteStartElement(string prefix,
                                      string localName,
                                      string ns)
        {
            this.instance.WriteStartElement(prefix, localName, ns);
        }

        /// <summary>
        ///     When overridden in a derived class, writes out a start tag with the specified local name.
        /// </summary>
        /// <param name="localName">The local name of the element.</param><exception cref="T:System.InvalidOperationException">The writer is closed.</exception><exception cref="T:System.Text.EncoderFallbackException">There is a character in the buffer that is a valid XML character but is not valid for the output encoding. For example, if the output encoding is ASCII, you should only use characters from the range of 0 to 127 for element and attribute names. The invalid character might be in the argument of this method or in an argument of previous methods that were writing to the buffer. Such characters are escaped by character entity references when possible (for example, in text nodes or attribute values). However, the character entity reference is not allowed in element and attribute names, comments, processing instructions, or CDATA sections. </exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteStartElement(string localName)
        {
            this.instance.WriteStartElement(localName);
        }

        /// <summary>
        ///     When overridden in a derived class, writes the given text content.
        /// </summary>
        /// <param name="text">The text to write.</param><exception cref="T:System.ArgumentException">The text string contains an invalid surrogate pair.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteString(string text)
        {
            this.instance.WriteString(text);
        }

        /// <summary>
        ///     When overridden in a derived class, generates and writes the surrogate character entity for the surrogate character pair.
        /// </summary>
        /// <param name="lowChar">The low surrogate. This must be a value between 0xDC00 and 0xDFFF.</param><param name="highChar">The high surrogate. This must be a value between 0xD800 and 0xDBFF.</param><exception cref="T:System.ArgumentException">An invalid surrogate character pair was passed.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteSurrogateCharEntity(char lowChar,
                                             char highChar)
        {
            this.instance.WriteSurrogateCharEntity(lowChar, highChar);
        }

        /// <summary>
        ///     Writes the object value.
        /// </summary>
        /// <param name="value">The object value to write.Note   With the release of the .NET Framework 3.5, this method accepts <see cref="T:System.DateTimeOffset"/> as a parameter.</param><exception cref="T:System.ArgumentException">An invalid value was specified.</exception><exception cref="T:System.ArgumentNullException">The <paramref name="value"/> is null.</exception><exception cref="T:System.InvalidOperationException">The writer is closed or in error state.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteValue(object value)
        {
            this.instance.WriteValue(value);
        }

        /// <summary>
        ///     Writes a <see cref="T:System.String"/> value.
        /// </summary>
        /// <param name="value">The <see cref="T:System.String"/> value to write.</param><exception cref="T:System.ArgumentException">An invalid value was specified.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteValue(string value)
        {
            this.instance.WriteValue(value);
        }

        /// <summary>
        ///     Writes a <see cref="T:System.Boolean"/> value.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Boolean"/> value to write.</param><exception cref="T:System.ArgumentException">An invalid value was specified.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteValue(bool value)
        {
            this.instance.WriteValue(value);
        }

        /// <summary>
        ///     Writes a <see cref="T:System.DateTime"/> value.
        /// </summary>
        /// <param name="value">The <see cref="T:System.DateTime"/> value to write.</param><exception cref="T:System.ArgumentException">An invalid value was specified.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteValue(DateTime value)
        {
            this.instance.WriteValue(value);
        }

        /// <summary>
        ///     Writes a <see cref="T:System.DateTimeOffset"/> value.
        /// </summary>
        /// <param name="value">The <see cref="T:System.DateTimeOffset"/> value to write.</param><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteValue(DateTimeOffset value)
        {
            this.instance.WriteValue(value);
        }

        /// <summary>
        ///     Writes a <see cref="T:System.Double"/> value.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Double"/> value to write.</param><exception cref="T:System.ArgumentException">An invalid value was specified.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteValue(double value)
        {
            this.instance.WriteValue(value);
        }

        /// <summary>
        ///     Writes a single-precision floating-point number.
        /// </summary>
        /// <param name="value">The single-precision floating-point number to write.</param><exception cref="T:System.ArgumentException">An invalid value was specified.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteValue(float value)
        {
            this.instance.WriteValue(value);
        }

        /// <summary>
        ///     Writes a <see cref="T:System.Decimal"/> value.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Decimal"/> value to write.</param><exception cref="T:System.ArgumentException">An invalid value was specified.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteValue(decimal value)
        {
            this.instance.WriteValue(value);
        }

        /// <summary>
        ///     Writes a <see cref="T:System.Int32"/> value.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Int32"/> value to write.</param><exception cref="T:System.ArgumentException">An invalid value was specified.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteValue(int value)
        {
            this.instance.WriteValue(value);
        }

        /// <summary>
        ///     Writes a <see cref="T:System.Int64"/> value.
        /// </summary>
        /// <param name="value">The <see cref="T:System.Int64"/> value to write.</param><exception cref="T:System.ArgumentException">An invalid value was specified.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteValue(long value)
        {
            this.instance.WriteValue(value);
        }

        /// <summary>
        ///     When overridden in a derived class, writes out the given white space.
        /// </summary>
        /// <param name="ws">The string of white space characters.</param><exception cref="T:System.ArgumentException">The string contains non-white space characters.</exception><exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlWriter"/> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException"/> is thrown with the message “An asynchronous operation is already in progress.”</exception>
        public void WriteWhitespace(string ws)
        {
            this.instance.WriteWhitespace(ws);
        }

        #endregion
    }
}