## Non-model status message form

[![initial][1]][1]

One thing you could experiment with is making the `SendMessage` method awaitable by using a `SemaphoreSlim` in the class.

```csharp
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
```

___

When the user is logged in, the message posts to the `MessageForm` label and returns immediately (even before the label text is populated with the message). So if in `MainForm`, if you disable the main form first and the user is logged in, the main for reenables so quickly you never know that it blinked.
___
**Successful login flow**
[![successful login flow.][3]][3]

```
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
                                if(!await MessageForm.SendMessage(this, textBoxSendMessage.Text))
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
    private MessageForm MessageForm { get; } = new MessageForm();
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        // Make sure placeholder shows.
        BeginInvoke(()=> ActiveControl = null);
    }
}
```


___
But if the user isn't ligged in, the `MainForm` remains disabled until user either succeeds in logging in or cancels.

**Cancelled login flow**
[![cancel login flow][2]][2]



  [1]: https://i.stack.imgur.com/Tttrv.png
  [2]: https://i.stack.imgur.com/S2Eqt.png
  [3]: https://i.stack.imgur.com/31Mq6.png