using ExampleApp.CQRS.Queries.Request;
using ExampleApp.CQRS.Queries.Response;
using ExampleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.CQRS.Handlers.QueryHandlers
{
    public class GetByIdProductQueryHandler
    {
        private readonly AppDbContext dbContext;
        public GetByIdProductQueryHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public GetByIdProductQueryResponse GetByIdProduct(GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            var product = dbContext.Products.FirstOrDefault(p => p.Id == getByIdProductQueryRequest.Id);
            return new GetByIdProductQueryResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CreateTime = product.CreateTime
            };
        }
    }
}
