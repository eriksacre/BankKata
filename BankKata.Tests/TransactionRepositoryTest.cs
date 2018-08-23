using System.Collections.Generic;
using BankKata.Domain;
using BankKata.External;
using NUnit.Framework;

namespace BankKata.Tests
{
    public class TransactionRepositoryTest
    {
        [Test]
        public void Add_EmptyTransactionRepo_AddsTransactionToRepo()
        {
            var transactionRepository = new TransactionRepository();

            transactionRepository.Add(new Transaction("01/02/2018", 100));
            
            Assert.That(transactionRepository.All(), Is.EqualTo(new List<Transaction>
            {
                new Transaction("01/02/2018", 100)
            }));
        }

        [Test]
        public void All_MultipleTransactionsInRepo_ReturnsTransactionsInInsertionOrder()
        {
            var transactionRepository = new TransactionRepository();
            transactionRepository.Add(new Transaction("01/01/2018", 100));
            transactionRepository.Add(new Transaction("02/01/2018", -50));

            var transactions = transactionRepository.All();
            
            Assert.That(transactions, Is.EqualTo(new List<Transaction>
            {
                new Transaction("01/01/2018", 100),
                new Transaction("02/01/2018", -50)
            }));
        }
    }
}