using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Interfaces;

namespace Decryption.Models {
    public class XORKeyFinder : IXORKeyFinder {
        private readonly IEncryptedText encryptedText;
        private readonly ITextChecker textChecker;
        private readonly byte lowerKeyBound;
        private readonly byte upperKeyBound;
        private readonly string[] wordsToFind;

        public XORKeyFinder (IEncryptedText encryptedText, ITextChecker textChecker, byte lowerKeyBound, byte upperKeyBound, params string[] wordsToFind) {
            this.encryptedText = encryptedText;
            this.textChecker = textChecker;
            this.lowerKeyBound = lowerKeyBound;
            this.upperKeyBound = upperKeyBound;
            this.wordsToFind = wordsToFind;
	    }

        public async Task<byte[]> FindNextKeyAsync(byte[] key, Func<string, string> decrypt, Action<byte[]> updateKeyTried) {
            await Task.Run(() => {
                if (!KeyIsWithinAutoSearchBounds(key)) {
                    return;
                }

                IncrementKey(ref key, updateKeyTried);
                var test = decrypt(encryptedText.Text);
                while (!textChecker.ContainsAll(decrypt(encryptedText.Text), wordsToFind)) {
                    IncrementKey(ref key, updateKeyTried);
                }
            });

            return key;
        }

        private bool KeyIsWithinAutoSearchBounds(byte[] key) {
            return key.Count() > 0 && key.All(x => x >= lowerKeyBound && x <= upperKeyBound);
        }

        private void IncrementKey(ref byte[] key, Action<byte[]> updateKeyTried) {
            for (var i = key.Count()-1; i >= 0; i--) {
                if (key[i] < upperKeyBound) {
                    key[i]++;
                    break;
                }
                else {
                    key[i] = lowerKeyBound;
                }
            }

            updateKeyTried(key);
        }
    }
}
