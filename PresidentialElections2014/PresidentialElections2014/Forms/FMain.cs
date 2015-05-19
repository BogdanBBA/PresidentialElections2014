using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PresidentialElections2014.Classes;
using PresidentialElections2014.VisualComponents;
using OSF3.Forms;

namespace PresidentialElections2014.Forms
{
	public partial class FMain : Form
	{
		private const int ComponentPadding = 12, HeaderPanelHeight = 50;
		private List<string> DefaultSortingOptions = new List<string>(new string[2] { "Name", "..." });
		private List<string> CategoryCaptions = new List<string>(new string[8] { "Worldwide", "Regions", "Electoral bureaus", "Municipalities", "Towns", "Villages", "Other countries", "Voting sections" });
		private List<string> FilterButtonCaptions = new List<string>(new string[1] { "Select bureaus" });
		private List<string> RefreshButtonCaptions = new List<string>(new string[1] { "Refresh list" });
		private List<string> MenuButtonCaptions = new List<string>(new string[1] { "CLOSE" });
		private List<PictureBoxButton> FilterButtons, RefreshButtons, MenuButtons;

		private FProjectSelect ProjectSelectForm;
		public FMenu MenuForm;
		public FFilters FiltersForm;
		public FColumnSelect ColumnSelectForm;
		public FSettings SettingsForm;

		public CheckBoxManager CategManager;
		public InformationManager InfoManager;

		public ElectionsDatabase ProjectDB;
		public AppSettings Settings;

		public FMain(FProjectSelect menuForm, AppSettings settings)
		{
			InitializeComponent();
			this.ProjectSelectForm = menuForm;
			this.FiltersForm = new FFilters(this);
			this.ColumnSelectForm = new FColumnSelect(this);
			this.SettingsForm = new FSettings(this);
			this.Settings = settings;
			this.MenuForm = new FMenu(this, this.FiltersForm);
			MyGUIs.InitializeAndFormatFormComponents(this);
		}

		private void FMain_Resize(object sender, EventArgs e)
		{
			LeftP.SetBounds(ComponentPadding, ComponentPadding, this.Width / 6, this.Height - 2 * ComponentPadding);
			MenuP.SetBounds(0, LeftP.Height - MenuButtonCaptions.Count * 60, LeftP.Width, MenuButtonCaptions.Count * 60);
			RefreshP.SetBounds(MenuP.Left, MenuP.Top - 60, MenuP.Width, 60);

			CategTitleL.Location = Point.Empty;
			CategoryP.SetBounds(CategTitleL.Left, CategTitleL.Bottom + ComponentPadding, LeftP.Width, CategoryCaptions.Count * CheckBoxPB.PBHeight);
			FilterInfoL.Location = new Point(0, CategoryP.Bottom + ComponentPadding);
			FilterInfoP.SetBounds(FilterInfoL.Left, FilterInfoL.Bottom + ComponentPadding, LeftP.Width, 60);
			FilterMenuP.SetBounds(FilterInfoL.Left, FilterInfoP.Bottom + ComponentPadding, LeftP.Width, 60);
			SortByL.Location = new Point(0, FilterMenuP.Bottom + ComponentPadding);
			SortP.SetBounds(SortByL.Left, SortByL.Bottom + ComponentPadding, LeftP.Width, SortOrderCB.Bottom);
			SortByCB.Width = SortP.Width;
			SortOrderCB.Width = SortByCB.Width;

			MainP.SetBounds(LeftP.Right + ComponentPadding, LeftP.Top, this.Width - LeftP.Width - 3 * ComponentPadding, LeftP.Height);
			HeaderP.SetBounds(0, 0, MainP.Width, HeaderPanelHeight);
			TotalsP.SetBounds(0, MainP.Height - HeaderPanelHeight, MainP.Width, HeaderPanelHeight);
		}

