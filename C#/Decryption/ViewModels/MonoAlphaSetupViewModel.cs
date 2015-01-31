using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using Decryption.Common;
using Decryption.Interfaces;

namespace Decryption.ViewModels {
    class MonoAlphaSetupViewModel : ModelBase, IDecryptionSetupViewModel {
        private readonly IDecrypter decrypter;
        private readonly IObservableText encryptedText;

        public ICommand LoadSampleTextCommand { get; private set; }

        public string Name {
            get {
                return decrypter.ToString();
            }
        }

        public MonoAlphaSetupViewModel(IDecrypter decrypter, IObservableText encryptedText) {
            this.decrypter = decrypter;
            this.encryptedText = encryptedText;
            LoadSampleTextCommand = new SimpleDelegateCommand(LoadSampleText);
        }

        private void LoadSampleText() {
            encryptedText.Text = Properties.Resources.SampleMonoAlphaText;
        }
    }
}
