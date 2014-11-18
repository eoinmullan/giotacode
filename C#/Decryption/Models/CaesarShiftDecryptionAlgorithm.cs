using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Interfaces;
using System.ComponentModel;

namespace Decryption.Models {
    public class CaesarShiftDecryptionAlgorithm : IDecryptionAlgorithm {
        public event EventHandler EncryptionChanged;

        private int key;
        public int Key {
            get {
                return key;
            }
            set {
                key = value;
                if (EncryptionChanged != null) {
                    EncryptionChanged(this, new EventArgs());
                }
            }
        }

        public CaesarShiftDecryptionAlgorithm() : this(0) { 
        }

        public CaesarShiftDecryptionAlgorithm(int key) {
            Key = key;
        }

        public string DecryptText(string encryptedText) {
            return String.Concat(encryptedText.Select(x => ShiftCharacter(x)));
        }

        private char ShiftCharacter(char character) {
            if (char.IsUpper(character)) {
                return (char)(((character - 'A' + Key) % 26) + 'A');
            }
            else if (char.IsLower(character)) {
                return (char)(((character - 'a' + Key) % 26) + 'a');
            }

            return character;
        }

        public override string ToString() {
            return "Caesar";
        }
    }
}
