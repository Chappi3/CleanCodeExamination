using CleanCodeExamination.Interfaces;
using System;

namespace CleanCodeExamination.Views
{
    public class ConsoleIo : IStringIo
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void Exit()
        {
            Environment.Exit(0);
        }

        public string Input()
        {
            return Console.ReadLine();
        }

        public void Output(string value, bool isNewLine)
        {
            if (isNewLine)
            {
                Console.WriteLine(value);
            }
            else
            {
                Console.Write(value);
            }
        }
    }
}
