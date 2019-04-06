using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany
{
    using Pairings = Dictionary<int, string>; //Dictionary Type alias

    class Program
    {
        static void Main(string[] args)
        {
            int limit = 100;
            if (args.Length == 1)
            {
                if (int.TryParse(args[0], out int i))
                {
                    limit = i;
                }
                else
                {
                    Console.WriteLine($"Unrecognized command line parameter: {args[0]}");
                    Environment.Exit(-1);
                }
            }
            else if (args.Length > 1)
            {
                Console.WriteLine($"At most one parameter can be specified");
                Environment.Exit(-1);
            }

            FizzBuzz fb = new FizzBuzz(new Pairings { [3] = "fizz", [5] = "buzz", [7] = "taco" });
            IEnumerable<string> fb1 = fb.GetFizzBuzz(limit);

            foreach (string s in fb1)
            {
                Console.WriteLine(s);
            }
        }
    }
}
