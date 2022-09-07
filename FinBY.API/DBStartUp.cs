using FinBY.Domain.Entities;
using FinBY.Domain.Enum;
using FinBY.Infra.Context;
using System.Drawing;

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
        new TransactionType("Supermercado", Color.Aqua.ToArgb()),
        new TransactionType("Bebida", Color.Aquamarine.ToArgb()),
        new TransactionType("Casa", Color.LightGreen.ToArgb()),
        new TransactionType("Pessoal", Color.Salmon.ToArgb()),
        new TransactionType("Agua", Color.Orange.ToArgb()),
        new TransactionType("Luz", Color.LightBlue.ToArgb()),
        new TransactionType("Internet", Color.DarkBlue.ToArgb()),
        new TransactionType("Aluguel", Color.DarkGreen.ToArgb()),
        new TransactionType("Farmácia", Color.Goldenrod.ToArgb()),
        new TransactionType("Medico", Color.LightGray.ToArgb()),
        new TransactionType("Restaurante", Color.LightPink.ToArgb()),
        new TransactionType("Transporte", Color.LimeGreen.ToArgb()),
        new TransactionType("Novo AP", Color.MediumPurple.ToArgb()),
        new TransactionType("Outro", Color.MediumVioletRed.ToArgb()),
        new TransactionType("Férias", Color.Orange.ToArgb()),
        new TransactionType("Lazer", Color.OrangeRed.ToArgb()));

        dbContext.SaveChanges();

        List<Transaction> transactions = new List<Transaction>();
        for (int i = 1; i <= 100; i++)
        {
            Random random = new Random();   
            List<TransactionAmount> transactionAmounts = new List<TransactionAmount>() {
                        new TransactionAmount(0, 1, Convert.ToDecimal(random.Next(9) + 1) + Convert.ToDecimal(random.Next(99))/100),
                        new TransactionAmount(0, 2, Convert.ToDecimal(random.Next(9) + 1) + Convert.ToDecimal(random.Next(99))/100)
                    };

            var transaction = new Transaction(
                eTransactionFlow.Credit,
                random.Next(14) + 1
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
