using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Interfaces;
using Decryption.Models;

namespace Decryption.ViewModels {
    internal class MockDecrypterViewModel {
        private List<IDecrypter> decrypters;
        public List<IDecrypter> Decrypters {
            get {
                return decrypters;
            }
        }

        private IXORDecrypter xorDecrypter;
        private IDecryptionSetupViewModel xorDecrypterViewModel;
        public IDecryptionSetupViewModel CurrentDecrypterViewModel {
            get {
                return xorDecrypterViewModel;
            }
            set { }
        }

        public MockDecrypterViewModel() {
            xorDecrypter = new XORDecrypter(null, null, null);
            xorDecrypterViewModel = new XORSetupViewModel(xorDecrypter, null);
            decrypters = new List<IDecrypter>() {
                new CaesarShiftDecrypter(),
                xorDecrypter
            };
        }
    }
}
