﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decryption.Interfaces {
    public interface ICaesarShiftDecrypter {
        int Shift { get; set; }
    }
}
