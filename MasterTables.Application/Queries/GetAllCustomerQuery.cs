using MediatR;
using MasterTables.Application.DTOs;
using System.Collections.Generic;

namespace MasterTables.Application.Queries
{
    public class GetAllCustomerQuery : IRequest<IEnumerable<CustomerDto>>
    {
    }
}
