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
	public partial class FSettings : Form
	{
		private FMain MainForm;
		private BorderPictureBox BorderPB;

		public FSettings(FMain mainForm)
		{
			this.MainForm = mainForm;
			InitializeComponent();
			MyGUIs.InitializeAndFormatFormComponents(this);
			this.BorderPB = new BorderPictureBox(6);
			this.BorderPB.Parent = this;
			BorderPB.SetBounds(0, 0, this.Width, this.Height);
			BorderPB.RedrawBorder();
		}

		private void FSettings_Load(object sender, EventArgs e)
		{
			MyGUIs.CreateMenuButtons(MenuP, new List<string>(new string[1] { "SAVE AND CLOSE" }), true, SaveAndCloseButton_Click);
		}

		private void SaveAndCloseButton_Click(object sender, EventArgs e)
		{
			this.MainForm.ShowFormAndHideAllOthers(null);
		}
	}
}
