﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decryption.Interfaces {
    public interface IXORKeyFinderFactory {
        IXORKeyFinder Create(IEncryptedText encryptedText, ITextChecker textChecker, byte lowerKeyBound, byte upperKeyBound, params string[] wordsToFind);
    }
}
