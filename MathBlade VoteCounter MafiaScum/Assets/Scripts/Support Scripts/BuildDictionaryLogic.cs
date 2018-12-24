using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Assets.Scripts.Support_Scripts
{
    public static class BuildDictionaryLogic
    {      

        public static string[] ScrubDictionaryFile(TextAsset wordsFile)
        {
          
            string[] textAssetLines = Regex.Split(wordsFile.text, "\n|\r|\r\n");

            return textAssetLines;
        }

        public static List<String> GetAllWordsInString(string[] words, string bigString)
        {
            List<string> wordsFound = new List<String>();
            foreach (string word in words)
            {
                if (word.Length <= 2)
                {
                    continue;
                }
                if (bigString.ContainsIgnoreCase(word))
                {
                    wordsFound.Add(word.ToLower());
                }
            }
            return wordsFound;
        }




    }
}
