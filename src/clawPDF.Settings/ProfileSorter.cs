using System;
using System.Collections;

namespace clawSoft.clawPDF.Core.Settings
{
    public class ProfileSorter : IComparer
    {
        public int Compare(object a, object b)
        {
            var profileA = a as ConversionProfile;
            if (profileA == null)
                return 0;

            var profileB = b as ConversionProfile;
            if (profileB == null)
                return 0;

            if (profileA.IsDefault && !profileB.IsDefault)
                return -1;
            if (!profileA.IsDefault && profileB.IsDefault)
                return 1;

            return string.Compare(profileA.Name, profileB.Name, StringComparison.Ordinal);
        }
    }
}