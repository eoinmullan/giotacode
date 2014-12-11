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

            IObservableText encryptedText = new ObservableText();
            IDecryptedText decryptedText = new DecryptedText(encryptedText);
            ITextHelper textChecker = new TextHelper();
            IXORKeyFinderFactory xorKeyFinderFactory = new XORKeyFinderFactory();
            IDecrypter caesarShiftAlgorithm = new CaesarShiftDecrypter();
            IDecryptionSetupViewModel caesarShiftViemModel = new CaesarShiftSetupViewModel(caesarShiftAlgorithm as ICaesarShiftDecrypter, encryptedText);
            IDecrypter xorAlgorithm = new XORDecrypter(encryptedText, textChecker, xorKeyFinderFactory);
            IDecryptionSetupViewModel xorViewModel = new XORSetupViewModel(xorAlgorithm as IXORDecrypter, encryptedText);
            IDecrypter monoAlphaAlgorithm = new MonoAlphaSubDecrypter();

            DataContext = new DecrypterViewModel(
                encryptedText,
                decryptedText,
                Tuple.Create(caesarShiftAlgorithm, caesarShiftViemModel),
                Tuple.Create(xorAlgorithm, xorViewModel));
        }
    }
}
