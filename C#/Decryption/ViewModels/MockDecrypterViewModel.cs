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

        public MockDecrypterViewModel() {
            decrypters = new List<IDecrypter>() {
                new CaesarShiftDecrypter(),
                new XORDecrypter(null, null, null)
            };
        }
    }
}
