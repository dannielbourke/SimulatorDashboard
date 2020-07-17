using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace SimulatorlDashboard
{
    public partial class Form1 : Form
    {
        string dataOut;
        string dataIn;
        int inPointer = 0;
        int pollNext = 0;
        string response;
        
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxComPort.Items.AddRange(ports);
        }

        //Buttons on com settings tab
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cBoxComPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(cBoxBaudRate.Text);
                serialPort1.DataBits = Convert.ToInt32(cBoxDataBits.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxStopBits.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxParityBits.Text);
                serialPort1.Open();
                progressBar1.Value = 100;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message,"You must first select a com port", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                progressBar1.Value = 0;
            }
        }


        private void btnSendData_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOut = tBoxDataOut.Text;
                serialPort1.WriteLine(dataOut);
            }
        }

        private void ClearData_Click(object sender, EventArgs e)
        {
            tBoxDataOut.Text = "";
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                dataIn = serialPort1.ReadExisting();
                this.Invoke(new EventHandler(ShowData));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Receive error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ShowData(object sender, EventArgs e)
        {
           response += dataIn; inPointer = response.Length - 1;
            
           if (response[inPointer] == '\n' && inPointer >= 4) // changed to 4!!!
            {            
                if (response[0] == 'D') //Response to digital query
                {
                    //tBoxDataIn.Text += "in digital parse method\n";
                    if (response[1] == '0' && response[2] == '0') { if (response[3] == '1') { pin0label.ForeColor = System.Drawing.Color.Red; } else { pin0label.ForeColor = System.Drawing.Color.Black; } }
                    if (response[1] == '0' && response[2] == '1') { if (response[3] == '1') { label13.ForeColor = System.Drawing.Color.Red; } else { label13.ForeColor = System.Drawing.Color.Black; } }
                    if (response[1] == '0' && response[2] == '2') { if (response[3] == '1') { label14.ForeColor = System.Drawing.Color.Red; } else { label14.ForeColor = System.Drawing.Color.Black; } }
                    if (response[1] == '0' && response[2] == '3') { if (response[3] == '1') { label15.ForeColor = System.Drawing.Color.Red; } else { label15.ForeColor = System.Drawing.Color.Black; } }
                    if (response[1] == '0' && response[2] == '4') { if (response[3] == '1') { label16.ForeColor = System.Drawing.Color.Red; } else { label16.ForeColor = System.Drawing.Color.Black; } }
                    if (response[1] == '0' && response[2] == '5') { if (response[3] == '1') { label17.ForeColor = System.Drawing.Color.Red; } else { label17.ForeColor = System.Drawing.Color.Black; } }
                    if (response[1] == '0' && response[2] == '6') { if (response[3] == '1') { label18.ForeColor = System.Drawing.Color.Red; } else { label18.ForeColor = System.Drawing.Color.Black; } }
                    if (response[1] == '0' && response[2] == '7') { if (response[3] == '1') { label19.ForeColor = System.Drawing.Color.Red; } else { label19.ForeColor = System.Drawing.Color.Black; } }
                    if (response[1] == '0' && response[2] == '8') { if (response[3] == '1') { label20.ForeColor = System.Drawing.Color.Red; } else { label20.ForeColor = System.Drawing.Color.Black; } }
                    if (response[1] == '0' && response[2] == '9') { if (response[3] == '1') { label21.ForeColor = System.Drawing.Color.Red; } else { label21.ForeColor = System.Drawing.Color.Black; } }
                    if (response[1] == '1' && response[2] == '0') { if (response[3] == '1') { label22.ForeColor = System.Drawing.Color.Red; } else { label22.ForeColor = System.Drawing.Color.Black; } }
                    if (response[1] == '1' && response[2] == '1') { if (response[3] == '1') { label23.ForeColor = System.Drawing.Color.Red; } else { label23.ForeColor = System.Drawing.Color.Black; } }
                    if (response[1] == '1' && response[2] == '2') { if (response[3] == '1') { label24.ForeColor = System.Drawing.Color.Red; } else { label24.ForeColor = System.Drawing.Color.Black; } }
                    if (response[1] == '1' && response[2] == '3') { if (response[3] == '1') { label25.ForeColor = System.Drawing.Color.Red; } else { label25.ForeColor = System.Drawing.Color.Black; } }
                    response = ""; inPointer = 0;
                }
            }
            
            if (response != "") //When try to point at first character in empty string exception occurs
            {
                if (response[inPointer] == '\n' && inPointer >= 2) 
                {
                    if (response[0] == 'A') //Response to analog query
                    {
                        //tBoxDataIn.Text += "in analog parse method\n"; 
                        if (response[1] == '0') { textBox6.Text = response.Substring(2); }
                        if (response[1] == '1') { textBox1.Text = response.Substring(2); }
                        if (response[1] == '2') { textBox2.Text = response.Substring(2); }
                        if (response[1] == '3') { textBox3.Text = response.Substring(2); }
                        if (response[1] == '4') { textBox4.Text = response.Substring(2); }
                        if (response[1] == '5') { textBox5.Text = response.Substring(2); }
                        response = ""; inPointer = 0;
                    }
                }
            }
            if (showIncomingBtn.ForeColor == System.Drawing.Color.Red) { tBoxDataIn.Text += dataIn; dataIn = ""; }
        }

        private void ClearReceivedData_Click(object sender, EventArgs e)
        {
            tBoxDataIn.Text = "";
        }

        private void showIncomingBtn_Click(object sender, EventArgs e)
        {
            if (showIncomingBtn.ForeColor == System.Drawing.Color.Black) { showIncomingBtn.ForeColor = System.Drawing.Color.Red; }
            else { showIncomingBtn.ForeColor = System.Drawing.Color.Black; }
        }

        private void tBoxDataOut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataOut = tBoxDataOut.Text; tBoxDataOut.Text = "";
                dataOut += '\n'; serialPort1.Write(dataOut);
            }
        }

        //Digital pin write buttons on telemetry tab 
        private void pin0Btn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (Pin0Btn.ForeColor == System.Drawing.Color.Black) { Pin0Btn.ForeColor = System.Drawing.Color.Red; dataOut = "!0001"; serialPort1.WriteLine(dataOut); }
                else { Pin0Btn.ForeColor = System.Drawing.Color.Black; dataOut = "!0000"; serialPort1.WriteLine(dataOut); }
            }
        }

        private void Pin1Btn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (Pin1Btn.ForeColor == System.Drawing.Color.Black) { Pin1Btn.ForeColor = System.Drawing.Color.Red; dataOut = "!0101"; serialPort1.WriteLine(dataOut); }
                else { Pin1Btn.ForeColor = System.Drawing.Color.Black; dataOut = "!0100"; serialPort1.WriteLine(dataOut); }
            }
        }

        private void Pin2Btn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (Pin2Btn.ForeColor == System.Drawing.Color.Black) { Pin2Btn.ForeColor = System.Drawing.Color.Red; dataOut = "!0201"; serialPort1.WriteLine(dataOut); }
                else { Pin2Btn.ForeColor = System.Drawing.Color.Black; dataOut = "!0200"; serialPort1.WriteLine(dataOut); }
            }
        }

        private void Pin3Btn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (Pin3Btn.ForeColor == System.Drawing.Color.Black) { Pin3Btn.ForeColor = System.Drawing.Color.Red; dataOut = "!0301"; serialPort1.WriteLine(dataOut); }
                else { Pin3Btn.ForeColor = System.Drawing.Color.Black; dataOut = "!0300"; serialPort1.WriteLine(dataOut); }
            }
        }

        private void Pin4Btn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (Pin4Btn.ForeColor == System.Drawing.Color.Black) { Pin4Btn.ForeColor = System.Drawing.Color.Red; dataOut = "!0401"; serialPort1.WriteLine(dataOut); }
                else { Pin4Btn.ForeColor = System.Drawing.Color.Black; dataOut = "!0400"; serialPort1.WriteLine(dataOut); }
            }
        }

        private void Pin5Btn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (Pin5Btn.ForeColor == System.Drawing.Color.Black) { Pin5Btn.ForeColor = System.Drawing.Color.Red; dataOut = "!0501"; serialPort1.WriteLine(dataOut); }
                else { Pin5Btn.ForeColor = System.Drawing.Color.Black; dataOut = "!0500"; serialPort1.WriteLine(dataOut); }
            }
        }

        private void Pin6Btn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (Pin6Btn.ForeColor == System.Drawing.Color.Black) { Pin6Btn.ForeColor = System.Drawing.Color.Red; dataOut = "!0601"; serialPort1.WriteLine(dataOut); }
                else { Pin6Btn.ForeColor = System.Drawing.Color.Black; dataOut = "!0600"; serialPort1.WriteLine(dataOut); }
            }
        }

        private void Pin7Btn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (Pin7Btn.ForeColor == System.Drawing.Color.Black) { Pin7Btn.ForeColor = System.Drawing.Color.Red; dataOut = "!0701"; serialPort1.WriteLine(dataOut); }
                else { Pin7Btn.ForeColor = System.Drawing.Color.Black; dataOut = "!0700"; serialPort1.WriteLine(dataOut); }
            }
        }

        private void Pin8Btn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (Pin8Btn.ForeColor == System.Drawing.Color.Black) { Pin8Btn.ForeColor = System.Drawing.Color.Red; dataOut = "!0801"; serialPort1.WriteLine(dataOut); }
                else { Pin8Btn.ForeColor = System.Drawing.Color.Black; dataOut = "!0800"; serialPort1.WriteLine(dataOut); }
            }
        }

        private void Pin9Btn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (Pin9Btn.ForeColor == System.Drawing.Color.Black) { Pin9Btn.ForeColor = System.Drawing.Color.Red; dataOut = "!0901"; serialPort1.WriteLine(dataOut); }
                else { Pin9Btn.ForeColor = System.Drawing.Color.Black; dataOut = "!0900"; serialPort1.WriteLine(dataOut); }
            }
        }

        private void Pin10Btn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (Pin10Btn.ForeColor == System.Drawing.Color.Black) { Pin10Btn.ForeColor = System.Drawing.Color.Red; dataOut = "!1001"; serialPort1.WriteLine(dataOut); }
                else { Pin10Btn.ForeColor = System.Drawing.Color.Black; dataOut = "!1000"; serialPort1.WriteLine(dataOut); }
            }
        }

        private void Pin11Btn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (Pin11Btn.ForeColor == System.Drawing.Color.Black) { Pin11Btn.ForeColor = System.Drawing.Color.Red; dataOut = "!1101"; serialPort1.WriteLine(dataOut); }
                else { Pin11Btn.ForeColor = System.Drawing.Color.Black; dataOut = "!1100"; serialPort1.WriteLine(dataOut); }
            }
        }

        private void Pin12Btn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (Pin12Btn.ForeColor == System.Drawing.Color.Black) { Pin12Btn.ForeColor = System.Drawing.Color.Red; dataOut = "!1201"; serialPort1.WriteLine(dataOut); }
                else { Pin12Btn.ForeColor = System.Drawing.Color.Black; dataOut = "!1200"; serialPort1.WriteLine(dataOut); }
            }
        }

        private void Pin13Btn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (Pin13Btn.ForeColor == System.Drawing.Color.Black) { Pin13Btn.ForeColor = System.Drawing.Color.Red; dataOut = "!1301"; serialPort1.WriteLine(dataOut); }
                else { Pin13Btn.ForeColor = System.Drawing.Color.Black; dataOut = "!1300"; serialPort1.WriteLine(dataOut); }
            }
        }

        // Polling select pins on polling tab
        private void pin0PollSelect_Click(object sender, EventArgs e)
        {
            if (pin0PollSelect.ForeColor == System.Drawing.Color.Black) { pin0PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pin0PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pin1PollSelect_Click(object sender, EventArgs e)
        {
            if (pin1PollSelect.ForeColor == System.Drawing.Color.Black) { pin1PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pin1PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pin2PollSelect_Click(object sender, EventArgs e)
        {
            if (pin2PollSelect.ForeColor == System.Drawing.Color.Black) { pin2PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pin2PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pin3PollSelect_Click(object sender, EventArgs e)
        {
            if (pin3PollSelect.ForeColor == System.Drawing.Color.Black) { pin3PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pin3PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pin4PollSelect_Click(object sender, EventArgs e)
        {
            if (pin4PollSelect.ForeColor == System.Drawing.Color.Black) { pin4PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pin4PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pin5PollSelect_Click(object sender, EventArgs e)
        {
            if (pin5PollSelect.ForeColor == System.Drawing.Color.Black) { pin5PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pin5PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pin6PollSelect_Click(object sender, EventArgs e)
        {
            if (pin6PollSelect.ForeColor == System.Drawing.Color.Black) { pin6PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pin6PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pin7PollSelect_Click(object sender, EventArgs e)
        {
            if (pin7PollSelect.ForeColor == System.Drawing.Color.Black) { pin7PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pin7PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pin8PollSelect_Click(object sender, EventArgs e)
        {
            if (pin8PollSelect.ForeColor == System.Drawing.Color.Black) { pin8PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pin8PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pin9PollSelect_Click(object sender, EventArgs e)
        {
            if (pin9PollSelect.ForeColor == System.Drawing.Color.Black) { pin9PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pin9PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pin10PollSelect_Click(object sender, EventArgs e)
        {
            if (pin10PollSelect.ForeColor == System.Drawing.Color.Black) { pin10PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pin10PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pin11PollSelect_Click(object sender, EventArgs e)
        {
            if (pin11PollSelect.ForeColor == System.Drawing.Color.Black) { pin11PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pin11PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pin12PollSelect_Click(object sender, EventArgs e)
        {
            if (pin12PollSelect.ForeColor == System.Drawing.Color.Black) { pin12PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pin12PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pin13PollSelect_Click(object sender, EventArgs e)
        {
            if (pin13PollSelect.ForeColor == System.Drawing.Color.Black) { pin13PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pin13PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pinA0PollSelect_Click(object sender, EventArgs e)
        {
            if (pinA0PollSelect.ForeColor == System.Drawing.Color.Black) { pinA0PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pinA0PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pinA1PollSelect_Click(object sender, EventArgs e)
        {
            if (pinA1PollSelect.ForeColor == System.Drawing.Color.Black) { pinA1PollSelect.ForeColor = System.Drawing.Color.Red; }
            else { pinA1PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pinA2PollSelect_Click(object sender, EventArgs e)
        {
            if (pinA2PollSelect.ForeColor == System.Drawing.Color.Black) { pinA2PollSelect.ForeColor = System.Drawing.Color.Red; }
                else {  pinA2PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pinA3PollSelect_Click(object sender, EventArgs e)
        {
            if (pinA3PollSelect.ForeColor == System.Drawing.Color.Black) { pinA3PollSelect.ForeColor = System.Drawing.Color.Red; }
                else { pinA3PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pinA4PollSelect_Click(object sender, EventArgs e)
        {
            if (pinA4PollSelect.ForeColor == System.Drawing.Color.Black) { pinA4PollSelect.ForeColor = System.Drawing.Color.Red; }
                else { pinA4PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        private void pinA5PollSelect_Click(object sender, EventArgs e)
        {
            if (pinA5PollSelect.ForeColor == System.Drawing.Color.Black) { pinA5PollSelect.ForeColor = System.Drawing.Color.Red; }
                else { pinA5PollSelect.ForeColor = System.Drawing.Color.Black; }
        }

        //Polling timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                switch (pollNext)
                {
                    case 0: if (pin0PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?00"; serialPort1.WriteLine(dataOut); } break;
                    case 1: if (pin1PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?01"; serialPort1.WriteLine(dataOut); } break;
                    case 2: if (pin2PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?02"; serialPort1.WriteLine(dataOut); } break;
                    case 3: if (pin3PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?03"; serialPort1.WriteLine(dataOut); } break;
                    case 4: if (pin4PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?04"; serialPort1.WriteLine(dataOut); } break;
                    case 5: if (pin5PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?05"; serialPort1.WriteLine(dataOut); } break;
                    case 6: if (pin6PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?06"; serialPort1.WriteLine(dataOut); } break;
                    case 7: if (pin7PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?07"; serialPort1.WriteLine(dataOut); } break;
                    case 8: if (pin8PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?08"; serialPort1.WriteLine(dataOut); } break;
                    case 9: if (pin9PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?09"; serialPort1.WriteLine(dataOut); } break;
                    case 10: if (pin10PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?10"; serialPort1.WriteLine(dataOut); } break;
                    case 11: if (pin11PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?11"; serialPort1.WriteLine(dataOut); } break;
                    case 12: if (pin12PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?12"; serialPort1.WriteLine(dataOut); } break;
                    case 13: if (pin13PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?13"; serialPort1.WriteLine(dataOut); } break;


                    case 14: if (pinA0PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?14"; serialPort1.WriteLine(dataOut); } break;
                    case 15: if (pinA1PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?15"; serialPort1.WriteLine(dataOut); } break;
                    case 16: if (pinA2PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?16"; serialPort1.WriteLine(dataOut); } break;
                    case 17: if (pinA3PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?17"; serialPort1.WriteLine(dataOut); } break;
                    case 18: if (pinA4PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?18"; serialPort1.WriteLine(dataOut); } break;
                    case 19: if (pinA5PollSelect.ForeColor == System.Drawing.Color.Red) { dataOut = "?19"; serialPort1.WriteLine(dataOut); } break;
                }

                pollNext++; if (pollNext >= 20) { pollNext = 0; }
            }
        }

        

        
    }
}
