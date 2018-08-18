namespace BankKata
{
    public class Console : IConsole
    {
        public void PrintLine(string text)
        {
            System.Console.WriteLine(text);
        }
    }
}