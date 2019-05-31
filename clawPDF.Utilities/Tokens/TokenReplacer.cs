using System;
using System.Collections.Generic;
using System.Text;
using SystemInterface;

namespace clawSoft.clawPDF.Utilities.Tokens
{
    /// <summary>
    ///     class with method to replace Tokens (see -> ReplaceTokens) previously added in its private TokenDict (see ->
    ///     AddToken)
    /// </summary>
    public class TokenReplacer
    {
        private readonly Dictionary<string, IToken> _tokenDict = new Dictionary<string, IToken>();

        #region Constants

        private const char TokenOpenChar = '<';
        private const char TokenCloseChar = '>';
        private const string TokenSplitString = ":";

        #endregion Constants

        #region Methods

        /// <summary>
        ///     Replace valid Token-names (previously added into the private TokenDict -> see AddToken) in the InputString by their
        ///     values.
        ///     Optional use of C#-format, declared behind a colon behind the Token-name.
        /// </summary>
        /// <param name="InputString">InputString which may contain Tokens, that will be replaced</param>
        /// <returns>
        ///     Returns the InputString with formated values of valid Tokens.
        /// </returns>
        /// <example>
        ///     <code lang="C#"><![CDATA[
        /// TokenReplacer tr = new TokenReplacer();
        /// tr.AddToken(new StringToken("Author", "NameOfAuthor"));
        /// string s = tr.ReplaceToken("<Author> becomes NameOfAuthor"));
        /// //output: s = "NameOfAuthor becomes NameOfAuthor"
        /// tr.AddToken(new NumberToken("Counter", 23));
        /// s = tr.ReplaceToken("NumberToken with Format <Counter:0000>");
        /// //output: s = "NumberToken with Format 0023"
        /// s = tr.ReplaceToken("<Non-added Token Name> <Counter>");
        /// //output: s = "<Non-added Token Name> 23"
        /// ]]>
        /// </code>
        /// </example>
        public string ReplaceTokens(string InputString)
        {
            int beginOfToken, endOfToken, lastIndexOfToken; //Memorize Indexes
            var begin = false; //Flag for the beginning of a Token

            beginOfToken = InputString.IndexOf(TokenOpenChar);
            lastIndexOfToken = InputString.LastIndexOf(TokenCloseChar);

            if (beginOfToken == -1 || lastIndexOfToken == -1 //No pair of <> included
                                   || beginOfToken > lastIndexOfToken) //No valid token pair possible
                return InputString;

            var outputString = new StringBuilder();

            if (beginOfToken > 0) //Add part ahead of the first TokenOpenChar
                outputString.Append(InputString.Substring(0, beginOfToken));

            begin = true; //Flag for an opened Token
            endOfToken = beginOfToken + 1; //First possible Position for a TokenCloseChar

            for (var i = beginOfToken + 1; i <= lastIndexOfToken; i++)
                if (InputString[i] == TokenOpenChar && !begin) //Regular opening of Token
                {
                    //check for text in between
                    if (i > endOfToken + 1)
                        outputString.Append(InputString.Substring(endOfToken + 1, i - endOfToken - 1));

                    beginOfToken = i;
                    begin = true;
                }
                else if (InputString[i] == TokenOpenChar) //(&& Begin) //second TokenOpenChar without previous closing
                {
                    outputString.Append(InputString.Substring(beginOfToken, i - beginOfToken));
                    //add substring from the last BeginOfToken to the new TokenOpenChar
                    beginOfToken = i;
                }
                else if (InputString[i] == TokenCloseChar && begin) //regular closing of an opened Token
                {
                    begin = false;
                    endOfToken = i;

                    var ExtractedTokenString = InputString.Substring(beginOfToken, i - beginOfToken + 1);
                    //Extract Token from Input String

                    if (ExtractedTokenString.Contains(TokenSplitString)
                        &&
                        _tokenDict.ContainsKey(
                            ExtractedTokenString.Substring(1, ExtractedTokenString.IndexOf(':') - 1).ToUpper()))
                    //Token contains a format and TokenName (without <>) is Key in the TokenDict
                    {
                        var token =
                            _tokenDict[
                                ExtractedTokenString.Substring(1, ExtractedTokenString.IndexOf(':') - 1).ToUpper()
                            ];

                        try
                        {
                            ExtractedTokenString =
                                token.GetValueWithFormat(
                                    ExtractedTokenString.Substring(ExtractedTokenString.IndexOf(':') + 1,
                                        ExtractedTokenString.Length - 1 - ExtractedTokenString.IndexOf(':') - 1));
                            //If the Format is valid, replace the string with the formated value of Token
                        }
                        catch
                        {
                            ExtractedTokenString = token.GetValue();
                            //If the format is not valid, replace the string with the non-formated value of Token
                        }

                        outputString.Append(ExtractedTokenString);
                    }
                    else if (
                            _tokenDict.ContainsKey(
                                InputString.Substring(beginOfToken + 1, i - beginOfToken - 1).ToUpper()))
                    //Tokenname (without <>) is Key in the TokenDict
                    {
                        outputString.Append(
                            _tokenDict[InputString.Substring(beginOfToken + 1, i - beginOfToken - 1).ToUpper()]
                                .GetValue());
                    }
                    else //Tokenname (without <>) is not Key in the TokenDict
                    {
                        outputString.Append(InputString.Substring(beginOfToken, i - beginOfToken + 1));
                        //add the non-valid Tokenname (with <>)
                    }
                }
                else if (InputString[i] == TokenCloseChar) //&& !Begin //closing of Token without opening
                {
                    outputString.Append(InputString.Substring(endOfToken + 1, i - endOfToken));
                    //add part from the last regular closing to the actual irregular closing
                    endOfToken = i;
                }

            if (lastIndexOfToken < InputString.Length) //Add part behind the last TokenCloseChar
                outputString.Append(InputString.Substring(lastIndexOfToken + 1,
                    InputString.Length - lastIndexOfToken - 1));

            return outputString.ToString();
        }

