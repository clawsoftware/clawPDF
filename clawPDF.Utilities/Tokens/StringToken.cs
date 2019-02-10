namespace clawSoft.clawPDF.Utilities.Tokens
{
    /// <summary>
    ///     Class for Token with Name and Value, each of Type string.
    /// </summary>
    public class StringToken : IToken
    {
        private readonly string _name;
        private readonly string _value;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="name">Token Name</param>
        /// <param name="value">Token Value</param>
        public StringToken(string name, string value)
        {
            _name = name;
            _value = value;
        }

        /// <summary>
        ///     Returns value of Token
        /// </summary>
        /// <returns>Value of Token as string</returns>
        public string GetValue()
        {
            return _value;
        }

        /// <summary>
        ///     Returns Value of Token in given C#-format
        /// </summary>
        /// <param name="formatString">C#-format String</param>
        /// <returns>Formated Value as string</returns>
        public string GetValueWithFormat(string formatString)
        {
            return GetValue();
        }

        /// <summary>
        ///     Returns Name of Token
        /// </summary>
        /// <returns>Name of Token as String</returns>
        public string GetName()
        {
            return _name;
        }
    }
}