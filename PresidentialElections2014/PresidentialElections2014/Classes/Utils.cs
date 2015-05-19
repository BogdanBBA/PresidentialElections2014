using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PresidentialElections2014.Classes
{
	public static class Paths
	{
		public const string ProjectsFolder = @"projects";
		public const string UtilsFolder = @"utils";
		public const string SettingsFile = @"settings.xml";
		public const string LogoFile = UtilsFolder + @"\logo.jpg";
	}

	public static class AllNames
	{
		public static readonly Names Bureau = new Names("bureau", "bureaus");
	}

	public class Names
	{
		public readonly string SingularForm;
		public readonly string PluralForm;

		public Names(string singularForm, string pluralForm)
		{
			this.SingularForm = singularForm;
			this.PluralForm = pluralForm;
		}

		public string GetAppropriateForm(double quantity)
		{
			return quantity == 1 ? this.SingularForm : this.PluralForm;
		}

		public string GetAppropriateFormWithQuantity(double quantity)
		{
			return quantity + " " + (quantity == 1 ? this.SingularForm : this.PluralForm);
		}
	}

	public static class Utils
	{
		public static XmlAttribute GetXmlAttribute(XmlDocument doc, string name, string value)
		{
			XmlAttribute attr = doc.CreateAttribute(name);
			attr.Value = value;
			return attr;
		}

		public static Size ScaleRectangle(int width, int height, int maxWidth, int maxHeight)
		{
			var ratioX = (double) maxWidth / width;
			var ratioY = (double) maxHeight / height;
			var ratio = Math.Min(ratioX, ratioY);

			var newWidth = (int) (width * ratio);
			var newHeight = (int) (height * ratio);

			return new Size(newWidth, newHeight);
		}

		public static Image ScaleImage(Image image, int maxWidth, int maxHeight, bool disposeOldImage)
		{
			Size newSize = ScaleRectangle(image.Width, image.Height, maxWidth, maxHeight);
			Image newImage = new Bitmap(newSize.Width, newSize.Height);
			Graphics.FromImage(newImage).DrawImage(image, 0, 0, newSize.Width, newSize.Height);
			if (disposeOldImage)
				image.Dispose();
			return newImage;
		}

		public static string FormatDateTime(DateTime date, string format)
		{
			return date.ToString(format, CultureInfo.InvariantCulture);
		}

		public static string FormatNumberOrder(long number)
		{
			if (number == 0)
				return "0";
			number = Math.Abs(number);
			char[] ordChr = { ' ', 'K', 'M', 'B' };
			int powOrd = 3;
			while (powOrd >= 0)
			{
				long x = (long) Math.Pow(1000, powOrd);
				if (number >= x)
					return ((float) number / x).ToString("n" + powOrd) + ordChr[powOrd];
				powOrd--;
			}
			return "???";
		}

		public static string FormatNumber(long number)
		{
			return number.ToString("#,##0");
		}

		public static string FormatNumber(double number)
		{
			return number.ToString("#,##0.00");
		}

		public static string FormatDuration(long seconds)
		{
			return string.Format("{0}:{1}", seconds / 60, (seconds % 60).ToString("00"));
		}
	}
}
