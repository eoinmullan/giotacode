using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Decryption.ViewModels {
    internal class MockMonoAlphaSetupViewModel {
        private string encryptedText = "the quick brown fox jumped over the lazy dog";

        public ObservableCollection<char> InputCharacterSet {
            get {
                return new ObservableCollection<char>(encryptedText.Distinct());
            }
        }
    }
}
