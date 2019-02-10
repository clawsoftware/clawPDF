using System;
using clawSoft.clawPDF.Core.Actions;

namespace clawSoft.clawPDF.Shared.ViewModels
{
    public class SignaturePasswordViewModel : ViewModelBase
    {
        private string _password;
        private ISignaturePasswordCheck _signaturePasswordCheck;

        public SignaturePasswordViewModel()
        {
            Result = SignaturePasswordResult.Cancel;
            StorePasswordCommand = new DelegateCommand(ExecuteStorePasswordCommand, CanExecuteStorePasswordCommand);
            SkipCommand = new DelegateCommand(ExecuteSkipCommand);
            RemovePasswordCommand = new DelegateCommand(ExecuteRemovePasswordCommand);
        }

        public SignaturePasswordViewModel(ISignaturePasswordCheck passwordCheck)
            : this()
        {
            SignaturePasswordCheck = passwordCheck;
        }

        public DelegateCommand StorePasswordCommand { get; }
        public DelegateCommand SkipCommand { get; }
        public DelegateCommand RemovePasswordCommand { get; }

        public ISignaturePasswordCheck SignaturePasswordCheck
        {
            get => _signaturePasswordCheck;
            set
            {
                _signaturePasswordCheck = value;
                StorePasswordCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                StorePasswordCommand.RaiseCanExecuteChanged();
            }
        }

        public SignaturePasswordResult Result { get; set; }

        private bool CanExecuteStorePasswordCommand(object o)
        {
            if (SignaturePasswordCheck == null)
                return false;

            return SignaturePasswordCheck.IsValidPassword(Password);
        }

        private void ExecuteStorePasswordCommand(object o)
        {
            if (!CanExecuteStorePasswordCommand(o))
                throw new InvalidOperationException();

            Result = SignaturePasswordResult.StorePassword;

            RaiseCloseView(true);
        }

        private void ExecuteSkipCommand(object obj)
        {
            Result = SignaturePasswordResult.Skip;
            _password = "";
            RaiseCloseView(true);
        }

        private void ExecuteRemovePasswordCommand(object obj)
        {
            Result = SignaturePasswordResult.RemovePassword;
            _password = "";
            RaiseCloseView(true);
        }
    }

    public interface ISignaturePasswordCheck
    {
        bool IsValidPassword(string password);
    }

    public class SignaturePasswordCheck : ISignaturePasswordCheck
    {
        private readonly string _certificateFile;

        public SignaturePasswordCheck(string certificateFile)
        {
            _certificateFile = certificateFile;
        }

        public bool IsValidPassword(string password)
        {
            if (password == null || _certificateFile == null)
                return false;

            return EncryptAndSignPdfAction.IsValidCertificatePassword(_certificateFile, password);
        }
    }

    public enum SignaturePasswordResult
    {
        Cancel,
        StorePassword,
        RemovePassword,
        Skip
    }
}