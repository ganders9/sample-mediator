using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sample.Mediator.Api.Database;
using Sample.Mediator.Api.Database.Entities;
using Sample.Mediator.Api.Models.Product;
using SimpleSoft.Mediator;

namespace Sample.Mediator.Api.Handlers.Product
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductModel>
    {
        private readonly IQueryable<ProductEntity> _products;

        public GetProductByIdQueryHandler(ApiDbContext context)
        {
            _products = context.Set<ProductEntity>();
        }

        public async Task<ProductModel> HandleAsync(GetProductByIdQuery query, CancellationToken ct)
        {
            var product = await _products.SingleOrDefaultAsync(p => p.ExternalId == query.ProductId, ct);

            if (product == null)
            {
                throw new InvalidOperationException($"Product '{query.ProductId}' not found");
            }

            return new ProductModel
            {
                Id = product.ExternalId,
                Code = product.Code,
                Name = product.Name,
                Price = product.Price
            };
        }
    }

    public class GetProductByIdQuery : Query<ProductModel>
    {
        public Guid ProductId { get; set; }
    }
}