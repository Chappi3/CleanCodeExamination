using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCodeExamination.Games
{
    [TestClass()]
    public class MooGameTests
    {
        private MooGame mooGame;

        [TestInitialize]
        public void Init()
        {
            mooGame = new MooGame();
        }

        [TestMethod()]
        public void CreateGoalTest()
        {
            var expectedNumberAmount = 4;

            var actualResult = mooGame.CreateGoal();

            Assert.IsTrue(expectedNumberAmount == actualResult.Length);
        }

        [TestMethod()]
        public void CheckGuessTestWithWrongNumbers()
        {
            var guess = "5678";
            var goal = "4321";
            var expected = ",";

            var (result, isCorrect) = mooGame.CheckGuess(goal, guess);

            Assert.AreEqual(expected, result);
            Assert.IsFalse(isCorrect);
        }

        [TestMethod()]
        public void CheckGuessTestWithHalfRightButWrongPlaceNumbers()
        {
            var guess = "3478";
            var goal = "4321";
            var expected = ",CC";

            var (result, isCorrect) = mooGame.CheckGuess(goal, guess);

            Assert.AreEqual(expected, result);
            Assert.IsFalse(isCorrect);
        }

        [TestMethod()]
        public void CheckGuessTestWithHalfCorrectNumbers()
        {
            var guess = "4378";
            var goal = "4321";
            var expected = "BB,";

            var (result, isCorrect) = mooGame.CheckGuess(goal, guess);

            Assert.AreEqual(expected, result);
            Assert.IsFalse(isCorrect);
        }

        [TestMethod()]
        public void CheckGuessTestWithCorrectNumbersButWrongPlace()
        {
            var guess = "1234";
            var goal = "4321";
            var expected = ",CCCC";

            var (result, isCorrect) = mooGame.CheckGuess(goal, guess);

            Assert.AreEqual(expected, result);
            Assert.IsFalse(isCorrect);
        }

        [TestMethod()]
        public void CheckGuessTestWithSomeRightSomeWrongNumbers()
        {
            var guess = "4312";
            var goal = "4321";
            var expected = "BB,CC";

            var (result, isCorrect) = mooGame.CheckGuess(goal, guess);

            Assert.AreEqual(expected, result);
            Assert.IsFalse(isCorrect);
        }

        [TestMethod()]
        public void CheckGuessTestWithCorrectNumbersOnTheRightPlace()
        {
            var guess = "4321";
            var goal = "4321";
            var expected = "BBBB,";

            var (result, isCorrect) = mooGame.CheckGuess(goal, guess);

            Assert.AreEqual(expected, result);
            Assert.IsTrue(isCorrect);
        }
    }
}