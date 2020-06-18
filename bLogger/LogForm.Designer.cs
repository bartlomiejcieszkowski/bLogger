namespace bLogger.Forms
{
	partial class LogForm
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
			this.logBox = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			//
			// logBox
			//
			this.logBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logBox.FormattingEnabled = true;
			this.logBox.HorizontalScrollbar = true;
			this.logBox.Location = new System.Drawing.Point(0, 0);
			this.logBox.Margin = new System.Windows.Forms.Padding(0);
			this.logBox.Name = "logBox";
			this.logBox.ScrollAlwaysVisible = true;
			this.logBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.logBox.Size = new System.Drawing.Size(284, 261);
			this.logBox.TabIndex = 0;
			this.logBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LogBox_KeyDown);
			this.logBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LogBox_MouseDown);
			//
			// LogForm
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.logBox);
			this.Name = "LogForm";
			this.Text = "Log Window";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox logBox;
	}
}