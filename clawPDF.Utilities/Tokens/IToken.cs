namespace clawSoft.clawPDF.Utilities.Tokens
{
    /// <summary>
    ///     Interface description for methods of Token-classes
    /// </summary>
    public interface IToken
    {
        /// <summary>
        ///     Returns value of Token
        /// </summary>
        /// <returns>Value of Token as string</returns>
        string GetValue();

        /// <summary>
        ///     Returns value of Token in given C#-format
        /// </summary>
        /// <param name="formatString">C#-format String</param>
        /// <returns>Formated Value as string</returns>
        string GetValueWithFormat(string formatString);

        /// <summary>
        ///     Returns Name of Token
        /// </summary>
        /// <returns>Name of Token as String</returns>
        string GetName();
    }
}