using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Decryption.Models;

namespace DecryptionUnitTests {
    [TestClass]
    public class MonoAlphaSubDecrypterUnitTest {
        private MonoAlphaSubDecrypter target;

        [TestInitialize]
        public void Initialize() {
            target = new MonoAlphaSubDecrypter();
        }

        [TestMethod]
        public void ShouldHandleEmptyString() {
            Assert.AreEqual("", target.DecryptText(""));
        }

        [TestMethod]
        public void ShouldReturnAllQuestionMarksWhenNoKeyIsSet() {
            Assert.AreEqual("??????????", target.DecryptText("a12BC4./9y"));
        }

        [TestMethod]
        public void ShouldReplaceCharactersThatHaveBeenSpecified() {
            target.LoadSubstitutionPair('A', 'Q');
            target.LoadSubstitutionPair('e', 'u');
            target.LoadSubstitutionPair('.', '.');

            Assert.AreEqual("Qu?.??", target.DecryptText("Aes.Mu"));
        }

        [TestMethod]
        public void ShouldAllowSubstitutionPairsToBeReplaced() {
            target.LoadSubstitutionPair('A', 'Q');
            target.LoadSubstitutionPair('e', 'G');
            target.LoadSubstitutionPair('e', 'u');

            Assert.AreEqual("Qu????", target.DecryptText("Aes.Mu"));
        }

        [TestMethod]
        public void ShouldNotReplaceCharactersThatHaveJustBeenSubstitutedIn() {
            target.LoadSubstitutionPair('a', 'b');
            target.LoadSubstitutionPair('b', 'c');

            Assert.AreEqual("bc?", target.DecryptText("abc"));
            Assert.AreEqual("?cb", target.DecryptText("cba"));
        }

        [TestMethod]
        public void ShouldReturnCorrectNameOnToString() {
            Assert.AreEqual("MonoAlpha", target.ToString());
        }
        
        [TestMethod]
        public void ShouldRaiseEncryptionChangedWhenSubstitutionPairLoaded() {
            var encryptionChangedRaised = false;
            target.EncryptionChanged += (s, e) => encryptionChangedRaised = true;

            target.LoadSubstitutionPair('a', 'b');

            Assert.IsTrue(encryptionChangedRaised);
        }
    }
}
