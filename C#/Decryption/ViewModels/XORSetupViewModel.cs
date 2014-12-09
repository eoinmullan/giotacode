﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decryption.Common;
using Decryption.Interfaces;
using Decryption.Models;

namespace Decryption.ViewModels {
    public class XORSetupViewModel : ModelBase, IDecryptionAlgorithmViewModel {
        private readonly XORDecryptionAlgorithm algorithm;
        public ICommand FindKeyCommand { get; private set; }

        public XORSetupViewModel(XORDecryptionAlgorithm algorithm) {
            this.algorithm = algorithm;
            FindKeyCommand = new SimpleDelegateCommand(FindKey);
            algorithm.KeyChanged += HandleKeyChanged;
            KeyAutoSearchLowerBound = 97;
            KeyAutoSearchUpperBound = 122;
            WordsToFind = string.Empty;
        }

        void HandleKeyChanged(object sender, EventArgs e) {
            OnPropertyChanged("Key");
        }

        public string Key {
            get {
                return ASCIIEncoding.ASCII.GetString(algorithm.Key);
            }
            set {
                algorithm.Key = Encoding.ASCII.GetBytes(value);
                OnPropertyChanged("Key");
            }
        }

        public string Name {
            get {
                return algorithm.ToString();
            }
        }

        public byte KeyAutoSearchLowerBound { get; set; }
        public byte KeyAutoSearchUpperBound { get; set; }
        public string WordsToFind { get; set; }

        private void FindKey() {
            algorithm.FindKey(KeyAutoSearchLowerBound, KeyAutoSearchUpperBound, WordsToFind.Replace(" ", string.Empty).Split(','));
        }
    }
}