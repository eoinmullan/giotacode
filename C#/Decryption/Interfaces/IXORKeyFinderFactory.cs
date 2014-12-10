using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decryption.Interfaces {
    public interface IXORKeyFinderFactory {
        IXORKeyFinder Create(IText encryptedText, ITextHelper textChecker, byte lowerKeyBound, byte upperKeyBound, params string[] wordsToFind);
    }
}
