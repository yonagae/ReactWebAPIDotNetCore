using FinBY.Domain.Entities;
using FinBY.Domain.Enum;
using FinBY.Infra.Context;

namespace FinBY.API;

public static class DBStartUp
{
    public static void StartupBase(ApplicationDbContext dbContext)
    {
        //return;

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        dbContext.AddRange(
          new User("Jonh Main", "main", "24-0B-E5-18-FA-BD-27-24-DD-B6-F0-4E-EB-1D-A5-96-74-48-D7-E8-31-C0-8C-8F-A8-22-80-9F-74-C7-20-A9", "h9lzVOoLlBoTbcQrh/e16/aIj+4p6C67lLdDbBRMsjE=", DateTime.Now.AddYears(1)),
          new User("Batman", "batman", "24-0B-E5-18-FA-BD-27-24-DD-B6-F0-4E-EB-1D-A5-96-74-48-D7-E8-31-C0-8C-8F-A8-22-80-9F-74-C7-20-A9", "h9lzVOoLlBoTbcQrh/e16/aIj+4p6C67lLdDbBRMsjE=",  DateTime.Now.AddYears(1))
         );

        dbContext.AddRange(
          new TransactionType("Casa"),
          new TransactionType("Mercado"),
          new TransactionType("Pessoal"),
          new TransactionType("Luz"),
          new TransactionType("Agua"));

        dbContext.SaveChanges();

        List<Transaction> transactions = new List<Transaction>();
        for (int i = 1; i <= 10; i++)
        {
            List<TransactionAmount> transactionAmounts = new List<TransactionAmount>()
                    {
                        new TransactionAmount(0, 1, 1.0m),
                        new TransactionAmount(0, 2, 2.0m)
                    };
            var transaction = new Transaction(
                eTransactionFlow.Credit,
                (i % 3) + 1
                , (i % 2) + 1
                , new DateTime(2022, 01, 01).AddDays(i)
                , $"Gasto número {i}"
                , $"Gasto {i}"
                );
            transaction.AddAmounts(transactionAmounts);
            transactions.Add(transaction);
        }

        dbContext.AddRange(transactions);
        dbContext.SaveChanges();
    }
}
