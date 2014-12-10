using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Interfaces;
using Decryption.Models;

namespace Decryption.Common {
    public class XORKeyFinderFactory : IXORKeyFinderFactory {
        public IXORKeyFinder Create(IText encryptedText, ITextHelper textChecker, byte lowerKeyBound, byte upperKeyBound, params string[] wordsToFind) {
            return new XORKeyFinder(encryptedText, textChecker, lowerKeyBound, upperKeyBound, wordsToFind);
        }
    }
}
