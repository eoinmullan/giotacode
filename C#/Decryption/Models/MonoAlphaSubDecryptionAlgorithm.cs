using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Interfaces;

namespace Decryption.Models {
    internal class MonoAlphaSubDecryptionAlgorithm : IDecryptionAlgorithm {
        public event EventHandler EncryptionChanged;

        IDictionary<char, char> pairs;

        public MonoAlphaSubDecryptionAlgorithm() {
            pairs = new Dictionary<char, char>();
        }

        public void LoadSubstitutionPair(char find, char replace) {
            if (pairs.ContainsKey(find)) {
                pairs[find] = replace;
            }
            else {
                pairs.Add(find, replace);
            }

            RaiseEncryptionChanged();
        }

        public string DecryptText(string encryptedText) {
            return String.Concat(encryptedText.Select(x => SubstuteOrReplaceWithQuestionMark(x)));
        }

        private char SubstuteOrReplaceWithQuestionMark(char c) {
            if (pairs.ContainsKey(c)) {
                return pairs[c];
            }

            return '?';
        }

        public override string ToString() {
            return "MonoAlpha";
        }

        private void RaiseEncryptionChanged() {
            if (EncryptionChanged != null) {
                EncryptionChanged(this, new EventArgs());
            }
        }
    }
}
