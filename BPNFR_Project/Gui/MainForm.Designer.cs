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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblEncoderPositions = new System.Windows.Forms.Label();
            this.formChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabConfiguration = new System.Windows.Forms.TabPage();
            this.grpBoxSerialConnections = new System.Windows.Forms.GroupBox();
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
            this.btnInitMotor1 = new System.Windows.Forms.Button();
            this.btnINC1 = new System.Windows.Forms.Button();
            this.tabResults = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.formChart)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabConfiguration.SuspendLayout();
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
            series1.Name = "Series1";
            this.formChart.Series.Add(series1);
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
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(721, 396);
            this.tabControl1.TabIndex = 3;
            // 
            // tabConfiguration
            // 
            this.tabConfiguration.Controls.Add(this.grpBoxSerialConnections);
            this.tabConfiguration.Location = new System.Drawing.Point(4, 22);
            this.tabConfiguration.Name = "tabConfiguration";
            this.tabConfiguration.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfiguration.Size = new System.Drawing.Size(713, 370);
            this.tabConfiguration.TabIndex = 0;
            this.tabConfiguration.Text = "Configuration";
            this.tabConfiguration.UseVisualStyleBackColor = true;
            // 
            // grpBoxSerialConnections
            // 
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
            this.grpBoxSerialConnections.Size = new System.Drawing.Size(381, 130);
            this.grpBoxSerialConnections.TabIndex = 11;
            this.grpBoxSerialConnections.TabStop = false;
            this.grpBoxSerialConnections.Text = "Serial Connections";
            // 
            // btnConnectSerials
            // 
            this.btnConnectSerials.Location = new System.Drawing.Point(253, 101);
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
            this.tabOperation.Controls.Add(this.btnInitMotor1);
            this.tabOperation.Controls.Add(this.btnINC1);
            this.tabOperation.Controls.Add(this.btnStart);
            this.tabOperation.Controls.Add(this.formChart);
            this.tabOperation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabOperation.Location = new System.Drawing.Point(4, 22);
            this.tabOperation.Name = "tabOperation";
            this.tabOperation.Padding = new System.Windows.Forms.Padding(3);
            this.tabOperation.Size = new System.Drawing.Size(713, 370);
            this.tabOperation.TabIndex = 1;
            this.tabOperation.Text = "Operation";
            this.tabOperation.UseVisualStyleBackColor = true;
            // 
            // btnInitMotor1
            // 
            this.btnInitMotor1.Location = new System.Drawing.Point(6, 35);
            this.btnInitMotor1.Name = "btnInitMotor1";
            this.btnInitMotor1.Size = new System.Drawing.Size(75, 23);
            this.btnInitMotor1.TabIndex = 4;
            this.btnInitMotor1.Text = "Init Motor 1";
            this.btnInitMotor1.UseVisualStyleBackColor = true;
            this.btnInitMotor1.Click += new System.EventHandler(this.btnInitMotor1_Click);
            // 
            // btnINC1
            // 
            this.btnINC1.Location = new System.Drawing.Point(6, 64);
            this.btnINC1.Name = "btnINC1";
            this.btnINC1.Size = new System.Drawing.Size(75, 23);
            this.btnINC1.TabIndex = 3;
            this.btnINC1.Text = "INC Motor";
            this.btnINC1.UseVisualStyleBackColor = true;
            this.btnINC1.Click += new System.EventHandler(this.btnINC1_Click);
            // 
            // tabResults
            // 
            this.tabResults.Location = new System.Drawing.Point(4, 22);
            this.tabResults.Name = "tabResults";
            this.tabResults.Size = new System.Drawing.Size(713, 370);
            this.tabResults.TabIndex = 2;
            this.tabResults.Text = "Results";
            this.tabResults.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 421);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblEncoderPositions);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.formChart)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabConfiguration.ResumeLayout(false);
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
    }
}

