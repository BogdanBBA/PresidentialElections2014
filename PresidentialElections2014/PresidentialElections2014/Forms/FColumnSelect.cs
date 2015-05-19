using PresidentialElections2014.Classes;
using PresidentialElections2014.VisualComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresidentialElections2014.Forms
{
	public partial class FColumnSelect : Form
	{
		private Color ColOK = Color.LightGreen, ColError = Color.Red;
		private BorderPictureBox BorderPB;

		private FMain MainForm;

		public FColumnSelect(FMain mainForm)
		{
			this.MainForm = mainForm;
			InitializeComponent();
			MyGUIs.InitializeAndFormatFormComponents(this);
			this.BorderPB = new BorderPictureBox(6);
			this.BorderPB.Parent = this;
			BorderPB.SetBounds(0, 0, this.Width, this.Height);
			BorderPB.RedrawBorder();
		}

		private void FColumnSelect_Load(object sender, EventArgs e)
		{
			MyGUIs.CreateMenuButtons(MenuP, new List<string>(new string[1] { "SAVE AND CLOSE" }), true, SaveAndCloseButton_Click);
		}

		private void ColsLB_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			RightB_Click(sender, e);
		}

		private void ColLB_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			LeftB_Click(sender, e);
		}

		private void FColumnSelect_VisibleChanged(object sender, EventArgs e)
		{
			if (this.Visible)
			{
				ColLB.Items.Clear();
				ColsLB.Items.Clear();
				foreach (Column col in this.MainForm.ProjectDB.ColumnConfig.Columns)
					if (col.Visible)
						ColLB.Items.Add(this.MainForm.ProjectDB.ColumnConfig.EncodeColumn(col));
					else
						ColsLB.Items.Add(this.MainForm.ProjectDB.ColumnConfig.EncodeColumn(col));
				RefreshSelectedColumnInfo();
				CalculateColumnWidths();
			}
		}

		private void LeftB_Click(object sender, EventArgs e)
		{
			if (ColLB.SelectedIndex < 0 || ColLB.SelectedIndex >= ColLB.Items.Count)
				return;
			ColsLB.Items.Add(ColLB.Items[ColLB.SelectedIndex]);
			ColsLB.SelectedIndex = ColsLB.Items.Count - 1;
			ColLB.Items.RemoveAt(ColLB.SelectedIndex);
			CalculateColumnWidths();
		}

		private void RightB_Click(object sender, EventArgs e)
		{
			if (ColsLB.SelectedIndex < 0 || ColsLB.SelectedIndex >= ColsLB.Items.Count)
				return;
			ColLB.Items.Add(ColsLB.Items[ColsLB.SelectedIndex]);
			ColLB.SelectedIndex = ColLB.Items.Count - 1;
			ColsLB.Items.RemoveAt(ColsLB.SelectedIndex);
			CalculateColumnWidths();
		}

		private void UpB_Click(object sender, EventArgs e)
		{
			if (ColLB.SelectedIndex <= 0 || ColLB.SelectedIndex >= ColLB.Items.Count)
				return;
			SwapListBoxPositions(ColLB.SelectedIndex, ColLB.SelectedIndex - 1);
		}

		private void DownB_Click(object sender, EventArgs e)
		{
			if (ColLB.SelectedIndex < 0 || ColLB.SelectedIndex >= ColLB.Items.Count - 1)
				return;
			SwapListBoxPositions(ColLB.SelectedIndex, ColLB.SelectedIndex + 1);
		}

		private void ColLB_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshSelectedColumnInfo();
		}

		private void TrBar_Scroll(object sender, EventArgs e)
		{
			if (ColLB.SelectedIndex == -1)
				return;
			Column col = this.MainForm.ProjectDB.ColumnConfig.DecodeColumn(ColLB.Items[ColLB.SelectedIndex] as string);
			col.WidthPercentage = TrBar.Value;
			ColLB.Items[ColLB.SelectedIndex] = this.MainForm.ProjectDB.ColumnConfig.EncodeColumn(col);
			RefreshSelectedColumnInfo();
			CalculateColumnWidths();
		}

		private void SwapListBoxPositions(int a, int b)
		{
			string auxS = ColLB.Items[a] as string;
			ColLB.Items[a] = ColLB.Items[b];
			ColLB.Items[b] = auxS;
			ColLB.SelectedIndex = b;
			RefreshSelectedColumnInfo();
		}

		private void RefreshSelectedColumnInfo()
		{
			if (ColLB.SelectedIndex == -1)
			{
				NameL.Text = "---";
				CandidateNameL.Text = "---";
				PercentageWPL.Text = "---";
				ColWidthL.Text = "---";
				return;
			}
			Column col = this.MainForm.ProjectDB.ColumnConfig.DecodeColumn(ColLB.Items[ColLB.SelectedIndex] as string);
			if (col == null)
				return;
			NameL.Text = col.ColumnInfoText();
			CandidateNameL.Text = col.ColumnType == Column.CANDIDATE_VOTES ? col.Candidate.Name : "---";
			PercentageWPL.Text = col.PercentageWherePossible.ToString();
			ColWidthL.Text = col.WidthPercentage + "%";
			TrBar.Value = col.WidthPercentage;
		}

		private void CalculateColumnWidths()
		{
			int totalWidthPerc = 0;
			foreach (object colStr in ColLB.Items)
			{
				Column col = this.MainForm.ProjectDB.ColumnConfig.DecodeColumn(colStr as string);
				totalWidthPerc += col.WidthPercentage;
			}
			ColsWidthL.Text = totalWidthPerc + "%";
			ColsWidthL.ForeColor = totalWidthPerc > 100 ? ColError : ColOK;
		}

		private void SaveAndCloseButton_Click(object sender, EventArgs e)
		{
			if (ColsWidthL.ForeColor.Equals(ColError))
			{
				if (MessageBox.Show("The selected columns can not fit on your screen! You should either shrink them or hide a part of them.\n\nWould you like to save and close anyway?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
					return;
			}

			List<Column> cols = new List<Column>();
			foreach (object colStr in ColLB.Items)
			{
				Column col = this.MainForm.ProjectDB.ColumnConfig.DecodeColumn(colStr as string);
				col.Visible = true;
				cols.Add(col);
			}
			foreach (object colStr in ColsLB.Items)
			{
				Column col = this.MainForm.ProjectDB.ColumnConfig.DecodeColumn(colStr as string);
				col.Visible = false;
				cols.Add(col);
			}
			this.MainForm.ProjectDB.ColumnConfig.Columns.Clear();
			this.MainForm.ProjectDB.ColumnConfig.Columns.AddRange(cols);
			this.MainForm.ProjectDB.ColumnConfig.SaveToFile();

			this.MainForm.ShowFormAndHideAllOthers(null);
			this.MainForm.RefreshFilterStats();
			//this.MainForm.InfoManager.RecreateHeaderAndInformationBoxes(this.MainForm.Settings, this.MainForm.InfoManager.LastItems);
		}
	}
}
