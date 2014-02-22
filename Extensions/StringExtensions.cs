using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Snowball;
using Lucene.Net.QueryParsers;

namespace Extensions
{
    public static class StringExtensions
    {
        #region IsSimilarTo

        public static Single SimilarityThreshold = 0.75F;

        public static bool IsSimilarTo(this string string1, string string2)
        {
            return (string1.GetSimilarity(string2) > SimilarityThreshold);
        }

        public static float GetSimilarity(this string string1, string string2)
        {
            float retval = 0.0F;

            if (string.IsNullOrWhiteSpace(string1) || string.IsNullOrWhiteSpace(string2))
            {
                return retval;
            }

            if (string1.Equals(string2))
            {
                retval = 1.0F;
            }
            else
            {
                var maxLen = string1.Length;

                if (maxLen < string2.Length)
                {
                    maxLen = string2.Length;
                }

                if (maxLen == 0)
                {
                    retval = 1.0F;
                }
                else
                {
                    float dis = ComputeDistance(string1, string2);
                    retval = 1.0F - dis / maxLen;
                }
            }

            return retval;
        }

        private static int ComputeDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            var distance = new int[n + 1, m + 1]; // matrix

            if (n == 0)
            {
                return m;
            }
            if (m == 0)
            {
                return n;
            }

            //init1
            for (var i = 0; i <= n; distance[i, 0] = i++)
            {
            }
            for (var j = 0; j <= m; distance[0, j] = j++)
            {
            }

            //find min distance
            for (var i = 1; i <= n; i++)
            {
                for (var j = 1; j <= m; j++)
                {
                    var cost = (t.Substring(j - 1, 1) == s.Substring(i - 1, 1) ? 0 : 1);

                    distance[i, j] = Min3(distance[i - 1, j] + 1,
                      distance[i, j - 1] + 1,
                      distance[i - 1, j - 1] + cost);
                }
            }

            return distance[n, m];
        }

        static int Min3(int a, int b, int c)
        {
            return Math.Min(Math.Min(a, b), c);
        }

        #endregion IsSimilarTo

        #region Porter

        public const string PorterAnalyzerName = "Porter";

        public static string ToAlphabetical(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }

            var strTokens = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Array.Sort(strTokens);

            return string.Join(" ", strTokens).Trim();
        }

        public static string ToLowerAlphabetical(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }

            return str.ToLower().ToAlphabetical();
        }

        public static string ToStopwordNormalized(this string str)
        {
            str = str.ToLowerAlphabetical();

            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }

            var parse = new QueryParser(Lucene.Net.Util.Version.LUCENE_29, string.Empty, new StopAnalyzer(Lucene.Net.Util.Version.LUCENE_29));
            return parse.Parse(QueryParser.Escape(str)).ToString().Trim();
        }

        public static string ToPorterStemNormalized(this string str)
        {
            str = str.ToStopwordNormalized();

            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }

            var parse = new QueryParser(Lucene.Net.Util.Version.LUCENE_29, string.Empty, new SnowballAnalyzer(Lucene.Net.Util.Version.LUCENE_29, PorterAnalyzerName));
            return parse.Parse(QueryParser.Escape(str)).ToString().Trim();
        }

        #endregion Porter

        #region Misc String Extensions

        private static readonly String Utf7Preamble = Encoding.UTF8.GetString(Encoding.UTF7.GetPreamble());
        private static readonly String Utf8Preamble = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
        private static readonly String Utf32Preamble = Encoding.UTF8.GetString(Encoding.UTF32.GetPreamble());

        private static readonly Regex WhiteSpaceRegex = new Regex(@"\s+", RegexOptions.Compiled);
        private static readonly Regex ByteOrderMarkRegex = new Regex(@"(" + Utf8Preamble + "|" + Utf7Preamble + "|" + Utf32Preamble + ")+", RegexOptions.Compiled);

        public static string RemoveSuperfluousWhiteSpace(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }

            str = WhiteSpaceRegex.Replace(str, " ");
            str = str.TrimStart(' ').TrimEnd(' ');
            return str;
        }

        public static string RemoveByteOrderMarks(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }
            str = ByteOrderMarkRegex.Replace(str, "");
            return str;
        }

        public static string ToLowerWhiteSpaceNormalized(this string str)
        {
            return str.RemoveByteOrderMarks().RemoveSuperfluousWhiteSpace().ToLower();
        }

        public static string ReplaceWithNullCheck(this string str, string oldValue, string newValue)
        {
            var result = str ?? string.Empty;
            return result.Replace(oldValue, newValue);
        }

        public static string ToCamelCase(this string str)
        {
            var normalized = str.RemoveSuperfluousWhiteSpace();
            var parts = normalized.Split(' ');
            parts = parts.Select(x => x.Substring(0, 1).ToUpper() + x.Substring(1).ToLower()).ToArray();
            return parts.Aggregate((x, y) => x + y);
        }

        #endregion Misc String Extensions
    }
}
