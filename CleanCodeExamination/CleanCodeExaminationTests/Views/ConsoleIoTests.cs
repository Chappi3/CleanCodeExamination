using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleanCodeExamination.Interfaces;
using System.IO;
using System;

namespace CleanCodeExamination.Views
{
    [TestClass()]
    public class ConsoleIoTests
    {
        IStringIo ui;
        StringWriter stringWriter;
        StringReader stringReader;

        [TestInitialize]
        public void Init()
        {
            ui = new ConsoleIo();
            stringWriter = new StringWriter();
        }

        [TestMethod()]
        public void ClearTestException()
        {
            var testOutput = "Testing! Testing!";
            Console.SetOut(stringWriter);

            ui.Output(testOutput);
            ui.Clear();

            var output = stringWriter.ToString();
            Assert.AreEqual($"{testOutput}\r\nThe handle is invalid.\r\n", output);
        }

        [TestMethod()]
        public void InputTest()
        {
            var testInput = "Testing! Testing!";
            stringReader = new StringReader(testInput);
            Console.SetIn(stringReader);

            var input = ui.Input();

            Assert.AreEqual($"{testInput}", input);
        }

        [TestMethod()]
        public void OutputTestWrite()
        {
            var testWrite1 = "Test1";
            var testWrite2 = "Test2";
            Console.SetOut(stringWriter);

            ui.Output(testWrite1, false);
            ui.Output(testWrite2, false);

            var output = stringWriter.ToString();
            Assert.AreEqual($"{testWrite1}{testWrite2}", output);
        }

        [TestMethod()]
        public void OutputTestWriteLine()
        {
            var testWriteLine1 = "Test1";
            var testWriteLine2 = "Test2";
            Console.SetOut(stringWriter);

            ui.Output(testWriteLine1);
            ui.Output(testWriteLine2);

            var output = stringWriter.ToString();
            Assert.AreEqual($"{testWriteLine1}\r\n{testWriteLine2}\r\n", output);
        }
    }
}