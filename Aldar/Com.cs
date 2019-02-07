using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Management;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace Aldar
{
    public class Com
    {
        public enum comState
        {
            fail, open, close
        }
        public comState State { get; private set; }
        public SerialPort serialPort = new SerialPort();
        public static string[] DejPorty { get { return SerialPort.GetPortNames(); } }
        public bool IsRxEvent { get; set; } = false;
        public bool IsAck { get; set; }
        public string txBuffer { get; set; }
        public string rxBuffer { get; set; }
        //private DataClass dataClass;

        public Com(string deviceName, int baudrate)
        {
            //try
            //{
            //    if (deviceName.Contains("COM")) { serialPort.PortName = deviceName; }
            //    else { serialPort.PortName = SearchDevice(deviceName); }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //this.dataClass = dataClass;
            serialPort.PortName = deviceName;
            serialPort.DataReceived += SerialPort_DataReceived;
            serialPort.DtrEnable = true;
            serialPort.RtsEnable = true;
            serialPort.BaudRate = baudrate;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            serialPort.DataBits = 8;
            serialPort.Handshake = Handshake.None;
            serialPort.ReadTimeout = -1;
            serialPort.WriteTimeout = -1;

        }


        public string SearchDevice(string deviceName)
        {
            List<string> tmp = new List<string>();
            string comName = "";

            //try
            //{

            //    bool nalezeno = false;
            //    //instance vyhledávače objektů(portů)
            //    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_SerialPort");

            //    //projede kolekci nalezených objektů
            //    foreach (ManagementObject queryObj in searcher.Get())
            //    {
            //        //vyseparuje název
            //        tmp.Add(queryObj["Caption"].ToString());

            //        //když najde požadovaný název v seznamu přiřadí mu místní serialPort("Port")
            //        //vypíše hlášku o úspěchu/ neúspěchu
            //        if (queryObj["Caption"].ToString().Contains(deviceName))
            //        {
            //            comName = queryObj["DeviceID"].ToString();
            //            nalezeno = true;
            //        }
            //        //eZ430-ChronosAP
            //        //{
            //        //    MessageBox.Show("Arduino nalezeno na portu: " + queryObj["DeviceID"]);
            //        //    //ez43serial.PortName = queryObj["DeviceID"].ToString();
            //        //    //retval = true;

            //        //}

            //    }

            //    if (nalezeno == false)
            //    {
              
            //        if (MessageBox.Show("Nebylo nalezeno žádné zařízení ArDaLu na USB \n chcete pokračovat přes bluetooth?", "POZOR", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //        {
            //            return "COM100";
            //        }
            //        else
            //        {
            //            comName = "none";
            //        }
            //    }

            //}

            //catch (ManagementException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            return comName;
        }

        public void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //string s = 
            rxBuffer = serialPort.ReadLine();
            //IsRxEvent = true;
            serialPort.DiscardInBuffer();
            try
            {
                if (rxBuffer.Contains("dt>"))
                {
                    int pom = 0;
                    int.TryParse(rxBuffer.Substring(rxBuffer.IndexOf('<') + 11, 2), out pom);
                    MainWindow.textMsg[1] = rxBuffer.Substring(3, rxBuffer.IndexOf('<') - 3);
                    MainWindow.teploty[0] = rxBuffer.Substring(rxBuffer.IndexOf('<') + 1, 4) + " °C";
                    MainWindow.teploty[1] = rxBuffer.Substring(rxBuffer.IndexOf('<') + 6, 4) + " °C";
                    MainWindow.gsmSigValue[0] = pom;
                }
                else
                {
                    MainWindow.textMsg[0] += rxBuffer;
                    if (rxBuffer.Contains("ack"))
                    {
                        MessageBox.Show("data upload succesful");
                    }
                    if (rxBuffer.Contains("OKrec"))
                        MessageBox.Show("data upload succesful");

                    else if (rxBuffer.Contains("Err"))
                    {
                        MessageBox.Show("chyba přenosu dat - zkuste znovu");
                        //MainWindow.curs = Cursors.Arrow;
                    }
                    if (rxBuffer.Contains("data"))
                    {

                        LoadData(rxBuffer);
                        //MainWindow.texts_counters[c1] = "";
                    }

                    if (rxBuffer.Contains("in"))
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            //MainWindow.VSTUPY[i] = Convert.ToInt16(rxBuffer[i + 2] - 0x30);
                        }
                    }

                    if (rxBuffer.Contains("out"))
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            //MainWindow.VYSTUPY[i] = Convert.ToInt16(rxBuffer[i + 3] - 0x30);
                        }
                    }
                    if (rxBuffer.Contains("tmr"))
                    {
                        try
                        {
                            //MainWindow.textMsg[1] = rxBuffer;
                            //Thread.Sleep(500);
                            int i1 = rxBuffer.IndexOf("-") + 1;
                            int i2 = rxBuffer.IndexOf("<");
                            int c2 = Convert.ToInt16(rxBuffer.Substring(i1, i2 - i1));
                            int c1 = Convert.ToInt16(rxBuffer[0] - 0x30);
                            //MainWindow.texts_counters[c1] = Convert.ToString(c2 / 60).PadLeft(2, '0') + ':' + Convert.ToString(c2 % 60).PadLeft(2, '0');
                        }
                        catch { }

                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        public void send()
        {

         
            serialPort.WriteLine(rxBuffer);
        }

        public void send(byte[] data)
        {
            //int checksum = 0;

            //byte[] pole = Encoding.ASCII.GetBytes(command);
            ////checksum calc
            //for (int i = 0; i < pole.Length - 3; i++)
            //{
            //    checksum += pole[i];
            //}
            //checksum &= 0xFF;
            //checksum = (byte)(0 - checksum);

            //pole[command.Length - 2] = (byte)checksum;
            //serialPort.Write("W");
            serialPort.Write(data, 0, data.Length);
            serialPort.Write("\r\n");
        }

        public void TogglePort()
        {
            State = comState.fail;
            try
            {
                if (serialPort != null)
                {
                    if (!serialPort.IsOpen)
                    {
                        // port neotevren,otevrit 
                        serialPort.Open();
                        State = comState.open;
                        //MainWindow.inputs[0].Led.Fill = Brushes.Lime;
                    }
                    else
                    {
                        //port otevren, zavrit
                        State = comState.close;
                        serialPort.DiscardInBuffer();
                        //   while (!zavritPortFlag) ;
                        serialPort.Close();
                        //   zavritPortFlag = false;
                    }

                }

            }

            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                MessageBox.Show("chyba");
            }
        }

        //public bool GetAck(string s, int timeout)
        //{
        //    int cnt = 0;
        //    send(s);
        //    IsRxEvent = false;
        //    while (!IsRxEvent)
        //    {
        //        Thread.Sleep(1);
        //        if (++cnt > timeout) break;
        //    }

        //    //IsRxEvent = false;
        //    return (cnt > timeout) ? false : true;
        //}
    public bool Verify(string str)
    {
                bool isok = true;
                string[] arr1 = rxBuffer.Split(' ');//zakladni deleni
                string[] arr2 = arr1[21].Split('/');
                string[] arr3 = arr1[22].Split(':');
                if (arr1[0] != MainWindow.dataClass.Entry) isok = false;
                if (arr1[1] != MainWindow.dataClass.Exit) isok = false;
                if (arr1[2] != MainWindow.dataClass.Alarm) isok = false;
                if (arr1[3] != MainWindow.dataClass.ZoneActiv) isok = false;
                if (arr1[4] != MainWindow.dataClass.ZoneWait) isok = false;
                if (arr1[5].Substring(0, 1) != MainWindow.dataClass.LoopTypes[0]) isok = false;
                if (arr1[5].Substring(1, 1) != MainWindow.dataClass.LoopTypes[1]) isok = false;
                if (arr1[5].Substring(2, 1) != MainWindow.dataClass.LoopTypes[2]) isok = false;
                if (arr1[5].Substring(3, 1) != MainWindow.dataClass.LoopTypes[3]) isok = false;
                //tel1
                if (arr1[6] != MainWindow.dataClass.TelNum1) isok = false;
                if (arr1[7] != MainWindow.dataClass.IsTel1SmsCtrl) isok = false;
                if (arr1[8] != MainWindow.dataClass.IsTel1RngCtrl) isok = false;
                if (arr1[9] != MainWindow.dataClass.IsTel1Sms) isok = false;
                if (arr1[10] != MainWindow.dataClass.IsTel1Rng) isok = false;
                //tel2
                if (arr1[11] != MainWindow.dataClass.TelNum2) isok = false;
                if (arr1[12] != MainWindow.dataClass.IsTel2SmsCtrl) isok = false;
                if (arr1[13] != MainWindow.dataClass.IsTel2RngCtrl) isok = false;
                if (arr1[14] != MainWindow.dataClass.IsTel2Sms) isok = false;
                if (arr1[15] != MainWindow.dataClass.IsTel2Rng) isok = false;
                //tel3
                if (arr1[16] != MainWindow.dataClass.TelNum3) isok = false;
                if (arr1[17] != MainWindow.dataClass.IsTel3SmsCtrl) isok = false;
                if (arr1[18] != MainWindow.dataClass.IsTel3RngCtrl) isok = false;
                if (arr1[19] != MainWindow.dataClass.IsTel3Sms) isok = false;
                if (arr1[20] != MainWindow.dataClass.IsTel3Rng) isok = false;
                //spinacky
                int i = 0;
                string[] s2 = arr2[0].Split(':');
                int.TryParse(s2[0], out i);
                if (i != MainWindow.dataClass.MinuteSpan1.startTime) isok = false;
                int.TryParse(s2[1], out i);
                if (i != MainWindow.dataClass.MinuteSpan1.stopTime) isok = false;

                s2 = arr2[1].Split(':');
                int.TryParse(s2[0], out i);
                if (i != MainWindow.dataClass.MinuteSpan2.startTime) isok = false;
                int.TryParse(s2[1], out i);
                if (i != MainWindow.dataClass.MinuteSpan2.stopTime) isok = false;

                s2 = arr2[2].Split(':');
                int.TryParse(s2[0], out i);
                if (i != MainWindow.dataClass.MinuteSpan3.startTime) isok = false;
                int.TryParse(s2[1], out i);
                if (i != MainWindow.dataClass.MinuteSpan3.stopTime) isok = false;

                s2 = arr2[3].Split(':');
                int.TryParse(s2[0], out i);
                if (i != MainWindow.dataClass.MinuteSpan4.startTime) isok = false;
                int.TryParse(s2[1], out i);
                if (i != MainWindow.dataClass.MinuteSpan4.stopTime) isok = false;
                //teploty
                if (arr3[0] != MainWindow.dataClass.Temp) isok = false;
                if (arr3[1] != MainWindow.dataClass.TempHyst) isok = false;
                if (arr3[2] != MainWindow.dataClass.TempAl1) isok = false;
                if (arr3[3] != MainWindow.dataClass.TempAl2) isok = false;
                if (arr3[4] != MainWindow.dataClass.ActivAlT1) isok = false;
                if (arr3[5] != MainWindow.dataClass.Temp) isok = false;
                //MainWindow.texts_counters[c1] = "";
            
            return isok;
        }

        public void LoadData(string str)
        {
   
            string[] arr1 = rxBuffer.Split(' ');//zakladni deleni
            string[] arr2 = arr1[22].Split('/');
            string[] arr3 = arr1[23].Split(':');
            MainWindow.dataClass.Entry =  arr1[0];
            MainWindow.dataClass.Exit = arr1[1];
            MainWindow.dataClass.Alarm = arr1[2];
            MainWindow.dataClass.ZoneActiv = arr1[3];
            MainWindow.dataClass.ZoneWait = arr1[4];
            MainWindow.dataClass.LoopTypes[0]= arr1[5].Substring(0, 1);
            MainWindow.dataClass.LoopTypes[1] = arr1[5].Substring(1, 1);
            MainWindow.dataClass.LoopTypes[2] = arr1[5].Substring(2, 1);
            MainWindow.dataClass.LoopTypes[3] = arr1[5].Substring(3, 1);
            MainWindow.dataClass.LoopActivate = arr1[6];
            //tel1
            MainWindow.dataClass.TelNum1 = arr1[7];
            MainWindow.dataClass.IsTel1SmsCtrl = arr1[8];
            MainWindow.dataClass.IsTel1RngCtrl = arr1[9];
            MainWindow.dataClass.IsTel1Sms = arr1[10];
            MainWindow.dataClass.IsTel1Rng = arr1[11];
            //tel2
            MainWindow.dataClass.TelNum2 = arr1[12];
            MainWindow.dataClass.IsTel2SmsCtrl = arr1[13];
            MainWindow.dataClass.IsTel2RngCtrl = arr1[14];
            MainWindow.dataClass.IsTel2Sms = arr1[15];
            MainWindow.dataClass.IsTel2Rng = arr1[16];
            //tel3
            MainWindow.dataClass.TelNum3 = arr1[17];
            MainWindow.dataClass.IsTel3SmsCtrl = arr1[18];
            MainWindow.dataClass.IsTel3RngCtrl = arr1[19];
            MainWindow.dataClass.IsTel3Sms = arr1[20];
            MainWindow.dataClass.IsTel3Rng = arr1[21];
            //spinacky
            Int16 i = 0;
            string[] s2 = arr2[0].Split(':');
            Int16.TryParse(s2[0], out i);
            MainWindow.dataClass.MinuteSpan1.startTime = i;

            Int16.TryParse(s2[1], out i);
            MainWindow.dataClass.MinuteSpan1.stopTime = i;

            s2 = arr2[1].Split(':');
            Int16.TryParse(s2[0], out i);
            MainWindow.dataClass.MinuteSpan2.startTime = i;
            Int16.TryParse(s2[1], out i);
            MainWindow.dataClass.MinuteSpan2.stopTime = i;

            s2 = arr2[2].Split(':');
            Int16.TryParse(s2[0], out i);
            MainWindow.dataClass.MinuteSpan3.startTime = i;
            Int16.TryParse(s2[1], out i);
            MainWindow.dataClass.MinuteSpan3.stopTime = i;

            s2 = arr2[3].Split(':');
            Int16.TryParse(s2[0], out i);
            MainWindow.dataClass.MinuteSpan4.startTime = i;
            Int16.TryParse(s2[1], out i);
            MainWindow.dataClass.MinuteSpan4.stopTime = i;
            //teploty
            MainWindow.dataClass.Temp = arr3[0];
            MainWindow.dataClass.TempHyst = arr3[1];
            MainWindow.dataClass.TempAl1 = arr3[2];
            MainWindow.dataClass.TempAl2 = arr3[3];
            MainWindow.dataClass.ActivAlT1 = arr3[4];
            MainWindow.dataClass.ActivAlT2 = arr3[5];
            //MainWindow.texts_counters[c1] = "";
            IsRxEvent = true;

        }


    }
    }


