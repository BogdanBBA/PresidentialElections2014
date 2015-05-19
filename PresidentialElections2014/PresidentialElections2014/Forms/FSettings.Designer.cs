namespace PresidentialElections2014.Forms
{
	partial class FSettings
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
			this.MenuP = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// MenuP
			// 
			this.MenuP.Location = new System.Drawing.Point(175, 156);
			this.MenuP.Name = "MenuP";
			this.MenuP.Size = new System.Drawing.Size(300, 60);
			this.MenuP.TabIndex = 1;
			// 
			// FSettings
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(650, 372);
			this.Controls.Add(this.MenuP);
			this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FSettings";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Geographical statistics / SETTINGS";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.FSettings_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel MenuP;
	}
}