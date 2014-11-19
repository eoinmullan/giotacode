using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Decryption.Interfaces;
using Decryption.ViewModels;

namespace Decryption.Models {
    public class XORDecryptionAlgorithm : ModelBase, IDecryptionAlgorithm {
        public event EventHandler EncryptionChanged;
        public event EventHandler KeyChanged;
        private IEncryptedText encryptedText;
        private byte[] ByteArrayForError = new byte[5] { 69, 114, 114, 111, 114 };

        private byte[] key;
        public byte[] Key {
            get {
                return key;
            }
            set {
                key = value;
                RaiseKeyChanged();
                if (EncryptionChanged != null) {
                    EncryptionChanged(this, new EventArgs());
                }
            }
        }

        public XORDecryptionAlgorithm(IEncryptedText encryptedText) :
            this(encryptedText, 0) {
        }

        public XORDecryptionAlgorithm(IEncryptedText encryptedText, params byte[] key) {
            this.encryptedText = encryptedText;
            Key = key;
        }

        public string DecryptText(string encryptedText) {
            if (encryptedText == null || encryptedText.Equals(string.Empty) || Key.Length == 0) {
                return string.Empty;
            }

            return String.Concat(encryptedText.Split(',').Select((x, i) => (char)(Int32.Parse(x) ^ Key[i % Key.Length])));
        }

        public override string ToString() {
            return "XOR";
        }

        public void FindKey() {
            Task.Factory.StartNew(() => {
                if (!KeyHasThreeLowerCaseCharacters()) {
                    Key = ByteArrayForError;
                    return;
                }

                IncrementKey();
                while (!DecryptText(encryptedText.Text).Contains("uler")) {
                    IncrementKey();
                }
            });
        }

        private bool KeyHasThreeLowerCaseCharacters() {
            if (Key.Length != 3) {
                return false;
            }

            return Key.All(x => x >= 97 && x <= 122);
        }

        private void IncrementKey() {
            if (Key[2] < 122) {
                Key[2]++;
            }
            else {
                Key[2] = 97;
                if (Key[1] < 122) {
                    Key[1]++;
                }
                else {
                    Key[1] = 97;
                    Key[0]++;
                }
            }
            Key = key;
        }

        private void RaiseKeyChanged() {
            if (KeyChanged != null) {
                KeyChanged(this, new EventArgs());
            }
        }
    }
}
