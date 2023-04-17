using System;

namespace SystemInterface
{
    /// <summary>
    /// Wrapper for <see cref="System.Version"/> class.
    /// </summary>
    public interface IVersion : ICloneable, IComparable, IComparable<IVersion>, IEquatable<IVersion>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.VersionWrap"/> class.
        /// </summary>
        void Initialize(Version version);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.VersionWrap"/> class.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Initializes a new instance of the Version class using the specified string.
        /// </summary>
        /// <param name="version">A string containing the major, minor, build, and revision numbers, where each number is delimited with a period character ('.').</param>
        void Initialize(string version);

        /// <summary>
        /// Initializes a new instance of the Version class using the specified major and minor values.
        /// </summary>
        /// <param name="major">The major version number.</param>
        /// <param name="minor">The minor version number.</param>
        void Initialize(int major, int minor);

        /// <summary>
        /// Initializes a new instance of the Version class using the specified major and minor values.
        /// </summary>
        /// <param name="major">The major version number.</param>
        /// <param name="minor">The minor version number.</param>
        /// <param name="build">The build number.</param>
        void Initialize(int major, int minor, int build);

        /// <summary>
        /// Initializes a new instance of the Version class using the specified major and minor values.
        /// </summary>
        /// <param name="major">The major version number.</param>
        /// <param name="minor">The minor version number.</param>
        /// <param name="build">The build number.</param>
        /// <param name="revision">The revision number.</param>
        void Initialize(int major, int minor, int build, int revision);

        // Properties

        /// <summary>
        /// Gets <see cref="T:System.Version"/> object.
        /// </summary>
        Version VersionInstance { get; }

        /// <summary>
        /// Gets the value of the build component of the version number for the current Version object.
        /// </summary>
        int Build { get; }

        /// <summary>
        /// Gets the value of the major component of the version number for the current Version object.
        /// </summary>
        int Major { get; }

        /// <summary>
        /// Gets the high 16 bits of the revision number.
        /// </summary>
        short MajorRevision { get; }

        /// <summary>
        /// Gets the value of the minor component of the version number for the current Version object.
        /// </summary>
        int Minor { get; }

        /// <summary>
        /// Gets the low 16 bits of the revision number.
        /// </summary>
        short MinorRevision { get; }

        /// <summary>
        /// Gets the value of the revision component of the version number for the current Version object.
        /// </summary>
        int Revision { get; }

        // Methods

        /// <summary>
        /// Returns a value indicating whether the current Version object is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with the current Version object, or nullNothingnullptra null reference (Nothing in Visual Basic).</param>
        /// <returns> <c>true</c> if the current Version object and obj are both Version objects, and every component of the current Version object matches the corresponding component of obj; otherwise, <c>false</c>.</returns>
        bool Equals(object obj);

        /// <summary>
        /// Returns a hash code for the current Version object.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        int GetHashCode();

        /// <summary>
        /// Converts the value of the current IVersionWrap object to its equivalent String representation.
        /// </summary>
        /// <returns>
        /// The String representation of the values of the major, minor, build, and revision components of the current Version object, as depicted in the following format. Each component is separated by a period character ('.'). Square brackets ('[' and ']') indicate a component that will not appear in the return value if the component is not defined:
        /// major.minor[.build[.revision]].
        /// </returns>
        string ToString();

        /// <summary>
        /// Converts the value of the current Version object to its equivalent String representation. A specified count indicates the number of components to return.
        /// </summary>
        /// <param name="fieldCount">The number of components to return. The fieldCount ranges from 0 to 4.</param>
        /// <returns>The String representation of the values of the major, minor, build, and revision components of the current Version object, each separated by a period character ('.'). The fieldCount parameter determines how many components are returned.</returns>
        string ToString(int fieldCount);

        /*
            public static bool operator ==(Version v1, Version v2);
            public static bool operator >(Version v1, Version v2);
            public static bool operator >=(Version v1, Version v2);
            public static bool operator !=(Version v1, Version v2);
            public static bool operator <(Version v1, Version v2);
            public static bool operator <=(Version v1, Version v2);
        */
    }
}
