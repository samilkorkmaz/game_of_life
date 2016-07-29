namespace Game_of_Life
{
    partial class View
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
            this.cbPattern = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbPattern
            // 
            this.cbPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPattern.FormattingEnabled = true;
            this.cbPattern.Items.AddRange(new object[] {
            "Blinker",
            "Glider",
            "RPentomino"});
            this.cbPattern.Location = new System.Drawing.Point(266, 2);
            this.cbPattern.Name = "cbPattern";
            this.cbPattern.Size = new System.Drawing.Size(121, 21);
            this.cbPattern.TabIndex = 0;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 296);
            this.Controls.Add(this.cbPattern);
            this.Name = "View";
            this.Text = "Game of Life - Jul 2016";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.View_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ComboBox cbPattern;
    }
}

