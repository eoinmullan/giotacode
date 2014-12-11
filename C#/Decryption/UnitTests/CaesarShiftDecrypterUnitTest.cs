using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Decryption.Models;

namespace DecryptionUnitTests {
    [TestClass]
    public class CaesarShiftDecrypterUnitTest {
        private CaesarShiftDecrypter target;

        [TestInitialize]
        public void Initialize() {
            target = new CaesarShiftDecrypter();
        }

        [TestMethod]
        public void ShouldHandleEmptyStringInput() {
            Assert.AreEqual("", target.DecryptText(""));
        }

        [TestMethod]
        public void ShouldHandleNullStringInput() {
            Assert.AreEqual("", target.DecryptText(null));
        }

        [TestMethod]
        public void ShouldReturnEncryptedTextUnchangedWhenNoKeyIsSet() {
            var testString = "The Quick Brown Fox Jumped Over The Lazy Dog.";

            Assert.AreEqual(testString, target.DecryptText(testString));
        }

        [TestMethod]
        public void ShouldShiftBy16CharactersWordsRespectingCase() {
            target.Shift = 16;
            var encryptedWords = "AesMu LbYgx PyH TeWZc yFob dro VkJI Nyq".Split(' ');
            var decryptedWords = "QuiCk BrOwn FoX JuMPs oVer the LaZY Dog".Split(' ');

            for (var i = 0; i < encryptedWords.Length; i++) {
                Assert.AreEqual(decryptedWords[i], target.DecryptText(encryptedWords[i]));
            }
        }

        [TestMethod]
        public void ShouldShiftMessageWhileLeavingPuntuationIntact() {
            target.Shift = 16;
            var encryptedSentence = "Aes.Mu L?bYgx P<yH> Te(WZc) yFob! d££ro V;k:JI N]y[q";
            var decryptedSentence = "Qui.Ck B?rOwn F<oX> Ju(MPs) oVer! t££he L;a:ZY D]o[g";

            Assert.AreEqual(decryptedSentence, target.DecryptText(encryptedSentence));
        }

        [TestMethod]
        public void ShouldUseKeySuppliedInConstructor() {
            var newTarget = new CaesarShiftDecrypter(16);
            var encryptedSentence = "AesMu LbYgx PyH TeWZc yFob dro VkJI Nyq";
            var decryptedSentence = "QuiCk BrOwn FoX JuMPs oVer the LaZY Dog";

            Assert.AreEqual(decryptedSentence, newTarget.DecryptText(encryptedSentence));
        }

        [TestMethod]
        public void ShouldReturnCorrectNameOnToString() {
            Assert.AreEqual("Caesar", target.ToString());
        }

        [TestMethod]
        public void ShouldRaiseEncryptionChangedWhenShiftValueIsChanged() {
            var encryptionChangedRaised = false;
            target.EncryptionChanged += (s, e) => encryptionChangedRaised = true;

            target.Shift = 4;

            Assert.IsTrue(encryptionChangedRaised);
        }
    }
}
