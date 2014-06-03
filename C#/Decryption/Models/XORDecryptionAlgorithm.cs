using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Interfaces;

namespace Decryption.Models {
    internal class XORDecryptionAlgorithm : IDecryptionAlgorithm {
        public byte[] Key { get; set; }

        public XORDecryptionAlgorithm() : this(0) {
        }

        public XORDecryptionAlgorithm(params byte[] key) {
            Key = key;
        }

        public string DecryptText(string encryptedText) {
            if (encryptedText.Equals(string.Empty)) {
                return string.Empty;
            }

            return String.Concat(encryptedText.Split(',').Select((x, i) => (char)(Int32.Parse(x) ^ Key[i % Key.Length])));
        }

        public override string ToString() {
            return "XOR";
        }
    }
}
