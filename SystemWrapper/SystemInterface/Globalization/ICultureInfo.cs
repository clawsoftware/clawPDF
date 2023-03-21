namespace SystemInterface.Globalization
{
    using System;
    using System.Globalization;
    using System.Runtime;
    using System.Runtime.InteropServices;
    using System.Security;

    /// <summary>
    ///     Gets the default calendar used by the culture.    
    /// </summary>
    public interface ICultureInfo
    {
        #region Public Properties

        /// <summary>
        ///     Gets the default calendar used by the culture.
        /// </summary>
        /// <returns>
        ///     A System.Globalization.Calendar that represents the default calendar used
        ///     by the culture.
        /// </returns>
        Calendar Calendar { get; }

        /// <summary>
        ///    Gets the System.Globalization.CompareInfo that defines how to compare strings
        ///     for the culture.
        /// </summary>
        /// <returns>
        ///     The System.Globalization.CompareInfo that defines how to compare strings
        ///     for the culture.        
        /// </returns>
        CompareInfo CompareInfo { get; }

        /// 
        ///  <Summary>
        ///      Gets the culture types that pertain to the current System.Globalization.CultureInfo
        ///      object.
        ///  </Summary>
        /// <Returns>
        ///      A bitwise combination of one or more System.Globalization.CultureTypes values.
        ///      There is no default value.
        /// </returns>
        [ComVisible(false)]
        CultureTypes CultureTypes { get; }

        /// <Summary>
        ///     Gets or sets a System.Globalization.DateTimeFormatInfo that defines the culturally
        ///     appropriate format of displaying dates and times.
        /// </Summary> 
        /// <Returns>
        ///     A System.Globalization.DateTimeFormatInfo that defines the culturally appropriate
        ///     format of displaying dates and times.i
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     The property is set to null.
        /// </exception>>
        /// <exception cref="InvalidOperationException">
        ///     The System.Globalization.CultureInfo.DateTimeFormat property or any of the
        ///     System.Globalization.DateTimeFormatInfo properties is set, and the System.Globalization.CultureInfo
        ///     is read-only.
        /// </exception>
        DateTimeFormatInfo DateTimeFormat { get; set; }

        /// <Summary>
        ///     Gets the culture name in the format "&lt;languagefull&gt; (&lt;country/regionfull&gt;)"
        ///     in the language of the localized version of .NET Framework.
        /// </Summary> 
        /// <Returns>
        ///     The culture name in the format "&lt;languagefull&gt; (&lt;country/regionfull&gt;)" in
        ///     the language of the localized version of .NET Framework, where &lt;languagefull&gt;
        ///     is the full name of the language and &lt;country/regionfull&gt; is the full name
        ///     of the country/region.
        /// </Returns>
        string DisplayName { get; }

        /// <Summary>
        ///     Gets the culture name in the format "&lt;languagefull&gt; (&lt;country/regionfull&gt;)"
        ///     in English.
        /// </Summary> 
        /// <Returns>
        ///     The culture name in the format "&lt;languagefull&gt; (&lt;country/regionfull&gt;)" in
        ///     English, where &lt;languagefull&gt; is the full name of the language and &lt;country/regionfull&gt;
        ///     is the full name of the country/region.
        /// </Returns>
        string EnglishName { get; }

        /// <Summary>
        ///     Deprecated. Gets the RFC 4646 standard identification for a language.
        /// </Summary> 
        /// <Returns>
        ///     A string that is the RFC 4646 standard identification for a language.
        /// </Returns>
        [ComVisible(false)]
        string IetfLanguageTag { get; }

        /// <Summary> 
        ///     Gets a value indicating whether the current System.Globalization.CultureInfo
        ///     represents a neutral culture.
        /// </Summary> 
        /// <Returns>
        ///     true if the current System.Globalization.CultureInfo represents a neutral
        ///     culture; otherwise, false.
        /// </Returns>
        bool IsNeutralCulture { get; }

        /// <Summary>
        ///     Gets a value indicating whether the current System.Globalization.CultureInfo
        ///     is read-only.
        /// </Summary> 
        /// <Returns>
        ///     true if the current System.Globalization.CultureInfo is read-only; otherwise,
        ///     false. The default is false.
        /// </Returns>
        bool IsReadOnly { get; }

        /// <Summary>
        ///     Gets the active input locale identifier.
        /// </Summary> 
        /// <Returns>
        ///     A 32-bit signed number that specifies an input locale identifier.
        /// </Returns> 
        [ComVisible(false)]
        int KeyboardLayoutId { get; }

        /// <Summary>
        ///     Gets the culture identifier for the current System.Globalization.CultureInfo.
        /// </Summary> 
        /// <Returns>
        ///     The culture identifier for the current System.Globalization.CultureInfo.
        /// </Returns>
// ReSharper disable once InconsistentNaming
        int LCID { get; }

        /// <Summary>
        ///     Gets the culture name in the format "languagecode2-country/regioncode2".
        /// </Summary> 
        /// <Returns>
        ///     The culture name in the format "languagecode2-country/regioncode2", where
        ///     languagecode2 is a lowercase two-letter code derived from ISO 639-1 and country/regioncode2
        ///     is an uppercase two-letter code derived from ISO 3166.
        /// </Returns>
        string Name { get; }

        /// <Summary>
        ///     Gets the System.Globalization.CultureInfo object that is culture-independent
        ///     (invariant).
        /// </Summary>
        /// <Returns>
        ///     The object that is culture-independent (invariant).
        /// </Returns>        
        CultureInfo InvariantCulture { get; }

        /// <Summary>
        ///     Gets the culture name, consisting of the language, the country/region, and
        ///     the optional script, that the culture is set to display.
        /// </Summary> 
        /// <Returns>
        ///     The culture name. consisting of the full name of the language, the full name
        ///     of the country/region, and the optional script. The format is discussed in
        ///     the description of the System.Globalization.CultureInfo class.
        /// </Returns>
        string NativeName { get; }

        /// <Summary>
        ///     Gets or sets a System.Globalization.NumberFormatInfo that defines the culturally
        ///     appropriate format of displaying numbers, currency, and percentage.
        ///
        /// </Summary> 
        /// <Returns>
        ///     A System.Globalization.NumberFormatInfo that defines the culturally appropriate
        ///     format of displaying numbers, currency, and percentage.
        /// </Returns>
        /// <exception cref="ArgumentNullException">
        ///     The property is set to null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The System.Globalization.CultureInfo.NumberFormat property or any of the
        ///     System.Globalization.NumberFormatInfo properties is set, and the System.Globalization.CultureInfo
        ///     is read-only.
        /// </exception>:
        NumberFormatInfo NumberFormat { get; set; }

        ///
        /// <Summary>
        ///     Gets the list of calendars that can be used by the culture.
        /// </Summary> 
        /// <Returns>
        ///     An array of type System.Globalization.Calendar that represents the calendars
        ///     that can be used by the culture represented by the current System.Globalization.CultureInfo.
        /// </Returns>
        Calendar[] OptionalCalendars { get; }

        /// <Summary>
        ///     Gets the System.Globalization.TextInfo that defines the writing system associated
        ///     with the culture.
        /// </Summary> 
        /// <Returns>
        ///     The System.Globalization.TextInfo that defines the writing system associated
        ///     with the culture.
        /// </Returns>
        TextInfo TextInfo { get; }

        /// <Summary>
        ///     Gets the ISO 639-2 three-letter code for the language of the current System.Globalization.CultureInfo.
        /// </Summary> 
        /// <Returns>
        ///     The ISO 639-2 three-letter code for the language of the current System.Globalization.CultureInfo.
        /// </Returns>
// ReSharper disable once InconsistentNaming
        string ThreeLetterISOLanguageName { get; }

        /// <Summary>
        ///     Gets the three-letter code for the language as defined in the Windows API.
        /// </Summary> 
        /// <Returns>
        ///     The three-letter code for the language as defined in the Windows API.
        /// </Returns>
        string ThreeLetterWindowsLanguageName { get; }

        /// <Summary>
        ///     Gets the ISO 639-1 two-letter code for the language of the current System.Globalization.CultureInfo.
        /// </Summary> 
        /// <Returns>
        ///     The ISO 639-1 two-letter code for the language of the current System.Globalization.CultureInfo.
        /// </Returns>
// ReSharper disable once InconsistentNaming
        string TwoLetterISOLanguageName { get; }

        /// <Summary>
        ///     Gets a value indicating whether the current System.Globalization.CultureInfo
        ///     uses the user-selected culture settings.
        /// </Summary> 
        /// <Returns>
        ///     true if the current System.Globalization.CultureInfo uses the user-selected
        ///     culture settings; otherwise, false.
        /// </Returns>
        bool UseUserOverride { get; }

        #endregion

        #region Public Methods and Operators

        /// <Summary>
        ///     Refreshes cached culture-related information.
        /// </Summary> 
        void ClearCachedData();

        /// <Summary>
        ///     Creates a copy of the current System.Globalization.CultureInfo.
        /// </Summary> 
        /// <Returns>
        ///     A copy of the current System.Globalization.CultureInfo.
        /// </Returns>
        [SecuritySafeCritical]
        object Clone();

        /// <Summary>
        ///     Creates a System.Globalization.CultureInfo that represents the specific culture
        ///     that is associated with the specified name.
        /// </Summary> 
        /// <Param name="name">
        ///     A predefined System.Globalization.CultureInfo name or the name of an existing
        ///     System.Globalization.CultureInfo object. name is not case-sensitive.
        /// </Param>        
        /// <Returns>
        ///     A System.Globalization.CultureInfo object that represents:The invariant culture,
        ///     if name is an empty string ("").-or- The specific culture associated with
        ///     name, if name is a neutral culture.-or- The culture specified by name, if
        ///     name is already a specific culture.
        /// </Returns>        
        CultureInfo CreateSpecificCulture(string name);

        /// <Summary>
        ///     Determines whether the specified object is the same culture as the current
        ///     System.Globalization.CultureInfo.
        /// </Summary>
        /// <Param name="value">        
        ///     The object to compare with the current System.Globalization.CultureInfo.
        /// </Param>        
        /// <Returns>
        ///     true if value is the same culture as the current System.Globalization.CultureInfo;
        ///     otherwise, false.
        /// </Returns> 
        bool Equals(object value);

        /// <Summary>
        ///     Gets an alternate user interface culture suitable for console applications
        ///     when the default graphic user interface culture is unsuitable.
        /// </Summary> 
        /// <Returns>
        ///     An alternate culture that is used to read and display text on the console.
        /// </Returns>
        [ComVisible(false)]
        [SecuritySafeCritical]
        CultureInfo GetConsoleFallbackUICulture();

        ///
        /// <Summary>
        ///     Retrieves a cached, read-only instance of a culture by using the specified
        ///     culture identifier.
        ///
        /// Parameters:
        ///   culture:
        ///     A locale identifier (LCID).
        ///
        /// </Summary> 
        /// <Returns>
        ///     A read-only instance of the specified culture.
        /// </Returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     culture is less than zero.
        /// </exception>
        /// <exception cref="CultureNotFoundException">
        ///     culture specifies a culture that is not supported.
        /// </exception>
        CultureInfo GetCultureInfo(int culture);

        /// <Summary>
        ///     Retrieves a cached, read-only instance of a culture by using the specified
        ///     culture name.
        /// </Summary> 
        /// Parameters:
        ///   name:
        ///     The name of a culture. name is not case-sensitive.
        /// <Returns>
        ///     A read-only instance of the specified culture.
        /// </Returns>
        /// <exception cref="ArgumentNullException">
        ///     name is null.
        /// </exception>
        /// <exception cref="CultureNotFoundException">
        ///     name specifies a culture that is not supported.
        /// </exception>
        CultureInfo GetCultureInfo(string name);

        /// <Summary>
        ///     Retrieves a cached, read-only instance of a culture. Parameters specify a
        ///     culture that is initialized with the System.Globalization.TextInfo and System.Globalization.CompareInfo
        ///     objects specified by another culture.
        /// </summary>
        /// <param name="name">
        ///     The name of a culture. name is not case-sensitive./// 
        /// </param>
        /// <param name="altName">
        ///     The name of a culture that supplies the System.Globalization.TextInfo and
        ///     System.Globalization.CompareInfo objects used to initialize name. altName
        ///     is not case-sensitive.
        /// </param>
        /// <Returns>
        ///     A read-only instance of the specified culture.
        /// </Returns>
        /// <exception cref="ArgumentNullException">
        ///     name or altName is null.
        /// </exception>
        /// <exception cref="CultureNotFoundException">
        ///     name or altName specifies a culture that is not supported.
        /// </exception>
        CultureInfo GetCultureInfo(string name,
                                   string altName);

        /// <Summary>
        ///     Deprecated. Retrieves a read-only System.Globalization.CultureInfo object
        ///     having linguistic characteristics that are identified by the specified RFC
        ///     4646 language tag.
        /// </Summary>
        /// <Param name="name">
        ///     The name of a language as specified by the RFC 4646 standard.
        /// </Param>
        /// <Returns>
        ///     A read-only System.Globalization.CultureInfo object.
        /// </Returns>
        /// <exception cref="ArgumentNullException">
        ///     name is null.
        /// </exception>
        /// <exception cref="CultureNotFoundException">
        ///     name does not correspond to a supported culture.
        /// </exception>
        CultureInfo GetCultureInfoByIetfLanguageTag(string name);

        ///
        /// <Summary>
        ///     Gets the list of supported cultures filtered by the specified System.Globalization.CultureTypes
        ///     parameter.
        ///
        /// Parameters:
        ///   types:
        ///     A bitwise combination of the enumeration values that filter the cultures
        ///     to retrieve.
        ///
        /// </Summary> 
        /// <Returns>
        ///     An array that contains the cultures specified by the types parameter. The
        ///     array of cultures is unsorted.
        /// </Returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     types specifies an invalid combination of System.Globalization.CultureTypes
        /// </exception>
        CultureInfo[] GetCultures(CultureTypes types);

        /// <Summary>
        ///     Gets an object that defines how to format the specified type.
        /// Parameters:
        ///   formatType:
        ///     The System.Type for which to get a formatting object. This method only supports
        ///     the System.Globalization.NumberFormatInfo and System.Globalization.DateTimeFormatInfo
        ///     types.
        /// </Summary> 
        /// <Returns>
        ///     The value of the System.Globalization.CultureInfo.NumberFormat property,
        ///     which is a System.Globalization.NumberFormatInfo containing the default number
        ///     format information for the current System.Globalization.CultureInfo, if formatType
        ///     is the System.Type object for the System.Globalization.NumberFormatInfo class.-or-
        ///     The value of the System.Globalization.CultureInfo.DateTimeFormat property,
        ///     which is a System.Globalization.DateTimeFormatInfo containing the default
        ///     date and time format information for the current System.Globalization.CultureInfo,
        ///     if formatType is the System.Type object for the System.Globalization.DateTimeFormatInfo
        ///     class.-or- null, if formatType is any other object.
        /// </Returns>
        [SecuritySafeCritical]
        object GetFormat(Type formatType);

        /// <Summary>
        ///     Serves as a hash function for the current System.Globalization.CultureInfo,
        ///     suitable for hashing algorithms and data structures, such as a hash table.
        /// </Summary> 
        /// <Returns>
        ///     A hash code for the current System.Globalization.CultureInfo.
        /// </Returns>
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        int GetHashCode();

        /// <Summary>
        ///     Returns a read-only wrapper around the specified System.Globalization.CultureInfo.
        /// Parameters:
        ///   ci:
        ///     The System.Globalization.CultureInfo to wrap.
        ///
        /// </Summary> 
        /// <Returns>
        ///     A read-only System.Globalization.CultureInfo wrapper around ci.
        /// </Returns>
        /// <exception cref="ArgumentNullException">
        ///     ci is null.
        /// </exception>
        [SecuritySafeCritical]
        CultureInfo ReadOnly(CultureInfo ci);

        /// <Summary>
        ///     Returns a string containing the name of the current System.Globalization.CultureInfo
        ///     in the format "languagecode2-country/regioncode2".
        /// </Summary> 
        /// <Returns>
        ///     A string containing the name of the current System.Globalization.CultureInfo.
        /// </Returns>
        string ToString();

        #endregion
    }
}