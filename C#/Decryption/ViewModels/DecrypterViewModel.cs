using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Interfaces;
using System.ComponentModel;
using System.Windows.Data;

namespace Decryption.ViewModels {
    internal class DecrypterViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private string encryptedText = "";
        public string EncryptedText {
            get {
                return encryptedText;
            }
            set {
                encryptedText = value;
                OnPropertyChanged("EncryptedText");
                OnPropertyChanged("DecryptedText");
            }
        }

        public string DecryptedText {
            get {
                return DecryptionAlgorithm.DecryptText(encryptedText);
            }
        }

        public List<IDecryptionAlgorithm> DecryptionAlgorithms { get; set; }

        public IDecryptionAlgorithm DecryptionAlgorithm { get; set; }

        public DecrypterViewModel(params IDecryptionAlgorithm[] decryptionAlgorithms) {
            this.DecryptionAlgorithms = new List<IDecryptionAlgorithm>(decryptionAlgorithms);
            DecryptionAlgorithm = DecryptionAlgorithms[0];
        }
    }
}
