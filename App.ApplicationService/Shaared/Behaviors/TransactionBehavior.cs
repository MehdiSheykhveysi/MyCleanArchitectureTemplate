﻿using App.ApplicationService.Shaared.Attributes;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace App.ApplicationService.Shaared.Behaviors
{
    [ServiceMark]
    public class TransactionBehavior<TRequest, TResponse> :
       IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions,
                TransactionScopeAsyncFlowOption.Enabled))
            {
                TResponse response = await next();

                transaction.Complete();

                return response;
            }
        }
    }
}