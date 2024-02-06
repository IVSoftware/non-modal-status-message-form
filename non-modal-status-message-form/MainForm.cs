

namespace non_modal_status_message_form
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            textBoxSendMessage.KeyDown += (sender, e) =>
            {
                switch (e.KeyData) 
                {
                    case Keys.Enter:
                        // Handle message and suppress audible entry sound.
                        e.Handled = e.SuppressKeyPress = true;
                        if (!string.IsNullOrEmpty(textBoxSendMessage.Text))
                        {
                            BeginInvoke(async () =>
                            {
                                // Do not block the key event for this.
                                try
                                {
                                    Enabled = false;
                                    if(!await MessageForm.SendMessageAsync(this, textBoxSendMessage.Text))
                                    {
                                        BeginInvoke(()=>BringToFront());
                                        BeginInvoke(()=>MessageBox.Show("User is offline"));
                                        BeginInvoke(()=>BringToFront());
                                    }
                                }
                                finally
                                {
                                    Enabled = true;
                                }
                            });
                        }
                        break;
                }
            };
        }
        private MessengerForm MessageForm { get; } = new MessengerForm();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Make sure placeholder shows.
            BeginInvoke(()=> ActiveControl = null);
        }
    }
}
