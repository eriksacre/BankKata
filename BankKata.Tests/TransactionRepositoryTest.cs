using NUnit.Framework;

namespace BankKata.Tests
{
    public class TransactionRepositoryTest
    {
        [Test]
        public void Add_EmptyTransactionRepo_AddsTransactionToRepo()
        {
            var transactionRepository = new TransactionRepository();
            var transaction = new Transaction("01/02/2018", 100);
            
            transactionRepository.Add(transaction);
            
            Assert.That(transactionRepository.All().Count, Is.EqualTo(1));
            Assert.That(transactionRepository.All()[0], Is.EqualTo(transaction));
        }
    }
}