using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Decryption.Models;

namespace DecryptionUnitTests {
    [TestClass]
    public class EncryptedTextUnitTest {
        EncryptedText target;

        [TestInitialize]
        public void Initialize() {
            target = new EncryptedText();
        }

        [TestMethod]
        public void ShouldSetAndGetText() {
            target.Text = "Some test text";

            Assert.AreEqual("Some test text", target.Text);
        }
    }
}
