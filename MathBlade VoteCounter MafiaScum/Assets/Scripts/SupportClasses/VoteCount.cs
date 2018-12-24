using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Support_Scripts;
using UnityEngine;

namespace Assets.Scripts.SupportClasses
{
    [Serializable]
    public class VoteCount
    {



        int dayNumber;
        List<Wagon> wagons;
        //List<Player> playersNotVoting;
        int maxThreshold;
        bool isHammered;
        List<Player> playersInVoteCount;
        List<Day> days;
        string latestVCOutput;
        bool hasVotes;
        private bool isRestCall;


        public VoteCount(int _dayNumber, List<Player> players, int _maxThreshold, List<Day> _days, bool _isRestCall)
        {
            dayNumber = _dayNumber;
            wagons = new List<Wagon>();
            //playersNotVoting = new List<Player> ();
            maxThreshold = _maxThreshold;
            isHammered = false;
            isRestCall = _isRestCall;

           /*foreach (Player player in players)
            {
                if (player.IsAlive)
                {
                    player.PlayerCurrentlyVoting = null;
                    player.PostNumberOfVote = -1;
                    //playersNotVoting.Add (player);
                }
            }*/
            playersInVoteCount = players;
            days = _days;
        }

        public bool HasVotes { get { return hasVotes; } }

        public string LatestVCOutput { get { return latestVCOutput; } }

        public int DayNumber
        {
            get
            {
                return dayNumber;
            }
        }

        public bool IsHammered
        {
            get
            {
                return isHammered;
            }
        }

        public int MaxThreshold
        {
            get
            {
                return maxThreshold;
            }
            set
            {
                maxThreshold = value;
            }
        }

        public List<Wagon> Wagons
        {
            get
            {
                return wagons;
            }
        }

        public void debugDumpAllWagons(int postDayStartedOn, int postDayEndedOn)
        {
            foreach (Wagon wagon in wagons)
            {
                if (wagon.PlayersVoting.Count > 0)
                {
                    System.Console.WriteLine(wagon.ToString());
                }
            }
            System.Console.WriteLine("Players Not Voting: " + getPlayersNotVotingString(postDayStartedOn, postDayEndedOn));
        }

        private string getPlayersNotVotingString(int postDayStartedOn, int postDayEndedOn)
        {
            //string listOfPlayers = "";
            /*if (playersNotVoting.Count == 0) {
                return "";
            }
            foreach (Player player in playersNotVoting) {
                listOfPlayers = listOfPlayers + player.Name + ", ";
            }*/

            List<string> displayStrings = new List<String>();

            foreach (Player player in playersInVoteCount)
            {
                if ((player.PlayerCurrentlyVoting == null) && (player.IsAlive))
                {
                    if (player.PostNumberOfVote > -1)
                    {
                        displayStrings.Add(" [post=" + player.PostNumberOfVote + "]" + player.Name + "[/post]" + "(" + player.getNumberOfPostsInDay(postDayStartedOn, postDayEndedOn) + ")");
                    }
                    else
                    {
                        displayStrings.Add(" " + player.Name + "(" + player.getNumberOfPostsInDay(postDayStartedOn, postDayEndedOn) + ")");
                    }
                }

            }

            return string.Join(",", displayStrings.ToArray());

            /*int lastCommaSpot = listOfPlayers.LastIndexOf (", ");
            if (lastCommaSpot < 0)
                return listOfPlayers;
            else 
                return listOfPlayers.Substring (0, lastCommaSpot);*/
        }

