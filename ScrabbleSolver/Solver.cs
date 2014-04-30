using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScrabbleSolver
{
    public static class Solver{
        public static IEnumerable<string> GetCombinations(List<string> tiles)
        {
            var combinations = RecurseTiles(tiles);
            return combinations;
        }

        private static IEnumerable<string> RecurseTiles(List<string> tiles)
        {
            var combinations=new List<string>();
            for (int j = 0; j < tiles.Count(); j++)
            {
                combinations.Add(tiles[j]);
                var smallerList = tiles.Except(new List<string>(){tiles[j]});
                // smallerList.Remove(smallerList[j]);
                if (smallerList.Count() > 0)
                {
                    var therest = RecurseTiles(smallerList.ToList());
                    foreach (var combo in therest)
                    {
                        combinations.Add(tiles[j] + combo);
                    }
                }
            }
            return combinations;

        }

        public static int GetScore(string word)
        {
            char[] charArray = word.ToUpper().ToCharArray();
            int finalResult = 0;

            foreach (var c in charArray)
            {
                finalResult += TileValues.TileVals[c];
            }

            return finalResult;
        }

        public static List<KeyValuePair<string, int>> GetScoredAndRankedCombinations(List<string> wordList)
        {
            var results = new List<KeyValuePair<string, int>>();

            var words = GetCombinations(wordList);
            foreach (string word in words)
            {
                int score = GetScore(word);
                results.Add(new KeyValuePair<string, int>(word,score));
            }

            return results.OrderByDescending(x=>x.Value).ToList();
        }

        public static List<string> ReadCSV()
        {
            string csvContent = File.ReadAllText("word_list.csv");
            csvContent = csvContent.Replace("\n", "").Replace("\r", "");
            return csvContent.Split(',').ToList();
        }

        public static List<KeyValuePair<string, int>> GetValidScrabbleWords(List<string> tileSet)
        {
            var candidates = GetScoredAndRankedCombinations(tileSet);

            var validWords = ReadCSV();

            var results = new List<KeyValuePair<string, int>>();

            foreach (KeyValuePair<string, int> candidate in candidates)
            {
                if (validWords.Any(x => x == candidate.Key))
                {
                    results.Add(candidate);
                }
            }

            return results;
        }
    }
}