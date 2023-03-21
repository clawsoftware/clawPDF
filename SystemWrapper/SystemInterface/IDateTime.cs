using System;
using System.Globalization;

namespace SystemInterface
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Wrapper for <see cref="System.DateTime"/> class.
    /// </summary>
    public interface IDateTime
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.DateTimeWrap"/> class.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.DateTimeWrap"/> class.
        /// </summary>
        /// <param name="dateTime">A DateTime object.</param>
        void Initialize(DateTime dateTime);

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to a specified number of ticks.
        /// </summary>
        /// <param name="ticks">A date and time expressed in 100-nanosecond units. </param>
        void Initialize(long ticks);

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to a specified number of ticks and to Coordinated Universal Time (UTC) or local time.
        /// </summary>
        /// <param name="ticks">A date and time expressed in 100-nanosecond units. </param>
        /// <param name="kind">One of the DateTimeKind values that indicates whether ticks specifies a local time, Coordinated Universal Time (UTC), or neither.</param>
        void Initialize(long ticks, DateTimeKind kind);

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to the specified year, month, and day.
        /// </summary>
        /// <param name="year">The year (1 through 9999). </param>
        /// <param name="month">The month (1 through 12). </param>
        /// <param name="day">The day (1 through the number of days in month). </param>
        void Initialize(int year, int month, int day);

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to the specified year, month, and day for the specified calendar.
        /// </summary>
        /// <param name="year">The year (1 through 9999). </param>
        /// <param name="month">The month (1 through 12). </param>
        /// <param name="day">The day (1 through the number of days in month). </param>
        /// <param name="calendar">The Calendar that applies to this DateTimeWrap. </param>
        void Initialize(int year, int month, int day, Calendar calendar);

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to the specified year, month, day, hour, minute, and second.
        /// </summary>
        /// <param name="year">The year (1 through 9999). </param>
        /// <param name="month">The month (1 through 12). </param>
        /// <param name="day">The day (1 through the number of days in month). </param>
        /// <param name="hour">The hours (0 through 23). </param>
        /// <param name="minute">The minutes (0 through 59). </param>
        /// <param name="second">The seconds (0 through 59). </param>
        void Initialize(int year, int month, int day, int hour, int minute, int second);

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to the specified year, month, day, hour, minute, second, and Coordinated Universal Time (UTC) or local time.
        /// </summary>
        /// <param name="year">The year (1 through 9999). </param>
        /// <param name="month">The month (1 through 12). </param>
        /// <param name="day">The day (1 through the number of days in month). </param>
        /// <param name="hour">The hours (0 through 23). </param>
        /// <param name="minute">The minutes (0 through 59). </param>
        /// <param name="second">The seconds (0 through 59). </param>
        /// <param name="kind">One of the DateTimeKind values that indicates whether year, month, day, hour, minute and second specify a local time, Coordinated Universal Time (UTC), or neither.</param>
        void Initialize(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind);

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to the specified year, month, day, hour, minute, and second for the specified calendar.
        /// </summary>
        /// <param name="year">The year (1 through 9999). </param>
        /// <param name="month">The month (1 through 12). </param>
        /// <param name="day">The day (1 through the number of days in month). </param>
        /// <param name="hour">The hours (0 through 23). </param>
        /// <param name="minute">The minutes (0 through 59). </param>
        /// <param name="second">The seconds (0 through 59). </param>
        /// <param name="calendar">The Calendar that applies to this DateTimeWrap. </param>
        void Initialize(int year, int month, int day, int hour, int minute, int second, Calendar calendar);

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to the specified year, month, day, hour, minute, second, and millisecond.
        /// </summary>
        /// <param name="year">The year (1 through 9999). </param>
        /// <param name="month">The month (1 through 12). </param>
        /// <param name="day">The day (1 through the number of days in month). </param>
        /// <param name="hour">The hours (0 through 23). </param>
        /// <param name="minute">The minutes (0 through 59). </param>
        /// <param name="second">The seconds (0 through 59). </param>
        /// <param name="millisecond">The milliseconds (0 through 999). </param>
        void Initialize(int year, int month, int day, int hour, int minute, int second, int millisecond);

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to the specified year, month, day, hour, minute, second, millisecond, and Coordinated Universal Time (UTC) or local time.
        /// </summary>
        /// <param name="year">The year (1 through 9999). </param>
        /// <param name="month">The month (1 through 12). </param>
        /// <param name="day">The day (1 through the number of days in month). </param>
        /// <param name="hour">The hours (0 through 23). </param>
        /// <param name="minute">The minutes (0 through 59). </param>
        /// <param name="second">The seconds (0 through 59). </param>
        /// <param name="millisecond">The milliseconds (0 through 999). </param>
        /// <param name="kind">One of the DateTimeKind values that indicates whether year, month, day, hour, minute, second, and millisecond specify a local time, Coordinated Universal Time (UTC), or neither.</param>
        void Initialize(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind);

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to the specified year, month, day, hour, minute, second, millisecond for the specified calendar.
        /// </summary>
        /// <param name="year">The year (1 through 9999). </param>
        /// <param name="month">The month (1 through 12). </param>
        /// <param name="day">The day (1 through the number of days in month). </param>
        /// <param name="hour">The hours (0 through 23). </param>
        /// <param name="minute">The minutes (0 through 59). </param>
        /// <param name="second">The seconds (0 through 59). </param>
        /// <param name="millisecond">The milliseconds (0 through 999). </param>
        /// <param name="calendar">The Calendar that applies to this DateTimeWrap.</param>
        void Initialize(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar);

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to the specified year, month, day, hour, minute, second, millisecond, and Coordinated Universal Time (UTC) or local time for the specified calendar.
        /// </summary>
        /// <param name="year">The year (1 through 9999). </param>
        /// <param name="month">The month (1 through 12). </param>
        /// <param name="day">The day (1 through the number of days in month). </param>
        /// <param name="hour">The hours (0 through 23). </param>
        /// <param name="minute">The minutes (0 through 59). </param>
        /// <param name="second">The seconds (0 through 59). </param>
        /// <param name="millisecond">The milliseconds (0 through 999). </param>
        /// <param name="calendar">The Calendar that applies to this DateTimeWrap.</param>
        /// <param name="kind">One of the DateTimeKind values that indicates whether year, month, day, hour, minute, second, and millisecond specify a local time, Coordinated Universal Time (UTC), or neither.</param>
        void Initialize(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, DateTimeKind kind);

        // Properties

        /// <summary>
        /// Gets the date component of this instance.
        /// </summary>
        IDateTime Date { get; }

        /// <summary>
        /// Gets <see cref="T:System.DateTime"/> object.
        /// </summary>
        DateTime DateTimeInstance { get; }

        /// <summary>
        /// Gets the day of the month represented by this instance.
        /// </summary>
        int Day { get; }

        /// <summary>
        /// Gets the day of the week represented by this instance.
        /// </summary>
        DayOfWeek DayOfWeek { get; }

        /// <summary>
        /// Gets the day of the year represented by this instance.
        /// </summary>
        int DayOfYear { get; }

        /// <summary>
        /// Gets the hour component of the date represented by this instance.
        /// </summary>
        int Hour { get; }

        /// <summary>
        /// Specifies whether a DateTime object represents a local time, a Coordinated Universal Time (UTC), or is not specified as either local time or UTC.
        /// </summary>
        DateTimeKind Kind { get; }

        /// <summary>
        /// Gets the milliseconds component of the date represented by this instance.
        /// </summary>
        int Millisecond { get; }

        /// <summary>
        /// Gets the minute component of the date represented by this instance.
        /// </summary>
        int Minute { get; }

        /// <summary>
        /// Gets the month component of the date represented by this instance.
        /// </summary>
        int Month { get; }

        /// <summary>
        /// Gets a IDateTimeWrap object that is set to the current date and time on this computer, expressed as the local time.
        /// </summary>
        IDateTime Now { get; }

        /// <summary>
        /// Gets the seconds component of the date represented by this instance.
        /// </summary>
        int Second { get; }

        /// <summary>
        /// Gets the number of ticks that represent the date and time of this instance.
        /// </summary>
        long Ticks { get; }

        /// <summary>
        /// Gets the time of day for this instance.
        /// </summary>
        TimeSpan TimeOfDay { get; }

        /// <summary>
        /// Gets the current date.
        /// </summary>
        IDateTime Today { get; }

        /// <summary>
        /// Gets a DateTime object that is set to the current date and time on this computer, expressed as the Coordinated.
        /// </summary>
        IDateTime UtcNow { get; }

        /// <summary>
        /// Gets the year component of the date represented by this instance.
        /// </summary>
        int Year { get; }

        // Methods

        /// <summary>
        /// Adds the value of the specified TimeSpan to the value of this instance.
        /// </summary>
        /// <param name="value">A TimeSpan object that represents a positive or negative time interval. </param>
        /// <returns>A IDateTimeWrap whose value is the sum of the date and time represented by this instance and the time interval represented by value.</returns>
        IDateTime Add(TimeSpan value);

        /// <summary>
        /// Adds the specified number of days to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional days. The value parameter can be negative or positive. </param>
        /// <returns>A IDateTimeWrap whose value is the sum of the date and time represented by this instance and the number of days represented by value.</returns>
        IDateTime AddDays(double value);

        /// <summary>
        /// Adds the specified number of hours to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional hours. The value parameter can be negative or positive. </param>
        /// <returns>A IDateTimeWrap whose value is the sum of the date and time represented by this instance and the number of hours represented by value.</returns>
        IDateTime AddHours(double value);

        /// <summary>
        /// Adds the specified number of milliseconds to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional milliseconds. The value parameter can be negative or positive. This value is rounded to the nearest integer.</param>
        /// <returns>A IDateTimeWrap whose value is the sum of the date and time represented by this instance and the number of milliseconds represented by value.</returns>
        IDateTime AddMilliseconds(double value);

        /// <summary>
        /// Adds the specified number of minutes to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional minutes. The value parameter can be negative or positive.</param>
        /// <returns>A DateTime whose value is the sum of the date and time represented by this instance and the number of minutes represented by value.</returns>
        IDateTime AddMinutes(double value);

        /// <summary>
        /// Adds the specified number of months to the value of this instance.
        /// </summary>
        /// <param name="months">A number of months. The months parameter can be negative or positive. </param>
        /// <returns>A IDateTimeWrap whose value is the sum of the date and time represented by this instance and months.</returns>
        IDateTime AddMonths(int months);

        /// <summary>
        /// Adds the specified number of seconds to the value of this instance.
        /// </summary>
        /// <param name="value">A number of whole and fractional seconds. The value parameter can be negative or positive. </param>
        /// <returns>A IDateTimeWrap whose value is the sum of the date and time represented by this instance and the number of seconds represented by value.</returns>
        IDateTime AddSeconds(double value);

        /// <summary>
        /// Adds the specified number of ticks to the value of this instance.
        /// </summary>
        /// <param name="value">A number of 100-nanosecond ticks. The value parameter can be positive or negative.</param>
        /// <returns>A IDateTimeWrap whose value is the sum of the date and time represented by this instance and the time represented by value.</returns>
        IDateTime AddTicks(long value);

        /// <summary>
        /// Adds the specified number of years to the value of this instance.
        /// </summary>
        /// <param name="value">A number of years. The value parameter can be negative or positive.</param>
        /// <returns>A IDateTimeWrap whose value is the sum of the date and time represented by this instance and the number of years represented by value.</returns>
        IDateTime AddYears(int value);

        /// <summary>
        /// Compares two instances of IDateTimeWrap and returns an integer that indicates whether the first instance is earlier than, the same as, or later than the second instance.
        /// </summary>
        /// <param name="t1">The first IDateTimeWrap. </param>
        /// <param name="t2">The second IDateTimeWrap. </param>
        /// <returns>A signed number indicating the relative values of t1 and t2.</returns>
        int Compare(IDateTime t1, IDateTime t2);

        /// <summary>
        /// Compares the value of this instance to a specified IDateTimeWrap value and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified IDateTimeWrap value.
        /// </summary>
        /// <param name="value">A IDateTimeWrap object to compare. </param>
        /// <returns>A signed number indicating the relative values of this instance and the value parameter.</returns>
        int CompareTo(IDateTime value);

        /// <summary>
        /// Compares the value of this instance to a specified object that contains a specified IDateTimeWrap value, and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified IDateTimeWrap value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        int CompareTo(object value);

        /// <summary>
        /// Returns the number of days in the specified month and year.
        /// </summary>
        /// <param name="year">The year. </param>
        /// <param name="month">The month (a number ranging from 1 to 12). </param>
        /// <returns>The number of days in month for the specified year.</returns>
        int DaysInMonth(int year, int month);

        /// <summary>
        /// Returns a value indicating whether this instance is equal to the specified IDateTimeWrap instance.
        /// </summary>
        /// <param name="value">A IDateTimeWrap instance to compare to this instance. </param>
        /// <returns> <c>true</c> if the value parameter equals the value of this instance; otherwise, false.</returns>
        bool Equals(IDateTime value);

        /// <summary>
        /// Returns a value indicating whether two instances of IDateTimeWrap are equal.
        /// </summary>
        /// <param name="t1">The first IDateTimeWrap instance. </param>
        /// <param name="t2">The second IDateTimeWrap instance. </param>
        /// <returns> <c>true</c> if the two IDateTimeWrap values are equal; otherwise, false.</returns>
        bool Equals(IDateTime t1, IDateTime t2);

        /// <summary>
        /// Deserializes a 64-bit binary value and recreates an original serialized IDateTimeWrap object.
        /// </summary>
        /// <param name="dateData">A 64-bit signed integer that encodes the Kind property in a 2-bit field and the Ticks property in a 62-bit field. </param>
        /// <returns>A IDateTimeWrap object that is equivalent to the DateTime object that was serialized by the ToBinary method.</returns>
        IDateTime FromBinary(long dateData);

        /// <summary>
        /// Converts the specified Windows file time to an equivalent local time.
        /// </summary>
        /// <param name="fileTime">A Windows file time expressed in ticks. </param>
        /// <returns>A IDateTimeWrap object that represents a local time equivalent to the date and time represented by the fileTime parameter.</returns>
        IDateTime FromFileTime(long fileTime);

        /// <summary>
        /// Converts the specified Windows file time to an equivalent UTC time.
        /// </summary>
        /// <param name="fileTime">A Windows file time expressed in ticks. </param>
        /// <returns>A IDateTimeWrap object that represents a UTC time equivalent to the date and time represented by the fileTime parameter.</returns>
        IDateTime FromFileTimeUtc(long fileTime);

        /// <summary>
        /// Returns a IDateTimeWrap equivalent to the specified OLE Automation Date.
        /// </summary>
        /// <param name="d">An OLE Automation Date value. </param>
        /// <returns>A IDateTimeWrap that represents the same date and time as d.</returns>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Valid method name from .NET API.")]
        IDateTime FromOADate(double d);

        /// <summary>
        /// Converts the value of this instance to all the string representations supported by the standard DateTime format specifiers.
        /// </summary>
        /// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard IDateTimeWrap formatting specifiers.</returns>
        string[] GetDateTimeFormats();

        /// <summary>
        /// Converts the value of this instance to all the string representations supported by the standard DateTime format specifiers.
        /// </summary>
        /// <param name="format">A DateTime format string.</param>
        /// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard IDateTimeWrap formatting specifiers.</returns>
        string[] GetDateTimeFormats(char format);

        /// <summary>
        /// Converts the value of this instance to all the string representations supported by the standard IDateTimeWrap format specifiers and the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An IFormatProvider that supplies culture-specific formatting information about this instance.</param>
        /// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard IDateTimeWrap formatting specifiers.</returns>
        string[] GetDateTimeFormats(IFormatProvider provider);

        /// <summary>
        /// Converts the value of this instance to all the string representations supported by the specified standard IDateTimeWrap format specifier and culture-specific formatting information.
        /// </summary>
        /// <param name="format">A IDateTimeWrap format string. </param>
        /// <param name="provider">An IFormatProvider that supplies culture-specific formatting information about this instance. </param>
        /// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard IDateTimeWrap formatting specifiers.</returns>
        string[] GetDateTimeFormats(char format, IFormatProvider provider);

        /// <summary>
        /// Returns the TypeCode for value type DateTime.
        /// </summary>
        /// <returns>The enumerated constant.</returns>
        TypeCode GetTypeCode();

        /// <summary>
        /// Indicates whether this instance of IDateTimeWrap is within the Daylight Saving Time range for the current time zone.
        /// </summary>
        /// <returns> <c>true</c> if Kind is Local or Unspecified and the value of this instance of DateTime is within the Daylight Saving Time range for the current time zone. <c>false</c> if Kind is Utc.</returns>
        bool IsDaylightSavingTime();

        /// <summary>
        /// Returns an indication whether the specified year is a leap year.
        /// </summary>
        /// <param name="year">A 4-digit year. </param>
        /// <returns> <c>true</c> if year is a leap year; otherwise, <c>false</c>.</returns>
        bool IsLeapYear(int year);

        /// <summary>
        /// Converts the specified string representation of a date and time to its IDateTimeWrap equivalent.
        /// </summary>
        /// <param name="s">A string containing a date and time to convert. </param>
        /// <returns>A IDateTimeWrap equivalent to the date and time contained in s.</returns>
        IDateTime Parse(string s);

        /// <summary>
        /// Converts the specified string representation of a date and time to its IDateTimeWrap equivalent using the specified culture-specific format information.
        /// </summary>
        /// <param name="s">A string containing a date and time to convert. </param>
        /// <param name="provider">An object that supplies culture-specific format information about s. </param>
        /// <returns>A IDateTimeWrap equivalent to the date and time contained in s as specified by provider.</returns>
        IDateTime Parse(string s, IFormatProvider provider);

        /// <summary>
        /// Converts the specified string representation of a date and time to its IDateTimeWrap equivalent using the specified culture-specific format information and formatting style.
        /// </summary>
        /// <param name="s">A string containing a date and time to convert. </param>
        /// <param name="provider">An object that supplies culture-specific formatting information about s. </param>
        /// <param name="styles">A bitwise combination of the enumeration values that indicates the style elements that can be present in s for the parse operation to succeed and that defines how to interpret the parsed date in relation to the current time zone or the current date. A typical value to specify is None.</param>
        /// <returns>A IDateTimeWrap equivalent to the date and time contained in s as specified by provider and styles.</returns>
        IDateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles);

        /// <summary>
        /// Converts the specified string representation of a date and time to its IDateTimeWrap equivalent using the specified format and culture-specific format information. The format of the string representation must match the specified format exactly.
        /// </summary>
        /// <param name="s">A string that contains a date and time to convert. </param>
        /// <param name="format">A format specifier that defines the required format of s. </param>
        /// <param name="provider">An object that supplies culture-specific format information about s. </param>
        /// <returns>A IDateTimeWrap equivalent to the date and time contained in s as specified by format and provider.</returns>
        IDateTime ParseExact(string s, string format, IFormatProvider provider);

        /// <summary>
        /// Converts the specified string representation of a date and time to its DateTime equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly or an exception is thrown.
        /// </summary>
        /// <param name="s">A string that contains a date and time to convert. </param>
        /// <param name="format">A format specifier that defines the required format of s. </param>
        /// <param name="provider">An object that supplies culture-specific format information about s. </param>
        /// <param name="style">A bitwise combination of the enumeration values that provides additional information about s, about style elements that may be present in s, or about the conversion from s to a IDateTimeWrap value. A typical value to specify is None.</param>
        /// <returns>A IDateTimeWrap equivalent to the date and time contained in s as specified by format, provider, and style.</returns>
        IDateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style);

        /// <summary>
        /// Converts the specified string representation of a date and time to its IDateTimeWrap equivalent using the specified array of formats, culture-specific format information, and style. The format of the string representation must match at least one of the specified formats exactly or an exception is thrown.
        /// </summary>
        /// <param name="s">A string containing one or more dates and times to convert. </param>
        /// <param name="formats">An array of allowable formats of s. </param>
        /// <param name="provider">An IFormatProvider that supplies culture-specific format information about s. </param>
        /// <param name="style">A bitwise combination of DateTimeStyles values that indicates the permitted format of s. A typical value to specify is None.</param>
        /// <returns>A IDateTimeWrap equivalent to the date and time contained in s as specified by formats, provider, and style.</returns>
        IDateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style);

        /// <summary>
        /// Creates a new IDateTimeWrap object that represents the same time as the specified IDateTimeWrap, but is designated in either local time, Coordinated Universal Time (UTC), or neither, as indicated by the specified DateTimeKind value.
        /// </summary>
        /// <param name="value">A IDateTimeWrap object.</param>
        /// <param name="kind">One of the DateTimeKind values.</param>
        /// <returns>A new IDateTimeWrap object consisting of the same time represented by the value parameter and the DateTimeKind value specified by the kind parameter.</returns>
        IDateTime SpecifyKind(IDateTime value, DateTimeKind kind);

        /// <summary>
        /// Subtracts the specified date and time from this instance.
        /// </summary>
        /// <param name="value">An instance of IDateTimeWrap. </param>
        /// <returns>A TimeSpan interval equal to the date and time represented by this instance minus the date and time represented by value.</returns>
        TimeSpan Subtract(IDateTime value);

        /// <summary>
        /// Subtracts the specified duration from this instance.
        /// </summary>
        /// <param name="value">An instance of TimeSpan. </param>
        /// <returns>A IDateTimeWrap equal to the date and time represented by this instance minus the time interval represented by value.</returns>
        IDateTime Subtract(TimeSpan value);

        /// <summary>
        /// Serializes the current IDateTimeWrap object to a 64-bit binary value that subsequently can be used to recreate the IDateTimeWrap object.
        /// </summary>
        /// <returns>A 64-bit signed integer that encodes the Kind and Ticks properties. </returns>
        long ToBinary();

        /// <summary>
        /// Converts the value of the current IDateTimeWrap object to a Windows file time.
        /// </summary>
        /// <returns>The value of the current IDateTimeWrap object expressed as a Windows file time.</returns>
        long ToFileTime();

        /// <summary>
        /// Converts the value of the current IDateTimeWrap object to a Windows file time.
        /// </summary>
        /// <returns>The value of the current IDateTimeWrap object expressed as a Windows file time.</returns>
        long ToFileTimeUtc();

        /// <summary>
        /// Converts the value of the current IDateTimeWrap object to local time.
        /// </summary>
        /// <returns>A IDateTimeWrap object whose Kind property is Local, and whose value is the local time equivalent to the value of the current DateTime object, or MaxValue if the converted value is too large to be represented by a DateTime object, or MinValue if the converted value is too small to be represented as a DateTime object.</returns>
        IDateTime ToLocalTime();

        /// <summary>
        /// Converts the value of the current IDateTimeWrap object to its equivalent long date string representation.
        /// </summary>
        /// <returns>The value of the current IDateTimeWrap object is formatted using the pattern defined by the LongDatePattern property associated with the current thread culture. The return value is identical to the value returned by specifying the "D" standard DateTime format string with the ToString(String) method. </returns>
        string ToLongDateString();

        /// <summary>
        /// Converts the value of the current IDateTimeWrap object to its equivalent long time string representation.
        /// </summary>
        /// <returns>A string that contains the long time string representation of the current IDateTimeWrap object.</returns>
        string ToLongTimeString();

        /// <summary>
        /// Converts the value of this instance to the equivalent OLE Automation date.
        /// </summary>
        /// <returns>A double-precision floating-point number that contains an OLE Automation date equivalent to the value of this instance.</returns>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Valid method name from .NET API.")]
        double ToOADate();

        /// <summary>
        /// Converts the value of the current IDateTimeWrap object to its equivalent short date string representation.
        /// </summary>
        /// <returns>The value of the current IDateTimeWrap object is formatted using the pattern defined by the DateTimeFormatInfo..::.ShortDatePattern property associated with the current thread culture. The return value is identical to the value returned by specifying the "d" standard DateTime format string with the ToString(String) method.</returns>
        string ToShortDateString();

        /// <summary>
        /// Converts the value of the current IDateTimeWrap object to its equivalent short time string representation.
        /// </summary>
        /// <returns>The value of the current IDateTimeWrap object is formatted using the pattern defined by the DateTimeFormatInfo..::.ShortTimePattern property associated with the current thread culture. The return value is identical to the value returned by specifying the "t" standard DateTime format string with the ToString(String) method.</returns>
        string ToShortTimeString();

        /// <summary>
        /// Converts the value of the current IDateTimeWrap object to its equivalent string representation using the specified culture-specific format information.
        /// </summary>
        /// <param name="provider">An IFormatProvider that supplies culture-specific formatting information. </param>
        /// <returns>A string representation of value of the current IDateTimeWrap object as specified by provider.</returns>
        string ToString(IFormatProvider provider);

        /// <summary>
        /// Converts the value of the current IDateTimeWrap object to its equivalent string representation using the specified format.
        /// </summary>
        /// <param name="format">A DateTime format string. </param>
        /// <returns>A string representation of value of the current IDateTimeWrap object as specified by format.</returns>
        string ToString(string format);

        /// <summary>
        /// Converts the value of the current IDateTimeWrap object to its equivalent string representation using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A DateTime format string. </param>
        /// <param name="provider">An IFormatProvider that supplies culture-specific formatting information. </param>
        /// <returns>A string representation of value of the current IDateTimeWrap object as specified by format and provider.</returns>
        string ToString(string format, IFormatProvider provider);

        /// <summary>
        /// Converts the value of the current IDateTimeWrap object to Coordinated Universal Time (UTC).
        /// </summary>
        /// <returns>A IDateTimeWrap object whose Kind property is Utc, and whose value is the UTC equivalent to the value of the current DateTime object, or MaxValue if the converted value is too large to be represented by a DateTime object, or MinValue if the converted value is too small to be represented by a DateTime object.</returns>
        IDateTime ToUniversalTime();

        /// <summary>
        /// Converts the specified string representation of a date and time to its IDateTimeWrap equivalent and returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A string containing a date and time to convert.</param>
        /// <param name="result">When this method returns, contains the IDateTimeWrap value equivalent to the date and time contained in s, if the conversion succeeded, or MinValue if the conversion failed. The conversion fails if the s parameter is nullNothingnullptra null reference (Nothing in Visual Basic), is an empty string (""), or does not contain a valid string representation of a date and time. This parameter is passed uninitialized.</param>
        /// <returns> <c>true</c> if the s parameter was converted successfully; otherwise, <c>false</c>.</returns>
        bool TryParse(string s, out IDateTime result);

        /// <summary>
        /// Converts the specified string representation of a date and time to its IDateTimeWrap equivalent using the specified culture-specific format information and formatting style, and returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A string containing a date and time to convert. </param>
        /// <param name="provider">An object that supplies culture-specific formatting information about s.</param>
        /// <param name="styles">A bitwise combination of enumeration values that defines how to interpret the parsed date in relation to the current time zone or the current date. A typical value to specify is None.</param>
        /// <param name="result">When this method returns, contains the IDateTimeWrap value equivalent to the date and time contained in s, if the conversion succeeded, or MinValue if the conversion failed. The conversion fails if the s parameter is nullNothingnullptra null reference (Nothing in Visual Basic), is an empty string (""), or does not contain a valid string representation of a date and time. This parameter is passed uninitialized.</param>
        /// <returns> <c>true</c> if the s parameter was converted successfully; otherwise, <c>false</c>.</returns>
        bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out IDateTime result);

        /// <summary>
        /// Converts the specified string representation of a date and time to its IDateTimeWrap equivalent using the specified array of formats, culture-specific format information, and style. The format of the string representation must match at least one of the specified formats exactly. The method returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A string containing a date and time to convert. </param>
        /// <param name="formats">An array of allowable formats of s. </param>
        /// <param name="provider">An IFormatProvider object that supplies culture-specific formatting information about s. </param>
        /// <param name="style">A bitwise combination of one or more enumeration values that indicate the permitted format of s. </param>
        /// <param name="result">When this method returns, contains the IDateTimeWrap value equivalent to the date and time contained in s, if the conversion succeeded, or MinValue if the conversion failed. The conversion fails if s or formats is nullNothingnullptra null reference (Nothing in Visual Basic), s or an element of formats is an empty string, or the format of s is not exactly as specified by at least one of the format patterns in formats. This parameter is passed uninitialized.</param>
        /// <returns>true if the s parameter was converted successfully; otherwise, <c>false</c>.</returns>
        bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out IDateTime result);

        /// <summary>
        /// Converts the specified string representation of a date and time to its IDateTimeWrap equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly. The method returns a value that indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A string containing a date and time to convert. </param>
        /// <param name="format">The required format of s. </param>
        /// <param name="provider">An IFormatProvider object that supplies culture-specific formatting information about s. </param>
        /// <param name="style">A bitwise combination of one or more enumeration values that indicate the permitted format of s. </param>
        /// <param name="result">When this method returns, contains the DateTime value equivalent to the date and time contained in s, if the conversion succeeded, or MinValue if the conversion failed. The conversion fails if either the s or format parameter is nullNothingnullptra null reference (Nothing in Visual Basic), is an empty string, or does not contain a date and time that correspond to the pattern specified in format. This parameter is passed uninitialized. </param>
        /// <returns><c>true</c> if s was converted successfully; otherwise, <c>false</c>.</returns>
        bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out IDateTime result);
    }
}
