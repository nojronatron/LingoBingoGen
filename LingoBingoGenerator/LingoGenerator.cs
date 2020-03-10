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
        public LingoGenerator() { }
        private string LongestWord { get; set; }
        private int LongestWordLen { get; set; } = 0;
        public void LingoWords(List<string> phrases)
        {
            //  takes input List<string> of phrases and stores them in this object instance
            this._lingoWords = new List<string>(phrases);
        }
        public void LingoWords(string[] phrases)
        {
            //  overload for string[] array type input
            this._lingoWords = new List<string>(phrases);
        }
        public string GetLongestWord()
        {
            if (_lingoWords.Capacity < 24)
            {
                return string.Empty;
            }
            int longLen = 0;
            string longestWord = string.Empty;
            foreach (string word in _lingoWords)
            {
                if (word.Length > longLen)
                {
                    LongestWord = longestWord;
                    longLen = word.Length;
                }
            }
            this.LongestWordLen = longestWord.Length;
            this.LongestWord = longestWord;
            return this.LongestWord;
        }
        public int GetLongestWordLen()
        {
            return this.LongestWordLen;
        }
        public List<string> GetRandomizedList()
        {
            //  uses field this._phrases as input, returns _phrases as reordered List<string>
            string[] arrResult = _lingoWords.ToArray();
            Random rand = new Random(); // see: https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netframework-4.8
            double[] order = new double[24];
            for (int counter = 0; counter < 24; counter++)
            {
                order[counter] = rand.NextDouble();
            }
            Array.Sort(order, arrResult);
            return new List<string>(arrResult);
        }
    }
}
