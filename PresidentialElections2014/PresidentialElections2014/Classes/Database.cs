using PresidentialElections2014.VisualComponents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PresidentialElections2014.Classes
{
	public partial class ElectionsDatabase
	{
		public ElectionsDatabase(string path, out string error)
		{
			string phase = "initializing data types";

			phase = "initializing xml";
			XmlDocument doc = new XmlDocument();
			try
			{
				phase = "opening xml (at '" + path + "')";
				doc.Load(path);

				phase = "decoding candidates (also shortName!)";
				XmlNodeList nodeList = doc.SelectNodes("ElectionsDatabase/Candidates/Candidate");
				this.Candidates = new List<Candidate>();
				foreach (XmlNode candNode in nodeList)
				{
					int number = Int32.Parse(candNode.Attributes["number"].Value);
					string name = candNode.Attributes["name"].Value;
					string shortName = candNode.Attributes["shortName"].Value;
					Color color = ColorTranslator.FromHtml(candNode.Attributes["color"].Value);
					this.Candidates.Add(new Candidate(number, name, shortName, color));
				}

				phase = "decoding electoral bureaus";
				nodeList = doc.SelectNodes("ElectionsDatabase/ElectoralBureaus/ElectoralBureau");
				this.WorldwideResults = new ElectoralBureauList("Worldwide results", false);
				foreach (XmlNode ebNode in nodeList) // each electoral bureau
				{
					int ebNumber = Int32.Parse(ebNode.Attributes["number"].Value);
					string ebName = ebNode.Attributes["name"].Value;
					XmlNodeList townList = ebNode.SelectNodes("Town");
					List<TownVillage> townVillages = new List<TownVillage>();
					foreach (XmlNode townNode in townList) // each town/village in electoral bureau
					{
						string townName = townNode.Attributes["name"].Value;
						int townRank = Int32.Parse(townNode.Attributes["rank"].Value);
						XmlNodeList vsList = townNode.SelectNodes("VotingSection");
						List<VotingSection> votingSections = new List<VotingSection>();
						foreach (XmlNode vsNode in vsList) // each voting section in town/village
						{
							int vsNumber = Int32.Parse(vsNode.Attributes["number"].Value);
							string vsName = vsNode.Attributes["name"].Value;
							int totalPermV = Int32.Parse(vsNode.Attributes["totalPermV"].Value);
							int permV = Int32.Parse(vsNode.Attributes["permV"].Value);
							int tourV = Int32.Parse(vsNode.Attributes["tourV"].Value);
							int specV = Int32.Parse(vsNode.Attributes["specV"].Value);
							string[] votesStr = vsNode.Attributes["votes"].Value.Split(';');
							List<Vote> votes = new List<Vote>();
							for (int iVote = 0; iVote < votesStr.Length; iVote++) // each vote in voting section
								votes.Add(new Vote(this.Candidates[iVote], Int32.Parse(votesStr[iVote])));
							VoteSet voteSet = new VoteSet(totalPermV, permV, tourV, specV, votes); // vote set
							votingSections.Add(new VotingSection(vsName, vsNumber, voteSet)); // voting section
						}
						townVillages.Add(new TownVillage(townName, townRank, votingSections)); // town/village
						foreach (VotingSection kVS in townVillages.Last().VotingSections)
							kVS.Parent = townVillages.Last();
					}
					this.WorldwideResults.AddElectoralBureau(new ElectoralBureau(ebName, ebNumber, townVillages)); // electoral bureau
					foreach (TownVillage kTV in this.WorldwideResults.ElectoralBureaus.Last().TownsVillages)
						kTV.Parent = this.WorldwideResults.ElectoralBureaus.Last();
				}

				phase = "decoding regions";
				nodeList = doc.SelectNodes("ElectionsDatabase/Regions/Region");
				this.Regions = new List<ElectoralBureauList>();
				foreach (XmlNode regNode in nodeList)
				{
					ElectoralBureauList region = new ElectoralBureauList(regNode.Attributes["name"].Value, true);
					string[] regEBstrs = regNode.Attributes["electoralBureaus"].Value.Split(';');
					foreach (string regEBstr in regEBstrs)
					{
						ElectoralBureau regEB = this.WorldwideResults.GetElectoralBureauByNumber(Int32.Parse(regEBstr));
						region.AddElectoralBureau(regEB);
						regEB.Parent = region;
					}
					region.Parent = this.WorldwideResults;
					this.Regions.Add(region);
				}
			}
			catch (ApplicationException E)
			{
				error = "# A preventable error occured while opening the project, at phase \"" + phase + "\". See below for detailed information.\n\n# " + E.ToString();
				return;
			}
			catch (Exception E)
			{
				error = "# An unexpected error occured while opening the project, at phase \"" + phase + "\". See below for detailed information.\n\n# " + E.ToString();
				return;
			}
			this.ColumnConfig = new ColumnConfiguration(path, this);
			error = "";
		}
	}

	///
	///
	///

	public class AppSettings
	{
		public ElectoralBureauList FilteredEBs;

		public AppSettings()
		{
			this.FilteredEBs = new ElectoralBureauList("Filtered EBs", false);
		}

		public void ReadFromFile()
		{
			try
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(Paths.SettingsFile);
				XmlNode node = doc.SelectSingleNode("/SETTINGS");

				/*try { CompactCategories = Boolean.Parse(node["CompactCategories"].Attributes["value"].Value); }
				catch (Exception E) { CompactCategories = false; }*/
			}
			catch (Exception E)
			{
				MessageBox.Show(E.ToString(), "TSettings.ReadFromFile() ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void SaveToFile()
		{
			try
			{
				XmlDocument doc = new XmlDocument();
				XmlNode root, node;

				root = doc.AppendChild(doc.CreateElement("SETTINGS"));
				root.Attributes.Append(Utils.GetXmlAttribute(doc, "lastSavedCirca", DateTime.Now.ToString("ddd, d MMMM yyyy, HH:mm")));

				/*node = root.AppendChild(doc.CreateElement("CompactCategories"));
				node.Attributes.Append(Utils.GetXmlAttribute(doc, "value", CompactCategories.ToString()));*/

				doc.Save(Paths.SettingsFile);
			}
			catch (Exception E)
			{
				MessageBox.Show(E.ToString(), "TSettings.SaveToFile() ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
