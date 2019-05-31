using System;
using System.Collections.Generic;
using clawSoft.clawPDF.Utilities.Tokens;
using SystemWrapper;

namespace clawSoft.clawPDF.Shared.Helper
{
    public static class TokenHelper
    {
        private static TokenReplacer _tokenReplacer;

        public static TokenReplacer TokenReplacerWithPlaceHolders =>
            _tokenReplacer ?? (_tokenReplacer = CreateTokenReplacerWithPlaceHolders());

        private static TokenReplacer CreateTokenReplacerWithPlaceHolders()
        {
            var tr = new TokenReplacer();

            if (!TranslationHelper.Instance.IsInitialized)
                return tr;

            tr.AddToken(new StringToken("Author", Environment.UserName));
            tr.AddToken(new StringToken("PrintJobAuthor", Environment.UserName));
            tr.AddToken(new StringToken("ClientComputer", Environment.MachineName));
            tr.AddToken(new StringToken("ComputerName", Environment.MachineName));
            tr.AddToken(new NumberToken("Counter", 1234));
            tr.AddToken(new DateToken("DateTime", new DateTimeWrap().Now));
            tr.AddToken(new StringToken("InputFilename",
                TranslationHelper.Instance.TranslatorInstance.GetTranslation("TokenPlaceHolders", "MyFileDocx",
                    "MyFile.docx")));
            tr.AddToken(new StringToken("InputFilePath", @"C:\Temp"));
            tr.AddToken(new NumberToken("JobID", 1));
            tr.AddToken(new NumberToken("NumberOfCopies", 1));
            tr.AddToken(new NumberToken("NumberOfPages", 1));
            tr.AddToken(new ListToken("OutputFilenames",
                new[]
                {
                    TranslationHelper.Instance.TranslatorInstance.GetTranslation("TokenPlaceHolders", "OutputFilename",
                        "OutputFilename.jpg"),
                    TranslationHelper.Instance.TranslatorInstance.GetTranslation("TokenPlaceHolders", "OutputFilename2",
                        "OutputFilename2.jpg"),
                    TranslationHelper.Instance.TranslatorInstance.GetTranslation("TokenPlaceHolders", "OutputFilename3",
                        "OutputFilename3.jpg")
                }));
            tr.AddToken(new StringToken("OutputFilePath", @"C:\Temp"));
            tr.AddToken(new StringToken("PrinterName", "clawPDF"));
            tr.AddToken(new NumberToken("SessionID", 0));
            tr.AddToken(new StringToken("Title",
                TranslationHelper.Instance.TranslatorInstance.GetTranslation("TokenPlaceHolders", "TitleFromSettings",
                    "Title from Settings")));
            tr.AddToken(new StringToken("PrintJobName",
                TranslationHelper.Instance.TranslatorInstance.GetTranslation("TokenPlaceHolders", "TitleFromPrintJob",
                    "Title from Printjob")));
            tr.AddToken(new StringToken("Username", Environment.UserName));
            tr.AddToken(new EnvironmentToken());

            return tr;
        }

        public static List<string> GetTokenListWithFormatting()
        {
            var tokenList = new List<string>();
            tokenList.AddRange(TokenReplacerWithPlaceHolders.GetTokenNames());
            tokenList.Sort();
            tokenList.Insert(tokenList.IndexOf("<DateTime>") + 1, "<DateTime:yyyyMMddHHmmss>");
            tokenList.Insert(tokenList.IndexOf("<Environment>") + 1, "<Environment:UserName>");

            return tokenList;
        }

        public static List<string> GetTokenListForAuthor()
        {
            var tokenList = GetTokenListWithFormatting();

            tokenList.Remove("<Author>");
            tokenList.Remove("<Title>");
            tokenList.Remove("<OutputFilenames>");
            tokenList.Remove("<InputFilename>");
            tokenList.Remove("<InputFilePath>");
            tokenList.Remove("<OutputFilePath>");

            return tokenList;
        }

        public static List<string> GetTokenListForTitle()
        {
            var tokenList = GetTokenListWithFormatting();

            tokenList.Remove("<Title>");
            tokenList.Remove("<OutputFilenames>");
            tokenList.Remove("<InputFilePath>");
            tokenList.Remove("<OutputFilePath>");

            return tokenList;
        }

        public static List<string> GetTokenListForFilename()
        {
            var tokenList = GetTokenListWithFormatting();

            tokenList.Remove("<OutputFilenames>");
            tokenList.Remove("<InputFilePath>");
            tokenList.Remove("<OutputFilePath>");

            return tokenList;
        }

        public static List<string> GetTokenListForDirectory()

        {
            var tokenList = GetTokenListWithFormatting();
            tokenList.Remove("<OutputFilePath>");

            return tokenList;
        }

        public static List<string> GetTokenListForEmail()
        {
            var tokenList = GetTokenListWithFormatting();

            tokenList.Insert(tokenList.IndexOf("<OutputFilePath>") + 1, "<OutputFilenames:, >");
            tokenList.Insert(tokenList.IndexOf("<OutputFilePath>") + 2, "<OutputFilenames:\\r\\n>");
            tokenList.Remove("<OutputFilePath>");

            return tokenList;
        }

        /// <summary>
        ///     Detection if string contains insecure tokens, like NumberOfPages, InputFilename or InputFilePath
        /// </summary>
        public static bool ContainsInsecureTokens(string textWithTokens)
        {
            if (Contains_IgnoreCase(textWithTokens, "<NumberOfPages>"))
                return true;
            if (Contains_IgnoreCase(textWithTokens, "<InputFilename>"))
                return true;
            if (Contains_IgnoreCase(textWithTokens, "<InputFilePath>"))
                return true;

            return false;
        }

        private static bool Contains_IgnoreCase(string source, string value)
        {
            return source.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}