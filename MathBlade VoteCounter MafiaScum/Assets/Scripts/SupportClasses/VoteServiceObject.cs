using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using Assets.Scripts.Support_Scripts;
using UnityEngine;

namespace Assets.Scripts.SupportClasses
{
    [Serializable]
    public class VoteServiceObject
    {
        private List<Vote> votes;
        private string errorMessage;
        private List<Player> players;
        private List<Replacement> replacements;
        private List<int> dayStartPostNumbers;
        private List<List<Player>> nightkilledPlayers;
        private List<DayviggedPlayer> dayviggedPlayers;
        private List<ResurrectedPlayer> resurrectedPlayers;
        private ProdTimer prodTimer;

        //Stuff that should have been part of the history if I can get it there.
        private int priorVCNumber;
        private string flavorText;
        private string deadlineCode;
        private bool isRestCall;
        private string colorCode;
        private string fontOverride;
        private bool areaTagsOn;
        private string dividerOverride;
        private bool showLLevel;
        private bool showZeroCountWagons;
        private bool hardReset;

        List<long> millisecondsEachCall;
        long timeElapsed = long.MinValue;
        List<int> pageNumbers;

        [NonSerialized]
        Stopwatch stopWatch;
        public VoteServiceObject(List<Vote> _votes, string _urlOfGame, List<Player> _players, List<Replacement> _replacements, List<string> _moderatorNames, List<int> _dayStartPostNumbers, List<List<Player>> _nightkilledPlayers, List<DayviggedPlayer> _dayviggedPlayers, List<ResurrectedPlayer> _resurrectedPlayers, string _colorCode, int _priorVCNumber, string _flavorText, string _deadlineCode, List<Vote> _votesByOverride, bool _isRestCall, ProdTimer _prodTimer, string _fontOverride, bool _areaTagsOn, string _dividerOverride, bool _showLLevel, bool _showZeroCountWagons, bool _hardReset)
        {
            //System settings
            isRestCall = _isRestCall;

            //UserInput
            players = _players;
            replacements = _replacements;
            dayStartPostNumbers = _dayStartPostNumbers;
            nightkilledPlayers = _nightkilledPlayers;
            flavorText = _flavorText;
            deadlineCode = "[countdown]" + _deadlineCode + "[/countdown]";
            priorVCNumber = _priorVCNumber;
            colorCode = _colorCode;
            fontOverride = _fontOverride;
            areaTagsOn = _areaTagsOn;
            dividerOverride = _dividerOverride;
            showLLevel = _showLLevel;
            showZeroCountWagons = _showZeroCountWagons;
            dayviggedPlayers = _dayviggedPlayers;
            resurrectedPlayers = _resurrectedPlayers;
            hardReset = _hardReset;

            if (_prodTimer == null)
            {
                prodTimer = new ProdTimer(0, 48, 0, 0);
            }
            else
            {
                prodTimer = _prodTimer;
            }

            List<Vote> regularVotes = _votes;
            votes = new List<Vote>();

            List<Vote> overridesCompleted = new List<Vote>();
            foreach (Vote vote in regularVotes)
            {
                bool voteWasOverriden = false;
                foreach (Vote overrideVote in _votesByOverride)
                {
                    if (vote.PostNumber == overrideVote.PostNumber)
                    {

                        votes.Add(overrideVote);
                        overridesCompleted.Add(overrideVote);
                        voteWasOverriden = true;
                    }


                }

                if (!voteWasOverriden)
                {
                    votes.Add(vote);
                }
            }

            foreach (Vote vote in _votesByOverride)
            {
                if (!overridesCompleted.Contains(vote))
                {
                    votes.Add(vote);
                }
            }

        }
        public VoteServiceObject(List<Vote> _votes, string _errorMessage = null)
        {
            votes = _votes;
            errorMessage = _errorMessage;
            isRestCall = false;
        }

