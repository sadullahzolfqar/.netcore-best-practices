using ExampleApp.CQRS.Queries.Request;
using ExampleApp.CQRS.Queries.Response;
using ExampleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.CQRS.Handlers.QueryHandlers
{
    public class GetAllProductQueryHandler
    {
        private readonly AppDbContext dbContext;
        public GetAllProductQueryHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<GetAllProductQueryResponse> GetAllProduct(GetAllProductQueryRequest getAllProductQueryRequest)
        {
            return dbContext.Products.Select(product => new GetAllProductQueryResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CreateTime = product.CreateTime
            }).ToList();
        }
    }
}
