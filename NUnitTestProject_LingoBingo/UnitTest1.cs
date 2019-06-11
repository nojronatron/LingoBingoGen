using NUnit.Framework;
using System;

namespace LingoBingoTests
{
    public class NUnitTests
    {
        [SetUp]
        public void Setup()
        {
            string[] strArray = {"method", "inheritance", "class", "object", "instance", "property", "field", "constructor",
                                    "dot net", "array", "main", "curly brace", "string", "boolean", "semicolon", "parse",
                                    "try catch", "partial", "return", "call", "override", "keyword", "get or set", "static"};
            LingoBingoGenerator.BingoBoard _bb = new LingoBingoGenerator.BingoBoard(strArray);
        }
        [Test]
        public void Test_CTOR()
        {
            try
            {
                LingoBingoGenerator.BingoBoard _bb2 = new LingoBingoGenerator.BingoBoard();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("", ex.Message);
            }
        }
        [Test]
        public void Test_longestWord()
        {
            Assert.Fail();
        }
        [Test]
        public void Test_longestWordLen()
        {
            Assert.Fail();
        }
        [Test]
        public void Test_TileWidth()
        {
            Assert.Fail();
        }
        [Test]
        public void Test_HorizFrameLength()
        {
            Assert.Fail();
        }
        [Test]
        public void Test_SetFreeSpace()
        {
            Assert.Fail();
        }
        [Test]
        public void Test_MakeBoard()
        {
            Assert.Fail();
        }
        [Test]
        public void Test_Randomizer()
        {
            Assert.Fail();
        }
    }
}