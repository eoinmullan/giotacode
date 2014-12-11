using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Interfaces;
using System.ComponentModel;

namespace Decryption.Models {
    internal class CaesarShiftDecrypter : DecrypterBase, ICaesarShiftDecrypter {
        private int shift;
        public int Shift {
            get {
                return shift;
            }
            set {
                shift = value;
                RaiseEncryptionChanged();
            }
        }

        public CaesarShiftDecrypter() : this(0) { 
        }

        public CaesarShiftDecrypter(int key) {
            Shift = key;
        }

        public override string DecryptText(string encryptedText) {
            if (encryptedText == null || encryptedText.Equals(string.Empty)) {
                return string.Empty;
            }

            return String.Concat(encryptedText.Select(x => ShiftCharacter(x)));
        }

        private char ShiftCharacter(char character) {
            if (char.IsUpper(character)) {
                return (char)(((character - 'A' + Shift) % 26) + 'A');
            }
            else if (char.IsLower(character)) {
                return (char)(((character - 'a' + Shift) % 26) + 'a');
            }

            return character;
        }

        public override string ToString() {
            return "Caesar";
        }
    }
}
