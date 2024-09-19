using MediatR;
using MasterTables.Application.Commands;
using MasterTables.Domain.Interfaces;
using MasterTables.Domain.Exceptions;

namespace MasterTables.Application.CommandHandlers
{
    public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, bool>
    {
        private readonly ILocationRepository _repository;

        public DeleteLocationCommandHandler(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetLocationByIdAsync(request.Id, cancellationToken);
            if (customer == null)
            {
                throw new LocationNotFoundException("Location not found");
            }

            await _repository.DeleteLocationAsync(customer, cancellationToken);
            return true;
        }
    }
}
