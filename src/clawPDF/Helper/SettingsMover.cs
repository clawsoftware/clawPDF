using System.Security;
using clawSoft.clawPDF.Utilities.Registry;
using SystemInterface.Microsoft.Win32;
using SystemWrapper.Microsoft.Win32;

namespace clawSoft.clawPDF.Helper
{
    /// <summary>
    ///     It is good practice to store Registry settings under HKEY_CURRENT_USER\Software\CompanyName\ProductName
    ///     In the past, we stored them under HKEY_CURRENT_USER\Software\clawPDF.net
    ///     They are now stored under HKEY_CURRENT_USER\Software\clawSoft\clawPDF
    ///     This class checks if old settings exist, if they need to be moved to the new location
    ///     and performs the move if required.
    /// </summary>
    public class SettingsMover
    {
        private const string OldRegistryPath = @"Software\clawPDF.Net";
        private const string NewRegistryPath = @"Software\clawSoft\clawPDF";
        private readonly RegistryUtility _registryUtility;
        private readonly IRegistry _registryWrap;

        public SettingsMover()
            : this(new RegistryWrap(), new RegistryUtility())
        {
        }

        public SettingsMover(IRegistry registryWrap, RegistryUtility registryUtility)
        {
            _registryWrap = registryWrap;
            _registryUtility = registryUtility;
        }

        public bool MoveRequired()
        {
            try
            {
                var regKey = _registryWrap.CurrentUser.OpenSubKey(OldRegistryPath);
                if (regKey == null)
                    return false;

                regKey.Close();

                regKey = _registryWrap.CurrentUser.OpenSubKey(NewRegistryPath);
                if (regKey == null)
                    return true;

                regKey.Close();
            }
            catch (SecurityException)
            {
            }

            return false;
        }

        public bool MoveSettings()
        {
            if (!MoveRequired())
                return false;

            return _registryUtility.RenameSubKey(_registryWrap.CurrentUser, OldRegistryPath, NewRegistryPath);
        }
    }
}