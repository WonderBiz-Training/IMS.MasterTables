using MasterTables.Application.DTOs;
using MasterTables.Application.Queries;
using MasterTables.Domain.Interfaces;
using MediatR;

namespace MasterTables.Application.QueryHandlers
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _repository;

        public GetAllProductQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllProductsAsync(cancellationToken);

            return products.Select(c => new ProductDto
            {
                Id = c.Id,
                ProductName = c.ProductName,
                Price = c.Price,
                Code = c.Code,
            });
        }
    }
}