        public void doVote(Vote vote, int maxThreshold, bool lSort, bool sortBy)
        {
            if (vote != null && !hasVotes)
            {
                hasVotes = true;
            }

            Player playerTarget = null;
            try
            {
                Player playerVoting = null;
                Player playerVotingFromVote = vote.PlayerVoting;
                foreach(Player player in playersInVoteCount)
                {
                    if (player.Name.Equals(playerVotingFromVote.Name))
                    {
                        playerVoting = player;
                        break;
                    }
                }
                if (playerVoting == null)
                {
                    Debug.Log("Couldn't find player for post number: " + vote.PostNumber);
                    return;
                }
                //Debug.Log ("VOTE NUMBER: " + vote.PostNumber + " PLAYER VOTING NAME:|" + playerVoting.Name + "|WHERE VOTE IS AT:|" + ((playerVoting.PlayerCurrentlyVoting == null) ? "NULL" : playerVoting.PlayerCurrentlyVoting.Name) + "|");
                Player playerTargetFromVote = vote.PlayerVoted;
                if (playerTargetFromVote != null)
                {
                    foreach (Player player in playersInVoteCount)
                    {
                        if (player.Name.Equals(playerTargetFromVote.Name))
                        {
                            playerTarget = player;
                            break;
                        }
                    }
                }

                Wagon currentWagon = null;
                Wagon newWagon = null;


                //Player is already voting there.
                if ((playerVoting.PlayerCurrentlyVoting != null) && ((playerTarget != null) && (playerVoting.PlayerCurrentlyVoting.Name.Equals(playerTarget.Name))))
                {
                    //Debug.Log("ALREADY VOTING THERE.");


                    return;
                }
                else
                {
                    //Debug.Log("HI");
                    //Unvote required
                    if ((playerTarget == null) || (playerTarget.Name.ContainsIgnoreCase("Unvote")))
                    {

                        currentWagon = getWagonByTarget(playerVoting.PlayerCurrentlyVoting);
                        if (currentWagon != null)
                        {
                            //currentWagon.removeVote (playerVoting);
                            currentWagon.removeVote(vote);
                        
                            //addPlayerNotVoting (playerVoting);
                            playerVoting.PlayerCurrentlyVoting = null;
                            playerVoting.PostNumberOfVote = vote.PostNumber;
                            return;
                        }
                        playerVoting.PlayerCurrentlyVoting = null;
                        playerVoting.PostNumberOfVote = vote.PostNumber;
                        return;
                    }

                    //This is a swap of vote.
                    if (playerVoting.PlayerCurrentlyVoting != null)
                    {
                        //Debug.Log ("PLAYER VOTING NAME:|" + playerVoting.Name + "| VOTE PLAYER VOTING NAME:|" + vote.PlayerVoting.Name + "|");
                        //Debug.Log("SWAP OF VOTE: ");
                        currentWagon = getWagonByTarget(playerVoting.PlayerCurrentlyVoting);
                        if (currentWagon != null)
                        {
                            //currentWagon.removeVote (playerVoting);
                            currentWagon.removeVote(vote);
                            playerVoting.PlayerCurrentlyVoting = null;
                            playerVoting.PostNumberOfVote = vote.PostNumber;
                            //addPlayerNotVoting (playerVoting);
                        }
                    }

                    //Debug.Log("ADD THAT VOTE");
                    //Add that vote
                    newWagon = getWagonByTarget(playerTarget);
                    if (newWagon == null)
                    {
                        if (playerTarget != null && playerTarget.IsAlive)
                        {
                            Wagon createdWagon = new Wagon(maxThreshold,lSort,sortBy);
                            createdWagon.addVote(vote);
                            playerVoting.PlayerCurrentlyVoting = vote.PlayerVoted;
                            playerVoting.PostNumberOfVote = vote.PostNumber;
                            wagons.Add(createdWagon);

                        }
                        else if (playerTarget == null)
                        {

                            System.Console.WriteLine("Wagon creation error. Check your hammer votes for vote:[color][u][b] is the proper order. " + vote.PostNumber);
                            throw new Exception("PLAYER TARGET WAS NULL!!!!!!! " + vote.buildDebugOutput());
                        }
                    }
                    else if (playerTarget.IsAlive)
                    {
                        //newWagon.addVote (playerVoting);
                        newWagon.addVote(vote);
                        playerVoting.PlayerCurrentlyVoting = vote.PlayerVoted;
                        playerVoting.PostNumberOfVote = vote.PostNumber;

                        //removePlayerNotVoting (playerVoting);
                        isHammered = newWagon.IsHammered;
                        if (isHammered && playerTarget != Wagon.NO_LYNCH_PLAYER)
                        {

                            days[vote.DayNumber - 1].killLynchedPlayer(playerTarget);
                            if (playerTarget != null)
                            {


                                foreach (Player aPlayer in playersInVoteCount)
                                {
                                    if (playerTarget.Name.Equals(aPlayer.Name))
                                    {

                                        aPlayer.IsDead = true;
                                        playerTarget.IsDead = true;
                                    }
                                }
                            }
                            System.Console.WriteLine("Player " + playerTarget.Name + " was hammered in post " + vote.PostNumber + ".");

                        }
                    }

                }
            }
            catch (NullReferenceException e)
            {
                System.Console.WriteLine(vote.buildDebugOutput() + "Player Target:|" + (playerTarget != null ? playerTarget.Name : "<null>") + "|");
                System.Console.WriteLine(e.ToString());
                System.Console.WriteLine("A player was missing from post " + vote.PostNumber + ". Please check your replacements.  Debug String: " + vote.buildDebugOutput());
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                System.Console.WriteLine("An unknown error occurred in post number" + vote.PostNumber + ". Please check formatting of file.");
            }

        }



