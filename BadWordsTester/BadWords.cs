using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Extensions;

namespace BadWordsTester
{
    public class BadWords
    {
        public SortedDictionary<string,int> SortedBadWords;

        #region Constructors
        public BadWords( string file )
        {
            var  newBadWords = new Dictionary<string, int>();
            String line;

            try
            {
                using (var sr = new StreamReader(file))
                {
                    while ( ( line = sr.ReadLine() ) != null )
                    {
                        if ( !newBadWords.ContainsKey( line ) )
                        {
                            newBadWords.Add( line, line.Split( ' ' ).Length );
                        }
                        else
                        {
                            Console.WriteLine("Duplicate: {0}", line);
                        }
                    }
                }

                SortedBadWords = new SortedDictionary<string, int>(RunBadWordsThroughPorterStem(newBadWords));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Reading file: {0}", file);
                Console.WriteLine(e.Message);
            }
        }

        public BadWords( Dictionary<string, int> badWords )
        {
            SortedBadWords = new SortedDictionary<string, int>( RunBadWordsThroughPorterStem( badWords ) );
        }

        #endregion Constructors

        #region Public

        public bool ContainsWord( string wordsToCheck )
        {
            string wordsStem = wordsToCheck.ToPorterStemNormalized();

            return SortedBadWords.ContainsKey( wordsStem );
        }

        #endregion Public

        #region Local

        private static Dictionary<string, int> RunBadWordsThroughPorterStem(Dictionary<string, int> badWords)
        {
            var dictionary = new Dictionary<string, int>();

            foreach ( KeyValuePair<string, int> word in badWords )
            {
                string s = word.Key.ToPorterStemNormalized();
                if ( !dictionary.ContainsKey( s ) )
                {
                    dictionary.Add( s, s.Split( ' ' ).Length );
                }
            }

            return dictionary;
        }

        #endregion Local
    }
}
