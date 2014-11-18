using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Interfaces;
using System.ComponentModel;
using System.Windows.Data;
using Decryption;

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

        public List<IDecryptionAlgorithm> DecryptionAlgorithms { get; private set; }
        public List<IDecryptionAlgorithmViewModel> DecryptionAlgorithmViewModels { get; private set; }

        private IDecryptionAlgorithm decryptionAlgorithm;
        public IDecryptionAlgorithm DecryptionAlgorithm {
            get {
                return decryptionAlgorithm;
            }
            set {
                decryptionAlgorithm = value;
                SetViewModel(decryptionAlgorithm);
            }
        }

        private IDecryptionAlgorithmViewModel decryptionAlgorithmViewModel;
        public IDecryptionAlgorithmViewModel DecryptionAlgorithmViewModel {
            get {
                return decryptionAlgorithmViewModel;
            }
            set {
                decryptionAlgorithmViewModel = value;
                OnPropertyChanged("DecryptionAlgorithmViewModel");
            }
        }

        public DecrypterViewModel(params Tuple<IDecryptionAlgorithm, IDecryptionAlgorithmViewModel>[] decryptionAlgorithmsVMPairs) {
            DecryptionAlgorithms = new List<IDecryptionAlgorithm>();
            DecryptionAlgorithmViewModels = new List<IDecryptionAlgorithmViewModel>();
            foreach (var decryptionAlgorithmVMPair in decryptionAlgorithmsVMPairs) {
                DecryptionAlgorithms.Add(decryptionAlgorithmVMPair.Item1);
                DecryptionAlgorithmViewModels.Add(decryptionAlgorithmVMPair.Item2);
            }

            DecryptionAlgorithm = DecryptionAlgorithms[0];
            DecryptionAlgorithmViewModel = DecryptionAlgorithmViewModels[0];
            decryptionAlgorithm.EncryptionChanged += (s, e) => OnPropertyChanged("DecryptedText");
        }

        void SetViewModel(IDecryptionAlgorithm decryptionAlgorithm) {
            var decryptionAlgorithmIndex = DecryptionAlgorithms.FindIndex(x => x == decryptionAlgorithm);
        }
    }
}
