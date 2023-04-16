using System.Collections.Generic;

namespace clawSoft.clawPDF.Utilities.Tokens
{
    /// <summary>
    ///     Class for Token with Name of type string and Value of type DateTime.
    /// </summary>
    public class ListToken : IToken
    {
        private readonly string _name;
        private readonly IList<string> _value;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="name">Token Name</param>
        /// <param name="value">Token Value</param>
        public ListToken(string name, IList<string> value)
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
            return GetValueWithFormat(", ");
        }

        /// <summary>
        ///     Returns value of Token in given C#-format
        /// </summary>
        /// <param name="formatString">^The format to apply</param>
        /// <returns>Formated Value as string</returns>
        public string GetValueWithFormat(string formatString)
        {
            var s = new string[_value.Count];
            _value.CopyTo(s, 0);
            return string.Join(formatString.Replace("\\n", "\n").Replace("\\r", "\r"), s);
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