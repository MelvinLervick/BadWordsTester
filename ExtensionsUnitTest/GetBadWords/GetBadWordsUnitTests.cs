using System.Collections.Generic;
using System.Linq;
using BadWordsTester;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionsUnitTest.GetBadWords
{
    [TestClass]
    public class GetBadWordsUnitTests
    {
        [TestMethod]
        public void ContainsBadWordFilterReturnsFalseForPartialBadWordsFound()
        {
            var badWords = GetBadWords();
            var keyword = new List<string>{ "large men" };
            Assert.IsFalse(badWords.ContainsBadWordFilter(keyword).Any());
        }

        [TestMethod]
        public void ContainsBadWordFilterReturnsTrueForAllBadWordsFoundInSameOrder()
        {
            var badWords = GetBadWords();
            var keyword = new List<string> {"very large men are always the best"};
            Assert.IsTrue(badWords.ContainsBadWordFilter(keyword).Any());
        }

        [TestMethod]
        public void ContainsBadWordFilterReturnTrueForAllBadWordsFoundInRandomOrder()
        {
            var badWords = GetBadWords();
            var keyword = new List<string>{"The best men are often large"};
            Assert.IsTrue(badWords.ContainsBadWordFilter(keyword).Any());
        }

        [TestMethod]
        public void ContainsBadWordFilterReturnFalseForMenVsMan()
        {
            var badWords = GetBadWords();
            var keyword = new List<string>{"The best man is often large"};
            Assert.IsFalse(badWords.ContainsBadWordFilter(keyword).Any());
        }

        private static BadWords GetBadWords()
        {
            var badWords =
                new BadWords( new List<string>
                {
                    {"xrated"},
                    {"the small girls"},
                    {"the large boys"},
                    {"the large men are best"}
                } );
            return badWords;
        }
    }
}
