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
        public void Test_LingoWords()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Test_LingoWords1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Test_GetLongestWord()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Test_GetLongestWordLen()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Test_GetRandomizedList()
        {
            Assert.Fail();
        }
    }
}