		private void FMain_Load(object sender, EventArgs e)
		{
			FilterButtons = MyGUIs.CreateMenuButtons(FilterMenuP, FilterButtonCaptions, false, FilterButton_Click);
			RefreshButtons = MyGUIs.CreateMenuButtons(RefreshP, RefreshButtonCaptions, false, RefreshButton_Click);
			MenuButtons = MyGUIs.CreateMenuButtons(MenuP, MenuButtonCaptions, false, MenuButton_Click);
			CategManager = new CheckBoxManager(CategoryP, new Rectangle(0, 0, CategoryP.Width, CategoryP.Height), CategoryButton_Click);
			CategManager.RecreateCategoryButtons(CategoryCaptions, this.Settings, true);
			SortByCB.Items.Clear();
			SortByCB.Items.AddRange(DefaultSortingOptions.ToArray());
			SortByCB.SelectedIndex = 0;
			SortOrderCB.SelectedIndex = 0;
			Rectangle bounds = new Rectangle(0, HeaderP.Bottom, MainP.Width, MainP.Height - HeaderP.Height - TotalsP.Height);
			InfoManager = new InformationManager(MainP, bounds, HeaderP, TotalsP, this.ProjectDB, HeaderPB_Click, InfoPB_Click);
			SortByCB.Items.Clear();
			foreach (Column column in this.ProjectDB.ColumnConfig.Columns)
				if (column.ColumnType != Column.POS)
					SortByCB.Items.Add(column.ColumnInfoText());
			SortByCB.SelectedIndex = 0;
		}

		private void FMain_VisibleChanged(object sender, EventArgs e)
		{
			if (this.Visible)
			{
				ResetSettings();
				RefreshFilterStats();
			}
		}

		public void RefreshFilterStats()
		{
			this.EBInfoL1.Text = string.Format("{0}/{1} {2} selected",
				this.Settings.FilteredEBs.ElectoralBureaus.Count, this.ProjectDB.WorldwideResults.ElectoralBureaus.Count,
				AllNames.Bureau.GetAppropriateForm(this.ProjectDB.WorldwideResults.ElectoralBureaus.Count));
		}

		public void ResetSettings()
		{
			this.Settings.FilteredEBs.ElectoralBureaus.Clear();
			foreach (ElectoralBureau eb in this.ProjectDB.WorldwideResults.ElectoralBureaus)
				this.Settings.FilteredEBs.ElectoralBureaus.Add(eb);
		}

		public void ShowFormAndHideAllOthers(Form form)
		{
			if (form != this.MenuForm)
				this.MenuForm.Hide();
			if (form != this.FiltersForm)
				this.FiltersForm.Hide();
			if (form != this.ColumnSelectForm)
				this.ColumnSelectForm.Hide();
			if (form != this.SettingsForm)
				this.SettingsForm.Hide();
			if (form != null)
			{
				form.Show();
				form.Focus();
			}
		}

		public void CategoryButton_Click(object sender, EventArgs e)
		{
			if (sender != null)
			{
				CheckBoxPB catButt = sender as CheckBoxPB;
				catButt.Checked = !catButt.Checked;
				catButt.OnMouseEnter(null, null);
			}
		}

		public void FilterButton_Click(object sender, EventArgs e)
		{
			this.ShowFormAndHideAllOthers(this.FiltersForm);
		}

