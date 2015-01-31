using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Common;
using Decryption.Interfaces;

namespace Decryption.ViewModels {
    class MonoAlphaSetupViewModel : ModelBase, IDecryptionSetupViewModel {
        private readonly IDecrypter decrypter;

        public string Name {
            get {
                return decrypter.ToString();
            }
        }

        public MonoAlphaSetupViewModel(IDecrypter decrypter) {
            this.decrypter = decrypter;
        }
    }
}
