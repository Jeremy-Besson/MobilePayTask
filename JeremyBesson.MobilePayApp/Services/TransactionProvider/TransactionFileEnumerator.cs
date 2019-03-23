using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JeremyBesson.MobilePayApp.Models;
using JeremyBesson.MobilePayApp.Helpers;

namespace JeremyBesson.MobilePayApp.Services.TransactionProvider
{
    public class TransactionFileEnumerator : IEnumerator<Transaction>
    {
        private string _line;
        private readonly StreamReader _streamReader;

        public TransactionFileEnumerator(string fileName)
        {
            _streamReader = new StreamReader(fileName);
        }

        public bool MoveNext()
        {
            _line = _streamReader.ReadLine();
            return _line != null;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public Transaction Current
        {
            get
            {
                var transaction = TransactionConvertor.Convert(_line);
                return transaction;
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            _streamReader?.Dispose();
        }
    }
}
