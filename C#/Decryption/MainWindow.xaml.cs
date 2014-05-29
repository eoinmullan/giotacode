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
using Decryption.ViewModels;
using Decryption.Models;

namespace Decryption {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            var caesarShiftAlgorithm = new CaesarShiftDecryptionAlgorithm();
            var xorAlgorithm = new XORDecryptionAlgorithm();
            var monoAlphaAlgorithm = new MonoAlphaSubDecryptionAlgorithm();

            DataContext = new DecrypterViewModel(caesarShiftAlgorithm, xorAlgorithm, monoAlphaAlgorithm);
        }
    }

    public class EnumToBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
