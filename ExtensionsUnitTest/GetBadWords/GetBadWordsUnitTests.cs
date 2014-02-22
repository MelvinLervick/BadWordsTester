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
        public void GetBadWordsCreatesOrderedDictionary()
        {
            var badWords = GetBadWords();

            Assert.IsTrue(badWords.SortedBadWords.First().Key.Contains("best"));
            Assert.IsTrue(badWords.SortedBadWords.First().Value == 3);
        }

        [TestMethod]
        public void GetBadWordsCreatesOrderedDictionaryWhenAddingBadWords()
        {
            var badWords = GetBadWords();
            const string aName = "apples";

            badWords.SortedBadWords.Add( aName, 1 );

            Assert.IsTrue(badWords.SortedBadWords.First().Key.Contains(aName));
        }

        private static BadWords GetBadWords()
        {
            var badWords =
                new BadWords( new Dictionary<string, int>
                {
                    {"xrated", 1},
                    {"the small girls", 3},
                    {"the large boys", 3},
                    {"the large men are best", 5}
                } );
            return badWords;
        }
    }
}
