using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterTables.Application.Commands
{
    public class DeleteLocationCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
