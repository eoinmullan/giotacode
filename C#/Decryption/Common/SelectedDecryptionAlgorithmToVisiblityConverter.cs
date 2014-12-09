using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Diagnostics;

namespace Decryption.Common {
    public class SelectedDecryptionAlgorithmToVisiblityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Debug.WriteLine("Value: " + value as string + ". Parameter: " + parameter as string + ".");
            return value as string == parameter as string ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return DependencyProperty.UnsetValue;
        }
    }
}
