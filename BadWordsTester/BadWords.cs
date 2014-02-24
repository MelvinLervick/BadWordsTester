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
        public SortedDictionary<string,int> SortedBadWords;

        #region Constructors
        public BadWords()
        {
            SortedBadWords = new SortedDictionary<string, int>();
        }

        public BadWords( Dictionary<string, int> badWords )
        {
            SortedBadWords = new SortedDictionary<string, int>( RunBadWordsThroughPorterStem( badWords ) );
        }

        #endregion Constructors

        #region Public

        public void GetBadWordsFromFile( string file )
        {
            var newBadWords = new Dictionary<string, int>();

            using (var sr = new StreamReader( file ))
            {
                String line;
                try
                {
                    while ( ( line = sr.ReadLine() ) != null )
                    {
                        if ( !newBadWords.ContainsKey( line ) )
                        {
                            newBadWords.Add( line, line.Split( ' ' ).Length );
                        }
                        else
                        {
                            Console.WriteLine( "Duplicate: {0}", line );
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error Reading file: {0}", file);
                    Console.WriteLine(e.Message);
                }
            }

            SortedBadWords = RunBadWordsThroughPorterStem( newBadWords );
        }

        public bool ContainsWord( string wordsToCheck )
        {
            string wordsStem = wordsToCheck.ToPorterStemNormalized();

            bool contains = SortedBadWords.ContainsKey( wordsStem );

            return contains;
        }

        #endregion Public

        #region Local

        private static SortedDictionary<string, int> RunBadWordsThroughPorterStem(Dictionary<string, int> badWords)
        {
            var dictionary = new SortedDictionary<string, int>();
            //var sw = new Stopwatch();

            foreach ( string word in badWords.Keys )
            {
                //sw.Restart();
                var s = word.ToPorterStemNormalized();
                //Console.Write("Porter: {0}   ::: ", sw.ElapsedMilliseconds);

                if ( !dictionary.ContainsKey( s ) )
                {
                    dictionary.Add( s, s.Split( ' ' ).Length );
                }
                //Console.WriteLine("Dictionary: {0}", sw.ElapsedMilliseconds);
            }

            return dictionary;
        }

        #endregion Local
    }
}
