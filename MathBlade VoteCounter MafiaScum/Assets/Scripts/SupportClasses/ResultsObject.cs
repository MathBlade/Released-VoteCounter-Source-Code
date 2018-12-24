using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SupportClasses
{
    [Serializable]
    public class ResultsObject
    {

        List<VoteCount> allVCs;
        string wagonText;
        bool cleanDay;

        public ResultsObject(List<VoteCount> _allVCs, string _wagonText, bool _cleanDay)
        {
            allVCs = _allVCs;
            wagonText = _wagonText;
            cleanDay = _cleanDay;
        }



        public VoteCount FinalVoteCount
        {
            get
            {

                if (AllVCs.Count == 0)
                {
                    throw new System.ArgumentNullException("No Vote Counts exist");
                }
                else if (AllVCs.Count == 1)
                {
                    return AllVCs[0];
                }

                for (int i = allVCs.Count - 1; i > -1; i--)
                {
                    if (cleanDay || allVCs[i].HasVotes)
                    {
                        return allVCs[i];
                    }
                }

                throw new System.ArgumentNullException("No Valid Vote Count found " + allVCs.Count);
                //return null;

            }
        }
        public string WagonText { get { return wagonText; } }
        public List<VoteCount> AllVCs { get { return allVCs; } }
    }
}
