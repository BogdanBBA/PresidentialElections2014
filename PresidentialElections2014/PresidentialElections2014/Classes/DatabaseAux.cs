using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PresidentialElections2014.Classes
{
	public class ColumnConfiguration
	{
		public readonly string ConfigFilePath;
		public readonly List<Column> Columns;

		public ColumnConfiguration(string databasePath, ElectionsDatabase eDB)
		{
			this.ConfigFilePath = databasePath.Replace(Path.GetExtension(databasePath), ".configXML");
			this.Columns = new List<Column>();

			try
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(this.ConfigFilePath);
				XmlNodeList colNodes = doc.SelectNodes("ElectionsDatabaseConfiguration/Column");
				foreach (XmlNode colNode in colNodes)
				{
					int type = Int32.Parse(colNode.Attributes["type"].Value);
					int percentageWidth = Int32.Parse(colNode.Attributes["percentageWidth"].Value);
					int candidateNumber = !colNode.Attributes["candidateNumber"].Value.Equals("")
						? Int32.Parse(colNode.Attributes["candidateNumber"].Value) : -1;
					Candidate candidate = eDB.GetCandidateByNumber(candidateNumber);
					bool percWP = Boolean.Parse(colNode.Attributes["percentageWherePossible"].Value);
					bool visible = Boolean.Parse(colNode.Attributes["visible"].Value);
					this.Columns.Add(new Column(type, candidate, percentageWidth, percWP, visible));
				}
			}
			catch (Exception E)
			{
				this.Columns.Add(new Column(Column.POS, null, 10, false, true));
				this.Columns.Add(new Column(Column.REG_POS, null, 10, false, true));
				this.Columns.Add(new Column(Column.NAME, null, 20, false, true));
				this.Columns.Add(new Column(Column.PARENT, null, 20, false, true));
				this.Columns.Add(new Column(Column.TOTAL_PERMANENT_VOTERS, null, 10, false, false));
				this.Columns.Add(new Column(Column.PRESENCE, null, 10, true, false));
				this.Columns.Add(new Column(Column.VALID_VOTES, null, 15, false, false));
				this.Columns.Add(new Column(Column.PERMANENT_VOTERS, null, 15, false, false));
				this.Columns.Add(new Column(Column.PERMANENT_VOTERS, null, 10, true, false));
				this.Columns.Add(new Column(Column.TOURIST_VOTERS, null, 15, false, false));
				this.Columns.Add(new Column(Column.TOURIST_VOTERS, null, 10, true, false));
				this.Columns.Add(new Column(Column.SPECIAL_VOTERS, null, 15, false, false));
				this.Columns.Add(new Column(Column.SPECIAL_VOTERS, null, 10, true, false));
				foreach (Candidate candidate in eDB.Candidates)
				{
					this.Columns.Add(new Column(Column.CANDIDATE_VOTES, candidate, 15, false, false));
					this.Columns.Add(new Column(Column.CANDIDATE_VOTES, candidate, 10, true, false));
				}
			}

			this.SaveToFile();
		}

		public void SaveToFile()
		{
			try
			{
				XmlDocument doc = new XmlDocument();
				XmlNode node = doc.CreateElement("ElectionsDatabaseConfiguration");
				node.Attributes.Append(Utils.GetXmlAttribute(doc, "lastSaved", DateTime.Now.ToString("ddd, d MMMM yyyy, HH:mm:ss")));
				foreach (Column column in this.Columns)
				{
					XmlNode colNode = doc.CreateElement("Column");
					colNode.Attributes.Append(Utils.GetXmlAttribute(doc, "type", column.ColumnType.ToString()));
					colNode.Attributes.Append(Utils.GetXmlAttribute(doc, "candidateNumber", column.Candidate == null ? "" : column.Candidate.Number.ToString()));
					colNode.Attributes.Append(Utils.GetXmlAttribute(doc, "percentageWidth", column.WidthPercentage.ToString()));
					colNode.Attributes.Append(Utils.GetXmlAttribute(doc, "percentageWherePossible", column.PercentageWherePossible.ToString()));
					colNode.Attributes.Append(Utils.GetXmlAttribute(doc, "visible", column.Visible.ToString()));
					node.AppendChild(colNode);
				}
				doc.AppendChild(node);
				doc.Save(this.ConfigFilePath);
			}
			catch (Exception E) { MessageBox.Show("ColumnConfiguration.SaveToFile() ERROR:\n\n" + E.ToString()); }
		}

		public string EncodeColumn(Column col)
		{
			return new StringBuilder().Append("[").Append(col.ColumnType).Append("]. ").Append(col.ColumnInfoText()).ToString();
		}

		public Column DecodeColumn(string colStr)
		{
			int colType = Int32.Parse(colStr.Substring(1, colStr.IndexOf("]") - 1));
			bool percWB = colStr.IndexOf("/%") != -1;
			foreach (Column col in this.Columns)
				if (col.ColumnType == colType && col.PercentageWherePossible == percWB && colStr.IndexOf(col.ColumnInfoText()) != -1)
					return col;
			return null;
		}

		public Column GetColumnByInformationText(string infoText)
		{
			foreach (Column col in this.Columns)
				if (infoText.Equals(col.ColumnInfoText()))
					return col;
			return null;
		}
	}

	public class Column
	{
		public const int POS = 1, REG_POS = 2, NAME = 3, PARENT = 4;
		public const int TOTAL_PERMANENT_VOTERS = 10, PRESENCE = 11, VALID_VOTES = 12, PERMANENT_VOTERS = 13, TOURIST_VOTERS = 14, SPECIAL_VOTERS = 15;
		public const int CANDIDATE_VOTES = 20;

		public int ColumnType;
		public Candidate Candidate;
		public int WidthPercentage;
		public bool PercentageWherePossible;
		public bool Visible;

		public Column(int columnType, Candidate candidate, int percentageWidth, bool percentageWherePossible, bool visible)
		{
			this.ColumnType = columnType;
			this.Candidate = candidate;
			this.WidthPercentage = percentageWidth;
			this.PercentageWherePossible = percentageWherePossible;
			this.Visible = visible;
		}

		public int GetInteger(NamedItem item)
		{
			switch (this.ColumnType)
			{
				case Column.TOTAL_PERMANENT_VOTERS:
					return item.Votes.TotalRegisteredVoters;
				case Column.VALID_VOTES:
					return item.Votes.Votes_TotalValid;
				case Column.PERMANENT_VOTERS:
					return item.Votes.Votes_RegisteredVoters;
				case Column.TOURIST_VOTERS:
					return item.Votes.Votes_TouristVoters;
				case Column.SPECIAL_VOTERS:
					return item.Votes.Votes_SpecialVoters;
				case Column.CANDIDATE_VOTES:
					return item.Votes.GetVoteByCandidate(this.Candidate).VoteCount;
				default:
					return -1;
			}
		}

		public double GetDouble(NamedItem item)
		{
			switch (this.ColumnType)
			{
				case Column.PRESENCE:
					return item.Votes.Votes_TotalValid * 100.0 / item.Votes.TotalRegisteredVoters;
				case Column.PERMANENT_VOTERS:
					return item.Votes.Votes_RegisteredVoters * 100.0 / item.Votes.Votes_TotalValid;
				case Column.TOURIST_VOTERS:
					return item.Votes.Votes_TouristVoters * 100.0 / item.Votes.Votes_TotalValid;
				case Column.SPECIAL_VOTERS:
					return item.Votes.Votes_SpecialVoters * 100.0 / item.Votes.Votes_TotalValid;
				case Column.CANDIDATE_VOTES:
					return item.Votes.GetVoteByCandidate(this.Candidate).VoteCount * 100.0 / item.Votes.Votes_TotalValid;
				default:
					return -1.0;
			}
		}

		public string FormatDoublePercentage(NamedItem item)
		{
			return Utils.FormatNumber(this.GetDouble(item)) + "%";
		}

		public string FormatInformation(NamedItem item)
		{
			switch (this.ColumnType)
			{
				case Column.POS:
				case Column.REG_POS:
					return "#0";
				case Column.NAME:
					return item.Name;
				case Column.PARENT:
					return item.Parent != null ? item.Parent.Name : "-";
				case Column.TOTAL_PERMANENT_VOTERS:
				case Column.VALID_VOTES:
					return Utils.FormatNumber(this.GetInteger(item));
				case Column.PRESENCE:
					return Utils.FormatNumber(this.GetDouble(item)) + "%";
				case Column.PERMANENT_VOTERS:
				case Column.TOURIST_VOTERS:
				case Column.SPECIAL_VOTERS:
				case Column.CANDIDATE_VOTES:
					return this.PercentageWherePossible ? this.FormatDoublePercentage(item) : Utils.FormatNumber(this.GetInteger(item));
				default:
					return "Column.FormatInformation() ERROR";
			}
		}

		public string ColumnInfoText()
		{
			string result;
			switch (this.ColumnType)
			{
				case Column.POS:
					result = "Pos";
					break;
				case Column.REG_POS:
					result = "Reg. pos";
					break;
				case Column.NAME:
					result = "Name";
					break;
				case Column.PARENT:
					result = "Parent";
					break;
				case Column.TOTAL_PERMANENT_VOTERS:
					result = "V:TotalPermanent";
					break;
				case Column.PRESENCE:
					result = "Presence";
					break;
				case Column.VALID_VOTES:
					result = "Valid votes";
					break;
				case Column.PERMANENT_VOTERS:
					result = "V:Permanent";
					break;
				case Column.TOURIST_VOTERS:
					result = "V:Tourists";
					break;
				case Column.SPECIAL_VOTERS:
					result = "V:Special";
					break;
				case Column.CANDIDATE_VOTES:
					result = "V:" + this.Candidate.ShortName;
					break;
				default:
					return "Column.ColumnInfoText() ERROR";
			}
			if (this.PercentageWherePossible)
				result += "/%";
			return result;
		}
	}
}
