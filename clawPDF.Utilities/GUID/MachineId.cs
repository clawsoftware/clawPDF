using System;
using clawSoft.clawPDF.Utilities.WindowsApi;
using SystemInterface.Microsoft.Win32;
using SystemWrapper.Microsoft.Win32;

namespace clawSoft.clawPDF.Utilities.GUID
{
    public class MachineId
    {
        public MachineId(long systemVolumeSerial, string windowsProductId)
        {
            SystemVolumeSerial = systemVolumeSerial;
            WindowsProductId = windowsProductId.Replace("-", "");
        }

        public long SystemVolumeSerial { get; }
        public string WindowsProductId { get; }

        public static MachineId BuildCurrentMachineId()
        {
            return BuildCurrentMachineId(Kernel32.GetSystemVolumeSerial, new RegistryWrap());
        }

        public static MachineId BuildCurrentMachineId(Func<long> getSystemVolumeSerial, IRegistry registry)
        {
            return new MachineId(getSystemVolumeSerial(), GetWindowsProductId(registry));
        }

        private static string GetWindowsProductId(IRegistry registry)
        {
            var v = registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductId",
                null);
            if (v == null)
                return "";
            return v.ToString();
        }

        public string CaclculateMachineHash()
        {
            return CaclculateMachineHash("GQ461qpa6s0SeD4qabZce6JVP7sTywtN");
        }

        public string CaclculateMachineHash(string salt)
        {
            if (string.IsNullOrEmpty(salt))
                throw new ArgumentException("salt");

            var serialString = SystemVolumeSerial.ToString("X8");

            var hashBase = serialString + WindowsProductId + salt;
            return HashUtil.GetSha1Hash(hashBase);
        }
    }
}