using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ScrabbleSolver
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class ScrabbleSolverTests
    {
        [Test]
        public void TwoTileTest()
        {
            string[] tiles =
                {
                    "a", "b"
                };
            var results = Solver.getCombinations(tiles.ToList());

            ApprovalTests.Approvals.VerifyAll(results, "foo"); 
        }
        [Test]
        public void TwoTileTest2()
        {
            string[] tiles =
                {
                    "c", "d"
                };
            var results = Solver.getCombinations(tiles.ToList());

            ApprovalTests.Approvals.VerifyAll(results, "foo");
        }
        [Test]
        public void TwoTileTest3()
        {
            string[] tiles =
                {
                    "a","b","c", "d"
                };
            var results = Solver.getCombinations(tiles.ToList());

            ApprovalTests.Approvals.VerifyAll(results, "foo");
        }
    }

    public static class Solver{
        public static IEnumerable<String> getCombinations(List<string> tiles)
        {
            var combinations = RecurseTiles(tiles);
            return combinations;
        }

        private static List<string> RecurseTiles(List<string> tiles)
        {
            List<string> combinations=new List<string>();
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
    }
}
