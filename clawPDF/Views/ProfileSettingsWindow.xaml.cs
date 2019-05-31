using System;
using System.ComponentModel;
using System.Windows;
using clawSoft.clawPDF.Core.Actions;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;
using clawSoft.clawPDF.Shared.Views;
using clawSoft.clawPDF.Shared.Views.ActionControls;
using clawSoft.clawPDF.Utilities.Tokens;
using clawSoft.clawPDF.ViewModels;
using clawSoft.clawPDF.Views.ActionControls;

namespace clawSoft.clawPDF.Views
{
    internal partial class ProfileSettingsWindow
    {
        private static readonly TranslationHelper TranslationHelper = TranslationHelper.Instance;
        private readonly TokenReplacer _tokenReplacer = TokenHelper.TokenReplacerWithPlaceHolders;

        public ProfileSettingsWindow(clawPDFSettings settings)
        {
            InitializeComponent();
            ViewModel = CreateViewModel(settings);

            ViewModel.ConversionProfilesView.CurrentChanged += OnConversionProfilesViewOnCurrentChanged;
            ViewModel.CurrentProfilePropertyChanged();

            AddActions();

            UpdateTabControls(ViewModel.CurrentProfile);

            if (ViewModel.CurrentProfile != null)
                DocumentTabUserControl.UpdateFontLabel(ViewModel.CurrentProfile.Stamping);
        }

        private ProfileSettingsViewModel ViewModel
        {
            get => (ProfileSettingsViewModel)DataContext;
            set => DataContext = value;
        }

        public clawPDFSettings Settings => ViewModel.Settings;

        private ProfileSettingsViewModel CreateViewModel(clawPDFSettings settings)
        {
            var viewModel = new ProfileSettingsViewModel(settings);

            viewModel.QueryProfileName = QueryProfileName;
            viewModel.QueryDeleteProfile = QueryDeleteProfile;
            viewModel.QueryDeleteProfileWithPrinterMapping = QueryDeleteProfileWithPrinterMapping;
            viewModel.QueryDiscardChanges = QueryDiscardUnsavedChanges;
            viewModel.QueryIgnoreDefectiveProfiles = QueryIgnoreDefectiveProfiles;
            viewModel.UpdateLayoutProfilesBoxAction = ProfilesBoxUpdateLayout;

            viewModel.CloseViewAction = delegate (bool? result) { DialogResult = result; };

            viewModel.InvalidProfileMessage = TranslationHelper.TranslatorInstance.GetTranslation(
                "ProfileSettingsWindow",
                "InvalidProfileName", "The entered profile name is invalid or does already exist.");

            return viewModel;
        }

        private void AddActions()
        {
            AddAction(new BackgroundActionControl());
            AddAction(new CoverActionControl());
            AddAction(new AttachmentActionControl());
            AddAction(new PrintActionControl());
            AddAction(new EmailClientActionControl());
            AddAction(new EmailSmtpActionControl());
            AddAction(new ScriptActionControl());
            AddAction(new FtpActionControl(_tokenReplacer));
        }

        private void AddAction(ActionControl action)
        {
            ActionsTabUserControl.ViewModel.AddAction(action);
        }

        private void OnConversionProfilesViewOnCurrentChanged(object sender, EventArgs e)
        {
            UpdateTabControls(ViewModel.CurrentProfile);

            if (ViewModel.CurrentProfile != null)
                DocumentTabUserControl.UpdateFontLabel(ViewModel.CurrentProfile.Stamping);
        }

        private void UpdateTabControls(ConversionProfile currentProfile)
        {
            DocumentTabUserControl.ViewModel.CurrentProfile = currentProfile;
            SaveTabUserControl.ViewModel.CurrentProfile = currentProfile;
            AutosaveTabUserControl.ViewModel.CurrentProfile = currentProfile;
            ActionsTabUserControl.ViewModel.CurrentProfile = currentProfile;
            ImageFormatsTabUserControl.ViewModel.CurrentProfile = currentProfile;
            PdfTabUserControl.ViewModel.CurrentProfile = currentProfile;
        }

