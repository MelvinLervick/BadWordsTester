using System.Linq;
using System.Text;
using Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionsUnitTest.Extensions
{
    [TestClass]
    public class StringExtensionsUnitTests
    {
        #region IsSimilarTo
        [TestMethod]
        public void IsSimilarToHaystackNullReturnsFalse()
        {
            string haystack = null;
            const string needle = null;
            Assert.IsFalse(haystack.IsSimilarTo(needle));
        }

        [TestMethod]
        public void IsSimilarToNeedleNullReturnsFalse()
        {
            const string haystack = "test";
            const string needle = null;
            Assert.IsFalse(haystack.IsSimilarTo(needle));
        }

        #endregion IsSimilarTo

        #region Stopwords

        [TestMethod]
        public void ToStopwordNormalizedNullReturnsEmptyString()
        {
            var outString = StringExtensions.ToStopwordNormalized(null);
            Assert.AreEqual(string.Empty, outString);
        }

        [TestMethod]
        public void ToStopwordNormalizedEmptyStringReturnsEmptyString()
        {
            var outString = string.Empty.ToStopwordNormalized();
            Assert.AreEqual(string.Empty, outString);
        }

        [TestMethod]
        public void ToStopwordNormalizedWhiteSpaceStringReturnsEmptyString()
        {
            var outString = "   ".ToStopwordNormalized();
            Assert.AreEqual(string.Empty, outString);
        }

        [TestMethod]
        public void ToStopwordNormalizedStopWordsRemoved()
        {
            const string inString = "the words";
            var outString = inString.ToStopwordNormalized();
            Assert.AreNotEqual(inString, outString);
            Assert.AreNotEqual(inString.Length, outString.Length);
            Assert.IsFalse(outString.Contains("the"));
        }

        [TestMethod]
        public void ToStopwordNormalizedReturnsLowerCaseString()
        {
            const string inString = "Test string";
            var outString = inString.ToStopwordNormalized();
            Assert.AreNotEqual(inString, outString);
            Assert.IsFalse(outString.Contains('T'));
        }

        [TestMethod]
        public void ToStopwordNormalizedReturnsOrderedWords()
        {
            const string inString = "second first";
            var outString = inString.ToStopwordNormalized();
            Assert.AreNotEqual(inString, outString);
            Assert.AreNotEqual(inString.Split(' ')[0], outString.Split(' ')[0]);
            Assert.AreEqual(inString.Split(' ')[0], outString.Split(' ')[1]);
            Assert.AreEqual(inString.Split(' ')[1], outString.Split(' ')[0]);
        }

        [TestMethod]
        public void ToStopwordNormalizedReturnsTrimmedString()
        {
            const string inString = "   term   ";
            var outString = inString.ToStopwordNormalized();
            Assert.AreNotEqual(inString, outString);
            Assert.AreNotEqual(inString.Length, outString.Length);
            Assert.IsFalse(char.IsWhiteSpace(outString[0]));
        }

        [TestMethod]
        public void ToStopwordNormalizedHandlesSpecialChars()
        {
            const string inString = @" '  term  [.]\/} ";
            var outString = inString.ToStopwordNormalized();
            Assert.AreNotEqual(inString, outString);
            Assert.AreNotEqual(inString.Length, outString.Length);
            Assert.AreEqual("term", outString);
        }

        [TestMethod]
        public void ToStopwordNormalizedHandlesNullOutputs()
        {
            const string inString = @"19 in 1";
            var outString = inString.ToStopwordNormalized();
            Assert.AreNotEqual(inString, outString);
            Assert.AreNotEqual(inString.Length, outString.Length);
            Assert.IsTrue(string.IsNullOrWhiteSpace(outString));
        }

        [TestMethod]
        public void ToStopwordNormalizedRemovesNumbersInWords()
        {
            const string inString = @"04 infiniti fx35";
            var outString = inString.ToStopwordNormalized();
            Assert.AreEqual("fx infiniti", outString);
        }

        [TestMethod]
        public void ToStopwordNormalizedPreservesForeignCharacters()
        {
            const string inString = "Μή μου ἅπτου";
            var outString = inString.ToPorterStemNormalized();
            Assert.AreEqual(outString, "ἅπτου μή μου");
        }

        #endregion Stopwords

        #region ToPorter

        [TestMethod]
        public void ToPorterStemNormalizedNullReturnsEmptyString()
        {
            var outString = StringExtensions.ToPorterStemNormalized(null);
            Assert.AreEqual(string.Empty, outString);
        }

        [TestMethod]
        public void ToPorterStemNormalizedEmptyStringReturnsEmptyString()
        {
            var outString = string.Empty.ToPorterStemNormalized();
            Assert.AreEqual(string.Empty, outString);
        }

        [TestMethod]
        public void ToPorterStemNormalizedWhiteSpaceStringReturnsEmptyString()
        {
            var outString = "   ".ToPorterStemNormalized();
            Assert.AreEqual(string.Empty, outString);
        }

        [TestMethod]
        public void ToPorterStemNormalizedPluralSuffixRemoved()
        {
            const string inString = "words";
            var outString = inString.ToPorterStemNormalized();
            Assert.AreNotEqual(inString, outString);
            Assert.AreNotEqual(inString.Length, outString.Length);
            Assert.IsFalse(outString[outString.Length - 1] == 's');
        }

        [TestMethod]
        public void ToPorterStemNormalizedStopWordsRemoved()
        {
            const string inString = "the words";
            var outString = inString.ToPorterStemNormalized();
            Assert.AreNotEqual(inString, outString);
            Assert.AreNotEqual(inString.Length, outString.Length);
            Assert.IsFalse(outString.Contains("the"));
        }

        [TestMethod]
        public void ToPorterStemNormalizedReturnsLowerCaseString()
        {
            const string inString = "Test string";
            var outString = inString.ToPorterStemNormalized();
            Assert.AreNotEqual(inString, outString);
            Assert.IsFalse(outString.Contains('T'));
        }

        [TestMethod]
        public void ToPorterStemNormalizedReturnsOrderedWords()
        {
            const string inString = "second first";
            var outString = inString.ToPorterStemNormalized();
            Assert.AreNotEqual(inString, outString);
            Assert.AreNotEqual(inString.Split(' ')[0], outString.Split(' ')[0]);
            Assert.AreEqual(inString.Split(' ')[0], outString.Split(' ')[1]);
            Assert.AreEqual(inString.Split(' ')[1], outString.Split(' ')[0]);
        }

        [TestMethod]
        public void ToPorterStemNormalizedReturnsTrimmedString()
        {
            const string inString = "   term   ";
            var outString = inString.ToPorterStemNormalized();
            Assert.AreNotEqual(inString, outString);
            Assert.AreNotEqual(inString.Length, outString.Length);
            Assert.AreEqual("term", outString);
        }

        [TestMethod]
        public void ToPorterStemNormalizedHandlesSpecialChars()
        {
            const string inString = @"   term  []\/} ";
            var outString = inString.ToPorterStemNormalized();
            Assert.AreNotEqual(inString, outString);
            Assert.AreNotEqual(inString.Length, outString.Length);
            Assert.AreEqual("term", outString);
        }

        [TestMethod]
        public void ToPorterStemNormalizedHandlesNullOutputs()
        {
            const string inString = @"19 in 1";
            var outString = inString.ToPorterStemNormalized();
            Assert.AreNotEqual(inString, outString);
            Assert.AreNotEqual(inString.Length, outString.Length);
            Assert.IsTrue(string.IsNullOrWhiteSpace(outString));
        }

        [TestMethod]
        public void ToPorterStemNormalizedPreservesForeignCharacters()
        {
            const string inString = "Μή μου ἅπτου";
            var outString = inString.ToPorterStemNormalized();
            Assert.AreEqual(outString, "ἅπτου μή μου");
        }

        #endregion ToPorter

        #region ToLower

        [TestMethod]
        public void ToLowerAlphabeticalNullReturnsEmptyString()
        {
            var outString = StringExtensions.ToLowerAlphabetical(null);
            Assert.AreEqual(string.Empty, outString);
        }

        [TestMethod]
        public void ToLowerAlphabeticalEmptyStringReturnsEmptyString()
        {
            var outString = string.Empty.ToLowerAlphabetical();
            Assert.AreEqual(string.Empty, outString);
        }

        [TestMethod]
        public void ToLowerAlphabeticalWhiteSpaceStringReturnsEmptyString()
        {
            var outString = "   ".ToLowerAlphabetical();
            Assert.AreEqual(string.Empty, outString);
        }

        [TestMethod]
        public void ToLowerAlphabeticalReturnsLowerCaseString()
        {
            const string inString = "Test string";
            var outString = inString.ToLowerAlphabetical();
            Assert.AreNotEqual(inString, outString);
            Assert.IsFalse(outString.Contains('T'));
        }

        [TestMethod]
        public void ToLowerAlphabeticalReturnsOrderedWords()
        {
            const string inString = "second first";
            var outString = inString.ToLowerAlphabetical();
            Assert.AreNotEqual(inString, outString);
            Assert.AreNotEqual(inString.Split(' ')[0], outString.Split(' ')[0]);
            Assert.AreEqual(inString.Split(' ')[0], outString.Split(' ')[1]);
            Assert.AreEqual(inString.Split(' ')[1], outString.Split(' ')[0]);
        }

        [TestMethod]
        public void ToLowerAlphabeticalReturnsTrimmedString()
        {
            const string inString = "   s   ";
            var outString = inString.ToLowerAlphabetical();
            Assert.AreNotEqual(inString, outString);
            Assert.AreNotEqual(inString.Length, outString.Length);
            Assert.IsFalse(char.IsWhiteSpace(outString[0]));
        }

        [TestMethod]
        public void ToLowerAlphabeticalPreservesPeriod()
        {
            const string inString = "st. moritz";
            var outString = inString.ToLowerAlphabetical();
            Assert.IsTrue(outString.Contains('.'));
        }

        [TestMethod]
        public void ToLowerAlphabeticalPreservesDash()
        {
            const string inString = "free e- cards";
            var outString = inString.ToLowerAlphabetical();
            Assert.IsTrue(outString.Contains('-'));
        }

        [TestMethod]
        public void ToLowerAlphabeticalPreservesTick()
        {
            const string inString = "kids' snowshoes";
            var outString = inString.ToLowerAlphabetical();
            Assert.IsTrue(outString.Contains('\''));
        }

        [TestMethod]
        public void ToLowerAlphabeticalPreservesQuote()
        {
            const string inString = "delsey 28\" luggage elite";
            var outString = inString.ToLowerAlphabetical();
            Assert.IsTrue(outString.Contains('\"'));
        }

        [TestMethod]
        public void ToLowerAlphabeticalPreservesBracket()
        {
            const string inString = "valentine's day cli[ art";
            var outString = inString.ToLowerAlphabetical();
            Assert.IsTrue(outString.Contains('['));
        }

        [TestMethod]
        public void ToLowerAlphabeticalPreservesNumbers()
        {
            const string inString = "12oo cal. diet plans";
            var outString = inString.ToLowerAlphabetical();
            Assert.IsTrue(outString.Contains("12oo"));
        }

        [TestMethod]
        public void ToLowerAlphabeticalPreservesAlternate()
        {
            const string inString = "24x24' cabin floor plans";
            var outString = inString.ToLowerAlphabetical();
            Assert.IsTrue(outString.Contains("24x24'"));
        }

        [TestMethod]
        public void ToLowerAlphabeticalPreservesForeignCharacters()
        {
            const string inString = "Μή μου ἅπτου";
            var outString = inString.ToLowerAlphabetical();
            Assert.AreEqual(outString, "ἅπτου μή μου");
        }

        [TestMethod]
        public void ToLowerWhiteSpaceNormalized()
        {
            string utf8bom = Encoding.UTF8.GetString(new byte[] { 239, 187, 191 });
            string inString = "  " + utf8bom + "    ABC   def" + utf8bom + " GHI " + utf8bom + " ";
            var outString = inString.ToLowerWhiteSpaceNormalized();
            Assert.AreEqual(outString, "abc def ghi");
        }

        [TestMethod]
        public void ToLowerWhiteSpaceNormalizedHandlesByteOrderMarkAtStart()
        {
            string inString = Encoding.UTF8.GetString(new byte[] { 239, 187, 191, 97, 98 });
            string outString = inString.ToLowerWhiteSpaceNormalized();
            byte[] outBytes = System.Text.Encoding.UTF8.GetBytes(outString);
            Assert.AreEqual(outBytes[0], 97);
            Assert.AreEqual(outBytes[1], 98);
            Assert.AreEqual(outBytes.Length, 2);
    }

        [TestMethod]
        public void ToLowerWhiteSpaceNormalizedHandlesByteOrderMarkAtEnd()
        {
            string inString = Encoding.UTF8.GetString(new byte[] { 97, 98, 239, 187, 191 });
            string outString = inString.ToLowerWhiteSpaceNormalized();
            byte[] outBytes = System.Text.Encoding.UTF8.GetBytes(outString);
            Assert.AreEqual(outBytes[0], 97);
            Assert.AreEqual(outBytes[1], 98);
            Assert.AreEqual(outBytes.Length, 2);
        }

        [TestMethod]
        public void ToLowerWhiteSpaceNormalizedHandlesByteOrderMarkInMiddle()
        {
            string inString = Encoding.UTF8.GetString(new byte[] { 97, 239, 187, 191, 98 });
            string outString = inString.ToLowerWhiteSpaceNormalized();
            byte[] outBytes = System.Text.Encoding.UTF8.GetBytes(outString);
            Assert.AreEqual(outBytes[0], 97);
            Assert.AreEqual(outBytes[1], 98);
            Assert.AreEqual(outBytes.Length, 2);
        }

        #endregion ToLower

        #region ToCamel

        [TestMethod]
        public void ToCamelCaseReturnsCamelCaseWordWithNoWhitespace()
        {
            var inString = "test phrase";
            var outString = inString.ToCamelCase();
            Assert.AreEqual("TestPhrase", outString);
        }

        [TestMethod]
        public void ToCamelCaseHandlesSingleCharWords()
        {
            var inString = "i a string";
            var outString = inString.ToCamelCase();
            Assert.AreEqual("IAString", outString);
        }

        [TestMethod]
        public void ToCamelCaseHandlesNumbers()
        {
            var inString = "1 to 2";
            var outString = inString.ToCamelCase();
            Assert.AreEqual("1To2", outString);
        }

        [TestMethod]
        public void ToCamelCaseHandlesPunctuation()
        {
            var inString = "1 :: n";
            var outString = inString.ToCamelCase();
            Assert.AreEqual("1::N", outString);
        }

        [TestMethod]
        public void ToCamelCaseHandlesMixedWhitespace()
        {
            var inString = "test     phrase";
            var outString = inString.ToCamelCase();
            Assert.AreEqual("TestPhrase", outString);
        }

        [TestMethod]
        public void ToCamelCaseHandlesMixedCasing()
        {
            var inString = "TestPhrase";
            var outString = inString.ToCamelCase();
            Assert.AreEqual("Testphrase", outString);
        }

        #endregion ToCamel
    }
}
