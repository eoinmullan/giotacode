using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Interfaces;
using System.Runtime.CompilerServices;

namespace Decryption.Models {
    public class EncryptedText : IEncryptedText {
        public string Text { get; set; }
    }
}
