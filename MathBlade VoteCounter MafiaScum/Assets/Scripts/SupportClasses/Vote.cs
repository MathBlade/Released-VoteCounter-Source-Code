using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SupportClasses
{
    [Serializable]
    public class Vote : IComparable<Vote>
    {

        Player playerVoting;
        Player playerVoted;

        int postNumber;
        DateTime timestamp;
        bool isBoldVote;
        List<string> debugString;


        public Vote(Player _playerVoting, Player _playerVoted, int _postNumber, DateTime _timestamp, bool _isBoldVote, List<string> _debugString)
        {
            timestamp = _timestamp;
            playerVoted = _playerVoted;
            playerVoting = _playerVoting;

            postNumber = _postNumber;
            debugString = _debugString;

            isBoldVote = _isBoldVote;

        }

        public bool IsBoldVote { get { return isBoldVote; } }

        public int DayNumber { get; set; }

        public string buildDebugOutput()
        {
            string returnString = "";
            if (debugString == null)
            {
                return returnString;
            }
            foreach (string dString in debugString)
            {
                returnString = returnString + dString + "|";
            }

            int lastPipe = returnString.LastIndexOf("|");
            return returnString.Substring(0, lastPipe);
        }

        public DateTime Timestamp
        {
            get
            {
                return timestamp;
            }
        }

        public string PostBBCode
        {
            get
            {
                return "[post=" + PostNumber + "]" + postNumber + "[/post]";
            }
        }

        public int CompareTo(Vote other)
        {
            return postNumber.CompareTo(other.postNumber);
        }
        public Player PlayerVoted
        {
            get
            {
                return playerVoted;
            }
        }
        public Player PlayerVoting
        {
            get
            {
                return playerVoting;
            }
        }

        public int PostNumber
        {
            get
            {
                return postNumber;
            }
        }

        public void updatePlayerReference(Player playerInput)
        {
           playerVoting.PostNumbers = playerInput.PostNumbers;
        }
    }

}
