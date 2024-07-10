

namespace EventMessageTool {   
    partial class FormMain {

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            progressBar = new ProgressBar();
            openFileDialog = new OpenFileDialog();
            tabPane = new TabControl();
            tabEvents = new TabPage();
            tbEvents = new TextBox();
            tabOutput = new TabPage();
            tbOutput = new TextBox();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            btnPing = new Button();
            label4 = new Label();
            tbBaseTag = new TextBox();
            lblSlot = new Label();
            upDnSlot = new NumericUpDown();
            label1 = new Label();
            tbIPAddress = new TextBox();
            panel2 = new Panel();
            label2 = new Label();
            btnFileSel = new Button();
            tbFileSelect = new TextBox();
            btnExportMsg = new Button();
            btnImportTags = new Button();
            panel3 = new Panel();
            btnDBExport = new Button();
            label3 = new Label();
            tbDBName = new TextBox();
            panel4 = new Panel();
            label5 = new Label();
            tbModule = new TextBox();
            toolTip1 = new ToolTip(components);
            tabPane.SuspendLayout();
            tabEvents.SuspendLayout();
            tabOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)upDnSlot).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // progressBar
            // 
            progressBar.Location = new Point(0, 568);
            progressBar.Margin = new Padding(3, 4, 3, 4);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(986, 31);
            progressBar.TabIndex = 1;
            // 
            // openFileDialog
            // 
            openFileDialog.Filter = "\"Macro Enabled(*.xlsm)\"|*.xlsm|\"Excel Workbook(*.xlsx)\"|*.xlsx|\"All Files(*.*)\"|*.*";
            // 
            // tabPane
            // 
            tabPane.Controls.Add(tabEvents);
            tabPane.Controls.Add(tabOutput);
            tabPane.Cursor = Cursors.Hand;
            tabPane.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabPane.Location = new Point(0, 294);
            tabPane.Margin = new Padding(3, 4, 3, 4);
            tabPane.Name = "tabPane";
            tabPane.SelectedIndex = 0;
            tabPane.Size = new Size(986, 273);
            tabPane.TabIndex = 2;
            // 
            // tabEvents
            // 
            tabEvents.BackColor = Color.Transparent;
            tabEvents.Controls.Add(tbEvents);
            tabEvents.ForeColor = SystemColors.ActiveCaptionText;
            tabEvents.Location = new Point(4, 24);
            tabEvents.Margin = new Padding(3, 4, 3, 4);
            tabEvents.Name = "tabEvents";
            tabEvents.Padding = new Padding(3, 4, 3, 4);
            tabEvents.Size = new Size(978, 245);
            tabEvents.TabIndex = 0;
            tabEvents.Text = "Events";
            // 
            // tbEvents
            // 
            tbEvents.AcceptsReturn = true;
            tbEvents.AccessibleRole = AccessibleRole.Text;
            tbEvents.Anchor = AnchorStyles.None;
            tbEvents.BackColor = SystemColors.Window;
            tbEvents.BorderStyle = BorderStyle.FixedSingle;
            tbEvents.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbEvents.ForeColor = SystemColors.WindowText;
            tbEvents.ImeMode = ImeMode.NoControl;
            tbEvents.Location = new Point(0, 0);
            tbEvents.Margin = new Padding(0);
            tbEvents.MaximumSize = new Size(1000, 1000);
            tbEvents.MaxLength = 50000;
            tbEvents.MinimumSize = new Size(969, 201);
            tbEvents.Multiline = true;
            tbEvents.Name = "tbEvents";
            tbEvents.ReadOnly = true;
            tbEvents.ScrollBars = ScrollBars.Both;
            tbEvents.Size = new Size(979, 245);
            tbEvents.TabIndex = 1;
            // 
            // tabOutput
            // 
            tabOutput.BackColor = Color.Transparent;
            tabOutput.Controls.Add(tbOutput);
            tabOutput.ImeMode = ImeMode.Off;
            tabOutput.Location = new Point(4, 24);
            tabOutput.Margin = new Padding(3, 4, 3, 4);
            tabOutput.Name = "tabOutput";
            tabOutput.Padding = new Padding(3, 4, 3, 4);
            tabOutput.Size = new Size(978, 245);
            tabOutput.TabIndex = 1;
            tabOutput.Text = "Output";
            // 
            // tbOutput
            // 
            tbOutput.AcceptsReturn = true;
            tbOutput.AccessibleRole = AccessibleRole.Text;
            tbOutput.Anchor = AnchorStyles.None;
            tbOutput.BackColor = SystemColors.Window;
            tbOutput.BorderStyle = BorderStyle.FixedSingle;
            tbOutput.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbOutput.ForeColor = SystemColors.WindowText;
            tbOutput.ImeMode = ImeMode.NoControl;
            tbOutput.Location = new Point(0, 0);
            tbOutput.Margin = new Padding(0);
            tbOutput.MaximumSize = new Size(1000, 1000);
            tbOutput.MaxLength = 50000;
            tbOutput.MinimumSize = new Size(969, 201);
            tbOutput.Multiline = true;
            tbOutput.Name = "tbOutput";
            tbOutput.ReadOnly = true;
            tbOutput.ScrollBars = ScrollBars.Both;
            tbOutput.Size = new Size(979, 245);
            tbOutput.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.ErrorImage = (Image)resources.GetObject("pictureBox1.ErrorImage");
            pictureBox1.Image = Properties.Resources.BBSLogo;
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(6, 7);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(156, 65);
            pictureBox1.TabIndex = 21;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnPing);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(tbBaseTag);
            panel1.Controls.Add(lblSlot);
            panel1.Controls.Add(upDnSlot);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(tbIPAddress);
            panel1.Location = new Point(4, 77);
            panel1.Name = "panel1";
            panel1.Size = new Size(978, 100);
            panel1.TabIndex = 22;
            // 
            // btnPing
            // 
            btnPing.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPing.ForeColor = SystemColors.ActiveCaptionText;
            btnPing.Location = new Point(502, 50);
            btnPing.Name = "btnPing";
            btnPing.Size = new Size(81, 31);
            btnPing.TabIndex = 26;
            btnPing.Text = "Ping";
            toolTip1.SetToolTip(btnPing, "Ping controller to verify connection");
            btnPing.UseVisualStyleBackColor = true;
            btnPing.Click += btnPing_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(618, 19);
            label4.Name = "label4";
            label4.Size = new Size(111, 20);
            label4.TabIndex = 19;
            label4.Text = "Base Tag Name";
            // 
            // tbBaseTag
            // 
            tbBaseTag.Anchor = AnchorStyles.None;
            tbBaseTag.BackColor = SystemColors.Window;
            tbBaseTag.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbBaseTag.Location = new Point(618, 50);
            tbBaseTag.Margin = new Padding(3, 4, 3, 4);
            tbBaseTag.Name = "tbBaseTag";
            tbBaseTag.Size = new Size(357, 31);
            tbBaseTag.TabIndex = 18;
            toolTip1.SetToolTip(tbBaseTag, "PLC tag that contains event message strings (gMod1000_uaEventDetails)");
            // 
            // lblSlot
            // 
            lblSlot.AutoSize = true;
            lblSlot.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSlot.ForeColor = Color.White;
            lblSlot.Location = new Point(458, 19);
            lblSlot.Name = "lblSlot";
            lblSlot.Size = new Size(35, 20);
            lblSlot.TabIndex = 17;
            lblSlot.Text = "Slot";
            // 
            // upDnSlot
            // 
            upDnSlot.BorderStyle = BorderStyle.FixedSingle;
            upDnSlot.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            upDnSlot.Location = new Point(455, 50);
            upDnSlot.Margin = new Padding(3, 4, 3, 4);
            upDnSlot.Maximum = new decimal(new int[] { 9, 0, 0, 0 });
            upDnSlot.Name = "upDnSlot";
            upDnSlot.Size = new Size(41, 31);
            upDnSlot.TabIndex = 16;
            upDnSlot.TextAlign = HorizontalAlignment.Center;
            toolTip1.SetToolTip(upDnSlot, "Physical slot on Control Logix backplane. If installed in first slot or processor is Compact Logix set valu to 0");
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(0, 19);
            label1.Name = "label1";
            label1.Size = new Size(78, 20);
            label1.TabIndex = 15;
            label1.Text = "IP Address";
            // 
            // tbIPAddress
            // 
            tbIPAddress.BackColor = SystemColors.Window;
            tbIPAddress.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbIPAddress.Location = new Point(4, 50);
            tbIPAddress.Margin = new Padding(3, 4, 3, 4);
            tbIPAddress.Name = "tbIPAddress";
            tbIPAddress.Size = new Size(445, 31);
            tbIPAddress.TabIndex = 14;
            toolTip1.SetToolTip(tbIPAddress, "IP address of the processor");
            tbIPAddress.TextChanged += tbIPAddress_TextChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(label2);
            panel2.Controls.Add(btnFileSel);
            panel2.Controls.Add(tbFileSelect);
            panel2.Controls.Add(btnExportMsg);
            panel2.Controls.Add(btnImportTags);
            panel2.Location = new Point(4, 185);
            panel2.Name = "panel2";
            panel2.Size = new Size(978, 100);
            panel2.TabIndex = 23;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(0, 20);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.TabIndex = 24;
            label2.Text = "Select File";
            // 
            // btnFileSel
            // 
            btnFileSel.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFileSel.ForeColor = SystemColors.ActiveCaptionText;
            btnFileSel.Location = new Point(685, 48);
            btnFileSel.Name = "btnFileSel";
            btnFileSel.Size = new Size(47, 32);
            btnFileSel.TabIndex = 25;
            btnFileSel.Text = "...";
            btnFileSel.UseVisualStyleBackColor = true;
            btnFileSel.Click += btnFileSel_Click;
            // 
            // tbFileSelect
            // 
            tbFileSelect.BackColor = SystemColors.Window;
            tbFileSelect.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbFileSelect.Location = new Point(4, 49);
            tbFileSelect.Margin = new Padding(3, 4, 3, 4);
            tbFileSelect.Name = "tbFileSelect";
            tbFileSelect.Size = new Size(678, 31);
            tbFileSelect.TabIndex = 23;
            toolTip1.SetToolTip(tbFileSelect, "Select event message Excel workbook (HmiMessageTable_MyProjectName)");
            // 
            // btnExportMsg
            // 
            btnExportMsg.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExportMsg.ForeColor = SystemColors.ActiveCaptionText;
            btnExportMsg.Location = new Point(856, 49);
            btnExportMsg.Name = "btnExportMsg";
            btnExportMsg.Size = new Size(118, 31);
            btnExportMsg.TabIndex = 22;
            btnExportMsg.Text = "PLC Download";
            toolTip1.SetToolTip(btnExportMsg, "Download event detail strings from Excel to PLC");
            btnExportMsg.UseVisualStyleBackColor = true;
            btnExportMsg.Click += btnExportMsg_Click;
            // 
            // btnImportTags
            // 
            btnImportTags.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnImportTags.ForeColor = SystemColors.ActiveCaptionText;
            btnImportTags.Location = new Point(735, 49);
            btnImportTags.Name = "btnImportTags";
            btnImportTags.Size = new Size(118, 31);
            btnImportTags.TabIndex = 21;
            btnImportTags.Text = "PLC Upload";
            toolTip1.SetToolTip(btnImportTags, "Upload event detail strings from PLC to Excel");
            btnImportTags.UseVisualStyleBackColor = true;
            btnImportTags.Click += btnImportTags_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnDBExport);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(tbDBName);
            panel3.Location = new Point(622, 7);
            panel3.Name = "panel3";
            panel3.Size = new Size(361, 65);
            panel3.TabIndex = 24;
            // 
            // btnDBExport
            // 
            btnDBExport.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDBExport.ForeColor = SystemColors.ActiveCaptionText;
            btnDBExport.Location = new Point(263, 31);
            btnDBExport.Name = "btnDBExport";
            btnDBExport.Size = new Size(94, 31);
            btnDBExport.TabIndex = 23;
            btnDBExport.Text = "DB Export";
            toolTip1.SetToolTip(btnDBExport, "Export and create SQL database file *(BBS_db.sql)");
            btnDBExport.UseVisualStyleBackColor = true;
            btnDBExport.Click += btnDBExport_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(2, 1);
            label3.Name = "label3";
            label3.Size = new Size(116, 20);
            label3.TabIndex = 21;
            label3.Text = "Database Name";
            // 
            // tbDBName
            // 
            tbDBName.Anchor = AnchorStyles.None;
            tbDBName.BackColor = SystemColors.Window;
            tbDBName.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbDBName.Location = new Point(2, 32);
            tbDBName.Margin = new Padding(3, 4, 3, 4);
            tbDBName.Name = "tbDBName";
            tbDBName.Size = new Size(250, 31);
            tbDBName.TabIndex = 20;
            toolTip1.SetToolTip(tbDBName, "Desired database name BBS_db");
            // 
            // panel4
            // 
            panel4.Controls.Add(label5);
            panel4.Controls.Add(tbModule);
            panel4.Location = new Point(392, 7);
            panel4.Name = "panel4";
            panel4.Size = new Size(219, 65);
            panel4.TabIndex = 25;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(3, 0);
            label5.Name = "label5";
            label5.Size = new Size(60, 20);
            label5.TabIndex = 23;
            label5.Text = "Module";
            // 
            // tbModule
            // 
            tbModule.Anchor = AnchorStyles.None;
            tbModule.BackColor = SystemColors.Window;
            tbModule.Font = new Font("Segoe UI", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbModule.Location = new Point(3, 31);
            tbModule.Margin = new Padding(3, 4, 3, 4);
            tbModule.Name = "tbModule";
            tbModule.Size = new Size(213, 31);
            tbModule.TabIndex = 22;
            toolTip1.SetToolTip(tbModule, "Module number for SQL database export");
            // 
            // toolTip1
            // 
            toolTip1.AutomaticDelay = 250;
            toolTip1.BackColor = Color.PaleGoldenrod;
            toolTip1.IsBalloon = true;
            toolTip1.ToolTipIcon = ToolTipIcon.Info;
            toolTip1.Popup += toolTip1_Popup;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.Desktop;
            ClientSize = new Size(986, 600);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(pictureBox1);
            Controls.Add(progressBar);
            Controls.Add(tabPane);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.AppWorkspace;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Event Message Export Tool";
            Load += Form1_Load;
            FormClosing += Form1_FormClosing;
            tabPane.ResumeLayout(false);
            tabEvents.ResumeLayout(false);
            tabEvents.PerformLayout();
            tabOutput.ResumeLayout(false);
            tabOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)upDnSlot).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private OpenFileDialog openFileDialog;
        private TabControl tabPane;
        private TabPage tabEvents;
        private TabPage tabOutput;
        public ProgressBar progressBar;
        public TextBox tbEvents;
        public TextBox tbOutput;
        private PictureBox pictureBox1;
        private Panel panel1;
        private Label label4;
        public TextBox tbBaseTag;
        public Label lblSlot;
        public NumericUpDown upDnSlot;
        private Label label1;
        public TextBox tbIPAddress;
        private Panel panel2;
        private Button btnFileSel;
        private Label label2;
        public TextBox tbFileSelect;
        public Button btnExportMsg;
        public Button btnImportTags;
        public Button btnPing;
        private Panel panel3;
        private Label label3;
        public TextBox tbDBName;
        public Button btnDBExport;
        private Panel panel4;
        private Label label5;
        public TextBox tbModule;
        private ToolTip toolTip1;
    }
}
