using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PresidentialElections2014.Classes;
using PresidentialElections2014.Forms;
using PresidentialElections2014.VisualComponents;

namespace PresidentialElections2014
{
	public partial class FProjectSelect : Form
	{
		private FMain MainForm;
		private FAbout AboutForm;
		private BorderPictureBox BorderPB;

		private List<string> MenuButtonCaptions = new List<string>(new string[3] { "Open project", "About", "EXIT" });

		private List<PictureBoxButton> MenuButtons;

		public FProjectSelect()
		{
			InitializeComponent();
			MyGUIs.InitializeAndFormatFormComponents(this);
			this.BorderPB = new BorderPictureBox(6);
			this.BorderPB.Parent = this;
			BorderPB.SetBounds(0, 0, this.Width, this.Height);
			BorderPB.RedrawBorder();
		}

		private void FMenu_Load(object sender, EventArgs e)
		{
			try
			{
				// Checks
				if (!Directory.Exists(Paths.ProjectsFolder))
					Directory.CreateDirectory(Paths.ProjectsFolder);
				if (!Directory.Exists(Paths.UtilsFolder))
					Directory.CreateDirectory(Paths.UtilsFolder);
				if (!File.Exists(Paths.SettingsFile))
					throw new ApplicationException("File \"" + Paths.SettingsFile + "\" does not exist!");
				if (!File.Exists(Paths.LogoFile))
					throw new ApplicationException("File \"" + Paths.LogoFile + "\" does not exist!");

				// UI init
				MenuButtons = MyGUIs.CreateMenuButtons(MenuP, MenuButtonCaptions, true, MenuButton_Click);
				string[] projects = Directory.GetFiles(Paths.ProjectsFolder, "*.xml", SearchOption.TopDirectoryOnly);
				foreach (string project in projects)
					ProjectsCB.Items.Add(Path.GetFileName(project));
				ProjectsCB.SelectedIndex = ProjectsCB.Items.Count > 0 ? 0 : -1;
				AppSettings Settings = new AppSettings();
				Settings.ReadFromFile();
				this.MainForm = new FMain(this, Settings);
				this.AboutForm = new FAbout(this);
			}
			catch (ApplicationException AE)
			{
				MessageBox.Show("A preventable ERROR occured while initializing the program. See below for details.\n\n" + AE.ToString(), "Application exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			catch (Exception E)
			{
				MessageBox.Show("An unexpected ERROR occured while initializing the program. See below for details.\n\n" + E.ToString(), "Application exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		private void MenuButton_Click(object sender, EventArgs e)
		{
			switch (MenuButtonCaptions.IndexOf((sender as PictureBoxButton).Caption))
			{
				case 0: // open
					if (ProjectsCB.SelectedIndex == -1)
					{
						MessageBox.Show("Select a database first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					string openResult;
					ElectionsDatabase tempDB = new ElectionsDatabase(Paths.ProjectsFolder + "\\" + ProjectsCB.Items[ProjectsCB.SelectedIndex], out openResult);
					if (!openResult.Equals(""))
					{
						MessageBox.Show(openResult, "Failed to open database", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					this.MainForm.ProjectDB = tempDB;
					this.Hide();
					this.MainForm.Show();
					this.MainForm.Focus();
					break;
				case 1: // more
					this.Hide();
					this.AboutForm.Show();
					this.AboutForm.Focus();
					break;
				case 2: // exit
					Application.Exit();
					break;
			}
		}
	}
}
