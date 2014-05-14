using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Interfaces;

namespace Decryption.Models {
    internal class MonoAlphaSubDecryptionAlgorithm : IDecryptionAlgorithm {
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
        }

        public string DecryptText(string encryptedText) {
            return String.Concat(encryptedText.Select(x => ReplaceOrQuestionMark(x)));
        }

        private char ReplaceOrQuestionMark(char c) {
            if (pairs.ContainsKey(c)) {
                return pairs[c];
            }

            return '?';
        }
    }
}
