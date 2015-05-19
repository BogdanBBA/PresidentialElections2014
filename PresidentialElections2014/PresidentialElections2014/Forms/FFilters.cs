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
	public partial class FFilters : Form
	{
		public Color ColOK = Color.LightGreen, ColError = Color.Red;
		private const int nRegColumns = 6, nEBColumns = 5;
		private List<string> RegionOperationCaptions = new List<string>(new string[4] { "Check region", "Uncheck region","Check all", "Uncheck all" });
		private BorderPictureBox BorderPB;

		private FMain MainForm;

		public List<CheckBoxManager> EBManagers;

		public FFilters(FMain mainForm)
		{
			this.MainForm = mainForm;
			this.EBManagers = new List<CheckBoxManager>();
			InitializeComponent();
			MyGUIs.InitializeAndFormatFormComponents(this);
			this.BorderPB = new BorderPictureBox(6);
			this.BorderPB.Parent = this;
			BorderPB.SetBounds(0, 0, this.Width, this.Height);
			BorderPB.RedrawBorder();
		}

		private void FFilters_Load(object sender, EventArgs e)
		{
			// create visual components
			MyGUIs.CreateMenuButtons(MenuP, new List<string>(new string[1] { "SAVE AND CLOSE" }), true, SaveAndCloseButton_Click);
			MyGUIs.CreateMenuButtons(RegionsMenuP, new List<string>(new string[1] { "+" }), true, RegionsMenuButton_Click);
			// add regions
			RegionsCB.Items.Clear();
			foreach (ElectoralBureauList ebList in this.MainForm.ProjectDB.Regions)
				RegionsCB.Items.Add(ebList.Name);
			// create electoral bureaus checkboxes
			for (int i = 0, manWidth = EBureauP.Width / nEBColumns, lastLeft = 0; i < nEBColumns; i++, lastLeft += manWidth)
				this.EBManagers.Add(new CheckBoxManager(EBureauP, new Rectangle(lastLeft, 0, manWidth, EBureauP.Height), ElectoralBureauButton_Click));// regions
			int nRows = this.MainForm.ProjectDB.WorldwideResults.ElectoralBureaus.Count % nEBColumns == 0 ? this.MainForm.ProjectDB.WorldwideResults.ElectoralBureaus.Count / nEBColumns : this.MainForm.ProjectDB.WorldwideResults.ElectoralBureaus.Count / nEBColumns + 1;
			for (int iEBM = 0; iEBM < this.EBManagers.Count; iEBM++)
			{
				List<ElectoralBureau> ebs = new List<ElectoralBureau>();
				for (int iEB = iEBM * nRows; iEB < iEBM * nRows + nRows; iEB++)
					if (iEB < this.MainForm.ProjectDB.WorldwideResults.ElectoralBureaus.Count)
						ebs.Add(this.MainForm.ProjectDB.WorldwideResults.ElectoralBureaus[iEB]);
				this.EBManagers[iEBM].RecreateCategoryButtons(ebs, this.MainForm.Settings, false);
			}
		}

		/*private List<CheckBoxPB> GetElectoralBureauPBsByRegionName(string regionName)
		{
			ElectoralBureauList region = this.MainForm.ProjectDB.GetRegionByName(regionName);
			List<CheckBoxPB> result = new List<CheckBoxPB>();
			foreach (CheckBoxManager ebMan in this.EBManagers)
				foreach (CheckBoxPB ebPB in ebMan.CategoryButtons)
					if (ebPB.Visible)
					{
						int ebNumber = Int32.Parse(ebPB.Caption.Substring(1, ebPB.Caption.IndexOf(']') - 1));
						ElectoralBureau eb = region.GetElectoralBureauByNumber(ebNumber);
						if (region.GetElectoralBureauByNumber(ebNumber) != null)
							result.Add(ebPB);
					}
			return result;
		}*/

		private void FFilters_VisibleChanged(object sender, EventArgs e)
		{
			if (this.Visible)
			{
				RegionsCB.SelectedIndex = 0;
				// refresh electoral bureaus by project settings
				foreach (CheckBoxManager ebMan in this.EBManagers)
					foreach (CheckBoxPB ebPB in ebMan.CategoryButtons)
						if (ebPB.Visible)
						{
							ebPB.Checked = this.MainForm.Settings.FilteredEBs.GetElectoralBureauByNumber((ebPB as EBCheckBoxPB).ElectoralBureau.Number) != null;
							ebPB.OnMouseLeave(ebPB, null);
						}
				RefreshElectoralBureausLabel();
			}
		}

		private void RegionsMenuButton_Click(object sender, EventArgs e)
		{
			Point menuLocation = new Point(this.Left + RegionsMenuP.Left, this.Top + RegionsMenuP.Bottom);
			this.MainForm.MenuForm.RefreshMenuForm(RegionOperationCaptions, this.MainForm.MenuForm.FiltersForm_RegionOperationsMenu_Click, menuLocation, new Size(300, 60));
		}

		private void ElectoralBureauButton_Click(object sender, EventArgs e)
		{
			if (sender != null)
			{
				CheckBoxPB ebB = sender as CheckBoxPB;
				ebB.Checked = !ebB.Checked;
				ebB.OnMouseEnter(null, null);
				RefreshElectoralBureausLabel();
			}
		}

		public void RefreshElectoralBureausLabel()
		{
			int nChecked = 0;
			foreach (CheckBoxManager ebMan in this.EBManagers)
				foreach (CheckBoxPB ebPB in ebMan.CategoryButtons)
					if (ebPB.Visible && ebPB.Checked)
						nChecked++;
			EbureausL.Text = "Electoral bureaus (" + nChecked + "/" + this.MainForm.ProjectDB.WorldwideResults.ElectoralBureaus.Count + ")";
		}

		private void SaveAndCloseButton_Click(object sender, EventArgs e)
		{
			ElectoralBureauList filteredEBs = this.MainForm.Settings.FilteredEBs;
			filteredEBs.ElectoralBureaus.Clear();
			foreach (CheckBoxManager ebMan in this.EBManagers)
				foreach (CheckBoxPB ebPB in ebMan.CategoryButtons)
					if (ebPB.Visible && ebPB.Checked)
						filteredEBs.ElectoralBureaus.Add((ebPB as EBCheckBoxPB).ElectoralBureau);
			this.MainForm.ShowFormAndHideAllOthers(null);
		}
	}
}
