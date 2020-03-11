using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingoBingoGenerator
{
    public class LingoGenerator
    {
        //  input: List of category Phrases
        //  output: an object with a randomized list
        //  behavior: instantiatable
        //  basic capabilities: accept list; randomize list; return properties of the list of phrases

        private List<string> _lingoWords = null;
        private List<string> _randomizedLingoWords = null;

        public LingoGenerator() { }

        private string LongestWord { get; set; } = string.Empty;
        private int LongestWordLen { get; set; } = 0;
        public int Count
        {
            get
            {
                if (_lingoWords != null)
                {
                    return _lingoWords.Count();
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool HasWords()
        {
            return (this.Count >= 24);
        }

        private bool HasRandomizedList()
        {
            return (this._randomizedLingoWords.Count >= 24);
        }
        
        public bool SetLingoWords(List<string> wordList)
        {
            //  takes input List<string> of phrases and stores them in this object instance
            if (wordList != null)
            {
                this._lingoWords = new List<string>(wordList);
                if (_lingoWords.Capacity >= 24)
                {
                    int longLen = 0;
                    string longestWord = string.Empty;
                    //  sets the first occurance of a word with the longest length
                    foreach (string word in _lingoWords)
                    {
                        if (word.Length > longLen)
                        {
                            longestWord = word;
                            longLen = word.Length;
                        }
                    }
                    this.LongestWordLen = longestWord.Length;
                    this.LongestWord = longestWord;
                }
                return true;
            }
            return false;
        }

        public bool SetLingoWords(string[] wordList)
        {
            //  overload for string[] array type input
            if (wordList != null)
            {
                this._lingoWords = new List<string>(wordList);
                if (_lingoWords.Capacity >= 24)
                {
                    int longLen = 0;
                    string longestWord = string.Empty;
                    //  sets the first occurance of a word with the longest length
                    foreach (string word in _lingoWords)
                    {
                        if (word.Length > longLen)
                        {
                            longestWord = word;
                            longLen = word.Length;
                        }
                    }
                    this.LongestWordLen = longestWord.Length;
                    this.LongestWord = longestWord;
                }
                return true;
            }
            return false;
        }

        public List<string> GetLingoWords()
        {
            if (this.HasWords())
            {
                return this._lingoWords;
            }
            return null;
        }
        
        public string GetLongestWord()
        {
            if (this.HasWords())
            {
                return this.LongestWord;
            }
            return string.Empty;
        }

        public int GetLongestWordLen()
        {
            return this.LongestWordLen;
        }

        public bool RandomizeList()
        {
            //  uses field this._phrases as input, returns _phrases as reordered List<string>
            if (this.HasWords())
            {
                string[] arrResult = _lingoWords.ToArray();
                Random rand = new Random(); // see: https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netframework-4.8
                double[] order = new double[24];
                for (int counter = 0; counter < 24; counter++)
                {
                    order[counter] = rand.NextDouble();
                }
                Array.Sort(order, arrResult);
                _randomizedLingoWords = new List<string>(arrResult);
                return true;
            }
            return false;
        }
        public List<string> GetRandomList()
        {
            if (this.HasWords() && this.HasRandomizedList())
            {
                return _randomizedLingoWords;
            }
            return new List<string>(0);
        }
    }
}
