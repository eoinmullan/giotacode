﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Decryption.Interfaces {
    public interface IText : INotifyPropertyChanged {
        string Text { get; set; }
    }
}
