using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresidentialElections2014.VisualComponents
{
	public static class MyGUIs
	{
		public const int PanelContentRightPadding = 20;

		public static Color FormBackgroundC = ColorTranslator.FromHtml("#100E10");
		public static Color ButtonC = ColorTranslator.FromHtml("#100E10");
		public static Color SelectedButtonC = ColorTranslator.FromHtml("#181618");
		public static Color FontC = ColorTranslator.FromHtml("#FFFFFF");
		public static Color SelectedFontC = ColorTranslator.FromHtml("#098CE3");

		public static Font GetFont(string name, int size, bool bold)
		{
			return GetFont(name, size, bold, false);
		}

		public static Font GetFont(string name, int size, bool bold, bool italic)
		{
			FontStyle fontStyle = bold ? FontStyle.Bold : FontStyle.Regular;
			if (italic)
				fontStyle = fontStyle | FontStyle.Italic;
			return new Font(name, size, fontStyle);
		}

		public static void InitializeAndFormatFormComponents(Form form)
		{
			form.BackColor = FormBackgroundC;
			foreach (Control control in form.Controls)
				InitializeAndFormatControlComponents(control);
		}

		private static void InitializeAndFormatControlComponents(Control control)
		{
			if (control is Label || control is PictureBox || control is Panel)
				control.BackColor = FormBackgroundC;
			foreach (Control subControl in control.Controls)
				if (subControl is Label)
					(subControl as Label).BackColor = control.BackColor;
				else if (subControl is PictureBox)
					(subControl as PictureBox).BackColor = control.BackColor;
				else if (subControl is Panel)
					InitializeAndFormatControlComponents(subControl);
		}

		public static List<PictureBoxButton> CreateMenuButtons(Panel container, List<string> captions, bool horizontal, EventHandler click)
		{
			List<PictureBoxButton> result = new List<PictureBoxButton>();
			for (int i = 0, n = captions.Count, dim = horizontal ? container.Width / n : container.Height / n; i < n; i++)
			{
				PictureBoxButton butt = new PictureBoxButton(captions[i], click);
				butt.Parent = container;
				butt.Cursor = Cursors.Hand;
				if (horizontal)
					butt.SetBounds(i * dim, 0, dim, container.Height);
				else
					butt.SetBounds(0, i * dim, container.Width, dim);
				butt.OnMouseLeave(butt, null);
				result.Add(butt);
			}
			return result;
		}
	}

	/// 
	/// 
	/// 

	public class FontAndColors
	{
		public readonly Font Font;
		public readonly Brush BgBrush;
		public readonly Brush FontBrush;

		public FontAndColors(Font font, Color bgColor, Color fontColor)
		{
			this.Font = font;
			this.BgBrush = new SolidBrush(bgColor);
			this.FontBrush = new SolidBrush(fontColor);
		}
	}

	/// 
	/// 
	/// 

	public class PictureBoxButton : PictureBox
	{
		protected const int ButtonBarHeight = 4;
		protected static readonly Brush BarBr = new SolidBrush(MyGUIs.SelectedButtonC);
		protected static readonly Brush BarMOBr = new SolidBrush(MyGUIs.SelectedFontC);

		public string Caption;
		private EventHandler ClickEH;

		public PictureBoxButton(string Information, EventHandler clickEvt)
			: base()
		{
			this.Caption = Information;
			this.SetOnClickEventHandler(clickEvt);
			this.MouseEnter += OnMouseEnter;
			this.MouseLeave += OnMouseLeave;
			this.OnMouseLeave(null, null);
		}

		public void SetOnClickEventHandler(EventHandler click)
		{
			ClearOnClickEventHandler();
			this.ClickEH = click;
			this.Click += click;
		}

		public void ClearOnClickEventHandler()
		{
			this.Click -= ClickEH;
		}

		public void OnMouseEnter(object sender, EventArgs e)
		{
			DrawPictureBoxButton(true);
		}

		public void OnMouseLeave(object sender, EventArgs e)
		{
			DrawPictureBoxButton(false);
		}

		private void DrawPictureBoxButton(bool mouseOver)
		{
			if (this.Image != null)
				this.Image.Dispose();
			Bitmap bmp = new Bitmap(this.Width, this.Height);
			Graphics g = Graphics.FromImage(bmp);
			Brush bgBrush = mouseOver ? new SolidBrush(MyGUIs.SelectedButtonC) : new SolidBrush(MyGUIs.ButtonC);
			Brush textBrush = mouseOver ? new SolidBrush(MyGUIs.SelectedFontC) : new SolidBrush(MyGUIs.FontC);
			Font font = MyGUIs.GetFont("Segoe UI", 17, true);
			g.FillRectangle(bgBrush, 0, 0, this.Width, this.Height);
			g.FillRectangle(mouseOver ? BarMOBr : BarBr, 0, this.Height - ButtonBarHeight, this.Width, ButtonBarHeight);
			Size size = g.MeasureString(this.Caption, font).ToSize();
			g.DrawString(this.Caption, font, textBrush, new Point(this.Width / 2 - size.Width / 2, this.Height / 2 - size.Height / 2));
			this.Image = bmp;
		}
	}

	public class BorderPictureBox : PictureBox
	{
		public const int DefaultBorderWidth = 4;
		private static readonly Brush BgBr = new SolidBrush(MyGUIs.FormBackgroundC);
		private static readonly Brush BorderBr = new SolidBrush(ColorTranslator.FromHtml("#AAAAAA"));

		public int BorderWidth { get; set; }

		public BorderPictureBox(int borderWidth)
			: base()
		{
			this.BorderWidth = borderWidth;
		}

		public void RedrawBorder()
		{
			if (this.Image != null)
				this.Image.Dispose();
			Bitmap bmp = new Bitmap(this.Width, this.Height);
			Graphics g = Graphics.FromImage(bmp);
			g.FillRectangle(BorderBr, new Rectangle(0, 0, this.Width, this.Height));
			g.FillRectangle(BgBr, new Rectangle(BorderWidth, BorderWidth, this.Width - BorderWidth * 2, this.Height - BorderWidth * 2));
			this.Image = bmp;
		}
	}
}
