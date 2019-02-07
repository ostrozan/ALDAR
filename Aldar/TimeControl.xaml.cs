using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Aldar
{
    [Serializable]
    /// <summary>
    /// Interakční logika pro TimeControl.xaml
    /// </summary>
    public partial class TimePicker : UserControl,INotifyPropertyChanged
    {
        private string hod;
        public string Hod
        {
            get { return hod; }
            set
            {
                if (hod != value)
                {
                    hod = value;
                    NotifyPropertyChanged("Hod");
                }
            }
        }
        private string min;
        public string Min
        {
            get { return min; }
            set
            {
                if (min != value)
                {
                    min = value;
                    NotifyPropertyChanged("Min");
                }
            }
        }
        private string sec;
        public string Sec
        {
            get { return sec; }
            set
            {
                if (sec != value)
                {
                    sec = value;
                    NotifyPropertyChanged("Sec");
                }
            }
        }

        public int _Hod, _Min, _Sec;
        bool focHod, focMin, focSec;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public TimePicker()
        {
            InitializeComponent();
        }

        public TimePicker( int minutes)
        {
            InitializeComponent();
            Hod = (minutes / 60).ToString().PadLeft(2, '0');
            Min = (minutes % 60).ToString().PadLeft(2, '0');
            //tbHod.Text = Hod.ToString().PadLeft(2, '0');
            //tbMin.Text = Min.ToString().PadLeft(2, '0');

        }

        private void tbHod_GotFocus(object sender, RoutedEventArgs e)
        {
            focHod = true;
            focMin = false;
            focSec = false;
            //tbHod.Background = Brushes.Aqua;
            //tbMin.Background = Brushes.White;
            //tbSec.Background = Brushes.White;
        }
        private void tbMin_GotFocus(object sender, RoutedEventArgs e)
        {
            focHod = false;
            focMin = true;
            focSec = false;
            //tbHod.Background = Brushes.White;
            //tbMin.Background = Brushes.Aqua;
            //tbSec.Background = Brushes.White;
        }

        private void tbSec_GotFocus(object sender, RoutedEventArgs e)
        {
            focHod = false;
            focMin = false;
            focSec = true;
            //tbHod.Background = Brushes.White;
            //tbMin.Background = Brushes.White;
            //tbSec.Background = Brushes.Aqua;
        }

        private void tbHod_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up) UpCount();
            if (e.Key == Key.Down) DnCount();
        }

        private void tbMin_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up) UpCount();
            if (e.Key == Key.Down) DnCount();
        }

        private void tbSec_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up) UpCount();
            if (e.Key == Key.Down) DnCount();
        }

        private void DnCount()
        {
            if (focHod)
            {
                if (--_Hod < 0) _Hod = 23;
                tbHod.Text = _Hod.ToString().PadLeft(2, '0');
            }
            if (focMin)
            {
                if (--_Min < 0) _Min = 59;
                tbMin.Text = _Min.ToString().PadLeft(2, '0');
            }
            if (focSec)
            {
                if (--_Sec < 0) _Sec = 59;
                tbSec.Text = _Sec.ToString().PadLeft(2, '0');
            }
        }      

        private void UpCount()
        {
            if (focHod)
            {
                if (++_Hod > 23) _Hod = 0;
                tbHod.Text = _Hod.ToString().PadLeft(2, '0');
            }
            if (focMin)
            {
                if (++_Min > 50) _Min = 0;
                tbMin.Text = _Min.ToString().PadLeft(2, '0');
            }
            if (focSec)
            {
                if (++_Sec > 59) _Sec = 0;
                tbSec.Text = _Sec.ToString().PadLeft(2, '0');
            }
        }

        private void tbHod_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (int.TryParse(tbHod.Text,out int x)&&x > 23)
            {
                tbHod.Text = "23";
                MessageBox.Show("den má jen 24 hodin");
            }
            else
            {
                _Hod = x;
                tbHod.Text.PadLeft(2, '0');
            }
        }

        private void tbMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(tbMin.Text, out int x) && x > 59)
            {
                tbMin.Text = "59";
                MessageBox.Show("hodina má jen 60 minut");
            }
            else
            {
                _Min = x;
                tbMin.Text.PadLeft(2, '0');
            }
        }

        private void tbSec_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(tbSec.Text, out int x) && x > 59)
            {
                tbSec.Text = "59";
                MessageBox.Show("minuta má jen 60 sekund");
            }
            else
            {
                _Sec = x;
                tbSec.Text.PadLeft(2, '0');
            }

        }

        private void UserControl_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void btUp_Click(object sender, RoutedEventArgs e)
        {
            UpCount();
            e.Handled = true;
        }

        private void btDn_Click(object sender, RoutedEventArgs e)
        {
            DnCount();
            e.Handled = true;
        }
        ///////////////////////////////////////////
        public int EmptyStringValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            else if (value is string)
                return value;
            else if (value is int && (int)value == EmptyStringValue)
                return string.Empty;
            else
                return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string s)
            {
                //if (s.IsNumeric(NumericValidator.Int32))
                    return System.Convert.ToInt32(s);
                //else
                //    return EmptyStringValue;
            }
            return value;
        }

    }
}
