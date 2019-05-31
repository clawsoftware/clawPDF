using SystemInterface.Microsoft.Win32;

namespace clawSoft.clawPDF.Utilities.Registry
{
    /// <summary>
    ///     http://www.codeproject.com/Articles/16343/Copy-and-Rename-Registry-Keys
    ///     License: This article, along with any associated source code and files, is licensed under The Code Project Open
    ///     License (CPOL)
    /// </summary>
    public class RegistryUtility
    {
        /// <summary>
        ///     Renames a subkey of the passed in registry key since
        ///     the Framework totally forgot to include such a handy feature.
        /// </summary>
        /// <param name="parentKey">
        ///     The RegistryKey that contains the subkey
        ///     you want to rename (must be writeable)
        /// </param>
        /// <param name="subKeyName">
        ///     The name of the subkey that you want to rename
        /// </param>
        /// <param name="newSubKeyName">The new name of the RegistryKey</param>
        /// <returns>True if succeeds</returns>
        public bool RenameSubKey(IRegistryKey parentKey, string subKeyName, string newSubKeyName)
        {
            CopyKey(parentKey, subKeyName, newSubKeyName);
            parentKey.DeleteSubKeyTree(subKeyName);
            return true;
        }

        /// <summary>
        ///     Copy a registry key.  The parentKey must be writeable.
        /// </summary>
        /// <param name="parentKey"></param>
        /// <param name="keyNameToCopy"></param>
        /// <param name="newKeyName"></param>
        /// <returns></returns>
        public bool CopyKey(IRegistryKey parentKey, string keyNameToCopy, string newKeyName)
        {
            //Create new key
            var destinationKey = parentKey.CreateSubKey(newKeyName);

            //Open the sourceKey we are copying from
            var sourceKey = parentKey.OpenSubKey(keyNameToCopy);

            RecurseCopyKey(sourceKey, destinationKey);

            return true;
        }

        private void RecurseCopyKey(IRegistryKey sourceKey, IRegistryKey destinationKey)
        {
            //copy all the values
            foreach (var valueName in sourceKey.GetValueNames())
            {
                var objValue = sourceKey.GetValue(valueName);
                var valKind = sourceKey.GetValueKind(valueName);
                destinationKey.SetValue(valueName, objValue, valKind);
            }

            //For Each subKey
            //Create a new subKey in destinationKey
            //Call myself
            foreach (var sourceSubKeyName in sourceKey.GetSubKeyNames())
            {
                var sourceSubKey = sourceKey.OpenSubKey(sourceSubKeyName);
                var destSubKey = destinationKey.CreateSubKey(sourceSubKeyName);
                RecurseCopyKey(sourceSubKey, destSubKey);
            }
        }
    }
}