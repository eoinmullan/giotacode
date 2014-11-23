using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decryption.Interfaces {
    public interface ITextChecker {
        bool ContainsAll(string inputText, params string[] wordsToCheck);
    }
}
