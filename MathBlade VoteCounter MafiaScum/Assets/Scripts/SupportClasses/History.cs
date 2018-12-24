using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Support_Scripts;

namespace Assets.Scripts.SupportClasses
{


    public class History
    {
        public const string NEW_LINE_HERE = "NEW_LINE_HERE";


        bool lSort;
        private bool isRestCall;

        public History(List<Wagon> historicalWagons, bool _lSort, bool _isRestCall)
        {
            HistoricalWagons = historicalWagons;
            lSort = _lSort;
            isRestCall = _isRestCall;
        }


        public string buildDisplayString(int dayNumber, bool simple)
        {
            string returnString = "[spoiler=Day " + dayNumber + "]";
            if (lSort)
            {

                HistoricalWagons.Sort();
                int currentLoopLevel = 0;
                foreach (Wagon historicalWagon in HistoricalWagons)
                {
                    if (historicalWagon.PlayersVoting.Count > 0)
                    {
                        if (currentLoopLevel != historicalWagon.L_Level)
                        {
                            if (currentLoopLevel != 0)
                            {
                                returnString = returnString + "[/area]";
                            }
                            /*if (historicalWagon.L_Level > 0) {
                                returnString = returnString + "[b]L-" + historicalWagon.L_Level + "[/b]" + NEW_LINE_HERE + "[area]";
                            } else {
                                returnString = returnString + "[b]HAMMER TIME![/b]" + NEW_LINE_HERE + "[area]";
                            }*/
                            returnString = returnString + NEW_LINE_HERE + "[area]";
                            currentLoopLevel = historicalWagon.L_Level;
                        }

                        returnString = returnString + historicalWagon.ToHistoricalDisplayString(!simple) + NEW_LINE_HERE;
                    }
                }
                returnString = returnString.Replace(NEW_LINE_HERE, System.Environment.NewLine + ((isRestCall) ? "" : "")) + "[/area][/spoiler]";

                return returnString;
            }
            else
            {
                returnString = returnString + "[area]";
                HistoricalWagons.Sort();
                foreach (Wagon historicalWagon in HistoricalWagons)
                {
                    if (historicalWagon.PlayersVoting.Count > 0)
                    {

                        returnString = returnString + historicalWagon.ToHistoricalDisplayString(!simple) + NEW_LINE_HERE;
                    }
                }
                returnString = returnString.Replace(NEW_LINE_HERE, System.Environment.NewLine + ((isRestCall) ? "" : "")) + "[/area][/spoiler]";
                return returnString;
            }

        }

        private List<Wagon> HistoricalWagons { get; set; }

    }


 }