using CleanCodeExamination.Interfaces;
using System.IO;
using System;

namespace CleanCodeExamination.Views
{
    public class ConsoleIo : IStringIo
    {
        public void Clear()
        {
            try
            {
                Console.Clear();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public string Input()
        {
            return Console.ReadLine().Trim();
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
