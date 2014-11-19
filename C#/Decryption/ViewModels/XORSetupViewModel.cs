using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Common;
using Decryption.Interfaces;
using Decryption.Models;

namespace Decryption.ViewModels {
    public class XORSetupViewModel : ModelBase, IDecryptionAlgorithmViewModel {
        private readonly XORDecryptionAlgorithm algorithm;
        public ICommand FindKeyCommand { get; private set; }

        public XORSetupViewModel(XORDecryptionAlgorithm algorithm) {
            this.algorithm = algorithm;
            FindKeyCommand = new SimpleDelegateCommand(FindKey);
            algorithm.KeyChanged += HandleKeyChanged;
        }

        void HandleKeyChanged(object sender, EventArgs e) {
            OnPropertyChanged("Key");
        }

        public string Key {
            get {
                return ASCIIEncoding.ASCII.GetString(algorithm.Key);
            }
            set {
                algorithm.Key = Encoding.ASCII.GetBytes(value);
                OnPropertyChanged("Key");
            }
        }

        private void FindKey() {
            algorithm.FindKey();
        }
    }
}
