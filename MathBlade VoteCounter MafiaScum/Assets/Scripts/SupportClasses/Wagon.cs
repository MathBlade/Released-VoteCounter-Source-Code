using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SupportClasses
{
    [Serializable]
    public class Wagon : IComparable<Wagon>
    {
        public const string NO_LYNCH = "No Lynch";
        public static readonly Player NO_LYNCH_PLAYER = new Player(NO_LYNCH);



      

        public Wagon(int _maxThreshold, bool _lSort, bool _alphaSort)
        {


            PlayersVoting = new List<Player>();


            PostNumbers = new List<int>();
           

            PostBBCodeList = new List<string>();
           

            UnvoteList = new List<bool>();
           

            MaxThreshold = _maxThreshold;
            IsHammered = false;

          

            AllVotesRelevantToWagon = new List<Vote>();
            


            LSort = _lSort;
            AlphaSort = _alphaSort;
           
        }



        public void addVote(Vote vote)
        {
            PlayersVoting.Add(vote.PlayerVoting);
            if (PlayerBeingVoted == null)
            {
                PlayerBeingVoted = vote.PlayerVoted;              
            }
            vote.PlayerVoting.PlayerCurrentlyVoting = vote.PlayerVoted;
            

            if (!PostNumbers.Contains(vote.PostNumber))
            {
                vote.PlayerVoting.PostNumberOfVote = vote.PostNumber;
                PostNumbers.Add(vote.PostNumber);
                PostBBCodeList.Add(vote.PostBBCode);
                UnvoteList.Add(false);

                if (vote.Timestamp > MaxTimeStamp)
                {
                    MaxTimeStamp = vote.Timestamp;
                }
                if (vote != null)
                {
                    AllVotesRelevantToWagon.Add(vote);
                }
            }
            else
            {
                UnityEngine.Debug.Log("Post data got corrupted.");
            }

            if (L_Level == 1 && PlayerBeingVoted.IsHated)
            {
                IsHammered = true;
            }
            else if (L_Level == 0 && !PlayerBeingVoted.IsLoved)
            {
                IsHammered = true;
            }
            else if (L_Level == -1 && PlayerBeingVoted.IsLoved)
            {
                IsHammered = true;
            }
        }

        public void removeVote(Vote vote)
        {
            List<Player> playersNowVoting = new List<Player>();
            foreach (Player playerVoting in PlayersVoting)
            {

                if (playerVoting.Name.Equals(vote.PlayerVoting.Name))
                {
                    if (!PostNumbers.Contains(vote.PostNumber))
                    {
                        PostNumbers.Add(vote.PostNumber);
                        PostBBCodeList.Add(vote.PostBBCode);
                        UnvoteList.Add(true);
                        if (vote.Timestamp > MaxTimeStamp)
                        {
                            MaxTimeStamp = vote.Timestamp;
                        }
                        if (vote != null)
                        {
                            AllVotesRelevantToWagon.Add(vote);
                        }
                    }


                }
                else
                {
                    playersNowVoting.Add(playerVoting);

                }
            }
            //l_Level = maxThreshold - playersNowVoting.Count;
            PlayersVoting = playersNowVoting;
        }

        public string ToHistoricalDisplayString(bool complexOn)
        {
            string complexOffString = " " + GetDisplayStringForSimplePostCount();
            string complexOnString = " [ " + voteBBCountsRelevantToThisWagon() + " ]";
            string wagonDataString = complexOn ? complexOnString : complexOffString;

            return "[b] [color=blue]" + PlayerBeingVoted.Name + " (" + PlayersVotingAndNotTreestumpedCount + ")" + "[/color][/b] ~ [color=#FF80FF]" + playersVotingCommaDelimitedList() + "[/color]" + wagonDataString;


        }
        private string voteBBCountsRelevantToThisWagon()
        {
            string listOfVotes = "";
            if (PostNumbers.Count == 0)
            {
                return "";
            }
            string modifiedBBCode = "";
            int indexOf = -1;

            int rightBracketIndex = -1;
            foreach (string bbcode in PostBBCodeList)
            {
                modifiedBBCode = bbcode;
                indexOf = PostBBCodeList.IndexOf(bbcode);
                if (UnvoteList[indexOf] == true)
                {
                    rightBracketIndex = modifiedBBCode.IndexOf("]") + 1;
                    //Debug.Log ("Post number: " + postNumbers [indexOf]);
                    //Debug.Log ("Modified BBCode:|" + modifiedBBCode + "|");
                    modifiedBBCode = modifiedBBCode.Substring(0, rightBracketIndex) + "[strike]" + modifiedBBCode.Substring(rightBracketIndex, modifiedBBCode.Length - rightBracketIndex - "[/post]".Length) + "[/strike][/post]";

                    //modifiedBBCode = modifiedBBCode.Substring (0, rightBracketIndex) + "[strike]" + modifiedBBCode.Substring (rightBracketIndex, modifiedBBCode.Length - rightBracketIndex - "[/url]".Length) + "[/strike][/url]";
                }
                listOfVotes = listOfVotes + modifiedBBCode + ", ";
            }
            int lastCommaSpot = listOfVotes.LastIndexOf(", ");
            return listOfVotes.Substring(0, lastCommaSpot);
        }

        private string GetDisplayStringForSimplePostCount()
        {
            bool lastVoteIsUnvote = UnvoteList[(UnvoteList.Count - 1)];
            int postThatMadeWagon = GetPostThatMadeWagon();
            bool distinctPosts = (postThatMadeWagon != PostNumbers[(UnvoteList.Count - 1)]);

            if (postThatMadeWagon > 0)
            {

                return PostBBCodeList[PostNumbers.IndexOf(postThatMadeWagon)] + ((lastVoteIsUnvote && distinctPosts) ? "(" + PostBBCodeList[(UnvoteList.Count - 1)] + ")" : "");
            }
            else
            {
                return "ERROR -- MATHBLADE FIX THIS!";
            }
        }

        private int GetPostThatMadeWagon()
        {
            try
            {
                string playerUnvoteName = null;
                string lastPlayerOnWagon = PlayersVoting[PlayersVoting.Count - 1].Name;
                string playerBeingVotedName = PlayerBeingVoted.Name;
                Vote relevantVote = null;

                for (int i = (UnvoteList.Count - 1); i > -1; i--)
                {
                    if (UnvoteList[i] != true)
                    {

                        relevantVote = getVoteByPostNumber(PostNumbers[i]);
                        playerUnvoteName = relevantVote.PlayerVoting.Name;
                        if (playerUnvoteName == lastPlayerOnWagon)
                        {
                            return PostNumbers[i];
                        }
                    }

                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log("Bad Post Data.");
            }

            return -1;
        }
        private Vote getVoteByPostNumber(int postNumber)
        {
            foreach (Vote vote in AllVotesRelevantToWagon)
            {
                if (vote.PostNumber == postNumber)
                {
                    return vote;
                }
            }

            return null;
        }


        public override string ToString()
        {
            return PlayerBeingVoted.Name + " ~ " + playersVotingCommaDelimitedList() + " [ " + voteCountsRelevantToThisWagon() + "]";
        }


        public string playersVotingCommaDelimitedList()
        {
            return playersVotingCommaDelimitedList(int.MinValue, int.MaxValue);
        }

        public string playersVotingCommaDelimitedList(int postDayStartedOn, int postDayEndedOn)
        {

            if (PlayersVoting.Count == 0)
            {
                return "";
            }

            List<string> displayStrings = new List<String>();
            foreach (Player player in PlayersVoting)
            {
                if (player.PostNumberOfVote > 0)
                {
                    displayStrings.Add(" [post=" + player.PostNumberOfVote + "]" + player.Name + "[/post]" + ((postDayStartedOn > int.MinValue) ? "(" + player.getNumberOfPostsInDay(postDayStartedOn, postDayEndedOn) + ")" : ""));
                }
                else
                {
                    displayStrings.Add(" " + player.Name + (postDayStartedOn > int.MinValue ? "(" + player.getNumberOfPostsInDay(postDayStartedOn, postDayEndedOn) + ")" : ""));

                }
            }

            return string.Join(",", displayStrings.ToArray());

        }

        private string voteCountsRelevantToThisWagon()
        {
            string listOfVotes = "";
            if (PostNumbers.Count == 0)
            {
                return "";
            }
            foreach (int integer in PostNumbers)
            {
                listOfVotes = listOfVotes + integer + ", ";
            }
            int lastCommaSpot = listOfVotes.LastIndexOf(", ");
            return listOfVotes.Substring(0, lastCommaSpot);
        }


        public int CompareTo(Wagon other)
        {

            if (LSort)
            {
                if (L_Level != other.L_Level)
                {
                    return (-1 * L_Level.CompareTo(other.L_Level));
                }
            }

            if (AlphaSort)
            {
                if (!(PlayerBeingVoted.Name.Equals(other.PlayerBeingVoted.Name)))
                {
                    return string.Compare(PlayerBeingVoted.Name, other.PlayerBeingVoted.Name);
                }

                return (MaxTimeStamp.CompareTo(other.MaxTimeStamp));
            }
            else
            {


                if (MaxTimeStamp.CompareTo(other.MaxTimeStamp) != 0)
                {
                    return (MaxTimeStamp.CompareTo(other.MaxTimeStamp));
                }
                else
                {
                    return (GetMaxPostNumber().CompareTo(other.GetMaxPostNumber()));
                }

            }



        }


        public int GetMaxPostNumber()
        {
            if (PostNumbers.Count == 0)
            {
                return -1;
            }
            else
            {
                int maxPostNumber = 0;
                foreach (int postNumber in PostNumbers)
                {
                    if (postNumber > maxPostNumber)
                    {
                        maxPostNumber = postNumber;
                    }
                }


                return maxPostNumber;
            }
        }



        public int L_Level
        {
            get
            {
                return MaxThreshold - PlayersVotingAndNotTreestumpedCount;
            }
        }

        public int PlayersVotingAndNotTreestumpedCount
        {
            get
            {
                int i = 0;
                List<Player> playersVotingAndNotTreestumped = new List<Player>();
                foreach (Player player in PlayersVoting)
                {
                    if (!player.IsTreestumped)
                    {
                        i++;
                    }
                }
                return i;
            }
        }

        public DateTime TimeOfLastVote
        {
            get
            {
                DateTime time = new DateTime(1986, 3, 14);
                foreach (Vote vote in this.AllVotesRelevantToWagon)
                {
                    if (vote.Timestamp > time)
                    {
                        time = vote.Timestamp;
                    }
                }

                return time;
            }
        }

        public Player PlayerBeingVoted { get; private set; }
        public List<Player> PlayersVoting { get; private set; }
        public bool IsHammered { get; private set; }
        public List<int> PostNumbers { get; private set; }
        public List<String> PostBBCodeList { get; private set; }
        public List<bool> UnvoteList { get; private set; }
        public int MaxThreshold { get; private set; }
        public List<Vote> AllVotesRelevantToWagon { get; private set; }
        public bool LSort { get; private set; }
        public bool AlphaSort { get; private set; }
        public DateTime MaxTimeStamp { get; private set; }



    }

}