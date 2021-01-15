using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace UART_TEST01
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            array[0] = 0x05;
            timer2.Interval = 500;

           

        }
        byte[] array = new byte[1];
        SerialPort mySerialPort = new SerialPort();
        private void button1_Click(object sender, EventArgs e)
        {
            timer2.Start();
            if (!mySerialPort.IsOpen)
            {
                mySerialPort.PortName = comboBox1.Text;
            }
            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;

            if (!mySerialPort.IsOpen)
            {
                mySerialPort.Open();
            }
            timer2.Start();
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            mySerialPort.Write(array, 0, 1);
            //mySerialPort.ReadTimeout = 500; //500ms Timeout
            //string indata = mySerialPort.ReadExisting();
            // MessageBox.Show("Data:" + indata);
            // MessageBox.Show(input);
           // mySerialPort.Close();
            mySerialPort.ReadTimeout = 200;
           
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            if (sp.IsOpen)
            {
                string indata = sp.ReadExisting();
                MessageBox.Show("Data Received Event:" + indata);
                sp.Close();
            }
            
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string[] text = SerialPort.GetPortNames();
            MessageBox.Show(String.Join(":", text));
            comboBox1.Items.AddRange(text);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            array[0]++;
            button1.PerformClick();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            
        }


    }
}
