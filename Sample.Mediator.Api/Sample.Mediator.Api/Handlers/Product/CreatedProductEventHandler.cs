using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SimpleSoft.Mediator;

namespace Sample.Mediator.Api.Handlers.Product
{
    public class CreatedProductEventHandler : IEventHandler<CreatedProductEvent>
    {
        private readonly ILogger<CreatedProductEventHandler> _logger;

        public CreatedProductEventHandler(ILogger<CreatedProductEventHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(CreatedProductEvent evt, CancellationToken ct)
        {
            _logger.LogInformation("The product '{externalId}' has been created", evt.ExternalId);

            return Task.CompletedTask;
        }
    }

    public class CreatedProductEvent : Event
    {
        public Guid ExternalId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}