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
            var results = Solver.GetCombinations(tiles.ToList());

            ApprovalTests.Approvals.VerifyAll(results, "foo"); 
        }
        [Test]
        public void TwoTileTest2()
        {
            string[] tiles =
                {
                    "c", "d"
                };
            var results = Solver.GetCombinations(tiles.ToList());

            ApprovalTests.Approvals.VerifyAll(results, "foo");
        }
        [Test]
        public void TwoTileTest3()
        {
            string[] tiles =
                {
                    "a","b","c", "d"
                };
            var results = Solver.GetCombinations(tiles.ToList());

            ApprovalTests.Approvals.VerifyAll(results, "foo");
        }

        [Test]
        public void ScoreAbcTest()
        {
            var word = new List<string>(){"abc", "def"};
            var results = new Dictionary<string, int>();
            
            foreach (var w in word)
            {
                results[w]=Solver.GetScore(w);
            }
            ApprovalTests.Approvals.VerifyAll(results);
        }

        [Test]
        public void GetScoresForTilesTest()
        {
            string[] tiles =
                {
                    "a","b","c", "d"
                };
            var results = Solver.GetScoredAndRankedCombinations(tiles.ToList());

             ApprovalTests.Approvals.VerifyAll(results, "doo");

        }

    }
}
