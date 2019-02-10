using clawSoft.clawPDF.Helper;

namespace clawSoft.clawPDF.Startup
{
    internal class InitializeSettingsStart : IAppStart
    {
        public bool Run()
        {
            SettingsHelper.SaveSettings();

            return true;
        }
    }
}