﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DecryptionUnitTests")]

namespace Decryption.Models {
    internal class EncryptedText : IEncryptedText {
        public string Text { get; set; }
    }
}
