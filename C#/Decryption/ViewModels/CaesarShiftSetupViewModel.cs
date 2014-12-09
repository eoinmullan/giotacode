﻿using System;
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
        private IEncryptedText encryptedText;
        public ICommand ShiftUpCommand { get; private set; }
        public ICommand ShiftDownCommand { get; private set; }
        public ICommand LoadSampleTextCommand { get; private set; }

        public CaesarShiftSetupViewModel(CaesarShiftDecryptionAlgorithm algorithm, IEncryptedText encryptedText) {
            this.algorithm = algorithm;
            this.encryptedText = encryptedText;
            ShiftUpCommand = new SimpleDelegateCommand(() => Shift++);
            ShiftDownCommand = new SimpleDelegateCommand(() => Shift--);
            LoadSampleTextCommand = new SimpleDelegateCommand(LoadSampleText);
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

        public string Name {
            get {
                return algorithm.ToString();
            }
        }

        private void LoadSampleText() {
            encryptedText.Text = Properties.Resources.SampleCaesarText;
        }
    }
}
