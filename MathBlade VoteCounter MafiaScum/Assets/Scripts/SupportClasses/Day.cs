using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SupportClasses
{
    [Serializable]
    public class Day : IComparable<Day>
    {

        int number;
        //string voteText;
        List<Vote> votesInDay;
        List<Player> deathsOvernight;
        List<Player> allPlayers;
        int postNumberStart;
        int postNumberEnd;
        DateTime firstPostTime;

        //Useful constants
        private const string COLOR_GREY_TAG = "[color=grey]";
        private static char LEFT_BRACKET = Convert.ToChar("[");
        private static char RIGHT_BRACKET = Convert.ToChar("]");
        private static char TILDE = Convert.ToChar("~");
        private const string CLOSE_COLOR_TAG = "[/color]";
        private const string OPEN_BOLD = "[b]";
        private const string CLOSE_BOLD = "[/b]";
        private const string COLOR_VOTES_FOR = "[color=#FF80FF]";
        private const string COLOR_VOTES_ON = "[color=blue]";
        private const string POST_OPEN = "[post=";
        private const string POST_CLOSE_TAG = "[/post]";

        //[color=grey][Wed May 03, 2017 12:52][/color] [b][color=#FF80FF]Nachomamma8[/color][/b] ~ [b][color=blue]TheRealGin-N-Tonic[/color][/b] [post=#9170080]7[/post]



        public Day(int _number, List<Player> _deathsOvernight, List<Player> _allPlayers, List<Vote> _votes, int _postNumberStart, DateTime _firstPostTime, int _postNumberEnd)
        {
            number = _number;
            //voteText = _voteText;

            deathsOvernight = _deathsOvernight;
            allPlayers = _allPlayers;
            //performDeathsOvernight ();

            votesInDay = _votes;
            postNumberStart = _postNumberStart;
            postNumberEnd = _postNumberEnd;
            firstPostTime = _firstPostTime;
            //votesInDay = parseVoteText ();
        }

        public void killLynchedPlayer(Player player)
        {
            if (player == null)
            {
                return;
            }
            foreach (Player aPlayer in allPlayers)
            {
                if (player.Name.Equals(aPlayer.Name))
                {

                    aPlayer.IsDead = true;
                    player.IsDead = true;
                }
            }

        }

        public List<Player> DeathsOvernight { get { return deathsOvernight; } }

        public void performDeathsOvernight()
        {
            if ((deathsOvernight == null) || (deathsOvernight.Count == 0))
            {
                return;
            }
            foreach (Player player in deathsOvernight)
            {


                foreach (Player aPlayer in allPlayers)
                {
                    if (player.Name.Equals(aPlayer.Name))
                    {
                        System.Console.WriteLine(player.Name + " HAS DIED");
                        aPlayer.IsDead = true;
                        player.IsDead = true;
                    }
                }


            }
        }



        public int Number
        {
            get
            {
                return number;
            }
        }

        public List<Vote> Votes
        {
            get
            {
                return votesInDay;
            }
        }

        public static List<Vote> parseVoteText(List<Player> playersAlive, string voteText, List<Replacement> replacements)
        {
            List<Vote> votes = new List<Vote>();

            if (voteText == null)
            {
                return votes;
            }
            else
            {
                voteText = voteText.Replace("color*", "color");
            }
            List<Player> players = playersAlive;


            List<string> debugString = new List<string>();
            string voteTextRemaining = voteText;

            if (voteTextRemaining.Length == 0 || voteTextRemaining.IndexOf(COLOR_GREY_TAG) < 0)
            {
                return votes;
            }


            do
            {
                //Clean up from last vote
                int newVoteLocation = voteTextRemaining.IndexOf(COLOR_GREY_TAG);
                voteTextRemaining = voteTextRemaining.Substring(newVoteLocation);

                //Check if this is a VOTE
                newVoteLocation = voteTextRemaining.IndexOf(COLOR_GREY_TAG);
                int tildeLocation = voteTextRemaining.IndexOf(TILDE);
                int postCloseTagLocation = voteTextRemaining.IndexOf(POST_CLOSE_TAG);

                if (tildeLocation > postCloseTagLocation)
                {

                    do
                    {

                        voteTextRemaining = voteTextRemaining.Substring(postCloseTagLocation + POST_CLOSE_TAG.Length);

                        tildeLocation = voteTextRemaining.IndexOf(TILDE);
                        postCloseTagLocation = voteTextRemaining.IndexOf(POST_CLOSE_TAG);


                        //Debug.Log("VOTE TEXT REMAINING INNER: " + voteTextRemaining);
                    } while ((tildeLocation > postCloseTagLocation));
                }

                tildeLocation = voteTextRemaining.IndexOf(TILDE);
                if (tildeLocation == -1) //Check needed if last line is a replacement.
                {
                    return votes;
                }
                voteTextRemaining = voteTextRemaining.Substring(newVoteLocation);

                int startInt = voteTextRemaining.IndexOf(COLOR_GREY_TAG) + COLOR_GREY_TAG.Length;
                int endInt = voteTextRemaining.IndexOf(RIGHT_BRACKET, startInt + COLOR_GREY_TAG.Length);



                //string dateString = voteTextRemaining.Substring (startInt + 1, endInt - COLOR_GREY_TAG.Length - 1);
                string dateString = voteTextRemaining.Substring(startInt + 1, endInt - startInt - 1);

                //
                DateTime dateTime = DateTime.ParseExact(dateString.Trim(), "MMM dd, yyyy HH:mm",
                                        System.Globalization.CultureInfo.InvariantCulture);
                //Debug.Log ("DATE TIME: " + dateTime.ToString ());
                voteTextRemaining = voteTextRemaining.Substring(voteTextRemaining.IndexOf(OPEN_BOLD) + OPEN_BOLD.Length);
                string playerNameVotedFor = voteTextRemaining.Substring(COLOR_VOTES_FOR.Length, voteTextRemaining.IndexOf(CLOSE_COLOR_TAG) - COLOR_VOTES_FOR.Length);
                //Debug.Log ("Player Name:" + playerNameVotedFor);
                voteTextRemaining = voteTextRemaining.Substring(voteTextRemaining.IndexOf(COLOR_VOTES_ON) + COLOR_VOTES_ON.Length);
                string playerVoted = voteTextRemaining.Substring(0, voteTextRemaining.IndexOf(CLOSE_COLOR_TAG));

                if (playerVoted.Contains(OPEN_BOLD))
                {
                    playerVoted = playerVoted.Substring((OPEN_BOLD.Length * 2), playerVoted.Length - (OPEN_BOLD.Length * 2) - (CLOSE_BOLD.Length * 2));
                }

                //Debug.Log ("Player Voted:" + playerVoted);
                voteTextRemaining = voteTextRemaining.Substring(voteTextRemaining.IndexOf(CLOSE_COLOR_TAG));
                string postnew = voteTextRemaining.Substring(voteTextRemaining.IndexOf(POST_OPEN), voteTextRemaining.IndexOf(POST_CLOSE_TAG) - POST_OPEN.Length);
                postnew = postnew.Trim();
                if (!postnew.EndsWith(RIGHT_BRACKET.ToString()))
                {
                    postnew = voteTextRemaining.Substring(voteTextRemaining.IndexOf(POST_OPEN), voteTextRemaining.IndexOf(POST_CLOSE_TAG) - POST_OPEN.Length + CLOSE_BOLD.Length);
                }
                postnew = postnew.Trim();


                //Debug.Log ("POSTnew:" + postnew);
                //Debug.Log("LBINT:" + postnew.IndexOf(RIGHT_BRACKET));
                //Debug.Log("RBINT:" + postnew.IndexOf(LEFT_BRACKET, postnew.IndexOf(RIGHT_BRACKET)));
                //int postNumber = 0;
                int postNumber = Int32.Parse(postnew.Substring(postnew.IndexOf(RIGHT_BRACKET) + 1, postnew.IndexOf(LEFT_BRACKET, postnew.IndexOf(RIGHT_BRACKET)) - postnew.IndexOf(RIGHT_BRACKET) - 1));
                //Debug.Log("POST NUMBER:" + postnew.Substring(postnew.IndexOf(RIGHT_BRACKET) + 1, postnew.IndexOf(LEFT_BRACKET, postnew.IndexOf(RIGHT_BRACKET)) - postnew.IndexOf(RIGHT_BRACKET) -1)  );

                postnew = "[post]" + postNumber + "[/post]";

                debugString = new List<string>();
                debugString.Add(playerNameVotedFor);
                debugString.Add(playerVoted);
                debugString.Add(postnew);
                debugString.Add("" + postNumber);
                debugString.Add(dateTime.ToString());

                Player voter = Player.FindPlayerByNameUserAidReplacementsLoop(players, playerNameVotedFor, replacements);
                if (voter == null)
                {
                    System.Console.WriteLine("could not find PLAYER:|" + playerNameVotedFor + "|");
                    string errorMessage = "could not find PLAYER:|" + playerNameVotedFor + "|";

                    throw new ArgumentException(errorMessage);

                }

                Player target = Player.FindPlayerByNameUserAidReplacementsLoop(players, playerVoted, replacements);
                if (target == null)
                {
                    target = new Player("UNVOTE: ");
                }

                Vote newVote = new Vote(Player.FindPlayerByNameUserAidReplacementsLoop(players, playerNameVotedFor, replacements), target, postNumber, dateTime, false, debugString);
                //Debug.Log ("TEXT NOW:" + voteTextRemaining);
                votes.Add(newVote);
            } while (voteTextRemaining.IndexOf(COLOR_GREY_TAG) > -1);




            return votes;
        }


        public int CompareTo(Day other)
        {
            return number.CompareTo(other.number);
        }

        public int PostNumberStart { get { return postNumberStart; } }

        public int PostNumberEnd { get { return postNumberEnd; } }

        public DateTime FirstPostTime { get { return firstPostTime; } }
    }
}
