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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblEncoderPositions = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabConfiguration = new System.Windows.Forms.TabPage();
            this.grpBoxSummary = new System.Windows.Forms.GroupBox();
            this.lblMeasurementSummary = new System.Windows.Forms.Label();
            this.grpBoxLoad = new System.Windows.Forms.GroupBox();
            this.lblLoadDescription = new System.Windows.Forms.Label();
            this.btnLoadMotors = new System.Windows.Forms.Button();
            this.grpBoxMeasurementOptions = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBoxLabel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
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
            this.pbScan = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblScanStatus = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStopScan = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRunSystem = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnZeroMotors = new System.Windows.Forms.Button();
            this.tabResults = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tsImages = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbBoxFFPowerPhiCuts = new System.Windows.Forms.ComboBox();
            this.cmbBoxNFPowerLinear = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBoxGridResolution = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBoxDTheta = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBoxPhis = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnStartMatlab = new System.Windows.Forms.Button();
            this.tabCalibration = new System.Windows.Forms.TabPage();
            this.indicatorCont1Connection = new System.Windows.Forms.Button();
            this.indicatorCont2Connection = new System.Windows.Forms.Button();
            this.indicatorEncoderConnection = new System.Windows.Forms.Button();
            this.indicatorVNAConnection = new System.Windows.Forms.Button();
            this.btnEStop = new System.Windows.Forms.Button();
            this.bwLoading = new System.ComponentModel.BackgroundWorker();
            this.bwControlSystem = new System.ComponentModel.BackgroundWorker();
            this.bwMatlab = new System.ComponentModel.BackgroundWorker();
            this.lblMainInfo = new System.Windows.Forms.Label();
            this.lblStat = new System.Windows.Forms.Label();
            this.lblSysState = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.stOperation = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.pb_xmag = new System.Windows.Forms.PictureBox();
            this.pb_xphase = new System.Windows.Forms.PictureBox();
            this.pb_ymag = new System.Windows.Forms.PictureBox();
            this.pb_yphase = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabConfiguration.SuspendLayout();
            this.grpBoxSummary.SuspendLayout();
            this.grpBoxLoad.SuspendLayout();
            this.grpBoxMeasurementOptions.SuspendLayout();
            this.grpBoxSerialConnections.SuspendLayout();
            this.tabOperation.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabResults.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tsImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.stOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_xmag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_xphase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ymag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_yphase)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEncoderPositions
            // 
            this.lblEncoderPositions.AutoSize = true;
            this.lblEncoderPositions.Location = new System.Drawing.Point(442, 21);
            this.lblEncoderPositions.Name = "lblEncoderPositions";
            this.lblEncoderPositions.Size = new System.Drawing.Size(0, 13);
            this.lblEncoderPositions.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabConfiguration);
            this.tabControl1.Controls.Add(this.tabOperation);
            this.tabControl1.Controls.Add(this.tabResults);
            this.tabControl1.Controls.Add(this.tabCalibration);
            this.tabControl1.Location = new System.Drawing.Point(17, 65);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(876, 501);
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
            this.tabConfiguration.Click += new System.EventHandler(this.tabConfiguration_Click);
            // 
            // grpBoxSummary
            // 
            this.grpBoxSummary.Controls.Add(this.lblMeasurementSummary);
            this.grpBoxSummary.Location = new System.Drawing.Point(393, 100);
            this.grpBoxSummary.Name = "grpBoxSummary";
            this.grpBoxSummary.Size = new System.Drawing.Size(381, 226);
            this.grpBoxSummary.TabIndex = 12;
            this.grpBoxSummary.TabStop = false;
            this.grpBoxSummary.Text = "Summary";
            // 
            // lblMeasurementSummary
            // 
            this.lblMeasurementSummary.AutoSize = true;
            this.lblMeasurementSummary.Location = new System.Drawing.Point(6, 19);
            this.lblMeasurementSummary.Name = "lblMeasurementSummary";
            this.lblMeasurementSummary.Size = new System.Drawing.Size(0, 13);
            this.lblMeasurementSummary.TabIndex = 14;
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
            this.grpBoxLoad.Text = "Load Configuration";
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
            this.btnLoadMotors.Location = new System.Drawing.Point(263, 54);
            this.btnLoadMotors.Name = "btnLoadMotors";
            this.btnLoadMotors.Size = new System.Drawing.Size(121, 23);
            this.btnLoadMotors.TabIndex = 13;
            this.btnLoadMotors.Text = "Load";
            this.btnLoadMotors.UseVisualStyleBackColor = true;
            this.btnLoadMotors.Click += new System.EventHandler(this.btnLoadMotors_Click);
            // 
            // grpBoxMeasurementOptions
            // 
            this.grpBoxMeasurementOptions.Controls.Add(this.label5);
            this.grpBoxMeasurementOptions.Controls.Add(this.txtBoxLabel);
            this.grpBoxMeasurementOptions.Controls.Add(this.label6);
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
            this.grpBoxMeasurementOptions.Size = new System.Drawing.Size(381, 141);
            this.grpBoxMeasurementOptions.TabIndex = 12;
            this.grpBoxMeasurementOptions.TabStop = false;
            this.grpBoxMeasurementOptions.Text = "Measurement Options";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(250, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Identifier for this Scan";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxLabel
            // 
            this.txtBoxLabel.Location = new System.Drawing.Point(123, 17);
            this.txtBoxLabel.Name = "txtBoxLabel";
            this.txtBoxLabel.Size = new System.Drawing.Size(121, 20);
            this.txtBoxLabel.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Scan Label";
            // 
            // lblGHz
            // 
            this.lblGHz.AutoSize = true;
            this.lblGHz.Location = new System.Drawing.Point(250, 68);
            this.lblGHz.Name = "lblGHz";
            this.lblGHz.Size = new System.Drawing.Size(28, 13);
            this.lblGHz.TabIndex = 17;
            this.lblGHz.Text = "GHz";
            this.lblGHz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxFrequency
            // 
            this.txtBoxFrequency.Location = new System.Drawing.Point(123, 68);
            this.txtBoxFrequency.Name = "txtBoxFrequency";
            this.txtBoxFrequency.Size = new System.Drawing.Size(121, 20);
            this.txtBoxFrequency.TabIndex = 18;
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Location = new System.Drawing.Point(8, 68);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(57, 13);
            this.lblFrequency.TabIndex = 16;
            this.lblFrequency.Text = "Frequency";
            // 
            // lblCritAngle
            // 
            this.lblCritAngle.AutoSize = true;
            this.lblCritAngle.Location = new System.Drawing.Point(250, 42);
            this.lblCritAngle.Name = "lblCritAngle";
            this.lblCritAngle.Size = new System.Drawing.Size(47, 13);
            this.lblCritAngle.TabIndex = 12;
            this.lblCritAngle.Text = "Degrees";
            this.lblCritAngle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxCriticalAngle
            // 
            this.txtBoxCriticalAngle.Location = new System.Drawing.Point(123, 42);
            this.txtBoxCriticalAngle.Name = "txtBoxCriticalAngle";
            this.txtBoxCriticalAngle.Size = new System.Drawing.Size(121, 20);
            this.txtBoxCriticalAngle.TabIndex = 13;
            // 
            // btnApplyMeasurementOptions
            // 
            this.btnApplyMeasurementOptions.Location = new System.Drawing.Point(254, 112);
            this.btnApplyMeasurementOptions.Name = "btnApplyMeasurementOptions";
            this.btnApplyMeasurementOptions.Size = new System.Drawing.Size(121, 23);
            this.btnApplyMeasurementOptions.TabIndex = 6;
            this.btnApplyMeasurementOptions.Text = "Apply Options";
            this.btnApplyMeasurementOptions.UseVisualStyleBackColor = true;
            this.btnApplyMeasurementOptions.Click += new System.EventHandler(this.btnApplyMeasurementOptions_Click);
            // 
            // cmbBoxMeasurementMode
            // 
            this.cmbBoxMeasurementMode.DisplayMember = "0";
            this.cmbBoxMeasurementMode.FormattingEnabled = true;
            this.cmbBoxMeasurementMode.Items.AddRange(new object[] {
            "Discrete Mode",
            "Continuous Mode"});
            this.cmbBoxMeasurementMode.Location = new System.Drawing.Point(123, 94);
            this.cmbBoxMeasurementMode.Name = "cmbBoxMeasurementMode";
            this.cmbBoxMeasurementMode.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxMeasurementMode.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Critical Angle";
            // 
            // lblMeasurementMode
            // 
            this.lblMeasurementMode.AutoSize = true;
            this.lblMeasurementMode.Location = new System.Drawing.Point(6, 99);
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
            this.grpBoxSerialConnections.Enter += new System.EventHandler(this.grpBoxSerialConnections_Enter);
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
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "VNA";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.tabOperation.Controls.Add(this.pbScan);
            this.tabOperation.Controls.Add(this.panel2);
            this.tabOperation.Controls.Add(this.groupBox3);
            this.tabOperation.Controls.Add(this.groupBox2);
            this.tabOperation.Controls.Add(this.groupBox1);
            this.tabOperation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabOperation.Location = new System.Drawing.Point(4, 22);
            this.tabOperation.Name = "tabOperation";
            this.tabOperation.Padding = new System.Windows.Forms.Padding(3);
            this.tabOperation.Size = new System.Drawing.Size(868, 475);
            this.tabOperation.TabIndex = 1;
            this.tabOperation.Text = "Operation";
            this.tabOperation.UseVisualStyleBackColor = true;
            // 
            // pbScan
            // 
            this.pbScan.Location = new System.Drawing.Point(12, 217);
            this.pbScan.Name = "pbScan";
            this.pbScan.Size = new System.Drawing.Size(378, 23);
            this.pbScan.TabIndex = 23;
            this.pbScan.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblScanStatus);
            this.groupBox3.Location = new System.Drawing.Point(6, 225);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(390, 137);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Scan Status";
            // 
            // lblScanStatus
            // 
            this.lblScanStatus.AutoSize = true;
            this.lblScanStatus.Location = new System.Drawing.Point(6, 18);
            this.lblScanStatus.MaximumSize = new System.Drawing.Size(250, 100);
            this.lblScanStatus.Name = "lblScanStatus";
            this.lblScanStatus.Size = new System.Drawing.Size(108, 13);
            this.lblScanStatus.TabIndex = 20;
            this.lblScanStatus.Text = "Status: Not Scanning";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStopScan);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnRunSystem);
            this.groupBox2.Location = new System.Drawing.Point(6, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(390, 114);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Run Scan";
            // 
            // btnStopScan
            // 
            this.btnStopScan.Location = new System.Drawing.Point(263, 82);
            this.btnStopScan.Name = "btnStopScan";
            this.btnStopScan.Size = new System.Drawing.Size(121, 23);
            this.btnStopScan.TabIndex = 21;
            this.btnStopScan.Text = "Stop";
            this.btnStopScan.UseVisualStyleBackColor = true;
            this.btnStopScan.Click += new System.EventHandler(this.btnStopScan_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.MaximumSize = new System.Drawing.Size(250, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 26);
            this.label3.TabIndex = 20;
            this.label3.Text = "Once the system is properly configured and calibrated, a scan can begin.";
            // 
            // btnRunSystem
            // 
            this.btnRunSystem.Location = new System.Drawing.Point(263, 53);
            this.btnRunSystem.Name = "btnRunSystem";
            this.btnRunSystem.Size = new System.Drawing.Size(121, 23);
            this.btnRunSystem.TabIndex = 5;
            this.btnRunSystem.Text = "Run";
            this.btnRunSystem.UseVisualStyleBackColor = true;
            this.btnRunSystem.Click += new System.EventHandler(this.btnRunSystem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnZeroMotors);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 93);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Calibrate Motors";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.MaximumSize = new System.Drawing.Size(250, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 39);
            this.label1.TabIndex = 20;
            this.label1.Text = "This function will align the arm and 2 antennas to their proper starting position" +
    ". This function must be performed before a scan can begin.";
            // 
            // btnZeroMotors
            // 
            this.btnZeroMotors.Location = new System.Drawing.Point(263, 64);
            this.btnZeroMotors.Name = "btnZeroMotors";
            this.btnZeroMotors.Size = new System.Drawing.Size(121, 23);
            this.btnZeroMotors.TabIndex = 13;
            this.btnZeroMotors.Text = "Calibrate";
            this.btnZeroMotors.UseVisualStyleBackColor = true;
            this.btnZeroMotors.Click += new System.EventHandler(this.btnZeroMotors_Click);
            // 
            // tabResults
            // 
            this.tabResults.Controls.Add(this.panel1);
            this.tabResults.Controls.Add(this.groupBox4);
            this.tabResults.Location = new System.Drawing.Point(4, 22);
            this.tabResults.Name = "tabResults";
            this.tabResults.Size = new System.Drawing.Size(958, 475);
            this.tabResults.TabIndex = 2;
            this.tabResults.Text = "Results";
            this.tabResults.UseVisualStyleBackColor = true;
            this.tabResults.Click += new System.EventHandler(this.tabResults_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tsImages);
            this.panel1.Controls.Add(this.pbImage);
            this.panel1.Location = new System.Drawing.Point(355, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 460);
            this.panel1.TabIndex = 24;
            // 
            // tsImages
            // 
            this.tsImages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.tsImages.Location = new System.Drawing.Point(0, 0);
            this.tsImages.Name = "tsImages";
            this.tsImages.Size = new System.Drawing.Size(600, 25);
            this.tsImages.TabIndex = 0;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "<";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = ">";
            this.toolStripButton2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(3, 28);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(594, 429);
            this.pbImage.TabIndex = 22;
            this.pbImage.TabStop = false;
            this.pbImage.Click += new System.EventHandler(this.pbNearField_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbBoxFFPowerPhiCuts);
            this.groupBox4.Controls.Add(this.cmbBoxNFPowerLinear);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.txtBoxGridResolution);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.txtBoxDTheta);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.txtBoxPhis);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.btnStartMatlab);
            this.groupBox4.Location = new System.Drawing.Point(13, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(316, 323);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Calculate Far-Field";
            // 
            // cmbBoxFFPowerPhiCuts
            // 
            this.cmbBoxFFPowerPhiCuts.FormattingEnabled = true;
            this.cmbBoxFFPowerPhiCuts.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbBoxFFPowerPhiCuts.Location = new System.Drawing.Point(187, 227);
            this.cmbBoxFFPowerPhiCuts.Name = "cmbBoxFFPowerPhiCuts";
            this.cmbBoxFFPowerPhiCuts.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxFFPowerPhiCuts.TabIndex = 37;
            // 
            // cmbBoxNFPowerLinear
            // 
            this.cmbBoxNFPowerLinear.FormattingEnabled = true;
            this.cmbBoxNFPowerLinear.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbBoxNFPowerLinear.Location = new System.Drawing.Point(187, 191);
            this.cmbBoxNFPowerLinear.Name = "cmbBoxNFPowerLinear";
            this.cmbBoxNFPowerLinear.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxNFPowerLinear.TabIndex = 36;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 246);
            this.label16.MaximumSize = new System.Drawing.Size(250, 1000);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 13);
            this.label16.TabIndex = 35;
            this.label16.Text = "(Explanation)";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 230);
            this.label17.MaximumSize = new System.Drawing.Size(250, 1000);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(94, 13);
            this.label17.TabIndex = 33;
            this.label17.Text = "FF Power Phi Cuts";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 207);
            this.label14.MaximumSize = new System.Drawing.Size(250, 1000);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 13);
            this.label14.TabIndex = 32;
            this.label14.Text = "(Explanation)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 191);
            this.label15.MaximumSize = new System.Drawing.Size(250, 1000);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(86, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "NF Power Linear";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 166);
            this.label12.MaximumSize = new System.Drawing.Size(250, 1000);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "(Explanation)";
            // 
            // txtBoxGridResolution
            // 
            this.txtBoxGridResolution.Location = new System.Drawing.Point(187, 143);
            this.txtBoxGridResolution.Name = "txtBoxGridResolution";
            this.txtBoxGridResolution.Size = new System.Drawing.Size(121, 20);
            this.txtBoxGridResolution.TabIndex = 28;
            this.txtBoxGridResolution.Text = "0.5";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 150);
            this.label13.MaximumSize = new System.Drawing.Size(250, 1000);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "Grid Resolution:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 83);
            this.label10.MaximumSize = new System.Drawing.Size(250, 1000);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "(Explanation)";
            // 
            // txtBoxDTheta
            // 
            this.txtBoxDTheta.Location = new System.Drawing.Point(187, 60);
            this.txtBoxDTheta.Name = "txtBoxDTheta";
            this.txtBoxDTheta.Size = new System.Drawing.Size(121, 20);
            this.txtBoxDTheta.TabIndex = 25;
            this.txtBoxDTheta.Text = "0.1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 67);
            this.label11.MaximumSize = new System.Drawing.Size(250, 1000);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Delta Theta:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 122);
            this.label9.MaximumSize = new System.Drawing.Size(250, 1000);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(247, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Enter as comma separated degree-values, i.e: 0,45";
            // 
            // txtBoxPhis
            // 
            this.txtBoxPhis.Location = new System.Drawing.Point(187, 99);
            this.txtBoxPhis.Name = "txtBoxPhis";
            this.txtBoxPhis.Size = new System.Drawing.Size(121, 20);
            this.txtBoxPhis.TabIndex = 22;
            this.txtBoxPhis.Text = "0,45";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 106);
            this.label8.MaximumSize = new System.Drawing.Size(250, 1000);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Phi-Cuts:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 18);
            this.label7.MaximumSize = new System.Drawing.Size(250, 1000);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(240, 39);
            this.label7.TabIndex = 20;
            this.label7.Text = "Once a scan is complete the near-field to far-field transformation can be perform" +
    "ed. This process takes about 3 minutes.";
            // 
            // btnStartMatlab
            // 
            this.btnStartMatlab.Location = new System.Drawing.Point(162, 294);
            this.btnStartMatlab.Name = "btnStartMatlab";
            this.btnStartMatlab.Size = new System.Drawing.Size(146, 23);
            this.btnStartMatlab.TabIndex = 0;
            this.btnStartMatlab.Text = "Calculate Far-Field";
            this.btnStartMatlab.UseVisualStyleBackColor = true;
            this.btnStartMatlab.Click += new System.EventHandler(this.btnStartMatlab_Click);
            // 
            // tabCalibration
            // 
            this.tabCalibration.Location = new System.Drawing.Point(4, 22);
            this.tabCalibration.Name = "tabCalibration";
            this.tabCalibration.Size = new System.Drawing.Size(958, 475);
            this.tabCalibration.TabIndex = 3;
            this.tabCalibration.Text = "Calibration";
            this.tabCalibration.UseVisualStyleBackColor = true;
            // 
            // indicatorCont1Connection
            // 
            this.indicatorCont1Connection.BackColor = System.Drawing.Color.Red;
            this.indicatorCont1Connection.Enabled = false;
            this.indicatorCont1Connection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.indicatorCont1Connection.Location = new System.Drawing.Point(365, 12);
            this.indicatorCont1Connection.Name = "indicatorCont1Connection";
            this.indicatorCont1Connection.Size = new System.Drawing.Size(77, 47);
            this.indicatorCont1Connection.TabIndex = 4;
            this.indicatorCont1Connection.Text = "Controller 1 Connection";
            this.indicatorCont1Connection.UseVisualStyleBackColor = false;
            // 
            // indicatorCont2Connection
            // 
            this.indicatorCont2Connection.BackColor = System.Drawing.Color.Red;
            this.indicatorCont2Connection.Enabled = false;
            this.indicatorCont2Connection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.indicatorCont2Connection.Location = new System.Drawing.Point(448, 12);
            this.indicatorCont2Connection.Name = "indicatorCont2Connection";
            this.indicatorCont2Connection.Size = new System.Drawing.Size(77, 47);
            this.indicatorCont2Connection.TabIndex = 5;
            this.indicatorCont2Connection.Text = "Controller 2 Connection";
            this.indicatorCont2Connection.UseVisualStyleBackColor = false;
            // 
            // indicatorEncoderConnection
            // 
            this.indicatorEncoderConnection.BackColor = System.Drawing.Color.Red;
            this.indicatorEncoderConnection.Enabled = false;
            this.indicatorEncoderConnection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.indicatorEncoderConnection.Location = new System.Drawing.Point(531, 12);
            this.indicatorEncoderConnection.Name = "indicatorEncoderConnection";
            this.indicatorEncoderConnection.Size = new System.Drawing.Size(77, 47);
            this.indicatorEncoderConnection.TabIndex = 6;
            this.indicatorEncoderConnection.Text = "Encoder Connection";
            this.indicatorEncoderConnection.UseVisualStyleBackColor = false;
            // 
            // indicatorVNAConnection
            // 
            this.indicatorVNAConnection.BackColor = System.Drawing.Color.Red;
            this.indicatorVNAConnection.Enabled = false;
            this.indicatorVNAConnection.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.indicatorVNAConnection.FlatAppearance.BorderSize = 10;
            this.indicatorVNAConnection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.indicatorVNAConnection.Location = new System.Drawing.Point(614, 12);
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
            // bwLoading
            // 
            this.bwLoading.WorkerReportsProgress = true;
            this.bwLoading.WorkerSupportsCancellation = true;
            this.bwLoading.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.bwLoading.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.bwLoading.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // bwControlSystem
            // 
            this.bwControlSystem.WorkerReportsProgress = true;
            this.bwControlSystem.WorkerSupportsCancellation = true;
            this.bwControlSystem.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwControlSystem_DoWork);
            this.bwControlSystem.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwControlSystem_ProgressChanged);
            this.bwControlSystem.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwControlSystem_RunWorkerCompleted);
            // 
            // bwMatlab
            // 
            this.bwMatlab.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwMatlab_DoWork);
            this.bwMatlab.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwMatlab_ProgressChanged);
            this.bwMatlab.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwMatlab_RunWorkerCompleted);
            // 
            // lblMainInfo
            // 
            this.lblMainInfo.AutoSize = true;
            this.lblMainInfo.Location = new System.Drawing.Point(18, 12);
            this.lblMainInfo.MaximumSize = new System.Drawing.Size(250, 100);
            this.lblMainInfo.Name = "lblMainInfo";
            this.lblMainInfo.Size = new System.Drawing.Size(225, 13);
            this.lblMainInfo.TabIndex = 21;
            this.lblMainInfo.Text = "Bi-Polar Planar Near Field Measurment System";
            // 
            // lblStat
            // 
            this.lblStat.AutoSize = true;
            this.lblStat.Location = new System.Drawing.Point(18, 29);
            this.lblStat.MaximumSize = new System.Drawing.Size(250, 100);
            this.lblStat.Name = "lblStat";
            this.lblStat.Size = new System.Drawing.Size(40, 13);
            this.lblStat.TabIndex = 22;
            this.lblStat.Text = "Status:";
            // 
            // lblSysState
            // 
            this.lblSysState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSysState.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSysState.Location = new System.Drawing.Point(59, 30);
            this.lblSysState.MaximumSize = new System.Drawing.Size(250, 100);
            this.lblSysState.Name = "lblSysState";
            this.lblSysState.Size = new System.Drawing.Size(200, 30);
            this.lblSysState.TabIndex = 23;
            this.lblSysState.Text = "Unconfigured";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pb_yphase);
            this.panel2.Controls.Add(this.pb_ymag);
            this.panel2.Controls.Add(this.pb_xphase);
            this.panel2.Controls.Add(this.pb_xmag);
            this.panel2.Controls.Add(this.stOperation);
            this.panel2.Location = new System.Drawing.Point(402, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(453, 463);
            this.panel2.TabIndex = 25;
            // 
            // stOperation
            // 
            this.stOperation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton4});
            this.stOperation.Location = new System.Drawing.Point(0, 0);
            this.stOperation.Name = "stOperation";
            this.stOperation.Size = new System.Drawing.Size(453, 25);
            this.stOperation.TabIndex = 0;
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "<";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = ">";
            this.toolStripButton4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pb_xmag
            // 
            this.pb_xmag.Location = new System.Drawing.Point(12, 28);
            this.pb_xmag.Name = "pb_xmag";
            this.pb_xmag.Size = new System.Drawing.Size(200, 200);
            this.pb_xmag.TabIndex = 23;
            this.pb_xmag.TabStop = false;
            // 
            // pb_xphase
            // 
            this.pb_xphase.Location = new System.Drawing.Point(218, 28);
            this.pb_xphase.Name = "pb_xphase";
            this.pb_xphase.Size = new System.Drawing.Size(200, 200);
            this.pb_xphase.TabIndex = 24;
            this.pb_xphase.TabStop = false;
            // 
            // pb_ymag
            // 
            this.pb_ymag.Location = new System.Drawing.Point(12, 247);
            this.pb_ymag.Name = "pb_ymag";
            this.pb_ymag.Size = new System.Drawing.Size(200, 200);
            this.pb_ymag.TabIndex = 25;
            this.pb_ymag.TabStop = false;
            // 
            // pb_yphase
            // 
            this.pb_yphase.Location = new System.Drawing.Point(218, 247);
            this.pb_yphase.Name = "pb_yphase";
            this.pb_yphase.Size = new System.Drawing.Size(200, 200);
            this.pb_yphase.TabIndex = 26;
            this.pb_yphase.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 578);
            this.Controls.Add(this.lblSysState);
            this.Controls.Add(this.lblStat);
            this.Controls.Add(this.lblMainInfo);
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
            this.Load += new System.EventHandler(this.MainForm_Load);
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
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabResults.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsImages.ResumeLayout(false);
            this.tsImages.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.stOperation.ResumeLayout(false);
            this.stOperation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_xmag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_xphase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ymag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_yphase)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEncoderPositions;
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
        public System.Windows.Forms.Button indicatorCont1Connection;
        public System.Windows.Forms.Button indicatorCont2Connection;
        public System.Windows.Forms.Button indicatorEncoderConnection;
        public System.Windows.Forms.Button indicatorVNAConnection;
        private System.Windows.Forms.Button btnEStop;
        private System.Windows.Forms.Button btnRunSystem;
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
        private System.ComponentModel.BackgroundWorker bwLoading;
        private System.ComponentModel.BackgroundWorker bwControlSystem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnZeroMotors;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBoxLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblScanStatus;
        private System.Windows.Forms.Button btnStopScan;
        private System.Windows.Forms.Button btnStartMatlab;
        private System.ComponentModel.BackgroundWorker bwMatlab;
        private System.Windows.Forms.ProgressBar pbScan;
        private System.Windows.Forms.Label lblMainInfo;
        private System.Windows.Forms.Label lblStat;
        private System.Windows.Forms.Label lblSysState;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip tsImages;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ComboBox cmbBoxFFPowerPhiCuts;
        private System.Windows.Forms.ComboBox cmbBoxNFPowerLinear;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBoxGridResolution;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBoxDTheta;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBoxPhis;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabCalibration;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip stOperation;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.PictureBox pb_xmag;
        private System.Windows.Forms.PictureBox pb_yphase;
        private System.Windows.Forms.PictureBox pb_ymag;
        private System.Windows.Forms.PictureBox pb_xphase;
    }
}