		public void RefreshButton_Click(object sender, EventArgs e)
		{
			// get named items
			List<NamedItem> items = new List<NamedItem>();
			if (this.CategManager.CategoryButtons[0].Checked) // worldwide
				items.Add(this.ProjectDB.WorldwideResults);
			if (this.CategManager.CategoryButtons[1].Checked) // regions
				foreach (ElectoralBureauList region in this.ProjectDB.Regions)
				{
					bool toAdd = false;
					foreach (ElectoralBureau eb in this.Settings.FilteredEBs.ElectoralBureaus)
						if (region.GetElectoralBureauByNumber(eb.Number) != null)
						{ toAdd = true; break; }
					if (toAdd)
						items.Add(region);
				}
			foreach (ElectoralBureau eb in this.ProjectDB.WorldwideResults.ElectoralBureaus)
				if (this.Settings.FilteredEBs.GetElectoralBureauByNumber(eb.Number) != null)
				{
					if (this.CategManager.CategoryButtons[2].Checked) // electoral bureaus
						items.Add(eb);
					foreach (TownVillage town in eb.TownsVillages)
					{
						if (this.CategManager.CategoryButtons[3].Checked && town.TownRank == TownVillage.MUNICIPALITY) // municip
							items.Add(town);
						else if (this.CategManager.CategoryButtons[4].Checked && town.TownRank == TownVillage.TOWN) // towns
							items.Add(town);
						else if (this.CategManager.CategoryButtons[5].Checked && town.TownRank == TownVillage.VILLAGE) // villages
							items.Add(town);
						else if (this.CategManager.CategoryButtons[6].Checked && town.TownRank == TownVillage.OTHER_COUNTRY) // countries
							items.Add(town);
						if (this.CategManager.CategoryButtons[7].Checked) // voting sections
							foreach (VotingSection vs in town.VotingSections)
								items.Add(vs);
					}
				}

			// sort items
			Column sortCol = this.ProjectDB.ColumnConfig.GetColumnByInformationText(SortByCB.Items[SortByCB.SelectedIndex] as string);
			bool descending = SortOrderCB.SelectedIndex == 1;

			for (int i = 0; i < items.Count - 1; i++)
				for (int j = i + 1; j < items.Count; j++)
				{
					NamedItem iNI = items[i], jNI = items[j];
					bool mustSwitch = false;
					switch (sortCol.ColumnType)
					{
						case Column.POS:
						case Column.REG_POS:
						case Column.NAME:
							mustSwitch = iNI.Name.CompareTo(jNI.Name) > 0;
							break;
						case Column.PARENT:
							if (iNI.Parent != null && jNI.Parent != null)
							{
								if (iNI.Parent.ItemRank() != jNI.Parent.ItemRank())
									mustSwitch = iNI.Parent.ItemRank() < jNI.Parent.ItemRank();
								else
								{
									int compRes = iNI.Parent.Name.CompareTo(jNI.Parent.Name);
									mustSwitch = compRes != 0 ? compRes < 0 : iNI.Name.CompareTo(jNI.Name) < 0;
								}
							}
							else
								mustSwitch = jNI.Parent != null;
							break;
						case Column.TOTAL_PERMANENT_VOTERS:
						case Column.VALID_VOTES:
							mustSwitch = sortCol.GetInteger(iNI) > sortCol.GetInteger(jNI);
							break;
						case Column.PRESENCE:
							mustSwitch = sortCol.GetDouble(iNI) > sortCol.GetDouble(jNI);
							break;
						case Column.PERMANENT_VOTERS:
						case Column.TOURIST_VOTERS:
						case Column.SPECIAL_VOTERS:
						case Column.CANDIDATE_VOTES:
							double iVal = sortCol.PercentageWherePossible ? sortCol.GetDouble(iNI) : sortCol.GetInteger(iNI);
							double jVal = sortCol.PercentageWherePossible ? sortCol.GetDouble(jNI) : sortCol.GetInteger(jNI);
							mustSwitch = iVal > jVal;
							break;
					}
					if (mustSwitch ^ descending)
					{
						NamedItem auxNI = items[i];
						items[i] = items[j];
						items[j] = auxNI;
					}
				}

			// refresh
			InfoManager.RecreateHeaderAndInformationBoxes(this.Settings, items);
		}

		private void HeaderPB_Click(object sender, EventArgs e)
		{
			this.ShowFormAndHideAllOthers(this.ColumnSelectForm);
		}

		private void InfoPB_Click(object sender, EventArgs e)
		{
			MessageBox.Show("InfoPB_Click");
		}

		private void MenuButton_Click(object sender, EventArgs e)
		{
			PictureBoxButton butt = sender as PictureBoxButton;
			switch (MenuButtonCaptions.IndexOf(butt.Caption))
			{
				case 0: // exit
					this.ShowFormAndHideAllOthers(null);
					this.Hide();
					this.ProjectSelectForm.Show();
					this.ProjectSelectForm.Focus();
					break;
			}
		}
	}
}
