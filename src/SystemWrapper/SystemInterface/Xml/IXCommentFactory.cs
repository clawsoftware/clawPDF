namespace SystemInterface.Xml
{
    using System;
    using System.Xml.Linq;

    /// <summary>
    ///   Defines the contract for the factory responsible for the creation of an instance of <see cref="XComment"/>.
    /// </summary>
    public interface IXCommentFactory
    {
        #region Public Methods and Operators

        /// <overloads>
        ///     Initializes a new instance of the <see cref="XComment"/> class.
        /// </overloads>
        /// <summary>
        ///     Initializes a new instance of the <see cref="XComment"/> class with the specified string content.
        /// </summary>
        /// <param name="value">
        ///     The contents of the new XComment object.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the specified value is null.
        /// </exception>
        IXComment Create(string value);

        /// <summary>
        ///     Initializes a new comment node from an existing comment node.
        /// </summary>
        /// <param name="other">Comment node to copy from.</param>
        IXComment Create(XComment other);

        #endregion
    }
}