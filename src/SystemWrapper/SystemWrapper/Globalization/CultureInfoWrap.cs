namespace SystemWrapper.Globalization
{
    using System;
    using System.Globalization;
    using System.Runtime;
    using System.Security;

    using SystemInterface.Globalization;

    /// <summary>
    ///     Provides information about a specific culture (called a locale for unmanaged
    ///     code development). The information includes the names for the culture, the
    ///     writing system, the calendar used, and formatting for dates and sort strings.
    /// </summary>
    public class CultureInfoWrap : ICultureInfo
    {
        #region Constructors and Destructors

        /// <Summary>
        ///     Initializes a new instance of the System.Globalization.CultureInfo class
        ///     based on the culture specified by the culture identifier.
        /// </Summary>
        /// <Param name="culture">
        ///     A predefined System.Globalization.CultureInfo identifier, System.Globalization.CultureInfo.LCID
        ///     property of an existing System.Globalization.CultureInfo object, or Windows-only
        ///     culture identifier.
        /// </Param>
        /// <Exception cref="ArgumentOutOfRangeException">
        ///     culture is less than zero.
        /// </Exception>
        /// <Exception cref="CultureNotFoundException">
        ///     culture is not a valid culture identifier.
        /// </Exception> 
        public CultureInfoWrap(int culture)
        {
            this.CultureInfoInstance = new CultureInfo(culture);
        }

        /// <Summary>
        ///     Initializes a new instance of the System.Globalization.CultureInfo class
        ///     based on the culture specified by name.
        /// </Summary>
        /// <Param name="name">
        ///     A predefined System.Globalization.CultureInfo name, System.Globalization.CultureInfo.Name
        ///     of an existing System.Globalization.CultureInfo, or Windows-only culture
        ///     name. name is not case-sensitive.
        /// </Param>
        /// <Exception cref="ArgumentNullException">
        ///     name is null.                
        /// </Exception>
        /// <Exception cref="CultureNotFoundException">
        ///     name is not a valid culture name.
        /// </Exception>        
        [SecuritySafeCritical]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public CultureInfoWrap(string name)
        {
            this.CultureInfoInstance = new CultureInfo(name);
        }

        /// <Summary>
        ///     Initializes a new instance of the System.Globalization.CultureInfo class
        ///     based on the culture specified by the culture identifier and on the Boolean
        ///     that specifies whether to use the user-selected culture settings from the
        ///     system.
        /// </Summary>
        /// <Param name="culture">
        ///     A predefined System.Globalization.CultureInfo identifier, System.Globalization.CultureInfo.LCID
        ///     property of an existing System.Globalization.CultureInfo object, or Windows-only
        ///     culture identifier.
        /// </Param>
        /// <Param name="useUserOverride">        
        ///     A Boolean that denotes whether to use the user-selected culture settings
        ///     (true) or the default culture settings (false).
        /// </Param>
        /// <Exception cref="ArgumentOutOfRangeException">
        ///     culture is less than zero.                
        /// </Exception>
        /// <Exception cref="CultureNotFoundException">
        ///     culture is not a valid culture identifier.
        /// </Exception>
        public CultureInfoWrap(int culture,
                               bool useUserOverride)
        {
            this.CultureInfoInstance = new CultureInfo(culture, useUserOverride);
        }

        /// <Summary>
        ///     Initializes a new instance of the System.Globalization.CultureInfo class
        ///     based on the culture specified by name and on the Boolean that specifies
        ///     whether to use the user-selected culture settings from the system.
        /// </Summary>
        /// <Param name="name">
        ///     A predefined System.Globalization.CultureInfo name, System.Globalization.CultureInfo.Name
        ///     of an existing System.Globalization.CultureInfo, or Windows-only culture
        ///     name. name is not case-sensitive.
        /// </Param>
        /// <Param name="useUserOverride">
        ///     A Boolean that denotes whether to use the user-selected culture settings
        ///     (true) or the default culture settings (false).
        /// </Param>
        /// <Exception cref="ArgumentNullException">
        ///     name is null.
        /// </Exception>
        /// <Exception cref="CultureNotFoundException">
        ///     name is not a valid culture name.
        /// </Exception>        
        public CultureInfoWrap(string name,
                               bool useUserOverride)
        {
            this.CultureInfoInstance = new CultureInfo(name, useUserOverride);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the default calendar used by the culture.
        /// </summary>
        /// <returns>
        ///     A System.Globalization.Calendar that represents the default calendar used
        ///     by the culture.
        /// </returns>
        public Calendar Calendar
        {
            get
            {
                return this.CultureInfoInstance.Calendar;
            }
        }

        /// <summary>
        ///    Gets the System.Globalization.CompareInfo that defines how to compare strings
        ///     for the culture.
        /// </summary>
        /// <returns>
        ///     The System.Globalization.CompareInfo that defines how to compare strings
        ///     for the culture.        
        /// </returns>
        public CompareInfo CompareInfo
        {
            get
            {
                return this.CultureInfoInstance.CompareInfo;
            }
        }

        /// <summary>
        /// Wrapped CultureInfo Instance.
        /// </summary>
        public CultureInfo CultureInfoInstance { get; set; }


        /// <Summary>
        ///     Gets the System.Globalization.CultureInfo object that is culture-independent
        ///     (invariant).
        /// </Summary>
        /// <Returns>
        ///     The object that is culture-independent (invariant).
        /// </Returns>        
        public CultureInfo InvariantCulture
        {
            get
            {
                return CultureInfo.InvariantCulture;
            } 
        }

        /// 
        ///  <Summary>
        ///      Gets the culture types that pertain to the current System.Globalization.CultureInfo
        ///      object.
        ///  </Summary>
        /// <Returns>
        ///      A bitwise combination of one or more System.Globalization.CultureTypes values.
        ///      There is no default value.
        /// </returns>
        public CultureTypes CultureTypes
        {
            get
            {
                return this.CultureInfoInstance.CultureTypes;
            }
        }

        /// <Summary>
        ///     Gets or sets a System.Globalization.DateTimeFormatInfo that defines the culturally
        ///     appropriate format of displaying dates and times.
        /// </Summary> 
        /// <Returns>
        ///     A System.Globalization.DateTimeFormatInfo that defines the culturally appropriate
        ///     format of displaying dates and times.
        /// </Returns>
        /// <Exception cref="ArgumentNullException">
        ///     The property is set to null.
        /// </Exception>
        /// <Exception cref="InvalidOperationException">
        ///     The System.Globalization.CultureInfo.DateTimeFormat property or any of the
        ///     System.Globalization.DateTimeFormatInfo properties is set, and the System.Globalization.CultureInfo
        ///     is read-only.
        /// </Exception>
        public DateTimeFormatInfo DateTimeFormat
        {
            get
            {
                return this.CultureInfoInstance.DateTimeFormat;
            }

            set
            {
                this.CultureInfoInstance.DateTimeFormat = value;
            }
        }

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
        public string DisplayName
        {
            get
            {
                return this.CultureInfoInstance.DisplayName;
            }
        }

        /// <Summary>
        ///     Gets the culture name in the format "&lt;languagefull&gt; (&lt;country/regionfull&gt;)"
        ///     in English.
        /// </Summary> 
        /// <Returns>
        ///     The culture name in the format "&lt;languagefull&gt; (&lt;country/regionfull&gt;)" in
        ///     English, where &lt;languagefull&gt; is the full name of the language and &lt;country/regionfull&gt;
        ///     is the full name of the country/region.
        /// </Returns>
        public string EnglishName
        {
            get
            {
                return this.CultureInfoInstance.EnglishName;
            }
        }

        /// <Summary>
        ///     Deprecated. Gets the RFC 4646 standard identification for a language.
        /// </Summary> 
        /// <Returns>
        ///     A string that is the RFC 4646 standard identification for a language.
        /// </Returns>
        public string IetfLanguageTag
        {
            get
            {
                return this.CultureInfoInstance.IetfLanguageTag;
            }
        }

        /// <Summary> 
        ///     Gets a value indicating whether the current System.Globalization.CultureInfo
        ///     represents a neutral culture.
        /// </Summary> 
        /// <Returns>
        ///     true if the current System.Globalization.CultureInfo represents a neutral
        ///     culture; otherwise, false.
        /// </Returns>
        public bool IsNeutralCulture
        {
            get
            {
                return this.CultureInfoInstance.IsNeutralCulture;
            }
        }

        /// <Summary>
        ///     Gets a value indicating whether the current System.Globalization.CultureInfo
        ///     is read-only.
        /// </Summary> 
        /// <Returns>
        ///     true if the current System.Globalization.CultureInfo is read-only; otherwise,
        ///     false. The default is false.
        /// </Returns>
        public bool IsReadOnly
        {
            get
            {
                return this.CultureInfoInstance.IsReadOnly;
            }
        }

        /// <Summary>
        ///     Gets the active input locale identifier.
        /// </Summary> 
        /// <Returns>
        ///     A 32-bit signed number that specifies an input locale identifier.
        /// </Returns> 
        public int KeyboardLayoutId
        {
            get
            {
                return this.CultureInfoInstance.KeyboardLayoutId;
            }
        }

        /// <Summary>
        ///     Gets the culture identifier for the current System.Globalization.CultureInfo.
        /// </Summary> 
        /// <Returns>
        ///     The culture identifier for the current System.Globalization.CultureInfo.
        /// </Returns>
        // ReSharper disable once InconsistentNaming
        public int LCID
        {
            get
            {
                return this.CultureInfoInstance.LCID;
            }
        }

        /// <Summary>
        ///     Gets the culture name in the format "languagecode2-country/regioncode2".
        /// </Summary> 
        /// <Returns>
        ///     The culture name in the format "languagecode2-country/regioncode2", where
        ///     languagecode2 is a lowercase two-letter code derived from ISO 639-1 and country/regioncode2
        ///     is an uppercase two-letter code derived from ISO 3166.
        /// </Returns>
        public string Name
        {
            get
            {
                return this.CultureInfoInstance.Name;
            }
        }

        /// <Summary>
        ///     Gets the culture name, consisting of the language, the country/region, and
        ///     the optional script, that the culture is set to display.
        /// </Summary> 
        /// <Returns>
        ///     The culture name. consisting of the full name of the language, the full name
        ///     of the country/region, and the optional script. The format is discussed in
        ///     the description of the System.Globalization.CultureInfo class.
        /// </Returns>
        public string NativeName
        {
            get
            {
                return this.CultureInfoInstance.NativeName;
            }
        }

        /// <Summary>
        ///     Gets or sets a System.Globalization.NumberFormatInfo that defines the culturally
        ///     appropriate format of displaying numbers, currency, and percentage.
        ///
        /// </Summary> 
        /// <Returns>
        ///     A System.Globalization.NumberFormatInfo that defines the culturally appropriate
        ///     format of displaying numbers, currency, and percentage.
        /// </Returns>
        /// <Exception cref="ArgumentNullException">
        ///     The property is set to null.
        /// </Exception>
        /// <Exception cref="InvalidOperationException">
        ///     The System.Globalization.CultureInfo.NumberFormat property or any of the
        ///     System.Globalization.NumberFormatInfo properties is set, and the System.Globalization.CultureInfo
        ///     is read-only.
        /// </Exception>:
        public NumberFormatInfo NumberFormat
        {
            get
            {
                return this.CultureInfoInstance.NumberFormat;
            }
            set
            {
                this.CultureInfoInstance.NumberFormat = value;
            }
        }

        ///
        /// <Summary>
        ///     Gets the list of calendars that can be used by the culture.
        /// </Summary> 
        /// <Returns>
        ///     An array of type System.Globalization.Calendar that represents the calendars
        ///     that can be used by the culture represented by the current System.Globalization.CultureInfo.
        /// </Returns>
        public Calendar[] OptionalCalendars
        {
            get
            {
                return this.CultureInfoInstance.OptionalCalendars;
            }
        }

        /// <Summary>
        ///     Gets the System.Globalization.TextInfo that defines the writing system associated
        ///     with the culture.
        /// </Summary> 
        /// <Returns>
        ///     The System.Globalization.TextInfo that defines the writing system associated
        ///     with the culture.
        /// </Returns>
        public TextInfo TextInfo
        {
            get
            {
                return this.CultureInfoInstance.TextInfo;
            }
        }

        /// <Summary>
        ///     Gets the ISO 639-2 three-letter code for the language of the current System.Globalization.CultureInfo.
        /// </Summary> 
        /// <Returns>
        ///     The ISO 639-2 three-letter code for the language of the current System.Globalization.CultureInfo.
        /// </Returns>
        // ReSharper disable once InconsistentNaming
        public string ThreeLetterISOLanguageName
        {
            get
            {
                return this.CultureInfoInstance.ThreeLetterISOLanguageName;
            }
        }

        /// <Summary>
        ///     Gets the three-letter code for the language as defined in the Windows API.
        /// </Summary> 
        /// <Returns>
        ///     The three-letter code for the language as defined in the Windows API.
        /// </Returns>
        public string ThreeLetterWindowsLanguageName
        {
            get
            {
                return this.CultureInfoInstance.ThreeLetterWindowsLanguageName;
            }
        }

        /// <Summary>
        ///     Gets the ISO 639-1 two-letter code for the language of the current System.Globalization.CultureInfo.
        /// </Summary> 
        /// <Returns>
        ///     The ISO 639-1 two-letter code for the language of the current System.Globalization.CultureInfo.
        /// </Returns>
        // ReSharper disable once InconsistentNaming
        public string TwoLetterISOLanguageName
        {
            get
            {
                return this.CultureInfoInstance.TwoLetterISOLanguageName;
            }
        }

        /// <Summary>
        ///     Gets a value indicating whether the current System.Globalization.CultureInfo
        ///     uses the user-selected culture settings.
        /// </Summary> 
        /// <Returns>
        ///     true if the current System.Globalization.CultureInfo uses the user-selected
        ///     culture settings; otherwise, false.
        /// </Returns>
        public bool UseUserOverride
        {
            get
            {
                return this.CultureInfoInstance.UseUserOverride;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <Summary>
        ///     Refreshes cached culture-related information.
        /// </Summary> 
        public void ClearCachedData()
        {
            throw new NotImplementedException();
        }

        /// <Summary>
        ///     Creates a copy of the current System.Globalization.CultureInfo.
        /// </Summary> 
        /// <Returns>
        ///     A copy of the current System.Globalization.CultureInfo.
        /// </Returns>
        public object Clone()
        {
            throw new NotImplementedException();
        }

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
        public CultureInfo CreateSpecificCulture(string name)
        {
            throw new NotImplementedException();
        }

        /// <Summary>
        ///     Gets an alternate user interface culture suitable for console applications
        ///     when the default graphic user interface culture is unsuitable.
        /// </Summary> 
        /// <Returns>
        ///     An alternate culture that is used to read and display text on the console.
        /// </Returns>
        public CultureInfo GetConsoleFallbackUICulture()
        {
            throw new NotImplementedException();
        }

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
        /// <Exception cref="ArgumentOutOfRangeException">
        ///     culture is less than zero.
        /// </Exception>
        /// <Exception cref="CultureNotFoundException">
        ///     culture specifies a culture that is not supported.
        /// </Exception>
        public CultureInfo GetCultureInfo(int culture)
        {
            throw new NotImplementedException();
        }

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
        /// <Exception cref="ArgumentNullException">
        ///     name is null.
        /// </Exception>
        /// <Exception cref="CultureNotFoundException">
        ///     name specifies a culture that is not supported.
        /// </Exception>
        public CultureInfo GetCultureInfo(string name)
        {
            throw new NotImplementedException();
        }

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
        /// <Exception cref="ArgumentNullException">
        ///     name or altName is null.
        /// </Exception>
        /// <Exception cref="CultureNotFoundException">
        ///     name or altName specifies a culture that is not supported.
        /// </Exception>
        public CultureInfo GetCultureInfo(string name,
                                          string altName)
        {
            throw new NotImplementedException();
        }

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
        /// <Exception cref="ArgumentNullException">
        ///     name is null.
        /// </Exception>
        /// <Exception cref="CultureNotFoundException">
        ///     name does not correspond to a supported culture.
        /// </Exception>
        public CultureInfo GetCultureInfoByIetfLanguageTag(string name)
        {
            throw new NotImplementedException();
        }

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
        /// <Exception cref="ArgumentOutOfRangeException">
        ///     types specifies an invalid combination of System.Globalization.CultureTypes
        /// </Exception>
        public CultureInfo[] GetCultures(CultureTypes types)
        {
            throw new NotImplementedException();
        }

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
        public object GetFormat(Type formatType)
        {
            throw new NotImplementedException();
        }

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
        /// <Exception cref="ArgumentNullException">
        ///     ci is null.
        /// </Exception>
        public CultureInfo ReadOnly(CultureInfo ci)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}