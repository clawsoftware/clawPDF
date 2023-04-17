using System.Globalization;
using SystemInterface;

namespace clawSoft.clawPDF.Utilities.Tokens
{
    /// <summary>
    ///     Class for Token with Name of type string and Value of type DateTime.
    /// </summary>
    public class DateToken : IToken
    {
        private readonly string _name;
        private readonly IDateTime _value;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="name">Token Name</param>
        /// <param name="value">Token Value</param>
        public DateToken(string name, IDateTime value)
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
            return _value.ToString(CultureInfo.CurrentUICulture);
        }

        /// <summary>
        ///     Returns value of Token in given C#-format
        /// </summary>
        /// <param name="formatString">C#-format String</param>
        /// <returns>Formated Value as string</returns>
        public string GetValueWithFormat(string formatString)
        {
            return string.Format("{0:" + formatString + "}", _value.DateTimeInstance);
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