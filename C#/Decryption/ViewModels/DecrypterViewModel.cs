using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Interfaces;
using System.ComponentModel;
using System.Windows.Data;
using Decryption;
using Decryption.Common;

namespace Decryption.ViewModels {
    internal class DecrypterViewModel : ModelBase {
        public event PropertyChangedEventHandler PropertyChanged;

        private IEncryptedText encryptedText;
        public string EncryptedText {
            get {
                return encryptedText.Text;
            }
            set {
                encryptedText.Text = value;
                OnPropertyChanged("EncryptedText");
                OnPropertyChanged("DecryptedText");
            }
        }

        public string DecryptedText {
            get {
                return DecryptionAlgorithm.DecryptText(EncryptedText);
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
                decryptionAlgorithm.EncryptionChanged -= HandleEncryptionChanged;
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
                OnPropertyChanged("DecryptedText");
            }
        }

        public string ActiveAlgorithmName {
            get {
                return DecryptionAlgorithm.ToString();
            }
        }

        public DecrypterViewModel(IEncryptedText encryptedText, params Tuple<IDecryptionAlgorithm, IDecryptionAlgorithmViewModel>[] decryptionAlgorithmsVMPairs) {
            this.encryptedText = encryptedText;
            DecryptionAlgorithms = new List<IDecryptionAlgorithm>();
            DecryptionAlgorithmViewModels = new List<IDecryptionAlgorithmViewModel>();
            foreach (var decryptionAlgorithmVMPair in decryptionAlgorithmsVMPairs) {
                DecryptionAlgorithms.Add(decryptionAlgorithmVMPair.Item1);
                DecryptionAlgorithmViewModels.Add(decryptionAlgorithmVMPair.Item2);
            }

            encryptedText.PropertyChanged += encryptedText_PropertyChanged;
            decryptionAlgorithm = DecryptionAlgorithms[0];
            decryptionAlgorithmViewModel = DecryptionAlgorithmViewModels[0];
            decryptionAlgorithm.EncryptionChanged += HandleEncryptionChanged;
        }

        private void encryptedText_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == "Text") {
                OnPropertyChanged("EncryptedText");
            }
        }

        private void SetViewModel(IDecryptionAlgorithm decryptionAlgorithm) {
            var decryptionAlgorithmIndex = DecryptionAlgorithms.FindIndex(x => x == decryptionAlgorithm);
            DecryptionAlgorithmViewModel = DecryptionAlgorithmViewModels[decryptionAlgorithmIndex];
            DecryptionAlgorithm.EncryptionChanged += HandleEncryptionChanged;
        }

        private void HandleEncryptionChanged(object sender, EventArgs e) {
            OnPropertyChanged("DecryptedText");
        }
    }
}
