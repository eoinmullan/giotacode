using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Decryption.Common;
using Decryption.Interfaces;
using Decryption.Models;

namespace Decryption.ViewModels {
    public class CaesarShiftSetupViewModel : ModelBase, IDecryptionAlgorithmViewModel {
        private CaesarShiftDecryptionAlgorithm algorithm;
        public ICommand ShiftUpCommand { get; private set; }
        public ICommand ShiftDownCommand { get; private set; }

        public CaesarShiftSetupViewModel(CaesarShiftDecryptionAlgorithm algorithm) {
            this.algorithm = algorithm;
            ShiftUpCommand = new SimpleDelegateCommand(() => Shift++);
            ShiftDownCommand = new SimpleDelegateCommand(() => Shift--);
        }

        public int Shift {
            get {
                return algorithm.Key;
            }
            set {
                algorithm.Key = value;
                OnPropertyChanged("Shift");
            }
        }
    }
}
