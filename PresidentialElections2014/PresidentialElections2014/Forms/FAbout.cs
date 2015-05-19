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
	public partial class FAbout : Form
	{
		private FProjectSelect SelectProjectForm;
		private BorderPictureBox BorderPB;

		public FAbout(FProjectSelect selectProjectForm)
		{
			this.SelectProjectForm = selectProjectForm;
			InitializeComponent();
			MyGUIs.InitializeAndFormatFormComponents(this);
			this.BorderPB = new BorderPictureBox(6);
			this.BorderPB.Parent = this;
			BorderPB.SetBounds(0, 0, this.Width, this.Height);
			BorderPB.RedrawBorder();
		}

		private void FAbout_Load(object sender, EventArgs e)
		{
			MyGUIs.CreateMenuButtons(MenuP, new List<string>(new string[1] { "CLOSE" }), true, ExitButton_Click);
			LogoPB.Load(Paths.LogoFile);
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			this.Hide();
			this.SelectProjectForm.Show();
			this.SelectProjectForm.Focus();
		}
	}
}
