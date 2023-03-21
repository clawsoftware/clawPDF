using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using SystemInterface;

namespace SystemWrapper
{
    /// <summary>
    /// Wrapper for <see cref="System.DateTime"/> class.
    /// </summary>
    [Serializable]
    public class DateTimeWrap : IDateTime
    {
        #region Constructors and Initializers

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.DateTimeWrap"/> class.
        /// </summary>
        public DateTimeWrap()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.DateTimeWrap"/> class.
        /// </summary>
        public void Initialize()
        {
            DateTimeInstance = new DateTime();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.DateTimeWrap"/> class.
        /// </summary>
        /// <param name="dateTime">A DateTime object.</param>
        public DateTimeWrap(DateTime dateTime)
        {
            Initialize(dateTime);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.DateTimeWrap"/> class.
        /// </summary>
        /// <param name="dateTime">A DateTime object.</param>
        public void Initialize(DateTime dateTime)
        {
            DateTimeInstance = dateTime;
        }

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to a specified number of ticks.
        /// </summary>
        /// <param name="ticks">A date and time expressed in 100-nanosecond units. </param>
        public DateTimeWrap(long ticks)
        {
            Initialize(ticks);
        }

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to a specified number of ticks.
        /// </summary>
        /// <param name="ticks">A date and time expressed in 100-nanosecond units. </param>
        public void Initialize(long ticks)
        {
            DateTimeInstance = new DateTime(ticks);
        }

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to a specified number of ticks and to Coordinated Universal Time (UTC) or local time.
        /// </summary>
        /// <param name="ticks">A date and time expressed in 100-nanosecond units. </param>
        /// <param name="kind">One of the DateTimeKind values that indicates whether ticks specifies a local time, Coordinated Universal Time (UTC), or neither.</param>
        public DateTimeWrap(long ticks, DateTimeKind kind)
        {
            Initialize(ticks, kind);
        }

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to a specified number of ticks and to Coordinated Universal Time (UTC) or local time.
        /// </summary>
        /// <param name="ticks">A date and time expressed in 100-nanosecond units. </param>
        /// <param name="kind">One of the DateTimeKind values that indicates whether ticks specifies a local time, Coordinated Universal Time (UTC), or neither.</param>
        public void Initialize(long ticks, DateTimeKind kind)
        {
            DateTimeInstance = new DateTime(ticks, kind);
        }

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to the specified year, month, and day.
        /// </summary>
        /// <param name="year">The year (1 through 9999). </param>
        /// <param name="month">The month (1 through 12). </param>
        /// <param name="day">The day (1 through the number of days in month). </param>
        public DateTimeWrap(int year, int month, int day)
        {
            Initialize(year, month, day);
        }

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to the specified year, month, and day.
        /// </summary>
        /// <param name="year">The year (1 through 9999). </param>
        /// <param name="month">The month (1 through 12). </param>
        /// <param name="day">The day (1 through the number of days in month). </param>
        public void Initialize(int year, int month, int day)
        {
            DateTimeInstance = new DateTime(year, month, day);
        }

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to the specified year, month, and day for the specified calendar.
        /// </summary>
        /// <param name="year">The year (1 through 9999). </param>
        /// <param name="month">The month (1 through 12). </param>
        /// <param name="day">The day (1 through the number of days in month). </param>
        /// <param name="calendar">The Calendar that applies to this DateTimeWrap. </param>
        public DateTimeWrap(int year, int month, int day, Calendar calendar)
        {
            Initialize(year, month, day, calendar);
        }

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to the specified year, month, and day for the specified calendar.
        /// </summary>
        /// <param name="year">The year (1 through 9999). </param>
        /// <param name="month">The month (1 through 12). </param>
        /// <param name="day">The day (1 through the number of days in month). </param>
        /// <param name="calendar">The Calendar that applies to this DateTimeWrap. </param>
        public void Initialize(int year, int month, int day, Calendar calendar)
        {
            DateTimeInstance = new DateTime(year, month, day, calendar);
        }

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to the specified year, month, day, hour, minute, and second.
        /// </summary>
        /// <param name="year">The year (1 through 9999). </param>
        /// <param name="month">The month (1 through 12). </param>
        /// <param name="day">The day (1 through the number of days in month). </param>
        /// <param name="hour">The hours (0 through 23). </param>
        /// <param name="minute">The minutes (0 through 59). </param>
        /// <param name="second">The seconds (0 through 59). </param>
        public DateTimeWrap(int year, int month, int day, int hour, int minute, int second)
        {
            Initialize(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// Initializes a new instance of the DateTimeWrap class to the specified year, month, day, hour, minute, and second.
        /// </summary>
        /// <param name="year">The year (1 through 9999). </param>
        /// <param name="month">The month (1 through 12). </param>
        /// <param name="day">The day (1 through the number of days in month). </param>
        /// <param name="hour">The hours (0 through 23). </param>
        /// <param name="minute">The minutes (0 through 59). </param>
        /// <param name="second">The seconds (0 through 59). </param>
        public void Initialize(int year, int month, int day, int hour, int minute, int second)
        {
            DateTimeInstance = new DateTime(year, month, day, hour, minute, second);
        }

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
        public DateTimeWrap(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
        {
            Initialize(year, month, day, hour, minute, second, kind);
        }

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
        public void Initialize(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
        {
            DateTimeInstance = new DateTime(year, month, day, hour, minute, second, kind);
        }

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
        public DateTimeWrap(int year, int month, int day, int hour, int minute, int second, Calendar calendar)
        {
            Initialize(year, month, day, hour, minute, second, calendar);
        }

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
        public void Initialize(int year, int month, int day, int hour, int minute, int second, Calendar calendar)
        {
            DateTimeInstance = new DateTime(year, month, day, hour, minute, second, calendar);
        }

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
        public DateTimeWrap(int year, int month, int day, int hour, int minute, int second, int millisecond)
        {
            Initialize(year, month, day, hour, minute, second, millisecond);
        }

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
        public void Initialize(int year, int month, int day, int hour, int minute, int second, int millisecond)
        {
            DateTimeInstance = new DateTime(year, month, day, hour, minute, second, millisecond);
        }

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
        public DateTimeWrap(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind)
        {
            Initialize(year, month, day, hour, minute, second, millisecond, kind);
        }

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
        public void Initialize(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind)
        {
            DateTimeInstance = new DateTime(year, month, day, hour, minute, second, millisecond, kind);
        }

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
        public DateTimeWrap(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar)
        {
            Initialize(year, month, day, hour, minute, second, millisecond, calendar);
        }

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
        public void Initialize(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar)
        {
            DateTimeInstance = new DateTime(year, month, day, hour, minute, second, millisecond, calendar);
        }

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
        public DateTimeWrap(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, DateTimeKind kind)
        {
            Initialize(year, month, day, hour, minute, second, millisecond, calendar, kind);
        }

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
        public void Initialize(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, DateTimeKind kind)
        {
            DateTimeInstance = new DateTime(year, month, day, hour, minute, second, millisecond, calendar, kind);
        }

        #endregion Constructors and Initializers

        /// <inheritdoc />
        public IDateTime Date
        {
            get { return new DateTimeWrap(DateTimeInstance.Date); }
        }

        /// <inheritdoc />
        public DateTime DateTimeInstance { get; private set; }

        /// <inheritdoc />
        public int Day
        {
            get { return DateTimeInstance.Day; }
        }

        /// <inheritdoc />
        public DayOfWeek DayOfWeek
        {
            get { return DateTimeInstance.DayOfWeek; }
        }

        /// <inheritdoc />
        public int DayOfYear
        {
            get { return DateTimeInstance.DayOfYear; }
        }

        /// <inheritdoc />
        public int Hour
        {
            get { return DateTimeInstance.Hour; }
        }

        /// <inheritdoc />
        public DateTimeKind Kind
        {
            get { return DateTimeInstance.Kind; }
        }

        /// <inheritdoc />
        public int Millisecond
        {
            get { return DateTimeInstance.Millisecond; }
        }

        /// <inheritdoc />
        public int Minute
        {
            get { return DateTimeInstance.Minute; }
        }

        /// <inheritdoc />
        public int Month
        {
            get { return DateTimeInstance.Month; }
        }

        /// <inheritdoc />
        public IDateTime Now
        {
            get { return new DateTimeWrap(DateTime.Now); }
        }

        /// <inheritdoc />
        public int Second
        {
            get { return DateTimeInstance.Second; }
        }

        /// <inheritdoc />
        public long Ticks
        {
            get { return DateTimeInstance.Ticks; }
        }

        /// <inheritdoc />
        public TimeSpan TimeOfDay
        {
            get { return DateTimeInstance.TimeOfDay; }
        }

        /// <inheritdoc />
        public IDateTime Today
        {
            get { return new DateTimeWrap(DateTime.Today); }
        }

        /// <inheritdoc />
        public IDateTime UtcNow
        {
            get { return new DateTimeWrap(DateTime.UtcNow); }
        }

        /// <inheritdoc />
        public int Year
        {
            get { return DateTimeInstance.Year; }
        }

        /// <inheritdoc />
        public IDateTime Add(TimeSpan value)
        {
            return new DateTimeWrap(DateTimeInstance.Add(value));
        }

        /// <inheritdoc />
        public IDateTime AddDays(double value)
        {
            return new DateTimeWrap(DateTimeInstance.AddDays(value));
        }

        /// <inheritdoc />
        public IDateTime AddHours(double value)
        {
            return new DateTimeWrap(DateTimeInstance.AddHours(value));
        }

        /// <inheritdoc />
        public IDateTime AddMilliseconds(double value)
        {
            return new DateTimeWrap(DateTimeInstance.AddMilliseconds(value));
        }

        /// <inheritdoc />
        public IDateTime AddMinutes(double value)
        {
            return new DateTimeWrap(DateTimeInstance.AddMinutes(value));
        }

        /// <inheritdoc />
        public IDateTime AddMonths(int months)
        {
            return new DateTimeWrap(DateTimeInstance.AddMonths(months));
        }

        /// <inheritdoc />
        public IDateTime AddSeconds(double value)
        {
            return new DateTimeWrap(DateTimeInstance.AddSeconds(value));
        }

        /// <inheritdoc />
        public IDateTime AddTicks(long value)
        {
            return new DateTimeWrap(DateTimeInstance.AddTicks(value));
        }

        /// <inheritdoc />
        public IDateTime AddYears(int value)
        {
            return new DateTimeWrap(DateTimeInstance.AddYears(value));
        }

        /// <inheritdoc />
        public int Compare(IDateTime t1, IDateTime t2)
        {
            return DateTime.Compare(t1.DateTimeInstance, t2.DateTimeInstance);
        }

        /// <inheritdoc />
        public int CompareTo(IDateTime value)
        {
            return DateTimeInstance.CompareTo(value.DateTimeInstance);
        }

        /// <inheritdoc />
        public int CompareTo(object value)
        {
            return DateTimeInstance.CompareTo(value);
        }

        /// <inheritdoc />
        public int DaysInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        /// <inheritdoc />
        public bool Equals(IDateTime value)
        {
            return DateTimeInstance.Equals(value);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return DateTimeInstance.Equals(obj);
        }

        /// <inheritdoc />
        public bool Equals(IDateTime t1, IDateTime t2)
        {
            return DateTime.Equals(t1.DateTimeInstance, t2.DateTimeInstance);
        }

        /// <inheritdoc />
        public IDateTime FromBinary(long dateData)
        {
            return new DateTimeWrap(DateTime.FromBinary(dateData));
        }

        /// <inheritdoc />
        public IDateTime FromFileTime(long fileTime)
        {
            return new DateTimeWrap(DateTime.FromFileTime(fileTime));
        }

        /// <inheritdoc />
        public IDateTime FromFileTimeUtc(long fileTime)
        {
            return new DateTimeWrap(DateTime.FromFileTimeUtc(fileTime));
        }

        /// <inheritdoc />
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Valid method name from .NET API.")]
        public IDateTime FromOADate(double d)
        {
            return new DateTimeWrap(DateTime.FromOADate(d));
        }

        /// <inheritdoc />
        public string[] GetDateTimeFormats()
        {
            return DateTimeInstance.GetDateTimeFormats();
        }

        /// <inheritdoc />
        public string[] GetDateTimeFormats(char format)
        {
            return DateTimeInstance.GetDateTimeFormats(format);
        }

        /// <inheritdoc />
        public string[] GetDateTimeFormats(IFormatProvider provider)
        {
            return DateTimeInstance.GetDateTimeFormats(provider);
        }

        /// <inheritdoc />
        public string[] GetDateTimeFormats(char format, IFormatProvider provider)
        {
            return DateTimeInstance.GetDateTimeFormats(format, provider);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return DateTimeInstance.GetHashCode();
        }

        /// <inheritdoc />
        public TypeCode GetTypeCode()
        {
            return DateTimeInstance.GetTypeCode();
        }

        /// <inheritdoc />
        public bool IsDaylightSavingTime()
        {
            return DateTimeInstance.IsDaylightSavingTime();
        }

        /// <inheritdoc />
        public bool IsLeapYear(int year)
        {
            return DateTime.IsLeapYear(year);
        }

        /// <inheritdoc />
        public IDateTime Parse(string s)
        {
            return new DateTimeWrap(DateTime.Parse(s));
        }

        /// <inheritdoc />
        public IDateTime Parse(string s, IFormatProvider provider)
        {
            return new DateTimeWrap(DateTime.Parse(s, provider));
        }

        /// <inheritdoc />
        public IDateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles)
        {
            return new DateTimeWrap(DateTime.Parse(s, provider, styles));
        }

        /// <inheritdoc />
        public IDateTime ParseExact(string s, string format, IFormatProvider provider)
        {
            return new DateTimeWrap(DateTime.ParseExact(s, format, provider));
        }

        /// <inheritdoc />
        public IDateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style)
        {
            return new DateTimeWrap(DateTime.ParseExact(s, format, provider, style));
        }

        /// <inheritdoc />
        public IDateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style)
        {
            return new DateTimeWrap(DateTime.ParseExact(s, formats, provider, style));
        }

        /// <inheritdoc />
        public IDateTime SpecifyKind(IDateTime value, DateTimeKind kind)
        {
            return new DateTimeWrap(DateTime.SpecifyKind(value.DateTimeInstance, kind));
        }

        /// <inheritdoc />
        public TimeSpan Subtract(IDateTime value)
        {
            return DateTimeInstance.Subtract(value.DateTimeInstance);
        }

        /// <inheritdoc />
        public IDateTime Subtract(TimeSpan value)
        {
            return new DateTimeWrap(DateTimeInstance.Subtract(value));
        }

        /// <inheritdoc />
        public long ToBinary()
        {
            return DateTimeInstance.ToBinary();
        }

        /// <inheritdoc />
        public long ToFileTime()
        {
            return DateTimeInstance.ToFileTime();
        }

        /// <inheritdoc />
        public long ToFileTimeUtc()
        {
            return DateTimeInstance.ToFileTimeUtc();
        }

        /// <inheritdoc />
        public IDateTime ToLocalTime()
        {
            return new DateTimeWrap(DateTimeInstance.ToLocalTime());
        }

        /// <inheritdoc />
        public string ToLongDateString()
        {
            return DateTimeInstance.ToLongDateString();
        }

        /// <inheritdoc />
        public string ToLongTimeString()
        {
            return DateTimeInstance.ToLongTimeString();
        }

        /// <inheritdoc />
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Valid method name from .NET API.")]
        public double ToOADate()
        {
            return DateTimeInstance.ToOADate();
        }

        /// <inheritdoc />
        public string ToShortDateString()
        {
            return DateTimeInstance.ToShortDateString();
        }

        /// <inheritdoc />
        public string ToShortTimeString()
        {
            return DateTimeInstance.ToShortTimeString();
        }

        /// <summary>
        /// Converts the value of the current IDateTimeWrap object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the value of the current IDateTimeWrap object.</returns>
        public override string ToString()
        {
            return DateTimeInstance.ToString();
        }

        /// <inheritdoc />
        public string ToString(IFormatProvider provider)
        {
            return DateTimeInstance.ToString(provider);
        }

        /// <inheritdoc />
        public string ToString(string format)
        {
            return DateTimeInstance.ToString(format);
        }

        /// <inheritdoc />
        public string ToString(string format, IFormatProvider provider)
        {
            return DateTimeInstance.ToString(format, provider);
        }

        /// <inheritdoc />
        public IDateTime ToUniversalTime()
        {
            return new DateTimeWrap(DateTimeInstance.ToUniversalTime());
        }

        /// <inheritdoc />
        public bool TryParse(string s, out IDateTime result)
        {
            DateTime dtResult;
            bool returnValue = DateTime.TryParse(s, out dtResult);
            result = new DateTimeWrap(dtResult);
            return returnValue;
        }

        /// <inheritdoc />
        public bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out IDateTime result)
        {
            DateTime dtResult;
            bool returnValue = DateTime.TryParse(s, provider, styles, out dtResult);
            result = new DateTimeWrap(dtResult);
            return returnValue;
        }

        /// <inheritdoc />
        public bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out IDateTime result)
        {
            DateTime dtResult;
            bool returnValue = DateTime.TryParseExact(s, formats, provider, style, out dtResult);
            result = new DateTimeWrap(dtResult);
            return returnValue;
        }

        /// <inheritdoc />
        public bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out IDateTime result)
        {
            DateTime dtResult;
            bool returnValue = DateTime.TryParseExact(s, format, provider, style, out dtResult);
            result = new DateTimeWrap(dtResult);
            return returnValue;
        }
    }
}
