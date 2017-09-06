using Poi.Sdk.Authorization.Report;
using System;
using System.Collections.Generic;
using MarkdownLog;
using System.Linq;

namespace Ferret.Core
{
    static internal class TransactionEntryExtension
    {
        static public void ShowSummary(this List<IAuthorizationReport> transactions)
        {
            Console.WriteLine(
                transactions.Select(t => new
                {
                    DateTime = t.DateTime,
                    ATK = t.AcquirerTransactionKey,
                    ITK = t.InitiatorTransactionKey,
                    TransactionType = t.TransactionType.ToString(),
                    CardBrand = t.Card.BrandName
                })
                .ToMarkdownTable()
            );
        }
    }
}
