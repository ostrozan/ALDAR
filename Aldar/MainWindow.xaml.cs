using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //    private  string temp1;
        //    public  string Temp1
        //    {
        //        get { return temp1; }
        //        set
        //        {
        //            if (temp1 != value)
        //            {
        //                temp1 = value;
        //                OnPropertyChanged("Temp1");
        //            }
        //        }
        //    }

        //    private string temp2;
        //    public string Temp2
        //    {
        //        get { return temp2; }
        //        set
        //        {
        //            if (temp2 != value)
        //            {
        //                temp2 = value;
        //                OnPropertyChanged("Temp2");
        //            }
        //        }
        //    }

        public static ObservableCollection<string> textMsg { get; set; } = new ObservableCollection<string>() { "", "" };
        public static ObservableCollection<string> teploty { get; set; } = new ObservableCollection<string>() { "", "" };
        public static ObservableCollection<int> gsmSigValue { get; set; } = new ObservableCollection<int>() { 0 };

        public static DataClass dataClass =new DataClass();

        //public  DataClass dataClass = new DataClass();
        Com com = new Com("COM4", 38400);

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            cmbComPorts.ItemsSource = Com.DejPorty;
             DataContext = this;
            teploty[0] = "30,5";
            teploty[1] = "20,5";
            //dataClass = new DataClass();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void cmbComPorts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            com = new Com(cmbComPorts.SelectedItem.ToString(), 38400);
        }

        private void btnCom_Click(object sender, RoutedEventArgs e)
        {
            com.TogglePort();
            if (com.State != Com.comState.fail)
            {
                if (com.State == Com.comState.open)
                {
                    btnCom.Content = "Odpojit";
                    MessageBox.Show(com.serialPort.PortName + " byl úspěšně  připojen");
                }
                else
                {

                    btnCom.Content = "Připojit";
                    MessageBox.Show(com.serialPort.PortName + " byl úspěšně  odpojen");
                }
            }
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            string s = "";
            List<byte> data = new List<byte>();
            data.Add((byte)'W');
            //smycky
            dataClass.Entry = tbxEntryTime.Text;
            data.AddRange(BitConverter.GetBytes(Convert.ToInt16(int.Parse(tbxEntryTime.Text))));
            dataClass.Exit = tbxExitTime.Text;
            data.AddRange(BitConverter.GetBytes(Convert.ToInt16(int.Parse(tbxExitTime.Text))));
            dataClass.Alarm = tbxAlarmTime.Text;
            data.AddRange(BitConverter.GetBytes(Convert.ToInt16(int.Parse(tbxAlarmTime.Text))));
            //zona
            dataClass.ZoneActiv = tbxZoneActivTime.Text;
            data.AddRange(BitConverter.GetBytes(Convert.ToInt16(int.Parse(tbxZoneActivTime.Text))));
            dataClass.ZoneWait = tbxZoneRestTime.Text;
            data.AddRange(BitConverter.GetBytes(Convert.ToInt16(int.Parse(tbxZoneRestTime.Text))));

            dataClass.LoopTypes[0] = (rbNone1.IsChecked == true) ? "0" : (rbInst1.IsChecked == true) ? "1" : (rbDel1.IsChecked == true) ? "2" : (rb24h1.IsChecked == true) ? "3" : "4";
            dataClass.LoopTypes[1] = (rbNone2.IsChecked == true) ? "0" : (rbInst2.IsChecked == true) ? "1" : (rbDel2.IsChecked == true) ? "2" : (rb24h2.IsChecked == true) ? "3" : "4";
            dataClass.LoopTypes[2] = (rbNone3.IsChecked == true) ? "0" : (rbInst3.IsChecked == true) ? "1" : (rbDel3.IsChecked == true) ? "2" : (rb24h3.IsChecked == true) ? "3" : "4"; ;
            dataClass.LoopTypes[3] = (rbNone4.IsChecked == true) ? "0" : (rbInst4.IsChecked == true) ? "1" : (rbDel4.IsChecked == true) ? "2" : (rb24h4.IsChecked == true) ? "3" : "4"; ;
            data.Add((byte)((rbNone1.IsChecked == true) ? 0 : (rbInst1.IsChecked == true) ? 1 : (rbDel1.IsChecked == true) ? 0x2 : (rb24h1.IsChecked == true) ? 0x3 : 0x4));
            data.Add((byte)((rbNone2.IsChecked == true) ? 0 : (rbInst2.IsChecked == true) ? 1 : (rbDel2.IsChecked == true) ? 0x2 : (rb24h2.IsChecked == true) ? 0x3 : 0x4));
            data.Add((byte)((rbNone3.IsChecked == true) ? 0 : (rbInst3.IsChecked == true) ? 1 : (rbDel3.IsChecked == true) ? 0x2 : (rb24h3.IsChecked == true) ? 0x3 : 0x4));
            data.Add((byte)((rbNone4.IsChecked == true) ? 0 : (rbInst4.IsChecked == true) ? 1 : (rbDel4.IsChecked == true) ? 0x2 : (rb24h4.IsChecked == true) ? 0x3 : 0x4));

            dataClass.LoopActivate = rbAktivSep.IsChecked == true ? "1" : "0";
            data.Add((byte)((rbAktivSep.IsChecked == true ? 1 : 0)));

            //gsm 1
            dataClass.TelNum1 = tbxTel1.Text;
            dataClass.IsTel1SmsCtrl = (bool)chbSmsCtrl1.IsChecked ? "1" : "0";
            dataClass.IsTel1RngCtrl = (bool)chbRingCtrl1.IsChecked ? "1" : "0";
            dataClass.IsTel1Sms = (bool)chbSms1.IsChecked ? "1" : "0";
            dataClass.IsTel1Rng = (bool)chbRing1.IsChecked ? "1" : "0";
            data.AddRange(Encoding.ASCII.GetBytes(tbxTel1.Text));
            data.Add(0);//doplneni nuly na konec stringu

            data.Add((byte)((bool)chbSmsCtrl1.IsChecked ? 1 : 0));
            data.Add((byte)((bool)chbRingCtrl1.IsChecked ? 1 : 0));
            data.Add((byte)((bool)chbSms1.IsChecked ? 1 : 0));
            data.Add((byte)((bool)chbRing1.IsChecked ? 1 : 0));
            data.Add(0);//doplneni nuly do zbyle promenne v mcu
            data.Add(0);//doplneni nuly do zbyle promenne v mcu
                        //gsm 2
            dataClass.TelNum2 = tbxTel2.Text;
            dataClass.IsTel2SmsCtrl = (bool)chbSmsCtrl2.IsChecked ? "1" : "0";
            dataClass.IsTel2RngCtrl = (bool)chbRingCtrl2.IsChecked ? "1" : "0";
            dataClass.IsTel2Sms = (bool)chbSms2.IsChecked ? "1" : "0";
            dataClass.IsTel2Rng = (bool)chbRing2.IsChecked ? "1" : "0";
            data.AddRange(Encoding.ASCII.GetBytes(tbxTel2.Text));
            data.Add(0);//doplneni nuly na konec stringu
            data.Add((byte)((bool)chbSmsCtrl2.IsChecked ? 1 : 0));
            data.Add((byte)((bool)chbRingCtrl2.IsChecked ? 1 : 0));
            data.Add((byte)((bool)chbSms2.IsChecked ? 1 : 0));
            data.Add((byte)((bool)chbRing2.IsChecked ? 1 : 0));
            data.Add(0);//doplneni nuly do zbyle promenne v mcu
            data.Add(0);//doplneni nuly do zbyle promenne v mcu
                        //gsm 3
            dataClass.TelNum3 = tbxTel3.Text;
            dataClass.IsTel3SmsCtrl = (bool)chbSmsCtrl3.IsChecked ? "1" : "0";
            dataClass.IsTel3RngCtrl = (bool)chbRingCtrl3.IsChecked ? "1" : "0";
            dataClass.IsTel3Sms = (bool)chbSms3.IsChecked ? "1" : "0";
            dataClass.IsTel3Rng = (bool)chbRing3.IsChecked ? "1" : "0";
            data.AddRange(Encoding.ASCII.GetBytes( tbxTel3.Text));
            data.Add(0);//doplneni nuly na konec stringu
            data.Add((byte)((bool)chbSmsCtrl3.IsChecked ? 1 : 0));
            data.Add((byte)((bool)chbRingCtrl3.IsChecked ? 1 : 0));
            data.Add((byte)((bool)chbSms3.IsChecked ? 1 : 0));
            data.Add((byte)((bool)chbRing3.IsChecked ? 1 : 0));
            data.Add(0);//doplneni nuly do zbyle promenne v mcu
            data.Add(0);//doplneni nuly do zbyle promenne v mcu
            //minute span 1 start
            int hh, mm;
            int.TryParse(tpOn1.Hod, out hh);
            int.TryParse(tpOn1.Min, out mm);
            dataClass.MinuteSpan1.startTime = Convert.ToInt16((hh * 60) + mm);
            data.AddRange(BitConverter.GetBytes(dataClass.MinuteSpan1.startTime));
            //minute span 1 stop
            int.TryParse(tpOff1.Hod, out hh);
            int.TryParse(tpOff1.Min, out mm);
            dataClass.MinuteSpan1.stopTime = Convert.ToInt16((hh * 60) + mm);
            data.AddRange(BitConverter.GetBytes(dataClass.MinuteSpan1.stopTime));
            //minute span 2 start
            int.TryParse(tpOn2.Hod, out hh);
            int.TryParse(tpOn2.Min, out mm);
            dataClass.MinuteSpan2.startTime = Convert.ToInt16((hh * 60) + mm);
            data.AddRange(BitConverter.GetBytes(dataClass.MinuteSpan2.startTime));
            //minute span 2 stop
            int.TryParse(tpOff2.Hod, out hh);
            int.TryParse(tpOff2.Min, out mm);
            dataClass.MinuteSpan2.stopTime = Convert.ToInt16((hh * 60) + mm);
            data.AddRange(BitConverter.GetBytes(dataClass.MinuteSpan2.stopTime));
            //minute span 3 start
            int.TryParse(tpOn3.Hod, out hh);
            int.TryParse(tpOn3.Min, out mm);
            dataClass.MinuteSpan3.startTime = Convert.ToInt16((hh * 60) + mm);
            data.AddRange(BitConverter.GetBytes(dataClass.MinuteSpan3.startTime));
            //minute span 3 stop
            int.TryParse(tpOff3.Hod, out hh);
            int.TryParse(tpOff3.Min, out mm);
            dataClass.MinuteSpan3.stopTime = Convert.ToInt16((hh * 60) + mm);
            data.AddRange(BitConverter.GetBytes(dataClass.MinuteSpan3.stopTime));
            //minute span 4 start
            int.TryParse(tpOn4.Hod, out hh);
            int.TryParse(tpOn4.Min, out mm);
            dataClass.MinuteSpan4.startTime = Convert.ToInt16((hh * 60) + mm);
            data.AddRange(BitConverter.GetBytes(dataClass.MinuteSpan4.startTime));
            //minute span 4 stop
            int.TryParse(tpOff4.Hod, out hh);
            int.TryParse(tpOff4.Min, out mm);
            dataClass.MinuteSpan4.stopTime = Convert.ToInt16((hh * 60) + mm);
            data.AddRange(BitConverter.GetBytes(dataClass.MinuteSpan4.stopTime));

            //teploty
            if (tbxTemp.Text.Contains(',')) s = tbxTemp.Text.Remove(tbxTemp.Text.IndexOf(','), 1);
            else s = tbxTemp.Text + "0";
            dataClass.Temp = s;
            data.AddRange(BitConverter.GetBytes(Convert.ToInt16(int.Parse(s))));
            if (tbxHystTemp.Text.Contains(',')) s = tbxHystTemp.Text.Remove(tbxHystTemp.Text.IndexOf(','), 1);
            else s = tbxHystTemp.Text + "0";
            dataClass.TempHyst = s;
            data.Add((byte)(int.Parse(s)));
            if (tbxTempAlT1.Text.Contains(',')) s = tbxTempAlT1.Text.Remove(tbxTempAlT1.Text.IndexOf(','), 1);
            else s = tbxTempAlT1.Text + "0";
            dataClass.TempAl1 = s;
            data.AddRange(BitConverter.GetBytes(Convert.ToInt16(int.Parse(s))));
            if (tbxTempAlT2.Text.Contains(',')) s = tbxTempAlT2.Text.Remove(tbxTempAlT2.Text.IndexOf(','), 1);
            else s = tbxTempAlT2.Text + "0";
            dataClass.TempAl2 = s;
            data.AddRange(BitConverter.GetBytes(Convert.ToInt16(int.Parse(s))));
            dataClass.ActivAlT1 = (bool)rbSelT1up.IsChecked ? "1" : "0";
            dataClass.ActivAlT2 = (bool)rbSelT2up.IsChecked ? "1" : "0";
            data.Add((byte)((bool)rbSelT1up.IsChecked ? 1 : 0));
            data.Add((byte)((bool)rbSelT2up.IsChecked ? 1 : 0));
            com.send(data.ToArray());

        }

        private async void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            com.IsRxEvent = false;
            await Load();
            //casy
            tbxEntryTime.Text =dataClass.Entry;
            tbxExitTime.Text = dataClass.Exit;
            tbxAlarmTime.Text = dataClass.Alarm;
            tbxZoneActivTime.Text = dataClass.ZoneActiv;
            tbxZoneRestTime.Text = dataClass.ZoneWait;
            //smycky
            switch(dataClass.LoopTypes[0])
            {
                case "0":rbNone1.IsChecked = true; break;
                case "1": rbInst1.IsChecked = true; break;
                case "2": rbDel1.IsChecked = true; break;
                case "3": rb24h1.IsChecked = true; break;
                case "4": rbZona1.IsChecked = true; break;
            }

            switch (dataClass.LoopTypes[1])
            {
                case "0": rbNone2.IsChecked = true; break;
                case "1": rbInst2.IsChecked = true; break;
                case "2": rbDel2.IsChecked = true; break;
                case "3": rb24h2.IsChecked = true; break;
                case "4": rbZona2.IsChecked = true; break;
            }

            switch (dataClass.LoopTypes[2])
            {
                case "0": rbNone3.IsChecked = true; break;
                case "1": rbInst3.IsChecked = true; break;
                case "2": rbDel3.IsChecked = true; break;
                case "3": rb24h3.IsChecked = true; break;
                case "4": rbZona3.IsChecked = true; break;
            }

            switch (dataClass.LoopTypes[3])
            {
                case "0": rbNone4.IsChecked = true; break;
                case "1": rbInst4.IsChecked = true; break;
                case "2": rbDel4.IsChecked = true; break;
                case "3": rb24h4.IsChecked = true; break;
                case "4": rbZona4.IsChecked = true; break;
            }

            if (dataClass.LoopActivate == "1") rbAktivSep.IsChecked = true;
            else rbAktivRozep.IsChecked = true;
            //tel1
            tbxTel1.Text = dataClass.TelNum1;
            chbRing1.IsChecked = dataClass.IsTel1Rng == "1";
            chbSms1.IsChecked = dataClass.IsTel1Sms == "1";
            chbRingCtrl1.IsChecked = dataClass.IsTel1RngCtrl == "1";
            chbSmsCtrl1.IsChecked = dataClass.IsTel1SmsCtrl == "1";
            //tel2
            tbxTel2.Text = dataClass.TelNum2;
            chbRing2.IsChecked = dataClass.IsTel2Rng == "1";
            chbSms2.IsChecked = dataClass.IsTel2Sms == "1";
            chbRingCtrl2.IsChecked = dataClass.IsTel2RngCtrl == "1";
            chbSmsCtrl2.IsChecked = dataClass.IsTel2SmsCtrl == "1";
            //tel3
            tbxTel3.Text = dataClass.TelNum3;
            chbRing3.IsChecked = dataClass.IsTel3Rng == "1";
            chbSms3.IsChecked = dataClass.IsTel3Sms == "1";
            chbRingCtrl3.IsChecked = dataClass.IsTel3RngCtrl == "1";
            chbSmsCtrl3.IsChecked = dataClass.IsTel3SmsCtrl == "1";

            tpOn1.Hod = (dataClass.MinuteSpan1.startTime / 60).ToString().PadLeft(2,'0');
            tpOn1.Min = (dataClass.MinuteSpan1.startTime % 60).ToString().PadLeft(2, '0');
            tpOff1.Hod = (dataClass.MinuteSpan1.stopTime / 60).ToString().PadLeft(2, '0');
            tpOff1.Min = (dataClass.MinuteSpan1.stopTime % 60).ToString().PadLeft(2, '0');

            tpOn2.Hod = (dataClass.MinuteSpan2.startTime / 60).ToString().PadLeft(2, '0');
            tpOn2.Min = (dataClass.MinuteSpan2.startTime % 60).ToString().PadLeft(2, '0');
            tpOff2.Hod = (dataClass.MinuteSpan2.stopTime / 60).ToString().PadLeft(2, '0');
            tpOff2.Min = (dataClass.MinuteSpan2.stopTime % 60).ToString().PadLeft(2, '0');

            tpOn3.Hod = (dataClass.MinuteSpan3.startTime / 60).ToString().PadLeft(2, '0');
            tpOn3.Min = (dataClass.MinuteSpan3.startTime % 60).ToString().PadLeft(2, '0');
            tpOff3.Hod = (dataClass.MinuteSpan3.stopTime / 60).ToString().PadLeft(2, '0');
            tpOff3.Min = (dataClass.MinuteSpan3.stopTime % 60).ToString().PadLeft(2, '0');

            tpOn4.Hod = (dataClass.MinuteSpan4.startTime / 60).ToString().PadLeft(2, '0');
            tpOn4.Min = (dataClass.MinuteSpan4.startTime % 60).ToString().PadLeft(2, '0');
            tpOff4.Hod = (dataClass.MinuteSpan4.stopTime / 60).ToString().PadLeft(2, '0');
            tpOff4.Min = (dataClass.MinuteSpan4.stopTime % 60).ToString().PadLeft(2, '0');

            int j;
            int.TryParse(dataClass.Temp, out j);
            tbxTemp.Text = string.Format("{0},{1}", j / 10, j % 10);

            int.TryParse(dataClass.TempHyst, out j);
            tbxHystTemp.Text = string.Format("{0},{1}", j / 10, j % 10);

            int.TryParse(dataClass.TempAl1, out j);
            tbxTempAlT1.Text = string.Format("{0},{1}", j / 10, j % 10);

            int.TryParse(dataClass.TempAl2, out j);
            tbxTempAlT2.Text = string.Format("{0},{1}", j / 10, j % 10);

            if (dataClass.ActivAlT1 == "1") rbSelT1up.IsChecked = true;
            else rbSelT1down.IsChecked = true;
            if (dataClass.ActivAlT2 == "1") rbSelT2up.IsChecked = true;
            else rbSelT2down.IsChecked = true;

        } 

        private async Task Load()
        {
            List<byte> data = new List<byte>();
            data.Add((byte)'R');
            com.send(data.ToArray());
            while (com.IsRxEvent == false) ;
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbxTel1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //dataStruct.tel1.number = tbxTel1.Text;
        }

        private void tbxTel2_TextChanged(object sender, TextChangedEventArgs e)
        {
            //dataStruct.tel2.number = tbxTel2.Text;
        }

        private void tbxTel3_TextChanged(object sender, TextChangedEventArgs e)
        {
            //dataStruct.tel3.number = tbxTel3.Text;
        }



        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void tbxTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!e.Text.Any(char.IsDigit)) e.Handled = true;
        }

        private void tbxTemp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string s = "123456789,";
            if (!s.Contains(e.Text)) e.Handled = true;
        }

        private void tbxTel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!e.Text.Any(char.IsDigit)) e.Handled = true;
            if (tb.Text.Length > 8) e.Handled = true;
        }

        private void tbxMessages_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbxMessages.ScrollToEnd();
        }
    }

    public struct TelNum
    {
        public string number;
        public bool is_sms_control;
        public bool is_ring_control;
        public bool send_sms;
        public bool ring;
    }

    public class MinuteSpan
    {

        public Int16 startTime { get; set; } = new int();
        public Int16 stopTime { get; set; } = new int();
    }

    public struct DataStruct
    {
        public int entry_delay;
        public int exit_delay;
        public int time_alarm;
        public TelNum tel1;
        public TelNum tel2;
        public TelNum tel3;
        public MinuteSpan minuteSpan1;
        public MinuteSpan minuteSpan2;
        public MinuteSpan minuteSpan3;
        public MinuteSpan minuteSpan4;
        public int temperature;
        public int temp_hyster;

    }
}
