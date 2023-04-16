namespace SystemInterface.Xml
{
    using System;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    ///   Defines the contract for the wrapper of the <see cref="XComment"/> class.
    /// </summary>
    public interface IXComment
    {
        #region Public Properties

        /// <summary>
        ///     Gets the original <see cref="XComment"/>.
        /// </summary>
        XComment Comment { get; }

        /// <summary>
        ///     Gets the node type for this node.
        /// </summary>
        /// <remarks>
        ///     This property will always return XmlNodeType.Comment.
        /// </remarks>
        XmlNodeType NodeType { get; }

        /// <summary>
        ///     Gets or sets the string value of this comment.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the specified value is null.
        /// </exception>
        string Value { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Write this <see cref="XComment"/> to the passed in <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="XmlWriter"/> to write this <see cref="XComment"/> to.
        /// </param>
        void WriteTo(XmlWriter writer);

        #endregion
    }
}