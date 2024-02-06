namespace non_modal_status_message_form
{
    partial class MessageForm
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
            labelMessage = new Label();
            groupBox1 = new GroupBox();
            checkBoxUserIsOnline = new CheckBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // labelMessage
            // 
            labelMessage.AutoSize = true;
            labelMessage.BackColor = Color.Azure;
            labelMessage.Location = new Point(39, 40);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new Size(62, 25);
            labelMessage.TabIndex = 0;
            labelMessage.Text = "Log In";
            labelMessage.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Azure;
            groupBox1.Controls.Add(checkBoxUserIsOnline);
            groupBox1.Location = new Point(39, 119);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(385, 102);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Test Conditions";
            // 
            // checkBoxUserIsOnline
            // 
            checkBoxUserIsOnline.AutoSize = true;
            checkBoxUserIsOnline.Location = new Point(38, 47);
            checkBoxUserIsOnline.Name = "checkBoxUserIsOnline";
            checkBoxUserIsOnline.Size = new Size(147, 29);
            checkBoxUserIsOnline.TabIndex = 1;
            checkBoxUserIsOnline.Text = "User Is Online";
            checkBoxUserIsOnline.UseVisualStyleBackColor = true;
            // 
            // MessageForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 266);
            Controls.Add(groupBox1);
            Controls.Add(labelMessage);
            Name = "MessageForm";
            Text = "MessageForm";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelMessage;
        private TextBox textBox1;
        private TextBox textBox2;
        private GroupBox groupBox1;
        private CheckBox checkBoxUserIsOnline;
    }
}