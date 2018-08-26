using System.Collections.Generic;
using BankKata.Domain;
using NUnit.Framework;

namespace BankKata.Tests
{
    public abstract class TransactionRepositoryBaseTest
    {
        protected abstract ITransactionRepository NewRepository();

        [Test]
        public void Add_EmptyTransactionRepo_AddsTransactionToRepo()
        {
            var transactionRepository = NewRepository();

            transactionRepository.Add(new Transaction("01/02/2018", 100));
            
            Assert.That(transactionRepository.AllOrderedByInsertionDate(), Is.EqualTo(new List<Transaction>
            {
                new Transaction("01/02/2018", 100)
            }));
        }

        [Test]
        public void AllOrderedByInsertionDate_MultipleTransactionsInRepo_ReturnsTransactionsInInsertionOrder()
        {
            var transactionRepository = NewRepository();
            transactionRepository.Add(new Transaction("01/01/2018", 100));
            transactionRepository.Add(new Transaction("02/01/2018", -50));

            var transactions = transactionRepository.AllOrderedByInsertionDate();
            
            Assert.That(transactions, Is.EqualTo(new List<Transaction>
            {
                new Transaction("01/01/2018", 100),
                new Transaction("02/01/2018", -50)
            }));
        }
    }
}