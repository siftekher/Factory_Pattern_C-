using System;
using System.Collections.Generic;
using System.Text;

namespace WDTA1
{
    class FactoryAccount
    {
        abstract class aAccount
        {
            public double Balance { get; set; }

            public void Deposit(int accountNumber, Double amount)
            {

                TalkDB dbObj = new TalkDB();

                DateTime saveNow = DateTime.Now;

                DateTime saveUtcNow = DateTime.UtcNow;
                String logQuery = $"INSERT INTO Transaction ('TransactionType','AccountNumber','Amount','TransactionTimeUtc') VALUES('D', '{accountNumber}', '{amount}', '{saveUtcNow}'); ";

                int rowEffect = dbObj.DbUID(logQuery);
            }
            public abstract void WithdrawMoney(int accountNumber, double amount);

        }

        class Savings : aAccount
        {
            public const double MINIMUM_BALANCE = 0.00;
            public override void WithdrawMoney(int accountNumber, double amount)
            {
                //Withdraw call Here
            }
        }

        class Checking : aAccount
        {
            public const double MINIMUM_BALANCE = 200.00;
            public override void WithdrawMoney(int accountNumber, double amount)
            {
                //Withdraw call Here
            }
        }

        static class Factory
        {
            public static aAccount GetAccountType(String accountType)
            {
                switch (accountType)
                {
                    case "C":
                        return new Checking();
                    case "S":
                        return new Savings();
                    default:
                        return new Checking();
                }
            }

        }

        public void Deposit(int accountNumber, Double amount, String accountType)
        {
            TalkDB dbObj = new TalkDB();

            var accountObj = Factory.GetAccountType(accountType);
            accountObj.Deposit(accountNumber, amount);
        }


        public void Withdraw(int accountNumber, Double amount, String accountType)
        {
            TalkDB dbObj = new TalkDB();

            var accountObj = Factory.GetAccountType(accountType);
            accountObj.WithdrawMoney(accountNumber, amount);
        }


    }
}