        private string QueryProfileName(string profileName)
        {
            var f = new InputBoxWindow();
            f.IsValidInput = ViewModel.ProfilenameIsValid;
            f.QuestionText = TranslationHelper.TranslatorInstance.GetTranslation("ProfileSettingsWindow",
                "EnterProfileName",
                "Please enter profile name:");

            if (profileName != null)
                f.InputText = profileName;
            else
                f.InputText =
                    TranslationHelper.TranslatorInstance.GetTranslation("ProfileSettingsWindow", "NewProfile",
                        "New Profile");

            f.Title = TranslationHelper.TranslatorInstance.GetTranslation("ProfileSettingsWindow", "ProfileName",
                "Profile name");

            if (f.ShowDialog() != true)
                return null;

            return f.InputText;
        }

        private bool QueryDeleteProfile()
        {
            var message = TranslationHelper.TranslatorInstance.GetFormattedTranslation("ProfileSettingsWindow",
                "ReallyDeleteProfile",
                "Do you really want to the delete '{0}'?", ViewModel.CurrentProfile.Name);
            var caption = TranslationHelper.TranslatorInstance.GetTranslation("ProfileSettingsWindow", "DeleteProfile",
                "Delete profile");
            if (MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.YesNo, MessageWindowIcon.Question) !=
                MessageWindowResponse.Yes)
                return false;
            return true;
        }

        private bool QueryDeleteProfileWithPrinterMapping(string profileName, string printerName)
        {
            var message = TranslationHelper.TranslatorInstance.GetFormattedTranslation("ProfileSettingsWindow",
                "DeleteProfileWithMappedPrinter",
                "The profile '{0}' is assigned to the printer '{1}'. If you delete this profile, the printer will use the default profile instead. Do you want to proceed?",
                profileName, printerName);
            var caption = TranslationHelper.TranslatorInstance.GetTranslation("ProfileSettingsWindow",
                "ProfileHasPrinterTitle",
                "Printer assignment");
            if (MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.YesNo, MessageWindowIcon.Question) !=
                MessageWindowResponse.Yes)
                return false;
            return true;
        }

        private void ProfileSettingsWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            TranslationHelper.TranslatorInstance.Translate(this);

            ViewModel.CurrentProfilePropertyChanged();

            ActionsTabUserControl.ViewModel.SelectFirstEnabledOrFirstAction();
        }

        private static bool QueryDiscardUnsavedChanges()
        {
            var caption =
                TranslationHelper.TranslatorInstance.GetTranslation("ProfileSettingsWindow", "UnsavedChanges",
                    "Unsaved changes");
            var message = TranslationHelper.TranslatorInstance.GetTranslation("ProfileSettingsWindow",
                "ReallyWantToCancel",
                "Do you really want to cancel the settings and discard the changes?");

            var response = MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.YesNo,
                MessageWindowIcon.Question);

            return response == MessageWindowResponse.Yes;
        }

        private bool QueryIgnoreDefectiveProfiles(ActionResultDict actionResultDict)
        {
            return DefectiveProfilesWindow.ShowDialogTopMost(actionResultDict);
        }

        private void ProfilesBoxUpdateLayout()
        {
            var item = ProfilesBox.SelectedItem;
            ProfilesBox.SelectedItem = null;
            ProfilesBox.SelectedItem = item;
        }

        private void ProfileSettingsWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (ViewModel.ButtonClickedToClose == ButtonClickedToClose.None)
                if (!ViewModel.CheckForClosingWindowAndRestoreTheSettings())
                {
                    e.Cancel = true;
                    return;
                }

            SettingsHelper.ApplySettings(Settings);
            SettingsHelper.SaveSettings();
        }
    }
}