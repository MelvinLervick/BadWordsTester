using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using Extensions;

namespace BadWordsTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var badWords = new BadWords();
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            badWords.GetBadWordsFromFile(ConfigurationManager.AppSettings["BadWordsFile"]);
            stopWatch.Stop();

            Console.WriteLine("Time to set up Sorted Dictionary: {0}", stopWatch.ElapsedMilliseconds);

            try
            {
                var needles = new List<string>();
                var needle = "door knobs";
                while ( (needle = Console.ReadLine()).Length > 0  )
                {
                    //needles.Clear();
                    //needles.Add(needle);

                    needles = new List<string>
                    {
                        "xrated", "now is the time", "very bad", "bad very word", "small men", "small boy",
                        "the small girls",
                        "the large boys",
                        "xrated1", "now is the time1", "very bad2", "bad very word1", "small men1", "small boy1",
                        "the small girls1",
                        "the large boys1",
                        "xrated2", "now is the time2", "very bad3", "bad very word2", "small men2", "small boy2",
                        "the small girls2",
                        "the large boys2",
                        "xrated3", "now is the time3", "very bad3", "bad very word3", "small men3", "small boy3",
                        "the small girls3",
                        "the large boys3",
                        "xrated4", "now is the time4", "very bad4", "bad very word4", "small men4", "small boy4",
                        "the small girls4",
                        "the large boys4",
                        "xrated5", "now is the time5", "very bad5", "bad very word5", "small men5", "small boy5",
                        "the small girls5",
                        "the large boys5",
                        "xrated6", "now is the time6", "very bad6", "bad very word6", "small men6", "small boy6",
                        "the small girls6",
                        "the large boys6",
                        "xrated7", "now is the time7", "very bad7", "bad very word7", "small men7", "small boy7",
                        "the small girls7",
                        "the large boys7",
                        "xrated8", "now is the time8", "very bad8", "bad very word8", "small men8", "small boy8",
                        "the small girls8",
                        "the large boys8",
                        "the large men are best"
                    };
                    Console.WriteLine("======================================================================");
                    Console.WriteLine("Needle :: {0}", needle);
                    stopWatch.Restart();
                    var filtered = badWords.ContainsBadWordFilter(needles);
                    Console.WriteLine("{0}",
                        filtered.Any()
                            ? string.Format("String contains one or more bad terms[{0}].", filtered.Count() )
                            : "No bad Terms were found.");
                    stopWatch.Stop();
                    Console.WriteLine("Time to check word[{0}]: {1} ms", needle, stopWatch.ElapsedMilliseconds);
                    Console.WriteLine("======================================================================\r\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
