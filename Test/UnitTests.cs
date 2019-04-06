using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MyCompany
{
    using Pairings = SortedDictionary<int, string>; //Dictionary Type alias

    [TestFixture]
    public class UnitTest
    {
        private readonly Random rnd = new Random(42); // Using Seed for repeatable unit tests

        // Canonical 3 Fizz / 5 Buzz instance for testing convenience
        private readonly Pairings FizzBuzzExample = new Pairings { [3] = "Fizz", [5] = "Buzz" };

        #region Utility Functions
        private string GenerateRandomWord(int size)
        {
            // Generates a random sequence of lowercase characters
            int minChar = (int)'a';
            int maxChar = (int)'z' + 1; // Make z inclusive

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                sb.Append((char)rnd.Next(minChar, maxChar));
            }

            return sb.ToString();
        }

        private void PrintDictionary(Pairings pairings)
        {
            // Prints a generic dictionary to Trace
            Trace.Indent();
            foreach (KeyValuePair<int, string> kvp in pairings)
            {
                Trace.WriteLine($"{kvp.Key.ToString(),5} : {kvp.Value.ToString()}");
            }
            Trace.Unindent();
        }

        private Pairings GenerateRandomPairings(int numberOfPairingsMin = 1, int numberOfPairingsMax = 20, int denominatorMin = 1, int denominatorMax = 100)
        {
            // Generates random pairings and test for their presence
            // Numbers are not guaranteed to be unique, so final number will be <= numberOfPairingsMax

            int maxWordSize = 10;
            int numberOfPairings = rnd.Next(numberOfPairingsMin, numberOfPairingsMax);
            Pairings pairings = new Pairings();

            for (int i = 0; i < numberOfPairings; i++)
            {
                int number = rnd.Next(denominatorMin, denominatorMax);
                string word = GenerateRandomWord(maxWordSize);
                pairings[number] = word;
            }

            Trace.WriteLine("Testing with the following pairings:");
            PrintDictionary(pairings);

            return pairings;
        }
        #endregion

        [Test]
        public void TestNumberOfItems()
        {
            // Test expected number of items returned for all list sizes between 1 and 1000
            FizzBuzz fb = new FizzBuzz();
            for (int i = 1; i <= 1000; i++)
            {
                List<string> fbList = fb.GetFizzBuzz(i).ToList();
                Assert.IsTrue(fbList.Count == i);
            }
        }

        [Test]
        public void TestPureInt()
        {
            // Test that when no pairings are supplied, all returned values are integers
            FizzBuzz fb = new FizzBuzz();
            int index = 1;
            foreach (string str in fb.GetFizzBuzz())
            {
                bool isInteger = int.TryParse(str, out int number);
                Assert.IsTrue(isInteger, $"Expected integer for #{index}. Got '{str}' instead.");
                Assert.IsTrue(number == index, $"Expected {index}. Got '{str}' instead.");
                index++;
            }
        }

        [Test]
        public void TestPairings()
        {
            // Test with generated pairings
            Pairings pairings = GenerateRandomPairings();
            FizzBuzz fb = new FizzBuzz(pairings);
            int limit = 10000;
            int index = 1;
            foreach (string output in fb.GetFizzBuzz(limit))
            {
                bool PairingMatch = false;
                foreach (KeyValuePair<int, string> kvp in pairings)
                {
                    if (index % kvp.Key == 0)
                    {
                        PairingMatch = true;
                        Assert.IsTrue(output.Contains(kvp.Value), $"{index} is divisible by {kvp.Key} and should contain '{kvp.Value}' but does not. Found '{output}' instead.");
                    }
                }

                // If no pairing matched, test that output is a number
                if (!PairingMatch)
                {
                    bool isInteger = int.TryParse(output, out int number);
                    Assert.IsTrue(isInteger, $"Expected integer for #{index}. Got '{output}' instead.");
                    Assert.IsTrue(number == index, $"Expected {index}. Got '{output}' instead.");
                }
                index++;
            }
        }

        [Test]
        public void Test15()
        {
            // Test #15
            int ntest = 15;
            FizzBuzz fb = new FizzBuzz(FizzBuzzExample);
            List<string> fb1 = fb.GetFizzBuzz(ntest).ToList();
            string value = fb1[ntest - 1];
            Assert.IsTrue(value == "FizzBuzz", $"#{ntest} does not contain 'FizzBuzz'. Found '{value}'.");
        }

        [Test]
        public void TestLargeList()
        {
            // Test requesting a large list
            FizzBuzz fb = new FizzBuzz(FizzBuzzExample);
            IEnumerable<string> fb1 = fb.GetFizzBuzz(int.MaxValue);
            fb1.FirstOrDefault();
        }

        [Test]
        public void TestInvalidLimit()
        {
            // Test limit < 1
            FizzBuzz fb = new FizzBuzz(FizzBuzzExample);
            Assert.Catch<ArgumentOutOfRangeException>(() =>
            {
                IEnumerable<string> fb1 = fb.GetFizzBuzz(0);
                fb1.FirstOrDefault();
            });
        }
    }
}
