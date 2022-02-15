namespace makinwave
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.comboBoxAvailablePorts = new System.Windows.Forms.ComboBox();
            this.buttonTrig = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.buttonConnect = new System.Windows.Forms.Button();
            this.groupBoxConnect = new System.Windows.Forms.GroupBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.groupBoxFlash = new System.Windows.Forms.GroupBox();
            this.checkBoxViewConsole = new System.Windows.Forms.CheckBox();
            this.checkBoxViewWaves = new System.Windows.Forms.CheckBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.chartWaveform = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBoxWave = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTipTrigger = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxWaves = new System.Windows.Forms.GroupBox();
            this.textBoxDutyCycle = new System.Windows.Forms.TextBox();
            this.textBoxOffset = new System.Windows.Forms.TextBox();
            this.radioButtonGaussian = new System.Windows.Forms.RadioButton();
            this.textBoxAmplitude = new System.Windows.Forms.TextBox();
            this.buttonSaveWave = new System.Windows.Forms.Button();
            this.textBoxFreq = new System.Windows.Forms.TextBox();
            this.radioButtonTriangle = new System.Windows.Forms.RadioButton();
            this.textBoxdGenLabel = new System.Windows.Forms.TextBox();
            this.buttonGenWave = new System.Windows.Forms.Button();
            this.radioButtonSine = new System.Windows.Forms.RadioButton();
            this.radioButtonSquare = new System.Windows.Forms.RadioButton();
            this.buttonSend = new System.Windows.Forms.Button();
            this.commandLine = new System.Windows.Forms.TextBox();
            this.textBoxConsole = new System.Windows.Forms.TextBox();
            this.groupBoxConsole = new System.Windows.Forms.GroupBox();
            this.groupBoxConnect.SuspendLayout();
            this.groupBoxFlash.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartWaveform)).BeginInit();
            this.groupBoxWaves.SuspendLayout();
            this.groupBoxConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxAvailablePorts
            // 
            this.comboBoxAvailablePorts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxAvailablePorts.FormattingEnabled = true;
            this.comboBoxAvailablePorts.Location = new System.Drawing.Point(70, 17);
            this.comboBoxAvailablePorts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxAvailablePorts.Name = "comboBoxAvailablePorts";
            this.comboBoxAvailablePorts.Size = new System.Drawing.Size(235, 25);
            this.comboBoxAvailablePorts.TabIndex = 0;
            this.comboBoxAvailablePorts.DropDown += new System.EventHandler(this.comboBoxAvailablePorts_DropDown);
            this.comboBoxAvailablePorts.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // buttonTrig
            // 
            this.buttonTrig.BackColor = System.Drawing.Color.Gray;
            this.buttonTrig.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTrig.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonTrig.Location = new System.Drawing.Point(185, 17);
            this.buttonTrig.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonTrig.Name = "buttonTrig";
            this.buttonTrig.Size = new System.Drawing.Size(176, 75);
            this.buttonTrig.TabIndex = 8;
            this.buttonTrig.Text = "PLAY WAVE (press and hold)";
            this.toolTipTrigger.SetToolTip(this.buttonTrig, "Press, Hold and then Release");
            this.buttonTrig.UseVisualStyleBackColor = false;
            this.buttonTrig.Click += new System.EventHandler(this.buttonTrig_Click_1);
            this.buttonTrig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonTrig_MouseDown);
            this.buttonTrig.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonTrig_MouseUp);
            // 
            // buttonLoad
            // 
            this.buttonLoad.BackColor = System.Drawing.Color.Gray;
            this.buttonLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoad.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonLoad.Location = new System.Drawing.Point(11, 17);
            this.buttonLoad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(74, 75);
            this.buttonLoad.TabIndex = 9;
            this.buttonLoad.Text = "Load File";
            this.buttonLoad.UseVisualStyleBackColor = false;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.BackColor = System.Drawing.Color.Gray;
            this.buttonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClear.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonClear.Location = new System.Drawing.Point(98, 17);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(74, 75);
            this.buttonClear.TabIndex = 10;
            this.buttonClear.Text = "Erase Wave";
            this.buttonClear.UseVisualStyleBackColor = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.Color.Gray;
            this.buttonConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnect.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonConnect.Location = new System.Drawing.Point(190, 52);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(115, 41);
            this.buttonConnect.TabIndex = 14;
            this.buttonConnect.Text = "CONNECT";
            this.buttonConnect.UseVisualStyleBackColor = false;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // groupBoxConnect
            // 
            this.groupBoxConnect.Controls.Add(this.buttonRefresh);
            this.groupBoxConnect.Controls.Add(this.comboBoxAvailablePorts);
            this.groupBoxConnect.Controls.Add(this.buttonConnect);
            this.groupBoxConnect.Location = new System.Drawing.Point(4, 15);
            this.groupBoxConnect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxConnect.Name = "groupBoxConnect";
            this.groupBoxConnect.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxConnect.Size = new System.Drawing.Size(363, 117);
            this.groupBoxConnect.TabIndex = 15;
            this.groupBoxConnect.TabStop = false;
            this.groupBoxConnect.Text = "Connect";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.Gray;
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRefresh.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonRefresh.Location = new System.Drawing.Point(70, 52);
            this.buttonRefresh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(115, 41);
            this.buttonRefresh.TabIndex = 15;
            this.buttonRefresh.Text = "REFRESH";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // groupBoxFlash
            // 
            this.groupBoxFlash.Controls.Add(this.checkBoxViewConsole);
            this.groupBoxFlash.Controls.Add(this.checkBoxViewWaves);
            this.groupBoxFlash.Controls.Add(this.buttonTrig);
            this.groupBoxFlash.Controls.Add(this.radioButton1);
            this.groupBoxFlash.Controls.Add(this.buttonLoad);
            this.groupBoxFlash.Controls.Add(this.buttonClear);
            this.groupBoxFlash.Location = new System.Drawing.Point(8, 2);
            this.groupBoxFlash.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxFlash.Name = "groupBoxFlash";
            this.groupBoxFlash.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxFlash.Size = new System.Drawing.Size(734, 106);
            this.groupBoxFlash.TabIndex = 17;
            this.groupBoxFlash.TabStop = false;
            this.groupBoxFlash.Visible = false;
            // 
            // checkBoxViewConsole
            // 
            this.checkBoxViewConsole.AutoSize = true;
            this.checkBoxViewConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxViewConsole.Location = new System.Drawing.Point(376, 49);
            this.checkBoxViewConsole.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxViewConsole.Name = "checkBoxViewConsole";
            this.checkBoxViewConsole.Size = new System.Drawing.Size(124, 24);
            this.checkBoxViewConsole.TabIndex = 36;
            this.checkBoxViewConsole.Text = "View Console";
            this.checkBoxViewConsole.UseVisualStyleBackColor = true;
            this.checkBoxViewConsole.CheckedChanged += new System.EventHandler(this.checkBoxViewConsole_CheckedChanged);
            // 
            // checkBoxViewWaves
            // 
            this.checkBoxViewWaves.AutoSize = true;
            this.checkBoxViewWaves.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxViewWaves.Location = new System.Drawing.Point(376, 17);
            this.checkBoxViewWaves.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxViewWaves.Name = "checkBoxViewWaves";
            this.checkBoxViewWaves.Size = new System.Drawing.Size(114, 24);
            this.checkBoxViewWaves.TabIndex = 35;
            this.checkBoxViewWaves.Text = "View Waves";
            this.checkBoxViewWaves.UseVisualStyleBackColor = true;
            this.checkBoxViewWaves.CheckedChanged += new System.EventHandler(this.checkBoxViewWaves_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(524, 18);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(34, 21);
            this.radioButton1.TabIndex = 25;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "0";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Visible = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // chartWaveform
            // 
            this.chartWaveform.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea4.Name = "ChartArea1";
            this.chartWaveform.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartWaveform.Legends.Add(legend4);
            this.chartWaveform.Location = new System.Drawing.Point(11, 18);
            this.chartWaveform.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chartWaveform.Name = "chartWaveform";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartWaveform.Series.Add(series4);
            this.chartWaveform.Size = new System.Drawing.Size(712, 300);
            this.chartWaveform.TabIndex = 0;
            this.chartWaveform.Text = "chart1";
            // 
            // textBoxWave
            // 
            this.textBoxWave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWave.Location = new System.Drawing.Point(104, 339);
            this.textBoxWave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxWave.Multiline = true;
            this.textBoxWave.Name = "textBoxWave";
            this.textBoxWave.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxWave.Size = new System.Drawing.Size(626, 72);
            this.textBoxWave.TabIndex = 34;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // groupBoxWaves
            // 
            this.groupBoxWaves.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxWaves.Controls.Add(this.textBoxdGenLabel);
            this.groupBoxWaves.Controls.Add(this.textBoxDutyCycle);
            this.groupBoxWaves.Controls.Add(this.chartWaveform);
            this.groupBoxWaves.Controls.Add(this.radioButtonTriangle);
            this.groupBoxWaves.Controls.Add(this.buttonGenWave);
            this.groupBoxWaves.Controls.Add(this.radioButtonSine);
            this.groupBoxWaves.Controls.Add(this.textBoxWave);
            this.groupBoxWaves.Controls.Add(this.textBoxOffset);
            this.groupBoxWaves.Controls.Add(this.buttonSaveWave);
            this.groupBoxWaves.Controls.Add(this.textBoxFreq);
            this.groupBoxWaves.Controls.Add(this.textBoxAmplitude);
            this.groupBoxWaves.Controls.Add(this.radioButtonSquare);
            this.groupBoxWaves.Controls.Add(this.radioButtonGaussian);
            this.groupBoxWaves.Location = new System.Drawing.Point(8, 118);
            this.groupBoxWaves.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxWaves.Name = "groupBoxWaves";
            this.groupBoxWaves.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxWaves.Size = new System.Drawing.Size(734, 543);
            this.groupBoxWaves.TabIndex = 28;
            this.groupBoxWaves.TabStop = false;
            this.groupBoxWaves.Text = "Edit";
            this.groupBoxWaves.Visible = false;
            // 
            // textBoxDutyCycle
            // 
            this.textBoxDutyCycle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxDutyCycle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDutyCycle.Location = new System.Drawing.Point(609, 467);
            this.textBoxDutyCycle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxDutyCycle.Name = "textBoxDutyCycle";
            this.textBoxDutyCycle.Size = new System.Drawing.Size(99, 26);
            this.textBoxDutyCycle.TabIndex = 49;
            this.textBoxDutyCycle.Text = "97";
            this.textBoxDutyCycle.Visible = false;
            this.textBoxDutyCycle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDutyCycle_KeyPress);
            // 
            // textBoxOffset
            // 
            this.textBoxOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxOffset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOffset.Location = new System.Drawing.Point(495, 467);
            this.textBoxOffset.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxOffset.Name = "textBoxOffset";
            this.textBoxOffset.Size = new System.Drawing.Size(88, 26);
            this.textBoxOffset.TabIndex = 46;
            this.textBoxOffset.Text = "0";
            this.textBoxOffset.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxOffset_KeyPress);
            // 
            // radioButtonGaussian
            // 
            this.radioButtonGaussian.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonGaussian.AutoSize = true;
            this.radioButtonGaussian.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonGaussian.Location = new System.Drawing.Point(106, 512);
            this.radioButtonGaussian.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButtonGaussian.Name = "radioButtonGaussian";
            this.radioButtonGaussian.Size = new System.Drawing.Size(86, 21);
            this.radioButtonGaussian.TabIndex = 48;
            this.radioButtonGaussian.TabStop = true;
            this.radioButtonGaussian.Text = "Gaussian";
            this.radioButtonGaussian.UseVisualStyleBackColor = true;
            this.radioButtonGaussian.CheckedChanged += new System.EventHandler(this.radioButtonGaussian_CheckedChanged);
            // 
            // textBoxAmplitude
            // 
            this.textBoxAmplitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxAmplitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAmplitude.Location = new System.Drawing.Point(376, 467);
            this.textBoxAmplitude.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxAmplitude.Name = "textBoxAmplitude";
            this.textBoxAmplitude.Size = new System.Drawing.Size(88, 26);
            this.textBoxAmplitude.TabIndex = 45;
            this.textBoxAmplitude.Text = "4095";
            this.textBoxAmplitude.TextChanged += new System.EventHandler(this.textBoxAmplitude_TextChanged);
            this.textBoxAmplitude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxAmplitude_KeyPress);
            // 
            // buttonSaveWave
            // 
            this.buttonSaveWave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveWave.BackColor = System.Drawing.Color.Gray;
            this.buttonSaveWave.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveWave.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSaveWave.Location = new System.Drawing.Point(17, 339);
            this.buttonSaveWave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSaveWave.Name = "buttonSaveWave";
            this.buttonSaveWave.Size = new System.Drawing.Size(74, 72);
            this.buttonSaveWave.TabIndex = 39;
            this.buttonSaveWave.Text = "Save Wave";
            this.buttonSaveWave.UseVisualStyleBackColor = false;
            this.buttonSaveWave.Click += new System.EventHandler(this.buttonSaveWave_Click);
            // 
            // textBoxFreq
            // 
            this.textBoxFreq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFreq.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFreq.Location = new System.Drawing.Point(265, 467);
            this.textBoxFreq.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxFreq.Name = "textBoxFreq";
            this.textBoxFreq.Size = new System.Drawing.Size(79, 26);
            this.textBoxFreq.TabIndex = 44;
            this.textBoxFreq.Text = "30.5";
            this.textBoxFreq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxFreq_KeyPress);
            // 
            // radioButtonTriangle
            // 
            this.radioButtonTriangle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonTriangle.AutoSize = true;
            this.radioButtonTriangle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonTriangle.Location = new System.Drawing.Point(106, 494);
            this.radioButtonTriangle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButtonTriangle.Name = "radioButtonTriangle";
            this.radioButtonTriangle.Size = new System.Drawing.Size(78, 21);
            this.radioButtonTriangle.TabIndex = 47;
            this.radioButtonTriangle.TabStop = true;
            this.radioButtonTriangle.Text = "Triangle";
            this.radioButtonTriangle.UseVisualStyleBackColor = true;
            this.radioButtonTriangle.CheckedChanged += new System.EventHandler(this.radioButtonTriangle_CheckedChanged);
            // 
            // textBoxdGenLabel
            // 
            this.textBoxdGenLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxdGenLabel.BackColor = System.Drawing.Color.Gray;
            this.textBoxdGenLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxdGenLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxdGenLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBoxdGenLabel.Location = new System.Drawing.Point(105, 436);
            this.textBoxdGenLabel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxdGenLabel.Name = "textBoxdGenLabel";
            this.textBoxdGenLabel.ReadOnly = true;
            this.textBoxdGenLabel.Size = new System.Drawing.Size(625, 19);
            this.textBoxdGenLabel.TabIndex = 27;
            this.textBoxdGenLabel.Text = "  Shape                                 Freq                Max Ampl            D" +
    "C Offset         Duty Cycle (%)";
            this.textBoxdGenLabel.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // buttonGenWave
            // 
            this.buttonGenWave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGenWave.BackColor = System.Drawing.Color.Gray;
            this.buttonGenWave.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGenWave.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonGenWave.Location = new System.Drawing.Point(17, 436);
            this.buttonGenWave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonGenWave.Name = "buttonGenWave";
            this.buttonGenWave.Size = new System.Drawing.Size(74, 72);
            this.buttonGenWave.TabIndex = 41;
            this.buttonGenWave.Text = "Make Wave";
            this.buttonGenWave.UseVisualStyleBackColor = false;
            this.buttonGenWave.Click += new System.EventHandler(this.buttonGenWave_Click);
            // 
            // radioButtonSine
            // 
            this.radioButtonSine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonSine.AutoSize = true;
            this.radioButtonSine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSine.Location = new System.Drawing.Point(106, 477);
            this.radioButtonSine.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButtonSine.Name = "radioButtonSine";
            this.radioButtonSine.Size = new System.Drawing.Size(54, 21);
            this.radioButtonSine.TabIndex = 43;
            this.radioButtonSine.TabStop = true;
            this.radioButtonSine.Text = "Sine";
            this.radioButtonSine.UseVisualStyleBackColor = true;
            // 
            // radioButtonSquare
            // 
            this.radioButtonSquare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonSquare.AutoSize = true;
            this.radioButtonSquare.Checked = true;
            this.radioButtonSquare.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSquare.Location = new System.Drawing.Point(106, 459);
            this.radioButtonSquare.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButtonSquare.Name = "radioButtonSquare";
            this.radioButtonSquare.Size = new System.Drawing.Size(72, 21);
            this.radioButtonSquare.TabIndex = 42;
            this.radioButtonSquare.TabStop = true;
            this.radioButtonSquare.Text = "Square";
            this.radioButtonSquare.UseVisualStyleBackColor = true;
            this.radioButtonSquare.CheckedChanged += new System.EventHandler(this.radioButtonSquare_CheckedChanged);
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.BackColor = System.Drawing.Color.Gray;
            this.buttonSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSend.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSend.Location = new System.Drawing.Point(717, 614);
            this.buttonSend.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(76, 31);
            this.buttonSend.TabIndex = 3;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = false;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // commandLine
            // 
            this.commandLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commandLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandLine.Location = new System.Drawing.Point(10, 614);
            this.commandLine.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.commandLine.Name = "commandLine";
            this.commandLine.Size = new System.Drawing.Size(703, 26);
            this.commandLine.TabIndex = 2;
            this.commandLine.TextChanged += new System.EventHandler(this.commandLine_TextChanged);
            this.commandLine.Enter += new System.EventHandler(this.commandLine_Enter);
            this.commandLine.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.commandLine_KeyPress_1);
            // 
            // textBoxConsole
            // 
            this.textBoxConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConsole.Location = new System.Drawing.Point(10, 0);
            this.textBoxConsole.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxConsole.Multiline = true;
            this.textBoxConsole.Name = "textBoxConsole";
            this.textBoxConsole.ReadOnly = true;
            this.textBoxConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxConsole.Size = new System.Drawing.Size(784, 608);
            this.textBoxConsole.TabIndex = 4;
            // 
            // groupBoxConsole
            // 
            this.groupBoxConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxConsole.Controls.Add(this.textBoxConsole);
            this.groupBoxConsole.Controls.Add(this.commandLine);
            this.groupBoxConsole.Controls.Add(this.buttonSend);
            this.groupBoxConsole.Location = new System.Drawing.Point(746, 8);
            this.groupBoxConsole.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxConsole.Name = "groupBoxConsole";
            this.groupBoxConsole.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxConsole.Size = new System.Drawing.Size(797, 653);
            this.groupBoxConsole.TabIndex = 27;
            this.groupBoxConsole.TabStop = false;
            this.groupBoxConsole.Visible = false;
            this.groupBoxConsole.Enter += new System.EventHandler(this.groupBoxConsole_Enter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1551, 673);
            this.Controls.Add(this.groupBoxWaves);
            this.Controls.Add(this.groupBoxConsole);
            this.Controls.Add(this.groupBoxFlash);
            this.Controls.Add(this.groupBoxConnect);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "MakinWaves";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxConnect.ResumeLayout(false);
            this.groupBoxFlash.ResumeLayout(false);
            this.groupBoxFlash.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartWaveform)).EndInit();
            this.groupBoxWaves.ResumeLayout(false);
            this.groupBoxWaves.PerformLayout();
            this.groupBoxConsole.ResumeLayout(false);
            this.groupBoxConsole.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxAvailablePorts;
        private System.Windows.Forms.Button buttonTrig;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonClear;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.GroupBox groupBoxConnect;
        private System.Windows.Forms.GroupBox groupBoxFlash;
        private System.Windows.Forms.Button buttonRefresh;

        private const string G_prompt = "=>";
        private string G_defaultFilename = "";
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolTip toolTipTrigger;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartWaveform;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox textBoxWave;
        private System.Windows.Forms.CheckBox checkBoxViewWaves;
        private System.Windows.Forms.CheckBox checkBoxViewConsole;
        private System.Windows.Forms.GroupBox groupBoxWaves;
        private System.Windows.Forms.Button buttonSaveWave;
        private System.Windows.Forms.TextBox textBoxAmplitude;
        private System.Windows.Forms.TextBox textBoxFreq;
        private System.Windows.Forms.RadioButton radioButtonSine;
        private System.Windows.Forms.RadioButton radioButtonSquare;
        private System.Windows.Forms.Button buttonGenWave;
        private System.Windows.Forms.TextBox textBoxdGenLabel;
        private System.Windows.Forms.TextBox textBoxOffset;
        private System.Windows.Forms.RadioButton radioButtonTriangle;
        private System.Windows.Forms.RadioButton radioButtonGaussian;
        private System.Windows.Forms.TextBox textBoxDutyCycle;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox commandLine;
        private System.Windows.Forms.TextBox textBoxConsole;
        private System.Windows.Forms.GroupBox groupBoxConsole;
    }
}

