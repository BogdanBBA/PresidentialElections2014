namespace PresidentialElections2014.Forms
{
	partial class FAbout
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.MenuP = new System.Windows.Forms.Panel();
			this.LogoPB = new System.Windows.Forms.PictureBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.LogoPB)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
			this.label1.Location = new System.Drawing.Point(422, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(371, 47);
			this.label1.TabIndex = 0;
			this.label1.Text = "Presidential elections";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(425, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(149, 30);
			this.label2.TabIndex = 1;
			this.label2.Text = "by BogdanBBA";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.LightBlue;
			this.label3.Location = new System.Drawing.Point(422, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(132, 32);
			this.label3.TabIndex = 2;
			this.label3.Text = "v1.0 alpha";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(560, 138);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(305, 30);
			this.label4.TabIndex = 3;
			this.label4.Text = "November 9th, 11th, 14th; 2014";
			// 
			// MenuP
			// 
			this.MenuP.Location = new System.Drawing.Point(520, 356);
			this.MenuP.Name = "MenuP";
			this.MenuP.Size = new System.Drawing.Size(300, 60);
			this.MenuP.TabIndex = 6;
			// 
			// LogoPB
			// 
			this.LogoPB.Location = new System.Drawing.Point(16, 16);
			this.LogoPB.Name = "LogoPB";
			this.LogoPB.Size = new System.Drawing.Size(400, 400);
			this.LogoPB.TabIndex = 4;
			this.LogoPB.TabStop = false;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Segoe UI Semilight", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(426, 93);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(269, 19);
			this.label5.TabIndex = 7;
			this.label5.Text = "Directly derived from GeographicalStatistics";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(560, 168);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(264, 30);
			this.label6.TabIndex = 9;
			this.label6.Text = "November 15th, 20th; 2014";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.LightBlue;
			this.label7.Location = new System.Drawing.Point(422, 166);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(120, 32);
			this.label7.TabIndex = 8;
			this.label7.Text = "v1.0 beta";
			// 
			// FAbout
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(912, 432);
			this.ControlBox = false;
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.MenuP);
			this.Controls.Add(this.LogoPB);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FAbout";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Geographical statistics / ABOUT";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.FAbout_Load);
			((System.ComponentModel.ISupportInitialize)(this.LogoPB)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox LogoPB;
		private System.Windows.Forms.Panel MenuP;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
	}
}