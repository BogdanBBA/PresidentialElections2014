using PresidentialElections2014.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresidentialElections2014.VisualComponents
{
	public class CheckBoxManager : Panel
	{
		public List<CheckBoxPB> CategoryButtons;
		private EventHandler OnClickEvt;

		public CheckBoxManager(Panel container, Rectangle bounds, EventHandler ClickEvt)
			: base()
		{
			this.CategoryButtons = new List<CheckBoxPB>();
			this.OnClickEvt += ClickEvt;
			this.Parent = container;
			this.Bounds = bounds;
			this.AutoScroll = true;
			this.VScroll = true;
		}

		public void RecreateCategoryButtons(List<string> categories, AppSettings settings, bool checkFirst)
		{
			for (int i = categories.Count; i < this.CategoryButtons.Count; i++)
				this.CategoryButtons[i].Hide();
			for (int i = 0; i < categories.Count; i++)
			{
				if (i >= this.CategoryButtons.Count)
				{
					CheckBoxPB newButton = new CheckBoxPB(null, false, this.OnClickEvt);
					newButton.Parent = this;
					this.CategoryButtons.Add(newButton);
				}
				CheckBoxPB button = this.CategoryButtons[i];
				button.Caption = categories[i];
				button.Checked = checkFirst && i == 0;
				button.SetBounds(0, i * CheckBoxPB.PBHeight, this.Width - MyGUIs.PanelContentRightPadding, CheckBoxPB.PBHeight);
				button.OnMouseLeave(null, null);
			}
		}

		public void RecreateCategoryButtons(List<ElectoralBureau> bureaus, AppSettings settings, bool checkFirst)
		{
			for (int i = bureaus.Count; i < this.CategoryButtons.Count; i++)
				this.CategoryButtons[i].Hide();
			for (int i = 0; i < bureaus.Count; i++)
			{
				if (i >= this.CategoryButtons.Count)
				{
					EBCheckBoxPB newButton = new EBCheckBoxPB(null, false, this.OnClickEvt);
					newButton.Parent = this;
					this.CategoryButtons.Add(newButton);
				}
				EBCheckBoxPB button = this.CategoryButtons[i] as EBCheckBoxPB;
				button.ElectoralBureau = bureaus[i];
				button.Checked = checkFirst && i == 0;
				button.SetBounds(0, i * CheckBoxPB.PBHeight, this.Width - MyGUIs.PanelContentRightPadding, CheckBoxPB.PBHeight);
				button.OnMouseLeave(null, null);
			}
		}
	}

	public class CheckBoxPB : PictureBox
	{
		public const int PBHeight = 32, CBPadding = 5, CBLineWidth = 3, TickLineWidth = 6;
		protected static readonly FontAndColors Cols = new FontAndColors(MyGUIs.GetFont("Didact Gothic", 13, false), MyGUIs.ButtonC, MyGUIs.FontC);
		protected static readonly FontAndColors ColsMO = new FontAndColors(MyGUIs.GetFont("Didact Gothic", 14, false), MyGUIs.SelectedButtonC, Color.DodgerBlue);
		protected static readonly Brush CBBoxBr = new SolidBrush(Color.DodgerBlue);
		protected static readonly Brush CBLineBr = new SolidBrush(Color.LightBlue);
		protected static readonly Pen TickPen = new Pen(CBLineBr, TickLineWidth);

		public string Caption;
		public bool Checked;

		public CheckBoxPB(string Caption, bool Checked, EventHandler Click)
			: base()
		{
			this.Caption = Caption;
			this.Cursor = Cursors.Hand;
			this.Click += Click;
			this.MouseEnter += OnMouseEnter;
			this.MouseLeave += OnMouseLeave;
			this.Checked = Checked;
		}

		public void OnMouseEnter(object sender, EventArgs e)
		{ DrawCheckBoxPB(true); }

		public void OnMouseLeave(object sender, EventArgs e)
		{ DrawCheckBoxPB(false); }

		protected void DrawCheckBoxPB(bool mouseOver)
		{
			if (this.Image != null)
				this.Image.Dispose();
			Bitmap bmp = new Bitmap(this.Width, this.Height);
			Graphics g = Graphics.FromImage(bmp);
			FontAndColors CF = mouseOver ? ColsMO : Cols;
			g.FillRectangle(CF.BgBrush, 0, 0, this.Width, this.Height);

			if (this is EBCheckBoxPB)
				this.Caption = (this as EBCheckBoxPB).ElectoralBureau == null ? "NULL EB" : (this as EBCheckBoxPB).ElectoralBureau.Name;
			int lastLeft = CBPadding;

			Rectangle chb = new Rectangle(lastLeft, CBPadding, this.Height - 2 * CBPadding, this.Height - 2 * CBPadding);
			g.FillRectangle(CBBoxBr, chb);
			lastLeft += chb.Width + CBPadding;
			chb.Inflate(-CBLineWidth, -CBLineWidth);
			g.FillRectangle(CF.BgBrush, chb);
			if (this.Checked)
			{
				chb.Inflate(CBLineWidth, CBLineWidth);
				g.DrawLines(TickPen, new Point[3] { new Point(chb.Left - 2, chb.Top + chb.Height / 2-1), 
					new Point(chb.Left + chb.Width / 2, chb.Bottom - 2), new Point(chb.Right + chb.Width / 4, chb.Top) });
			}

			Size size = g.MeasureString(this.Caption, CF.Font).ToSize();
			g.DrawString(this.Caption, CF.Font, CF.FontBrush, new Point(lastLeft, this.Height / 2 - size.Height / 2));
			this.Image = bmp;
		}
	}

	public class EBCheckBoxPB : CheckBoxPB
	{
		public ElectoralBureau ElectoralBureau;

		public EBCheckBoxPB(ElectoralBureau electoralBureau, bool Checked, EventHandler Click)
			: base("EBCheckBoxPB CTOR", Checked, Click)
		{
			this.ElectoralBureau = electoralBureau;
		}
	}
}
