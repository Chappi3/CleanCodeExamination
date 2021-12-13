using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCodeExamination.Games.Tests
{
    [TestClass()]
    public class MasterMindGameTests
    {
        private MasterMindGame masterMindGame;

        [TestInitialize]
        public void Init()
        {
            masterMindGame = new MasterMindGame();
        }

        [TestMethod()]
        public void CreateGoalTest()
        {
            var expectedNumberAmount = 4;

            var actualReslut = masterMindGame.CreateGoal();

            Assert.IsTrue(expectedNumberAmount == actualReslut.Length);
        }

        [TestMethod()]
        public void CheckGuessTestWithWrongNumbers()
        {
            var guess = "5678";
            var goal = "4321";
            var expected = ",";

            var (result, isCorrect) = masterMindGame.CheckGuess(goal, guess);

            Assert.AreEqual(expected, result);
            Assert.IsFalse(isCorrect);
        }
    }
}