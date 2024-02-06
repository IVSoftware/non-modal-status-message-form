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
        public MessageForm() => InitializeComponent();

        public bool SendMessage(IWin32Window owner, string text)
        {
            if (!checkBoxUserIsOnline.Checked)
            {
                ExecLogInFlow();
            }
            if (checkBoxUserIsOnline.Checked)
            {
                if (owner is Form form)
                {
                    Location = new Point(
                        form.Location.X + form.Width + 10,
                        form.Location.Y
                   );
                }
                if (!Visible) Show(owner);
                BeginInvoke(() =>
                {
                    labelMessage.Text = text;
                });
            }
            else Hide();
            return checkBoxUserIsOnline.Checked;
        }

        private void ExecLogInFlow()
        {
            checkBoxUserIsOnline.Checked = 
                DialogResult.Yes == 
                MessageBox.Show(this, "Are you a valid user?", "Log In", MessageBoxButtons.YesNo);
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
            base.OnFormClosing(e);
        }
    }
}
