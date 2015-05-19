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
	public class InformationManager : Panel
	{
		private static readonly int BoxHeight = 30;

		private EventHandler HeaderOnClickEvt;
		private EventHandler BoxesOnClickEvt;

		private ElectionsDatabase DB;
		public InformationHeaderPB Header;
		public InformationTotalsPB Totals;
		public List<InformationPB> Boxes;

		public List<NamedItem> LastItems;

		public InformationManager(Panel container, Rectangle bounds, Panel headerPanel, Panel totalsPanel, ElectionsDatabase database, EventHandler headerClickEvt, EventHandler boxesClickEvt)
			: base()
		{
			this.Parent = container;
			this.Bounds = bounds;
			this.DB = database;
			this.LastItems = new List<NamedItem>();
			this.HeaderOnClickEvt += headerClickEvt;
			this.BoxesOnClickEvt += boxesClickEvt;
			this.AutoScroll = true;
			this.VScroll = true;
			// header
			this.Header = new InformationHeaderPB(this.HeaderOnClickEvt);
			this.Header.Parent = headerPanel;
			this.Header.SetBounds(0, 0, this.Width - MyGUIs.PanelContentRightPadding, this.Header.Parent.Height);
			// totals
			this.Totals = new InformationTotalsPB();
			this.Totals.Parent = totalsPanel;
			this.Totals.SetBounds(0, 0, this.Width - MyGUIs.PanelContentRightPadding, this.Totals.Parent.Height);
		}

		public void RecreateHeaderAndInformationBoxes(AppSettings settings, List<NamedItem> items)
		{
			this.LastItems = items;

			// header
			this.Header.ColumnConfig = this.DB.ColumnConfig;

			// boxes
			this.VerticalScroll.Value = 0;
			if (this.Boxes == null)
				this.Boxes = new List<InformationPB>();
			for (int i = items.Count; i < this.Boxes.Count; i++)
				this.Boxes[i].Hide();
			for (int i = 0; i < items.Count; i++)
			{
				if (i >= this.Boxes.Count)
				{
					InformationPB newInfoPB = new InformationPB(null, this.BoxesOnClickEvt);
					newInfoPB.Parent = this;
					this.Boxes.Add(newInfoPB);
				}
				InformationPB box = this.Boxes[i];
				box.ColumnConfig = this.DB.ColumnConfig;
				box.Item = items[i];
				box.SetBounds(0, i * InformationManager.BoxHeight, this.Width - MyGUIs.PanelContentRightPadding, InformationManager.BoxHeight);
				box.Show();
			}

			// totals
			this.Totals.ColumnConfig = this.DB.ColumnConfig;

			// refresh
			this.RefreshAllInformation();
		}

		public void RefreshAllInformation()
		{
			this.Header.OnMouseLeave(null, null);
			for (int i = 0; i < this.Boxes.Count; i++)
				if (this.Boxes[i].Visible)
					this.Boxes[i].OnMouseLeave(null, null);
			this.Totals.OnMouseLeave(null, null);
		}
	}

	public class InformationHeaderPB : PictureBox
	{
		protected static readonly FontAndColors Cols = new FontAndColors(MyGUIs.GetFont("Ubuntu Condensed", 15, true), MyGUIs.ButtonC, MyGUIs.FontC);
		protected static readonly FontAndColors ColsMO = new FontAndColors(MyGUIs.GetFont("Ubuntu Condensed", 15, true), MyGUIs.SelectedButtonC, MyGUIs.SelectedFontC);
		protected static readonly Brush BarBr = new SolidBrush(Color.Orange);

		public ColumnConfiguration ColumnConfig;

		public InformationHeaderPB(EventHandler ClickEvt)
			: base()
		{
			this.ColumnConfig = null;
			this.Cursor = Cursors.Hand;
			this.Click += ClickEvt;
			this.MouseEnter += OnMouseEnter;
			this.MouseLeave += OnMouseLeave;
		}

		public void OnMouseEnter(object sender, EventArgs e)
		{ DrawInformationHeaderPB(true); }

		public void OnMouseLeave(object sender, EventArgs e)
		{ DrawInformationHeaderPB(false); }

		private void DrawInformationHeaderPB(bool mouseOver)
		{
			if (this.ColumnConfig == null)
				return;
			if (this.Image != null)
				this.Image.Dispose();
			Bitmap bmp = new Bitmap(this.Width, this.Height);
			Graphics g = Graphics.FromImage(bmp);
			FontAndColors CF = mouseOver ? ColsMO : Cols;
			g.FillRectangle(CF.BgBrush, 0, 0, this.Width, this.Height);

			int lastLeft = 0;
			FontAndColors cols = mouseOver ? ColsMO : Cols;
			foreach (Column column in this.ColumnConfig.Columns)
				if (column.Visible)
				{
					int colWidth = (int) Math.Round((float) this.Width / 100 * column.WidthPercentage);
					string text = column.ColumnInfoText();
					Size size = g.MeasureString(text, cols.Font).ToSize();
					g.DrawString(text, cols.Font, cols.FontBrush, new Point(lastLeft + colWidth / 2 - size.Width / 2, this.Height / 2 - size.Height / 2));
					lastLeft += colWidth;
				}

			this.Image = bmp;
		}
	}

	public class InformationTotalsPB : PictureBox
	{
		protected static readonly FontAndColors Cols = new FontAndColors(MyGUIs.GetFont("Ubuntu Condensed", 15, true), MyGUIs.ButtonC, MyGUIs.FontC);
		protected static readonly FontAndColors ColsMO = new FontAndColors(MyGUIs.GetFont("Ubuntu Condensed", 15, true), MyGUIs.SelectedButtonC, MyGUIs.SelectedFontC);
		protected static readonly Brush BarBr = new SolidBrush(Color.Orange);

		public ColumnConfiguration ColumnConfig;
		//public List<Entity> Entites;

		public InformationTotalsPB(/*, List<Entity> entities*/)
			: base()
		{
			this.ColumnConfig = null;
			//this.Entites = entities;
			this.MouseEnter += OnMouseEnter;
			this.MouseLeave += OnMouseLeave;
		}

		public void OnMouseEnter(object sender, EventArgs e)
		{ DrawInformationTotalsPB(true); }

		public void OnMouseLeave(object sender, EventArgs e)
		{ DrawInformationTotalsPB(false); }

		private void DrawInformationTotalsPB(bool mouseOver)
		{
			if (this.ColumnConfig == null)
				return;
			if (this.Image != null)
				this.Image.Dispose();
			Bitmap bmp = new Bitmap(this.Width, this.Height);
			Graphics g = Graphics.FromImage(bmp);
			FontAndColors CF = mouseOver ? ColsMO : Cols;
			g.FillRectangle(CF.BgBrush, 0, 0, this.Width, this.Height);

			int lastLeft = 0;
			FontAndColors cols = mouseOver ? ColsMO : Cols;
			foreach (Column column in this.ColumnConfig.Columns)
				if (column.Visible)
				{
					int colWidth = (int) Math.Round((float) this.Width / 100 * column.WidthPercentage);
					string text = ":\\\\";
					Size size = g.MeasureString(text, cols.Font).ToSize();
					g.DrawString(text, cols.Font, cols.FontBrush, new Point(lastLeft + colWidth / 2 - size.Width / 2, this.Height / 2 - size.Height / 2));
					lastLeft += colWidth;
				}

			this.Image = bmp;
		}
	}

	public class InformationPB : PictureBox
	{
		protected static readonly FontAndColors ParentCols = new FontAndColors(MyGUIs.GetFont("Ubuntu Condensed", 13, false), MyGUIs.ButtonC, MyGUIs.FontC);
		protected static readonly FontAndColors ParentColsMO = new FontAndColors(MyGUIs.GetFont("Ubuntu Condensed", 13, false), MyGUIs.SelectedButtonC, MyGUIs.SelectedFontC);

		protected static readonly FontAndColors DefaultCols = new FontAndColors(MyGUIs.GetFont("Segoe UI", 12, false), MyGUIs.ButtonC, MyGUIs.FontC);
		protected static readonly FontAndColors DefaultColsMO = new FontAndColors(MyGUIs.GetFont("Segoe UI", 12, false), MyGUIs.SelectedButtonC, MyGUIs.SelectedFontC);

		private static readonly FontAndColors ww = new FontAndColors(MyGUIs.GetFont("Ubuntu", 15, true), MyGUIs.ButtonC, Color.Purple);
		private static readonly FontAndColors wwMO = new FontAndColors(MyGUIs.GetFont("Ubuntu", 15, true), MyGUIs.SelectedButtonC, Color.Purple);
		private static readonly FontAndColors reg = new FontAndColors(MyGUIs.GetFont("Didact Gothic", 15, true), MyGUIs.ButtonC, Color.CadetBlue);
		private static readonly FontAndColors regMO = new FontAndColors(MyGUIs.GetFont("Didact Gothic", 15, true), MyGUIs.SelectedButtonC, Color.CadetBlue);
		private static readonly FontAndColors eb = new FontAndColors(MyGUIs.GetFont("Didact Gothic", 15, true), MyGUIs.ButtonC, Color.ForestGreen);
		private static readonly FontAndColors ebMO = new FontAndColors(MyGUIs.GetFont("Didact Gothic", 15, true), MyGUIs.SelectedButtonC, Color.ForestGreen);
		private static readonly FontAndColors mun = new FontAndColors(MyGUIs.GetFont("Ubuntu", 14, false), MyGUIs.ButtonC, Color.Maroon);
		private static readonly FontAndColors munMO = new FontAndColors(MyGUIs.GetFont("Ubuntu", 14, false), MyGUIs.SelectedButtonC, Color.Maroon);
		private static readonly FontAndColors town = new FontAndColors(MyGUIs.GetFont("Ubuntu", 14, false), MyGUIs.ButtonC, MyGUIs.FontC);
		private static readonly FontAndColors townMO = new FontAndColors(MyGUIs.GetFont("Ubuntu", 14, false), MyGUIs.SelectedButtonC, MyGUIs.FontC);
		private static readonly FontAndColors vill = new FontAndColors(MyGUIs.GetFont("Ubuntu", 13, false), MyGUIs.ButtonC, MyGUIs.FontC);
		private static readonly FontAndColors villMO = new FontAndColors(MyGUIs.GetFont("Ubuntu", 13, false), MyGUIs.SelectedButtonC, MyGUIs.FontC);
		private static readonly FontAndColors oCou = new FontAndColors(MyGUIs.GetFont("Ubuntu", 13, false), MyGUIs.ButtonC, MyGUIs.FontC);
		private static readonly FontAndColors oCouMO = new FontAndColors(MyGUIs.GetFont("Ubuntu", 13, false), MyGUIs.SelectedButtonC, MyGUIs.FontC);
		private static readonly FontAndColors vs = new FontAndColors(MyGUIs.GetFont("Ubuntu", 11, false, true), MyGUIs.ButtonC, Color.LightGray);
		private static readonly FontAndColors vsMO = new FontAndColors(MyGUIs.GetFont("Ubuntu", 11, false, true), MyGUIs.SelectedButtonC, Color.LightGray);

		private static readonly FontAndColors[] Cols = new FontAndColors[9] { DefaultCols, ww, reg, eb, mun, town, vill, oCou, vs };
		private static readonly FontAndColors[] ColsMO = new FontAndColors[9] { DefaultColsMO, wwMO, regMO, ebMO, munMO, townMO, villMO, oCouMO, vsMO };

		public ColumnConfiguration ColumnConfig;
		public NamedItem Item;

		public InformationPB(NamedItem item, EventHandler ClickEvt)
			: base()
		{
			this.ColumnConfig = null;
			this.Item = item;
			this.Click += ClickEvt;
			this.MouseEnter += OnMouseEnter;
			this.MouseLeave += OnMouseLeave;
		}

		public void OnMouseEnter(object sender, EventArgs e)
		{ DrawInformationPB(true); }

		public void OnMouseLeave(object sender, EventArgs e)
		{ DrawInformationPB(false); }

		private void DrawInformationPB(bool mouseOver)
		{
			if (this.ColumnConfig == null)
				return;
			if (this.Image != null)
				this.Image.Dispose();
			Bitmap bmp = new Bitmap(this.Width, this.Height);
			Graphics g = Graphics.FromImage(bmp);
			FontAndColors CF = mouseOver ? DefaultColsMO : DefaultCols;
			g.FillRectangle(CF.BgBrush, 0, 0, this.Width, this.Height);

			int lastLeft = 0;
			foreach (Column column in this.ColumnConfig.Columns)
				if (column.Visible)
				{
					// get the proper style for this column
					FontAndColors cols;
					switch (column.ColumnType)
					{
						case Column.NAME:
							int colsIndex = 0;
							switch (this.Item.ItemRank())
							{
								case NamedItem.WORLDWIDE_RANK:
								case NamedItem.REGION_RANK:
								case NamedItem.EB_RANK:
									colsIndex = this.Item.ItemRank();
									break;
								case NamedItem.TOWNVILLAGE_RANK:
									colsIndex = this.Item.ItemRank() + (this.Item as TownVillage).TownRank - 1;
									break;
								case NamedItem.VOTINGSECTION_RANK:
									colsIndex = 8;
									break;
							}
							cols = mouseOver ? ColsMO[colsIndex] : Cols[colsIndex];
							break;
						case Column.PARENT:
							cols = mouseOver ? ParentColsMO : ParentCols;
							break;
						default:
							cols = mouseOver ? ColsMO[0] : Cols[0];
							break;
					}
					Brush fontBrush = column.ColumnType == Column.CANDIDATE_VOTES ? column.Candidate.CandidateBrush : cols.FontBrush;
					
					// draw
					int colWidth = (int) Math.Round((float) this.Width / 100 * column.WidthPercentage);
					string text = column.FormatInformation(this.Item);
					Size size = g.MeasureString(text, cols.Font).ToSize();
					g.DrawString(text, cols.Font, fontBrush, new Point(lastLeft + colWidth / 2 - size.Width / 2, this.Height / 2 - size.Height / 2));
					lastLeft += colWidth;
				}

			this.Image = bmp;
		}
	}
}