        private Wagon getWagonByTarget(Player playerTarget)
        {
            if (playerTarget == null)
            {
                return null;
            }
            foreach (Wagon wagon in wagons)
            {
                if (wagon.PlayerBeingVoted.Name.Equals(playerTarget.Name))
                {
                    return wagon;
                }
            }
            return null;
        }

        public void buildLatest(int dayNumber, int priorVCNumber, string flavorBBCode, string deadlineCode, int playersKilledOvernight, string colorCode, int firstPostOfDay, int lastPostOfDay, ProdTimer prodTimer, string fontOverride, bool areaTagsOn, string dividerOverride, bool showLLevel, bool showZeroCountWagons)
        {

            string displayString = ((fontOverride != null) ? "[font=" + fontOverride + "]" : "") + (areaTagsOn ? "[area]" : "") + "[b][u][size=150][color=" + colorCode + "]" + "Votecount " + dayNumber + "." + (priorVCNumber + 1) + "[/color][/size][/u][/b]" + History.NEW_LINE_HERE;
            wagons.Sort((a, b) => -1 * a.CompareTo(b));
            bool firstOne = true;
            bool AWagonWasHammered = false;
            Wagon leadWagon = null;
            foreach (Wagon wagon in wagons)
            {
                if ((wagon.PlayersVoting.Count > 0) || (showZeroCountWagons && wagon.PlayerBeingVoted != null && wagon.PlayerBeingVoted.IsAlive && !wagon.PlayerBeingVoted.IsTreestumped && !wagon.PlayerBeingVoted.IsGunner))
                {
                    if (firstOne == true)
                    {
                        displayString = displayString + "[i]";
                        leadWagon = wagon;
                    }
                    displayString = displayString + "[b]" + wagon.PlayerBeingVoted.Name + "(" + wagon.PlayersVotingAndNotTreestumpedCount + ")[/b]" + ((dividerOverride != null) ? " " + dividerOverride + " " : " ~ ") + wagon.playersVotingCommaDelimitedList(firstPostOfDay, lastPostOfDay);
                    if (firstOne == true)
                    {
                        displayString = displayString + "[/i]" + ((wagon.IsHammered ? " -- HAMMER " : (showLLevel ? " L - " + wagon.L_Level : ""))) + History.NEW_LINE_HERE;
                    }
                    else
                    {
                        displayString = displayString + (showLLevel ? " L - " + wagon.L_Level : "") + History.NEW_LINE_HERE;
                    }
                    firstOne = false;
                }
                if (!AWagonWasHammered)
                {
                    AWagonWasHammered = wagon.IsHammered;
                }
            }

            int alivePlayersAtVoteCountStart = playersAliveNoTreestumpsNoGunners() + ((AWagonWasHammered == true) ? 1 : 0);

            /* displayString = displayString + History.NEW_LINE_HERE;
             List<string> playersDead = new List<string>();
             foreach(Player player in playersInVoteCount)
             {
                 if (player.IsDead)
                 {
                     playersDead.Add(player.Name);
                 }
             }
             displayString = displayString + "Players dead: " + String.Join(",", playersDead.ToArray());*/

            displayString = displayString + History.NEW_LINE_HERE;
            displayString = displayString + History.NEW_LINE_HERE;
            string playersNotVotingString = getPlayersNotVotingString(firstPostOfDay, lastPostOfDay);

            //Not voting failsafe.
            string[] playersNotVotingStringArray = playersNotVotingString.Length > 0 ? playersNotVotingString.Split(',') : null;
            List<string> playersNotVotingList = playersNotVotingStringArray != null ? new List<string>(playersNotVotingStringArray) : new List<string>();
            if (AWagonWasHammered == true && leadWagon != null)
            {
                Player playerHammered = leadWagon.PlayerBeingVoted;
                if (playerHammered.PlayerCurrentlyVoting == null)
                {
                    bool addPlayerToList = true;
                    foreach (string playerName in playersNotVotingStringArray)
                    {
                        if (playerName.Equals(playerHammered.Name))
                        {
                            addPlayerToList = false;
                            break;
                        }
                    }

                    if (addPlayerToList)
                    {
                        playersNotVotingList.Add(" " + playerHammered.Name + "(" + playerHammered.getNumberOfPostsInDay(firstPostOfDay, lastPostOfDay) + ")");
                        playersNotVotingStringArray = playersNotVotingList.ToArray();
                        playersNotVotingString = String.Join(",", playersNotVotingStringArray);
                    }
                }


            }

            string countNotVotingString = playersNotVotingStringArray != null ? playersNotVotingStringArray.Length + "" : "0";
            displayString = displayString + "Not Voting (" + countNotVotingString + "): " + playersNotVotingString;
            displayString = displayString + History.NEW_LINE_HERE;
            displayString = displayString + History.NEW_LINE_HERE;
            displayString = displayString + "With " + alivePlayersAtVoteCountStart + " alive it takes " + maxThreshold + " to lynch." + History.NEW_LINE_HERE;


            displayString = displayString + History.NEW_LINE_HERE;
            displayString = displayString + "Day " + dayNumber + " deadline is in " + deadlineCode + (areaTagsOn ? "[/area]" : "") + (fontOverride != null ? "[/font]" : "") + History.NEW_LINE_HERE + History.NEW_LINE_HERE;



            displayString = displayString + "[area=MOD REMINDERS]" + History.NEW_LINE_HERE;

            DateTime now = DateTime.UtcNow.AddHours(-6);
            bool aProdIsNeeded = false;

            DateTime timeToCountBackFrom = now;
            if (AWagonWasHammered)
            {

                timeToCountBackFrom = leadWagon.TimeOfLastVote;

            }

            if (timeToCountBackFrom - days[days.Count - 1].FirstPostTime > new TimeSpan(prodTimer.Days, prodTimer.Hours, prodTimer.Minutes, prodTimer.Seconds))
            {


                foreach (Player player in playersInVoteCount)
                {
                    if (player.IsAlive && player.TimeOfLastPost != null && player.TimeOfLastPost.Year > 1986)
                    {
                        DateTime lastPostPlusProd = player.TimeOfLastPost.AddDays(prodTimer.Days).AddHours(prodTimer.Hours).AddMinutes(prodTimer.Minutes).AddSeconds(prodTimer.Seconds);
                        if (player.IsAlive && lastPostPlusProd < timeToCountBackFrom)
                        {

                            TimeSpan timeSinceLastPost = (timeToCountBackFrom - player.TimeOfLastPost);
                            displayString = displayString + player.Name + " needs a prod. The last post was at: " + player.TimeOfLastPost.ToString() + " which was " + timeSinceLastPost.Days + " days " + timeSinceLastPost.Hours + " hours " + timeSinceLastPost.Minutes + " minutes " + timeSinceLastPost.Seconds + " seconds ago." + History.NEW_LINE_HERE;
                            aProdIsNeeded = true;
                        }
                    }
                    else if (player.TimeOfLastPost.Year == 1986)
                    {
                        displayString = displayString + player.Name + " has never posted. Please verify player interest. " + History.NEW_LINE_HERE;
                        aProdIsNeeded = true;
                    }
                }

            }
            if (!aProdIsNeeded)
            {
                displayString = displayString + "NONE" + History.NEW_LINE_HERE;
            }


            displayString = displayString + "[/area]" + History.NEW_LINE_HERE;

            displayString = displayString + "[area=FLAVOR]" + flavorBBCode + "[/area]";

            latestVCOutput = displayString.Replace(History.NEW_LINE_HERE, System.Environment.NewLine + ((isRestCall) ? "" : ""));



        }

        private int playersAliveNoTreestumps()
        {
            int playersAliveNoTreestump = 0;
            foreach (Player player in playersInVoteCount)
            {
                if ((player.IsAlive == true) && !player.IsTreestumped && (!player.IsDead))
                {
                    playersAliveNoTreestump++;
                }
            }

            return playersAliveNoTreestump;
        }

        private int playersAliveNoTreestumpsNoGunners()
        {
            int playersAliveNoTreestump = 0;
            foreach (Player player in playersInVoteCount)
            {
                if ((player.IsAlive == true) && !player.IsTreestumped && !player.IsGunner && (!player.IsDead))
                {
                    playersAliveNoTreestump++;
                }
            }

            return playersAliveNoTreestump;
        }

        private int playersAlive()
        {
            int playersAlive = 0;
            foreach (Player player in playersInVoteCount)
            {
                if ((player.IsAlive == true) && (!player.IsDead))
                {
                    playersAlive++;
                }
            }

            return playersAlive;
        }
    }
}
