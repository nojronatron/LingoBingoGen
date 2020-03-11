using Microsoft.VisualStudio.TestTools.UnitTesting;
using LingoBingoGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingoBingoGenerator.Tests
{
    [TestClass()]
    public class LingoGeneratorTests
    {
        public string[] ArrayWords26 = new string[]
        {
        "alpha", "bravo", "charlie", "delta", "echo", "foxtrot", "golf", "hotel", "india",
        "juliet", "kilo", "lima", "mike", "november", "oscar", "papa", "quebec", "roger",
        "sierra", "tango", "uniform", "victor", "whiskey", "xray", "yankee", "zulu"
        };

        public List<string> ListWords26 = new List<string>
        {
        "alpha", "bravo", "charlie", "delta", "echo", "foxtrot", "golf", "hotel", "india",
        "juliet", "kilo", "lima", "mike", "november", "oscar", "papa", "quebec", "roger",
        "sierra", "tango", "uniform", "victor", "whiskey", "xray", "yankee", "zulu"
        };


        [TestMethod()]
        public void Test_LingoGenerator()
        {
            LingoGenerator lg = new LingoGenerator();
            string expectedResult = "LingoBingoGenerator.LingoGenerator";
            Assert.AreEqual(expectedResult, lg.GetType().FullName.ToString());
        }

        [TestMethod()]
        public void Test_Count_Null()
        {
            LingoGenerator lg = new LingoGenerator();
            int expectedResult = 0;
            int actualResult = lg.Count;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void Test_Count_Valid()
        {
            LingoGenerator lg = new LingoGenerator();
            lg.SetLingoWords(ArrayWords26);
            int expectedResult = ArrayWords26.Length;
            int actualResult = lg.Count;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void Test_LingoWords_Null()
        {
            LingoGenerator lg = new LingoGenerator();
            List<string> actualResult = lg.GetLingoWords();
            List<string> expectedResult = null;
            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestMethod()]
        public void Test_LingoWords_Valid()
        {
            LingoGenerator lg = new LingoGenerator();
            lg.SetLingoWords(ArrayWords26);
            int actualResult = lg.GetLingoWords().Count;
            int expectedResult = 26;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void Test_GetLongestWordNovember()
        {
            LingoGenerator lg = new LingoGenerator();
            lg.SetLingoWords(ArrayWords26);
            string actualResult = lg.GetLongestWord();
            string expectedResult = "November";
            Assert.AreEqual(expectedResult.ToUpper(), actualResult.ToUpper());
        }

        [TestMethod()]
        public void Test_GetLongestWordLen()
        {
            LingoGenerator lg = new LingoGenerator();
            lg.SetLingoWords(ArrayWords26);
            int actualResult = lg.GetLongestWordLen();
            int expectedResult = 8;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void Test_GetRandomizedList()
        {
            LingoGenerator lg = new LingoGenerator();
            lg.SetLingoWords(ArrayWords26);
            lg.RandomizeList();
            List<string> randomizedList = lg.GetRandomList();
            List<string> originalList = ArrayWords26.ToList<string>();
            int countOfNonMatches = 0;
            int countOfMatches = 0;
            for (int index=0; index <= originalList.Count - 1; index++)
            {
                if (randomizedList[index].ToUpper() != originalList[index].ToUpper())
                {
                    countOfNonMatches++;
                }
                else
                {
                    countOfMatches++;
                }
            }
            double expectedResult = 0.99;
            double actualResult = countOfMatches / countOfNonMatches;
            Assert.IsTrue(expectedResult >= actualResult);
        }

        [TestMethod()]
        public void Test_RandomizeList_EmptyFails()
        {
            LingoGenerator lg = new LingoGenerator();
            bool actualResult = lg.RandomizeList();
            Assert.IsFalse(actualResult);
        }

    }
}