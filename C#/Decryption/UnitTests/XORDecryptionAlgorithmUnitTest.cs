using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Decryption.Models;
using System.Linq;
using System.Diagnostics;

namespace DecryptionUnitTests {
    [TestClass]
    public class XORDecryptionAlgorithmUnitTest {
        private XORDecryptionAlgorithm target;

        [TestInitialize]
        public void Initialize() {
            target = new XORDecryptionAlgorithm();
        }

        [TestMethod]
        public void ShouldConvertASCIICodesToCharactersWhenKeyIsNotSet() {
            var upperCaseASCIICodes = Enumerable.Range(65, 26);
            var upperCaseAlphabet = String.Concat(upperCaseASCIICodes.Select(x => (char)x));
            var commaSeperatedUpperCaseASCIICodes = string.Join(",", upperCaseASCIICodes);
            Assert.AreEqual(upperCaseAlphabet, target.DecryptText(commaSeperatedUpperCaseASCIICodes));

            var lowerCaseASCIICodes = Enumerable.Range(65, 26);
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
            target.Key = new byte[] {42};

            Assert.AreEqual("kkkkk", target.DecryptText("65,65,65,65,65"));
        }

        [TestMethod]
        public void ShouldConvert107ToAWhenKeyIs42() {
            target.Key = new byte[] { 42 };

            Assert.AreEqual("AAAAA", target.DecryptText("107,107,107,107,107"));
        }

        [TestMethod]
        public void ShouldRepeatKeyCyclicallyThroughoutMessage() {
            target.Key = new byte[] { 23, 32, 42, 200, 219 };

            Assert.AreEqual("Secret Message", target.DecryptText("68,69,73,186,190,99,0,103,173,168,100,65,77,173"));
        }

        [TestMethod]
        public void ShouldUseKeySuppliedInConstructor() {
            var newTarget = new XORDecryptionAlgorithm(new byte[] { 23, 32, 42, 200, 219 });

            Assert.AreEqual("Secret Message", newTarget.DecryptText("68,69,73,186,190,99,0,103,173,168,100,65,77,173"));
        }
    }
}
