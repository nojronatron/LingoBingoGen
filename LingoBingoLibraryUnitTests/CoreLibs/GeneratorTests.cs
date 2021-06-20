using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LingoBingoLibrary.CoreLibs.UnitTests
{
    [TestClass()]
    public class GeneratorTests
    {
        private List<string> Words {get; set;}
        private Generator Generator { get; set; }
        private string Category => "CSharp";

        [TestMethod()]
        public void OneBigTest()
        {
            this.Words = new List<string>();
            this.Generator = new Generator();
            var loadWordPoolResult = false;

            //if (generator.LoadWordPools())
            //{
            //    loadWordPoolResult = true;
            //}
            //else
            {
                Assert.Fail();
            }

            var getNewBingoBoardResult = false;
            //var bingoBoard = generator.GetNewBingoBoard(Category);

            //if (bingoBoard.Count == 25)
            //{
            //    getNewBingoBoardResult = true;
            //}
            //else
            {
                Assert.Fail();
            }

            //var saveWordPoolsResult = generator.SaveWordPools(generator.lingoWords);

            Assert.IsTrue(loadWordPoolResult);
            Assert.IsTrue(getNewBingoBoardResult);
            //Assert.IsTrue(saveWordPoolsResult);
        }

    }
}