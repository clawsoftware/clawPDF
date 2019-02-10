using System;
using SystemInterface;
using SystemWrapper;

namespace clawSoft.clawPDF.Utilities.Tokens
{
    public class EnvironmentToken : IToken
    {
        private readonly IEnvironment _environment;
        private readonly string _name;

        public EnvironmentToken() : this(new EnvironmentWrap())
        {
        }

        public EnvironmentToken(IEnvironment environment, string name = "Environment")
        {
            _environment = environment;
            _name = name;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="name">Token Name</param>
        public EnvironmentToken(string name) : this(new EnvironmentWrap(), name)
        {
        }

        /// <summary>
        ///     Returns value of Token
        /// </summary>
        /// <returns>Value of Token as string</returns>
        public string GetValue()
        {
            return "";
        }

        /// <summary>
        ///     Returns Value of Token in given C#-format
        /// </summary>
        /// <param name="formatString">C#-format String</param>
        /// <returns>Formated Value as string</returns>
        public string GetValueWithFormat(string formatString)
        {
            try
            {
                var s = _environment.GetEnvironmentVariable(formatString);
                if (s == null)
                    return "";
                return s;
            }
            catch (ArgumentNullException)
            {
                return "";
            }
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