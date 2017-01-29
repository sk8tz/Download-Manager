using System;
using System.Windows.Forms;

namespace Download_Manager
{
    public class Controls
    {
        private Form parent;
        public Label labelSpeed, labelReceived, labelTimeElapsed;
        public ToolStripStatusLabel labelStatus;
        public ProgressBar progressBar;
        public ToolStripProgressBar progressBarStatus;

        private Action action;

        public Controls(Form parent)
        {
            this.parent = parent;
        }

        private void UpdateLabel(string text, Label label)
        {
            parent.Invoke(action = () =>
            {
                label.Text = text;
            });
        }

        public void UpdateSpeed(string speed)
        {
            UpdateLabel(speed, labelSpeed);
        }

        public void UpdateReceived(string received)
        {
            UpdateLabel(received, labelReceived);
        }

        public void UpdateStatus(string status)
        {
            parent.Invoke(action = () =>
            {
                labelStatus.Text = status;
            });
        }

        public void UpdateProgressBarValue(int value)
        {
            parent.Invoke(action = () =>
            {
                progressBar.Value = value;
            });
        }

        public void UpdateProgressBarStatusValue(int value)
        {
            parent.Invoke(action = () =>
            {
                progressBarStatus.Value = value;
            });
        }
    }
}