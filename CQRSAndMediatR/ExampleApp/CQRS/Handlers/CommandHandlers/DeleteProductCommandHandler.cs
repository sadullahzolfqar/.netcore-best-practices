using ExampleApp.CQRS.Commands.Request;
using ExampleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApp.CQRS.Handlers.CommandHandlers
{
    public class DeleteProductCommandHandler
    {
        private readonly AppDbContext dbContext;
        public DeleteProductCommandHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DeleteProductCommandResponse DeleteProduct(DeleteProductCommandRequest deleteProductCommandRequest)
        {
            var deleteProduct = dbContext.Products.FirstOrDefault(p => p.Id == deleteProductCommandRequest.Id);
            dbContext.Products.Remove(deleteProduct);
            return new DeleteProductCommandResponse
            {
                IsSuccess = true
            };
        }
    }
}
