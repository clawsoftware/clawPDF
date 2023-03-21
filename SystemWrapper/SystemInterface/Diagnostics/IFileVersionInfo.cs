using System.Diagnostics;

namespace SystemInterface.Diagnostics
{
    /// <summary>
    /// Provides version information for a physical file on disk.
    /// </summary>
    public interface IFileVersionInfo : IWrapper<FileVersionInfo>
    {
        /// <summary>
        /// Gets the comments associated with the file.
        /// </summary>
        /// <value>
        /// The comments associated with the file or null if the file did not contain version information.
        /// </value>
        string Comments { get; }

        /// <summary>
        /// Gets the name of the company that produced the file.
        /// </summary>
        /// <value>
        /// The name of the company that produced the file or null if the file did not contain
        /// version information.
        /// </value>
        string CompanyName { get; }

        /// <summary>
        /// Gets the build number of the file.
        /// </summary>
        /// <value>
        /// A value representing the build number of the file or null if the file did not
        /// contain version information.
        /// </value>
        int FileBuildPart { get; }

        /// <summary>
        /// Gets the description of the file.
        /// </summary>
        /// <value>
        /// The description of the file or null if the file did not contain version information.
        /// </value>
        string FileDescription { get; }

        /// <summary>
        /// Gets the major part of the version number.
        /// </summary>
        /// <value>
        /// A value representing the major part of the version number or 0 (zero) if the
        /// file did not contain version information.
        /// </value>
        int FileMajorPart { get; }

        /// <summary>
        /// Gets the minor part of the version number of the file.
        /// </summary>
        /// <value>
        /// A value representing the minor part of the version number of the file or 0 (zero)
        /// if the file did not contain version information.
        /// </value>
        int FileMinorPart { get; }

        /// <summary>
        /// Gets the name of the file that this instance of System.Diagnostics.FileVersionInfo
        /// describes.
        /// </summary>
        /// <value>
        /// The name of the file described by this instance of System.Diagnostics.FileVersionInfo.
        /// </value>
        string FileName { get; }

        /// <summary>
        /// Gets the file private part number.
        /// </summary>
        /// <value>
        /// A value representing the file private part number or null if the file did not
        /// contain version information.
        /// </value>
        int FilePrivatePart { get; }

        /// <summary>
        /// Gets the file version number.
        /// </summary>
        /// <value>
        /// The version number of the file or null if the file did not contain version information.
        /// </value>
        string FileVersion { get; }

        /// <summary>
        /// Gets the internal name of the file, if one exists.
        /// </summary>
        /// <value>
        /// The internal name of the file. If none exists, this property will contain the
        /// original name of the file without the extension.
        /// </value>
        string InternalName { get; }

        /// <summary>
        /// Gets a value that specifies whether the file contains debugging information or
        /// is compiled with debugging features enabled.
        /// </summary>
        /// <value>
        /// true if the file contains debugging information or is compiled with debugging
        /// features enabled; otherwise, false.
        /// </value>
        bool IsDebug { get; }

        /// <summary>
        /// Gets a value that specifies whether the file has been modified and is not identical
        /// to the original shipping file of the same version number.
        /// </summary>
        /// <value>
        /// true if the file is patched; otherwise, false.
        /// </value>
        bool IsPatched { get; }

        /// <summary>
        /// Gets a value that specifies whether the file is a development version, rather
        /// than a commercially released product.
        /// </summary>
        /// <value>
        /// true if the file is prerelease; otherwise, false.
        /// </value>
        bool IsPreRelease { get; }

        /// <summary>
        /// Gets a value that specifies whether the file was built using standard release
        /// procedures.
        /// </summary>
        /// <value>
        /// true if the file is a private build; false if the file was built using standard
        /// release procedures or if the file did not contain version information.
        /// </value>
        bool IsPrivateBuild { get; }

        /// <summary>
        /// Gets a value that specifies whether the file is a special build.
        /// </summary>
        /// <value>
        /// true if the file is a special build; otherwise, false.
        /// </value>
        bool IsSpecialBuild { get; }

        /// <summary>
        /// Gets the default language string for the version info block.
        /// </summary>
        /// <value>
        /// The description string for the Microsoft Language Identifier in the version resource
        /// or null if the file did not contain version information.
        /// </value>
        string Language { get; }

        /// <summary>
        /// Gets all copyright notices that apply to the specified file.
        /// </summary>
        /// <value>
        /// The copyright notices that apply to the specified file.
        /// </value>
        string LegalCopyright { get; }

        /// <summary>
        /// Gets the trademarks and registered trademarks that apply to the file.
        /// </summary>
        /// <value>
        /// The trademarks and registered trademarks that apply to the file or null if the
        /// file did not contain version information.
        /// </value>
        string LegalTrademarks { get; }

        /// <summary>
        /// Gets the name the file was created with.
        /// </summary>
        /// <value>
        /// The name the file was created with or null if the file did not contain version
        /// information.
        /// </value>
        string OriginalFilename { get; }

        /// <summary>
        /// Gets information about a private version of the file.
        /// </summary>
        /// <value>
        /// Information about a private version of the file or null if the file did not contain
        /// version information.
        /// </value>
        string PrivateBuild { get; }

        /// <summary>
        /// Gets the build number of the product this file is associated with.
        /// </summary>
        /// <value>
        /// A value representing the build number of the product this file is associated
        /// with or null if the file did not contain version information.
        /// </value>
        int ProductBuildPart { get; }

        /// <summary>
        /// Gets the major part of the version number for the product this file is associated
        /// with.
        /// </summary>
        /// <value>
        /// A value representing the major part of the product version number or null if
        /// the file did not contain version information.
        /// </value>
        int ProductMajorPart { get; }

        /// <summary>
        /// Gets the minor part of the version number for the product the file is associated
        /// with.
        /// </summary>
        /// <value>
        /// A value representing the minor part of the product version number or null if
        /// the file did not contain version information.
        /// </value>
        int ProductMinorPart { get; }

        /// <summary>
        /// Gets the name of the product this file is distributed with.
        /// </summary>
        /// <value>
        /// The name of the product this file is distributed with or null if the file did
        /// not contain version information.
        /// </value>
        string ProductName { get; }

        /// <summary>
        /// Gets the private part number of the product this file is associated with.
        /// </summary>
        /// <value>
        /// A value representing the private part number of the product this file is associated
        /// with or null if the file did not contain version information.
        /// </value>
        int ProductPrivatePart { get; }

        /// <summary>
        /// Gets the version of the product this file is distributed with.
        /// </summary>
        /// <value>
        /// The version of the product this file is distributed with or null if the file
        /// did not contain version information.
        /// </value>
        string ProductVersion { get; }

        /// <summary>
        /// Gets the special build information for the file.
        /// </summary>
        /// <value>
        /// The special build information for the file or null if the file did not contain
        /// version information.
        /// </value>
        string SpecialBuild { get; }
    }
}
