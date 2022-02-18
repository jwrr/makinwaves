using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;

namespace makinwave
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 500;
            getAvailablePorts();
        }

        bool getAvailablePorts()
        {
            timer1.Stop();
            bool fail = false;
            this.Text = this.Text.Split('-')[0];
            this.Text = this.Text.Trim();
            groupBoxFlash.Visible = false;
            groupBoxWaves.Visible = false;
            groupBoxConnect.Visible = true;
            buttonRefresh.Visible = true;
            buttonRefresh.Enabled = true;
            buttonConnect.Visible = false;
            comboBoxAvailablePorts.Items.Clear();
            string[] ports = SerialPort.GetPortNames();

            string portName = serialPort1.PortName;

            // the ports list contains the current port, but the
            // current port close (for some reason). Check to see
            // if the port can be opened. If its not the mark it
            // as not connected.
            bool openSuccess = true;
            if (ports.Contains(portName))
            {
                serialPort1.Close();
                openSuccess = OpenSerialPort(portName);
                if (openSuccess)
                {
                    serialPort1.Close();
                }
                else
                {
                    int i = Array.IndexOf(ports, portName);
                    ports[i] += " Not Connected";
                    fail = true;
                }
            }

            int portsCount = ports.Count();
            if (portsCount == 0)
            {
                Array.Resize(ref ports, 1);    
                ports[0] = "No Ports Found";
                fail = true;
            }
            comboBoxAvailablePorts.Items.AddRange(ports);
            comboBoxAvailablePorts.SelectedIndex = 0;
            buttonConnect.Visible = (portsCount > 1) || (portsCount == 1 && openSuccess);
            bool success = !fail;
            return success;
        }

        void restartApp(String msg = "Connection failure")
        {
            if (msg != "")
            {
                MessageBox.Show("Error: " + msg);
            }
            Application.Restart();
            Environment.Exit(0);

            //Environment.Exit(1);
            // getAvailablePorts();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private bool OpenSerialPort(string portName)
        {
            bool success = false;
            try
            {
                serialPort1.PortName = portName;
                serialPort1.BaudRate = 9600;
                serialPort1.Open();
                success = true;
            }
            catch (UnauthorizedAccessException)
            {
                success = false;
            }
            catch (IOException)
            {
                success = false;
                restartApp("Failed to open port");
            }
            return success;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            string portName = comboBoxAvailablePorts.Text;
            bool success = OpenSerialPort(portName);
            if (success)
            {
                groupBoxConnect.Visible = false;
                groupBoxFlash.Visible = true;
                textBoxConsole.Enabled = true;
                textBoxConsole.AppendText("Connected to " + portName + Environment.NewLine + "=> ");
                this.Text += " - " + portName;
                getStatus();
            }
        }

        private bool rcvFromDevice(string rxTerminationStr = "", bool updateStatusBox = false)
        {
            bool rxTerminatorFound = false;
            int timeoutCnt = 0;
            while (!rxTerminatorFound)
            {
                timeoutCnt++;
                if (timeoutCnt > 100000)
                {
                    restartApp("Timeout reading port");
                    return false; // fail
                }
                string rxStr = "";
                try
                {
                    rxStr = serialPort1.ReadExisting();
                }
                catch
                {
                    restartApp("Failed reading port");
                }
                if (rxStr != "")
                {
                    rxTerminatorFound = rxStr.Contains(rxTerminationStr);
                    rxStr = rxStr.Replace("\r", "");
                    rxStr = rxStr.Replace("\n", Environment.NewLine);
                    if (textBoxConsole.Enabled)
                    {
                        textBoxConsole.AppendText(rxStr);
                    }
                    if (updateStatusBox)
                    {
                        rxStr = rxStr.Replace(Environment.NewLine + "=>" , "");
                        rxStr = rxStr.Replace("         ", "");
                        rxStr = rxStr.Replace("=>", "");
                        List<string> rxLines = rxStr.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();

                        int lastPos = rxLines.Count - 1;
                        while (lastPos > 0 && String.IsNullOrWhiteSpace(rxLines[lastPos])) lastPos--;

                        rxStr = string.Join(Environment.NewLine, rxLines.Take(lastPos-1));
                    }
                }
            }
            return true; // success
        }

        private bool sendCommandLine(string rxTerminationStr = G_prompt, bool updateStatusBox = false)
        {
            String cmd = commandLine.Text;
            try
            {
                serialPort1.WriteLine(cmd + "\n\r"); // FIXME try/catch needed to handle disconnect excepion System.InvalidOperationException: 'The port is closed.'
            }
            catch
            {
                restartApp("");
                //restartApp("Failed writing to port");
            }

            if (textBoxConsole.Enabled)
            {
                textBoxConsole.AppendText(cmd + Environment.NewLine);
            }
            bool success = rcvFromDevice(rxTerminationStr, updateStatusBox);
            commandLine.Text = "";
            return success;
        }

        private void handleSend()
        {
            sendCommandLine(G_prompt);
            getStatus();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            handleSend();
        }

        private void commandLine_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }

        private string[] slurpFile()
        {
            string[] lines = {};
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (G_defaultFilename != "")
            {
                openFileDialog1.FileName = G_defaultFilename;
            }
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                try
                {
                    lines = File.ReadAllLines(file);
                    G_defaultFilename = file;
                }
                catch (IOException)
                {
                }
            }
            return lines;
        }

        private RadioButton getCheckedRadio(Control container)
        {
            //Debug.WriteLine("In getCheckedRadio");
            foreach (var c in container.Controls)
            {
                RadioButton r = c as RadioButton;
                if (r != null && r.Checked)
                {
                    //Debug.WriteLine("radio found " + r.Text);
                    return r;
                }
            }
            //Debug.WriteLine("radio not found");
            return null;
        }

        private void sendChangeMode(string newMode)
        {
            commandLine.Text = newMode;
            sendCommandLine(G_prompt);
            getStatus();
        }

        private void sendChangeModeRadio()
        {
            RadioButton r = getCheckedRadio(groupBoxFlash);
            if (r == null) return;

            string mode = r.Text;
            sendChangeMode(mode);
        }

        private void writeWave()
        {
            sendChangeModeRadio();
            commandLine.Text = "w";
            sendCommandLine(G_prompt);
            string[] samples = slurpFile();
            Debug.WriteLine("num samples = " + samples.Length);
            bool endFound = false;
            foreach (string sample in samples)
            {
                string sampleLower = sample.ToLower();
                commandLine.Text = sampleLower;
                sendCommandLine(G_prompt);
                if (sampleLower == "-1" || sampleLower == "end")
                {
                    endFound = true;
                    break;
                }
            }
            if (!endFound)
            {
                commandLine.Text = "end";
                sendCommandLine(G_prompt);
            }
        }

        private void sendEraseCommand(string modeStr = "")
        {
            if (modeStr == "")
            {
                sendChangeModeRadio();
            }
            else
            {
                commandLine.Text = modeStr;
                sendCommandLine(G_prompt);
            }
            commandLine.Text = "e";
            sendCommandLine(G_prompt);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            writeWave();
            readWave();
            getStatus();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            RadioButton r = getCheckedRadio(groupBoxFlash);
            if (r == null) return;
            string mode = r.Text;
            sendEraseCommand(mode);
            readWave();
            getStatus();

        }

        private void getStatus(bool textBoxConsoleEnabled = false)
        {
            Boolean orig = textBoxConsole.Enabled;
            textBoxConsole.Enabled = textBoxConsoleEnabled;
            commandLine.Text = "status";
            sendCommandLine(G_prompt,true);
            textBoxConsole.Enabled = orig;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            bool success = getAvailablePorts();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            getStatus();
            timer1.Start();
        }

        private void PlotWaveform()
        {
            //string[] lines = textBoxConsole.Text.Split('\n');
            //int len = lines.Length;
            //if (len < 2) return;
            //textBoxWave.Text = lines[len - 2];

            string[] samplesString = textBoxWave.Text.Split(',');
            List<int> samplesInt = new List<int>();
            int samplePrev = 0;
            Stack<int> addrStack = new Stack<int>();
            Stack<int> cntStack = new Stack<int>();

            for (int i = 0; i < samplesString.Length; i++)
            {
                string sample = samplesString[i].Trim().ToUpper();
                Debug.WriteLine(sample);
                if (sample.Contains("REP"))
                {
                    String[] tokens = sample.Split();
                    if (tokens.Length < 2)
                    {
                        continue;
                    }
                    String repCntStr = tokens[1];
                    int repCntInt = 0;
                    try
                    {
                        repCntInt = Int32.Parse(repCntStr);
                    }
                    catch (FormatException)
                    {
                        continue;
                    }
                    for (int j = 0; j < repCntInt; j++)
                    {
                        samplesInt.Add(samplePrev);
                    }
                    continue;
                }
                else if (sample.Contains("ENDFOR"))
                {
                    int loopAddrInt = addrStack.Pop();
                    int loopCntInt = cntStack.Pop();
                    Debug.WriteLine("ENDFOR1 " + loopAddrInt + " " + loopCntInt);
                    if (loopCntInt <= 1)
                    {
                        continue;
                    }
                    i = loopAddrInt;
                    loopCntInt--;
                    Debug.WriteLine("ENDFOR2 " + i + " " + loopCntInt);
                    addrStack.Push(loopAddrInt);
                    cntStack.Push(loopCntInt);
                    continue;
                }
                else if (sample.Contains("FOR"))
                {
                    String[] tokens = sample.Split();
                    if (tokens.Length < 2)
                    {
                        continue;
                    }
                    String loopCntStr = tokens[1];
                    int loopCntInt = 0;
                    try
                    {
                        loopCntInt = Int32.Parse(loopCntStr);
                    }
                    catch (FormatException)
                    {
                        continue;
                    }
                    Debug.WriteLine("FOR " + i + " " + loopCntInt);
                    addrStack.Push(i);
                    if (sample.Contains("EVER"))
                    {
                        cntStack.Push(1); // plot just 1 iteration of forever loop
                    }
                    else
                    {
                        cntStack.Push(loopCntInt);
                    }
                    continue;
                }
                else
                {
                    int sampleInt;
                    try
                    {
                        sampleInt = int.Parse(sample);
                    }
                    catch
                    {
                        continue;
                    }
                    samplesInt.Add(sampleInt);
                    samplePrev = sampleInt;
                }
            }
            chartWaveform.Series.Clear();
            var series = new Series("Waveform");
            series.Points.DataBindY(samplesInt);
            chartWaveform.Series.Add(series);
        }

        private void insertWaveOpCodes()
        {

            string[] lines = textBoxConsole.Text.Split('\n');
            int len = lines.Length;
            if (len < 2) return;
            textBoxWave.Text = lines[len - 2];

            string[] samplesString = textBoxWave.Text.Split(',');
            List<int> samplesInt = new List<int>();
            string outString = "";
            for (int i = 0; i < samplesString.Length; i++)
            {
                string sample = samplesString[i].Trim();
                int sampleInt = 0;

                try
                {
                    sampleInt = Int32.Parse(sample);
                }
                catch
                {
                    outString += sample + ", ";
                    continue;
                }

                if (sampleInt == 8192)
                {
                    sample = "ENDLOOP";
                }
                else if (sampleInt >= 4096)
                {
                    int loopCount = sampleInt - 4096;
                    sample = "LOOP " + loopCount;
                }
                else if (sampleInt < -1)
                {
                    int msDelay = Math.Abs(sampleInt) / 2;
                    sample = "DELAY_MS " + msDelay;
                }
                outString += sample + ", ";
            }
            textBoxWave.Text = outString;
        }

        private bool readWave()
        {
            if (checkBoxViewWaves.Checked)
            {
                sendChangeModeRadio();
                commandLine.Text = "r";
                bool success = sendCommandLine(G_prompt);
                if (success)
                {
                    insertWaveOpCodes();
                    PlotWaveform();
                }
                else
                {
                    return false;
                }
            }
            return true;
        }



        private void commandLine_Enter(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void buttonTrig_Click_1(object sender, EventArgs e)
        {
            if (buttonTrig.Text.Contains("OFF"))
            {
                buttonTrig.Text = "WAVE ON (press to stop)";
            }
            else
            {
                buttonTrig.Text = "WAVE OFF (press to play)";
            }
            commandLine.Text = "f";
            sendCommandLine(G_prompt);
            getStatus();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                readWave();
                getStatus();
            }
        }

        private void checkBoxViewWaves_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxWaves.Visible = checkBoxViewWaves.Checked;
            if (checkBoxViewWaves.Checked)
            {
                groupBoxWaves.BringToFront();
                readWave();
                getStatus();
            }
        }

        private void checkBoxViewConsole_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxConsole.Visible = checkBoxViewConsole.Checked;
            if (checkBoxViewConsole.Checked)
            {
                groupBoxConsole.BringToFront();
            }
        }

        private void groupBoxConsole_Enter(object sender, EventArgs e)
        {

        }

        private void buttonSaveWave_Click(object sender, EventArgs e)
        {
            string oneLiner = textBoxWave.Text;
            sendChangeModeRadio();
            commandLine.Text = "w";
            sendCommandLine(G_prompt);
            string[] samples = textBoxWave.Text.Split(',');
            foreach (string sample in samples)
            {
                commandLine.Text = sample.Trim();
                sendCommandLine(G_prompt);
                if (sample == "-1")
                {
                    break;
                }
            }
            readWave();
            getStatus();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void createWave(int waveType)
        {
            int sineWave     = 0;
            int squareWave = 1;
            int triangleWave = 2;
            int guassianWave = 3;

            string waveFreqStr = textBoxFreq.Text;
            Double waveFreq = 0.0;
            try
            {
                waveFreq = Double.Parse(waveFreqStr);
                if (waveFreq < 5.0)
                {
                    waveFreq = 5.0;
                    textBoxFreq.Text = "5";
                }
            }
            catch (FormatException)
            {
                return;
            }

            string maxValStr = textBoxAmplitude.Text;
            int maxVal = 4095;
            try
            {
                maxVal = int.Parse(maxValStr);
            }
            catch (FormatException)
            {
                maxVal = 4095;
            }

            string dcOffsetStr = textBoxOffset.Text;
            int dcOffset = 0;
            try
            {
                dcOffset = int.Parse(dcOffsetStr);
            }
            catch (FormatException)
            {
                dcOffset = 0;
            }

            string squareWaveDutyCycleStr = textBoxDutyCycle.Text;
            int squareWaveDutyCycle = 0;
            try
            {
                squareWaveDutyCycle = int.Parse(squareWaveDutyCycleStr);
            }
            catch (FormatException)
            {
                squareWaveDutyCycle = 50;
            }

            Double wavePeriod = 1 / waveFreq;
            Double sampleRate = 2000;
            Double samplePeriod = 1 / sampleRate;
            int samplesPerWave = (int)(wavePeriod / samplePeriod);
            List<int> wave = new List<int>();
            int wave_offset = 0;
            for (int i = 0; i < samplesPerWave; i++)
            {
                int sample = 0;
                if (waveType == sineWave)
                {
                    Double phase = 0.75 * 2.0 * Math.PI;
                    Double x = (double)i / (double)samplesPerWave;
                    Double radians = 2 * Math.PI * (x) + phase;
                    Double sin = Math.Sin(radians);
                    Double sinPositive = 1.0 + sin;
                    Double sinHalf = sinPositive / 2.0;
                    Double sinScaled = (Double)maxVal * sinHalf;
                    sample = (int)(sinScaled);
                }
                else if (waveType == squareWave)
                {
                    int samplesPerDutyCycle = squareWaveDutyCycle * samplesPerWave / 100;
                    sample = (i < samplesPerDutyCycle) ? maxVal : 0;
                }
                else if (waveType == triangleWave)
                {
                    Double x = (double)i;
                    Double slope = 2.0 / (Double)samplesPerWave;
                    Double y = slope * x;
                    y = (y > 1.0) ? 1.0 - (y - 1.0) : y;
                    Double yScaled = (Double)maxVal * y;
                    sample = (int)yScaled;
                }
                else if (waveType == guassianWave)
                {
                    Double midPoint = samplesPerWave / 2;
                    Double decayRate = samplesPerWave / 5;
                    Double x = Math.Abs((double)i - (double)midPoint) / decayRate;
                    Double xSquared = x * x;
                    Double exponent = -xSquared / 2;
                    Double y = Math.Exp(exponent);
                    Double yScaled = (Double)maxVal * y;
                    sample = (int)yScaled;
                }

                if (waveType == squareWave)
                {
                    wave_offset = 0;
                }
                else if (i == 0)
                {
                    wave_offset = sample;
                }
                sample -= wave_offset;
                sample += dcOffset;
                if (sample > 4095)
                {
                    sample = 4095;
                }
                else if (sample < 0)
                {
                    sample = 0;
                }
                wave.Add(sample);
            }
            wave.Add(-1);
            var waveStr = string.Join(", ", wave);
            textBoxWave.Text = waveStr;
        }

        private void handleGenWaveButton()
        {
            if (radioButtonSine.Checked)
            {
                createWave(0);
            }
            else if (radioButtonSquare.Checked)
            {
                createWave(1);
            }
            else if (radioButtonTriangle.Checked)
            {
                createWave(2);
            }
            else if (radioButtonGaussian.Checked)
            {
                createWave(3);
            }
            else
            {
                return;
            }
            string oneLiner = textBoxWave.Text;
            sendChangeModeRadio();
            commandLine.Text = "w";
            sendCommandLine(G_prompt);
            string[] samples = textBoxWave.Text.Split(',');
            foreach (string sample in samples)
            {
                commandLine.Text = sample;
                sendCommandLine(G_prompt);
            }
            readWave();
            getStatus();
        }


        private void buttonGenWave_Click(object sender, EventArgs e)
        {
            handleGenWaveButton();
        }

        private void radioButtonSquare_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSquare.Checked == true)
            {
                textBoxDutyCycle.Visible = true;
            }
            else
            {
                textBoxDutyCycle.Visible = false;
            }
        }

        private void commandLine_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                handleSend();
            }
        }

        private void textBoxFreq_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                handleGenWaveButton();
            }
        }

        private void textBoxAmplitude_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                handleGenWaveButton();
            }
        }

        private void textBoxOffset_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                handleGenWaveButton();
            }
        }

        private void radioButtonGaussian_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAmplitude_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxDutyCycle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                handleGenWaveButton();
            }
        }

        private void comboBoxAvailablePorts_DropDown(object sender, EventArgs e)
        {
            bool success = getAvailablePorts();
        }

        private void radioButtonTriangle_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
