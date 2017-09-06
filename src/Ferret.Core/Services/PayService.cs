using Ferret.Core.CommandParser.Options;
using Ferret.Core.DependencyInjection;
using Microtef.Core;
using Microtef.Core.Authorization;
using Pinpad.Sdk.Model;
using Poi.Sdk.Authorization.Report;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ferret.Core.Services
{
    internal sealed class PayService : CoreService<PayOption>, IPinpadService
    {
        public override string CommandName => "pay";

        static public List<IAuthorizationReport> Transactions => new List<IAuthorizationReport>();

        public override void ConcreteExecute()
        {
            var option = this.Options as PayOption;
            var authorizer = IoC.Container.Resolve<ICollection<ICardPaymentAuthorizer>>()
                .FirstOrDefault();

            if (option.ShowTransactions == true)
            {
                PayService.Transactions.ShowSummary();
                return;
            }

            ITransactionEntry transaction = new TransactionEntry
            {
                Amount = (decimal) option.Amount,
                CaptureTransaction = true,
                InitiatorTransactionKey = option.InitiatorTransactionKey
            };
            ResponseStatus responseStatus;

            var authorizationResponse = authorizer.Authorize(transaction, out responseStatus);
            
            if (authorizationResponse.WasSuccessful == true)
            {
                authorizer.Cancel(authorizationResponse.AcquirerTransactionKey, authorizationResponse.Amount);
                PayService.Transactions.Add(authorizationResponse);
                Console.WriteLine("Transaction approved.");
                Console.WriteLine($"ATK: {authorizationResponse.AcquirerTransactionKey}");
            }
            else if (authorizationResponse == null)
            {
                Console.WriteLine("An unexpected error occurred.");
                Console.WriteLine($"ERROR: {responseStatus.ToString()}, {responseStatus.GetHashCode()}.");
            }
            else
            {
                Console.WriteLine("The transaction was declined.");

                if (string.IsNullOrEmpty(authorizationResponse.ResponseCode) == false)
                {
                    Console.WriteLine($"ERROR: {authorizationResponse.ResponseReason}, {authorizationResponse.ResponseCode}.");
                }
                if (responseStatus != ResponseStatus.Undefined)
                {
                    Console.WriteLine($"RESPONSE STATUS: {responseStatus.ToString()}, {responseStatus.GetHashCode()}.");
                }
            }
        }
    }
}
