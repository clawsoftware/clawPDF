using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;

namespace clawSoft.clawPDF.Core.Jobs
{
    /// <summary>
    ///     Replaces occurances within a string in a given order. This is used to remove unwanted parts from titles in the
    ///     JobInfos
    /// </summary>
    public class TitleReplacer
    {
        private readonly List<TitleReplacement> _replacements = new List<TitleReplacement>();

        /// <summary>
        ///     Replace the title string with the replacements
        /// </summary>
        /// <param name="originalTitle">The original title where replacements should be applied</param>
        /// <returns>The title with replacements</returns>
        public string Replace(string originalTitle)
        {
            if (originalTitle == null)
                throw new ArgumentException("originalTitle");

            var title = originalTitle;

            //replace longer strings first to avoid e.g. replacement of .doc before .docx
            _replacements.Sort(
                (a, b) => string.Compare(b.Search, a.Search, StringComparison.InvariantCultureIgnoreCase));

            foreach (var titleReplacement in _replacements) title = ReplaceTitle(titleReplacement, title);

            return title;
        }

        private string ReplaceTitle(TitleReplacement titleReplacement, string title)
        {
            if (string.IsNullOrEmpty(titleReplacement.Search))
                return title;

            switch (titleReplacement.ReplacementType)
            {
                case ReplacementType.RegEx:
                    if (!string.IsNullOrEmpty(titleReplacement.Replace))
                        title = Regex.Replace(title, titleReplacement.Search, titleReplacement.Replace);
                    break;

                case ReplacementType.Start:
                    if (title.StartsWith(titleReplacement.Search))
                    {
                        title = title.Substring(titleReplacement.Search.Length);
                        title = titleReplacement.Replace + title;
                    }

                    break;

                case ReplacementType.End:
                    if (title.EndsWith(titleReplacement.Search))
                    {
                        title = title.Substring(0,
                            title.LastIndexOf(titleReplacement.Search, StringComparison.InvariantCulture));
                        title += titleReplacement.Replace;
                    }

                    break;

                case ReplacementType.Replace:
                default:
                    title = title.Replace(titleReplacement.Search, titleReplacement.Replace);
                    break;
            }

            return title;
        }

        public void AddReplacement(TitleReplacement titleReplacement)
        {
            _replacements.Add(titleReplacement);
        }

        public void AddReplacements(IEnumerable<TitleReplacement> replacements)
        {
            foreach (var titleReplacement in replacements) AddReplacement(titleReplacement);
        }
    }
}