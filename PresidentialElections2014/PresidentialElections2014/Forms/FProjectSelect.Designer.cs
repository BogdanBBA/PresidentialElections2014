namespace PresidentialElections2014
{
	partial class FProjectSelect
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FProjectSelect));
			this.ProjectsCB = new System.Windows.Forms.ComboBox();
			this.MenuP = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ProjectsCB
			// 
			this.ProjectsCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ProjectsCB.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ProjectsCB.FormattingEnabled = true;
			this.ProjectsCB.Location = new System.Drawing.Point(81, 123);
			this.ProjectsCB.Margin = new System.Windows.Forms.Padding(6);
			this.ProjectsCB.MaxDropDownItems = 20;
			this.ProjectsCB.Name = "ProjectsCB";
			this.ProjectsCB.Size = new System.Drawing.Size(600, 38);
			this.ProjectsCB.TabIndex = 0;
			// 
			// MenuP
			// 
			this.MenuP.Location = new System.Drawing.Point(81, 183);
			this.MenuP.Name = "MenuP";
			this.MenuP.Size = new System.Drawing.Size(600, 60);
			this.MenuP.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(51, 87);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(258, 30);
			this.label2.TabIndex = 4;
			this.label2.Text = "Select a database to open:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
			this.label4.Location = new System.Drawing.Point(24, 25);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(371, 47);
			this.label4.TabIndex = 3;
			this.label4.Text = "Presidential elections";
			// 
			// FProjectSelect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(777, 302);
			this.ControlBox = false;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.MenuP);
			this.Controls.Add(this.ProjectsCB);
			this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(6);
			this.Name = "FProjectSelect";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Geographical statistics / MENU";
			this.Load += new System.EventHandler(this.FMenu_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox ProjectsCB;
		private System.Windows.Forms.Panel MenuP;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
	}
}

