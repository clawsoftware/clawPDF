using System;

namespace clawSoft.clawPDF.Shared.Helper
{
    public static class EnumToStringValueHelper
    {
        public static string GetStringValue(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes =
                (StringValueAttribute[])fi.GetCustomAttributes(typeof(StringValueAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Value;
            return value.ToString();
        }
    }

    public class StringValueAttribute : Attribute
    {
        public StringValueAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}