using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;

namespace Download_Manager
{
    public partial class Form : System.Windows.Forms.Form
    {
        private static int _DefaultConnectionLimit = ServicePointManager.DefaultConnectionLimit;    // initializing...

        public static int DefaultConnectionLimit
        {
            get
            {
                return _DefaultConnectionLimit;
            }
            set
            {
                ServicePointManager.DefaultConnectionLimit = _DefaultConnectionLimit = value;
            }
        }

        private static readonly object[] segments = { 1, 2, 4, 8, 16, 24, 32 };

        private static FileInformation fileInformation;

        public Form()
        {
            InitializeComponent();
        }

        private void FormLoad(object sender, EventArgs e)
        {
            if (sender.Equals(this))
            {
                if (ServicePointManager.Expect100Continue)
                {
                    ServicePointManager.Expect100Continue = false;
                }

                comboBoxSegments.Items.AddRange(segments);
                comboBoxSegments.SelectedIndex = 3;
            }
        }

        // need to check if downloading is not running...
        private void PasteURL()
        {
            string text = Clipboard.GetText();

            if (DownloadManager.IsURL(text))
            {
                textBoxDownloadLink.Text = text;

                try
                {
                    fileInformation = new FileInformation(textBoxDownloadLink.Text, textBoxSaveTo.Text);
                    textBoxFileName.Text = fileInformation.name;
                    labelReceived.Text = "0.0 Bytes of " + fileInformation.size;
                    labelStatus.Text = "Ready";
                }
                catch (Exception exception)
                {
                    textBoxFileName.Text = "Unavailable";
                    labelReceived.Text = "0.0 Bytes of 0.0 Bytes";
                    labelStatus.Text = exception.Message;
                }

                // Debugger.Log(0, "Check...", "FILE NAME = " + fileInformation.name);
            }
        }

        private void FormActivated(object sender, EventArgs e)
        {
            if (sender.Equals(this))
            {
                PasteURL();
            }
        }

        private void TextBoxTextChanged(object sender, EventArgs e)
        {
            if (sender.Equals(textBoxSaveTo))
            {
                Color color = Color.Gray;

                if (Directory.Exists(textBoxSaveTo.Text))
                {
                    color = Color.Black;

                    if (fileInformation != null)        // must check if downloading is not running...
                    {
                        fileInformation.saveTo = textBoxSaveTo.Text;
                    }
                }

                textBoxSaveTo.ForeColor = color;
            }
        }

        private void ComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender.Equals(comboBoxSegments))
            {
                DefaultConnectionLimit = int.Parse(segments[comboBoxSegments.SelectedIndex].ToString());
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            CustomButton customButton = sender as CustomButton;

            if (customButton.Active)
            {
                if (customButton.Equals(buttonPaste))
                {
                    PasteURL();
                }
                else if (customButton.Equals(buttonBrowse))
                {
                    if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        textBoxSaveTo.Text = folderBrowserDialog.SelectedPath;
                    }
                }

                if (customButton.Equals(buttonStart))
                {
                    backgroundWorker.RunWorkerAsync();
                }
            }
        }

        private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            if (sender.Equals(backgroundWorker))
            {
                DownloadManager dm = new DownloadManager(fileInformation, progressBarStatus);
                dm.Start();
            }
        }
    }
}