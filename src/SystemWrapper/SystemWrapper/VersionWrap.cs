using System;
using SystemInterface;

namespace SystemWrapper
{
    /// <summary>
    /// Wrapper for <see cref="System.Version"/> class.
    /// </summary>
    public class VersionWrap : IVersion
    {
        #region Constructors and Initializers

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.VersionWrap"/> class.
        /// </summary>
        public VersionWrap(Version version)
        {
            Initialize(version);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.VersionWrap"/> class.
        /// </summary>
        public void Initialize(Version version)
        {
            VersionInstance = version;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.VersionWrap"/> class.
        /// </summary>
        public VersionWrap()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.VersionWrap"/> class.
        /// </summary>
        public void Initialize()
        {
            VersionInstance = new Version();
        }

        /// <summary>
        /// Initializes a new instance of the Version class using the specified string.
        /// </summary>
        /// <param name="version">A string containing the major, minor, build, and revision numbers, where each number is delimited with a period character ('.').</param>
        public VersionWrap(string version)
        {
            Initialize(version);
        }

        /// <summary>
        /// Initializes a new instance of the Version class using the specified string.
        /// </summary>
        /// <param name="version">A string containing the major, minor, build, and revision numbers, where each number is delimited with a period character ('.').</param>
        public void Initialize(string version)
        {
            VersionInstance = new Version(version);
        }

        /// <summary>
        /// Initializes a new instance of the Version class using the specified major and minor values.
        /// </summary>
        /// <param name="major">The major version number.</param>
        /// <param name="minor">The minor version number.</param>
        public VersionWrap(int major, int minor)
        {
            Initialize(major, minor);
        }

        /// <summary>
        /// Initializes a new instance of the Version class using the specified major and minor values.
        /// </summary>
        /// <param name="major">The major version number.</param>
        /// <param name="minor">The minor version number.</param>
        public void Initialize(int major, int minor)
        {
            VersionInstance = new Version(major, minor);
        }

        /// <summary>
        /// Initializes a new instance of the Version class using the specified major and minor values.
        /// </summary>
        /// <param name="major">The major version number.</param>
        /// <param name="minor">The minor version number.</param>
        /// <param name="build">The build number.</param>
        public VersionWrap(int major, int minor, int build)
        {
            Initialize(major, minor, build);
        }

        /// <summary>
        /// Initializes a new instance of the Version class using the specified major and minor values.
        /// </summary>
        /// <param name="major">The major version number.</param>
        /// <param name="minor">The minor version number.</param>
        /// <param name="build">The build number.</param>
        public void Initialize(int major, int minor, int build)
        {
            VersionInstance = new Version(major, minor, build);
        }

        /// <summary>
        /// Initializes a new instance of the Version class using the specified major and minor values.
        /// </summary>
        /// <param name="major">The major version number.</param>
        /// <param name="minor">The minor version number.</param>
        /// <param name="build">The build number.</param>
        /// <param name="revision">The revision number.</param>
        public VersionWrap(int major, int minor, int build, int revision)
        {
            Initialize(major, minor, build, revision);
        }

        /// <summary>
        /// Initializes a new instance of the Version class using the specified major and minor values.
        /// </summary>
        /// <param name="major">The major version number.</param>
        /// <param name="minor">The minor version number.</param>
        /// <param name="build">The build number.</param>
        /// <param name="revision">The revision number.</param>
        public void Initialize(int major, int minor, int build, int revision)
        {
            VersionInstance = new Version(major, minor, build, revision);
        }

        #endregion Constructors and Initializers

        /// <inheritdoc />
        public Version VersionInstance { get; private set; }

        /// <inheritdoc />
        public int Build
        {
            get { return VersionInstance.Build; }
        }

        /// <inheritdoc />
        public int Major
        {
            get { return VersionInstance.Major; }
        }

        /// <inheritdoc />
        public short MajorRevision
        {
            get { return VersionInstance.MajorRevision; }
        }

        /// <inheritdoc />
        public int Minor
        {
            get { return VersionInstance.Minor; }
        }

        /// <inheritdoc />
        public short MinorRevision
        {
            get { return VersionInstance.MinorRevision; }
        }

        /// <inheritdoc />
        public int Revision
        {
            get { return VersionInstance.Revision; }
        }

        /// <inheritdoc />
        public object Clone()
        {
            return VersionInstance.Clone();
        }

        /// <inheritdoc />
        public int CompareTo(object version)
        {
            return VersionInstance.CompareTo(version);
        }

        /// <inheritdoc />
        public int CompareTo(IVersion value)
        {
            return VersionInstance.CompareTo(value.VersionInstance);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return VersionInstance.Equals(obj);
        }

        /// <inheritdoc />
        public bool Equals(IVersion obj)
        {
            return VersionInstance.Equals(obj.VersionInstance);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return VersionInstance.GetHashCode();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return VersionInstance.ToString();
        }

        /// <inheritdoc />
        public string ToString(int fieldCount)
        {
            return VersionInstance.ToString(fieldCount);
        }
    }
}