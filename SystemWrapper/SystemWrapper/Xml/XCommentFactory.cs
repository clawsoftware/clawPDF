namespace SystemWrapper.Xml
{
    using System;
    using System.Xml.Linq;

    using SystemInterface.Xml;

    /// <summary>
    ///   The factory responsible for the creation of an instance of <see cref="XComment"/>.
    /// </summary>
    public class XCommentFactory : IXCommentFactory
    {
        #region Public Methods and Operators

        /// <overloads>
        ///     Initializes a new instance of the <see cref="IXComment"/> class.
        /// </overloads>
        /// <summary>
        ///     Initializes a new instance of the <see cref="IXComment"/> class with the specified string content.
        /// </summary>
        /// <param name="value">
        ///     The contents of the new XComment object.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the specified value is null.
        /// </exception>
        public IXComment Create(string value)
        {
            return new XCommentWrap(value);
        }

        /// <summary>
        ///     Initializes a new comment node from an existing comment node.
        /// </summary>
        /// <param name="other">Comment node to copy from.</param>
        public IXComment Create(XComment other)
        {
            return new XCommentWrap((IXComment)other);
        }

        #endregion
    }
}