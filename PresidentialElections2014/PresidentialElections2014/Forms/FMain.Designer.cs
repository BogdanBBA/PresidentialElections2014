namespace PresidentialElections2014.Forms
{
	partial class FMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
			this.LeftP = new System.Windows.Forms.Panel();
			this.RefreshP = new System.Windows.Forms.Panel();
			this.FilterInfoL = new System.Windows.Forms.Label();
			this.FilterInfoP = new System.Windows.Forms.Panel();
			this.EBInfoL1 = new System.Windows.Forms.Label();
			this.FilterMenuP = new System.Windows.Forms.Panel();
			this.SortByL = new System.Windows.Forms.Label();
			this.SortP = new System.Windows.Forms.Panel();
			this.SortOrderCB = new System.Windows.Forms.ComboBox();
			this.SortByCB = new System.Windows.Forms.ComboBox();
			this.CategoryP = new System.Windows.Forms.Panel();
			this.MenuP = new System.Windows.Forms.Panel();
			this.CategTitleL = new System.Windows.Forms.Label();
			this.MainP = new System.Windows.Forms.Panel();
			this.TotalsP = new System.Windows.Forms.Panel();
			this.HeaderP = new System.Windows.Forms.Panel();
			this.EBInfoL2 = new System.Windows.Forms.Label();
			this.EBInfoL3 = new System.Windows.Forms.Label();
			this.LeftP.SuspendLayout();
			this.FilterInfoP.SuspendLayout();
			this.SortP.SuspendLayout();
			this.MainP.SuspendLayout();
			this.SuspendLayout();
			// 
			// LeftP
			// 
			this.LeftP.Controls.Add(this.RefreshP);
			this.LeftP.Controls.Add(this.FilterInfoL);
			this.LeftP.Controls.Add(this.FilterInfoP);
			this.LeftP.Controls.Add(this.FilterMenuP);
			this.LeftP.Controls.Add(this.SortByL);
			this.LeftP.Controls.Add(this.SortP);
			this.LeftP.Controls.Add(this.CategoryP);
			this.LeftP.Controls.Add(this.MenuP);
			this.LeftP.Controls.Add(this.CategTitleL);
			this.LeftP.Location = new System.Drawing.Point(33, 12);
			this.LeftP.Name = "LeftP";
			this.LeftP.Size = new System.Drawing.Size(289, 540);
			this.LeftP.TabIndex = 1;
			// 
			// RefreshP
			// 
			this.RefreshP.Location = new System.Drawing.Point(18, 419);
			this.RefreshP.Name = "RefreshP";
			this.RefreshP.Size = new System.Drawing.Size(200, 42);
			this.RefreshP.TabIndex = 8;
			// 
			// FilterInfoL
			// 
			this.FilterInfoL.AutoSize = true;
			this.FilterInfoL.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FilterInfoL.Location = new System.Drawing.Point(37, 132);
			this.FilterInfoL.Name = "FilterInfoL";
			this.FilterInfoL.Size = new System.Drawing.Size(80, 32);
			this.FilterInfoL.TabIndex = 7;
			this.FilterInfoL.Text = "Filters";
			// 
			// FilterInfoP
			// 
			this.FilterInfoP.Controls.Add(this.EBInfoL3);
			this.FilterInfoP.Controls.Add(this.EBInfoL2);
			this.FilterInfoP.Controls.Add(this.EBInfoL1);
			this.FilterInfoP.Location = new System.Drawing.Point(18, 160);
			this.FilterInfoP.Name = "FilterInfoP";
			this.FilterInfoP.Size = new System.Drawing.Size(200, 60);
			this.FilterInfoP.TabIndex = 6;
			// 
			// EBInfoL1
			// 
			this.EBInfoL1.AutoSize = true;
			this.EBInfoL1.Font = new System.Drawing.Font("Segoe UI Semilight", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.EBInfoL1.Location = new System.Drawing.Point(0, 0);
			this.EBInfoL1.Name = "EBInfoL1";
			this.EBInfoL1.Size = new System.Drawing.Size(54, 19);
			this.EBInfoL1.TabIndex = 8;
			this.EBInfoL1.Text = "EBInfoL";
			// 
			// FilterMenuP
			// 
			this.FilterMenuP.Location = new System.Drawing.Point(18, 223);
			this.FilterMenuP.Name = "FilterMenuP";
			this.FilterMenuP.Size = new System.Drawing.Size(200, 37);
			this.FilterMenuP.TabIndex = 5;
			// 
			// SortByL
			// 
			this.SortByL.AutoSize = true;
			this.SortByL.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SortByL.Location = new System.Drawing.Point(12, 269);
			this.SortByL.Name = "SortByL";
			this.SortByL.Size = new System.Drawing.Size(94, 32);
			this.SortByL.TabIndex = 4;
			this.SortByL.Text = "Sort by";
			// 
			// SortP
			// 
			this.SortP.Controls.Add(this.SortOrderCB);
			this.SortP.Controls.Add(this.SortByCB);
			this.SortP.Location = new System.Drawing.Point(28, 304);
			this.SortP.Name = "SortP";
			this.SortP.Size = new System.Drawing.Size(200, 100);
			this.SortP.TabIndex = 3;
			// 
			// SortOrderCB
			// 
			this.SortOrderCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SortOrderCB.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SortOrderCB.FormattingEnabled = true;
			this.SortOrderCB.Items.AddRange(new object[] {
            "Ascending",
            "Descending"});
			this.SortOrderCB.Location = new System.Drawing.Point(0, 37);
			this.SortOrderCB.Name = "SortOrderCB";
			this.SortOrderCB.Size = new System.Drawing.Size(121, 31);
			this.SortOrderCB.TabIndex = 1;
			// 
			// SortByCB
			// 
			this.SortByCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SortByCB.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SortByCB.FormattingEnabled = true;
			this.SortByCB.Location = new System.Drawing.Point(0, 0);
			this.SortByCB.Name = "SortByCB";
			this.SortByCB.Size = new System.Drawing.Size(121, 31);
			this.SortByCB.Sorted = true;
			this.SortByCB.TabIndex = 0;
			// 
			// CategoryP
			// 
			this.CategoryP.Location = new System.Drawing.Point(18, 54);
			this.CategoryP.Name = "CategoryP";
			this.CategoryP.Size = new System.Drawing.Size(200, 100);
			this.CategoryP.TabIndex = 2;
			// 
			// MenuP
			// 
			this.MenuP.Location = new System.Drawing.Point(33, 490);
			this.MenuP.Name = "MenuP";
			this.MenuP.Size = new System.Drawing.Size(200, 42);
			this.MenuP.TabIndex = 1;
			// 
			// CategTitleL
			// 
			this.CategTitleL.AutoSize = true;
			this.CategTitleL.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CategTitleL.Location = new System.Drawing.Point(0, 0);
			this.CategTitleL.Name = "CategTitleL";
			this.CategTitleL.Size = new System.Drawing.Size(131, 32);
			this.CategTitleL.TabIndex = 0;
			this.CategTitleL.Text = "Categories";
			// 
			// MainP
			// 
			this.MainP.Controls.Add(this.TotalsP);
			this.MainP.Controls.Add(this.HeaderP);
			this.MainP.Location = new System.Drawing.Point(448, 86);
			this.MainP.Name = "MainP";
			this.MainP.Size = new System.Drawing.Size(277, 227);
			this.MainP.TabIndex = 2;
			// 
			// TotalsP
			// 
			this.TotalsP.Location = new System.Drawing.Point(48, 135);
			this.TotalsP.Name = "TotalsP";
			this.TotalsP.Size = new System.Drawing.Size(176, 45);
			this.TotalsP.TabIndex = 4;
			// 
			// HeaderP
			// 
			this.HeaderP.Location = new System.Drawing.Point(48, 45);
			this.HeaderP.Name = "HeaderP";
			this.HeaderP.Size = new System.Drawing.Size(176, 45);
			this.HeaderP.TabIndex = 3;
			// 
			// EBInfoL2
			// 
			this.EBInfoL2.AutoSize = true;
			this.EBInfoL2.Font = new System.Drawing.Font("Segoe UI Semilight", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.EBInfoL2.Location = new System.Drawing.Point(0, 19);
			this.EBInfoL2.Name = "EBInfoL2";
			this.EBInfoL2.Size = new System.Drawing.Size(42, 19);
			this.EBInfoL2.TabIndex = 9;
			this.EBInfoL2.Text = "label1";
			// 
			// EBInfoL3
			// 
			this.EBInfoL3.AutoSize = true;
			this.EBInfoL3.Font = new System.Drawing.Font("Segoe UI Semilight", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.EBInfoL3.Location = new System.Drawing.Point(0, 38);
			this.EBInfoL3.Name = "EBInfoL3";
			this.EBInfoL3.Size = new System.Drawing.Size(44, 19);
			this.EBInfoL3.TabIndex = 10;
			this.EBInfoL3.Text = "label2";
			// 
			// FMain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(852, 556);
			this.Controls.Add(this.MainP);
			this.Controls.Add(this.LeftP);
			this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FMain";
			this.Text = "Geographical statistics / MAIN";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FMain_Load);
			this.VisibleChanged += new System.EventHandler(this.FMain_VisibleChanged);
			this.Resize += new System.EventHandler(this.FMain_Resize);
			this.LeftP.ResumeLayout(false);
			this.LeftP.PerformLayout();
			this.FilterInfoP.ResumeLayout(false);
			this.FilterInfoP.PerformLayout();
			this.SortP.ResumeLayout(false);
			this.MainP.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel LeftP;
		private System.Windows.Forms.Panel MainP;
		private System.Windows.Forms.Label CategTitleL;
		private System.Windows.Forms.Panel TotalsP;
		private System.Windows.Forms.Panel HeaderP;
		private System.Windows.Forms.Panel MenuP;
		private System.Windows.Forms.Panel CategoryP;
		private System.Windows.Forms.Label SortByL;
		private System.Windows.Forms.Panel SortP;
		private System.Windows.Forms.ComboBox SortOrderCB;
		private System.Windows.Forms.ComboBox SortByCB;
		private System.Windows.Forms.Panel RefreshP;
		private System.Windows.Forms.Label FilterInfoL;
		private System.Windows.Forms.Panel FilterInfoP;
		private System.Windows.Forms.Panel FilterMenuP;
		private System.Windows.Forms.Label EBInfoL1;
		private System.Windows.Forms.Label EBInfoL3;
		private System.Windows.Forms.Label EBInfoL2;
	}
}