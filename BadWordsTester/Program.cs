using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace BadWordsTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string needle = "this is a test";
            string haystack1 = "is test a this";
            string haystack2 = "a test is this";
            string haystack3 = "a is test this";
            string haystack4 = "this is test";

            try
            {
                Console.WriteLine("======================================================================");
                Console.WriteLine(string.Format("Needle :: {0}", needle));
                Console.WriteLine(string.Format("{0} :: {1} :: {2}", haystack1, haystack1.IsSimilarTo(needle), haystack1.GetSimilarity(needle)));
                Console.WriteLine(string.Format("{0} :: {1} :: {2}", haystack2, haystack2.IsSimilarTo(needle), haystack2.GetSimilarity(needle)));
                Console.WriteLine(string.Format("{0} :: {1} :: {2}", haystack3, haystack3.IsSimilarTo(needle), haystack3.GetSimilarity(needle)));
                Console.WriteLine(string.Format("{0} :: {1} :: {2}", haystack4, haystack4.IsSimilarTo(needle), haystack4.GetSimilarity(needle)));
                Console.WriteLine("======================================================================\r\n");
            }
            catch (Exception)
            {
            }

            Console.ReadLine();
        }
    }
}