        public VoteServiceObject(string _urlOfGame, List<Player> _players, List<Replacement> _replacements, List<string> _moderatorNames, List<int> _dayStartPostNumbers, List<List<Player>> _nightkilledPlayers, List<DayviggedPlayer> _dayviggedPlayers, List<ResurrectedPlayer> _resurrectedPlayers, string _colorCode, int _priorVCNumber, string _flavorText, string _deadlineCode, List<Vote> _votesByOverride, bool _isRestCall, ProdTimer _prodTimer, string _fontOverride, bool _areaTagsOn, string _dividerOverride, bool _showLLevel, bool _showZeroCountWagons, bool _hardReset)
        {
            //System settings
            isRestCall = _isRestCall;

            if (_prodTimer == null)
            {
                prodTimer = new ProdTimer(0, 48, 0, 0);
            }
            else
            {
                prodTimer = _prodTimer;
            }

            //User data input
            votes = new List<Vote>();
            //List<Vote> regularVotes = new List<Vote>();
            players = _players;
            replacements = _replacements;
            dayStartPostNumbers = _dayStartPostNumbers;
            nightkilledPlayers = _nightkilledPlayers;
            flavorText = _flavorText;
            deadlineCode = "[countdown]" + _deadlineCode + "[/countdown]";
            priorVCNumber = _priorVCNumber;
            colorCode = _colorCode;
            fontOverride = _fontOverride;
            areaTagsOn = _areaTagsOn;
            dividerOverride = _dividerOverride;
            showLLevel = _showLLevel;
            showZeroCountWagons = _showZeroCountWagons;
            dayviggedPlayers = _dayviggedPlayers;
            resurrectedPlayers = _resurrectedPlayers;
            hardReset = _hardReset;

           

            Posts = RunScrubLogic.RunScrubLogic.BuildPosts(_urlOfGame, players, _moderatorNames, replacements);
           
            //Handle replacements in core player object
            foreach(Replacement replacement in replacements)
            {
                replacement.performReplacement("", players);
            }

            List<Vote> regularVotes = new List<Vote>();

            string debugString = "";
            
            //Refresh post player connection. Because of rewrite this is needed.
            foreach(Post post in Posts)
            {
              
               

                //This is necessary because the reference to Players doesn't get updated because of the new ECS system.
                if (post.PlayerPosting != null)
                {
                    foreach(Player player in players)
                    {
                        if (player.Name.Equals(post.PlayerPosting.Name))
                        {
                            player.addPostNumber(post.PostNumber);
                            if (player.TimeOfLastPost < post.TimeOfPost)
                            {
                                player.TimeOfLastPost = post.TimeOfPost;
                            }

                            if (post.VoteInThisPost != null)
                            {
                                post.VoteInThisPost.updatePlayerReference(player);
                                regularVotes.Add(post.VoteInThisPost);
                            }

                            if (player.Name.Equals("Nauci"))
                            {
                                debugString = String.Join(",", player.PostNumbers.ToArray());
                            }
                        }


                       
                    }

                    
                }
                else
                {
                    UnityEngine.Debug.Log("Could not find player for post: " + post.PostNumber);
                }
               

                
                
            }

            

           

            List<Vote> overridesCompleted = new List<Vote>();
            foreach (Vote vote in regularVotes)
            {
                bool voteWasOverriden = false;
                foreach (Vote overrideVote in _votesByOverride)
                {
                    if (vote.PostNumber == overrideVote.PostNumber)
                    {

                        votes.Add(overrideVote);
                        overridesCompleted.Add(overrideVote);
                        voteWasOverriden = true;
                    }


                }

                if (!voteWasOverriden)
                {
                    votes.Add(vote);
                }
            }

            foreach (Vote vote in _votesByOverride)
            {
                if (!overridesCompleted.Contains(vote))
                {
                    votes.Add(vote);
                }
            }



        }


        public string VoteStringByPlayer(Player player)
        {
            List<Vote> specificPlayerVotes = new List<Vote>();
            foreach (Vote vote in votes)
            {
                try
                {
                    if (vote.PlayerVoting.Name.Equals(player.Name))
                    {
                        specificPlayerVotes.Add(vote);
                    }
                }
                catch
                {
                    //Do nothing??

                }
            }

            List<int> voteInts = new List<int>();
            foreach (Vote vote in specificPlayerVotes)
            {
                voteInts.Add(vote.PostNumber);
            }
            voteInts.Sort();
            //return string.Join(",", voteInts.ToArray());
            return JsonConvert.SerializeObject(specificPlayerVotes);
        }


        public string AllVotesString
        {
            get
            {
                return JsonConvert.SerializeObject(votes);
               
            }
        }

        public List<int> PageNumbersScraped { get { return pageNumbers; } }
        public long TimeElapsed
        {
            get
            {

                if (stopWatch != null)
                {
                    timeElapsed = stopWatch.ElapsedMilliseconds;
                    stopWatch.Stop();
                    stopWatch = null;
                    return timeElapsed;
                }
                else
                {
                    return timeElapsed;
                }

            }
        }
        public List<long> MillisecondsEachCall { get { return millisecondsEachCall; } }
        public string ErrorMessage { get { return errorMessage; } }
        public bool IsRestCall { get { return isRestCall; } }
        public List<Vote> Votes { get { return votes; } }
        public List<Player> Players { get { return players; } }
        public List<int> DayStartPostNumbers { get { return dayStartPostNumbers; } }
        public List<List<Player>> NightkilledPlayers { get { return nightkilledPlayers; } }
        public int PriorVCNumber { get { return priorVCNumber; } }
        public string FlavorText { get { return flavorText; } }
        public string DeadlineCode { get { return deadlineCode; } }
        public List<Replacement> Replacements { get { return replacements; } }
        public string ColorCode { get { return colorCode; } }
        public ProdTimer ProdTimer { get { return prodTimer; } }
        public string FontOverride { get { return fontOverride; } }
        public bool AreaTagsOn { get { return areaTagsOn; } }
        public string DividerOverride { get { return dividerOverride; } }
        public bool ShowLLevel { get { return showLLevel; } }
        public bool ShowZeroCountWagons { get { return showZeroCountWagons; } }
        public List<DayviggedPlayer> DayviggedPlayers { get { return dayviggedPlayers; } }
        public List<ResurrectedPlayer> ResurrectedPlayers { get { return resurrectedPlayers; } }
        public bool HardReset { get { return hardReset; } }
        public List<Post> Posts { get;  }
    }

}
