using System;
using System.Diagnostics;
using SystemInterface.Diagnostics;

namespace SystemWrapper.Diagnostics
{
    public class FileVersionInfoWrap : IFileVersionInfo
    {
        public FileVersionInfoWrap(FileVersionInfo instance)
        {
            this.Instance = instance;
        }

        public FileVersionInfo Instance { get; }

        public string Comments
        {
            get
            {
                return Instance.Comments;
            }
        }

        public string CompanyName
        {
            get
            {
                return Instance.CompanyName;
            }
        }

        public int FileBuildPart
        {
            get
            {
                return Instance.FileBuildPart;
            }
        }

        public string FileDescription
        {
            get
            {
                return Instance.FileDescription;
            }
        }

        public int FileMajorPart
        {
            get
            {
                return Instance.FileMajorPart;
            }
        }

        public int FileMinorPart
        {
            get
            {
                return Instance.FileMinorPart;
            }
        }

        public string FileName
        {
            get
            {
                return Instance.FileName;
            }
        }

        public int FilePrivatePart
        {
            get
            {
                return Instance.FilePrivatePart;
            }
        }

        public string FileVersion
        {
            get
            {
                return Instance.FileVersion;
            }
        }

        public string InternalName
        {
            get
            {
                return Instance.InternalName;
            }
        }

        public bool IsDebug
        {
            get
            {
                return Instance.IsDebug;
            }
        }

        public bool IsPatched
        {
            get
            {
                return Instance.IsPatched;
            }
        }

        public bool IsPreRelease
        {
            get
            {
                return Instance.IsPreRelease;
            }
        }

        public bool IsPrivateBuild
        {
            get
            {
                return Instance.IsPrivateBuild;
            }
        }

        public bool IsSpecialBuild
        {
            get
            {
                return Instance.IsSpecialBuild;
            }
        }

        public string Language
        {
            get
            {
                return Instance.Language;
            }
        }

        public string LegalCopyright
        {
            get
            {
                return Instance.LegalCopyright;
            }
        }

        public string LegalTrademarks
        {
            get
            {
                return Instance.LegalTrademarks;
            }
        }

        public string OriginalFilename
        {
            get
            {
                return Instance.OriginalFilename;
            }
        }

        public string PrivateBuild
        {
            get
            {
                return Instance.PrivateBuild;
            }
        }

        public int ProductBuildPart
        {
            get
            {
                return Instance.ProductBuildPart;
            }
        }

        public int ProductMajorPart
        {
            get
            {
                return Instance.ProductMajorPart;
            }
        }

        public int ProductMinorPart
        {
            get
            {
                return Instance.ProductMinorPart;
            }
        }

        public string ProductName
        {
            get
            {
                return Instance.ProductName;
            }
        }

        public int ProductPrivatePart
        {
            get
            {
                return Instance.ProductPrivatePart;
            }
        }

        public string ProductVersion
        {
            get
            {
                return Instance.ProductVersion;
            }
        }

        public string SpecialBuild
        {
            get
            {
                return Instance.SpecialBuild;
            }
        }

        public override string ToString()
        {
            return Instance.ToString();
        }
    }
}
