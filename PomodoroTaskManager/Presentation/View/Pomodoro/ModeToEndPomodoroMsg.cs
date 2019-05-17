using PomodoroTaskManager.DataTypeDef.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PomodoroTaskManager.Presentation.View.Pomodoro {

    [ValueConversion(typeof(Em_Mode), typeof(string))]
    class ModeToEndPomodoroMsg : IValueConverter {

        const string END_POMODORO_MSG = "End Pomodoro";
        const string END_BREAK_MSG = "End Break";
        const string END_LONG_BREAK_MSG = "End Long Break";


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Em_Mode emMode = (Em_Mode)value;
            switch (emMode) {
                case Em_Mode.Pomodoro:
                    return END_POMODORO_MSG;
                case Em_Mode.Break:
                    return END_BREAK_MSG;
                case Em_Mode.LongBreak:
                    return END_LONG_BREAK_MSG;
            }
            return "Select Action";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            string strMode = (string)value;
            if (strMode == END_POMODORO_MSG) {
                return Em_Mode.Pomodoro;
            } else if (strMode == END_BREAK_MSG) {
                return Em_Mode.Break;
            } else if (strMode == END_LONG_BREAK_MSG) {
                return Em_Mode.LongBreak;
            }
            return Em_Mode.Stop;
        }
    }
}