using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Common;
using Decryption.Interfaces;

namespace Decryption.Models {
    public class EncryptedText : ModelBase, IEncryptedText {
        private string text;
        public string Text {
            get {
                return text;
            }
            set {
                text = value;
                OnPropertyChanged("Text");
            }
        }
    }
}