        public string[] GetTokenNames()
        {
            return GetTokenNames(true);
        }

        public string[] GetTokenNames(bool withDelimiters)
        {
            var tokens = new List<string>();

            foreach (var t in _tokenDict.Values)
                if (withDelimiters)
                    tokens.Add(TokenOpenChar + t.GetName() + TokenCloseChar);
                else
                    tokens.Add(t.GetName());

            return tokens.ToArray();
        }

        public IToken GetToken(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            try
            {
                return _tokenDict[name.ToUpper()];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        ///     Add new Token to the private TokenDict for replacing the token-name with its value (-> see ReplaceToken).
        /// </summary>
        /// <param name="NewToken">Token (implements IToken)</param>
        public void AddToken(IToken NewToken)
        {
            _tokenDict[NewToken.GetName().ToUpper()] = NewToken;
        }

        /// <summary>
        ///     Comfort function to add a string token
        /// </summary>
        /// <param name="name">name of the token (without brackets)</param>
        /// <param name="value">the value that will be inserted</param>
        public void AddStringToken(string name, string value)
        {
            AddToken(new StringToken(name, value));
        }

        /// <summary>
        ///     Comfort function to add a date token
        /// </summary>
        /// <param name="name">name of the token (without brackets)</param>
        /// <param name="value">the value that will be inserted</param>
        public void AddDateToken(string name, IDateTime value)
        {
            AddToken(new DateToken(name, value));
        }

        /// <summary>
        ///     Comfort function to add a number token
        /// </summary>
        /// <param name="name">name of the token (without brackets)</param>
        /// <param name="value">the value that will be inserted</param>
        public void AddNumberToken(string name, int value)
        {
            AddToken(new NumberToken(name, value));
        }

        /// <summary>
        ///     Comfort function to add a list token
        /// </summary>
        /// <param name="name">name of the token (without brackets)</param>
        /// <param name="value">the value that will be inserted</param>
        public void AddListToken(string name, IList<string> value)
        {
            AddToken(new ListToken(name, value));
        }

        #endregion Methods
    }
}