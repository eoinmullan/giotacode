using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Decryption.Interfaces;
using Decryption.Models;
using Decryption.Common;
using Decryption.ViewModels;

namespace Decryption {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            IText encryptedText = new EncryptedText();
            ITextHelper textChecker = new TextHelper();
            IXORKeyFinderFactory xorKeyFinderFactory = new XORKeyFinderFactory();
            IDecryptionAlgorithm caesarShiftAlgorithm = new CaesarShiftDecryptionAlgorithm();
            IDecryptionAlgorithmViewModel caesarShiftViemModel = new CaesarShiftSetupViewModel(caesarShiftAlgorithm as CaesarShiftDecryptionAlgorithm, encryptedText);
            IDecryptionAlgorithm xorAlgorithm = new XORDecryptionAlgorithm(encryptedText, textChecker, xorKeyFinderFactory);
            IDecryptionAlgorithmViewModel xorViewModel = new XORSetupViewModel(xorAlgorithm as XORDecryptionAlgorithm, encryptedText);
            IDecryptionAlgorithm monoAlphaAlgorithm = new MonoAlphaSubDecryptionAlgorithm();

            DataContext = new DecrypterViewModel(
                encryptedText,
                Tuple.Create(caesarShiftAlgorithm, caesarShiftViemModel),
                Tuple.Create(xorAlgorithm, xorViewModel));
        }
    }
}
