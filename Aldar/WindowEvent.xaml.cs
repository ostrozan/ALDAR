using Aldar;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Shapes;

namespace GUI_logo
{
    /// <summary>
    /// Interakční logika pro WindowEvent.xaml
    /// </summary>
    public partial class WindowEvent : Window
    {

        private Com com;
        private string rx_data;
        Brush color;
        private List<string> events = new List<string>();
        private Paragraph par = new Paragraph();
        private TextRange tr;

        public WindowEvent(string rx_data,Com com)
        {
            InitializeComponent();
            this.com = com;
            this.rx_data = rx_data;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Nacti_historii();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //spojeni.Texxt[0] = null;

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "Text Files (*.rtf;)|*.rtf;";
            sfd.Filter = "Text Files (*.txt;)|*.txt;";
            //sfd.InitialDirectory = MainWindow.pathProj;
            if (sfd.ShowDialog() == true)
            {
                //using (FileStream fs = new FileStream(sfd.SafeFileName, FileMode.Create))
                //{

                //    //TextRange tr = new TextRange(document.ContentStart, document.ContentEnd);
                //    tr.Save(fs, DataFormats.Text);
                //}
                using (StreamWriter outputFile = new StreamWriter(sfd.FileName))
                {
                    

                        outputFile.WriteLine(tbMain.Text);
                }
            }

        }

        public void Nacti_historii()
        {  
            events = rx_data.Split('>').ToList();
            events.RemoveAt(0);//prvni odebrat
            events.RemoveAt(events.Count - 1);//posledni odebrat
            for (int i=0; i< events.Count;i++)
            {
                string[] sx = events[i].Split(' ');
                if(sx[0]!="65535" )
                {
                    sx[6] = sx[6].PadLeft(4, '0');
                    UInt16 udalost = Convert.ToUInt16(sx[6], 16);    
                    
                    string radek = sx[2] + '.' + sx[1] + '.' + sx[0] + "   " + sx[3] + ':' + sx[4] + ':' + sx[5] + "   ";
                  
                    radek += "  " + VyberUdalost(udalost);
                    if (udalost == 0x10) radek += VyberVstup(udalost);
                    radek += '\n';
                    tbMain.Text += radek;
                }
            }
        }


        private string VyberVstup(UInt16 typ_udalosti)
        {
            typ_udalosti &= 0xF;
            string ret = "";
            switch (typ_udalosti)
            {
                case 0x0: ret = "smyčka 1"; break;
                case 0x1: ret = "smyčka 2"; break;
                case 0x2: ret = "smyčka 3"; break;
                case 0x3: ret = "smyčka 4"; break;
            }

            return ret;
        }


        private string VyberUdalost(UInt16 typ_udalosti)
        {
            string str = "";
            UInt16 pom_typ = typ_udalosti;
            pom_typ &= 0xFFF0;//horni nible
            switch (pom_typ)
            {
                //#define RST 0x0000
                //#define ALARM 0x10
                //#define ALARM_ACT 0x20
                //#define ALARM_DEACT 0x30
                //#define ON_TEMP 0x40
                //#define OFF_TEMP 0x50
                //#define ON_SWCLOCK 0x60
                //#define OFF_SWCLOCK 0x70
                //#define ON_SMS 0x80
                //#define OFF_SMS 0x90
                //#define ON_RNG 0xa0
                //#define OFF_RNG 0xb0
                //#define SEND_SMS 0xc0
                //#define GSM_QEST 0xd0
                //#define SYS_ZAP_GSM 0xe0
                //#define SYS_VYP_GSM 0xf0
                case 0: str = "RESET"; break;
                case 0x10: str = "alarm"; color = Brushes.Black; break;
                case 0x20: str = "aktivace alarmu"; color = Brushes.Black; break;
                case 0x30: str = "deaktivace alarmu"; color = Brushes.Black; break;
                case 0x40: str = "termostat zap"; color = Brushes.Black; break;
                case 0x50: str = "termostat vyp"; color = Brushes.Black; break;
                case 0x60: str = "spínačky zap"; color = Brushes.Black; break;
                case 0x70: str = "spínačky vyp"; color = Brushes.Black; break;
                case 0x80: str = "výstup zapnut přes sms"; color = Brushes.Green; break;
                case 0x90: str = "výstup vypnut přes sms"; color = Brushes.Green; break;
                case 0xa0: str = "výstup zapnut prozvoněním"; color = Brushes.Blue; break;
                case 0xb0: str = "výstup vypnut prozvoněním"; color = Brushes.Blue; break;
                case 0xd0: str = "dotaz GSM"; color = Brushes.Green; break;
                case 0xe0: str = "aktivace alarmu GSM"; color = Brushes.Green; break;
                case 0xf0: str = "deaktivace alarmu GSM"; color = Brushes.Green; break;
            }
            return str;
        }

        private void buttonDeleteHistory_Click(object sender, RoutedEventArgs e)
        {
            com.send("C\r\n");
            Close();
            
        }
    }
}
