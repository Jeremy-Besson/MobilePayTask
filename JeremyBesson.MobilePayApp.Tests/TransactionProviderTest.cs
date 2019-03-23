using JeremyBesson.MobilePayApp.Helpers;
using JeremyBesson.MobilePayApp.Models;
using JeremyBesson.MobilePayApp.Services.TransactionProvider;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace JeremyBesson.MobilePayApp.Tests
{
    public class TransactionFileEnumeratorTest
    {
        private readonly List<Transaction> _transactions;
        private readonly string _fileName = "test.txt";

        public TransactionFileEnumeratorTest()
        {
            _transactions = new List<Transaction>()
            {
                new Transaction(){Date = new DateTime(2018,12,10),MerchantName = "M1",Amount = 20},
                new Transaction(){Date = new DateTime(2019,5,10),MerchantName = "M2",Amount = 100},
                new Transaction(){Date = new DateTime(2019,5,15),MerchantName = "M2",Amount = 120},
                new Transaction(){Date = new DateTime(2019,5,15),MerchantName = "TELIA",Amount = 120},
            };
            List<string> trLines = new List<string>();
            _transactions.ForEach(
                transaction =>
                {
                    trLines.Add(TransactionConvertor.Convert(transaction));
                }
                );
            File.WriteAllLines(_fileName, trLines);
        }

        [Fact]
        public void WhenWrongFile_FileNotFoundException()
        {
            var exception = Record.Exception(() => new TransactionFileEnumerator("NOTFOUND.txt"));
            Assert.NotNull(exception);
            Assert.IsType<FileNotFoundException>(exception);
        }

        [Fact]
        public void GetTransactionFromProvider_RightNumber()
        {
            // Arrange
            //Act
            int c = 0;
            using (var transactions = new TransactionFileEnumerator(_fileName))
            {
                while (transactions.MoveNext())
                {
                    c++;
                }
            }
            //Assert
            Assert.Equal(4, c);
        }

        [Fact]
        public void GetTransactionFromProvider_RightValues()
        {
            // Arrange
            //Act
            List<Transaction> expectedTransactions = new List<Transaction>();
            List<Transaction> realTransactions = new List<Transaction>();

            int c = 0;
            using (var transactions = new TransactionFileEnumerator(_fileName))
            {
                while (transactions.MoveNext())
                {
                    realTransactions.Add(transactions.Current);
                    expectedTransactions.Add(_transactions[c]);
                    c++;
                }
            }
            //Assert
            Assert.Equal(expectedTransactions.Count, c);

            for (int i = 0; i < expectedTransactions.Count; i++)
            {
                var expectedTransaction = expectedTransactions[i];
                var realTransaction = realTransactions[i];
                Assert.Equal(expectedTransaction.Amount, realTransaction.Amount);
                Assert.Equal(expectedTransaction.Date.Year, realTransaction.Date.Year);
                Assert.Equal(expectedTransaction.Date.Month, realTransaction.Date.Month);
                Assert.Equal(expectedTransaction.Date.Day, realTransaction.Date.Day);
                Assert.Equal(expectedTransaction.MerchantName, realTransaction.MerchantName);
            }
        }
    }
}
