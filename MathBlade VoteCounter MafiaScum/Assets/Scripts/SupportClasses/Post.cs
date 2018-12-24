using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Assets.Scripts.SupportClasses
{
    [Serializable]
    public class Post
    {
        public Post(int postNumber, Player playerPosting, DateTime timeOfPost, string bbCode)
        {
            PostNumber = postNumber;
            PlayerPosting = playerPosting;
            TimeOfPost = timeOfPost;          
            BBCode = bbCode;

        }

        public void ExtractVoteFromPost(List<Player> players, HtmlNode content, List<Replacement> replacements)
        {
            VoteInThisPost = RunScrubLogic.RunScrubLogic.GetVoteFromContent(this, content,players, replacements);
        }

        public int PostNumber { get; set; }
        public Player PlayerPosting { get; set; }
        public DateTime TimeOfPost { get; set; }       
        public string BBCode { get; set; }
        public Vote VoteInThisPost { get; set; }
    }
    

}
