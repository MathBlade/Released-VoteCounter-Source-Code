using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Support_Scripts;

namespace Assets.Scripts.SupportClasses
{
    [Serializable]
    public class HistoryServiceObject
    {

        List<List<Vote>> votesByDay;
        List<Day> days;
        VoteServiceObject vso;
        bool sortBy;
        bool simple;
        bool lSort;
        bool cleanDay;
        bool displayAllVCs;
        //Stopwatch stopWatch;


        public HistoryServiceObject(VoteServiceObject _vso, bool _sortBy, bool _simple, bool _lSort) : this(_vso, _sortBy, _simple, _lSort, false, false)
        {

        }

        public HistoryServiceObject(VoteServiceObject _vso, bool _sortBy, bool _simple, bool _lSort, bool _cleanDay, bool _displayAllVCs)
        {
            vso = _vso;
            votesByDay = BuildHistoryLogic.BuildVotesByDay(vso.Votes, vso.DayStartPostNumbers);
            days = BuildHistoryLogic.BuildDays(votesByDay, vso.NightkilledPlayers, vso.Players, vso.DayStartPostNumbers);
            sortBy = _sortBy;
            simple = _simple;
            lSort = _lSort;
            cleanDay = _cleanDay;
            displayAllVCs = _displayAllVCs;


        }


        public bool IsRestCall { get { return vso.IsRestCall; } }
        public List<List<Vote>> VotesByDay { get { return votesByDay; } }
        public List<Day> Days { get { return days; } }
        public VoteServiceObject VSO { get { return vso; } }
        public List<Player> AllPlayers { get { return VSO.Players; } }
        public bool SortBy { get { return sortBy; } }
        public bool Simple { get { return simple; } }
        public bool LSort { get { return lSort; } }
        public int PriorVCNumber { get { return VSO.PriorVCNumber; } }
        public string FlavorText { get { return VSO.FlavorText; } }
        public string DeadlineCode { get { return VSO.DeadlineCode; } }
        public bool CleanDay { get { return cleanDay; } }
        public bool DisplayAllVCs { get { return displayAllVCs; } }
        public string ColorCode { get { return VSO.ColorCode; } }
        public ProdTimer ProdTimer { get { return VSO.ProdTimer; } }
        public string FontOverride { get { return VSO.FontOverride; } }
        public bool AreaTagsOn { get { return VSO.AreaTagsOn; } }
        public string DividerOverride { get { return VSO.DividerOverride; } }
        public bool ShowLLevel { get { return VSO.ShowLLevel; } }
        public bool ShowZeroCountWagons { get { return VSO.ShowZeroCountWagons; } }
        public List<DayviggedPlayer> DayviggedPlayers { get { return VSO.DayviggedPlayers; } }
        public List<Replacement> Replacements { get { return VSO.Replacements; } }
        public List<ResurrectedPlayer> ResurrectedPlayers { get { return VSO.ResurrectedPlayers; } }



    }
}
