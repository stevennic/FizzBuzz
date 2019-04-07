using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCompany
{
    using Pairings = SortedDictionary<int, string>; //Dictionary Type alias

    public class FizzBuzz
    {
        private readonly Pairings pairings;

        public FizzBuzz()
        {
            // Zero pairings essentially makes it an integer generator.
            pairings = new Pairings();
        }

        public FizzBuzz(Pairings customPairings)
        {
            var invalidKeys = customPairings.Keys.Where(key => key < 1);
            if (invalidKeys.Count() > 0)
            {
                int firstInvalidKey = invalidKeys.First();
                throw new ArgumentOutOfRangeException(nameof(customPairings), firstInvalidKey, $"Pairing keys must be positive integers.");
            }

            pairings = customPairings;
        }

        public IEnumerable<string> GetFizzBuzz(int limit = 100)
        {
            if (limit < 1)
                throw new ArgumentOutOfRangeException(nameof(limit), limit, $"Limit must be >= 1.");

            for (int i = 1; i <= limit; i++)
            {
                StringBuilder sb = new StringBuilder();

                foreach (KeyValuePair<int, String> kvp in pairings)
                {
                    if (i % kvp.Key == 0)
                        sb.Append(kvp.Value);
                }

                string OutputValue = sb.ToString();
                if (OutputValue.Length == 0)
                    OutputValue = i.ToString();

                yield return OutputValue;
            }
        }
    }
}
