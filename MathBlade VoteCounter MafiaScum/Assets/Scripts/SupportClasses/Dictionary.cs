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
    public static class Dictionary
    {

        private static DictionaryInstance _instance;


        static Dictionary()
        {

            _instance = Instance();

        }


     


        private static void SetNewInstanceByTextAsset(TextAsset asset)
        {
            if ((asset != null) && (_instance == null))
            {
                _instance = BuildDictionaryTextAssetInstance(asset);
            }
          
        }

        public static DictionaryInstance BuildDictionaryTextAssetInstance(TextAsset textAsset)
        {
            string[] wordsFoundInFile = BuildDictionaryLogic.ScrubDictionaryFile(textAsset);
            if (wordsFoundInFile == null)
                return null;
            else
                return new DictionaryInstance(wordsFoundInFile);
        }

        public static List<string> AllWordsInString(string bigString)
        {
            return Instance().GetAllWordsInString(bigString);
        }

        public static DictionaryInstance Instance()
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                _instance = createNewInstance();
                return _instance;
            }


        }



        private static DictionaryInstance createNewInstance()
        {

            return BuildDictionaryTextAssetInstance(GameObject.FindObjectOfType<EntryPanel>().WordsFile);
        
        }


        public class DictionaryInstance
        {
            string[] words;
            public DictionaryInstance(string[] _words)
            {
                words = _words;
            }

            public List<String> GetAllWordsInString(string bigString)
            {
                return BuildDictionaryLogic.GetAllWordsInString(words, bigString);
            }
        }


    }



}
