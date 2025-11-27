using Audit360.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audit360.Application.Interfaces.Repositories
{
    public interface IAuditReadRepository : IReadRepository<Audit>
    {
        // read-specific methods
    }
}
