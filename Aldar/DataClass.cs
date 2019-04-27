using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aldar
{
  //  		Serial.print (u.s.telNums[1].is_sms_control,10);
		//Serial.print (' ');
		//Serial.print (u.s.telNums[1].is_ring_control,10);
		//Serial.print (' ');
		//Serial.print (u.s.telNums[1].send_sms,10);
		//Serial.print (' ');
		//Serial.print (u.s.telNums[1].ring,10);
    public class DataClass : INotifyPropertyChanged
    {
        private string entry;
        public string Entry
        {
            get { return entry; }
            set
            {
                if (entry != value)
                {
                    entry = value;
                    NotifyPropertyChanged("Entry");
                }
            }
        }

        private string exit;
        public string Exit
        {
            get { return exit; }
            set
            {
                if (exit != value)
                {
                    exit = value;
                    NotifyPropertyChanged("Exit");
                }
            }
        }

        private string alarm;
        public string Alarm
        {
            get { return alarm; }
            set
            {
                if (alarm != value)
                {
                    alarm = value;
                    NotifyPropertyChanged("Alarm");
                }
            }
        }

        private string zoneActiv;
        public string ZoneActiv
        {
            get { return zoneActiv; }
            set
            {
                if (zoneActiv != value)
                {
                    zoneActiv = value;
                    NotifyPropertyChanged("ZoneActiv");
                }
            }
        }

        private string zoneWait;
        public string ZoneWait
        {
            get { return zoneWait; }
            set
            {
                if (zoneWait != value)
                {
                    zoneWait = value;
                    NotifyPropertyChanged("ZoneWait");
                }
            }
        }

        private string loopActivate;
        public string LoopActivate
        {
            get { return loopActivate; }
            set
            {
                if (loopActivate != value)
                {
                    loopActivate = value;
                    NotifyPropertyChanged("LoopActivate");

                }
            }
        }

        public ObservableCollection<string> LoopTypes { get; set; } = new ObservableCollection<string>() {"","","","" };

        private string telNum1;
        public string TelNum1
        {
            get { return telNum1; }
            set
            {
                if (telNum1 != value)
                {
                    telNum1 = value;
                    NotifyPropertyChanged("TelNum1");
                }
            }
        }

        private string isTel1SmsCtrl;
        public string IsTel1SmsCtrl
        {
            get { return isTel1SmsCtrl; }
            set
            {
                if (isTel1SmsCtrl != value)
                {
                    isTel1SmsCtrl = value;
                    NotifyPropertyChanged("IsTel1SmsCtrl");
                }
            }
        }

        private string isTel1RngCtrl;
        public string IsTel1RngCtrl
        {
            get { return isTel1RngCtrl; }
            set
            {
                if (isTel1RngCtrl != value)
                {
                    isTel1RngCtrl = value;
                    NotifyPropertyChanged("IsTel1RngCtrl");
                }
            }
        }

        private string isTel1Sms;
        public string IsTel1Sms
        {
            get { return isTel1Sms; }
            set
            {
                if (isTel1Sms != value)
                {
                    isTel1Sms = value;
                    NotifyPropertyChanged("IsTel1Sms");
                }
            }
        }

        private string isTel1Rng;
        public string IsTel1Rng
        {
            get { return isTel1Rng; }
            set
            {
                if (isTel1Rng != value)
                {
                    isTel1Rng = value;
                    NotifyPropertyChanged("IsTel1Rng");
                }
            }
        }

        private string isTel1Microphone;
        public string IsTel1Microphone
        {
            get { return isTel1Microphone; }
            set
            {
                if (isTel1Microphone != value)
                {
                    isTel1Microphone = value;
                    NotifyPropertyChanged("IsTel1Microphone");
                }
            }
        }

        private string telNum2;
        public string TelNum2
        {
            get { return telNum2; }
            set
            {
                if (telNum2 != value)
                {
                    telNum2 = value;
                    NotifyPropertyChanged("TelNum2");
                }
            }
        }

        private string isTel2SmsCtrl;
        public string IsTel2SmsCtrl
        {
            get { return isTel2SmsCtrl; }
            set
            {
                if (isTel2SmsCtrl != value)
                {
                    isTel2SmsCtrl = value;
                    NotifyPropertyChanged("IsTel2SmsCtrl");
                }
            }
        }

        private string isTel2RngCtrl;
        public string IsTel2RngCtrl
        {
            get { return isTel2RngCtrl; }
            set
            {
                if (isTel2RngCtrl != value)
                {
                    isTel2RngCtrl = value;
                    NotifyPropertyChanged("IsTel2RngCtrl");
                }
            }
        }

        private string isTel2Sms;
        public string IsTel2Sms
        {
            get { return isTel2Sms; }
            set
            {
                if (isTel2Sms != value)
                {
                    isTel2Sms = value;
                    NotifyPropertyChanged("IsTel2Sms");
                }
            }
        }

        private string isTel2Rng;
        public string IsTel2Rng
        {
            get { return isTel2Rng; }
            set
            {
                if (isTel2Rng != value)
                {
                    isTel2Rng = value;
                    NotifyPropertyChanged("IsTel2Rng");
                }
            }
        }

        private string telNum3;
        public string TelNum3
        {
            get { return telNum3; }
            set
            {
                if (telNum3 != value)
                {
                    telNum3 = value;
                    NotifyPropertyChanged("TelNum3");
                }
            }
        }

        private string isTel3SmsCtrl;
        public string IsTel3SmsCtrl
        {
            get { return isTel3SmsCtrl; }
            set
            {
                if (isTel3SmsCtrl != value)
                {
                    isTel3SmsCtrl = value;
                    NotifyPropertyChanged("IsTel3SmsCtrl");
                }
            }
        }

        private string isTel3RngCtrl;
        public string IsTel3RngCtrl
        {
            get { return isTel3RngCtrl; }
            set
            {
                if (isTel3RngCtrl != value)
                {
                    isTel3RngCtrl = value;
                    NotifyPropertyChanged("IsTel3RngCtrl");
                }
            }
        }

        private string isTel3Sms;
        public string IsTel3Sms
        {
            get { return isTel3Sms; }
            set
            {
                if (isTel3Sms != value)
                {
                    isTel3Sms = value;
                    NotifyPropertyChanged("IsTel3Sms");
                }
            }
        }

        private string isTel3Rng;
        public string IsTel3Rng
        {
            get { return isTel3Rng; }
            set
            {
                if (isTel3Rng != value)
                {
                    isTel3Rng = value;
                    NotifyPropertyChanged("IsTel3Rng");
                }
            }
        }

        private MinuteSpan minuteSpan1;
        public MinuteSpan MinuteSpan1
        {
            get { return minuteSpan1; }
            set
            {
                minuteSpan1 = value;
                NotifyPropertyChanged("MinuteSpan1");
            }
        } 

        private MinuteSpan minuteSpan2;
        public MinuteSpan MinuteSpan2
        {
            get { return minuteSpan2; }
            set
            {
                minuteSpan2 = value;
                NotifyPropertyChanged("MinuteSpan2");
            }
        }

        private MinuteSpan minuteSpan3;
        public MinuteSpan MinuteSpan3
        {
            get { return minuteSpan3; }
            set
            {
                minuteSpan3 = value;
                NotifyPropertyChanged("MinuteSpan3");
            }
        }

        private MinuteSpan minuteSpan4;
        public MinuteSpan MinuteSpan4
        {
            get { return minuteSpan4; }
            set
            {
                minuteSpan4 = value;
                NotifyPropertyChanged("MinuteSpan4");
            }
        }

        private string temp;
        public string Temp
        {
            get { return temp; }
            set
            {
                if (temp != value)
                {
                    temp = value;
                    NotifyPropertyChanged("Temp");
                }
            }
        }

        private string tempHyst;
        public string TempHyst
        {
            get { return tempHyst; }
            set
            {
                if (tempHyst != value)
                {
                    tempHyst = value;
                    NotifyPropertyChanged("TempHyst");
                }
            }
        }

        private string tempAl1;
        public string TempAl1
        {
            get { return tempAl1; }
            set
            {
                if (tempAl1 != value)
                {
                    tempAl1 = value;
                    NotifyPropertyChanged("TempAl1");
                }
            }
        }

        private string tempAl2;
        public string TempAl2
        {
            get { return tempAl2; }
            set
            {
                if (tempAl2 != value)
                {
                    tempAl2 = value;
                    NotifyPropertyChanged("TempAl2");
                }
            }
        }

        private string activAlT1;
        public string ActivAlT1
        {
            get { return activAlT1; }
            set
            {
                if (activAlT1 != value)
                {
                    activAlT1 = value;
                    NotifyPropertyChanged("ActivAlT1");
                }
            }
        }

        private string activAlT2;
        public string ActivAlT2
        {
            get { return activAlT2; }
            set
            {
                if (activAlT2 != value)
                {
                    activAlT2 = value;
                    NotifyPropertyChanged("ActivAlT2");
                }
            }
        }


        public DataClass()
        {
            minuteSpan1 = new MinuteSpan();
            minuteSpan2 = new MinuteSpan();
            minuteSpan3 = new MinuteSpan();
            minuteSpan4 = new MinuteSpan();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
