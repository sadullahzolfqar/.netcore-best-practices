using ExampleApp.CQRS.Commands.Request;
using ExampleApp.CQRS.Commands.Response;
using ExampleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.CQRS.Handlers.CommandHandlers
{
    public class CreateProductCommandHandler
    {
        private readonly AppDbContext dbContext;
        public CreateProductCommandHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public CreateProductCommandResponse CreateProduct(CreateProductCommandRequest createProductCommandRequest)
        {
            var id = Guid.NewGuid();
            dbContext.Products.Add(new()
            {
                Id = id,
                Name = createProductCommandRequest.Name,
                Price = createProductCommandRequest.Price,
                Quantity = createProductCommandRequest.Quantity,
                CreateTime = DateTime.Now
            });
            return new CreateProductCommandResponse
            {
                IsSuccess = true,
                ProductId = id
            };
        }
    }
}
