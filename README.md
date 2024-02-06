## Non-modal status message form
[![initial][1]][1]

Here's a sample implementation for your `async Task SendMessageAsync` method that solves the issue of the `MessengerForm` not being able to load and that keeps it responsive throughout including the ability to cancel a login that is in progress. You stated that "to continue further execution" the user must be logged in, so in that case the main for caller is disabled until the Status is Online.

```csharp
enum Status
{
    Offline,
    Online,
}
public partial class MessengerForm : Form
{
    public MessengerForm()
    {
        InitializeComponent();
        StartPosition = FormStartPosition.Manual;
        checkBoxUserIsOnline.CheckedChanged += (sender, e) =>
        {
            if(checkBoxUserIsOnline.Checked)
            {
                _loginBusy.Release();
            }
            textBoxUserName.Visible = 
            textBoxPassword.Visible = 
                !checkBoxUserIsOnline.Checked;
        };
    }

    SemaphoreSlim _loginBusy = new SemaphoreSlim(1, 1);
    public async Task<bool> SendMessageAsync(IWin32Window owner, string text)
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
            await GetCurrentUserStatusAsyncMock();
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

    private async Task<Status> GetCurrentUserStatusAsyncMock()
    {
        try
        {
            _loginBusy.Wait(0);             // Start awaiter
            await _loginBusy.WaitAsync();   // Wait for checkbox.checked to be true.
            return checkBoxUserIsOnline.Checked ? Status.Online : Status.Offline;
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
```


___
But if the user isn't logged in, the `MainForm` remains disabled until user either succeeds in logging in or cancels.

**Cancelled login flow**
[![cancel login flow][2]][2]



  [1]: https://i.stack.imgur.com/Tttrv.png
  [2]: https://i.stack.imgur.com/S2Eqt.png
  [3]: https://i.stack.imgur.com/31Mq6.png