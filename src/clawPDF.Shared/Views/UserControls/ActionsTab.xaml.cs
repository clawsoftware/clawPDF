using System.Windows.Controls;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels.UserControls;

namespace clawSoft.clawPDF.Shared.Views.UserControls
{
    public partial class ActionsTab : UserControl
    {
        public ActionsTab()
        {
            InitializeComponent();
            if (TranslationHelper.Instance.IsInitialized) TranslationHelper.Instance.TranslatorInstance.Translate(this);
        }

        public ActionsTabViewModel ViewModel => (ActionsTabViewModel)DataContext;
    }
}