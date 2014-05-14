using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decryption.Interfaces {
    internal interface IDecryptionAlgorithm {
        string DecryptText(string encryptedText);
    }
}
