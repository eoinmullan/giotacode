using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decryption.Algorithms {
    public static class Algorithms {
        public static string CaesarShiftDecryption(int shift, string encryptedText) {
            if (encryptedText == null || encryptedText.Equals(string.Empty)) {
                return string.Empty;
            }

            return String.Concat(encryptedText.Select(x => CaesarShiftCharacter(shift, x)));
        }

        private static char CaesarShiftCharacter(int shift, char character) {
            if (char.IsUpper(character)) {
                return (char)(((character - 'A' + shift) % 26) + 'A');
            }
            else if (char.IsLower(character)) {
                return (char)(((character - 'a' + shift) % 26) + 'a');
            }

            return character;
        }
    }
}
