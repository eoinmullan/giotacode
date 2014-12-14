using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Decryption.Algorithms;
using Decryption.Models;

namespace DecryptionUnitTests {
    [TestClass]
    public class CaesarShiftAlgorithmUnitTest {

        [TestMethod]
        public void ShouldHandleEmptyStringInput() {
            Assert.AreEqual("", Algorithms.CaesarShiftDecryption(0, ""));
        }

        [TestMethod]
        public void ShouldHandleNullStringInput() {
            Assert.AreEqual("", Algorithms.CaesarShiftDecryption(0, null));
        }

        [TestMethod]
        public void ShouldReturnEncryptedTextUnchangedWhenZeroKeyIsUsed() {
            var testSentence = "The Quick Brown Fox Jumped Over The Lazy Dog.";

            Assert.AreEqual(testSentence, Algorithms.CaesarShiftDecryption(0, testSentence));
        }

        [TestMethod]
        public void ShouldShiftBy16CharactersWordsRespectingCase() {
            var encryptedSentence = "AesMu LbYgx PyH TeWZc yFob dro VkJI Nyq";
            var expectedDecryptedSentence = "QuiCk BrOwn FoX JuMPs oVer the LaZY Dog";

            Assert.AreEqual(expectedDecryptedSentence, Algorithms.CaesarShiftDecryption(16, encryptedSentence));
        }

        [TestMethod]
        public void ShouldShiftMessageWhileLeavingPuntuationIntact() {
            var encryptedSentence = "Aes.Mu L?bYgx P<yH> Te(WZc) yFob! d££ro V;k:JI N]y[q";
            var expectedDecryptedSentence = "Qui.Ck B?rOwn F<oX> Ju(MPs) oVer! t££he L;a:ZY D]o[g";

            Assert.AreEqual(expectedDecryptedSentence, Algorithms.CaesarShiftDecryption(16, encryptedSentence));
        }

        [TestMethod]
        public void ShouldUseKeySuppliedInConstructor() {
            var encryptedSentence = "AesMu LbYgx PyH TeWZc yFob dro VkJI Nyq";
            var expectedDecryptedSentence = "QuiCk BrOwn FoX JuMPs oVer the LaZY Dog";

            Assert.AreEqual(expectedDecryptedSentence, Algorithms.CaesarShiftDecryption(16, encryptedSentence));
        }
    }
}
