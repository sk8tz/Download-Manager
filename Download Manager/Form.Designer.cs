namespace Download_Manager
{
    partial class Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.textBoxDownloadLink = new System.Windows.Forms.TextBox();
            this.textBoxSaveTo = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.labelDownloadLink = new System.Windows.Forms.Label();
            this.labelSaveTo = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelEmpty = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBarStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.labelFileName = new System.Windows.Forms.Label();
            this._labelSpeed = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.labelReceived = new System.Windows.Forms.Label();
            this._labelReceived = new System.Windows.Forms.Label();
            this.comboBoxSegments = new System.Windows.Forms.ComboBox();
            this.labelSegments = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.labelTimeElapsed = new System.Windows.Forms.Label();
            this._labelTimeElapsed = new System.Windows.Forms.Label();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.buttonPaste = new Download_Manager.CustomButton();
            this.buttonBrowse = new Download_Manager.CustomButton();
            this.buttonPause = new Download_Manager.CustomButton();
            this.buttonStart = new Download_Manager.CustomButton();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxDownloadLink
            // 
            this.textBoxDownloadLink.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDownloadLink.Location = new System.Drawing.Point(121, 78);
            this.textBoxDownloadLink.Name = "textBoxDownloadLink";
            this.textBoxDownloadLink.ReadOnly = true;
            this.textBoxDownloadLink.Size = new System.Drawing.Size(270, 22);
            this.textBoxDownloadLink.TabIndex = 6;
            // 
            // textBoxSaveTo
            // 
            this.textBoxSaveTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxSaveTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.textBoxSaveTo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSaveTo.Location = new System.Drawing.Point(121, 12);
            this.textBoxSaveTo.Name = "textBoxSaveTo";
            this.textBoxSaveTo.Size = new System.Drawing.Size(270, 22);
            this.textBoxSaveTo.TabIndex = 1;
            this.textBoxSaveTo.Text = "F:\\Test";
            this.textBoxSaveTo.TextChanged += new System.EventHandler(this.TextBoxTextChanged);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 220);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(467, 15);
            this.progressBar.TabIndex = 16;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerDoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerRunWorkerCompleted);
            // 
            // labelDownloadLink
            // 
            this.labelDownloadLink.AutoSize = true;
            this.labelDownloadLink.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDownloadLink.ForeColor = System.Drawing.Color.LightGray;
            this.labelDownloadLink.Location = new System.Drawing.Point(12, 82);
            this.labelDownloadLink.Name = "labelDownloadLink";
            this.labelDownloadLink.Size = new System.Drawing.Size(86, 15);
            this.labelDownloadLink.TabIndex = 5;
            this.labelDownloadLink.Text = "Download link";
            // 
            // labelSaveTo
            // 
            this.labelSaveTo.AutoSize = true;
            this.labelSaveTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSaveTo.ForeColor = System.Drawing.Color.LightGray;
            this.labelSaveTo.Location = new System.Drawing.Point(12, 16);
            this.labelSaveTo.Name = "labelSaveTo";
            this.labelSaveTo.Size = new System.Drawing.Size(49, 15);
            this.labelSaveTo.TabIndex = 0;
            this.labelSaveTo.Text = "Save to";
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.DodgerBlue;
            this.statusStrip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus,
            this.labelEmpty,
            this.progressBarStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 283);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(494, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 19;
            // 
            // labelStatus
            // 
            this.labelStatus.ForeColor = System.Drawing.Color.White;
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(45, 17);
            this.labelStatus.Text = "Ready";
            // 
            // labelEmpty
            // 
            this.labelEmpty.Name = "labelEmpty";
            this.labelEmpty.Size = new System.Drawing.Size(301, 17);
            this.labelEmpty.Spring = true;
            // 
            // progressBarStatus
            // 
            this.progressBarStatus.Name = "progressBarStatus";
            this.progressBarStatus.Size = new System.Drawing.Size(100, 16);
            // 
            // labelFileName
            // 
            this.labelFileName.AutoSize = true;
            this.labelFileName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFileName.ForeColor = System.Drawing.Color.LightGray;
            this.labelFileName.Location = new System.Drawing.Point(12, 114);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(61, 15);
            this.labelFileName.TabIndex = 8;
            this.labelFileName.Text = "File name";
            // 
            // _labelSpeed
            // 
            this._labelSpeed.AutoSize = true;
            this._labelSpeed.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelSpeed.ForeColor = System.Drawing.Color.LightGray;
            this._labelSpeed.Location = new System.Drawing.Point(12, 143);
            this._labelSpeed.Name = "_labelSpeed";
            this._labelSpeed.Size = new System.Drawing.Size(43, 15);
            this._labelSpeed.TabIndex = 10;
            this._labelSpeed.Text = "Speed";
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSpeed.ForeColor = System.Drawing.Color.White;
            this.labelSpeed.Location = new System.Drawing.Point(118, 142);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(83, 16);
            this.labelSpeed.TabIndex = 11;
            this.labelSpeed.Text = "0.0 Byte(s)/s";
            // 
            // labelReceived
            // 
            this.labelReceived.AutoSize = true;
            this.labelReceived.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReceived.ForeColor = System.Drawing.Color.White;
            this.labelReceived.Location = new System.Drawing.Point(118, 168);
            this.labelReceived.Name = "labelReceived";
            this.labelReceived.Size = new System.Drawing.Size(154, 16);
            this.labelReceived.TabIndex = 13;
            this.labelReceived.Text = "0.0 Byte(s) of 0.0 Byte(s)";
            // 
            // _labelReceived
            // 
            this._labelReceived.AutoSize = true;
            this._labelReceived.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelReceived.ForeColor = System.Drawing.Color.LightGray;
            this._labelReceived.Location = new System.Drawing.Point(12, 169);
            this._labelReceived.Name = "_labelReceived";
            this._labelReceived.Size = new System.Drawing.Size(59, 15);
            this._labelReceived.TabIndex = 12;
            this._labelReceived.Text = "Received";
            // 
            // comboBoxSegments
            // 
            this.comboBoxSegments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSegments.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSegments.FormattingEnabled = true;
            this.comboBoxSegments.Location = new System.Drawing.Point(121, 44);
            this.comboBoxSegments.Name = "comboBoxSegments";
            this.comboBoxSegments.Size = new System.Drawing.Size(361, 24);
            this.comboBoxSegments.TabIndex = 4;
            this.comboBoxSegments.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSelectedIndexChanged);
            // 
            // labelSegments
            // 
            this.labelSegments.AutoSize = true;
            this.labelSegments.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSegments.ForeColor = System.Drawing.Color.LightGray;
            this.labelSegments.Location = new System.Drawing.Point(12, 49);
            this.labelSegments.Name = "labelSegments";
            this.labelSegments.Size = new System.Drawing.Size(65, 15);
            this.labelSegments.TabIndex = 3;
            this.labelSegments.Text = "Segments";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.SelectedPath = "Downloads";
            // 
            // labelTimeElapsed
            // 
            this.labelTimeElapsed.AutoSize = true;
            this.labelTimeElapsed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimeElapsed.ForeColor = System.Drawing.Color.White;
            this.labelTimeElapsed.Location = new System.Drawing.Point(118, 194);
            this.labelTimeElapsed.Name = "labelTimeElapsed";
            this.labelTimeElapsed.Size = new System.Drawing.Size(76, 16);
            this.labelTimeElapsed.TabIndex = 15;
            this.labelTimeElapsed.Text = "0 second(s)";
            // 
            // _labelTimeElapsed
            // 
            this._labelTimeElapsed.AutoSize = true;
            this._labelTimeElapsed.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelTimeElapsed.ForeColor = System.Drawing.Color.LightGray;
            this._labelTimeElapsed.Location = new System.Drawing.Point(12, 195);
            this._labelTimeElapsed.Name = "_labelTimeElapsed";
            this._labelTimeElapsed.Size = new System.Drawing.Size(83, 15);
            this._labelTimeElapsed.TabIndex = 14;
            this._labelTimeElapsed.Text = "Time elapsed";
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFileName.Location = new System.Drawing.Point(121, 110);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(361, 22);
            this.textBoxFileName.TabIndex = 9;
            this.textBoxFileName.TextChanged += new System.EventHandler(this.TextBoxTextChanged);
            // 
            // buttonPaste
            // 
            this.buttonPaste.Active = true;
            this.buttonPaste.BackColor = System.Drawing.Color.Black;
            this.buttonPaste.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.buttonPaste.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.buttonPaste.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonPaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPaste.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPaste.ForeColor = System.Drawing.Color.White;
            this.buttonPaste.Location = new System.Drawing.Point(397, 74);
            this.buttonPaste.Name = "buttonPaste";
            this.buttonPaste.Size = new System.Drawing.Size(85, 30);
            this.buttonPaste.TabIndex = 7;
            this.buttonPaste.Text = "Paste";
            this.buttonPaste.UseVisualStyleBackColor = false;
            this.buttonPaste.Click += new System.EventHandler(this.ButtonClick);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Active = true;
            this.buttonBrowse.BackColor = System.Drawing.Color.Black;
            this.buttonBrowse.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.buttonBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.buttonBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBrowse.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBrowse.ForeColor = System.Drawing.Color.White;
            this.buttonBrowse.Location = new System.Drawing.Point(397, 8);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(85, 30);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = false;
            this.buttonBrowse.Click += new System.EventHandler(this.ButtonClick);
            // 
            // buttonPause
            // 
            this.buttonPause.Active = true;
            this.buttonPause.BackColor = System.Drawing.Color.Black;
            this.buttonPause.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.buttonPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.buttonPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPause.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPause.ForeColor = System.Drawing.Color.White;
            this.buttonPause.Location = new System.Drawing.Point(306, 245);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(85, 30);
            this.buttonPause.TabIndex = 17;
            this.buttonPause.Text = "Pause";
            this.buttonPause.UseVisualStyleBackColor = false;
            this.buttonPause.Click += new System.EventHandler(this.ButtonClick);
            // 
            // buttonStart
            // 
            this.buttonStart.Active = true;
            this.buttonStart.BackColor = System.Drawing.Color.Black;
            this.buttonStart.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.buttonStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.buttonStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.ForeColor = System.Drawing.Color.White;
            this.buttonStart.Location = new System.Drawing.Point(397, 245);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(85, 30);
            this.buttonStart.TabIndex = 18;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.ButtonClick);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(494, 305);
            this.Controls.Add(this.textBoxFileName);
            this.Controls.Add(this.labelTimeElapsed);
            this.Controls.Add(this._labelTimeElapsed);
            this.Controls.Add(this.labelSegments);
            this.Controls.Add(this.comboBoxSegments);
            this.Controls.Add(this.buttonPaste);
            this.Controls.Add(this.labelReceived);
            this.Controls.Add(this._labelReceived);
            this.Controls.Add(this.labelSpeed);
            this.Controls.Add(this._labelSpeed);
            this.Controls.Add(this.labelFileName);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.labelSaveTo);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelDownloadLink);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.textBoxSaveTo);
            this.Controls.Add(this.textBoxDownloadLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form";
            this.Text = "Download Manager";
            this.Activated += new System.EventHandler(this.FormActivated);
            this.Load += new System.EventHandler(this.FormLoad);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDownloadLink;
        private System.Windows.Forms.Label labelSaveTo;
        private System.Windows.Forms.Label labelSegments;
        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.Label _labelSpeed;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label _labelReceived;
        private System.Windows.Forms.Label labelReceived;
        private System.Windows.Forms.Label _labelTimeElapsed;
        private System.Windows.Forms.Label labelTimeElapsed;
        private System.Windows.Forms.TextBox textBoxSaveTo;
        private System.Windows.Forms.TextBox textBoxDownloadLink;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.ComboBox comboBoxSegments;
        private CustomButton buttonPaste;
        private CustomButton buttonBrowse;
        private CustomButton buttonPause;
        private CustomButton buttonStart;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ToolStripStatusLabel labelEmpty;
        private System.Windows.Forms.ToolStripProgressBar progressBarStatus;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}