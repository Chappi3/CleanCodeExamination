namespace CleanCodeExamination.Interfaces
{
    public interface IStringIo
    {
        void Output(string value, bool isNewLine = true);
        string Input();
        void Clear();
    }
}
