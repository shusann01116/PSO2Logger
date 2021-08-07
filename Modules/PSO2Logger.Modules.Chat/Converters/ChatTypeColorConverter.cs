using PSO2Logger.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PSO2Logger.Modules.Chat.Converters {
    class ChatTypeColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value switch {
                ChatType.PUBLIC => Color.White.Name,
                ChatType.PARTY => "#53A2BA",
                ChatType.GUILD => "#CECA66",
                ChatType.REPLY => "#BA536E",
                ChatType.GROUP => "#53BA85",
                _ => Color.White.ToString(),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
