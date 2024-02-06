namespace non_modal_status_message_form
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxSendMessage = new TextBox();
            SuspendLayout();
            // 
            // textBoxSendMessage
            // 
            textBoxSendMessage.Font = new Font("Segoe UI", 12F);
            textBoxSendMessage.Location = new Point(38, 51);
            textBoxSendMessage.Name = "textBoxSendMessage";
            textBoxSendMessage.PlaceholderText = "Send Message";
            textBoxSendMessage.Size = new Size(273, 39);
            textBoxSendMessage.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 244);
            Controls.Add(textBoxSendMessage);
            Name = "MainForm";
            Text = "Main Form";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxSendMessage;
    }
}
