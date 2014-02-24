﻿using System;
using System.Configuration;
using System.Diagnostics;
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
                string needle = "door knobs";
                while ( (needle = Console.ReadLine()).Length > 0  )
                {
                    Console.WriteLine("======================================================================");
                    Console.WriteLine("Needle :: {0}", needle);
                    stopWatch.Restart();
                    Console.WriteLine("{0}", badWords.ContainsWord(needle) ? "String contains one or more bad terms." : "Term was not a bad word.");
                    stopWatch.Stop();
                    Console.WriteLine("Time to check word[{0}]: {1} ticks", needle, stopWatch.ElapsedTicks);
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
