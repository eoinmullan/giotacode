using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using Decryption.Interfaces;
using Decryption.ViewModels;

namespace Decryption.Models {
    public class XORDecryptionAlgorithm : ModelBase, IDecryptionAlgorithm {
        public event EventHandler EncryptionChanged;
        public event EventHandler KeyChanged;
        private readonly IEncryptedText encryptedText;
        private readonly ITextChecker textChecker;
        private readonly IXORKeyFinderFactory xorKeyFinderFactory;

        private byte LowerKeyBound { get; set; }
        private byte UpperKeyBound { get; set; }

        private byte[] key;
        public byte[] Key {
            get {
                return key;
            }
            set {
                if (value.Count() == 0) {
                    key = new byte[] { 0 };
                }
                else {
                    key = value;
                }

                RaiseKeyChanged();
                RaiseEncryptionChanged();
            }
        }

        public XORDecryptionAlgorithm(
            IEncryptedText encryptedText,
            ITextChecker textChecker,
            IXORKeyFinderFactory xorKeyFinderFactory,
            params byte[] key) {
            this.encryptedText = encryptedText;
            this.textChecker = textChecker;
            this.xorKeyFinderFactory = xorKeyFinderFactory;
            Key = key;
        }

        public string DecryptText(string encryptedText) {
            if (encryptedText == null || encryptedText.Equals(string.Empty)) {
                return string.Empty;
            }

            if (!IsEncryptedTextValid(encryptedText)) {
                return Properties.Resources.InvalidInput;
            }

            return String.Concat(encryptedText.Split(',').Where(x => !x.Equals(string.Empty)).Select((x, i) => (char)(Int32.Parse(x) ^ Key[i % Key.Length])));
        }

        public override string ToString() {
            return "XOR";
        }

        public async void FindKey(byte lowerBound, byte upperBound, params string[] wordsToFind) {
            var xorKeyFinder = xorKeyFinderFactory.Create(encryptedText, textChecker, lowerBound, upperBound, wordsToFind);
            Key = await xorKeyFinder.FindNextKeyAsync(key, DecryptText, x => Key = x);
        }

        private bool IsEncryptedTextValid(string text) {
            return text.All(x => Char.IsDigit(x) || x == ',');
        }

        private void RaiseKeyChanged() {
            if (KeyChanged != null) {
                KeyChanged(this, new EventArgs());
            }
        }

        private void RaiseEncryptionChanged() {
            if (EncryptionChanged != null) {
                EncryptionChanged(this, new EventArgs());
            }
        }
    }
}
