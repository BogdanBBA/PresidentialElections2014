namespace PresidentialElections2014.Forms
{
	partial class FFilters
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
			this.EbureausL = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.EBureauP = new System.Windows.Forms.Panel();
			this.RegionsMenuP = new System.Windows.Forms.Panel();
			this.RegionsL = new System.Windows.Forms.Label();
			this.RegionsInfoL = new System.Windows.Forms.Label();
			this.RegionsCB = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// MenuP
			// 
			this.MenuP.Location = new System.Drawing.Point(341, 612);
			this.MenuP.Name = "MenuP";
			this.MenuP.Size = new System.Drawing.Size(300, 60);
			this.MenuP.TabIndex = 0;
			// 
			// EbureausL
			// 
			this.EbureausL.AutoSize = true;
			this.EbureausL.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.EbureausL.ForeColor = System.Drawing.Color.LightBlue;
			this.EbureausL.Location = new System.Drawing.Point(33, 251);
			this.EbureausL.Name = "EbureausL";
			this.EbureausL.Size = new System.Drawing.Size(94, 32);
			this.EbureausL.TabIndex = 8;
			this.EbureausL.Text = "EBs (n)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(34, 65);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(729, 30);
			this.label2.TabIndex = 7;
			this.label2.Text = "Select the electoral bureaus from which you want information to be displayed";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
			this.label4.Location = new System.Drawing.Point(31, 18);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(394, 47);
			this.label4.TabIndex = 6;
			this.label4.Text = "Electoral bureau filters";
			// 
			// EBureauP
			// 
			this.EBureauP.Location = new System.Drawing.Point(39, 286);
			this.EBureauP.Name = "EBureauP";
			this.EBureauP.Size = new System.Drawing.Size(900, 320);
			this.EBureauP.TabIndex = 9;
			// 
			// RegionsMenuP
			// 
			this.RegionsMenuP.Location = new System.Drawing.Point(447, 185);
			this.RegionsMenuP.Name = "RegionsMenuP";
			this.RegionsMenuP.Size = new System.Drawing.Size(60, 60);
			this.RegionsMenuP.TabIndex = 11;
			// 
			// RegionsL
			// 
			this.RegionsL.AutoSize = true;
			this.RegionsL.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RegionsL.ForeColor = System.Drawing.Color.LightBlue;
			this.RegionsL.Location = new System.Drawing.Point(33, 158);
			this.RegionsL.Name = "RegionsL";
			this.RegionsL.Size = new System.Drawing.Size(212, 32);
			this.RegionsL.TabIndex = 10;
			this.RegionsL.Text = "Quick-operations";
			// 
			// RegionsInfoL
			// 
			this.RegionsInfoL.AutoSize = true;
			this.RegionsInfoL.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RegionsInfoL.Location = new System.Drawing.Point(34, 102);
			this.RegionsInfoL.Name = "RegionsInfoL";
			this.RegionsInfoL.Size = new System.Drawing.Size(853, 21);
			this.RegionsInfoL.TabIndex = 12;
			this.RegionsInfoL.Text = "Use quick-operations to perform mass checking on the electoral bureaus. Only the " +
    "voting sections and towns that belong to";
			// 
			// RegionsCB
			// 
			this.RegionsCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RegionsCB.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RegionsCB.FormattingEnabled = true;
			this.RegionsCB.Location = new System.Drawing.Point(38, 196);
			this.RegionsCB.Margin = new System.Windows.Forms.Padding(6);
			this.RegionsCB.MaxDropDownItems = 20;
			this.RegionsCB.Name = "RegionsCB";
			this.RegionsCB.Size = new System.Drawing.Size(400, 38);
			this.RegionsCB.TabIndex = 14;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(35, 123);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(816, 21);
			this.label1.TabIndex = 15;
			this.label1.Text = "the checked bureaus, as well as the regions that contain at least one checked bur" +
    "eau, will be displayed in the main list.";
			// 
			// FFilters
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(982, 688);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.RegionsCB);
			this.Controls.Add(this.RegionsInfoL);
			this.Controls.Add(this.RegionsMenuP);
			this.Controls.Add(this.RegionsL);
			this.Controls.Add(this.EBureauP);
			this.Controls.Add(this.EbureausL);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.MenuP);
			this.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FFilters";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select information columns";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.FFilters_Load);
			this.VisibleChanged += new System.EventHandler(this.FFilters_VisibleChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel MenuP;
		private System.Windows.Forms.Label EbureausL;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel EBureauP;
		private System.Windows.Forms.Panel RegionsMenuP;
		private System.Windows.Forms.Label RegionsL;
		private System.Windows.Forms.Label RegionsInfoL;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.ComboBox RegionsCB;
	}
}