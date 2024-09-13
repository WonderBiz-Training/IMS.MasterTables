using MasterTables.Application.Commands;
using MasterTables.Domain.Exceptions;
using MasterTables.Domain.Interfaces;
using MediatR;

namespace MasterTables.Application.CommandHandlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductByIdAsync(request.Id, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException("Product not found");
            }

            await _repository.DeleteProductAsync(product, cancellationToken);
            return true;
        }
    }
}
