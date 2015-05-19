using PresidentialElections2014.Forms;
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
	public partial class FMenu : Form
	{
		private FMain MainForm;
		private FFilters FiltersForm;

		private List<string> MenuButtonCaptions;
		private List<PictureBoxButton> MenuButtons;
		private BorderPictureBox BorderPB;

		public FMenu(FMain mainForm, FFilters filtersForm)
		{
			InitializeComponent();
			this.MainForm = mainForm;
			this.FiltersForm = filtersForm;
			MyGUIs.InitializeAndFormatFormComponents(this);
			//
			MenuButtonCaptions = new List<string>();
			MenuButtons = new List<PictureBoxButton>();
			BorderPB = new BorderPictureBox(BorderPictureBox.DefaultBorderWidth);
			BorderPB.Parent = this;
		}

		private void FMenu_Load(object sender, EventArgs e)
		{
		}

		public void RefreshMenuForm(List<string> captions, EventHandler menuButton_Click_Event, Point location, Size buttonSize)
		{
			MenuButtonCaptions.Clear();
			MenuButtonCaptions.AddRange(captions);
			MenuButtonCaptions.Add("CLOSE");
			//
			this.Location = location;
			this.Size = new Size(buttonSize.Width + 2 * BorderPB.BorderWidth, MenuButtonCaptions.Count * buttonSize.Height + 2 * BorderPB.BorderWidth);
			BorderPB.SetBounds(0, 0, this.Width, this.Height);
			BorderPB.RedrawBorder();
			//
			for (int i = MenuButtonCaptions.Count; i < MenuButtons.Count; i++)
				MenuButtons[i].Hide();
			//
			for (int i = 0; i < MenuButtonCaptions.Count; i++)
			{
				if (i >= MenuButtons.Count)
				{
					PictureBoxButton butt = new PictureBoxButton("null", null);
					butt.Parent = this;
					butt.SetBounds(BorderPB.BorderWidth, BorderPB.BorderWidth + i * buttonSize.Height, buttonSize.Width, buttonSize.Height);
					MenuButtons.Add(butt);
				}
				MenuButtons[i].SetOnClickEventHandler(menuButton_Click_Event);
				MenuButtons[i].Caption = MenuButtonCaptions[i];
				MenuButtons[i].OnMouseLeave(null, null);
				MenuButtons[i].Show();
			}
			BorderPB.SendToBack();
			//
			this.Show();
			this.Focus();
		}

		public int BorderWidth { get { return BorderPB.BorderWidth; } }

		//
		//
		//

		public void MainForm_MoreMenu_Click(object sender, EventArgs e)
		{
			int r = MenuButtonCaptions.IndexOf((sender as PictureBoxButton).Caption);
			this.MainForm.ShowFormAndHideAllOthers(null);
			if (r != MenuButtonCaptions.Count - 1)
				switch (r)
				{
					case 0: // select columns
						this.MainForm.ShowFormAndHideAllOthers(this.MainForm.FiltersForm);
						break;
					case 1: // global settings
						this.MainForm.ShowFormAndHideAllOthers(this.MainForm.SettingsForm);
						break;
					default:
						MessageBox.Show("Invalid menu button caption :\\", "Weird", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						break;
				}
		}

		public void MainForm_SelectCategoriesMenu_Click(object sender, EventArgs e)
		{
			int r = MenuButtonCaptions.IndexOf((sender as PictureBoxButton).Caption);
			this.Hide();
			/*if (r != MenuButtonCaptions.Count - 1)
				if (r == 0) // deselect all
					foreach (CategoryPB categPB in this.MainForm.CategManager.CategoryButtons)
					{
						categPB.Checked = false;
						categPB.OnMouseLeave(null, null);
					}
				else if (r == 1) // select all
					foreach (CategoryPB categPB in this.MainForm.CategManager.CategoryButtons)
					{
						categPB.Checked = true;
						categPB.OnMouseLeave(null, null);
					}
				else // select of rank r-2
					foreach (CategoryPB categPB in this.MainForm.CategManager.CategoryButtons)
					{
						categPB.Checked = categPB.Category.Rank == r - 2;
						categPB.OnMouseLeave(null, null);
					}*/
			this.MainForm.CategoryButton_Click(null, null);
		}

		public void FiltersForm_RegionOperationsMenu_Click(object sender, EventArgs e)
		{
			int r = MenuButtonCaptions.IndexOf((sender as PictureBoxButton).Caption);
			this.Hide();
			switch (r)
			{
				case 0:
				case 1: // check/uncheck all from selected region
					foreach (CheckBoxManager ebMan in this.FiltersForm.EBManagers)
						foreach (CheckBoxPB ebPB in ebMan.CategoryButtons)
							if (ebPB.Visible && this.MainForm.ProjectDB.Regions[this.FiltersForm.RegionsCB.SelectedIndex].GetElectoralBureauByNumber((ebPB as EBCheckBoxPB).ElectoralBureau.Number) != null)
							{
								ebPB.Checked = r == 0;
								ebPB.OnMouseLeave(ebPB, null);
							}
					this.FiltersForm.RefreshElectoralBureausLabel();
					break;
				case 2:
				case 3: // check/uncheck all
					foreach (CheckBoxManager ebMan in this.FiltersForm.EBManagers)
						foreach (CheckBoxPB ebPB in ebMan.CategoryButtons)
							if (ebPB.Visible)
							{
								ebPB.Checked = r == 2;
								ebPB.OnMouseLeave(ebPB, null);
							}
					this.FiltersForm.RefreshElectoralBureausLabel();
					break;
			}
			this.MainForm.CategoryButton_Click(null, null);
		}
	}
}
