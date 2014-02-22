using System;
using System.Configuration;
using Extensions;

namespace BadWordsTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var badWords = new BadWords( ConfigurationManager.AppSettings["BadWordsFile"] );
            
            string needle = "door knobs";

            try
            {
                Console.WriteLine("======================================================================");
                Console.WriteLine("Needle :: {0}", needle);
                Console.WriteLine("{0}", badWords.ContainsWord( needle )?"Bad words contains term.":"Term was not a bad word.");
                Console.WriteLine("======================================================================\r\n");
            }
            catch (Exception)
            {
            }

            Console.ReadLine();
        }
    }
}
