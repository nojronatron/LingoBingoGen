using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LingoBingoLibrary.Models.UnitTests
{
    [TestClass()]
    public class LingoWordTests
    {
        [TestMethod()]
        public void CTOR_Blank_Test()
        {
            var lw = new BasicLingoWord();
            var expectedTypeName = "LingoBingoLibrary.Models.LingoWord";
            var expectedWord = string.Empty;
            var expectedCategory = string.Empty;

            var actualTypeName = lw.GetType().FullName;
            var actualWord = string.IsNullOrEmpty(lw.Word) ? string.Empty : lw.Word;
            var actualCategory = string.IsNullOrEmpty(lw.Category) ? string.Empty : lw.Category;

            Assert.AreEqual(expectedTypeName, actualTypeName);
            Assert.AreEqual(expectedWord, actualWord);
            Assert.AreEqual(expectedCategory, actualCategory);
        }

        [TestMethod()]
        public void CTOR_Full_Test()
        {
            var lw = new BasicLingoWord("alpha", "test");
            var expectedTypeName = "LingoBingoLibrary.Models.LingoWord";
            var expectedWord = "alpha";
            var expectedCategory = "test";

            var actualTypeName = lw.GetType().FullName;
            var actualWord = string.IsNullOrEmpty(lw.Word) ? string.Empty : lw.Word;
            var actualCategory = string.IsNullOrEmpty(lw.Category) ? string.Empty : lw.Category;

            Assert.AreEqual(expectedTypeName, actualTypeName);
            Assert.AreEqual(expectedWord, actualWord);
            Assert.AreEqual(expectedCategory, actualCategory);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var a = new BasicLingoWord("alpha", "test");
            var b = new BasicLingoWord("bravo", "test");
            var c = new BasicLingoWord("alpha", "toast");
            var d = new BasicLingoWord("alpha", "test");

            var expected = true;
            var actual = a.Equals(a);
            Assert.AreEqual(expected, actual);

            expected = false;
            actual = a.Equals(b);
            Assert.AreEqual(expected, actual);

            expected = false;
            actual = a.Equals(c);
            Assert.AreEqual(expected, actual);

            expected = true;
            actual = d.Equals(a);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var lw = new BasicLingoWord("alpha", "test");
            var expected = true;
            var hc = lw.GetHashCode();
            var hcs = $"{ hc }";
            int isInteger = default;

            var actual = (hc < int.MaxValue && hc > int.MinValue && int.TryParse(hcs, out isInteger));

            Assert.IsTrue(isInteger != (int)default);
            Assert.IsTrue(expected == actual);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var lw = new BasicLingoWord("alpha", "test");
            var expected = "test: alpha";

            var actual = lw.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}