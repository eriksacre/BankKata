using BankKata.Domain;

namespace BankKata.External
{
    public class Console : IConsole
    {
        public void PrintLine(string text)
        {
            System.Console.WriteLine(text);
        }
    }
}