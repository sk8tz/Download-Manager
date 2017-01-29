using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Download_Manager
{
    public partial class Form : System.Windows.Forms.Form
    {
        public enum State
        {
            Ready,
            RetrievingFileInformation,
            Preparing,
            Downloading,
            Paused,
            Appending,
            Finished,
            Error
        };

        public static int countSegments = 8;
        private static readonly object[] segments = { 1, 2, 4, 8, 16, 24, 32 };

        public static volatile State state = State.Ready;       // will be changed how state enum is used...

        private static FileInformation fileInformation;
        private static Controls controls;
        private static DownloadManager downloadManager;

        public Form()
        {
            InitializeComponent();

            controls = new Controls(this);
            controls.labelSpeed = labelSpeed;
            controls.labelReceived = labelReceived;
            controls.labelTimeElapsed = labelTimeElapsed;
            controls.labelStatus = labelStatus;
            controls.progressBar = progressBar;
            controls.progressBarStatus = progressBarStatus;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            if (sender.Equals(this))
            {
                ServicePointManager.DefaultConnectionLimit = 32;        // maximum number of connections allowed...

                if (ServicePointManager.Expect100Continue)
                {
                    ServicePointManager.Expect100Continue = false;
                }

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemoteCertificateValidation);

                comboBoxSegments.Items.AddRange(segments);
                comboBoxSegments.SelectedIndex = 3;
            }
        }

        private void FormActivated(object sender, EventArgs e)
        {
            if (sender.Equals(this))      // duplicate code...
            {
                PasteURL();
            }
        }

        public bool RemoteCertificateValidation(object sender, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private void EnableControls(bool enable)
        {
            textBoxSaveTo.Enabled = comboBoxSegments.Enabled = textBoxFileName.Enabled =
                buttonBrowse.Active = buttonPaste.Active = buttonPause.Active = buttonStart.Active = enable;

            if (enable)
            {
                progressBarStatus.Style = ProgressBarStyle.Blocks;
            }
            else
            {
                progressBarStatus.Style = ProgressBarStyle.Marquee;
            }
        }

        private void PasteURL()
        {
            if (state != State.RetrievingFileInformation && state != State.Downloading && state != State.Preparing)
            {
                state = State.RetrievingFileInformation;

                string text = Clipboard.GetText();

                if (DownloadManager.IsURL(text))
                {
                    textBoxDownloadLink.Text = text;

                    EnableControls(false);

                    try
                    {
                        backgroundWorker.RunWorkerAsync(textBoxDownloadLink.Text + "[{(split-here)}]" + textBoxSaveTo.Text);

                        labelStatus.Text = "Retrieving file information";
                    }
                    catch
                    {

                    }
                }
                else
                {
                    state = State.Ready;
                }
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
            else if (sender.Equals(textBoxFileName))
            {
                if (fileInformation != null)
                {
                    fileInformation.name = textBoxFileName.Text;
                }
            }
        }

        private void ComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender.Equals(comboBoxSegments))
            {
                countSegments = int.Parse(segments[comboBoxSegments.SelectedIndex].ToString());
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            CustomButton customButton = sender as CustomButton;

            if (customButton.Active)
            {
                if (customButton.Equals(buttonPaste))      // duplicate code...
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
                else if (customButton.Equals(buttonPause))
                {
                    state = State.Paused;
                }
                else if (customButton.Equals(buttonStart))      // duplicate code...
                {
                    state = State.Preparing;

                    labelStatus.Text = "Preparing to download";

                    try
                    {
                        backgroundWorker.RunWorkerAsync();
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            if (sender.Equals(backgroundWorker))
            {
                if (state == State.RetrievingFileInformation)
                {
                    try
                    {
                        string[] substrings = (e.Argument as string).Split(new string[] { "[{(split-here)}]" } , StringSplitOptions.None);
                        fileInformation = new FileInformation(substrings[0], substrings[1]);

                        e.Result = "successful";
                    }
                    catch (Exception exception)
                    {
                        fileInformation = null;

                        e.Result = exception.Message;
                    }
                }
                else if (state == State.Preparing)
                {
                    downloadManager = new DownloadManager(fileInformation, controls);
                    downloadManager.Start();
                }
            }
        }

        private void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (sender.Equals(backgroundWorker))
            {
                if (state == State.RetrievingFileInformation)
                {
                    string result = e.Result as string;

                    if (result == "successful")
                    {
                        textBoxFileName.Text = fileInformation.name;
                        labelReceived.Text = "0.0 Bytes of " + fileInformation.size;
                        labelStatus.Text = "Ready";
                    }
                    else
                    {
                        textBoxFileName.Text = "Unavailable";
                        labelReceived.Text = "0.0 Bytes of 0.0 Bytes";
                        labelStatus.Text = result;
                    }

                    EnableControls(true);

                    state = State.Ready;
                }
            }
        }
    }
}