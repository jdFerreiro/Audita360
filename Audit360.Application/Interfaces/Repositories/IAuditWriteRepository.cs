using Audit360.Domain.Entities;
using System.Threading.Tasks;

namespace Audit360.Application.Interfaces.Repositories
{
    public interface IAuditWriteRepository : IWriteRepository<Audit>
    {
    }
}
