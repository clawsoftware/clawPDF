namespace SystemWrapper.Xml
{
    using System;
    using System.Xml;
    using System.Xml.Linq;

    using SystemInterface.Xml;

    /// <summary>
    ///     Wrapper of the <see cref="XComment"/> class.
    /// </summary>
    public class XCommentWrap : IXComment
    {
        #region Fields

        private readonly XComment instance;

        #endregion

        #region Constructors and Destructors

        /// <overloads>
        /// Initializes a new instance of the <see cref="XComment"/> class.
        /// </overloads>
        /// <summary>
        /// Initializes a new instance of the <see cref="XComment"/> class with the
        /// specified string content.
        /// </summary>
        /// <param name="value">
        /// The contents of the new XComment object.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the specified value is null.
        /// </exception>
        public XCommentWrap(string value)
        {
            this.instance = new XComment(value);
        }

        /// <summary>
        /// Initializes a new comment node from an existing comment node.
        /// </summary>
        /// <param name="other">Comment node to copy from.</param>
        public XCommentWrap(IXComment other)
        {
            this.instance = new XComment(other.Comment);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the original <see cref="XComment"/>.
        /// </summary>
        public XComment Comment
        {
            get
            {
                return this.instance;
            }
        }

        /// <summary>
        ///     Gets the node type for this node.
        /// </summary>
        /// <remarks>
        ///     This property will always return XmlNodeType.Comment.
        /// </remarks>
        public XmlNodeType NodeType
        {
            get
            {
                return this.instance.NodeType;
            }
        }

        /// <summary>
        ///     Gets or sets the string value of this comment.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the specified value is null.
        /// </exception>
        public string Value
        {
            get
            {
                return this.instance.Value;
            }
            set
            {
                this.instance.Value = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Write this <see cref="XComment"/> to the passed in <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="XmlWriter"/> to write this <see cref="XComment"/> to.
        /// </param>
        public void WriteTo(XmlWriter writer)
        {
            this.instance.WriteTo(writer);
        }

        #endregion
    }
}