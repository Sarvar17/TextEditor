namespace TextEditor
{
    partial class Help
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Help));
            this.textEditorLabel = new System.Windows.Forms.Label();
            this.hotKeysLabel = new System.Windows.Forms.Label();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // textEditorLabel
            // 
            this.textEditorLabel.AutoSize = true;
            this.textEditorLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textEditorLabel.Location = new System.Drawing.Point(78, 15);
            this.textEditorLabel.Name = "textEditorLabel";
            this.textEditorLabel.Size = new System.Drawing.Size(168, 21);
            this.textEditorLabel.TabIndex = 0;
            this.textEditorLabel.Text = "Text Editor Notepad+";
            // 
            // hotKeysLabel
            // 
            this.hotKeysLabel.AutoSize = true;
            this.hotKeysLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.hotKeysLabel.Location = new System.Drawing.Point(132, 43);
            this.hotKeysLabel.Name = "hotKeysLabel";
            this.hotKeysLabel.Size = new System.Drawing.Size(60, 19);
            this.hotKeysLabel.TabIndex = 0;
            this.hotKeysLabel.Text = "HotKeys";
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(25, 73);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(283, 272);
            this.richTextBox.TabIndex = 1;
            this.richTextBox.Text = resources.GetString("richTextBox.Text");
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 357);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.hotKeysLabel);
            this.Controls.Add(this.textEditorLabel);
            this.MaximumSize = new System.Drawing.Size(350, 396);
            this.MinimumSize = new System.Drawing.Size(350, 396);
            this.Name = "Help";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label textEditorLabel;
        private System.Windows.Forms.Label hotKeysLabel;
        private System.Windows.Forms.RichTextBox richTextBox;
    }
}