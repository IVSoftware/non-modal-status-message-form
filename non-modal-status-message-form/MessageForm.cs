using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static non_modal_status_message_form.MessageForm;

namespace non_modal_status_message_form
{
    public partial class MessageForm : Form
    {
        public MessageForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            checkBoxUserIsOnline.CheckedChanged += (sender, e) =>
            {
                if(checkBoxUserIsOnline.Checked)
                {
                    _loginBusy.Release();
                }
            };
        }

        SemaphoreSlim _loginBusy = new SemaphoreSlim(1, 1);
        public async Task<bool> SendMessage(IWin32Window owner, string text)
        {
            if (!Visible)
            {
                if (owner is Form form)
                {
                    Location = new Point(
                        form.Location.X + form.Width + 10,
                        form.Location.Y
                   );
                }
                Show(owner);
            }
            if (!checkBoxUserIsOnline.Checked)
            {
                await ExecLogInFlow();
            }
            if (checkBoxUserIsOnline.Checked)
            {
                BeginInvoke(() =>
                {
                    labelMessage.Text = text;
                });
            }
            else Hide();
            return checkBoxUserIsOnline.Checked;
        }

        private async Task ExecLogInFlow()
        {
            try
            {
                _loginBusy.Wait(0);             // Start awaiter
                await _loginBusy.WaitAsync();   // Wait for checkbox.checked to be true.
            }
            finally
            {
                _loginBusy.Release();
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    Hide();
                    break;
                default:
                    break;
            }
            Release();
            base.OnFormClosing(e);
        }

        public void Release()
        {
            // Make sure there's something to release.
            _loginBusy.Wait(0);
            _loginBusy.Release();
        }
    }
}
