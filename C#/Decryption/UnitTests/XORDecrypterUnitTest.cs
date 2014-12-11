using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Decryption.Interfaces;
using Decryption.Models;
using Rhino.Mocks;

namespace DecryptionUnitTests {
    [TestClass]
    public class XORDecrypterUnitTest {
        private XORDecrypter target;

        private IObservableText encryptedText;
        private ITextHelper textChecker;
        private IXORKeyFinderFactory xorKeyFinderFactory;

        [TestInitialize]
        public void Initialize() {
            encryptedText = MockRepository.GenerateMock<IObservableText>();
            textChecker = MockRepository.GenerateMock<ITextHelper>();
            xorKeyFinderFactory = MockRepository.GenerateMock<IXORKeyFinderFactory>();

            target = new XORDecrypter(encryptedText, textChecker, xorKeyFinderFactory);
        }

        [TestMethod]
        public void ShouldConvertASCIICodesToUpperCaseCharactersWhenKeyIsNotSet() {
            var upperCaseASCIICodes = Enumerable.Range(65, 26);
            var upperCaseAlphabet = String.Concat(upperCaseASCIICodes.Select(x => (char)x));
            var commaSeperatedUpperCaseASCIICodes = string.Join(",", upperCaseASCIICodes);
            Assert.AreEqual(upperCaseAlphabet, target.DecryptText(commaSeperatedUpperCaseASCIICodes));
        }

        [TestMethod]
        public void ShouldConvertASCIICodesToLowerCaseCharactersWhenKeyIsNotSet() {
            var lowerCaseASCIICodes = Enumerable.Range(97, 122);
            var lowerCaseAlphabet = String.Concat(lowerCaseASCIICodes.Select(x => (char)x));
            var commaSeperatedLowerCaseASCIICodes = string.Join(",", lowerCaseASCIICodes);
            Assert.AreEqual(lowerCaseAlphabet, target.DecryptText(commaSeperatedLowerCaseASCIICodes));
        }

        [TestMethod]
        public void ShouldHandleEmptyString() {
            Assert.AreEqual("", target.DecryptText(""));
        }

        [TestMethod]
        public void ShouldConvert65TokWhenKeyIs42() {
            SetTargetKey(42);

            Assert.AreEqual("kkkkk", target.DecryptText("65,65,65,65,65"));
        }

        [TestMethod]
        public void ShouldConvert107ToAWhenKeyIs42() {
            SetTargetKey(42);

            Assert.AreEqual("AAAAA", target.DecryptText("107,107,107,107,107"));
        }

        [TestMethod]
        public void ShouldRepeatKeyCyclicallyThroughoutMessage() {
            SetTargetKey(23, 32, 42, 200, 219);

            Assert.AreEqual("Secret Message", target.DecryptText("68,69,73,186,190,99,0,103,173,168,100,65,77,173"));
        }

        [TestMethod]
        public void ShouldUseKeySuppliedInConstructor() {
            var newTarget = new XORDecrypter(encryptedText, textChecker, xorKeyFinderFactory, new byte[] { 23, 32, 42, 200, 219 });

            Assert.AreEqual("Secret Message", newTarget.DecryptText("68,69,73,186,190,99,0,103,173,168,100,65,77,173"));
        }

        [TestMethod]
        public void ShouldReturnCorrectNameOnToString() {
            Assert.AreEqual("XOR", target.ToString());
        }

        [TestMethod]
        public void ShouldReturnErrorMessageWhenInputTextIsNotWellFormed() {
            SetTargetKey(42, 43, 44);

            Assert.AreEqual(Decryption.Properties.Resources.InvalidInput, target.DecryptText("Non ASCII code message"));
            Assert.AreEqual(Decryption.Properties.Resources.InvalidInput, target.DecryptText("107,a107,107,107,107"));
        }

        [TestMethod]
        public void ShouldRaiseEncryptionAndKeyChangedWhenKeyChanged() {
            var encryptionChangedRaised = false;
            var keyChangedRaised = false;
            target.EncryptionChanged += (s, e) => encryptionChangedRaised = true;
            target.KeyChanged += (s, e) => keyChangedRaised = true;

            SetTargetKey(42, 43, 44);

            Assert.IsTrue(encryptionChangedRaised);
            Assert.IsTrue(keyChangedRaised);
        }

        private void SetTargetKey(params byte[] keyValues) {
            target.Key = keyValues;
        }
    }
}
