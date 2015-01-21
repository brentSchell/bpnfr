namespace Gui
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblEncoderPositions = new System.Windows.Forms.Label();
            this.formChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabConfiguration = new System.Windows.Forms.TabPage();
            this.grpBoxSummary = new System.Windows.Forms.GroupBox();
            this.lblMeasurementSummary = new System.Windows.Forms.Label();
            this.grpBoxLoad = new System.Windows.Forms.GroupBox();
            this.lblLoadDescription = new System.Windows.Forms.Label();
            this.btnLoadMotors = new System.Windows.Forms.Button();
            this.grpBoxMeasurementOptions = new System.Windows.Forms.GroupBox();
            this.lblGHz = new System.Windows.Forms.Label();
            this.txtBoxFrequency = new System.Windows.Forms.TextBox();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.lblCritAngle = new System.Windows.Forms.Label();
            this.txtBoxCriticalAngle = new System.Windows.Forms.TextBox();
            this.btnApplyMeasurementOptions = new System.Windows.Forms.Button();
            this.cmbBoxMeasurementMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMeasurementMode = new System.Windows.Forms.Label();
            this.grpBoxSerialConnections = new System.Windows.Forms.GroupBox();
            this.lblVNAConnectionStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBoxVNAConnection = new System.Windows.Forms.ComboBox();
            this.btnDisconnectSerials = new System.Windows.Forms.Button();
            this.btnConnectSerials = new System.Windows.Forms.Button();
            this.lblCont2Status = new System.Windows.Forms.Label();
            this.cmbBoxCont1 = new System.Windows.Forms.ComboBox();
            this.lblEncoderStatus = new System.Windows.Forms.Label();
            this.cmbBoxCont2 = new System.Windows.Forms.ComboBox();
            this.lblCont1Status = new System.Windows.Forms.Label();
            this.lblComPort1 = new System.Windows.Forms.Label();
            this.lblCommPort2 = new System.Windows.Forms.Label();
            this.lblEncoderComPort = new System.Windows.Forms.Label();
            this.cmbBoxEncoder = new System.Windows.Forms.ComboBox();
            this.tabOperation = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnInitMotor1 = new System.Windows.Forms.Button();
            this.btnINC1 = new System.Windows.Forms.Button();
            this.tabResults = new System.Windows.Forms.TabPage();
            this.indicatorCont1Connection = new System.Windows.Forms.Button();
            this.indicatorCont2Connection = new System.Windows.Forms.Button();
            this.indicatorEncoderConnection = new System.Windows.Forms.Button();
            this.indicatorVNAConnection = new System.Windows.Forms.Button();
            this.btnEStop = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.formChart)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabConfiguration.SuspendLayout();
            this.grpBoxSummary.SuspendLayout();
            this.grpBoxLoad.SuspendLayout();
            this.grpBoxMeasurementOptions.SuspendLayout();
            this.grpBoxSerialConnections.SuspendLayout();
            this.tabOperation.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(715, 6);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Read Encoders";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblEncoderPositions
            // 
            this.lblEncoderPositions.AutoSize = true;
            this.lblEncoderPositions.Location = new System.Drawing.Point(94, 21);
            this.lblEncoderPositions.Name = "lblEncoderPositions";
            this.lblEncoderPositions.Size = new System.Drawing.Size(0, 13);
            this.lblEncoderPositions.TabIndex = 1;
            // 
            // formChart
            // 
            this.formChart.Location = new System.Drawing.Point(392, 64);
            this.formChart.Name = "formChart";
            series3.Name = "Series1";
            this.formChart.Series.Add(series3);
            this.formChart.Size = new System.Drawing.Size(300, 300);
            this.formChart.TabIndex = 2;
            this.formChart.Text = "chart1";
            this.formChart.Click += new System.EventHandler(this.chart_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabConfiguration);
            this.tabControl1.Controls.Add(this.tabOperation);
            this.tabControl1.Controls.Add(this.tabResults);
            this.tabControl1.Location = new System.Drawing.Point(17, 65);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(804, 376);
            this.tabControl1.TabIndex = 3;
            // 
            // tabConfiguration
            // 
            this.tabConfiguration.Controls.Add(this.grpBoxSummary);
            this.tabConfiguration.Controls.Add(this.grpBoxLoad);
            this.tabConfiguration.Controls.Add(this.grpBoxMeasurementOptions);
            this.tabConfiguration.Controls.Add(this.grpBoxSerialConnections);
            this.tabConfiguration.Location = new System.Drawing.Point(4, 22);
            this.tabConfiguration.Name = "tabConfiguration";
            this.tabConfiguration.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfiguration.Size = new System.Drawing.Size(796, 350);
            this.tabConfiguration.TabIndex = 0;
            this.tabConfiguration.Text = "Configuration";
            this.tabConfiguration.UseVisualStyleBackColor = true;
            // 
            // grpBoxSummary
            // 
            this.grpBoxSummary.Controls.Add(this.lblMeasurementSummary);
            this.grpBoxSummary.Location = new System.Drawing.Point(393, 108);
            this.grpBoxSummary.Name = "grpBoxSummary";
            this.grpBoxSummary.Size = new System.Drawing.Size(381, 218);
            this.grpBoxSummary.TabIndex = 12;
            this.grpBoxSummary.TabStop = false;
            this.grpBoxSummary.Text = "Summary";
            // 
            // lblMeasurementSummary
            // 
            this.lblMeasurementSummary.AutoSize = true;
            this.lblMeasurementSummary.Location = new System.Drawing.Point(6, 19);
            this.lblMeasurementSummary.Name = "lblMeasurementSummary";
            this.lblMeasurementSummary.Size = new System.Drawing.Size(172, 52);
            this.lblMeasurementSummary.TabIndex = 14;
            this.lblMeasurementSummary.Text = "Summary:\r\n\r\nScan Area Radius: N/A\r\nEstimated Measurement Time: N/A\r\n";
            this.lblMeasurementSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMeasurementSummary.Click += new System.EventHandler(this.lblMeasurementSummary_Click);
            // 
            // grpBoxLoad
            // 
            this.grpBoxLoad.Controls.Add(this.lblLoadDescription);
            this.grpBoxLoad.Controls.Add(this.btnLoadMotors);
            this.grpBoxLoad.Location = new System.Drawing.Point(393, 11);
            this.grpBoxLoad.Name = "grpBoxLoad";
            this.grpBoxLoad.Size = new System.Drawing.Size(390, 83);
            this.grpBoxLoad.TabIndex = 19;
            this.grpBoxLoad.TabStop = false;
            this.grpBoxLoad.Text = "Load to Motors";
            // 
            // lblLoadDescription
            // 
            this.lblLoadDescription.AutoSize = true;
            this.lblLoadDescription.Location = new System.Drawing.Point(6, 18);
            this.lblLoadDescription.MaximumSize = new System.Drawing.Size(250, 100);
            this.lblLoadDescription.Name = "lblLoadDescription";
            this.lblLoadDescription.Size = new System.Drawing.Size(247, 39);
            this.lblLoadDescription.TabIndex = 20;
            this.lblLoadDescription.Text = "Once all connections are made, and measurement options are configured, load these" +
    " settings to the motor controllers.";
            // 
            // btnLoadMotors
            // 
            this.btnLoadMotors.Location = new System.Drawing.Point(254, 54);
            this.btnLoadMotors.Name = "btnLoadMotors";
            this.btnLoadMotors.Size = new System.Drawing.Size(121, 23);
            this.btnLoadMotors.TabIndex = 13;
            this.btnLoadMotors.Text = "Load Motors";
            this.btnLoadMotors.UseVisualStyleBackColor = true;
            this.btnLoadMotors.Click += new System.EventHandler(this.btnLoadMotors_Click);
            // 
            // grpBoxMeasurementOptions
            // 
            this.grpBoxMeasurementOptions.Controls.Add(this.lblGHz);
            this.grpBoxMeasurementOptions.Controls.Add(this.txtBoxFrequency);
            this.grpBoxMeasurementOptions.Controls.Add(this.lblFrequency);
            this.grpBoxMeasurementOptions.Controls.Add(this.lblCritAngle);
            this.grpBoxMeasurementOptions.Controls.Add(this.txtBoxCriticalAngle);
            this.grpBoxMeasurementOptions.Controls.Add(this.btnApplyMeasurementOptions);
            this.grpBoxMeasurementOptions.Controls.Add(this.cmbBoxMeasurementMode);
            this.grpBoxMeasurementOptions.Controls.Add(this.label4);
            this.grpBoxMeasurementOptions.Controls.Add(this.lblMeasurementMode);
            this.grpBoxMeasurementOptions.Location = new System.Drawing.Point(6, 203);
            this.grpBoxMeasurementOptions.Name = "grpBoxMeasurementOptions";
            this.grpBoxMeasurementOptions.Size = new System.Drawing.Size(381, 123);
            this.grpBoxMeasurementOptions.TabIndex = 12;
            this.grpBoxMeasurementOptions.TabStop = false;
            this.grpBoxMeasurementOptions.Text = "Measurement Options";
            // 
            // lblGHz
            // 
            this.lblGHz.AutoSize = true;
            this.lblGHz.Location = new System.Drawing.Point(250, 47);
            this.lblGHz.Name = "lblGHz";
            this.lblGHz.Size = new System.Drawing.Size(28, 13);
            this.lblGHz.TabIndex = 17;
            this.lblGHz.Text = "GHz";
            this.lblGHz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxFrequency
            // 
            this.txtBoxFrequency.Location = new System.Drawing.Point(123, 47);
            this.txtBoxFrequency.Name = "txtBoxFrequency";
            this.txtBoxFrequency.Size = new System.Drawing.Size(121, 20);
            this.txtBoxFrequency.TabIndex = 18;
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Location = new System.Drawing.Point(8, 47);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(57, 13);
            this.lblFrequency.TabIndex = 16;
            this.lblFrequency.Text = "Frequency";
            // 
            // lblCritAngle
            // 
            this.lblCritAngle.AutoSize = true;
            this.lblCritAngle.Location = new System.Drawing.Point(250, 21);
            this.lblCritAngle.Name = "lblCritAngle";
            this.lblCritAngle.Size = new System.Drawing.Size(47, 13);
            this.lblCritAngle.TabIndex = 12;
            this.lblCritAngle.Text = "Degrees";
            this.lblCritAngle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxCriticalAngle
            // 
            this.txtBoxCriticalAngle.Location = new System.Drawing.Point(123, 21);
            this.txtBoxCriticalAngle.Name = "txtBoxCriticalAngle";
            this.txtBoxCriticalAngle.Size = new System.Drawing.Size(121, 20);
            this.txtBoxCriticalAngle.TabIndex = 13;
            // 
            // btnApplyMeasurementOptions
            // 
            this.btnApplyMeasurementOptions.Location = new System.Drawing.Point(253, 94);
            this.btnApplyMeasurementOptions.Name = "btnApplyMeasurementOptions";
            this.btnApplyMeasurementOptions.Size = new System.Drawing.Size(121, 23);
            this.btnApplyMeasurementOptions.TabIndex = 6;
            this.btnApplyMeasurementOptions.Text = "Apply Options";
            this.btnApplyMeasurementOptions.UseVisualStyleBackColor = true;
            this.btnApplyMeasurementOptions.Click += new System.EventHandler(this.btnApplyMeasurementOptions_Click);
            // 
            // cmbBoxMeasurementMode
            // 
            this.cmbBoxMeasurementMode.FormattingEnabled = true;
            this.cmbBoxMeasurementMode.Items.AddRange(new object[] {
            "Discrete Mode",
            "Continuous Mode"});
            this.cmbBoxMeasurementMode.Location = new System.Drawing.Point(123, 73);
            this.cmbBoxMeasurementMode.Name = "cmbBoxMeasurementMode";
            this.cmbBoxMeasurementMode.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxMeasurementMode.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Critical Angle";
            // 
            // lblMeasurementMode
            // 
            this.lblMeasurementMode.AutoSize = true;
            this.lblMeasurementMode.Location = new System.Drawing.Point(6, 78);
            this.lblMeasurementMode.Name = "lblMeasurementMode";
            this.lblMeasurementMode.Size = new System.Drawing.Size(95, 13);
            this.lblMeasurementMode.TabIndex = 3;
            this.lblMeasurementMode.Text = "Mesurement Mode";
            this.lblMeasurementMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMeasurementMode.Click += new System.EventHandler(this.lblMeasurementMode_Click);
            // 
            // grpBoxSerialConnections
            // 
            this.grpBoxSerialConnections.Controls.Add(this.lblVNAConnectionStatus);
            this.grpBoxSerialConnections.Controls.Add(this.label2);
            this.grpBoxSerialConnections.Controls.Add(this.cmbBoxVNAConnection);
            this.grpBoxSerialConnections.Controls.Add(this.btnDisconnectSerials);
            this.grpBoxSerialConnections.Controls.Add(this.btnConnectSerials);
            this.grpBoxSerialConnections.Controls.Add(this.lblCont2Status);
            this.grpBoxSerialConnections.Controls.Add(this.cmbBoxCont1);
            this.grpBoxSerialConnections.Controls.Add(this.lblEncoderStatus);
            this.grpBoxSerialConnections.Controls.Add(this.cmbBoxCont2);
            this.grpBoxSerialConnections.Controls.Add(this.lblCont1Status);
            this.grpBoxSerialConnections.Controls.Add(this.lblComPort1);
            this.grpBoxSerialConnections.Controls.Add(this.lblCommPort2);
            this.grpBoxSerialConnections.Controls.Add(this.lblEncoderComPort);
            this.grpBoxSerialConnections.Controls.Add(this.cmbBoxEncoder);
            this.grpBoxSerialConnections.Location = new System.Drawing.Point(6, 6);
            this.grpBoxSerialConnections.Name = "grpBoxSerialConnections";
            this.grpBoxSerialConnections.Size = new System.Drawing.Size(381, 191);
            this.grpBoxSerialConnections.TabIndex = 11;
            this.grpBoxSerialConnections.TabStop = false;
            this.grpBoxSerialConnections.Text = "Serial Connections";
            // 
            // lblVNAConnectionStatus
            // 
            this.lblVNAConnectionStatus.AutoSize = true;
            this.lblVNAConnectionStatus.Location = new System.Drawing.Point(250, 102);
            this.lblVNAConnectionStatus.Name = "lblVNAConnectionStatus";
            this.lblVNAConnectionStatus.Size = new System.Drawing.Size(73, 13);
            this.lblVNAConnectionStatus.TabIndex = 14;
            this.lblVNAConnectionStatus.Text = "Disconnected";
            this.lblVNAConnectionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "VNA COM Port";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbBoxVNAConnection
            // 
            this.cmbBoxVNAConnection.FormattingEnabled = true;
            this.cmbBoxVNAConnection.Location = new System.Drawing.Point(123, 102);
            this.cmbBoxVNAConnection.Name = "cmbBoxVNAConnection";
            this.cmbBoxVNAConnection.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxVNAConnection.TabIndex = 12;
            // 
            // btnDisconnectSerials
            // 
            this.btnDisconnectSerials.Location = new System.Drawing.Point(253, 160);
            this.btnDisconnectSerials.Name = "btnDisconnectSerials";
            this.btnDisconnectSerials.Size = new System.Drawing.Size(121, 23);
            this.btnDisconnectSerials.TabIndex = 11;
            this.btnDisconnectSerials.Text = "Disconnect All";
            this.btnDisconnectSerials.UseVisualStyleBackColor = true;
            this.btnDisconnectSerials.Click += new System.EventHandler(this.btnDisconnectSerials_Click);
            // 
            // btnConnectSerials
            // 
            this.btnConnectSerials.Location = new System.Drawing.Point(253, 135);
            this.btnConnectSerials.Name = "btnConnectSerials";
            this.btnConnectSerials.Size = new System.Drawing.Size(121, 23);
            this.btnConnectSerials.TabIndex = 6;
            this.btnConnectSerials.Text = "Connect All";
            this.btnConnectSerials.UseVisualStyleBackColor = true;
            this.btnConnectSerials.Click += new System.EventHandler(this.btnConnectSerials_Click);
            // 
            // lblCont2Status
            // 
            this.lblCont2Status.AutoSize = true;
            this.lblCont2Status.Location = new System.Drawing.Point(250, 47);
            this.lblCont2Status.Name = "lblCont2Status";
            this.lblCont2Status.Size = new System.Drawing.Size(73, 13);
            this.lblCont2Status.TabIndex = 10;
            this.lblCont2Status.Text = "Disconnected";
            this.lblCont2Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbBoxCont1
            // 
            this.cmbBoxCont1.FormattingEnabled = true;
            this.cmbBoxCont1.Location = new System.Drawing.Point(123, 20);
            this.cmbBoxCont1.Name = "cmbBoxCont1";
            this.cmbBoxCont1.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxCont1.TabIndex = 0;
            this.cmbBoxCont1.DropDown += new System.EventHandler(this.cmbBoxCont1_DropDown);
            // 
            // lblEncoderStatus
            // 
            this.lblEncoderStatus.AutoSize = true;
            this.lblEncoderStatus.Location = new System.Drawing.Point(250, 75);
            this.lblEncoderStatus.Name = "lblEncoderStatus";
            this.lblEncoderStatus.Size = new System.Drawing.Size(73, 13);
            this.lblEncoderStatus.TabIndex = 9;
            this.lblEncoderStatus.Text = "Disconnected";
            this.lblEncoderStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbBoxCont2
            // 
            this.cmbBoxCont2.FormattingEnabled = true;
            this.cmbBoxCont2.Location = new System.Drawing.Point(123, 47);
            this.cmbBoxCont2.Name = "cmbBoxCont2";
            this.cmbBoxCont2.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxCont2.TabIndex = 1;
            this.cmbBoxCont2.DropDown += new System.EventHandler(this.cmbBoxCont2_DropDown);
            // 
            // lblCont1Status
            // 
            this.lblCont1Status.AutoSize = true;
            this.lblCont1Status.Location = new System.Drawing.Point(250, 20);
            this.lblCont1Status.Name = "lblCont1Status";
            this.lblCont1Status.Size = new System.Drawing.Size(73, 13);
            this.lblCont1Status.TabIndex = 8;
            this.lblCont1Status.Text = "Disconnected";
            this.lblCont1Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblComPort1
            // 
            this.lblComPort1.AutoSize = true;
            this.lblComPort1.Location = new System.Drawing.Point(8, 20);
            this.lblComPort1.Name = "lblComPort1";
            this.lblComPort1.Size = new System.Drawing.Size(109, 13);
            this.lblComPort1.TabIndex = 2;
            this.lblComPort1.Text = "Controller 1 COM Port";
            // 
            // lblCommPort2
            // 
            this.lblCommPort2.AutoSize = true;
            this.lblCommPort2.Location = new System.Drawing.Point(8, 47);
            this.lblCommPort2.Name = "lblCommPort2";
            this.lblCommPort2.Size = new System.Drawing.Size(109, 13);
            this.lblCommPort2.TabIndex = 3;
            this.lblCommPort2.Text = "Controller 2 COM Port";
            this.lblCommPort2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEncoderComPort
            // 
            this.lblEncoderComPort.AutoSize = true;
            this.lblEncoderComPort.Location = new System.Drawing.Point(8, 75);
            this.lblEncoderComPort.Name = "lblEncoderComPort";
            this.lblEncoderComPort.Size = new System.Drawing.Size(96, 13);
            this.lblEncoderComPort.TabIndex = 5;
            this.lblEncoderComPort.Text = "Encoder COM Port";
            this.lblEncoderComPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbBoxEncoder
            // 
            this.cmbBoxEncoder.FormattingEnabled = true;
            this.cmbBoxEncoder.Location = new System.Drawing.Point(123, 75);
            this.cmbBoxEncoder.Name = "cmbBoxEncoder";
            this.cmbBoxEncoder.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxEncoder.TabIndex = 4;
            this.cmbBoxEncoder.DropDown += new System.EventHandler(this.cmbBoxEncoder_DropDown);
            // 
            // tabOperation
            // 
            this.tabOperation.Controls.Add(this.button2);
            this.tabOperation.Controls.Add(this.button1);
            this.tabOperation.Controls.Add(this.btnInitMotor1);
            this.tabOperation.Controls.Add(this.btnINC1);
            this.tabOperation.Controls.Add(this.btnStart);
            this.tabOperation.Controls.Add(this.formChart);
            this.tabOperation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabOperation.Location = new System.Drawing.Point(4, 22);
            this.tabOperation.Name = "tabOperation";
            this.tabOperation.Padding = new System.Windows.Forms.Padding(3);
            this.tabOperation.Size = new System.Drawing.Size(796, 350);
            this.tabOperation.TabIndex = 1;
            this.tabOperation.Text = "Operation";
            this.tabOperation.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(181, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Start Control System";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(181, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Load Control System";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnInitMotor1
            // 
            this.btnInitMotor1.Location = new System.Drawing.Point(6, 35);
            this.btnInitMotor1.Name = "btnInitMotor1";
            this.btnInitMotor1.Size = new System.Drawing.Size(141, 23);
            this.btnInitMotor1.TabIndex = 4;
            this.btnInitMotor1.Text = "Init Motor";
            this.btnInitMotor1.UseVisualStyleBackColor = true;
            this.btnInitMotor1.Click += new System.EventHandler(this.btnInitMotor1_Click);
            // 
            // btnINC1
            // 
            this.btnINC1.Location = new System.Drawing.Point(6, 64);
            this.btnINC1.Name = "btnINC1";
            this.btnINC1.Size = new System.Drawing.Size(141, 23);
            this.btnINC1.TabIndex = 3;
            this.btnINC1.Text = "Increment Motor";
            this.btnINC1.UseVisualStyleBackColor = true;
            this.btnINC1.Click += new System.EventHandler(this.btnINC1_Click);
            // 
            // tabResults
            // 
            this.tabResults.Location = new System.Drawing.Point(4, 22);
            this.tabResults.Name = "tabResults";
            this.tabResults.Size = new System.Drawing.Size(796, 350);
            this.tabResults.TabIndex = 2;
            this.tabResults.Text = "Results";
            this.tabResults.UseVisualStyleBackColor = true;
            // 
            // indicatorCont1Connection
            // 
            this.indicatorCont1Connection.BackColor = System.Drawing.Color.Red;
            this.indicatorCont1Connection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.indicatorCont1Connection.Location = new System.Drawing.Point(17, 12);
            this.indicatorCont1Connection.Name = "indicatorCont1Connection";
            this.indicatorCont1Connection.Size = new System.Drawing.Size(77, 47);
            this.indicatorCont1Connection.TabIndex = 4;
            this.indicatorCont1Connection.Text = "Controller 1 Connection";
            this.indicatorCont1Connection.UseVisualStyleBackColor = false;
            // 
            // indicatorCont2Connection
            // 
            this.indicatorCont2Connection.BackColor = System.Drawing.Color.Red;
            this.indicatorCont2Connection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.indicatorCont2Connection.Location = new System.Drawing.Point(100, 12);
            this.indicatorCont2Connection.Name = "indicatorCont2Connection";
            this.indicatorCont2Connection.Size = new System.Drawing.Size(77, 47);
            this.indicatorCont2Connection.TabIndex = 5;
            this.indicatorCont2Connection.Text = "Controller 2 Connection";
            this.indicatorCont2Connection.UseVisualStyleBackColor = false;
            // 
            // indicatorEncoderConnection
            // 
            this.indicatorEncoderConnection.BackColor = System.Drawing.Color.Red;
            this.indicatorEncoderConnection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.indicatorEncoderConnection.Location = new System.Drawing.Point(183, 12);
            this.indicatorEncoderConnection.Name = "indicatorEncoderConnection";
            this.indicatorEncoderConnection.Size = new System.Drawing.Size(77, 47);
            this.indicatorEncoderConnection.TabIndex = 6;
            this.indicatorEncoderConnection.Text = "Encoder Connection";
            this.indicatorEncoderConnection.UseVisualStyleBackColor = false;
            // 
            // indicatorVNAConnection
            // 
            this.indicatorVNAConnection.BackColor = System.Drawing.Color.Red;
            this.indicatorVNAConnection.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.indicatorVNAConnection.FlatAppearance.BorderSize = 10;
            this.indicatorVNAConnection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.indicatorVNAConnection.Location = new System.Drawing.Point(266, 12);
            this.indicatorVNAConnection.Name = "indicatorVNAConnection";
            this.indicatorVNAConnection.Size = new System.Drawing.Size(77, 47);
            this.indicatorVNAConnection.TabIndex = 7;
            this.indicatorVNAConnection.Text = "VNA Connection";
            this.indicatorVNAConnection.UseVisualStyleBackColor = false;
            // 
            // btnEStop
            // 
            this.btnEStop.BackColor = System.Drawing.SystemColors.Control;
            this.btnEStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEStop.Location = new System.Drawing.Point(700, 12);
            this.btnEStop.Name = "btnEStop";
            this.btnEStop.Size = new System.Drawing.Size(121, 47);
            this.btnEStop.TabIndex = 11;
            this.btnEStop.Text = "EMERGENCY STOP";
            this.btnEStop.UseVisualStyleBackColor = false;
            this.btnEStop.Click += new System.EventHandler(this.btnEStop_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 453);
            this.Controls.Add(this.btnEStop);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.indicatorVNAConnection);
            this.Controls.Add(this.indicatorEncoderConnection);
            this.Controls.Add(this.indicatorCont2Connection);
            this.Controls.Add(this.indicatorCont1Connection);
            this.Controls.Add(this.lblEncoderPositions);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.formChart)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabConfiguration.ResumeLayout(false);
            this.grpBoxSummary.ResumeLayout(false);
            this.grpBoxSummary.PerformLayout();
            this.grpBoxLoad.ResumeLayout(false);
            this.grpBoxLoad.PerformLayout();
            this.grpBoxMeasurementOptions.ResumeLayout(false);
            this.grpBoxMeasurementOptions.PerformLayout();
            this.grpBoxSerialConnections.ResumeLayout(false);
            this.grpBoxSerialConnections.PerformLayout();
            this.tabOperation.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblEncoderPositions;
        private System.Windows.Forms.DataVisualization.Charting.Chart formChart;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabConfiguration;
        private System.Windows.Forms.TabPage tabOperation;
        private System.Windows.Forms.TabPage tabResults;
        private System.Windows.Forms.Label lblEncoderComPort;
        private System.Windows.Forms.ComboBox cmbBoxEncoder;
        private System.Windows.Forms.Label lblCommPort2;
        private System.Windows.Forms.Label lblComPort1;
        private System.Windows.Forms.ComboBox cmbBoxCont2;
        private System.Windows.Forms.ComboBox cmbBoxCont1;
        private System.Windows.Forms.GroupBox grpBoxSerialConnections;
        private System.Windows.Forms.Button btnConnectSerials;
        private System.Windows.Forms.Label lblCont2Status;
        private System.Windows.Forms.Label lblEncoderStatus;
        private System.Windows.Forms.Label lblCont1Status;
        private System.Windows.Forms.Button btnINC1;
        private System.Windows.Forms.Button btnInitMotor1;
        public System.Windows.Forms.Button indicatorCont1Connection;
        public System.Windows.Forms.Button indicatorCont2Connection;
        public System.Windows.Forms.Button indicatorEncoderConnection;
        public System.Windows.Forms.Button indicatorVNAConnection;
        private System.Windows.Forms.Button btnEStop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnDisconnectSerials;
        private System.Windows.Forms.GroupBox grpBoxMeasurementOptions;
        private System.Windows.Forms.Label lblMeasurementSummary;
        private System.Windows.Forms.TextBox txtBoxCriticalAngle;
        private System.Windows.Forms.Button btnApplyMeasurementOptions;
        private System.Windows.Forms.ComboBox cmbBoxMeasurementMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMeasurementMode;
        private System.Windows.Forms.Label lblGHz;
        private System.Windows.Forms.TextBox txtBoxFrequency;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.Label lblCritAngle;
        private System.Windows.Forms.Button btnLoadMotors;
        private System.Windows.Forms.GroupBox grpBoxSummary;
        private System.Windows.Forms.GroupBox grpBoxLoad;
        private System.Windows.Forms.Label lblLoadDescription;
        private System.Windows.Forms.Label lblVNAConnectionStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBoxVNAConnection;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

