using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Extensions;

namespace BadWordsTester
{
    public class BadWords
    {
        public List<string> BadWordsList;

        #region Constructors
        public BadWords()
        {
            BadWordsList = new List<string>();
        }

        public BadWords( List<string> badWords )
        {
            BadWordsList = badWords;
        }

        #endregion Constructors

        #region Public

        public void GetBadWordsFromFile( string file )
        {
            using (var sr = new StreamReader( file ))
            {
                try
                {
                    String line;
                    while ( ( line = sr.ReadLine() ) != null )
                    {
                        BadWordsList.Add( line );
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error Reading file: {0}", file);
                    Console.WriteLine(e.Message);
                }
            }
        }

        public IEnumerable<string> ContainsBadWordFilter(IEnumerable<string> suspectKeywords)
        {
            var filterKeywords = BadWordsList;

            if (filterKeywords.Count == 0) return Enumerable.Empty<string>();


            var filterMap = GenerateFilterMap(filterKeywords);


            var filteredKeywords =
                suspectKeywords.Select(x => new PorterStemTokenizer(x))
                    .Where(x => x.KeywordFilterMatch(filterMap))
                    .ToDictionary(x => x.Original, y => y.Filter);


            if (filteredKeywords.Count > 0)
            {
                //filteredKeywordRegistrar.LogKeywordsFiltered(filteredKeywords, FilteredKeywordFilterTypes.FuzzyContains);
            }


            return filteredKeywords.Keys;
        }

        //public bool ContainsBadWordFilter( string wordsToCheck )
        //{
        //    var wordsStem = wordsToCheck.ToPorterStemNormalized();

        //    var contains = true;
        //    var wordsStemList = wordsStem.Split(' ').GroupBy(x => x).ToDictionary(x => x.Key, y => y.Count());

        //    if ( wordsStemList.Count == 1 )
        //    {
        //        contains = BadWordsList.ContainsKey( wordsStemList.First().Key );
        //    }
        //    else
        //    {
        //        foreach ( var sortedBadWord in BadWordsList.Keys )
        //        {
        //            var badwords = sortedBadWord.Split( ' ' ).ToList();
        //            if (wordsStemList.Count < badwords.Count || string.IsNullOrWhiteSpace(sortedBadWord)) continue;
        //            contains = true;
        //            foreach (var word in badwords)
        //            {
        //                contains &= wordsStemList.ContainsKey(word);
        //                if ( !contains ) break;
        //            }
        //            if ( contains ) break;
        //        }
        //    }

        //    return contains;
        //}

        #endregion Public

        #region Local

        private IDictionary<string, IEnumerable<PorterStemTokenizer>> GenerateFilterMap(IEnumerable<string> filterKeywords)
        {
            var tokenizedFilters = filterKeywords.Select(x => new PorterStemTokenizer(x)).ToList();
            var expanded = tokenizedFilters.SelectMany(AssociateTokenizerToTokens);
            var grouped = CollapseAssociatedTokenizers(expanded);
            return grouped;
        }


        private IEnumerable<KeyValuePair<string, PorterStemTokenizer>> AssociateTokenizerToTokens(
            PorterStemTokenizer tokenizer)
        {
            return tokenizer.Tokens.Keys.ToDictionary(token => token, value => tokenizer);
        }


        private IDictionary<string, IEnumerable<PorterStemTokenizer>> CollapseAssociatedTokenizers(
            IEnumerable<KeyValuePair<string, PorterStemTokenizer>> associations)
        {
            var grouped = associations.GroupBy(association => association.Key, association => association.Value);
            return grouped.ToDictionary(group => group.Key, group => group.ToList().AsEnumerable());
        }


        private class PorterStemTokenizer
        {
            public readonly string Original;
            public string Filter;
            public readonly IDictionary<string, int> Tokens;


            public PorterStemTokenizer(string s)
            {
                Original = s;
                Tokens =
                    s.ToPorterStemNormalized().Split(' ').GroupBy(x => x).ToDictionary(x => x.Key, y => y.Count());
            }


            public bool KeywordFilterMatch(IDictionary<string, IEnumerable<PorterStemTokenizer>> tokenizedFilters)
            {
                var narrowedFilters = NarrowFilters(tokenizedFilters);
                return narrowedFilters.Any(KeywordMatchesFilterItem);
            }


            private IEnumerable<PorterStemTokenizer> NarrowFilters(IDictionary<string, IEnumerable<PorterStemTokenizer>> tokenizedFilters)
            {
                var filtersForSuspectTokens =
                    Tokens.Keys.Where(tokenizedFilters.ContainsKey).Select(x => tokenizedFilters[x]);

                var smallestFilterSet =
                    filtersForSuspectTokens.Where(x => x.Any()).OrderBy(x => x.Count()).FirstOrDefault();


                return smallestFilterSet ?? Enumerable.Empty<PorterStemTokenizer>();
            }


            private bool KeywordMatchesFilterItem(PorterStemTokenizer tokenizedFilter)
            {
                Filter = tokenizedFilter.Original;
                var matched =
                    tokenizedFilter.Tokens.All(
                        x =>
                            Tokens.ContainsKey(x.Key) &&
                            Tokens[x.Key] >= tokenizedFilter.Tokens[x.Key]);


                return matched;
            }
        }

        #endregion Local
    }
}
