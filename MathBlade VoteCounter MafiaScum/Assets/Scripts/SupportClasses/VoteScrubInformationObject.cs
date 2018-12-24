using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SupportClasses
{
    [Serializable]
    public class VoteScrubInformationObject
    {

        string priorVCNumberInput;
        string urlOfGame;
        string playerTextInput;
        string replacementTextInput;
        string moderatorNamesInput;
        string dayNumbersInput;
        string deadListInput;
        string deadLineInput;
        string flavorInput;
        string voteOverridesInput;
        string alphaSortInput;
        string simpleInput;
        string lSortInput;
        string cleanDayInput;
        string displayAllVCsInput;
        string colorCode;
        string prodTimer;
        string fontOverride;
        bool areaTagsOn;
        string dividerOverride;
        bool showLLevel;
        bool showZeroCountWagons;
        string dayviggedInput;
        string resurrectedInput;
        bool hardReset;

        public VoteScrubInformationObject(string _priorVCInput, string _urlOfGame, string _playerTextInput, string _dayNumbersInput, string _replacementTextInput, string _moderatorNamesInput, string _deadListInput, string _deadlineInput, string _flavorInput, string _voteOverridesInput, string _alphaSortInput, string _simpleInput, string _lSortInput, string _cleanDayInput, string _displayAllVCsInput, string _colorCode, string _prodTimer, string _fontOverride, bool _areaTagsOn, string _dividerOverride, bool _showLLevel, bool _showZeroCountWagons, string _dayviggedInput, string _resurrectedInput, bool _hardReset)
        {
            priorVCNumberInput = _priorVCInput;
            urlOfGame = _urlOfGame;
            playerTextInput = _playerTextInput;
            replacementTextInput = _replacementTextInput;
            moderatorNamesInput = _moderatorNamesInput;
            dayNumbersInput = _dayNumbersInput;
            deadListInput = _deadListInput;
            deadLineInput = _deadlineInput;
            flavorInput = _flavorInput;
            voteOverridesInput = _voteOverridesInput;
            alphaSortInput = _alphaSortInput;
            simpleInput = _simpleInput;
            lSortInput = _lSortInput;
            cleanDayInput = _cleanDayInput;
            displayAllVCsInput = _displayAllVCsInput;
            colorCode = _colorCode;
            prodTimer = _prodTimer;
            fontOverride = _fontOverride;
            areaTagsOn = _areaTagsOn;
            dividerOverride = _dividerOverride;
            showLLevel = _showLLevel;
            showZeroCountWagons = _showZeroCountWagons;
            dayviggedInput = _dayviggedInput;
            resurrectedInput = _resurrectedInput;
            hardReset = _hardReset;
        }

        public string PriorVCNumberInput
        {
            get
            {
                return priorVCNumberInput;
            }

            set
            {
                priorVCNumberInput = value;
            }
        }

        public string UrlOfGame
        {
            get
            {
                return urlOfGame;
            }

            set
            {
                urlOfGame = value;
            }
        }

        public string PlayerTextInput
        {
            get
            {
                return playerTextInput;
            }

            set
            {
                playerTextInput = value;
            }
        }

        public string PlayerTextInput1
        {
            get
            {
                return playerTextInput;
            }

            set
            {
                playerTextInput = value;
            }
        }

        public string ReplacementTextInput
        {
            get
            {
                return replacementTextInput;
            }

            set
            {
                replacementTextInput = value;
            }
        }

        public string ModeratorNamesInput
        {
            get
            {
                return moderatorNamesInput;
            }

            set
            {
                moderatorNamesInput = value;
            }
        }

        public string DayNumbersInput
        {
            get
            {
                return dayNumbersInput;
            }

            set
            {
                dayNumbersInput = value;
            }
        }

        public string DeadListInput
        {
            get
            {
                return deadListInput;
            }

            set
            {
                deadListInput = value;
            }
        }

        public string DeadLineInput
        {
            get
            {
                return deadLineInput;
            }

            set
            {
                deadLineInput = value;
            }
        }

        public string FlavorInput
        {
            get
            {
                return flavorInput;
            }

            set
            {
                flavorInput = value;
            }
        }

        public string VoteOverridesInput
        {
            get
            {
                return voteOverridesInput;
            }

            set
            {
                voteOverridesInput = value;
            }
        }

        public string AlphaSortInput
        {
            get
            {
                return alphaSortInput;
            }

            set
            {
                alphaSortInput = value;
            }
        }

        public string SimpleInput
        {
            get
            {
                return simpleInput;
            }

            set
            {
                simpleInput = value;
            }
        }

        public string LSortInput
        {
            get
            {
                return lSortInput;
            }

            set
            {
                lSortInput = value;
            }
        }

        public string CleanDayInput
        {
            get
            {
                return cleanDayInput;
            }

            set
            {
                cleanDayInput = value;
            }
        }

        public string DisplayAllVCsInput
        {
            get
            {
                return displayAllVCsInput;
            }

            set
            {
                displayAllVCsInput = value;
            }
        }

        public string ColorCode
        {
            get
            {
                return colorCode;
            }

            set
            {
                colorCode = value;
            }
        }

        public string ProdTimer
        {
            get
            {
                return prodTimer;
            }

            set
            {
                prodTimer = value;
            }
        }

        public string FontOverride
        {
            get
            {
                return fontOverride;
            }

            set
            {
                fontOverride = value;
            }
        }

        public bool AreaTagsOn
        {
            get
            {
                return areaTagsOn;
            }

            set
            {
                areaTagsOn = value;
            }
        }

        public string DividerOverride
        {
            get
            {
                return dividerOverride;
            }

            set
            {
                dividerOverride = value;
            }
        }

        public bool ShowLLevel
        {
            get
            {
                return showLLevel;
            }

            set
            {
                showLLevel = value;
            }
        }

        public bool ShowZeroCountWagons
        {
            get
            {
                return showZeroCountWagons;
            }

            set
            {
                showZeroCountWagons = value;
            }
        }

        public string DayviggedInput
        {
            get
            {
                return dayviggedInput;
            }

            set
            {
                dayviggedInput = value;
            }
        }

        public string ResurrectedInput
        {
            get
            {
                return resurrectedInput;
            }

            set
            {
                resurrectedInput = value;
            }
        }

        public bool HardReset
        {
            get
            {
                return hardReset;
            }

            set
            {
                hardReset = value;
            }
        }



        /*public string PriorVCNumberInput { get { return priorVCNumberInput} set { priorVCNumberInput = value; }  }
        public string UrlOfGame { get; set; }
        public string PlayerTextInput { get ; set ; }
        public string ReplacementTextInput { get ; set ; }
        public string ModeratorNamesInput { get; set; }
        public string DayNumbersInput { get ; set ; }
        public string DeadListInput { get ; set ; }
        public string DeadLineInput { get ; set ; }
        public string FlavorInput { get ; set; }
        public string VoteOverridesInput { get ; set ; }
        public string AlphaSortInput { get ; set; }
        public string SimpleInput { get ; set ; }
        public string LSortInput { get ; set ; }
        public string CleanDayInput { get ; set ; }
        public string DisplayAllVCsInput { get ; set ; }
        public string ColorCode { get ; set ; }
        public string ProdTimer { get ; set ; }
        public string FontOverride { get; set ; }
        public bool AreaTagsOn { get; set ; }
        public string DividerOverride { get ; set ; }
        public bool ShowLLevel { get ; set ; }
        public bool ShowZeroCountWagons { get ; set ; }
        public string DayviggedInput { get; set ; }
        public string ResurrectedInput { get; set ; }
        public bool HardReset { get; set; }*/


        /*public string PriorVCNumberInput { get => priorVCNumberInput; set => priorVCNumberInput = value; }
        public string UrlOfGame { get => urlOfGame; set => urlOfGame = value; }
        public string PlayerTextInput { get => playerTextInput; set => playerTextInput = value; }
        public string ReplacementTextInput { get => replacementTextInput; set => replacementTextInput = value; }
        public string ModeratorNamesInput { get => moderatorNamesInput; set => moderatorNamesInput = value; }
        public string DayNumbersInput { get => dayNumbersInput; set => dayNumbersInput = value; }
        public string DeadListInput { get => deadListInput; set => deadListInput = value; }
        public string DeadLineInput { get => deadLineInput; set => deadLineInput = value; }
        public string FlavorInput { get => flavorInput; set => flavorInput = value; }
        public string VoteOverridesInput { get => voteOverridesInput; set => voteOverridesInput = value; }
        public string AlphaSortInput { get => alphaSortInput; set => alphaSortInput = value; }
        public string SimpleInput { get => simpleInput; set => simpleInput = value; }
        public string LSortInput { get => lSortInput; set => lSortInput = value; }
        public string CleanDayInput { get => cleanDayInput; set => cleanDayInput = value; }
        public string DisplayAllVCsInput { get => displayAllVCsInput; set => displayAllVCsInput = value; }
        public string ColorCode { get => colorCode; set => colorCode = value; }
        public string ProdTimer { get => prodTimer; set => prodTimer = value; }
        public string FontOverride { get => fontOverride; set => fontOverride = value; }
        public bool AreaTagsOn { get => areaTagsOn; set => areaTagsOn = value; }
        public string DividerOverride { get => dividerOverride; set => dividerOverride = value; }
        public bool ShowLLevel { get => showLLevel; set => showLLevel = value; }
        public bool ShowZeroCountWagons { get => showZeroCountWagons; set => showZeroCountWagons = value; }
        public string DayviggedInput { get => dayviggedInput; set => dayviggedInput = value; }
        public string ResurrectedInput { get => resurrectedInput; set => ResurrectedInput = value; }
        public bool HardReset { get => hardReset; set => hardReset = value; }*/
    }
}
