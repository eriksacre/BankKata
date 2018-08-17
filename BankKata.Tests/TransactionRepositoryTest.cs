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

        [Test]
        public void All_MultipleTransactionsInRepo_ReturnsTransactionsInInsertionOrder()
        {
            var transactionRepository = new TransactionRepository();
            var transaction1 = new Transaction("01/01/2018", 100);
            var transaction2 = new Transaction("02/01/2018", -50);
            transactionRepository.Add(transaction1);
            transactionRepository.Add(transaction2);

            var transactions = transactionRepository.All();
            
            Assert.That(transactions.Count, Is.EqualTo(2));
            Assert.That(transactions[0], Is.EqualTo(transaction1));
            Assert.That(transactions[1], Is.EqualTo(transaction2));
        }
    }
}