using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresidentialElections2014.Classes
{
	public abstract class NamedItem
	{
		public const int OBSCURE_RANK = 0, WORLDWIDE_RANK = 1, REGION_RANK = 2, EB_RANK = 3, TOWNVILLAGE_RANK = 4, VOTINGSECTION_RANK = 5;

		public readonly string Name;
		public NamedItem Parent;
		public readonly VoteSet Votes;

		public NamedItem(string name)
		{
			this.Name = name;
			this.Parent = null;
			this.Votes = new VoteSet(0, 0, 0, 0, new List<Vote>());
		}

		public abstract int ItemRank();
	}

	public abstract class NumberedItem : NamedItem
	{
		public readonly int Number;

		public NumberedItem(string name, int number)
			: base(name)
		{
			this.Number = number;
		}

		public override int ItemRank()
		{ return NamedItem.OBSCURE_RANK; }
	}

	public partial class ElectionsDatabase
	{
		public readonly List<Candidate> Candidates;
		public readonly ElectoralBureauList WorldwideResults;
		public readonly List<ElectoralBureauList> Regions;
		public readonly ColumnConfiguration ColumnConfig;

		public Candidate GetCandidateByNumber(int number)
		{
			foreach (Candidate candidate in this.Candidates)
				if (candidate.Number == number)
					return candidate;
			return null;
		}

		public ElectoralBureauList GetRegionByName(string name)
		{
			foreach (ElectoralBureauList region in this.Regions)
				if (region.Name.Equals(name))
					return region;
			return null;
		}
	}

	public class Candidate : NumberedItem
	{
		public string ShortName;
		public readonly Brush CandidateBrush;

		public Candidate(int number, string name, string shortName, Color color)
			: base(name, number)
		{
			this.ShortName = shortName;
			this.CandidateBrush = new SolidBrush(color);
		}

		public override int ItemRank()
		{ return NamedItem.OBSCURE_RANK; }
	}

	public class ElectoralBureauList : NamedItem
	{
		public readonly List<ElectoralBureau> ElectoralBureaus;
		private readonly bool IsRegion;

		public ElectoralBureauList(string name, bool isRegion)
			: base(name)
		{
			this.ElectoralBureaus = new List<ElectoralBureau>();
			this.IsRegion = isRegion;
		}

		public override int ItemRank()
		{ return this.IsRegion ? NamedItem.REGION_RANK : NamedItem.WORLDWIDE_RANK; }

		public void AddElectoralBureau(ElectoralBureau eb)
		{
			this.ElectoralBureaus.Add(eb);
			this.Votes.AddVotes(eb.Votes);
		}

		public ElectoralBureau GetElectoralBureauByNumber(int number)
		{
			foreach (ElectoralBureau electoralBureau in this.ElectoralBureaus)
				if (electoralBureau.Number == number)
					return electoralBureau;
			return null;
		}
	}

	public class ElectoralBureau : NumberedItem
	{
		public readonly List<TownVillage> TownsVillages;

		public ElectoralBureau(string name, int number, List<TownVillage> townsVillages)
			: base(name, number)
		{
			this.TownsVillages = townsVillages;
			foreach (TownVillage town in townsVillages)
				this.Votes.AddVotes(town.Votes);
		}

		public override int ItemRank()
		{ return NamedItem.EB_RANK; }
	}

	public class TownVillage : NamedItem
	{
		public const int MUNICIPALITY = 1, TOWN = 2, VILLAGE = 3, OTHER_COUNTRY = 4;

		public readonly List<VotingSection> VotingSections;
		public readonly int TownRank;

		public TownVillage(string name, int townRank, List<VotingSection> votingSections)
			: base(name)
		{
			this.TownRank = townRank;
			this.VotingSections = votingSections;
			foreach (VotingSection votingSection in votingSections)
				this.Votes.AddVotes(votingSection.Votes);
		}

		public override int ItemRank()
		{ return NamedItem.TOWNVILLAGE_RANK; }
	}

	public class VotingSection : NumberedItem
	{
		public VotingSection(string name, int number, VoteSet votes)
			: base(name, number)
		{
			this.Votes.AddVotes(votes);
		}

		public override int ItemRank()
		{ return NamedItem.VOTINGSECTION_RANK; }
	}

	public class VoteSet : List<Vote>
	{
		public int TotalRegisteredVoters;
		public int Votes_RegisteredVoters;
		public int Votes_TouristVoters;
		public int Votes_SpecialVoters;
		public int Votes_TotalValid;

		public VoteSet(int totalRegVot, int vRegVot, int vTourVot, int vSpecVot, List<Vote> votes)
			: base()
		{
			this.TotalRegisteredVoters = totalRegVot;
			this.Votes_RegisteredVoters = vRegVot;
			this.Votes_TouristVoters = vTourVot;
			this.Votes_SpecialVoters = vSpecVot;
			this.Votes_TotalValid = 0;
			this.AddRange(votes);
		}

		public void AddVotes(VoteSet voteSet)
		{
			this.TotalRegisteredVoters += voteSet.TotalRegisteredVoters;
			this.Votes_RegisteredVoters += voteSet.Votes_RegisteredVoters;
			this.Votes_TouristVoters += voteSet.Votes_TouristVoters;
			this.Votes_SpecialVoters += voteSet.Votes_SpecialVoters;
			foreach (Vote vote in voteSet)
			{
				this.GetVoteByCandidate(vote.Candidate).VoteCount += vote.VoteCount;
				this.Votes_TotalValid += vote.VoteCount;
			}
			this.SortVotes();
		}

		public void SortVotes()
		{
			if (this.Count < 2)
				return;
			for (int i = 0; i < this.Count - 1; i++)
				for (int j = i + 1; j < this.Count; j++)
					if (this[i].VoteCount < this[j].VoteCount)
					{
						Vote auxV = this[i];
						this[i] = this[j];
						this[j] = auxV;
					}
		}

		public Vote GetVoteByCandidate(Candidate candidate)
		{
			foreach (Vote vote in this)
				if (vote.Candidate.Name.Equals(candidate.Name))
					return vote;
			this.Add(new Vote(candidate, 0));
			return this.Last();
		}
	}

	public class Vote
	{
		public readonly Candidate Candidate;
		public int VoteCount;

		public Vote(Candidate candidate, int voteCount)
		{
			this.Candidate = candidate;
			this.VoteCount = voteCount;
		}
	}
}
